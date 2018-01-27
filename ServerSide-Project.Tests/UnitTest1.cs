using ServerSide_Project.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using ServerSide_Project.Models;

namespace ServerSide_Project.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Home()
        {
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();

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
    }
}
