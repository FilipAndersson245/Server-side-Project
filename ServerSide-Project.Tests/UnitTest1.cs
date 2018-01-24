using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerSide_Project.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;

namespace ServerSide_Project.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ListBooks()
        {
            var Control = new ListController();
            var res = Control.ListBooks() as ViewResult;
            var model = res.Model as IEnumerable<Models.Book>;
            foreach(var item in model)
            {
                Assert.AreEqual("Tolkien", item.Author.LastName);
            }
        }
    }
}
