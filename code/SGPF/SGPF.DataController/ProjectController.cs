using SGPF.Data;
using SGPF.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.DataController
{
    public class ProjectController : IProjectController
    {
        private static readonly string
            _onProjectCreatedMessageTemplate = "created: {0}",
            _onProjectSearchMessageTemplate = "searched",
            _onStateChangedMessageTemplate = "new state: {0}",
            _onTechnicalOpinionMessageTemplate = "tech. opinion: {0} - {1}",
            _onSuspensionStateChangeMessageTemplate = "suspended: {1}";
        
        private readonly ISGPFDatabase _db;

        public ProjectController(ISGPFDatabase db)
        {
            this._db = db;
        }

        public async Task Create(Person person, Data.Project project)
        {
            project.Id = _db.GenerateProjectId();

            await _db.Projects.Add(project);
            await SendToDispatchQueue(person, project);

            AddToHistory(person, project, _onProjectCreatedMessageTemplate, project.Id);
        }

        public async Task<Data.Project> GetById(Person person, int id)
        {
            Project proj = await _db.Projects.Get(id);
            AddToHistory(person, proj, _onProjectSearchMessageTemplate);
            return proj;
        }

        public async Task SendToDispatchQueue(Person person, Data.Project project)
        {
            throw new NotImplementedException();
        }

        public async Task Archive(Person person, Data.Project project)
        {
            SetState(person, project, ProjectState.Archived);
        }

        public async Task AddTechnicalOpinion(Person person, Data.Project project, string comment, Data.TechnicalOpinion opinion)
        {
            AddToHistory(person, project, _onTechnicalOpinionMessageTemplate, opinion.ToString(), comment);

            if (opinion == TechnicalOpinion.Reject)
            {
                await Archive(person, project);
                return;
            }

            await SendToDispatchQueue(person, project);
        }

        public Task AddDispatch(Person person, Data.Project project, Data.TechnicalOpinion opinion)
        {
            
        }

        public async Task Suspend(Person person, Data.Project project)
        {
            SetSuspensionState(person, project, true);
        }

        public async Task Resume(Person person, Data.Project project)
        {
            SetSuspensionState(person, project, false);
        }

        private void SetSuspensionState(Person person, Project project, bool suspend) 
        {
            project.IsSuspended = suspend;
            AddToHistory(person, project, _onSuspensionStateChangeMessageTemplate, suspend);
        }

        private void SetState(Person person, Project proj, Data.ProjectState state)
        {
            proj.State = state;
            AddToHistory(person, proj, _onStateChangedMessageTemplate, state.ToString());
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
