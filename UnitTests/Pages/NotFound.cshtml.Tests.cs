using ContosoCrafts.WebSite.Pages;
using NUnit.Framework;

namespace UnitTests.Pages
{
    /// <summary>
    /// NotFoundTests handle the tests for the not found page
    /// </summary>
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
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Tests that NotFoundModel.type correctly gets / sets type value
        /// </summary>
        [Test]
        public void NotFound_Type_Should_Get_Set_Type()
        {
            // Set page model type using set
            pageModel.type = NotFoundTypes.Recipe;
            // Assert page model type is equal to set value
            Assert.AreEqual(pageModel.type, NotFoundTypes.Recipe);
        }
        /// <summary>
        /// Tests that response get / set correctly gets and sets response
        /// </summary>
        [Test]
        public void NotFound_Response_Should_Get_Set_Response()
        {
            const string response = "TEST"; // TEST value
            // Set page model response using set
            pageModel.response = response;
            // assert page model response is equal to set value
            Assert.AreEqual(pageModel.response, response);
        }
        /// <summary>
        /// Tests that when OnGet is called model response is equal to RECIPE_NOT_FOUND_RESPONSE
        /// </summary>
        [Test]
        public void NotFound_OnGet_Type_Recipe_Response_Should_Be_Recipe_Not_Found_Response()
        {
            // Set page model response type
            pageModel.type = NotFoundTypes.Recipe;
            // Act - call OnGet
            pageModel.OnGet();
            // Assert - response is equal to not found response
            Assert.AreEqual(pageModel.response, NotFoundModel.RECIPE_NOT_FOUND_RESPONSE);
        }
        /// <summary>
        /// Tests that when OnGet is called model response is equal to DEFAULT_NOT_FOUND_RESPONSE
        /// </summary>
        [Test]
        public void NotFound_OnGet_Type_Default_Response_Should_Be_Default_Not_Found_Response()
        {
            // Arrange - set page model response type to none
            pageModel.type = NotFoundTypes.None;
            // Act - Call OnGet
            pageModel.OnGet();
            // Assert - response is equal to default response
            Assert.AreEqual(pageModel.response, NotFoundModel.DEFAULT_NOT_FOUND_RESPONSE);
        }
        #endregion OnGet
    }
}