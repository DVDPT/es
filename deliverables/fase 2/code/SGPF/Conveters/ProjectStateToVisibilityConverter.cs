using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using SGPF.Data;

namespace SGPF.Conveters
{
    public class ProjectStateToVisibilityConverter : IValueConverter
    {
        public ProjectState State { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var project = (ProjectState) value;

            var cond = project == State;

            if (parameter != null)
                cond = !cond;

            return cond ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
