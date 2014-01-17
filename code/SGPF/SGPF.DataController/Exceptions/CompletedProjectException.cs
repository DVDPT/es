using SGPF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.DataController.Exceptions
{
    public class CompletedProjectException : Exception
    {
        public CompletedProjectException(Project project) :
            base(string.Format(ErrorConstants.CompletedProjectFormat, project.Id))
        { }
    }
}
