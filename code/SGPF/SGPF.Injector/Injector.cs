using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using SGPF.Database;
using SGPF.Database.InMemory;

namespace SGPF.Injector
{
    public class Injector
    {
        public static void Configure(SimpleIoc container)
        {
            container.Register<ISGPFDatabase, InMemorySGPFDatabase>();
        }
    }
}
