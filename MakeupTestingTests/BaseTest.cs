using ER = AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;

namespace MakeupTestingTests
{
    public class BaseTest
    {
        public IWebDriver driver;
        protected ER.ExtentReports extentReports;
        protected ER.ExtentTest extentTest;

        struct ContextOfTest
        {
            public Status status;
            public string stackTrace;
            public string errorMessage;
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net8.0", "");
            var testClassName = TestContext.CurrentContext.Test.ClassName;
            var date = DateTime.Now.ToString(" dd-MM-yyyy_(HH_mm_ss)");
            var outputDir = $"{dir}\\Report\\{testClassName}{date}\\";
            var param = "Automation_Report.html";

            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter($"{outputDir}{param}");
            extentReports = new ER.ExtentReports();
            extentReports.AttachReporter(htmlReporter);
        }

        [SetUp]
        public void SetUp()
        {
            extentTest = extentReports.CreateTest(TestContext.CurrentContext.Test.MethodName);
            driver = new BrowsersList().GetBrowserByName(BrowserEnum.Chrome);
            driver.Navigate().GoToUrl("https://makeup.com.ua/ua/");
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            ContextOfTest test;
            test.stackTrace = TestContext.CurrentContext.Result.StackTrace;
            test.errorMessage = TestContext.CurrentContext.Result.Message;
            test.status = GetStatus(TestContext.CurrentContext.Result.Outcome.Status);
            AddTestHTML(test);

            driver.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extentReports.Flush();
        }

        private Status GetStatus(TestStatus testStatus)
        {
            switch (testStatus)
            {
                case TestStatus.Failed:
                    return Status.Fail;
                case TestStatus.Skipped:
                    return Status.Skip;
                default:
                    return Status.Pass;
            }
        }

        private void AddTestHTML(ContextOfTest test)
        {
            string stackTrace = string.IsNullOrEmpty(test.stackTrace) ? "\n<br>" : $"\n<br>{test.stackTrace}\n<br>";
            string errorMessage = string.IsNullOrEmpty(test.errorMessage) ? "\n<br>" : $"\n<br>{test.errorMessage}\n<br>";
            extentTest.Log(test.status, $"Test ended with {test.status}, {stackTrace}{errorMessage}");
        }
    }
}
