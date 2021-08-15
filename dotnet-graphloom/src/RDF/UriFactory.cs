using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GraphLoom.Mapper.RDF
{
    public class URIFactory
    {
        private static Regex s_templateRegex = new Regex("{(.*?)}", RegexOptions.Compiled);

        public static Uri FromTemplate(string template, IDictionary<string, string> row)
        {
            MatchCollection matches = s_templateRegex.Matches(template);
            return new Uri(template.Replace(matches[0].Groups[0].Value, row[matches[0].Groups[1].Value]));
        }

        public static Uri FromString(string uri)
        {
            return new Uri(uri);
        }
    }
}
