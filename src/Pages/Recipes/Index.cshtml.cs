using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Pages.Recipes
{
    public class IndexList : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexList(ILogger<IndexModel> logger,
            JsonFileRecipeService recipeService)
        {
            _logger = logger;
            RecipeService = recipeService;
        }

        public JsonFileRecipeService RecipeService{get;}
        public IEnumerable<RecipeModel> Recipes {get; private set;}

        public void OnGet()
        {
            Recipes = RecipeService.GetRecipes();
        }
    }
}