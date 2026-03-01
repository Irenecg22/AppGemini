using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace AppGemini.Converters
{
    public class RoleToBubbleAlignConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var role = value?.ToString()?.ToLowerInvariant();
            return role == "user" ? LayoutOptions.End : LayoutOptions.Start;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class RoleToBubbleMarginConverter : IValueConverter
    {
        // Ajusta estos números a tu gusto
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var role = value?.ToString()?.ToLowerInvariant();

            // usuario -> margen grande a la izquierda (para que la burbuja no ocupe todo)
            // modelo -> margen grande a la derecha
            return role == "user"
                ? new Thickness(80, 4, 0, 4)
                : new Thickness(0, 4, 80, 4);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class RoleToBubbleColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var role = value?.ToString()?.ToLowerInvariant();

            // Cambia colores si quieres
            return role == "user"
                ? Color.FromArgb("#5B4BFF")   // moradito para user
                : Color.FromArgb("#2B2F36");  // gris oscuro para model
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class RoleToTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => Colors.White;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}