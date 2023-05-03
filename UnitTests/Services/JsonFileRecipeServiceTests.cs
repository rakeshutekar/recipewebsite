using ContosoCrafts.WebSite.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Services
{
    public class JsonFileRecipeServiceTests
    {
        
        [SetUp]
        public void TestInitialize()
        {
            
        }

        [Test]
        public void JsonFileRecipeService_Constructor_Should_Set_WebHostEnvironment()
        {
            var result = TestHelper.RecipeService.WebHostEnvironment != null;
            Assert.AreEqual(true, result);
        }

        [Test]
        public void JsonFileRecipeService_GetRecipes_Should_Return_Valid()
        {
            IEnumerable<RecipeModel> recipes = TestHelper.RecipeService.GetRecipes();
            Assert.AreEqual(true, recipes != null);
            Assert.AreEqual(true, recipes.Count() > 0);
        }

        [Test]
        public void JsonFileRecipeService_GetRecipe_Should_Return_Valid_Recipe()
        {
            var firstValidRecipe = TestHelper.RecipeService.GetRecipes().First();
            var validRecipe = TestHelper.RecipeService.GetRecipe(firstValidRecipe.RecipeID);

            Assert.IsNotNull(validRecipe);
            Assert.AreEqual(firstValidRecipe.RecipeID, validRecipe.RecipeID);
            
        }

        [Test]
        public void JsonFileRecipeService_DeleteRecipe_Should_Set_Deleted_Flag_True()
        {
            var recipes = TestHelper.RecipeService.GetRecipes();
            RecipeModel firstValidRecipe = null;
            foreach(RecipeModel recipeModel in recipes)
            {
                if (!recipeModel.Deleted)
                {
                    firstValidRecipe = recipeModel;
                    break;
                }
            }

            TestHelper.RecipeService.DeleteRecipe(firstValidRecipe.RecipeID);
            var deletedRecipe = TestHelper.RecipeService.GetRecipe(firstValidRecipe.RecipeID);
            Assert.IsTrue(deletedRecipe.Deleted);
        }

        [Test]
        public void JsonFileRecipeService_AddRecipe_Should_Add_And_Save_New_RecipeModel()
        {
            int recipeCount = TestHelper.RecipeService.GetRecipes().Count();
            RecipeModel newRecipe = new RecipeModel();
            newRecipe.RecipeID = TestHelper.RecipeService.NextRecipeID();

            TestHelper.RecipeService.AddRecipe(newRecipe);
            int newRecipeCount = TestHelper.RecipeService.GetRecipes().Count();
            Assert.IsTrue(newRecipeCount - recipeCount == 1);
        }

        [Test]
        public void JsonFileRecipeService_NextRecipeID_Should_Return_Next_Valid_RecipeID()
        {
            var rCount = TestHelper.RecipeService.GetRecipes().Count();
            Assert.IsTrue(TestHelper.RecipeService.NextRecipeID() == rCount + 1);
        }

        [Test]
        public void JsonFileRecipeService_UpdateRecipe_Valid_Recipe_Should_Update_Recipe()
        {
            var recipe = TestHelper.RecipeService.GetRecipes().First();
            const string TEST_VAL = "Test";
            recipe.Title = TEST_VAL;
            recipe.Image = TEST_VAL;
            recipe.Description = TEST_VAL;
            recipe.Ingredients = new string[] { TEST_VAL };
            recipe.Instructions = new string[] { TEST_VAL };

            TestHelper.RecipeService.UpdateRecipe(recipe);

            var testRecipe = TestHelper.RecipeService.GetRecipe(recipe.RecipeID);
            Assert.IsNotNull(testRecipe);
            Assert.AreEqual(testRecipe.Title, recipe.Title);
            Assert.AreEqual(testRecipe.Image, recipe.Image);
            Assert.AreEqual(testRecipe.Description, recipe.Description);
            Assert.IsTrue(testRecipe.Ingredients.SequenceEqual(recipe.Ingredients));
            Assert.IsTrue(testRecipe.Instructions.SequenceEqual(recipe.Instructions));

        }

        [Test]
        public void JsonFileRecipeService_Update_Recipe_Invalid_Recipe_Should_Return_Null()
        {
            var recipe = new RecipeModel();
            recipe.RecipeID = -1;
            var update = TestHelper.RecipeService.UpdateRecipe(recipe);

            Assert.IsNull(update);
        }
    }
}
