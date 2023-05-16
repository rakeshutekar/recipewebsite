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
    public class RecipeListTests: Bunit.TestContext
    {

        #region TestSetup
        [SetUp]
        public void TestInitialize()
        {
            // Test Initialization


        }
        #endregion TestSetup

        [Test]
        public void RecipeList_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileRecipeService>(TestHelper.RecipeService);

            // Act
            var page = RenderComponent<RecipeList>();
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("Beef Taco"));
        }




    }
}
