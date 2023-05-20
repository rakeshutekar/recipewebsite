using Bunit;
using NUnit.Framework;
using System.Linq;
using ContosoCrafts.WebSite.Components;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Microsoft.Extensions.DependencyInjection;
using Bunit.TestDoubles;
using Newtonsoft.Json.Linq;
using System;

namespace UnitTests.Components
{
    /// <summary>
    /// Test class to cover the ReactionList component used across the site
    /// </summary>
    public class ReactionListTests : BunitTestContext
    {
        #region TestSetup
        /// <summary>
        /// Initializes the test
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region PageLoad
        /// <summary>
        /// Tests that the appropriate content is returned on the page
        /// </summary>
        [Test]
        public void ReactionList_Load_Should_Rreturn_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileRecipeService>(TestHelper.RecipeService);
            var recipe = TestHelper.RecipeService.GetRecipes().FirstOrDefault();
            var reactions = recipe.GetReactions();

            // Act - Render page with the passed parameter from the recipe
            var page = RenderComponent<ReactionList>(parameters => parameters
                .Add(p => p.RecipeID, recipe.RecipeID));

            // Get the component markup
            var result = page.Markup;

            // Assert that all reaction types are rendered on the component
            foreach(var reaction in reactions)
            {
                var enumValue = (int)reaction.Item1;
                var enumString = Enum.GetName(typeof(Reaction), enumValue);
                Assert.AreEqual(true, result.Contains(enumString));
            }
            
        }
        #endregion PageLoad

        #region ButtonClick
        /// <summary>
        /// Tests that buttons increment the value when clicked
        /// </summary>
        [Test]
        public void ReactionList_Button_Click_Should_Increment_The_Value() 
        {
            // Arrange
            Services.AddSingleton<JsonFileRecipeService>(TestHelper.RecipeService);
            var recipe = TestHelper.RecipeService.GetRecipes().FirstOrDefault();
            var reactions = recipe.GetReactions();

            // Get the current value of the first reaction prior to button click
            var buttonId = (int) reactions[0].Item1;
            var buttonIdString = buttonId.ToString();
            var reactionCount = reactions[0].Item2;
            var reactionCountString = reactionCount.ToString();

            // Act - Render page with the passed parameter from the recipe
            var page = RenderComponent<ReactionList>(parameters => parameters
                .Add(p => p.RecipeID, recipe.RecipeID));

            // Click the buttonId button
            var buttonList = page.FindAll("button");
            var button = buttonList.First(x => x.Id == buttonIdString);
            var preClickCountEqualsReaction = button.InnerHtml.Contains(reactionCountString);
            button.Click();

            // Get new count values
            var clickedReactions = TestHelper.RecipeService.GetRecipe(recipe.RecipeID).GetReactions();
            var clickedReactionCount = clickedReactions[0].Item2;
            var clickedReactionCountString = clickedReactionCount.ToString();

            // Gather the results from the button
            var clickedButtonList = page.FindAll("button");
            var clickedButton = clickedButtonList.First(x => x.Id == buttonIdString);
            var clickedButtonEqualsNewReaction = clickedButton.InnerHtml.Contains(clickedReactionCountString);

            // Assert that button value incremented
            Assert.AreEqual(true, preClickCountEqualsReaction);
            Assert.AreEqual(true, clickedButtonEqualsNewReaction);
        }
        #endregion ButtonClick
    }
}
