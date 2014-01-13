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
            if (project.State == ProjectState.Rejected)
                throw new RejectedProjectException(project);
            
            if (project.IsSuspended)
                throw new SuspendedProjectException(project);

            SetState(person, project, ProjectState.AwaitingDispatch);
        }

        public async Task Archive(BasePerson person, Data.Project project)
        {
            if (project.IsSuspended)
                throw new SuspendedProjectException(project);

            SetState(person, project, ProjectState.Archived);
        }

        public async Task AddTechnicalOpinion(BasePerson person, Data.Project project, string comment, Data.TechnicalOpinion opinion)
        {
            if (project.IsSuspended)
                throw new SuspendedProjectException(project);

            AddToHistory(person, project, _onTechnicalOpinionMessageFormat, opinion.ToString(), comment);

            if (opinion == TechnicalOpinion.Reject)
            {
                await Archive(person, project);
                return;
            }

            await SendToDispatchQueue(person, project);
        }

        public async Task AddCommiteeDispatch(BasePerson person, Data.Project project, Data.TechnicalOpinion opinion, FinancialManager manager = null)
        {
            if (project.State == ProjectState.Rejected)
                throw new RejectedProjectException(project);
            
            if (project.IsSuspended)
                throw new SuspendedProjectException(project);

            if (project.State != ProjectState.AwaitingDispatch)
                throw new InvalidStateException(project.State);
            
            AddToHistory(person, project, _onAddDispatchMessageFormat, opinion.ToString());

            switch (opinion)
            {
                case TechnicalOpinion.Approve:

                    if (manager == null)
                    {
                        SetState(person, project, ProjectState.InPayment);
                    }
                    else
                    {
                        project.Manager = manager;
                        AddToHistory(person, project, _onAssignedFinancialManagerMessageFormat, project.Manager.Id, project.Manager.Name);
                    }

                    break;

                case TechnicalOpinion.Reject:
                    SetState(person, project, ProjectState.Rejected);
                    break;

                case TechnicalOpinion.ConvertToLoan:
                    project.Type = ProjectType.Loan;
                    AddToHistory(person, project, "converted to loan");
                    break;
            }
        }

        public async Task Suspend(BasePerson person, Data.Project project)
        {
            if (project.State == ProjectState.Rejected) 
                throw new RejectedProjectException(project);
            
            if (project.IsSuspended || project.State == ProjectState.Undefined)
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
            if (project.State == ProjectState.Rejected)
                throw new RejectedProjectException(project);
            
            ///
            /// When a project is suspended it have to be resumed by the user that suspend it.
            ///
            if (project.IsSuspended && !person.Equals(project.SuspendedBy))
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
                Description = String.Format("[{0}] {1} - {2}", person.Id, person.Name, String.Format(template, parameters))
            });
        }


        public async Task<IEnumerable<Project>> GetProjectsFor(BasePerson person)
        {
            if(typeof(FinancialManager).Equals(person.GetType())) 
            {
                return (await _db.Projects.All()).Where(p => person.Equals(p.Manager));
            }
            
            if(typeof(FinantialCommitteeMember).Equals(person.GetType())) 
            {
                return (await _db.Projects.All()).Where(p => p.State.Equals(ProjectState.AwaitingDispatch));
            }

            return Enumerable.Empty<Project>();
        }
    }
}
