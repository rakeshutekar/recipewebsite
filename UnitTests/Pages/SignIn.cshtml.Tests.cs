using ContosoCrafts.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace UnitTests.Pages
{
    /// <summary>
    /// Unit test class for testing the SignIn page
    /// </summary>
    public class SignInTests
    {
        #region TestSetup
        // mock page model used for testing
        public static SignInModel pageModel;

        /// <summary>
        /// Test initialize - performs initialization before running any tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<SignInModel>>();
            pageModel = new SignInModel(MockLoggerDirect);
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// OnGet method test to ensure page model is valid on OnGet call
        /// </summary>
        [Test]
        public void OnGet_Should_Return_Page_Result_And_Valid_Model()
        {
            // Act
            var validModel = pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }
        #endregion OnGet

        #region OnPost
        /// <summary>
        /// OnPost method test to ensure page model is valid on OnPost call
        /// </summary>
        [Test]
        public void OnPost_Should_Return_Page_Result_And_Valid_Model()
        {
            // Act
            var validModel = pageModel.OnPost();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }
        #endregion OnPost
    }
}