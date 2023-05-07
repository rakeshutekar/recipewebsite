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
            var validRecipeID = 1;
            var validRecipeTitle = "Prawn and Avocado Omelette";
            pageModel.OnGet(validRecipeID);

            // Act

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(validRecipeTitle, pageModel.Recipe.Title);
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
        public void OnPost_Invalid_Model_NotValid_Return_Page()
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
