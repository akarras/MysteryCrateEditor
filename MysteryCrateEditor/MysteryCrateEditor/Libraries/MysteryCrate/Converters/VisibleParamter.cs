using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Converters
{
    public class VisibleParamter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string && parameter is string)
            {
                string stringParameter = (string)parameter;
                var parameterParts = stringParameter.Split(new char[] { '&', '|' });
                foreach(var part in parameterParts)
                {
                    if(value.ToString() == part)
                    {
                        return Visibility.Visible;
                    }
                }
                if (value.ToString() == parameter.ToString())
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
