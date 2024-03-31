using MakeupTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    public class Footer : BasePage
    {
        public Footer(IWebDriver driver) : base(driver) {}

        public void SelectYouTube() => webDriver.FindElement(By.XPath("//li[@class='social__item social-icon yt']")).Click();

        public bool IsEmailSubscriptionErrorDisplayed() => webDriver.FindElement(By.Id("email-field")).GetAttribute("class") == "footer-input-group invalid";
        
        public void InputEmail(string text) => webDriver.FindElement(By.XPath("//input[@placeholder='Електронна пошта']")).SendKeys(text);
        
        public void ClickBtnEmailSubscription() => webDriver.FindElement(By.XPath("//button[contains(text(), 'підписатися')]")).Click();
    }
}
