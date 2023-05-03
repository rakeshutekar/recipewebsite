using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Recipes;
using ContosoCrafts.WebSite.Pages; // Add this line
using System;
using ContosoCrafts.WebSite.Services;
using System.Linq;

namespace UnitTests.Pages.Recipes
{
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
        pageModel = new CreateModel(TestHelper.RecipeService)
        {
            Recipe = new Recipe()
            {
                ImgLink = "https://www.example.com/image.jpg",
                Title = "Test Recipe",
                Description = "This is a test recipe.",
                Ingredients = "Ingredient 1, Ingredient 2",
                Directions = "Step 1, Step 2"
            }
        };
    }

    #endregion TestSetup

    #region OnPostAsync
    /// <summary>
    /// This method is a unit test that checks the behavior of a web page model's "OnPostAsync" method
    /// when provided with valid recipe data.
    /// </summary>
    [Test]
    public async Task OnPostAsync_Valid_Recipe_Data_Should_Save_To_Database_And_Redirect_To_Index()
    {
        // Arrange
        var recipeCountBefore = TestHelper.RecipeService.GetRecipes().Count();

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        var recipeCountAfter = TestHelper.RecipeService.GetRecipes().Count();
        Assert.AreEqual(recipeCountBefore + 1, recipeCountAfter);
        Assert.IsInstanceOf(typeof(RedirectToPageResult), result);
        var redirectResult = (RedirectToPageResult)result;
        Assert.AreEqual("/Index", redirectResult.PageName);
    }
    #endregion OnPostAsync
}
}