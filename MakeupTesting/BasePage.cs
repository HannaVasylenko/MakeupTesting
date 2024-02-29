using OpenQA.Selenium;

namespace MakeupTesting
{
    public class BasePage
    {
        public static IWebDriver webDriver;

        public BasePage(IWebDriver driver)
        {
            webDriver = driver;
        }
    }
}
