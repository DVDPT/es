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
        Task Create(Person person, Project project);

        Task<Project> GetById(Person person, int id);

        Task SendToDispatchQueue(Person person, Project project);

        Task Archive(Person person, Project project);

        Task AddTechnicalOpinion(Person technician, Project project, String comment, TechnicalOpinion opinion);

        Task AddDispatch(Person person, Project project, TechnicalOpinion opinion);

        Task Suspend(Person person, Project project);

        Task Resume(Person person, Project project);

    }
}
