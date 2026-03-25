using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace FridgeBuddy.Converters;

public class EnumDescriptionConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Enum e)
            return value;
        
        return e.GetType().GetField(e.ToString())?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? e.ToString();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}