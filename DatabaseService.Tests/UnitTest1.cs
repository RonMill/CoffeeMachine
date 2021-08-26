using NUnit.Framework;
using KaffeemaschineWPF.Models;
using System.Collections.Generic;

namespace DatabaseService.Tests
{
    public class Tests
    {
        private DatabaseManager databaseManager;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            databaseManager = new DatabaseManager();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateDatabaseTabelTest()
        {
            databaseManager.CreateDatabaseTable();
        }

        private static List<TestCaseData> User => new()
        {
            new TestCaseData(new User
            {
                FirstName = "Ronny",
                LastName = "Milling"
            }),
            new TestCaseData(new User
            {

            }),
            new TestCaseData(new User
            {
                 FirstName="qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq" +
                "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq" +
                "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq"
            })
        };

        //private static List<TestCaseData> user
        //{
        //    get
        //    {
        //        var list = new List<TestCaseData>();

        //        var user = new User();
        //        user.FirstName = "Ronny";
        //        user.LastName = "Milling";
        //        var testCaseData = new TestCaseData(user);
        //        list.Add(testCaseData);

        //        list.Add(new TestCaseData(new User
        //        {

        //        }));
        //        list.Add(new TestCaseData(new User
        //        {
        //            FirstName = "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq" +
        //            "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq" +
        //            "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq"
        //        }));
        //        return list;
        //    }
        //} 

        [TestCaseSource(nameof(User))]
        //[TestCase("Hallo", 3)]
        //[TestCase("Welt", 2)]
        public void AddUserTest(User user)
        {
            int a = databaseManager.AddUser(user);
            Assert.That(a == 1,"Falscher Wert");
        }

        [TearDown]
        public void TearDown()
        {

        }
        
    }
}