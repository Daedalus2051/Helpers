using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            string output = "";
            foreach (string element in input)
                output += element + Environment.NewLine;
            return output.TrimEnd(Environment.NewLine.ToCharArray());
        }
    }
}
