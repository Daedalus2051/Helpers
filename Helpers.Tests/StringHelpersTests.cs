using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helpers.Tests
{
    [TestClass]
    public class StringHelpersTests
    {
        [TestMethod]
        public void ParseVariable_VerifyTextReplaced()
        {
            // arrange
            string testData = @"This $data1 will be replaced.";
            string variableData = "special_string";
            string validResult = @"This special_string will be replaced.";

            // act
            StringHelpers.ParseVariable("$data1", variableData, ref testData);

            // assert
            Assert.AreEqual(validResult, testData);
        }

        [TestMethod]
        public void ParseMultipleVariables_VerifyTextReplaced()
        {
            // arrange
            string testData = @"This $data1 will be replaced, $data2 will also be replaced";
            Dictionary<string, string> variableData = new Dictionary<string, string>
            {
                { "$data1", "special_string" },
                { "$data2", "additional_string" }
            };
            string validResult = @"This special_string will be replaced, additional_string will also be replaced";

            // act
            StringHelpers.ParseMultipleVariables(variableData, ref testData);

            // assert
            Assert.AreEqual(validResult, testData);
        }

        [TestMethod]
        public void ParseVariable_ReturnStringVerify()
        {
            // arrange
            string testData = @"This $data1 will be replaced.";
            string variableData = "special_string";
            string validResult = @"This special_string will be replaced.";
            string output = "";

            // act
            output = StringHelpers.ParseVariable("$data1", variableData, testData);

            // assert
            Assert.AreEqual(validResult, output);
        }

        [TestMethod]
        public void ParseMultipleVariables_ReturnStringVerify()
        {
            // arrange
            string testData = @"This $data1 will be replaced, $data2 will also be replaced";
            Dictionary<string, string> variableData = new Dictionary<string, string>
            {
                { "$data1", "special_string" },
                { "$data2", "additional_string" }
            };
            string validResult = @"This special_string will be replaced, additional_string will also be replaced";
            string output = "";

            // act
            output = StringHelpers.ParseMultipleVariables(variableData, testData);

            // assert
            Assert.AreEqual(validResult, output);
        }

        [TestMethod]
        public void ParseCommandLineArgs_VerifyParseNormal()
        {
            // arrange
            var testData = new List<string> {
                "--command1", "value1",
                "--command2", "value2",
                "--command3", "value3"
            };
            var commands = new List<string>();
            var values = new List<string>();
            foreach(var item in testData)
            {
                if (item.Contains("--"))
                {
                    commands.Add(item);
                }
                else
                {
                    values.Add(item);
                }
            }
            
            // act
            var output = StringHelpers.ParseCommandLineArgs(testData.ToArray());

            bool expected = true;

            foreach(var command in commands)
            {
                if (!output.ContainsKey(command.Replace("--", ""))) expected = false;
            }
            foreach(var value in values)
            {
                if (!output.ContainsValue(value)) expected = false;
            }

            // assert
            Assert.AreEqual(true, expected);
        }

        [TestMethod]
        public void ParseCommandLineArgs_VerifySequentialCommands()
        {
            // arrange
            var testData = new List<string> {
                "--command1", "value1",
                "--command2", "--sequentialCommand",
                "--sequentialCommand1", "withValue",
                "--command3", "--sequentialCommand2"
            };
            var commands = new List<string>();
            var values = new List<string>();
            foreach (var item in testData)
            {
                if (item.Contains("--"))
                {
                    commands.Add(item);
                }
                else
                {
                    values.Add(item);
                }
            }

            // act
            var output = StringHelpers.ParseCommandLineArgs(testData.ToArray());

            bool expected = true;

            foreach (var command in commands)
            {
                if (!output.ContainsKey(command.Replace("--", ""))) expected = false;
            }
            foreach (var value in values)
            {
                if (!output.ContainsValue(value)) expected = false;
            }

            // assert
            Assert.AreEqual(true, expected);
        }

        [TestMethod]
        public void ParseCommandLineArgs_VerifyAllCommands()
        {
            // arrange
            var testData = new List<string> {
                "--command1", "--command2", "--sequentialCommand",
                "--sequentialCommand1", "--command3", "--sequentialCommand2"
            };
            var commands = new List<string>();
            var values = new List<string>();
            foreach (var item in testData)
            {
                if (item.Contains("--"))
                {
                    commands.Add(item);
                }
                else
                {
                    values.Add(item);
                }
            }

            // act
            var output = StringHelpers.ParseCommandLineArgs(testData.ToArray());

            bool expected = true;

            foreach (var command in commands)
            {
                if (!output.ContainsKey(command.Replace("--", ""))) expected = false;
            }
            foreach (var value in values)
            {
                if (!output.ContainsValue(value)) expected = false;
            }

            // assert
            Assert.AreEqual(true, expected);
        }
    }
}
