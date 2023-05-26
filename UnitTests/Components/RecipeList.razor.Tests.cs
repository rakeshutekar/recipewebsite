using Bunit;
using NUnit.Framework;
using System.Linq;
using ContosoCrafts.WebSite.Components;
using ContosoCrafts.WebSite.Services;
using Microsoft.Extensions.DependencyInjection;
using Bunit.TestDoubles;

namespace UnitTests.Components
{
    /// <summary>
    /// This class contains unit tests for the RecipeList razor components.
    /// </summary>
    public class RecipeListTests: BunitTestContext
    {
        #region TestSetup
        [SetUp]
        /// <summary>
        /// This setup method is executed before each test method is executed.
        /// </summary>
        public void TestInitialize()
        {
            // Test Initialization
        }
        #endregion TestSetup

        [Test]
        /// <summary>
        /// This method tests that the RecipeList component correctly renders
        /// contents.
        /// </summary>
        public void RecipeList_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileRecipeService>(TestHelper.RecipeService);

            // Act
            var page = RenderComponent<RecipeList>();
            // Get the cards returned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("Beef Taco"));
        }

        /// <summary>
        /// This method tests that when the GoTo button is clicked the user is 
        /// redirected to the Recipe Page
        /// </summary>
        [Test]
        public void GoTo_When_Clicked_Should_Go_To_Recipe_Page()
        {
            // Arrange
            Services.AddSingleton<JsonFileRecipeService>(TestHelper.RecipeService);
            var ctx = new Bunit.TestContext();
            var navMan = Services.GetRequiredService<FakeNavigationManager>();

            // Act
            var page = RenderComponent<RecipeList>();
            var recipe = TestHelper.RecipeService.GetRecipes().FirstOrDefault();
            var recipeId = recipe.RecipeID;
            var htmlRecipeId = "recipe-" + recipeId.ToString();
            var buttonList = page.FindAll("button");

            var button = buttonList.First(x => x.Id == htmlRecipeId);

            button.Click();

            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, navMan.Uri.Contains("Recipe"));
        }
    }
}
