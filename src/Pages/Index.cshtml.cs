using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Pages

{

    // Need to add logic to actually perform the search
    public class SearchController : Controller
    {
        public IActionResult Index(string request)
        {
            // Perform the search using the query parameter
            // ...

            return View();
        }
    }


    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger,
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