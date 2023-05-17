using ContosoCrafts.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace UnitTests.Pages
{
    public class IndexTests
    {
        #region TestSetup
        public static IndexModel pageModel; // mock page model used for testing

        /// <summary>
        /// Test Initialize - performs initialization before running any tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();
            pageModel = new IndexModel(MockLoggerDirect,TestHelper.RecipeService)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Test to ensure OnGet correctly gets recipes from recipe service and sets
        /// internal recipe model collection - recipe model collection should have
        /// same size as service collection and should have same recipes.
        /// </summary>
        [Test]
        public void OnGet_Should_Set_Recipes_To_All_Recipes_Retrieved_From_Recipe_Service()
        {
            // Arrange
            var recipes = TestHelper.RecipeService.GetRecipes();
            // Act
            pageModel.OnGet();
            // Assert
            Assert.AreEqual(pageModel.Recipes.Count(), recipes.Count());
            Assert.AreEqual(pageModel.Recipes.First().RecipeID, recipes.First().RecipeID);
        }
        #endregion OnGet
    }
}
