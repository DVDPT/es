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
        Task Create(Project project);

        Task<Project> GetById(int id);

        Task SendToDispatchQueue(Project project);

        Task Archive(Project project);

        Task AddTechnicalOpinion(Project project, String comment, TechnicalOpinion opinion);

        Task AddDispatch(Project project, TechnicalOpinion opinion);

        Task Suspend(Project project);

        Task Resume(Project project);

    }
}
