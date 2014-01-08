using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGPF.Data;

namespace SGPF.DataController
{
    public interface IAuthenticationService
    {
        Task<UserSession> StartSession(string id);

        Task EndSession(UserSession session);
    }
}
