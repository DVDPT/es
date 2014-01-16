using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.DataController.Exceptions
{
    public class TecnicalDispatchException : Exception
    {
        public TecnicalDispatchException() : base("Please insert your opinion") { }
    }
}
