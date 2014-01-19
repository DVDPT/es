using SGPF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.DataController.Exceptions
{
    public class RejectedProjectException : Exception
    {
        public RejectedProjectException(Project project)
            : base(string.Format(ErrorConstants.RejectedProjectExceptionFormat, project.Id))
        { }
    }
}
