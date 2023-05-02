using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System;

namespace ContosoCrafts.WebSite.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<CreateModel> _logger;
        private readonly JsonFileRecipeService _recipeService;

        public CreateModel(ILogger<CreateModel> logger, JsonFileRecipeService recipeService)
        {
            _logger = logger;
            _recipeService = recipeService;
        }

        [BindProperty]
        public RecipeModel Recipe { get; set; }

        public void OnGet()
        {
        }

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
