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
        Task<Project> Create(Project project);

        Task<Project> GetById(int id);

        Task SendToDispatchQueue(Project project);

        Task Archive(Project project);

        Task AddTechnicalOpinion(Project project, String comment, SGPF.Data.Manager.ManagerOpinion opinion);

        Task AddDispatch(Project project, );

        Task Suspend(Project project);

        Task Resume(Project project);

    }
}
