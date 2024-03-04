using MakeupTesting;
using OpenQA.Selenium;
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

        private List<IWebElement> productList => webDriver.FindElements(By.XPath("//div[@class='catalog-products']")).ToList();

        private IWebElement btnlastPageInSearch => webDriver.FindElement(By.XPath("(//li[@class='page__item']/label)[last()]"));


        public string GetSearchTitleText() => titleText.Text;

        public void LastPageClick() => btnlastPageInSearch.Click();


        public List<string> GetProductTitleText()
        {
            List<string> result = new List<string>();

            foreach (var product in productList)
            {
                IWebElement productTitle = product.FindElement(By.XPath(".//a[@data-default-name]"));
                string productTitleText = productTitle.Text;
                result.Add(productTitleText);
            }
            return result;
        }
    }
}
