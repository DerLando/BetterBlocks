using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace RealBlocksUI.Converters
{
    public class AssemblyToImageConverter : IValueConverter
    {
        public static AssemblyToImageConverter Instance = new AssemblyToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageName = (bool) value ? 
                "assembly_icon.png" : 
                "root_icon.png";

            return new BitmapImage(new Uri($"pack://application:,,,/RealBlocksUI;component/Images/{imageName}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
