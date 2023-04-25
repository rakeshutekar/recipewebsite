using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System.Linq;

namespace ContosoCrafts.WebSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesAPIController : ControllerBase
    {
        public RecipesAPIController(JsonFileRecipeService recipeService)
        {
            RecipeService = recipeService;
        }

        public JsonFileRecipeService RecipeService { get; }

        /**
        [HttpGet]
        public IEnumerable<RecipeModel> Get()
        {
            return RecipeService.GetRecipes();
        }
        */

        [HttpGet]
        // TODO: Implement the search bar
        // The Get method takes in an optional searchTerm parameter
        public IEnumerable<RecipeModel> Get(string searchTerm = null)
        {
            var recipes = RecipeService.GetRecipes();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                recipes = recipes.Where(r => r.Title.Contains(searchTerm) || r.Description.Contains(searchTerm));
            }

            return recipes;
        }

        // TODO: Set the value of the ImageCaption property
    }


}