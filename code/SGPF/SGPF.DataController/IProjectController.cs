using SGPF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.DataController
{
    public interface IProjectController
    {
        Task Create(BasePerson person, Project project);

        Task Open(BasePerson person, Project project);
        Task Update(BasePerson person, Project project);

        Task<Project> GetById(BasePerson person, int id);

        Task SendToDispatchQueue(BasePerson person, Project project);

        Task Archive(BasePerson person, Project project);

        Task Reject(BasePerson member, Project project);

        Task AddTechnicalOpinion(BasePerson manager, Project project, ProjectTechnicalDispatch opinion);

        Task ApproveProject(BasePerson member, Project project);

        Task Suspend(BasePerson person, Project project);

        Task Resume(BasePerson person, Project project);

        Task<IEnumerable<Project>> GetProjectsFor(BasePerson person);

        Task AddPayment(BasePerson person, Project project, ProjectPayment payment);
    }
}
