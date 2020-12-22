using System;
using System.Globalization;

namespace Questify.Builder.Services.TasksService.Interfaces
{
    public interface IConvertEnumToLocalizedString
    {
        string ConvertToString(Enum value, CultureInfo culture);
        string ConvertToString(Enum value);
    }
}
