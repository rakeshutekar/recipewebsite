using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace ContosoCrafts.WebSite.Pages.Recipes
{
	public class SearchModel : PageModel
    {
        private readonly JsonFileRecipeService RecipeService;

        /// <summary>
        /// Constructor that injects the recipe service
        /// </summary>
        /// <param name="recipeService"></param>
        public SearchModel (JsonFileRecipeService recipeService)
        {
            RecipeService = recipeService;
        }

        // Bind the search results to a property so that it can be used
        // in the OnGet() and OnPost() methods
        [BindProperty]
        public IEnumerable<RecipeModel> SearchResults { get; set; }

        // Bind the search query to a property so that it can be used
        // in the OnGet() and OnPost() methods
        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }

        // Bind the selected tags to a property so that they can be used
        // in the OnGet() and OnPost() methods
        [BindProperty(SupportsGet = true)]
        public string[] Tags { get; set; }

        /// <summary>
        /// Called when the page is loaded with a GET request
        /// </summary>
        public void OnGet()
        {
            // If tags were selected, filter the recipes by those tags
            if (Tags != null && Tags.Length > 0)
            {
                SearchResults = RecipeService.FilterRecipesByTags(Tags);
            }
            // If a search query was entered, search for recipes that
            // match that query
            else if (!string.IsNullOrEmpty(Query))
            {
                SearchResults = RecipeService.SearchRecipes(Query);
            }
        }
    }
}
