using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Http;

namespace ContosoCrafts.WebSite.Pages.Recipes
{
	public class SearchModel : PageModel
    {
        private readonly JsonFileRecipeService RecipeService;

        public SearchModel (JsonFileRecipeService recipeService)
        {
            RecipeService = recipeService;
        }

        [BindProperty]
        public IEnumerable<RecipeModel> SearchResults { get; set; }


        public void OnGet(string query)
        {
            // Check if query is not null and replaces any plus sign with a space.
            query = query?.Replace("+", " ");   

            var recipes = RecipeService.GetRecipes();
            char[] Mychar = new Char[] { ' ', '*', '.', '?', '/', ';' };
            if (!string.IsNullOrEmpty(query))
            {
                // Case-insensitive & ignore space
                SearchResults = recipes.Where(r => r.Title.ToLower().Contains(query.Trim(Mychar).ToLower()) ||
                                r.Description.ToLower().Contains(query.Trim(Mychar).ToLower()) ||
                                r.Ingredients.Any(i => i.Contains(query.Trim(Mychar), StringComparison.OrdinalIgnoreCase)) ||
                                r.Instructions.Any(i => i.Contains(query.Trim(Mychar), StringComparison.OrdinalIgnoreCase)) ||
                                r.Tags.Any(i => i.Contains(query.Trim(Mychar), StringComparison.OrdinalIgnoreCase)));

            }
        }

        public IActionResult OnPost(string query)
        {
            query = query?.Replace("+", " ");

            if (string.IsNullOrEmpty(query))
            {
                return Page();
            }

            var recipes = RecipeService.GetRecipes();
            char[] Mychar = new Char[] { ' ', '*', '.', '?', '/', ';' };
            var results = recipes.Where(r => r.Title.ToLower().Contains(query.Trim(Mychar).ToLower()) ||
                                r.Description.ToLower().Contains(query.Trim(Mychar).ToLower()) ||
                                r.Ingredients.Any(i => i.Contains(query.Trim(Mychar), StringComparison.OrdinalIgnoreCase)) ||
                                r.Instructions.Any(i => i.Contains(query.Trim(Mychar), StringComparison.OrdinalIgnoreCase)) ||
                                r.Tags.Any(i => i.Contains(query.Trim(Mychar), StringComparison.OrdinalIgnoreCase)));

            SearchResults = results;

            return Page();
        }

    }
}
