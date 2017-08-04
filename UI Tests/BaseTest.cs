using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using UI_Tests.Properties;
using Framework;

namespace UI_Tests
{
    [Parallelizable]
    [TestFixture]
    internal abstract class BaseTest
    {
        protected Library Lib;
        protected IWebDriver Driver;
        public string[] Table;
        private string TestClass;
        private string DataSheet;
        private string PDFDirectory;
        private string DownloadDirectory;
        private static string Host;
        private ScreenShot ScreenShot;
        private static string Platform;
        private static string BrowserName;
        private static string BaseDirectory;
        private static string BrowserVersion;
        private static string VendorDirectory;
        private string ScreenshotDirectory;
        protected internal static string Environment;
        protected internal static string BaseUrl;

        [OneTimeSetUp]
        protected void OneTimeSetUp()
        {
            LoadConfigValues();
        }

        [SetUp]
        protected void SetUp()
        {
            TestClass = TestContext.CurrentContext.Test.ClassName;

            //CreateLibrary(TestClass);

            switch (Host.ToLower())
            {
                case "localhost":
                    switch (BrowserName.ToLower())
                    {
                        case "firefox":
                            var service = FirefoxDriverService.CreateDefaultService(VendorDirectory);
                            Driver = new FirefoxDriver(service);
                            break;
                        case "chrome":
                            ChromeOptions options = new ChromeOptions();
                            options.AddUserProfilePreference("download.default_directory", PDFDirectory);
                            options.AddUserProfilePreference("download.prompt_for_download", false);
                            Driver = new ChromeDriver(VendorDirectory, options);
                            break;
                    }
                    break;
            }
        }

        [TearDown]
        protected void TearDown()
        {
            var testPassed = TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Passed);
            var status = TestContext.CurrentContext.Result.Outcome.Status.ToString();
            var testName = TestContext.CurrentContext.Test.Name;
            var fullName = TestContext.CurrentContext.Test.FullName;

            Debug.WriteLine(fullName);
            var screenShotName = ScreenshotDirectory + " " + testName + " " + DateTime.Now.ToString("MMMM dd") + " " + status + ".png";

            Driver.Quit();
        }

        private void LoadConfigValues()
        {
            Host = Settings.Default.Host;
            BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            BrowserName = Settings.Default.BrowserName;
            BaseUrl = Settings.Default.BaseUrl;
            VendorDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).
                                      Parent?.Parent?.FullName + @"\Vendor";
            ScreenshotDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).
                                      Parent?.Parent?.FullName + @"\Screenshots\";
            DownloadDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).
                                      Parent?.Parent?.FullName + @"\Downloads\";

            //Set Current Directory for easier file references
            Directory.SetCurrentDirectory(BaseDirectory);
        }

        private void CreateLibrary(string testClass)
        {
            Lib = new Library();

            Table = new Library().GetTable(testClass);

            DataSheet = "../../Data/" + Table[0] + "_" + Environment + ".xlsx";

            PDFDirectory = DownloadDirectory + Table[0] + "\\" + Table[1] + "\\";

            //Open/Save Excel file to update date formulas
            Lib.OpenAndSave(DataSheet);

            //Add data to the datatable
            Lib.PopulateInCollection(DataSheet, Table[1]);
        }

        protected void Rename(string oldName, string newName)
        {
            FileAssert.Exists(PDFDirectory + oldName);
            File.Move(PDFDirectory + oldName, PDFDirectory + newName + " " + DateTime.Now.ToString("dd_MMMM_HHmmss") + " " + oldName);
        }
    }
}