using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Recipes;
using System;
using ContosoCrafts.WebSite.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Pages.Recipes
{
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

        #endregion OnGet


        #region OnPostAsync
        /// <summary>
        /// Tests succesful deletion of a recipe. Requires creating a fake recipe
        /// and add to the Test data set. Once created, test that the model is valid,
        /// that the page returns to the Index, and that the recipe is no longer part
        /// of the dataset
        /// </summary>
        [Test]
        public void OnPost_Valid_Model_Should_Return_Products()
        {
            // Requires creation of fake data, then confirming the pageModel is valid
            // and returns a redirect, and then finally confirming the results are truly deleted
        }

        /// <summary>
        /// Tests that an invalid model returns to Page() rather than executing a delete
        /// and then redirect action
        /// </summary>
        [Test]
        public void OnPost_InValid_Model_NotValid_Return_Page()
        {
            
        }

        #endregion OnPostAsync
    }
}
