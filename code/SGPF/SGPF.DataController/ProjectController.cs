﻿using SGPF.Data;
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
        private readonly ISGPFDatabase _db;

        public ProjectController(ISGPFDatabase db) 
        {
            this._db = db;
        }

        public async Task Create(Data.Project project)
        {
            project.Id = _db.GenerateProjectId();
            
            await _db.Projects.Add(project);
            await SendToDispatchQueue(project);
        }

        public async Task<Data.Project> GetById(int id)
        {
            await _db.Projects.Get(id);
        }

        public async Task SendToDispatchQueue(Data.Project project)
        {
            throw new NotImplementedException();
        }

        public async Task Archive(Data.Project project)
        {
            SetState(project, ProjectState.Archived);
        }

        public Task AddTechnicalOpinion(Data.Project project, string comment, Data.TechnicalOpinion opinion)
        {
            throw new NotImplementedException();
        }

        public Task AddDispatch(Data.Project project, Data.TechnicalOpinion opinion)
        {
            throw new NotImplementedException();
        }

        public Task Suspend(Data.Project project)
        {
            
        }

        public Task Resume(Data.Project project)
        {
            throw new NotImplementedException();
        }

        private void SetState(Project proj, Data.ProjectState state) 
        {
            
        }
    }
}
