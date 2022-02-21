
namespace Smokeball.WPF.Extensions
{
    public static class StringExtensions
    {
        public static string ConvertToQueryParameter(this string value)
        {
            if (!string.IsNullOrEmpty(value)) 
            {
                var parts = value.Split(",");

                return parts.Length > 1 ? string.Join("+", parts) : value;

            }
            return string.Empty;
        }
    }
}
