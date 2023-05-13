using ContosoCrafts.WebSite.Controllers;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Pages
{
    public class AboutTests
    {
        #region TestSetup
        public static AboutModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<AboutModel>>();
            pageModel = new AboutModel(MockLoggerDirect)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        [Test]
        public void OnGet_Should_Return_Page_Result_And_Valid_Model()
        {
            // Arrange

            // Act
            var validModel = pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }
        #endregion OnGet
    }
}
