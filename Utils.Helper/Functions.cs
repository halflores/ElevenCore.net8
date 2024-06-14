using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utils.Helper
{
    public class Functions
    {
        public static string? GetDocEntry(string url)
        {
            string pattern = @"\(([0-9]{1,})\)";
            string input = url;
            RegexOptions options = RegexOptions.Multiline;

            var res = Regex.Matches(input, pattern, options);

            return res.Count > 0 ? res[0].Groups[1].Value.ToString() : null;

        }
    }
}
