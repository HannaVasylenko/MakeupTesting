using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MakeupTesting
{
    public class BasePage
    {
        public static IWebDriver webDriver;

        public BasePage(IWebDriver driver)
        {
            webDriver = driver;
        }

        public void ScrollDownByPixels(int pixels)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript($"window.scrollTo(0, {pixels});");
            Thread.Sleep(1000);
        }

        public IWebElement WaitUntilWebElementExists(By by, int seconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds));
            return wait.Until(e => webDriver.FindElement(by));
        }

        public bool IsElementExists(By by)
        {
            try
            {
                webDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
