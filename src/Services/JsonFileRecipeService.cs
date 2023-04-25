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
    }
}