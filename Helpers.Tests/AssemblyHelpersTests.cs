using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helpers.Tests
{
    [TestClass]
    public class AssemblyHelpersTests
    {
        [TestMethod]
        public void WriteResourceToFile_ExecutionTest()
        {
            //arrange 
            string testFilename = $"{Environment.CurrentDirectory}\\WriteResourceToFile_ExecutionTest.txt";
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string resourceName = $"{assemblyName}.Resources.TextFileTest.txt";
            int fileLen = 0;
            bool isFileCreated = false;
            if (File.Exists(testFilename))
                File.Delete(testFilename);

            //act 
            AssemblyHelpers.WriteResourceToFile(resourceName, testFilename);

            isFileCreated = File.Exists(testFilename);
            if (isFileCreated) fileLen = File.ReadAllText(testFilename).Length;

            //assert
            Assert.AreEqual(true, (isFileCreated && fileLen > 0) );
        }

        [TestMethod]
        public void GetResourceStream_ExecutionTest()
        {
            //arrange
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string resourceName = $"{assemblyName}.Resources.TextFileTest.txt";
            string result = "";
            //bool isStreamNull;
            Stream resourceStream;

            //act
            resourceStream = AssemblyHelpers.GetResourceStream(resourceName);
            //if (resourceStream == null) isStreamNull = true;

            using (StreamReader reader = new StreamReader(resourceStream))
            {
                result = reader.ReadToEnd();
            }

            string expectedText = "";
            using (StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)))
            {
                expectedText = reader.ReadToEnd();
            }

            //assert
            Assert.AreEqual(expectedText, result);
        }

        [TestMethod]
        public void GetResourceString_ExecutionTest()
        {            
            //arrange
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string resourceName = $"{assemblyName}.Resources.TextFileTest.txt";
            string resourceString = "";

            //act
            resourceString = AssemblyHelpers.GetResourceString(resourceName);

            string expectedText = "";
            using (StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)))
            {
                expectedText = reader.ReadToEnd();
            }

            //assert
            Assert.AreEqual(expectedText, resourceString);
        }

        [TestMethod]
        public void GetVersion_ExecutionTest()
        {
            //arrange
            var assembly = Assembly.GetExecutingAssembly().GetName();
            string currentVersion = $"{assembly.Version.Major}.{assembly.Version.Minor}.{assembly.Version.Build}.{assembly.Version.Revision}";
            string actualVersion = "";

            //act
            actualVersion = AssemblyHelpers.GetVersion();

            //assert
            Assert.AreEqual(currentVersion, actualVersion);
        }
    }
}
