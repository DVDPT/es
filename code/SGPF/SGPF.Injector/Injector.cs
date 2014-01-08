using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using SGPF.Database;
using SGPF.Database.InMemory;
using SGPF.DataController;

namespace SGPF.Injector
{
    public class Injector
    {
        public static void Configure(SimpleIoc container)
        {
            container.Register<IMessenger, Messenger>();

            container.Register<ISGPFDatabase, InMemorySGPFDatabase>();
            container.Register<IAuthenticationService, MockAuthenticationService>();
        }
    }
}
