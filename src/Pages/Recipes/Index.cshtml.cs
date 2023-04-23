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

        public JsonFileProductService ProductService { get; }
        public JsonFileRecipeService RecipeService{get;}
        public IEnumerable<ProductModel> Products { get; private set; }
        public IEnumerable<RecipeModel> Recipes {get; private set;}

        public void OnGet()
        {
            // Products = ProductService.GetProducts();
            Recipes = RecipeService.GetRecipes();
        }
    }
}