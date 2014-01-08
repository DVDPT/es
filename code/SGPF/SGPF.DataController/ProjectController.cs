using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.DataController
{
	public class ProjectController : IProjectController
	{
        private readonly ISGPF
	    public Task<Data.Project> Create(Data.Project project)
        {
	        throw new NotImplementedException();
        }

        public Task<Data.Project> GetById(int id)
        {
	        throw new NotImplementedException();
        }

        public Task SendToDispatchQueue(Data.Project project)
        {
	        throw new NotImplementedException();
        }

        public Task Archive(Data.Project project)
        {
	        throw new NotImplementedException();
        }

        public Task AddTechnicalOpinion(Data.Project project, ? ?)
        {
	        throw new NotImplementedException();
        }

        public Task AddDispatch(Data.Project project, ? ?)
        {
	        throw new NotImplementedException();
        }

        public Task Suspend(Data.Project project)
        {
	        throw new NotImplementedException();
        }

        public Task Resume(Data.Project project)
        {
	        throw new NotImplementedException();
        }
}
}
