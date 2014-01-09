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
    class PersonTypeToVisibilityConverter : IValueConverter
    {

        public PersonType TargetType { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = (BasePerson)value;

            return t.Type == TargetType ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
