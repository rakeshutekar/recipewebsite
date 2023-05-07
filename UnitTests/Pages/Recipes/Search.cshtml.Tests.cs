﻿using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Recipes;
using System;
using ContosoCrafts.WebSite.Services;
using System.Linq;

namespace UnitTests.Pages.Recipes
{
    public class SearchTests
    {
        #region TestSetup
        // Page model for testing
        public static SearchModel pageModel;

        /// <summary>
        /// Initialize test environment for Search Tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new SearchModel(TestHelper.RecipeService)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        [Test]
        public void OnGet_Tags_Not_Null_And_Length_Not_Zero_Return_Search_Results()
        {
            // Arrange
            var recipe = TestHelper.RecipeService.GetRecipes().First();
            var tags = recipe.Tags;
            pageModel.Tags = tags;

            // Act
            pageModel.OnGet();

            // Verify returned results are not null
            Assert.IsNotNull(pageModel.SearchResults);

            // Check if recipe is in the search results
            var recipeInTagSearch = false;
            foreach (var result in pageModel.SearchResults)
            {
                if (result.RecipeID == recipe.RecipeID)
                {
                    recipeInTagSearch = true;
                    break;
                }
            }

            // Verify that the retrieved recipe in the tag search
            Assert.IsTrue(recipeInTagSearch);
        }

        [Test]
        public void OnGet_Search_Query_Not_Null_Return_Search_Results()
        {
            // Arrange
            var recipe = TestHelper.RecipeService.GetRecipes().First();
            var title = recipe.Title;
            pageModel.Query = title;

            // Act
            pageModel.OnGet();

            // Verify returned results are not null
            Assert.IsNotNull(pageModel.SearchResults);

            // Check if recipe is in the search results
            var recipeInQuerySearch = false;
            foreach (var result in pageModel.SearchResults)
            {
                if (result.RecipeID == recipe.RecipeID)
                {
                    recipeInQuerySearch = true;
                    break;
                }
            }

            // Verify that the retrieved recipe in the tag search
            Assert.IsTrue(recipeInQuerySearch);
        }

        #endregion OnGet

        #region OnPost

        #endregion OnPost
    }
}