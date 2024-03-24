using MakeupTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MakeupTestingPageObjects
{
    public class SearchResultPage : BasePage
    {
        public SearchResultPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement titleText => webDriver.FindElement(By.XPath("//div[@class='search-results info-text']"));
        private IWebElement titleCategory(string breadCrumbsVariant) => webDriver.FindElement(By.XPath($"//h1[contains(text(), '{breadCrumbsVariant}')]"));

        private List<IWebElement> productList => webDriver.FindElements(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']//div[@class='info-product-wrapper']")).ToList();

        private IWebElement btnLastPageInSearch => webDriver.FindElement(By.XPath("(//li[@class='page__item']/label)[last()]"));

        public string GetSearchTitleText()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => titleText.Displayed); //.Enabled
            return titleText.Text;
        }

        public void LastPageClick()
        {
            ScrollDownByPixels(7500);
            btnLastPageInSearch.Click();
        }

        public List<string> GetProductTitleText()
        {
            List<string> result = new List<string>();

            foreach (var product in productList)
            {
                string productTitle = product.FindElement(By.XPath(".//a")).GetAttribute("data-default-name");
                result.Add(productTitle);
            }
            return result;
        }
        public string GetBreadCrumbsTitle(string breadCrumbsVariant)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => titleCategory(breadCrumbsVariant).Displayed);
            return titleCategory(breadCrumbsVariant).Text;
        }
    }
}
