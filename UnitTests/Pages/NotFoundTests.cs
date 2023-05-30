using ContosoCrafts.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace UnitTests.Pages
{

    public class NotFoundTests
    {
        #region TestSetup
        public static NotFoundModel pageModel; // mock page model used for testing

        /// <summary>
        /// Test Initialize - performs initialization before running any tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new NotFoundModel()
            {
            };
        }
        #endregion TestSetup
    }
}
