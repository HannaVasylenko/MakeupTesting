using MakeupTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    public class Header : BasePage
    {
        public Header(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement btnPageLanguageru => webDriver.FindElement(By.XPath("//header//a[text()='Рус']"));
        private IWebElement btnPageLanguageUA => webDriver.FindElement(By.XPath("//header//a[text()='Укр']"));
        private IWebElement linkDelivery => webDriver.FindElement(By.XPath("//li[@class='header-top-list__item']/a[text()='Доставка та Оплата']"));
        private IWebElement btnSearch => webDriver.FindElement(By.XPath("//div[@data-popup-handler='search']"));
        private IWebElement txtSearch => webDriver.FindElement(By.XPath("//input[@itemprop='query-input']"));

        private IWebElement linkDecorativeCosmetics(string category) => webDriver.FindElement(By.XPath($"//a[text()='{category}']"));
        private IWebElement linkBeautyClub => webDriver.FindElement(By.XPath("//a[@class='header-top-list__link bc-about-link']"));

        private IWebElement linkHintFeatures => webDriver.FindElement(By.XPath("//span[text()='Безкоштовна доставка по Україні!']"));

        private IWebElement hintFeatures => webDriver.FindElement(By.XPath("//span[text()='Безкоштовна доставка по Україні!']/parent::*"));
        private IWebElement linkBrands => webDriver.FindElement(By.XPath("//a[text()='Бренди']"));

        public void SelectBeautyClub()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => linkBeautyClub.Displayed);
            linkBeautyClub.Click();
        }

        public void SelectBrandsPage()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => linkBrands.Displayed);
            linkBrands.Click();
        }

        public void OpenDeliveryPage()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => linkBrands.Displayed);
            linkDelivery.Click();
        }

        public void SwitchLanguageToru()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnPageLanguageru.Displayed);
            btnPageLanguageru.Click();
        }

        public void SwitchLanguageToUA()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnPageLanguageUA.Displayed);
            btnPageLanguageUA.Click();
        }

        public void SelectCategory(string category)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => linkDecorativeCosmetics(category).Displayed);
            linkDecorativeCosmetics(category).Click();
        }

        public void SearchClick()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnSearch.Displayed);
            btnSearch.Click();
        }

        public IWebElement GetDecorativeСosmeticsElement(string category) => linkDecorativeCosmetics(category);
        public void InputProductName(string text)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementToBeClickable(txtSearch));
            txtSearch.SendKeys(text);
            txtSearch.SendKeys(Keys.Enter);
        }
        public IWebElement GetHintFeatures() => linkHintFeatures;
        public string GetHintText() => hintFeatures.GetAttribute("data-text");
    }
}
