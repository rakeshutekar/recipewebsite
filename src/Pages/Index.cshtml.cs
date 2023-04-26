using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Pages
{
    // Define the IndexModel class that represents the model for the Index page
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        // Constructor that takes in a logger and a recipe service dependency
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileRecipeService recipeService)
        {
            _logger = logger;
            RecipeService = recipeService;
        }
        // Provides access to an instance of the JsonFileProductService class.
        public JsonFileProductService ProductService { get; }
        // Provides access to an instance of the JsonFileRecipeService class.
        public JsonFileRecipeService RecipeService{get;}
        // Stores a collection of ProductModel instances.
        public IEnumerable<ProductModel> Products { get; private set; }
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