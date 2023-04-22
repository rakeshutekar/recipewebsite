using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Recipes
{
    /// <summary>
    /// The ReadModel class is the backbone for an individual full screen recipe page.
    /// The class pulls in the JsonRecipe service to retrieve an individual recipe's
    /// data from the JSON "Database"
    /// </summary>
    public class ReadModel : PageModel
    {
        /// <summary>
        /// Constructor of the ReadModel class
        /// </summary>
        /// <param name="recipeService"></param>
        public ReadModel(JsonFileRecipeService recipeService)
        {
            RecipeService = recipeService;
        }

        // Json service to-be-injected via constructor
        public JsonFileRecipeService RecipeService { get; set; }

        // Individual recipe model
        public RecipeModel Recipe { get; private set; }

        // ID of the recipe passed in via URL/Path
        public int RecipeID;

        /// <summary>
        /// Retrieves the specified recipe by its ID passed in via the URL
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(int id)
        {
            RecipeID = id;
            Recipe = RecipeService.GetRecipes().FirstOrDefault(x => x.RecipeID == RecipeID);
        }
    }
}
