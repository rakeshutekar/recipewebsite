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
    public class IndexModel : PageModel
    {
        // Const string used for comparison
        private const string CUISINES_FILTER = "cuisines";

        // Static Getter acts as a constant list of cuisine tag options
        public static List<string> cuisines
        {
            get
            {
                return new List<string>()
                {
                    "Japanese", "Chinese", "Mexican", "Korean", "French", "Thai", "Vietnamese", "Indian"
                };
            }
        }

        // Logger field
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Default constructor that injects the logger and recipeService via DI
        /// for use with the page
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="recipeService">RecipeService that retrieves data</param>
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileRecipeService recipeService)
        {
            _logger = logger;
            RecipeService = recipeService;
        }

        // Service that retrieves the data
        public JsonFileRecipeService RecipeService{get;}

        // Collection of recipes retrieved from the database
        public IEnumerable<RecipeModel> Recipes {get; private set;}

        // Filter parameter when searching for cuisines. Bound so that query
        // can be executed via URI
        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        /// <summary>
        /// OnGet() method retrieves all the recipes from JSON file and returns
        /// it as an IEnumerable collection
        /// </summary>
        /// <returns>IActionresult</returns>
        public IActionResult OnGet()
        {
            // Ensure invalid filters do not return results
            if (!string.IsNullOrEmpty(Filter) && !Filter.Equals(CUISINES_FILTER))
            {
                return RedirectToPage("../Error");
            }

            // If matching the cuisines filter, send back filter results
            if (!string.IsNullOrEmpty(Filter) && Filter.Equals(CUISINES_FILTER))
            {
                Recipes = RecipeService.FilterRecipesByTags(cuisines);
            }
            // else return normal recipes
            else
            {
                Recipes = RecipeService.GetRecipes();
            }

            return Page();
        }
    }
}