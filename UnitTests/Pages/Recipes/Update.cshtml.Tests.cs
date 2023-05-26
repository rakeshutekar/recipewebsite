using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Pages.Recipes;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Pages.Recipes
{
    /// <summary>
    /// Test class for the update page unit tests
    /// </summary>
    public class UpdateTests
    {
        #region TestSetup
        // A static page from the UpdateModel class
        public static UpdateModel pageModel;

        /// <summary>
        /// Initializes the test by calling the TestHelper class and creating a static page
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new UpdateModel(TestHelper.RecipeService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Test the 'OnGet' method with a valid recipe model
        /// Check that the model state is valid and the recipe title is correct
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Recipes()
        {
            // Arragne
            var validRecipeId = 2;
            var validRecipe = TestHelper.RecipeService.GetRecipe(validRecipeId);
            var validRecipeTitle = validRecipe.Title;
            var result = pageModel.OnGet(validRecipe.RecipeID);
            // Act

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(validRecipeTitle, pageModel.Recipe.Title);

            // Check if the recipe is not null after the one get
            var recipeNotNull = pageModel.Recipe != null;
            Assert.AreEqual(true, recipeNotNull);
        }

        /// <summary>
        /// Unit tests that ensures deleted receipes when attempting to access
        /// the page returns to the error page
        /// </summary>
        [Test]
        public void OnGet_Deleted_Recipes_Should_Redirect_To_Error_Page()
        {
            // Arrange
            TestHelper.RecipeService.DeleteRecipe(1);
            var deletedID = 1;

            // Act
            var pageResult = pageModel.OnGet(deletedID) as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageResult.PageName.Contains("NotFound"));
        }

        /// <summary>
        /// Unit test to ensure that invalid recipe ID return a recipe not found
        /// of true value
        /// </summary>
        [Test]
        public void OnGet_Invalid_RecipeId_Should_Redirect_To_Error_Page()
        {
            // Arrange
            var numRecipes = TestHelper.RecipeService.GetRecipes().Count();
            var invalidRecipeId = numRecipes + 1;

            // Act
            var pageResult = pageModel.OnGet(invalidRecipeId) as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageResult.PageName.Contains("NotFound"));
        }

        #endregion OnGet

        #region OnPost
        /// <summary>
        /// Test the 'OnPost'Method
        /// Check that the model state is valid and it redirects to page name
        /// contains "Read"
        /// </summary>
        [Test]
        public void OnPost_Valid_Model_Should_Update_Recipe_And_Redirect_Page()
        {
            // Arrange
            pageModel.Recipe  = new RecipeModel
            {
                RecipeID = 1,
                Title = "Test",
                Image = "URL",
                Description = "Test",
                Ingredients = new string[] { "new ingredient", "new ingredient" },
                Instructions = new string[] { "new instruction", "new instruction" },
                Tags = new string[] { "TEST" },
            };

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Read"));
        }

        /// <summary>
        /// Test 'OnPost' with an invalid model
        /// Check that the model state is not valid
        /// </summary>
        [Test]
        public void OnPost_Invalid_Model_ActionResult_Should_Return_False()
        {
            // Arrange
            // Force an invalid error state
            pageModel.ModelState.AddModelError("error", "error");

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);   
        }

        #endregion OnPost
    }
}