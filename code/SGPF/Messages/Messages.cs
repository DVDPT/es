using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGPF.Data;

namespace SGPF.Messages
{
    public class ProjectMessage
    {
        public Project Project { get; set; }
        public BasePerson UserDetails { get; set; }


        public ProjectMessage(BasePerson userDetails, Project project)
            : this(userDetails)
        {
            Project = project;
        }

        public ProjectMessage(BasePerson userDetails)
        {
            UserDetails = userDetails;
        }
    }

    public class SessionMessage
    {
        public UserSession Session { get; set; }
    }
}
