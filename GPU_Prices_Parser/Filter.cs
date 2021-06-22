using System.Text.RegularExpressions;

namespace GPU_Prices_Parser
{
    internal class Filter
    {
        public bool IsMatchRegex(string line)
        {
            var regex = new Regex(@"\w{3} \d{4}\s?(ti)?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matches = regex.Matches(line);
            return matches.Count > 0;
        }
    }
}