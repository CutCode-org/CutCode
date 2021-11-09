using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace CutCode.CrossPlatform.Converters
{
    public class StringToFontFamily : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var font = (string) value;

            return FontFamily.Parse(font);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}