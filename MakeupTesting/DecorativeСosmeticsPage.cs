using MakeupTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        private List<IWebElement> productList => webDriver.FindElements(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']//div[@class='info-product-wrapper']")).ToList(); // //div[@class='catalog-products']/ul/li
        private IWebElement linkProductTitle(string productAddToCart) => webDriver.FindElement(By.XPath($"//a[text()='{productAddToCart}']"));

        public void SelectProductCard(string productAddToCart)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => linkProductTitle(productAddToCart).Displayed);
            linkProductTitle(productAddToCart).Click();
        }

        public string GetCategoryTitleText(string categoryTitle) => titleDecorativeCosmetics(categoryTitle).Text;

        public void CheckFiltersByNameAndTypeOfProduct(string nameOfBrand, string productName)
        {
            chbFilterByBrand(nameOfBrand).Click();
            chbFilterByProduct(productName).Click();
        }

        public List<(string productName, string productType)> GetProductTitleText()
        {
            List<(string productName, string productType)> result = new List<(string productName, string productType)>();

            foreach (var product in productList)
            {
                var productName = product.FindElement(By.XPath("./a")).Text;
                var productType = product.FindElement(By.XPath("./a")).GetAttribute("data-default-name");
                result.Add((productName, productType));
            }
            return result;
        }
    }
}
