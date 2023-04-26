using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Defines the IndexModel class that represents the model for the Index
    /// page. This is the landing page for the / route of the website
    /// </summary>
    public class IndexModel : PageModel
    {
        // Private logger class field
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Constructor that takes in a logger and a recipe service dependency
        /// </summary>
        /// <param name="logger">Logger service injected via DI</param>
        /// <param name="recipeService">Recipe service injected via DI</param>
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileRecipeService recipeService)
        {
            _logger = logger;
            RecipeService = recipeService;
        }

        // Provides access to an instance of the JsonFileRecipeService class.
        public JsonFileRecipeService RecipeService{get;}
        // Stores a collection of RecipeModel instances.
        public IEnumerable<RecipeModel> Recipes {get; private set;}

        // Handler method for HTTP GET requests to the Index page
        public void OnGet()
        {
            // Get the list of recipes from the recipe service
            Recipes = RecipeService.GetRecipes();
        }
    }
}