using System.IO;

using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Test fixture class that sets up the testing framework
    /// </summary>
    [SetUpFixture]
    public class TestFixture
    {
        // Path to the Web Root
        public static string DataWebRootPath = "./wwwroot";

        // Path to the data folder for the content
        public static string DataContentRootPath = "./data/";

        /// <summary>
        /// Initialization function that runs before any tests to grab a fres
        /// copy of the json file "database"
        /// </summary>
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            // Run this code once when the test harness starts up.

            // This will copy over the latest version of the database files

            var DataWebPath = "../../../../src/bin/Debug/net6.0/wwwroot/data";
            var DataUTDirectory = "wwwroot";
            var DataUTPath = DataUTDirectory + "/data";

            //// Delete the Detination folder
            if (Directory.Exists(DataUTDirectory))
            {
                Directory.Delete(DataUTDirectory, true);
            }

            // Make the directory
            Directory.CreateDirectory(DataUTPath);

            // Copy over all data files
            var filePaths = Directory.GetFiles(DataWebPath);
            foreach (var filename in filePaths)
            {
                string OriginalFilePathName = filename.ToString();
                var newFilePathName = OriginalFilePathName.Replace(DataWebPath, DataUTPath);

                File.Copy(OriginalFilePathName, newFilePathName);
            }
        }

        /// <summary>
        /// Teardown function after all tests have run
        /// </summary>
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}