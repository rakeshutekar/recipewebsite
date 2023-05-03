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
        public static UpdateModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            pageModel = new UpdateModel(TestHelper.RecipeService)
            {

            };
        }

        #endregion TestSetup

        #region OnGet
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
                Instructions = new string[] { "new instruction", "new instruction" }
            };

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Read"));
        }

        [Test]
        public void OnPost_Invalid_Model_NotValid_Return_Page()
        {
            // Arrange

            pageModel.ModelState.AddModelError("error", "error");

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);   
        }

        #endregion OnPost
    }
}
