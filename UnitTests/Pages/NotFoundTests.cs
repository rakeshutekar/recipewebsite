using ContosoCrafts.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace UnitTests.Pages
{

    public class NotFoundTests
    {
        #region TestSetup
        public static NotFoundModel pageModel; // mock page model used for testing

        /// <summary>
        /// Test Initialize - performs initialization before running any tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new NotFoundModel()
            {
            };
        }

        [Test]
        public void NotFound_OnGet_Type_Should_Get_Set_Type()
        {
            // Set page model type using set
            pageModel.type = NotFoundTypes.Recipe;
            // Assert page model type is equal to set value
            Assert.AreEqual(pageModel.type, NotFoundTypes.Recipe);
        }

        [Test]
        public void NotFound_Response_Should_Get_Set_Response()
        {
            const string response = "TEST"; // TEST value
            // Set page model response using set
            pageModel.response = response;
            // assert page model response is equal to set value
            Assert.AreEqual(pageModel.response, response);
        }

        [Test]
        public void NotFound_OnGet_Type_Recipe_Response_Should_Be_Not_Found()
        {
            // Set page model response type
            pageModel.type = NotFoundTypes.Recipe;
            // Act - call OnGet
            pageModel.OnGet();
            // Assert - response is equal to not found response
            Assert.AreEqual(pageModel.response, NotFoundModel.RECIPE_NOT_FOUND_RESPONSE);
        }

        [Test]
        public void NotFound_OnGet_Type_Default_Response_Should_Be_Default()
        {
            // Arrange - set page model response type to none
            pageModel.type = NotFoundTypes.None;
            // Act - Call OnGet
            pageModel.OnGet();
            // Assert - response is equal to default response
            Assert.AreEqual(pageModel.response, NotFoundModel.RECIPE_DEFAULT_RESPONSE);
        }
        #endregion TestSetup
    }
}
