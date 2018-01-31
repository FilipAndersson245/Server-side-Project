using ServerSide_Project.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using ServerSide_Project.Models;

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
        public void ShortenerOfString()
        {
            var book = new Book { Description = "But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful. Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that produces no resultant pleasure?" };
            Assert.IsTrue(book.ShortDescription.Length < 55);
        }

    }
}
