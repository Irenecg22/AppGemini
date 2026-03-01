using System.Globalization;

namespace AppGemini.Converters;

public class RoleToColumnConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // user -> columna 1 (derecha) | model -> columna 0 (izquierda)
        return value?.ToString() == "user" ? 1 : 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}