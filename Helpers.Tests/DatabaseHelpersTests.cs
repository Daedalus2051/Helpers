using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helpers.Tests
{
    [TestClass]
    public class DatabaseHelpersTests
    {
        [TestMethod]
        public void ConnectionStringBuilder_ValidateUserAndPass()
        {
            //arrange 
            string expectedConnString = @"Data Source=localhost;Initial Catalog=Database1;Integrated Security=False;User ID=User1;Password=Pass123";


            //act 
            string actualConnString = DatabaseHelpers.ConnectionStringBuilder(
                serverName: "localhost",
                databaseName: "Database1",
                integratedSecurity: false,
                userName: "User1",
                password: "Pass123"
                );

            //assert
            Assert.AreEqual(expectedConnString, actualConnString);
        }

        [TestMethod]
        public void ConnectionStringBuilder_Validate()
        {
            //arrage
            string expectedConnString = @"Data Source=localhost;Initial Catalog=Database1;Integrated Security=True";

            //act
            string actualConnString = DatabaseHelpers.ConnectionStringBuilder(
                serverName: "localhost",
                databaseName: "Database1",
                integratedSecurity: true
                );

            //assert
            Assert.AreEqual(expectedConnString, actualConnString);
        }
    }
}
