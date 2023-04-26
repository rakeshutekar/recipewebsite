using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Pages.Recipes
{
    /// <summary>
    /// IndexList is the c# model behind the index page of the recipes. This
    /// class retrieves the recipes and shows the user a full listing of the
    /// catalogue.
    /// </summary>
    public class IndexList : PageModel
    {
        // Logger field
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Default constructor that injects the logger and recipeService via DI
        /// for use with the page
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="recipeService">RecipeService that retrieves data</param>
        public IndexList(ILogger<IndexModel> logger,
            JsonFileRecipeService recipeService)
        {
            _logger = logger;
            RecipeService = recipeService;
        }

        // Service that retrieves the data
        public JsonFileRecipeService RecipeService{get;}
        // Collection of recipes retrieved from the database
        public IEnumerable<RecipeModel> Recipes {get; private set;}

        /// <summary>
        /// OnGet() method retrieves all the recipes from JSON file and returns
        /// it as an IEnumerable collection
        /// </summary>
        public void OnGet()
        {
            Recipes = RecipeService.GetRecipes();
        }
    }
}