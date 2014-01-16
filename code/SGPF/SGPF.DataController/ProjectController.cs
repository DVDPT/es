using SGPF.Data;
using SGPF.Database;
using SGPF.DataController.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.DataController
{
    public class ProjectController : IProjectController
    {
        private const string
            _onProjectCreatedMessageFormat = "created: {0}",
            _onProjectSearchMessageFormat = "searched",
            _onStateChangedMessageFormat = "new state: {0}",
            _onAddDispatchMessageFormat = "dispatch: {0}",
            _onAssignedFinancialManagerMessageFormat = "financial manager: [{0}] {1}",
            _onTechnicalOpinionMessageFormat = "tech. opinion: {0} - {1}",
            _onSuspensionStateChangeMessageFormat = "suspended: {0}",
            _paymentMessageFormat = "payment: {0}, value{0}",
            _onProjectUpdateMessageFormat = "update project";

        private readonly ISGPFDatabase _db;

        public ProjectController(ISGPFDatabase db)
        {
            this._db = db;
        }

        public async Task Create(BasePerson person, Data.Project project)
        {
            project.Id = _db.GenerateProjectId();

            await _db.Projects.Add(project);

            AddToHistory(person, project, _onProjectCreatedMessageFormat, project.Id);
        }

        public async Task Open(BasePerson person, Project project)
        {
            SetState(person, project, ProjectState.Open);
        }

        public async Task Update(BasePerson person, Project project)
        {
            EnsurePreConditions(project);

            if (project.IsEditable == false)
                throw new UpdateProjectException();

            await _db.Projects.Update(project);
            AddToHistory(person, project, _onProjectUpdateMessageFormat);
        }

        public async Task<Data.Project> GetById(BasePerson person, int id)
        {
            Project proj = await _db.Projects.Get(id);
            AddToHistory(person, proj, _onProjectSearchMessageFormat);
            return proj;
        }

        public async Task SendToDispatchQueue(BasePerson person, Data.Project project)
        {
            EnsurePreConditions(project);


            SetState(person, project, ProjectState.AwaitingDispatch);
        }

        public async Task Archive(BasePerson person, Data.Project project)
        {
            EnsurePreConditions(project);


            SetState(person, project, ProjectState.Archived);
        }

        public async Task AddTechnicalOpinion(BasePerson manager, Data.Project project, ProjectTechnicalDispatch opinion)
        {
            EnsurePreConditions(project);

            if (opinion == null || opinion.Opinion == TechnicalOpinion.Undefined)
                throw new TecnicalDispatchException();

            AddToHistory(manager, project, opinion.ToString());

            project.TechnicalDispatch = opinion;

            await SendToDispatchQueue(manager, project);
        }

        public async Task ApproveProject(BasePerson member, Data.Project project)
        {
            EnsurePreConditions(project);

            if (project.State != ProjectState.AwaitingDispatch)
                throw new InvalidStateException(project.State);

            AddToHistory(member, project, _onAddDispatchMessageFormat, "approve");

            SetState(member, project, ProjectState.InPayment);

            AddToHistory(member, project, _onAssignedFinancialManagerMessageFormat, project.Manager.Id, project.Manager.Name);


        }

        private static void EnsurePreConditions(Project project)
        {

            if (project.State == ProjectState.Rejected)
                throw new RejectedProjectException(project);

            if (project.IsSuspended)
                throw new SuspendedProjectException(project);

            if(project.IsEditable == false)
                throw new UpdateProjectException();
           
        }

        public async Task Suspend(BasePerson person, Data.Project project)
        {

            if (project.State == ProjectState.Rejected)
                throw new RejectedProjectException(project);

            if (project.IsSuspended)
                throw new SuspendedProjectException(project);


            if (person is Technician || person is FinancialManager || person is FinantialCommitteeMember)
            {
                project.PrevSuspendedState = project.State;
                project.SuspendedBy = person;
                SetSuspensionState(person, project, true);
            }
        }

        public async Task Resume(BasePerson person, Data.Project project)
        {
            
            ///
            /// When a project is suspended it have to be resumed by the user that suspend it.
            ///
            if (project.IsSuspended == false || !person.Equals(project.SuspendedBy))
            {
                throw new InvalidResumeOperationException(project.SuspendedBy);
            }

            project.State = project.PrevSuspendedState;
            project.SuspendedBy = null;
            SetSuspensionState(person, project, false);
        }

        private void SetSuspensionState(BasePerson person, Project project, bool suspend)
        {
            AddToHistory(person, project, _onSuspensionStateChangeMessageFormat, suspend);
        }

        public void SetState(BasePerson person, Project proj, Data.ProjectState state)
        {
            proj.State = state;
            AddToHistory(person, proj, _onStateChangedMessageFormat, state.ToString());
        }

        private void AddToHistory(BasePerson person, Project project, string template, params object[] parameters)
        {
            project.History.Add(new ProjectHistory()
            {
                ProjectId = project.Id,
                Date = System.DateTime.Now,
                Description = String.Format("[{0}] {1} - {2}", person.Id, person.Name, parameters.Length == 0 ? template : String.Format(template, parameters))
            });
        }


        public async Task<IEnumerable<Project>> GetProjectsFor(BasePerson person)
        {
            if (typeof(FinancialManager) == person.GetType())
            {
                return (await _db.Projects.All()).Where(p => person.Equals(p.Manager));
            }

            if (typeof(FinantialCommitteeMember) == person.GetType())
            {
                return (await _db.Projects.All()).Where(p => !p.State.Equals(ProjectState.Rejected) || !p.State.Equals(ProjectState.Completed));
            }

            return Enumerable.Empty<Project>();
        }

        public async Task AddPayment(BasePerson person, Project project, ProjectPayment payment)
        {
            payment.ProjectId = project.Id;
            project.Payments.Add(payment);
            AddToHistory(person, project, _paymentMessageFormat, payment.PaymentDate, payment.Amount);
        }


        public async Task Reject(BasePerson member, Project project)
        {
            EnsurePreConditions(project);

            if(project.State == ProjectState.InPayment)
                throw new UpdateProjectException();

            SetState(member, project, ProjectState.Rejected);
        }
    }
}
