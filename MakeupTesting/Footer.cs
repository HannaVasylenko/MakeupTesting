﻿using MakeupTesting;
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
        public Footer(IWebDriver driver) : base(driver)
        {
        }
        private IWebElement linkYouTube => webDriver.FindElement(By.XPath("//li[@class='social__item social-icon yt']"));
        private IWebElement txtEmailSubscription => webDriver.FindElement(By.XPath("//input[@placeholder='Електронна пошта']"));
        private IWebElement btnEmailSubscription => webDriver.FindElement(By.XPath("//button[contains(text(), 'підписатися')]"));

        public void SelectYouTube()
        {
            ScrollDownByPixels(11800);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => linkYouTube.Displayed);
            linkYouTube.Click();
            ScrollDownByPixels(50);
        }

        public bool IsEmailSubscriptionErrorDisplayed()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                IWebElement errorElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email-field")));

                if (errorElement.GetAttribute("class") == "footer-input-group invalid")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public void InputEmail(string text)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementToBeClickable(txtEmailSubscription));
            //WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            //wait.Until(x => linkYouTube.Displayed);
            txtEmailSubscription.SendKeys(text);
        }
        public void ClickBtnEmailSubscription()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnEmailSubscription.Displayed);
            btnEmailSubscription.Click();
        }
    }
}
