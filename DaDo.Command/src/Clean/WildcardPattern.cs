using System.Text.RegularExpressions;

namespace DaDo.Command
{
    class WildcardPattern : Regex
    {
        public WildcardPattern(string wildCardPattern)
            : base(ConvertPatternToRegex(wildCardPattern), RegexOptions.IgnoreCase)
        {
        }

        public WildcardPattern(string wildcardPattern, RegexOptions regexOptions)
            : base(ConvertPatternToRegex(wildcardPattern), regexOptions)
        {
        }

        private static string ConvertPatternToRegex(string wildcardPattern)
        {
            string patternWithWildcards = Regex.Escape(wildcardPattern).Replace("\\*", ".*");
            patternWithWildcards = string.Concat("^", patternWithWildcards.Replace("\\?", "."), "$");
            return patternWithWildcards;
        }
    }
}