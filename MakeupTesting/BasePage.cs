using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MakeupTesting
{
    public abstract class BasePage
    {
        protected readonly IWebDriver webDriver;

        protected BasePage(IWebDriver driver)
        {
            webDriver = driver;
        }

        /// <summary>
        /// Waits until a web element identified by the specified locator exists within the specified time frame.
        /// </summary>
        /// <param name="by">The By object used to locate the web element.</param>
        /// <param name="seconds">The maximum time to wait for the web element to exist, in seconds. Default is 10 seconds.</param>
        /// <returns>The first IWebElement found using the given locator.</returns>
        protected IWebElement WaitUntilWebElementExists(By by, int seconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds));
            return wait.Until(e => webDriver.FindElement(by));
        }

        /// <summary>
        /// Checks if an element identified by the specified locator exists.
        /// </summary>
        /// <param name="by">The By object used to locate the element.</param>
        /// <returns>True if the element exists, false otherwise.</returns>
        protected bool IsElementExists(By by)
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

        /// <summary>
        /// Waits until the specified condition defined by the function is met within the specified time frame.
        /// </summary>
        /// <param name="func">The function representing the condition to wait for. It takes an IWebDriver as input and returns a boolean indicating whether the condition is met.</param>
        /// <param name="seconds">The maximum time to wait for the condition to be met, in seconds. Default is 10 seconds.</param>
        protected void WaitUntil(Func<IWebDriver, bool> func, int seconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds));
            wait.Until(func);
        }

        /// <summary>
        /// Switches the driver's focus to the specified browser window by its handle or name and waits until the new window's title is not null.
        /// </summary>
        /// <param name="window">The handle or name of the window to switch to.</param>
        public void SwitchToWindow(string window)
        {
            webDriver.SwitchTo().Window(window);
            WaitUntil(e => webDriver.Title != null);
        }

        /// <summary>
        /// Retrieves a list of handles for all currently open browser windows.
        /// </summary>
        /// <returns>A list of strings representing the window handles.</returns>
        public List<string> GetAllWindows() => webDriver.WindowHandles.ToList();

        /// <summary>
        /// Retrieves the handle of the currently focused browser window.
        /// </summary>
        /// <returns>A string representing the handle of the current window.</returns>
        public string GetCurrentWindow() => webDriver.CurrentWindowHandle;
    }
}
