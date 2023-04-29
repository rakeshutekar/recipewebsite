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
	public class DeleteModel : PageModel
    {
        /// <summary>
        /// Constructor of the DeleteModel class
        /// </summary>
        /// <param name="recipeService"></param>
        public DeleteModel(JsonFileRecipeService recipeService)
        {
            RecipeService = recipeService;
        }

        // Json service to-be-injected via constructor
        public JsonFileRecipeService RecipeService { get; set; }

        // Individual recipe model
        [BindProperty]
        public RecipeModel Recipe { get; private set; }

        // ID of the recipe passed in via URL/Path
        public int RecipeID;

        // Combined Name for use with display
        public string fullName;

        /// <summary>
        /// Retrieves the specified recipe by its ID passed in via the URL
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(int id)
        {
            RecipeID = id;
            Recipe = RecipeService.GetRecipe(id);
            fullName = Recipe.FirstName + " " + Recipe.LastName;
        }
    }
}
