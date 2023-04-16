using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {
        public RecipesController(JsonFileRecipeService recipeService)
        {
            RecipeService = recipeService;
        }

        public JsonFileRecipeService RecipeService { get; }

        [HttpGet]
        public IEnumerable<RecipeModel> Get()
        {
            return RecipeService.GetRecipes();
        }
    }
}