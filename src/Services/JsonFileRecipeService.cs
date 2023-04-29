using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using ContosoCrafts.WebSite.Models;

using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// JsonFileRecipeService class provides the DB to Model service, grabbing
    /// the Recipes from the DB and returning the applicable RecipeModel object
    /// </summary>
    public class JsonFileRecipeService
    {
        /// <summary>
        /// Default constructor, takes in IWebHostEnvironment as parameter to
        /// access information about the project (directories, etc)
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public JsonFileRecipeService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        // WebHostEnvironment class variable
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Private helper method to get the recipe data from the appropriate
        /// directory
        /// </summary>
        /// <returns>String value of the filepath</returns>
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "recipes.json"); }
        }

        /// <summary>
        /// Retrieves all recipes from the JSON file and returns collection of
        /// RecipeModel objects
        /// </summary>
        /// <returns>IEnumerable of RecipeModel objects</returns>
        public IEnumerable<RecipeModel> GetRecipes()
        {
            using(var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<RecipeModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// Retrieves a single recipe from JSON file denoted by its recipeID
        /// </summary>
        /// <returns>RecipeModel objecs</returns>
        public RecipeModel GetRecipe(int recipeID)
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<RecipeModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }).First(x => x.RecipeID == recipeID);
            }
        }

        /// <summary>
        /// Sets the Deleted flag of a Recipe to true, updates the Enumerable and
        /// overwrites the existing file with the new data
        /// </summary>
        /// <param name="recipeID">ID of the recipe to delete</param>
        /// <returns>The updated RecipeModel</returns>
        public RecipeModel DeleteRecipe(int recipeID)
        {
            // Retrieve the specified recipe, set Deleted flag to true
            var data = GetRecipe(recipeID);
            data.Deleted = true;

            // Retrieve all data not including the to-be-delete recipe, then 
            // save back to the file, then serialize to update file
            var updatedRecipes = GetRecipes().Where(x => x.RecipeID != recipeID).ToList();
            updatedRecipes.Add(data);
            SaveRecipes(updatedRecipes);

            return data;
        }

        /// <summary>
        /// Private helper function to serialize the RecipeModel with new data
        /// back to JSON
        /// </summary>
        /// <param name="recipes">Collection of recipes</param>
        private void SaveRecipes(IEnumerable<RecipeModel> recipes)
        {
            
        }
    }
}