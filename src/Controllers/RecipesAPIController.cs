using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

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

        [HttpGet]
        public IEnumerable<RecipeModel> Get()
        {
            return RecipeService.GetRecipes();
        }

        /// <summary>
        /// Search function implementation (case insensitive)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Please provide a search term.");
            }

            var recipes = RecipeService.GetRecipes();

            var results = recipes.Where(r => r.Title.ToLower().Contains(query.ToLower()) ||
                                r.Description.ToLower().Contains(query.ToLower()) ||
                                r.Ingredients.Any(i => i.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                                r.Instructions.Any(i => i.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                                r.Tags.Any(i => i.Contains(query, StringComparison.OrdinalIgnoreCase)));

            if (!results.Any())
            {
                return NotFound($"No recipes found for query '{query}'.");
            }

            return RedirectToPage("/Recipes/Search", new { recipes = results });
        }
    }
}