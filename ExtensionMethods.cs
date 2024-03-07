using System.ComponentModel.DataAnnotations;
using System.Reflection;


internal static class ExtensionMethods
{
    public static string? GetDisplayName(this Enum EnumVal)
    {
        return EnumVal.GetType()?
            .GetMember(EnumVal.ToString())?
            .FirstOrDefault()?
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName();
    }
}