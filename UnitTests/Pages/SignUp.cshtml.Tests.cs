using ContosoCrafts.WebSite.Pages;
using NUnit.Framework;

namespace UnitTests.Pages
{
    public class SignUpTests
    {
        #region TestSetup
        // mock page model used for testing
        public static SignUpModel pageModel;

        /// <summary>
        /// Test initialize - performs initialization before running any tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new SignUpModel();
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Tests that OnGet does not throw exceptions when called
        /// </summary>
        [Test]
        public void SignUpModel_OnGet_Should_Not_Throw_An_Exception()
        {
            Assert.DoesNotThrow(() => pageModel.OnGet());
        }
        #endregion OnGet

        #region OnPost
        /// <summary>
        /// Tests that OnPost returns a non-null Action Result
        /// </summary>
        [Test]
        public void SignUpModel_OnPost_Should_Return_Not_Null()
        {
            Assert.NotNull(pageModel.OnPost());
        }
        #endregion OnPost
    }
}
