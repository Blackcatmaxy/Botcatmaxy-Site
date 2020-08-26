using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BotcatmaxySite.Client
{
    public static class UserUtilities
    {
        const string tokenRegex = @"&+access_token=(\S+?)&";

        public static string GetTokenFromURL(string URL)
        {
            var match = Regex.Match(URL, tokenRegex, RegexOptions.Singleline);
            if (!match.Success) return null;

            return match.Groups[1].Value;
        }
    }
}
