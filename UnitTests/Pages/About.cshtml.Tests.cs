using ContosoCrafts.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace UnitTests.Pages
{
    /// <summary>
    /// Test class for the About page unit tests
    /// </summary>
    public class AboutTests
    {
        #region TestSetup
        // Static AboutModel class used for interacting with the pages
        public static AboutModel pageModel;

        /// <summary>
        /// Initializes the test class with a mock logger
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<AboutModel>>();
            pageModel = new AboutModel(MockLoggerDirect)
            {
            };
        }
        #endregion TestSetup

        /// <summary>
        /// Tests that the about page is in a valid state when the page is requested
        /// via the OnGet method
        /// </summary>
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