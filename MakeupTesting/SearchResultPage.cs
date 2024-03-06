﻿using MakeupTesting;
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

        private List<IWebElement> productList => webDriver.FindElements(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']//div[@class='info-product-wrapper']")).ToList();

        private IWebElement btnlastPageInSearch => webDriver.FindElement(By.XPath("(//li[@class='page__item']/label)[last()]"));

        public string GetSearchTitleText() => titleText.Text;

        public void LastPageClick() => btnlastPageInSearch.Click();

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
    }
}
