using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Bukimedia.PrestaSharp
{
    /// <summary>
    /// XML Extension Methods
    /// </summary>
    public static class RestSharpExtensions
    {
        /// <summary>
        /// Returns the name of an element with the namespace if specified
        /// </summary>
        /// <param name="name">Element name</param>
        /// <param name="namespace">XML Namespace</param>
        /// <returns></returns>
        public static XName AsNamespaced(this string name, string @namespace)
        {
            XName xName = name;

            if (@namespace.HasValue())
                xName = XName.Get(name, @namespace);

            return xName;
        }

        /// <summary>
        /// Check that a string is not null or empty
        /// </summary>
        /// <param name="input">String to check</param>
        /// <returns>bool</returns>
        public static bool HasValue(this string input) => !string.IsNullOrEmpty(input);

        public static string ToCamelCase(this string lowercaseAndUnderscoredWord, CultureInfo culture)
            => MakeInitialLowerCase(ToPascalCase(lowercaseAndUnderscoredWord, culture), culture);

        /// <summary>
        /// Convert the first letter of a string to lower case
        /// </summary>
        /// <param name="word">String to convert</param>
        /// <param name="culture"></param>
        /// <returns>string</returns>
        public static string MakeInitialLowerCase(this string word, CultureInfo culture) => string.Concat(word.Substring(0, 1).ToLower(culture), word.Substring(1));

        public static string ToPascalCase(this string lowercaseAndUnderscoredWord, CultureInfo culture)
            => ToPascalCase(lowercaseAndUnderscoredWord, true, culture);

        public static string ToPascalCase(this string text, bool removeUnderscores, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            text = text.Replace('_', ' ');

            var joinString = removeUnderscores ? string.Empty : "_";
            var words = text.Split(' ');

            return words
                .Where(x => x.Length > 0)
                .Select(CaseWord)
                .JoinToString(joinString);

            string CaseWord(string word)
            {
                var restOfWord = word.Substring(1);
                var firstChar = char.ToUpper(word[0], culture);

                if (restOfWord.IsUpperCase())
                    restOfWord = restOfWord.ToLower(culture);

                return string.Concat(firstChar, restOfWord);
            }
        }

        internal static string RemoveUnderscoresAndDashes(this string input) => input.Replace("_", "").Replace("-", "");

        static bool IsUpperCase(this string inputString) => IsUpperCaseRegex.IsMatch(inputString);

        internal static string JoinToString<T>(this IEnumerable<T> collection, string separator, Func<T, string> getString)
            => JoinToString(collection.Select(getString), separator);

        internal static string JoinToString(this IEnumerable<string> strings, string separator) => string.Join(separator, strings);

        static readonly Regex IsUpperCaseRegex = new(@"^[A-Z]+$");
    }
}
