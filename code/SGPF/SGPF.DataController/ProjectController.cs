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
        private static const string
            _onProjectCreatedMessageFormat = "created: {0}",
            _onProjectSearchMessageFormat = "searched",
            _onStateChangedMessageFormat = "new state: {0}",
            _onAddDispatchMessageFormat = "dispatch: {0}",
            _onAssignedFinancialManagerMessageFormat = "financial manager: [{0}] {1}",
            _onTechnicalOpinionMessageFormat = "tech. opinion: {0} - {1}",
            _onSuspensionStateChangeMessageFormat = "suspended: {1}";
        
        private readonly ISGPFDatabase _db;

        public ProjectController(ISGPFDatabase db)
        {
            this._db = db;
        }

        public async Task Create(Person person, Data.Project project)
        {
            project.Id = _db.GenerateProjectId();

            await _db.Projects.Add(project);

            AddToHistory(person, project, _onProjectCreatedMessageFormat, project.Id);
        }

        public async Task<Data.Project> GetById(Person person, int id)
        {
            Project proj = await _db.Projects.Get(id);
            AddToHistory(person, proj, _onProjectSearchMessageFormat);
            return proj;
        }

        public async Task SendToDispatchQueue(Person person, Data.Project project)
        {
            if (project.IsSuspended)
                throw new SuspendedProjectException(project);
            
            SetState(person, project, ProjectState.AwaitingDispatch);
        }

        public async Task Archive(Person person, Data.Project project)
        {
            if (project.IsSuspended)
                throw new SuspendedProjectException(project);

            SetState(person, project, ProjectState.Archived);
        }

        public async Task AddTechnicalOpinion(Person person, Data.Project project, string comment, Data.TechnicalOpinion opinion)
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

        public async Task AddDispatch(Person person, Data.Project project, Data.TechnicalOpinion opinion)
        {
            if (project.IsSuspended)
                throw new SuspendedProjectException(project);

            AddToHistory(person, project, _onAddDispatchMessageFormat, opinion.ToString());

            switch (opinion) 
            {
                case TechnicalOpinion.Approve:

                    if (project.AssignedTecnitian == null)
                    {
                        project.AssignedTecnitian = (await _db.Persons.All()).OfType<FinancialManager>().Random();
                        AddToHistory(person, project, _onAssignedFinancialManagerMessageFormat, project.AssignedTecnitian.Id, project.AssignedTecnitian.Name);
                        SetState(person, project, ProjectState.WaitingForTechnicalOpinion);
                    }
                    else 
                    {
                        SetState(person, project, ProjectState.InPayment);
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

        public async Task Suspend(Person person, Data.Project project)
        {
            if (project.IsSuspended)
                throw new SuspendedProjectException(project);

            // TODO project.SuspendedBy = person;
            SetSuspensionState(person, project, true);
        }

        public async Task Resume(Person person, Data.Project project)
        {
            SetSuspensionState(person, project, false);
        }

        private void SetSuspensionState(Person person, Project project, bool suspend) 
        {
            project.IsSuspended = suspend;
            AddToHistory(person, project, _onSuspensionStateChangeMessageFormat, suspend);
        }

        public void SetState(Person person, Project proj, Data.ProjectState state)
        {
            proj.State = state;
            AddToHistory(person, proj, _onStateChangedMessageFormat, state.ToString());
        }

        private void AddToHistory(Person person, Project project, string template, params object[] parameters) 
        {
            project.History.Add(new ProjectHistory()
            {
                ProjectId = project.Id,
                Date = System.DateTime.Now,
                Description = String.Format("[{0}] {1} - {2}", person.Id, person.Name, String.Format(template, parameters))
            });
        }
    }
}
