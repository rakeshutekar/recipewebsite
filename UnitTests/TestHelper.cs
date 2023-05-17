
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

using Moq;

using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Internal;
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;

namespace UnitTests
{
    /// <summary>
    /// Helper to hold the web start settings
    ///
    /// HttpClient
    /// 
    /// Action Contect
    /// 
    /// The View Data and Teamp Data
    /// 
    /// The Product Service
    /// </summary>
    public static class TestHelper
    {
        public static Mock<IWebHostEnvironment> MockWebHostEnvironment;
        public static IUrlHelperFactory UrlHelperFactory;
        public static DefaultHttpContext HttpContextDefault;
        public static IWebHostEnvironment WebHostEnvironment;
        public static ModelStateDictionary ModelState;
        public static ActionContext ActionContext;
        public static EmptyModelMetadataProvider ModelMetadataProvider;
        public static ViewDataDictionary ViewData;
        public static TempDataDictionary TempData;
        public static PageContext PageContext;
        public static JsonFileRecipeService RecipeService;
        
        // Constant string test value
        public const string STRING_TEST_VAL = "TEST";
        // Test recipe model used for instantiating during tests
        // and validating changes
        public static RecipeModel TEST_RECIPE_MODEL
        {
            get
            {
                var newRecipe = new RecipeModel();
                newRecipe.Deleted = false;
                newRecipe.AuthorID = 0;
                newRecipe.FirstName = STRING_TEST_VAL;
                newRecipe.LastName = STRING_TEST_VAL;
                newRecipe.Title = STRING_TEST_VAL;
                newRecipe.Instructions = new string[] { STRING_TEST_VAL };
                newRecipe.Ingredients = new string[] { STRING_TEST_VAL };
                newRecipe.Tags = new string[] { STRING_TEST_VAL };
                newRecipe.PublishDate = STRING_TEST_VAL;
                newRecipe.EditDate = STRING_TEST_VAL;
                newRecipe.ImageCaption = STRING_TEST_VAL;
                newRecipe.Image = STRING_TEST_VAL;
                newRecipe.Comments = new List<CommentModel>()
                {
                    new CommentModel()
                    {
                        FirstName = STRING_TEST_VAL,
                        LastName = STRING_TEST_VAL,
                        Comment = STRING_TEST_VAL,
                        Id = System.Guid.NewGuid().ToString()
                    }
                };
                return newRecipe;
            }
        }
        // Reusable Test Comment Model
        public static CommentModel TEST_COMMENT_MODEL
        {
            get
            {
                return new CommentModel()
                {
                    FirstName = STRING_TEST_VAL,
                    LastName = STRING_TEST_VAL,
                    Comment = STRING_TEST_VAL,
                    Id = System.Guid.NewGuid().ToString()
                };
            }
        }


        /// <summary>
        /// Default Constructor
        /// </summary>
        static TestHelper()
        {
            MockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            MockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            MockWebHostEnvironment.Setup(m => m.WebRootPath).Returns(TestFixture.DataWebRootPath);
            MockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns(TestFixture.DataContentRootPath);

            HttpContextDefault = new DefaultHttpContext()
            {
                TraceIdentifier = "trace",
            };
            HttpContextDefault.HttpContext.TraceIdentifier = "trace";

            ModelState = new ModelStateDictionary();

            ActionContext = new ActionContext(HttpContextDefault, HttpContextDefault.GetRouteData(), new PageActionDescriptor(), ModelState);

            ModelMetadataProvider = new EmptyModelMetadataProvider();
            ViewData = new ViewDataDictionary(ModelMetadataProvider, ModelState);
            TempData = new TempDataDictionary(HttpContextDefault, Mock.Of<ITempDataProvider>());

            PageContext = new PageContext(ActionContext)
            {
                ViewData = ViewData,
                HttpContext = HttpContextDefault
            };

            RecipeService = new JsonFileRecipeService(MockWebHostEnvironment.Object);

            JsonFileRecipeService productService;

            productService = new JsonFileRecipeService(TestHelper.MockWebHostEnvironment.Object);
        }
    }
}
