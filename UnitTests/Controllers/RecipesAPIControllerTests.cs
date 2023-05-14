using ContosoCrafts.WebSite.Controllers;
using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Controllers
{
    /// <summary>
    /// This class contains unit tests for the RecipesAPIController.
    /// </summary>
    public class RecipesAPIControllerTests
    {
        /// <summary>
        /// This method is executed before each test method is executed.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Test initialization code goes here
        }

        /// <summary>
        /// This method tests that the constructor of the RecipesAPIController sets the RecipeService property correctly.
        /// It creates a new RecipesAPIController object with the RecipeService property set to TestHelper.RecipeService,
        /// and then verifies that the RecipeService property of the created object is the same as TestHelper.RecipeService.
        /// </summary>
        [Test]
        public void RecipesAPIController_Constructor_Should_Set_RecipeService()
        {
            RecipesAPIController aPIController = new RecipesAPIController(TestHelper.RecipeService);
            Assert.AreSame(TestHelper.RecipeService, aPIController.RecipeService);
        }

        /// <summary>
        /// This method tests that the Get method of the RecipesAPIController returns a valid list of RecipeModels.
        /// It creates a new RecipesAPIController object with the RecipeService property set to TestHelper.RecipeService,
        /// calls the Get method of the created object, and then verifies that the number of returned RecipeModels
        /// is the same as the number of recipes returned by TestHelper.RecipeService.GetRecipes().
        /// </summary>
        [Test]
        public void RecipesAPIController_Get_Should_Return_Valid_RecipeModels()
        {
            RecipesAPIController aPIController = new RecipesAPIController(TestHelper.RecipeService);
            IEnumerable<RecipeModel> models = aPIController.Get();
            Assert.AreEqual(TestHelper.RecipeService.GetRecipes().Count(), models.Count());
        }

        /// <summary>
        /// Test ensures that GetByCuisine method correctly filters recipes by cuisine. Uses same
        /// cuisine filtering in service and in controller to ensure the same number of results and 
        /// that the first entry is the same in each collection.
        /// </summary>
        [Test]
        public void RecipesAPIController_GetByCuisines_Should_Return_Recipes_Filtered_By_Cuisine()
        {
            // Arrange
            RecipesAPIController aPIController = new RecipesAPIController(TestHelper.RecipeService);
            // Act
            var controllerResults = aPIController.GetByCuisines();
            var serviceResults = TestHelper.RecipeService.FilterRecipesByTags(RecipesAPIController.cuisines);
            // Assert
            Assert.IsNotNull(controllerResults);
            Assert.AreEqual(controllerResults.Count(), serviceResults.Count());
            Assert.AreEqual(controllerResults.First().RecipeID, serviceResults.First().RecipeID);
        }
    }

}
