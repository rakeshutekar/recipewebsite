using Bunit;
using NUnit.Framework;
using System.Linq;
using ContosoCrafts.WebSite.Components;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Microsoft.AspNetCore.Components;

namespace UnitTests.Components
{
    /// <summary>
    /// This class contains unit tests for the RecipeList razor components.
    /// </summary>
    public class RecipeListTests: Bunit.TestContext
    {
        #region TestSetup
        [SetUp]
        /// <summary>
        /// This setup method is executed before each test method is executed.
        /// </summary>
        public void TestInitialize()
        {
            // Test Initialization
        }
        #endregion TestSetup

        [Test]
        /// <summary>
        /// This method tests that the RecipeList component correctly renders
        /// contents.
        /// </summary>
        public void RecipeList_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileRecipeService>(TestHelper.RecipeService);

            // Act
            var page = RenderComponent<RecipeList>();
            // Get the cards returned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("Beef Taco"));
        }
    }
}
