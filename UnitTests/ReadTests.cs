using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Recipes;
using System;
using ContosoCrafts.WebSite.Services;
using System.Linq;

namespace UnitTests.Recipes.ReadPage
{
    public class ReadTests
    {
        #region TestSetup
        public static ReadModel pageModel;

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
        #endregion OnGet
    }
}

