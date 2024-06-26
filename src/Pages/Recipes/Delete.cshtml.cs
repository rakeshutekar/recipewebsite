﻿using System;
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
    /// The delete model class represents the delete page of the website
    /// </summary>
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

        // ID of the recipe passed in via URL/Path
        public int RecipeID;

        // Combined Name for use with display
        public string fullName;

        /// <summary>
        /// Retrieves the specified recipe by its ID passed in via the URL
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet(int id)
        {
            RecipeID = id;
            Recipe = RecipeService.GetRecipe(id);

            // If recipe is not found or deleted, redirect to error page
            if (Recipe == null || Recipe.Deleted == true)
            {
                // Return error page if the recipe is deleted
                // "1" indicates enum type for recipe-not-found
                return RedirectToPage("../NotFound", new {type = 1});
            }

            // Set full name to be displayed, then display the page
            fullName = Recipe.FirstName + " " + Recipe.LastName;
            return Page();
            }

        /// <summary>
        /// Updates the designated recipe on the page to Deleted = true, saves
        /// the change using the RecipeService and returns the caller to the
        /// root page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            RecipeService.DeleteRecipe(Recipe.RecipeID);

            return RedirectToPage("../Index");
        }
    }
}
