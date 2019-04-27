using System;
using System.Collections.Generic;
using System.IO;
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
        public static string ParseVariable(string variableName, string replacement, string input)
        {
            string output = "";

            ParseVariable(variableName, replacement, ref input);
            output = input;

            return output;
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
        public static string ParseMultipleVariables(Dictionary<string, string> variableList, string input)
        {
            string output = "";

            ParseMultipleVariables(variableList, ref input);
            output = input;

            return output;
        }

        /// <summary>
        /// Searches through an array of strings in order to find key value pairs of command line arguments.
        /// </summary>
        /// <param name="args">String array of arguments passed from the command line.</param>
        /// <returns>Returns dictionary of string,string containing the passed command name and value.</returns>
        public static Dictionary<string, string> ParseCommandLineArgs(string[] args)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            if (args.Count() < 2) return arguments;

            // Check and/or parse any arguments passed to the executable
            for (int index = 0; index < args.Length; index++)
            {
                int nextIndex = index + 1;
                if (args[index].Contains("--"))
                {
                    string argName = args[index].Replace("--", "");

                    if (nextIndex >= args.Length) nextIndex--;

                    if (args[nextIndex].Contains("--"))
                    {
                        // if we have two commands in a row, either someone forgot a value or it's
                        // an operational command
                        arguments.Add(argName, "");
                    }
                    else
                    {
                        // if the command contains a value, extract it
                        string argValue = args[nextIndex];
                        arguments.Add(argName, argValue);
                    }
                }
            }

            return arguments;
        }
        public static Dictionary<string, string> ParseCommandLineArgs(IEnumerable<string> args)
        {
            return ParseCommandLineArgs(args.ToArray());
        }

        //public static Dictionary<string, Dictionary<string,string>> ReadIniSettings(string filePath)
        //{
        //    throw new NotImplementedException();

        //    if (!File.Exists(filePath)) throw new FileNotFoundException();
        //    Dictionary<string, Dictionary<string, string>> iniSettingsCollection = new Dictionary<string, Dictionary<string, string>>();

        //    var fileLines = File.ReadAllLines(filePath);
        //    foreach (var line in fileLines)
        //    {
        //        string sectionName = "";
        //        if (line.Contains('[') && line.Contains(']'))
        //        {
        //            sectionName = line.Replace("[", "").Replace("]", "");
        //        }
        //    }

        //    return iniSettingsCollection;
        //}
    }
}
