using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GaiaV2CustomActions.Extensions
{
    public static class StringExtensions
    {
        // Regex for splitting camel case string to separate words
        private const string c_camelCaseRegex = "(?<!^)(?=[A-Z])";

        /// <summary>
        /// Formats an HTML link to the given url.
        /// </summary>
        /// <param name="prettyName">Text to diaplay</param>
        /// <param name="url">Target URL</param>
        /// <returns>HTML link</returns>
        public static string CreateLink(string prettyName, string url)
        {
            return $"[{prettyName}]({url})";
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string CreateMailContact(string item)
        {
            const string c_singleUserContactPattern = @"(.*)\s+\(upn:\s*(.*?)\)";
            Regex singleUserContactRegex = new Regex(c_singleUserContactPattern);
            Match match = singleUserContactRegex.Match(item);

            if (match.Success)
            {
                return IsValidEmail(match.Groups[2].ToString()) ? $"\n* [{match.Groups[1]}](mailto:{match.Groups[2]})" : $"\n* {item}";
            }

            return $"\n* [{item}](mailto:{item})";

        }

        
    }
}
