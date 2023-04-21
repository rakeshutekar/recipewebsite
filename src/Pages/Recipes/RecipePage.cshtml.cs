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
	public class RecipePageModel : PageModel
    {
        public RecipePageModel(JsonFileRecipeService recipeService)
        {
            RecipeService = recipeService;
        }

        public JsonFileRecipeService RecipeService { get; set; }


        public IEnumerable<RecipeModel> Recipes { get; private set; }

        public void OnGet()
        {
            Recipes = RecipeService.GetRecipes();
        }
    }
}
