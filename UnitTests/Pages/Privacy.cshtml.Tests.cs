﻿using ContosoCrafts.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace UnitTests.Pages
{
    /// <summary>
    /// Test class for privacy page tests
    /// </summary>
    public class PrivacyTests
    {
        // Page model used for testing
        public static PrivacyModel pageModel;
        /// <summary>
        /// Perform one-time setup before running tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<PrivacyModel>>();
            pageModel = new PrivacyModel(MockLoggerDirect)
            {
            };
        }

        /// <summary>
        /// Test ensures that page model constructor has initialized a valid page model
        /// </summary>
        [Test]
        public void Constructor_Should_Initialize_Valid_Model()
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(pageModel);
        }

        /// <summary>
        /// Test ensures that OnGet does not compromise model state
        /// </summary>
        [Test]
        public void OnGet_Should_Not_Compromise_Model()
        {
            // Arrange
            // Act
            pageModel.OnGet();
            // Assert
            Assert.NotNull(pageModel);
        }
    }
}