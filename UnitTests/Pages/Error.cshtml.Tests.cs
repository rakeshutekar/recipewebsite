using ContosoCrafts.WebSite.Pages;
using Microsoft.AspNetCore.Http;
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
    public class ErrorTests
    {
        #region TestSetup
        public static ErrorModel pageModel;

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
        [Test]
        public void OnGet_Should_Set_RequestId_To_HttpContext_TraceIdentifier_If_Current_Activity_Is_Null()
        {
            Console.WriteLine(TestHelper.HttpContextDefault.TraceIdentifier);
            // Arrange
            if (Activity.Current != null) Activity.Current.Stop();

            // Act
            Console.WriteLine(pageModel.HttpContext == null);
            pageModel.PageContext = TestHelper.PageContext;
            pageModel.OnGet();
            // Assert
            Assert.AreEqual(pageModel.RequestId, TestHelper.HttpContextDefault.TraceIdentifier);
        }
        #endregion OnGet
    }
}
