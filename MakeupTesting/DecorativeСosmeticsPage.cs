using MakeupTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    /// <summary>
    /// The class is a page of a separate category - Decorative cosmetics.
    /// </summary>
    public class DecorativeСosmeticsPage : BasePage
    {
        public DecorativeСosmeticsPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement titleDecorativeCosmetics(string categoryTitle) => webDriver.FindElement(By.XPath($"//span[text()='{categoryTitle}']"));
        private IWebElement chbFilterByBrand(string nameOfBrand) => webDriver.FindElement(By.XPath($"//div[@class='catalog-filter-list-wrap']//a[text()='{nameOfBrand}']"));
        private IWebElement chbFilterByProduct(string filterProductName) => webDriver.FindElement(By.XPath($"//div[@class='catalog-filter-list-wrap']//span[text()='{filterProductName}']"));
        private List<IWebElement> productList => webDriver.FindElements(By.XPath("//div[@class='catalog-products']//li//div[@class='simple-slider-list__link']")).ToList(); // //div[@class='catalog-products']/ul/li


        public string GetCategoryTitleText(string categoryTitle) => titleDecorativeCosmetics(categoryTitle).Text;

        public void CheckFilters(string nameOfBrand, string productName)
        {
            chbFilterByBrand(nameOfBrand).Click();
            chbFilterByProduct(productName).Click();
        }

        public List<string> GetProductTitleText()
        {
            List<string> result = new List<string>();

            foreach (var product in productList)
            {
                string productTitle = webDriver.FindElement(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']/li/div[@class='simple-slider-list__link']/div[@class='info-product-wrapper']/a")).GetAttribute("data-default-name"); // .//div[@class='info-product-wrapper']/a
                result.Add(productTitle);
            }
            return result;
        }
    }
}
