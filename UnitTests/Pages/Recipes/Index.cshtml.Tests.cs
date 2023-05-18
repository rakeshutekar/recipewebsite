using ContosoCrafts.WebSite.Controllers;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Pages.Recipes;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Pages.Recipes
{
    /// <summary>
    /// IndexTests includes unit tests for the IndexList page mode.
    /// </summary>
    internal class IndexModelTests
    {
        #region TestSetup
        [SetUp]
        /// <summary>
        /// Initializes test environment
        /// </summary>
        public void TestInitialize()
        {
            pageModel = new IndexModel(null, TestHelper.RecipeService)
            {
            };
        }
        // A static page from the IndexList model class
        public static IndexModel pageModel;

        /// <summary>
        /// Tests that constructor correctly injects recipes service 
        /// to model
        /// </summary>
        [Test]
        public void IndexList_Constructor_Should_Inject_RecipeService()
        {
            var result = pageModel.RecipeService != null;
            Assert.AreEqual(true, result);
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Tests that OnGet correctly retrieves recipes from recipe service
        /// Asserts that pageMode.Recipes has equal count to RecipeService.Recipes
        /// and that the ID of each Recipe is identical
        /// </summary>
        [Test]
        public void OnGet_Should_Assign_Get_Recipes_From_RecipeService()
        {
            // Call OnGet on page model.
            pageModel.OnGet();

            // Lists to iterate through via index
            List<RecipeModel> pageModelRecipes = pageModel.Recipes.ToList();
            List<RecipeModel> recipeServiceRecipes = TestHelper.RecipeService.GetRecipes().ToList();

            // Assert that each list has the same number of contents
            Assert.AreEqual(true, pageModelRecipes.Count() == recipeServiceRecipes.Count());
            // Iterate through both lists, comparing the contents
            for(int i = 0; i < pageModelRecipes.Count; i++)
            {
                Assert.AreEqual(true, pageModelRecipes[i].ToString() == recipeServiceRecipes[i].ToString());    
            }
        }

        /// <summary>
        /// This test ensures that invalid filter queries are redirected to the error page
        /// </summary>
        [Test]
        public void OnGet_Invalid_Filter_Query_Redirects_To_Error_Page()
        {
            // Arrange
            // Act
            pageModel.Filter = "test";
            var pageResult = pageModel.OnGet() as RedirectToPageResult;
            // Assert
            Assert.AreEqual(true, pageResult.PageName.Contains("Error"));
        }

        /// <summary>
        /// This test ensures that when the filter = "cuisines" it returns a valid set
        /// </summary>
        [Test]
        public void OnGet_Valid_Filter_Query_Retrieves_Cuisines_And_Valid_Model()
        {
            // Arrange
            const string CUISINE_FILTER = "cuisines";
            // Act
            pageModel.Filter = CUISINE_FILTER;
            pageModel.OnGet();
            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }
    }
        #endregion OnGet
}