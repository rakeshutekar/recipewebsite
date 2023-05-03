using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System.Linq;

namespace ContosoCrafts.WebSite.Controllers
{
    /// <summary>
    /// RecipesAPIController is an API to access Recipe information via HTTP
    /// requests, either directly from the URL or embedded as HTTP requests
    /// within the rest of the application.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RecipesAPIController : ControllerBase
    {
        /// <summary>
        /// Default constructor takes in the JsonFileRecipeService via DI
        /// </summary>
        /// <param name="recipeService">DI service to access JSON data</param>
        public RecipesAPIController(JsonFileRecipeService recipeService)
        {
            RecipeService = recipeService;
        }

        // The service to access the JSON file storing recipe data
        public JsonFileRecipeService RecipeService { get; }

        /// <summary>
        /// Gets the recipe(s) using either no search term or the parameter
        /// search term
        /// </summary>
        /// <param name="recipeService">DI service to access JSON data</param>
        [HttpGet]
        // TODO: Implement the search bar
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