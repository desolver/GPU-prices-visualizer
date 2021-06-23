namespace GPU_Prices_Parser.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string str)
        {
            if (str.Length == 0) return str;
            if (str.Length == 1) return str.ToUpperInvariant();
            
            return str[0].ToString().ToUpperInvariant() + str.Substring(1).ToLowerInvariant();
        }
    }
}