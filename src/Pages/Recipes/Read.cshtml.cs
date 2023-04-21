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
	public class ReadModel : PageModel
    {
        public ReadModel(JsonFileRecipeService recipeService)
        {
            RecipeService = recipeService;
        }

        public JsonFileRecipeService RecipeService { get; set; }


        public RecipeModel Recipe { get; private set; }

        public int RecipeID;

        public void OnGet(int id)
        {
            RecipeID = id;
            Recipe = RecipeService.GetRecipes().FirstOrDefault(x => x.RecipeID == RecipeID);
        }
    }
}
