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
    }
}
