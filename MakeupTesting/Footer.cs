using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{
    public class Footer : BasePage
    {
        public Footer(IWebDriver driver) : base(driver) {}

        public void SelectYouTube() => WaitUntilWebElementExists(By.XPath("//li[@class='social__item social-icon yt']")).Click();

        public bool IsEmailSubscriptionErrorDisplayed() => WaitUntilWebElementExists(By.Id("email-field")).GetAttribute("class") == "footer-input-group invalid";
        
        public void InputEmail(string text) => WaitUntilWebElementExists(By.XPath("//input[@placeholder='Електронна пошта']")).SendKeys(text);
        
        public void ClickBtnEmailSubscription() => WaitUntilWebElementExists(By.XPath("//button[contains(text(), 'підписатися')]")).Click();
    }
}
