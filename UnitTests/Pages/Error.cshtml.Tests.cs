using ContosoCrafts.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Diagnostics;

namespace UnitTests.Pages
{
    /// <summary>
    /// Test class for error page tests
    /// </summary>
    public class ErrorTests
    {
        #region TestSetup
        public static ErrorModel pageModel; // mock page model used for testing

        /// <summary>
        /// Test Initialize - performs initialization before running any tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<ErrorModel>>();
            pageModel = new ErrorModel(MockLoggerDirect)
            {
            };
        }
        #endregion TestSetup

        #region RequestID
        /// <summary>
        /// Test to ensure that the ShowRequestId property returns expected result
        /// </summary>
        [Test]
        public void ShowRequestId_Should_Return_False_When_RequestId_Is_Null_Or_Empty()
        {
            // Arrange
            ErrorModel tempModel = new ErrorModel(null);
            
            // Act

            // Assert
            Assert.IsFalse(tempModel.ShowRequestId);
        }
        #endregion

        #region OnGet
        /// <summary>
        /// Test to ensure that OnGet sets the RequestId field to the Current Activity ID if the
        /// Current Activity is not null.
        /// </summary>
        [Test]
        public void OnGet_Should_Set_RequestId_To_Activity_Current_Id_If_Activity_Current_Is_Not_Null()
        {
            // Arrange
            if (Activity.Current == null) Activity.Current = new Activity("Unit Test Operation").Start();
            
            // Act
            pageModel.OnGet();
            // Assert
            Assert.AreEqual(pageModel.RequestId, Activity.Current.Id);
        }
        /// <summary>
        /// Test to ensure that OnGet sets the RequestId field to the HttpContext Trace Identifier if
        /// the current activity is null.
        /// </summary>
        [Test]
        public void OnGet_Should_Set_RequestId_To_HttpContext_TraceIdentifier_If_Current_Activity_Is_Null()
        {
            // Arrange
            if (Activity.Current != null) Activity.Current.Stop();

            // Act
            pageModel.PageContext = TestHelper.PageContext;
            pageModel.OnGet();
            // Assert
            Assert.AreEqual(pageModel.RequestId, TestHelper.HttpContextDefault.TraceIdentifier);
        }
        #endregion OnGet
    }
}