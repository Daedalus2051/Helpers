using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    /// <summary>
    /// Contains methods related to the conversion or manipulation of strings.
    /// </summary>
    public class StringHelpers
    {
        /// <summary>
        /// Converts a collection of strings into a single string with newline character breaks.
        /// </summary>
        /// <param name="input">Collection of strings.</param>
        /// <returns></returns>
        public static string CollectionToString(IEnumerable<string> input)
        {
            if (input.Count() == 0) return "";

            string output = "";
            string last = input.Last();

            foreach (string element in input)
            {
                if (element == last)
                {
                    output += $"{element}";
                }
                else
                {
                    output += $"{element}{Environment.NewLine}";
                }
            }

            return output;
        }
        /// <summary>
        /// Given a reference string as input, replaces all occurrences of the desired keyword with the given variable replacement.
        /// </summary>
        /// <param name="variableName">The keyword to search for and replace with actual data.</param>
        /// <param name="replacement">The actual data that the variable should be replaced with.</param>
        /// <param name="input">The string containing the data you wish to replace.</param>
        public static void ParseVariable(string variableName, string replacement, ref string input)
        {
            input = input.Replace(variableName, replacement);
        }
        /// <summary>
        /// Given a reference string as input, replaces all occurrences of the desired keyword with the given variable replacement.
        /// </summary>
        /// <param name="variableList">A dictionary of name, value where name is the keyword and value is the variable data.</param>
        /// <param name="input">The string containing the data you wish to replace.</param>
        public static void ParseMultipleVariables(Dictionary<string, string> variableList, ref string input)
        {
            foreach (KeyValuePair<string, string> variable in variableList)
            {
                input = input.Replace(variable.Key, variable.Value);
            }
        }
    }
}
