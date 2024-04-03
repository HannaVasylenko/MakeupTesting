using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{
    /// <summary>
    /// The class is an page-element located at the bottom of each page.
    /// </summary>
    public class Footer : BasePage
    {
        public Footer(IWebDriver driver) : base(driver) {}

        /// <summary>
        /// Clicks on the YouTube social media icon.
        /// </summary>
        public void SelectYouTube() => WaitUntilWebElementExists(By.XPath("//li[@class='social__item social-icon yt']")).Click();

        /// <summary>
        /// Checks if the email subscription error message is displayed.
        /// </summary>
        /// <returns>True if the error message is displayed, otherwise false.</returns>
        public bool IsEmailSubscriptionErrorDisplayed() => WaitUntilWebElementExists(By.Id("email-field")).GetAttribute("class") == "footer-input-group invalid";

        /// <summary>
        /// Enters the specified email into the email input field.
        /// </summary>
        /// <param name="text">The email to be entered.</param>
        public void InputEmail(string text) => WaitUntilWebElementExists(By.XPath("//input[@placeholder='Електронна пошта']")).SendKeys(text);

        /// <summary>
        /// Clicks on the button to subscribe via email.
        /// </summary>
        public void ClickBtnEmailSubscription() => WaitUntilWebElementExists(By.XPath("//button[contains(text(), 'підписатися')]")).Click();
    }
}
