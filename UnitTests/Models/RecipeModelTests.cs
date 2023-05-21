using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    public class RecipeModelTests
    {
        [SetUp]
        public void TestInitialize()
        {

        }
        /// <summary>
        /// Tests that RecipeModel.Description setter does not set description if value length is 
        /// less than or equal to 1.
        /// </summary>
        [Test]
        public void RecipeModel_Set_Description_Should_Not_Set_Description_If_Value_Length_Is_Less_Than_Or_Equal_To_One()
        {
            var testVal = "_";
            RecipeModel model = new RecipeModel();
            model.Description = testVal;
        }
        /// <summary>
        /// Tests that the InstructiosnAttribute.IsValid returns a valid result
        /// </summary>
        [Test]
        public void InstructionsAttribute_IsValid_Should_Return_Validation_Result()
        {
            RecipeModel.InstructionsAttribute instructionsAttribute = new RecipeModel.InstructionsAttribute();
            var result = instructionsAttribute.IsValid(TestHelper.TEST_RECIPE_MODEL.Instructions);
            Assert.IsTrue(result);
        }
        /// <summary>
        /// Tests that InstructionsAttribute.IsValid returns false if invalid instructions (null or empty)
        /// are provided
        /// </summary>
        [Test]
        public void InstructionsAttribute_IsValid_Should_Return_False_When_Instruction_Is_Null_Or_White_Space()
        {
            RecipeModel.InstructionsAttribute instructionsAttribute = new RecipeModel.InstructionsAttribute();
            List<string> inputValue = new List<string>() { null };
            var result = instructionsAttribute.IsValid(inputValue);
            Assert.IsFalse(result);
        }
        /// <summary>
        /// Tests that IngredientsAttribute.IsValid returns true if valid ingredients are provided
        /// </summary>
        [Test]
        public void IngredientsAttribute_IsValid_Valid_Ingredients_Should_Return_True()
        {
            RecipeModel.IngredientsAttribute ingredientsAttribute = new RecipeModel.IngredientsAttribute();
            var result = ingredientsAttribute.IsValid(TestHelper.TEST_RECIPE_MODEL.Ingredients);
            Assert.IsTrue(result);
        }
        /// <summary>
        /// Tests that IngredientsAttribute.IsValid returns false if invalid ingredients are provided
        /// </summary>
        [Test]
        public void IngredientsAttribute_IsValid_Invalid_Ingredients_Should_Return_False()
        {
            RecipeModel.IngredientsAttribute ingredientsAttribute = new RecipeModel.IngredientsAttribute();
            List<string> inputValue = new List<string>() { null };
            var result = ingredientsAttribute.IsValid(inputValue);
            Assert.IsFalse(result);
        }
        /// <summary>
        /// Tests that RecipeModel.GetReactions returns default reactions list if model reactions are null
        /// </summary>
        [Test]
        public void RecipeModel_GetReactions_Should_Return_Default_Reactions_If_Reactions_Are_Null()
        {
            RecipeModel model = new RecipeModel();
            model.Reactions = null;
            var result = model.GetReactions();
            Assert.NotNull(result);
        }
        /// <summary>
        /// Tests that RecipeModel.GetReactions returns valid tuple list if model reactions are not null
        /// </summary>
        [Test]
        public void RecipeModel_GetReactions_Should_Return_Valid_Tuple_List_If_Reactions_Are_Not_Null()
        {
            var model = TestHelper.RecipeService.GetRecipes().First();
            TestHelper.RecipeService.AddReaction(model.RecipeID, Reaction.Content);
            model = TestHelper.RecipeService.GetRecipe(model.RecipeID);
            var result = model.GetReactions();
            Assert.NotNull(result);
            int rsum = 0;
            foreach( var reaction in result) rsum += reaction.Item2;

            Assert.Greater(rsum, 0);
        }
        /// <summary>
        /// Tests that RecipeModel.ToString() returns valid serialized string value
        /// </summary>
        [Test]
        public void RecipeModel_ToString_Should_Return_Valid_Serialized_Value()
        {
            var json = TestHelper.TEST_RECIPE_MODEL.ToString();
            Assert.NotNull(json);
            Assert.IsNotEmpty(json);
        }
    }
}
