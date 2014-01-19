using SGPF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.DataController.Exceptions
{
    public class SuspendedProjectException : Exception
    {
        public SuspendedProjectException(Project project) : base(string.Format(ErrorConstants.SuspendedProjectExceptionFormat, project.Id)) 
        {
        
        }
    }
}
