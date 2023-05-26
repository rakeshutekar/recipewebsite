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
    /// Mange the Update of the data for a single recipe
    /// </summary>
	public class UpdateModel : PageModel
    {
        // Data middletier
        public JsonFileRecipeService RecipeService { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="recipeService"></param>
        public UpdateModel(JsonFileRecipeService recipeService)
        {
            RecipeService = recipeService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public RecipeModel Recipe { get; set; }

        public int RecipeID;

        /// <summary>
        /// REST Get request
        /// Loads the data
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet(int id)
        {
            RecipeID = id;
            Recipe = RecipeService.GetRecipe(id);

            // If recipe is not found, set RecipeNotFound to true
            if (Recipe == null)
            {
                // Return error page if the recipe is deleted
                // "1" indicates enum type for recipe-not-found
                return RedirectToPage("../NotFound", new { type = 1 });
            }
            else if (Recipe.Deleted == true)
            {
                // Return error page if the recipe is deleted
                // "1" indicates enum type for recipe-not-found
                return RedirectToPage("../NotFound", new { type = 1 });
            }

            return Page();
        }

        /// <summary>
        /// Post the model back to the page
        /// The model is in the class variable Recipe
        /// Call the data layer to update the data
        /// Then return to the index page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            RecipeService.UpdateRecipe(Recipe);

            return RedirectToPage("./Read", new { id = Recipe.RecipeID});  
        }
    }
}