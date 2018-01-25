using ServerSide_Project.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;

namespace ServerSide_Project.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Home()
        {
            var controler = new HomeController();
            var res = controler.Index() as ViewResult;
            Assert.AreEqual("Home", res.ViewName);
            //wip
        }
    }
}
