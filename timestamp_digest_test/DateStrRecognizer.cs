using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace timestamp_digest_test
{
    public class DateStrRecognizer
    {
        // Idea:
        // Store a list of potential date format
        // Store another list of keywords that is related to date
        // Assumption: if a string contains a related keyword
        //  then it is likely to also have date string in it
        // 1. filter list of input strings with keywords
        //  to get two lists: A and B
        //  list A will contain strings that likely to have date string in it
        //  and strings in list B are less likely to have date string
        // 2. search list A for date strings
        // 3. if date string is found then great
        //  else search list B
        // NOTE: Assumes only one date string should be returned
        //      If we need to return all date string then just search all strings
        //string[] keywords = { };

        //static string[,] formats = { { date format, regex } }
        static Dictionary<string, string> regexByFormat = new Dictionary<string, string> {
            { "MM/dd/yyyy", "(1[0-2]|0[1-9])/(3[0-1]|[1-2][0-9]|0[1-9])/([0-9]{4})" },
            { "M/d/yyyy", "(1[0-2]|[1-9])/(3[0-1]|[1-2][0-9]|[1-9])/([0-9]{4})"},
            { "MMM dd, yyyy", "(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec) (3[0-1]|[1-2][0-9]|0[1-9]), ([0-9]{4})" },
            { "MMM d, yyyy", "(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec) (3[0-1]|[1-2][0-9]|[1-9]), ([0-9]{4})" },
            { "MM-dd-yyyy", "(1[0-2]|0[1-9])-(3[0-1]|[1-2][0-9]|0[1-9])-([0-9]{4})" },
            { "M-d-yyyy", "(1[0-2]|[1-9])-(3[0-1]|[1-2][0-9]|[1-9])-([0-9]{4})" },
            { "MM dd yyyy", "(1[0-2]|0[1-9]) (3[0-1]|[1-2][0-9]|0[1-9]) ([0-9]{4})" },
            { "M d yyyy", "(1[0-2]|[1-9]) (3[0-1]|[1-2][0-9]|[1-9]) ([0-9]{4})" },
            { "yyyy MM dd", "([0-9]{4}) (1[0-2]|0[1-9]) (3[0-1]|[1-2][0-9]|0[1-9])" },
            { "yyyy M d",   "([0-9]{4}) (1[0-2]|[1-9]) (3[0-1]|[1-2][0-9]|[1-9])" },
            { "yyyy-MM-dd", "([0-9]{4})-(1[0-2]|0[1-9])-(3[0-1]|[1-2][0-9]|0[1-9])" },
            { "yyyy-M-d",   "([0-9]{4})-(1[0-2]|[1-9])-(3[0-1]|[1-2][0-9]|[1-9])" },
            { "yyyy/MM/dd", "([0-9]{4})-(1[0-2]|0[1-9])-(3[0-1]|[1-2][0-9]|0[1-9])" },
            { "yyyy/M/d",   "([0-9]{4})-(1[0-2]|[1-9])-(3[0-1]|[1-2][0-9]|[1-9])" }
        };

        // To generate new regex for date format dynamically
        // Proof of concept code:
        //static Dictionary<string, string> regexPartByKeyword = new Dictionary<string, string>
        //{
        //    { "MMM", "Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec" },
        //    { "MM", "1[0-2]|0[1-9]" },
        //    { "M",  "1[0-2]|[1-9]" },
        //    { "dd", "3[0-1]|[1-2][0-9]|0[1-9]" },
        //    { "d",   "3[0-1]|[1-2][0-9]|[1-9]" },
        //    { "y", "[0-9]" }
        //};
        //var regexList = new List<string>();
        //var i = 0;
        //    foreach (var format in formats) {
        //        var formatRegex = format;
        //var partById = new Dictionary<String, String>();
        //        foreach (var pair in regexPartByKeyword) {
        //            var partId = "@" + i;
        //formatRegex = formatRegex.Replace(pair.Key, partId);
        //        partById[partId] = pair.Value;
        //    }
        //    foreach (var pair in partById) {
        //        formatRegex = formatRegex.Replace(pair.Key, pair.Value);
        //    }
        //}

        public static string FindDateStr(string[] inputs) {
            foreach (var str in inputs) {
                foreach (var pair in regexByFormat)
                {
                    var m = Regex.Match(str, pair.Value);
                    if (!m.Success) continue;

                    var dateStr = m.Value;
                    var format = pair.Key;
                    var dt = DateTime.Parse(dateStr);
                    return dt.ToString(format);
                }
            }
            return null;
        }
    }
}
