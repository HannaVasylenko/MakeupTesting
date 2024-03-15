using MakeupTesting;
using OpenQA.Selenium;
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

        public void SelectBeautyClub() => linkBeautyClub.Click();

        public void OpenDeliveryPage() => linkDelivery.Click();

        public void SwitchLanguageToru() => btnPageLanguageru.Click();

        public void SwitchLanguageToUA() => btnPageLanguageUA.Click();
        public void SelectCategory(string category) => linkDecorativeCosmetics(category).Click();
        public void SearchClick() => btnSearch.Click();
        public IWebElement GetDecorativeСosmeticsElement(string category) => linkDecorativeCosmetics(category);
        public void InputProductName(string text)
        {
            txtSearch.SendKeys(text);
            txtSearch.SendKeys(Keys.Enter);
        }
    }
}
