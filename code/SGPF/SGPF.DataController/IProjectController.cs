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

        Task<Project> GetById(BasePerson person, int id);

        Task SendToDispatchQueue(BasePerson person, Project project);

        Task Archive(BasePerson person, Project project);

        Task AddTechnicalOpinion(BasePerson technician, Project project, String comment, TechnicalOpinion opinion);

        Task AddCommiteeDispatch(BasePerson person, Project project, TechnicalOpinion opinion, FinancialManager manager = null);

        Task Suspend(BasePerson person, Project project);

        Task Resume(BasePerson person, Project project);

        Task<IEnumerable<Promoter>> GetPromoters();
    }
}
