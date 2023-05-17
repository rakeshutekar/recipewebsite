using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Services
{
    public class JsonFileRecipeServiceTests
    {
        /// <summary>
        /// Perform initialization before running tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            
        }

        /// <summary>
        /// Verifies that the constructor of JsonFileRecipeService sets the WebHostEnvironment property.
        /// </summary>
        [Test]
        public void JsonFileRecipeService_Constructor_Should_Set_WebHostEnvironment()
        {
            // Check if the WebHostEnvironment property of the RecipeService instance is not null.
            var result = TestHelper.RecipeService.WebHostEnvironment != null;
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Verifies that the GetRecipes method of JsonFileRecipeService returns valid recipes.
        /// </summary>
        [Test]
        public void JsonFileRecipeService_GetRecipes_Should_Return_Valid()
        {
            // Call the GetRecipes method of JsonFileRecipeService and verify that it returns a non-null IEnumerable of RecipeModel objects.
            IEnumerable<RecipeModel> recipes = TestHelper.RecipeService.GetRecipes();
            Assert.AreEqual(true, recipes != null);
            Assert.AreEqual(true, recipes.Count() > 0);
        }


        /// <summary>
        /// Verifies that the GetRecipe method of JsonFileRecipeService returns a valid recipe for the given recipe ID.
        /// </summary>
        [Test]
        public void JsonFileRecipeService_GetRecipe_Should_Return_Valid_Recipe()
        {
            // Retrieve the first valid recipe from the RecipeService instance.
            var firstValidRecipe = TestHelper.RecipeService.GetRecipes().First();

            // Retrieve the recipe with the ID of the firstValidRecipe and verify that it is not null and has the same ID as firstValidRecipe.
            var validRecipe = TestHelper.RecipeService.GetRecipe(firstValidRecipe.RecipeID);
            Assert.IsNotNull(validRecipe);
            Assert.AreEqual(firstValidRecipe.RecipeID, validRecipe.RecipeID);
        }

        /// <summary>
        /// Verifies that the DeleteRecipe method of JsonFileRecipeService sets the Deleted flag of a recipe to true.
        /// </summary>
        [Test]
        public void JsonFileRecipeService_DeleteRecipe_Should_Set_Deleted_Flag_True()
        {
            // Retrieve all recipes from the RecipeService instance and find the first recipe that is not marked as deleted.
            var recipes = TestHelper.RecipeService.GetRecipes();
            RecipeModel firstValidRecipe = null;
            foreach (RecipeModel recipeModel in recipes)
            {
                if (!recipeModel.Deleted)
                {
                    firstValidRecipe = recipeModel;
                    break;
                }
            }

            // Delete the firstValidRecipe and verify that its Deleted flag is set to true.
            TestHelper.RecipeService.DeleteRecipe(firstValidRecipe.RecipeID);
            var deletedRecipe = TestHelper.RecipeService.GetRecipe(firstValidRecipe.RecipeID);
            Assert.IsTrue(deletedRecipe.Deleted);
        }

        /// <summary>
        /// Verifies that the AddRecipe method of JsonFileRecipeService adds and saves a new RecipeModel instance.
        /// </summary>
        [Test]
        public void JsonFileRecipeService_AddRecipe_Should_Add_And_Save_New_RecipeModel()
        {
            // Retrieve the count of recipes before adding a new recipe.
            int recipeCount = TestHelper.RecipeService.GetRecipes().Count();

            // Create a new RecipeModel instance and add it to the RecipeService instance.
            RecipeModel newRecipe = TestHelper.TEST_RECIPE_MODEL;
            newRecipe.RecipeID = TestHelper.RecipeService.NextRecipeID();
            TestHelper.RecipeService.AddRecipe(newRecipe);

            // Retrieve the count of recipes after adding the new recipe and verify that it has increased by 1.
            int newRecipeCount = TestHelper.RecipeService.GetRecipes().Count();
            Assert.IsTrue(newRecipeCount - recipeCount == 1);
        }


        /// <summary>
        /// Verifies that the NextRecipeID method of JsonFileRecipeService returns the next valid RecipeID.
        /// </summary>
        [Test]
        public void JsonFileRecipeService_NextRecipeID_Should_Return_Next_Valid_RecipeID()
        {
            // Get the current number of recipes and verify that the NextRecipeID method returns a value greater than the current count.
            var rCount = TestHelper.RecipeService.GetRecipes().Count();
            Assert.IsTrue(TestHelper.RecipeService.NextRecipeID() == rCount + 1);
        }

        /// <summary>
        /// Verifies that the UpdateRecipe method of JsonFileRecipeService updates a valid recipe.
        /// </summary>
        [Test]
        public void JsonFileRecipeService_UpdateRecipe_Valid_Recipe_Should_Update_Recipe()
        {
            // Get the first recipe in the list and update its properties with test values.
            var recipe = TestHelper.RecipeService.GetRecipes().First();
            const string TEST_VAL = "Test";
            recipe.Title = TEST_VAL;
            recipe.Image = TEST_VAL;
            recipe.Description = TEST_VAL;
            recipe.Ingredients = new string[] { TEST_VAL };
            recipe.Instructions = new string[] { TEST_VAL };

            // Call the UpdateRecipe method with the modified recipe and verify that the properties were updated correctly.
            TestHelper.RecipeService.UpdateRecipe(recipe);

            var testRecipe = TestHelper.RecipeService.GetRecipe(recipe.RecipeID);
            Assert.IsNotNull(testRecipe);
            Assert.AreEqual(testRecipe.Title, recipe.Title);
            Assert.AreEqual(testRecipe.Image, recipe.Image);
            Assert.AreEqual(testRecipe.Description, recipe.Description);
            Assert.IsTrue(testRecipe.Ingredients.SequenceEqual(recipe.Ingredients));
            Assert.IsTrue(testRecipe.Instructions.SequenceEqual(recipe.Instructions));
        }

        /// <summary>
        /// Verifies that the UpdateRecipe method of JsonFileRecipeService returns null for an invalid recipe.
        /// </summary>
        [Test]
        public void JsonFileRecipeService_Update_Recipe_Invalid_Recipe_Should_Return_Null()
        {
            // Create a new recipe with an invalid RecipeID and call the UpdateRecipe method.
            var recipe = new RecipeModel();
            recipe.RecipeID = -1;
            var update = TestHelper.RecipeService.UpdateRecipe(recipe);

            // Verify that the UpdateRecipe method returned null.
            Assert.IsNull(update);
        }

        /// <summary>
        /// This method tests that the SearchRecipes method of the JsonFileRecipeService returns valid RecipeModels.
        /// It retrieves the first recipe from TestHelper.RecipeService and gets its title. It then calls the SearchRecipes
        /// method of TestHelper.RecipeService with the retrieved title as the search query, and verifies that the returned
        /// search results are not null and contain the retrieved recipe.
        /// </summary>
        [Test]
        public void JsonFileRecipeService_SearchRecipes_Should_Return_Valid_RecipeModels()
        {
            // Retrieve the first recipe from TestHelper.RecipeService
            var firstData = TestHelper.RecipeService.GetRecipes().First();

            // Get the title of the first recipe
            var query = firstData.Title;

            // Call the SearchRecipes method of TestHelper.RecipeService with the retrieved title as the search query
            var searchResults = TestHelper.RecipeService.SearchRecipes(query);

            // Verify that the returned search results are not null
            Assert.IsNotNull(searchResults);

            // Check if the retrieved recipe is in the search results
            var recipeInSearch = false;
            foreach (var recipe in searchResults)
            {
                if (recipe.RecipeID == firstData.RecipeID)
                {
                    recipeInSearch = true;
                    break;
                }
            }

            // Verify that the retrieved recipe is in the search results
            Assert.IsTrue(recipeInSearch);
        }

        /// <summary>
        /// This method tests the behavior of the SearchRecipes method in the JsonFileRecipService class 
        /// when an invalid query is passed as a parameter. The method should return empty results.
        /// </summary>
        [Test]
        public void JsonFileRecipService_SearchRecipes_Invalid_Query_Should_Return_Empty_Results()
        {
            // Call the SearchRecipes method with a null query parameter
            var searchResults = TestHelper.RecipeService.SearchRecipes(null);

            // Assert that the searchResults collection is empty
            Assert.IsEmpty(searchResults);
        }

        /// <summary>
        /// This method tests that the FilterRecipesByTags method of the JsonFileRecipeService returns valid RecipeModels.
        /// It retrieves the first recipe from TestHelper.RecipeService and gets its tags. It then calls the FilterRecipesByTags
        /// method of TestHelper.RecipeService with the retrieved tags as the filter criteria, and verifies that the returned
        /// filtered results are not null and contain the retrieved recipe.
        /// </summary>
        [Test]
        public void JsonFileRecipeService_FilterRecipesByTags_Should_Return_Valid_RecipeModels()
        {
            // Retrieve the first recipe from TestHelper.RecipeService
            var recipe = TestHelper.RecipeService.GetRecipes().First();

            // Get the tags of the first recipe
            var tags = recipe.Tags;

            // Call the FilterRecipesByTags method of TestHelper.RecipeService with the retrieved tags as the filter criteria
            var filterResults = TestHelper.RecipeService.FilterRecipesByTags(tags);

            // Verify that the returned filtered results are not null
            Assert.IsNotNull(filterResults);

            // Check if the retrieved recipe is in the filtered results
            var recipeInFilter = false;
            foreach (var filter in filterResults)
            {
                if (filter.RecipeID == recipe.RecipeID)
                {
                    recipeInFilter = true;
                    break;
                }
            }

            // Verify that the retrieved recipe is in the filtered results
            Assert.IsTrue(recipeInFilter);
        }

        /// <summary>
        /// Test ensures when filtering recipes by tag, method returns an empty enumerable if the provided array of tags is null
        /// </summary>
        [Test]
        public void JsonFileRecipeService_FilterRecipesByTags_Should_Return_Empty_Enumerable_If_Tag_Array_Is_Null()
        {
            // Arrange
            // Act
            var result = TestHelper.RecipeService.FilterRecipesByTags(null);

            // Assert
            Assert.IsEmpty(result);
        }

        /// <summary>
        /// Test ensures that AddComment method can handle a null comment model argument
        /// </summary>
        [Test]
        public void JsonFileRecipeService_AddComment_Should_Return_Immediate_With_Null_Comment_Model()
        {
            // Create test recipe model
            var newRecipe = TestHelper.TEST_RECIPE_MODEL;
            // Set comment list to null
            newRecipe.Comments = null;
            // Get next available recipe id
            var testID = TestHelper.RecipeService.NextRecipeID();
            // Assign id to test recipe
            newRecipe.RecipeID = testID;
            // Add test recipe to file
            TestHelper.RecipeService.AddRecipe(newRecipe);
            // Ensure new recipe has been correctly added to the data store
            var getResult = TestHelper.RecipeService.GetRecipe(testID);
            // Assert that a new recipe has been created with a null comment section
            Assert.NotNull(getResult);

            // Act
            // Attempt to add a comment 
            TestHelper.RecipeService.AddComment(testID, null);
            // Assert
            // Ensure that the test recipe has been added but the comment section is still
            // null because add comment did not do anything when provided with a null comment
            // model
            var recipe = TestHelper.RecipeService.GetRecipe(testID);
            Assert.NotNull(recipe);
            Assert.IsNull(recipe.Comments);
        }

        /// <summary>
        /// Test ensures that AddComment method can handle a bad (non existent) recipe id.
        /// </summary>
        [Test]
        public void JsonFileRecipeService_AddComment_Should_Return_Immediate_Null_Recipe_Valid_Comment()
        {
            // Get a bad recipe id - sum of all other recipe id
            var badID = 0;
            // Sum up all other recipe id
            var recipes = TestHelper.RecipeService.GetRecipes();
            foreach(var recipe in recipes) { badID += recipe.RecipeID; }
            // Ensure that reciep does not exist with id
            var result = TestHelper.RecipeService.GetRecipe(badID);
            Assert.IsNull(result);
            // Attempt to add comment to non-existent recipe 
            TestHelper.RecipeService.AddComment(badID, TestHelper.TEST_COMMENT_MODEL);
        }

        /// <summary>
        /// Test ensures that AddComment will initialize the comment list if a recipe's comment list is null
        /// </summary>
        [Test]  
        public void JsonFileRecipeService_AddComment_Valid_Recipe_Valid_Comment_Should_Initialize_Recipe_Comments_List_If_Null()
        {
            // Create test recipe
            var newRecipe = TestHelper.TEST_RECIPE_MODEL;
            // ensure 0/null comments
            newRecipe.Comments = null;
            // Get next id for later verification
            var testID = TestHelper.RecipeService.NextRecipeID();
            // Assign id to test recipe
            newRecipe.RecipeID = testID;

            // Add recipe to service
            TestHelper.RecipeService.AddRecipe(newRecipe);
            var recipe = TestHelper.RecipeService.GetRecipe(testID);
            // Ensure that recipe was correctly added
            Assert.NotNull(recipe);
            // Attempt to add a new valid comment 
            TestHelper.RecipeService.AddComment(testID, TestHelper.TEST_COMMENT_MODEL);
        }

        /// <summary>
        /// Test ensures that valid comments are correctly added and saved to a valid recipe
        /// </summary>
        [Test]
        public void JsonFileRecipeService_AddComment_Valid_ID_Valid_Comment_Should_Add_And_Save_Comment()
        {
            // Create test model and add to temporary data store
            RecipeModel testModel = TestHelper.TEST_RECIPE_MODEL;
            var nextID = TestHelper.RecipeService.NextRecipeID();
            testModel.RecipeID = nextID;
            TestHelper.RecipeService.AddRecipe(testModel);

            // Add comment to new recipe
            var recipe = TestHelper.RecipeService.GetRecipe(nextID);
            Assert.NotNull(recipe);
            TestHelper.RecipeService.AddComment(nextID, TestHelper.TEST_COMMENT_MODEL);

            // Assert that recipe contains new comment with test value
            recipe = TestHelper.RecipeService.GetRecipe(nextID);
            Assert.NotNull(recipe);
            Assert.NotNull(recipe.Comments);
            Assert.IsTrue(recipe.Comments.Last().Comment == TestHelper.STRING_TEST_VAL);
        }
    }
}
