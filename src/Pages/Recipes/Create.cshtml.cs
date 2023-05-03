using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System;

namespace ContosoCrafts.WebSite.Pages.Recipes
{
    /// <summary>
    /// This page manages the creation of new recipes to the JSON file
    /// </summary>
    public class CreateModel : PageModel
    {
        // Logger service injected into the model
        private readonly ILogger<CreateModel> _logger;
        // Service used to retrieve and store information to the JSON file
        private readonly JsonFileRecipeService _recipeService;

        /// <summary>
        /// Constructor of the CreateModel takes in the logger and recipe
        /// service via DI.
        /// </summary>
        /// <param name="logger">Logging service</param>
        /// <param name="recipeService">Recipe service to access JSON</param>
        public CreateModel(ILogger<CreateModel> logger, JsonFileRecipeService recipeService)
        {
            _logger = logger;
            _recipeService = recipeService;
        }

        // Recipe model bound to the page to retrive user input
        [BindProperty]
        public RecipeModel Recipe { get; set; }

        /// <summary>
        /// Collects information from the form into a Recipe Model, and retrives
        /// the next recipeID to add it to the file. Adds recipe to the file and
        /// then redirects to the home page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            Recipe.RecipeID = _recipeService.NextRecipeID();   
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _recipeService.AddRecipe(Recipe);

            return RedirectToPage("/Index");
        }
    }
}
