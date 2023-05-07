using System;
using System.Collections;
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
        /// Adds a new recipe model to the database. Appends new model to 
        /// the end of GetRecipes() IEnumerable and overwrites json file.
        /// </summary>
        /// <param name="model"></param>
        public void AddRecipe(RecipeModel model) => SaveRecipes(GetRecipes().Concat(new[] { model }));

        /// <summary>
        /// Get next availabe recipe ID.
        /// </summary>
        /// <returns>GetRecipes().Count + 1</returns>
        public int NextRecipeID() => GetRecipes().Count() + 1;

        /// <summary>
        /// Find the recipe record
        /// Update the fields
        /// Save to the recipe store
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public RecipeModel UpdateRecipe(RecipeModel recipe)
        {
            var recipes = GetRecipes();
            var recipeToUpdate = recipes.FirstOrDefault(r => r.RecipeID == recipe.RecipeID);
            if (recipeToUpdate == null)
            {
                return null;
            }

            // Update the recipe with the new passed in values
            recipeToUpdate.Title = recipe.Title;    
            recipeToUpdate.Image = recipe.Image;    
            recipeToUpdate.Description = recipe.Description.Trim();
            recipeToUpdate.Ingredients = recipe.Ingredients;
            recipeToUpdate.Instructions = recipe.Instructions;
            recipeToUpdate.Tags = recipe.Tags;

            // Save the updated list of recipes to the JSON file
            SaveRecipes(recipes);

            return recipeToUpdate;
        }

        /// <summary>
        /// Search function for search bar
        /// searches for recipes based on a given query by matching the search terms
        /// with recipe titles, descriptions, tags, ingredients, and instructions,
        /// and ranks the results based on the weights assigned to each search category.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<RecipeModel> SearchRecipes(string query)
        {
            // Modify the query
            query = query?.Replace("+", " ");
            char[] Mychar = new Char[] { ' ', '*', '.', '?', '/', ';', '+'};
            var searchTerm = query.Trim(Mychar).ToLower().Split(' ');
            var recipes = GetRecipes();

            // Assign weight to each property
            var titleWeight = 3;
            var tagsWeight = 3;
            var descriptionWeight = 2;
            var ingredientsWeight = 1;
            var instructionsWeight = 1;

            // Get the filtered recipes in descending order by relevance 
            var filteredRecipes = recipes.Where(r => searchTerm.All( s =>
                                              r.Title.ToLower().Contains(s) ||
                                              r.Description.ToLower().Contains(s) ||
                                              r.Ingredients.Any(i => i.ToLower().Contains(s)) ||
                                              r.Instructions.Any(i => i.ToLower().Contains(s)) ||
                                              r.Tags.Any(i => i.ToLower().Contains(s))))
                                         .OrderByDescending(r => titleWeight * searchTerm.Count(s => r.Title.ToLower().Contains(s)) +
                                              descriptionWeight * searchTerm.Count(s => r.Description.ToLower().Contains(s)) +
                                              tagsWeight * searchTerm.Count(s => r.Tags.Any(t => t.ToLower().Contains(s))) +
                                              ingredientsWeight * searchTerm.Count(s => r.Ingredients.All(i => i.ToLower().Contains(s))) +
                                              instructionsWeight * searchTerm.Count(s => r.Instructions.All(i => i.ToLower().Contains(s))));

            return filteredRecipes;
        }

        /// <summary>
        /// Filter recipes by keywords in tags
        /// Used for individual filtered page under Recipes in navbar
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public IEnumerable<RecipeModel> FilterRecipesByTags(IEnumerable<string> tags)
        {
            var recipes = GetRecipes();
            return recipes.Where(r => r.Tags.Any(t => tags.Contains(t, StringComparer.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// Private helper function to serialize the RecipeModel with new data
        /// back to JSON
        /// </summary>
        /// <param name="recipes">Collection of recipes</param>
        private void SaveRecipes(IEnumerable<RecipeModel> recipes)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(recipes, options);
            File.WriteAllText(JsonFileName, jsonString);
        }
    }
}