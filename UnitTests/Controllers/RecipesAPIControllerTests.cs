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
    public class RecipesAPIControllerTests
    {
        [SetUp]
        public void TestInitialize()
        {

        }

        [Test]
        public void RecipesAPIController_Constructor_Should_Set_RecipeService()
        {
            RecipesAPIController aPIController = new RecipesAPIController(TestHelper.RecipeService);
            Assert.AreSame(TestHelper.RecipeService, aPIController.RecipeService);
        }
        [Test]
        public void RecipesAPIController_Get_Should_Return_Valid_RecipeModels()
        {
            RecipesAPIController aPIController = new RecipesAPIController(TestHelper.RecipeService);
            IEnumerable<RecipeModel> models = aPIController.Get();
            Assert.AreEqual(TestHelper.RecipeService.GetRecipes().Count(), models.Count());
        }
    }
}
