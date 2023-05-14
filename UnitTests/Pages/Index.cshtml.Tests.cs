using ContosoCrafts.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Pages
{
    public class IndexTests
    {
        #region TestSetup
        public static IndexModel pageModel; // mock page model used for testing

        /// <summary>
        /// Test Initialize - performs initialization before running any tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();
            pageModel = new IndexModel(MockLoggerDirect,TestHelper.RecipeService)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        [Test]
        public void OnGet_Should_Set_Recipes_To_All_Recipes_Retrieved_From_Recipe_Service()
        {

        }
        #endregion OnGet
    }
}
