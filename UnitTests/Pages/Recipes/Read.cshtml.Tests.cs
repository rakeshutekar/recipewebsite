using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Recipes;
using System;
using System.Linq;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Pages.Recipes
{
    /// <summary>
    /// The test class for the read page
    /// </summary>
    public class ReadTests
    {
        #region TestSetup
        // Page model for testing
        public static ReadModel pageModel;

        /// <summary>
        /// Initialize test environment for Read Tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new ReadModel(TestHelper.RecipeService)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// This method is a unit test that checks the behavior of a web page model's "OnGet" method
        /// when provided with a valid recipe ID.
        /// </summary>
        [Test]
        public void OnGet_Valid_Recipe_ID_Should_Get_And_Set_ReadModel_Recipe()
        {
            // Before Calling OnGet ReadModel Recipe property should be null
            var result = pageModel.Recipe == null;
            Assert.AreEqual(true, result);

            // Get a valid recipe - first in list
            var dat = TestHelper.RecipeService.GetRecipes().First();
            // Call on Get with valid recipe ID
            pageModel.OnGet(dat.RecipeID);

            // After calling OnGet ReadModel.Recipe property should not be null
            result = pageModel.Recipe != null;
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Test ensures that OnGet sets recipe not found to true when recipe ID
        /// does not correspond to a recipe in the data store
        /// </summary>
        [Test]
        public void OnGet_Invalid_RecipeId_Should_Redirect_To_Error_Page()
        {
            // Arrange
            // Get a bad id - sum of all other ids
            var badId = 0;
            var recipes = TestHelper.RecipeService.GetRecipes();
            foreach(RecipeModel recipeModel in recipes)
            {
                badId += recipeModel.RecipeID;
            }

            // Act
            var pageResult = pageModel.OnGet(badId) as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageResult.PageName.Contains("NotFound"));
        }
        /// <summary>
        /// Test ensures that OnGet() with deleted recipe ID redirects to valid (non-null) page
        /// </summary>
        [Test]
        public void OnGet_Valid_Recipe_ID_Deleted_Recipe_Should_Redirect_To_Error_Page()
        {
            // Arrage
            TestHelper.RecipeService.DeleteRecipe(1);
            var deletedID = 1;
            
            // Act
            Console.WriteLine(deletedID);
            
            var result = pageModel.OnGet(deletedID);
            // Assert
            Assert.NotNull(result);
        }
        /// <summary>
        /// Test ensures that OnPost returns valid result (returns current page)
        /// when model state is invalid
        /// </summary>
        [Test]
        public void OnPost_Should_Return_Non_Null_Result_When_Model_State_Is_Not_Valid()
        {
            // Arrange
            // Set model state invalid
            pageModel.ModelState.AddModelError("Test", "Test Error");
            
            var result = pageModel.OnPost();

            Assert.NotNull(result);
        }
        /// <summary>
        /// Test ensures that OnPost returns valid result when model state is valid
        /// including a new comment.
        /// </summary>
        [Test]
        public void OnPost_Should_Return_Non_Null_Result_When_Model_State_Is_Valid()
        {
            // Find valid recipe ID
            var validRecipeID = 0;
            foreach(RecipeModel recipeModel in TestHelper.RecipeService.GetRecipes())
            {
                if (!recipeModel.Deleted) { validRecipeID = recipeModel.RecipeID;}
            }
            pageModel.OnGet(validRecipeID);

            pageModel.NewComment = new CommentModel();
            // result from post
            var result = pageModel.OnPost();

            Assert.NotNull(result);
        }
        #endregion OnGet

        /// <summary>
        /// Test ensures that the RecipeID getter returns the same recipe ID passed in via
        /// OnGet
        /// </summary>
        [Test]
        public void RecipeID_Get_Should_Return_Correct_Recipe_Model_ID()
        {
            // Test ID
            var id = TestHelper.RecipeService.GetRecipes().First().RecipeID;
            pageModel.OnGet(id);
            Assert.AreEqual(id, pageModel.RecipeID);
        }
    }
}