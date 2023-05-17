using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Recipes;
using System;
using ContosoCrafts.WebSite.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Pages.Recipes
{
    /// <summary>
    /// Unit test class for the delete page
    /// </summary>
    public class DeleteTests
    {
        #region TestSetup
        // A static page from the DeleteModel class
        public static DeleteModel pageModel;

        /// <summary>
        /// Initializes the test by calling the TestHelper class and creating a static page
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new DeleteModel(TestHelper.RecipeService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Test to ensure that the page succesfully retrives the information to be
        /// displayed prior to the user confirming the delete action
        /// </summary>
        [Test] 
        public void OnGet_Valid_Should_Return_Product()
        {
            // Arrange

            // Act
            // Preparing a valid productID from the current dataset
            var validProductId = 1;
            var validProductTitle = "Prawn and Avocado Omelette";
            pageModel.OnGet(validProductId);

            // Assert - test the model is valid and that expected title is the same as the
            // the retrieved title
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(validProductTitle, pageModel.Recipe.Title);
        }

        /// <summary>
        /// Tests that invalid recipes are not found and therefore not displayed on the
        /// page
        /// </summary>
        [Test]
        public void OnGet_Invalid_RecipeID_Should_Return_Not_Found_Recipe()
        {
            // Arange

            // Act
            var invalidProductId = 999;
            pageModel.OnGet(invalidProductId);

            // Assert
            Assert.AreEqual(true, pageModel.RecipeNotFound);
        }
        #endregion OnGet

        #region OnPostAsync
        /// <summary>
        /// Tests succesful deletion of a recipe. Prepares a page model with
        /// the first item int he actual dataset. Once created, test that the model is 
        /// valid, that the page returns to the Index, and that the recipe is no longer
        /// part of the dataset
        /// </summary>
        [Test]
        public void OnPost_Valid_Model_Should_Return_Products()
        {
            // Arrange
            // Preparing a valid productID from the current dataset
            var validProductId = 1;
            pageModel.OnGet(validProductId);

            // Act
            // Delete the prepared recipe page
            // redirect to /Index occurs
            var result = pageModel.OnPost() as RedirectToPageResult;
            var deletedRecipe = TestHelper.RecipeService.GetRecipe(validProductId);

            // Assert
            // Confirm that the model is valid after the delete, redirect occurs, and delete confirmed
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
            Assert.AreEqual(true, deletedRecipe.Deleted);
        }

        /// <summary>
        /// Tests that an invalid model returns to Page() rather than executing a delete
        /// and then redirect action
        /// </summary>
        [Test]
        public void OnPost_InValid_Model_NotValid_Return_Page()
        {
            // Arrange - create a new fake recipe
            pageModel.Recipe = TestHelper.TEST_RECIPE_MODEL;
            pageModel.ModelState.AddModelError("Fake Error", "Fake Error");

            // Act
            var pagePostResult = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }
        #endregion OnPostAsync
    }
}