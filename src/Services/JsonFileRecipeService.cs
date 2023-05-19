using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

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
                    }).FirstOrDefault(x => x.RecipeID == recipeID);
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
            // If query is null return empty results
            if (query == null) return Enumerable.Empty<RecipeModel>();

            // Modify the query
            query = query.Replace("+", " ");
            char[] Mychar = new Char[] { ' ', '*', '.', '?', '/', ';', '+'};

            // Split query into words, removing trailing 's' from each word
            // and constructing a regex for exact word matching
            var searchTerm = query.Trim(Mychar).ToLower().Split(' ').Select(x => new Regex($@"\b{x.TrimEnd('s')}\b"));

            // Get all recipes
            var recipes = GetRecipes();

            // Assign weight to each property
            var titleWeight = 4;
            var tagsWeight = 4;
            var descriptionWeight = 2;
            var ingredientsWeight = 1;
            var instructionsWeight = 1;

            // Get the filtered recipes in descending order by relevance
            // For each regex, check if it matches anywhere in the recipe's fields
            var filteredRecipes = recipes.Where(r => searchTerm.All(regex =>
                                         regex.IsMatch(r.Title.ToLower().TrimEnd('s')) ||
                                         regex.IsMatch(r.Description.ToLower().TrimEnd('s')) ||
                                         r.Ingredients.Any(i => regex.IsMatch(i.ToLower().TrimEnd('s'))) ||
                                         r.Instructions.Any(i => regex.IsMatch(i.ToLower().TrimEnd('s'))) ||
                                         r.Tags.Any(i => regex.IsMatch(i.ToLower().TrimEnd('s')))))
                                    .OrderByDescending(r => titleWeight * searchTerm.Count(s => s.IsMatch(r.Title.ToLower())) +
                                         descriptionWeight * searchTerm.Count(s => s.IsMatch(r.Description.ToLower())) +
                                         tagsWeight * searchTerm.Count(s => r.Tags.Any(t => s.IsMatch(t.ToLower()))) +
                                         ingredientsWeight * searchTerm.Count(s => r.Ingredients.Any(i => s.IsMatch(i.ToLower()))) +
                                         instructionsWeight * searchTerm.Count(s => r.Instructions.Any(i => s.IsMatch(i.ToLower()))));

            return filteredRecipes;
        }

        /// <summary>
        /// Filter recipes by keywords in tags (trimmed of whitespace, converted to
        /// lower case before comparing and handles plural ending with "s")
        /// Used for individual filtered page under Recipes in navbar
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public IEnumerable<RecipeModel> FilterRecipesByTags(IEnumerable<string> tags)
        {
            if (tags == null) return Enumerable.Empty<RecipeModel>();

            // Create a regular expression pattern to match the trailing "s" in words
            var regex = new Regex("s$");
            // Process the tags using regex to remove the trailing "s" and convert to lowercase
            var _tags = tags.Select(tag => regex.Replace(tag.Trim().ToLower(), ""));

            var recipes = GetRecipes();

            // Filter the recipes based on tags,
            // Compare after removing the trailing "s" and converting to lowercase
            return recipes.Where(r => r.Tags.Any(t => _tags.Contains(regex.Replace(t.Trim().ToLower(), ""))));
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

        /// <summary>
        /// Add comments and save the added comments
        /// </summary>
        /// <param name="recipeID"></param>
        /// <param name="comment"></param>
        public void AddComment(int recipeID, CommentModel comment)
        {
            if (comment == null) return;
            var recipes = GetRecipes().ToList();  // get the list of all recipes
            var recipe = recipes.FirstOrDefault(r => r.RecipeID == recipeID);  // find the recipe in the list

            if (recipe == null) return;

            if (recipe.Comments == null)
            {
                recipe.Comments = new List<CommentModel>();
            }

            recipe.Comments.Add(comment);

            SaveRecipes(recipes);  // save the updated list
        }

        /// <summary>
        /// Add a new reaction to recipe by ID
        /// </summary>
        /// <param name="recipeID">ID for recipe to add reaction to</param>
        /// <param name="reaction">Reaction to add</param>
        public void AddReaction(int recipeID, Reaction reaction)
        {
            var recipes = GetRecipes();
            foreach(RecipeModel model in recipes)
            {
                if (model.RecipeID == recipeID)
                {
                    if (model.Reactions == null) model.Reactions = new List<Reaction>();
                    model.Reactions.Add(reaction);
                }
            }
            SaveRecipes(recipes);
        }
    }
}