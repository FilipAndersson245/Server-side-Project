using ServerSide_Project.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using Repository;
using Repository.Support;

namespace ServerSide_Project.Tests
{
    public class MockHttpSession : HttpSessionStateBase
    {
        Dictionary<string, object> _sessionDictonary = new Dictionary<string, object>();
        public override object this[string name] { get { return _sessionDictonary[name]; } set { _sessionDictonary[name] = value; } }
    }

    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Home()
        {
            var context = new Mock<ControllerContext>();
            var session = new Mock<MockHttpSession>();

            context.Setup(m => m.HttpContext.Session).Returns(session.Object);

            var controler = new HomeController
            {
                ControllerContext = context.Object
            };

            //controler.Session["repo"] = new Repository();

            var res = controler.Index() as ViewResult;
            Assert.AreEqual("Home", res.ViewName);
            

            //wip
        }

        [TestMethod]
        public void TestSearch()
        {
            List<BOOK> testSearch = EBook.GetBookSearchResultat("data", null);
            Assert.IsTrue(testSearch.Count > 3);

        }

    }
}
