using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;

namespace SGPF.ViewModel
{
    public class BaseAppViewModel : ViewModelBase
    {

        protected async void SafeRun(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
