using MakeupTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    public class InitPage : BasePage
    {
        public InitPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement btnPageLanguageru => webDriver.FindElement(By.XPath("//header//a[text()='Рус']"));
        private IWebElement btnPageLanguageUA => webDriver.FindElement(By.XPath("//header//a[text()='Укр']"));
        private IWebElement btnSearch => webDriver.FindElement(By.XPath("//div[@data-popup-handler='search']"));
        private IWebElement txtSearch => webDriver.FindElement(By.XPath("//input[@itemprop='query-input']"));
        private IWebElement linkDecorativeCosmetics => webDriver.FindElement(By.XPath("//a[text()='Макіяж']"));
        private IWebElement linkSubCategory => webDriver.FindElement(By.XPath("//a[text()='Очі']"));
        private IWebElement titleSubCategory => webDriver.FindElement(By.XPath("//span[text()='Косметика для очей']"));

        public void SwitchLanguageToru() => btnPageLanguageru.Click();

        public void SwitchLanguageToUA() => btnPageLanguageUA.Click();
        public void SelectCategory() => linkDecorativeCosmetics.Click();

        public void SelectSubCategory() => linkSubCategory.Click();

        public void InputProductName(string text)
        {
            txtSearch.SendKeys(text);
            txtSearch.SendKeys(Keys.Enter);
        }

        public void SearchClick() => btnSearch.Click();

        public string GetSubCategoryTitleText() => titleSubCategory.Text;

        public IWebElement GetDecorativeСosmeticsElement() => linkDecorativeCosmetics;
    }
}
