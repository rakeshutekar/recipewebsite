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
        public RecipeModel Recipe { get; set; }

        // Boolean to keep track of if recipe is found
        public bool RecipeNotFound { get; set; }

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

            // If recipe is not found, set RecipeNotFound to true
            if (Recipe == null)
            {
                RecipeNotFound = true;
            } else
            {
                fullName = Recipe.FirstName + " " + Recipe.LastName;
            }
        }

        /// <summary>
        /// Updates the designated recipe on the page to Deleted = true, saves
        /// the change using the RecipeService and returns the caller to the
        /// root page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            // Model needs to be revalidated as the delete page has minimal
            // fields filled out and the OnPost() action is stateless so doesn't
            // carry field information over for non-bound fields.
            // https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0
            var id = Recipe.RecipeID;
            Recipe = RecipeService.GetRecipe(id);


            // Revalidate, then load /index or reload page
            ModelState.ClearValidationState(nameof(Recipe));
            if (!TryValidateModel(Recipe, nameof(Recipe)))
            {
                return Page();
            }

            RecipeService.DeleteRecipe(Recipe.RecipeID);

            return RedirectToPage("../Index");
        }
    }
}
