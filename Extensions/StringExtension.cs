using System;
using System.Text.RegularExpressions;

namespace Slackiffy.Extensions
{
    public static class StringExtension
    {
        public static string ToSentenceCase(this string sourcestring)
        {
            if(String.IsNullOrEmpty(sourcestring)) return String.Empty;

            // start by converting entire string to lower case
            var lowerCase = sourcestring.ToLower();
            // matches the first sentence of a string, as well as subsequent sentences
            var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
            // MatchEvaluator delegate defines replacement of setence starts to uppercase
            var result = r.Replace(lowerCase, s => s.Value.ToUpper());

            return result;
        }
    }
}
