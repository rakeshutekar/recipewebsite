using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Recipes;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UnitTests.Pages.Recipes
{
    /// <summary>
    /// The test class for the Create page unit tests
    /// </summary>
    public class CreateTests
    {
        #region TestSetup
        // Page model for testing
        public static CreateModel pageModel;

        /// <summary>
        /// Initialize test environment for Create Tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            ILogger<CreateModel> logger = new NullLogger<CreateModel>();
            pageModel = new CreateModel(logger, TestHelper.RecipeService)
            {
                Recipe = TestHelper.TEST_RECIPE_MODEL
            };
        }
        #endregion TestSetup

        #region OnPost
        /// <summary>
        /// Method checks that invalid models return an error
        /// </summary>
        [Test]
        public void OnPost_Invalid_Model_Should_Return_NotValid_Return_Page()
        {
            // Arrange
            // Force an invalid error state
            pageModel.ModelState.AddModelError("error", "error");

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }

        /// <summary>
        /// This method is a unit test that checks the behavior of a web page model's "OnPost" method
        /// when provided with valid recipe data.
        /// </summary>
        [Test]
        public async Task OnPost_Valid_Recipe_Data_Should_Save_To_Database_And_Redirect_To_Index()
        {
            // Arrange
            var recipeCountBefore = TestHelper.RecipeService.GetRecipes().Count();

            // Act
            var result = await Task.FromResult(pageModel.OnPost());

            // Assert
            var recipeCountAfter = TestHelper.RecipeService.GetRecipes().Count();
            Assert.AreEqual(recipeCountBefore + 1, recipeCountAfter);
            Assert.IsInstanceOf(typeof(RedirectToPageResult), result);
            var redirectResult = (RedirectToPageResult)result;
            Assert.AreEqual("/Index", redirectResult.PageName);

            return;
        }
        #endregion OnPost
    }
}