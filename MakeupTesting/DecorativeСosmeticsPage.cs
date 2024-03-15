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
        private IWebElement txtPriceMin => webDriver.FindElement(By.XPath("//input[@id='price-from']"));
        private IWebElement txtPriceMax => webDriver.FindElement(By.XPath("//input[@id='price-to']")); //window.scrollTo(0, 2000);
        private IWebElement OldpriceWithoutPromotion => webDriver.FindElement(By.XPath("//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price_old empty']/span[@class='price_item']")); //0

        private IWebElement priceWithoutPromotion => webDriver.FindElement(By.XPath("//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price']/span[@class='price_item']"));

        private IWebElement priceWithPromotion => webDriver.FindElement(By.XPath("//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price product-item__price_red']/span[@class='price_item']"));

        private IWebElement OldpriceWithPromotion => webDriver.FindElement(By.XPath("//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price_old']/span[@class='price_item']"));

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

        public void SetFilterByPrice(double priceMin, double priceMax)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => txtPriceMin.Displayed);

            txtPriceMin.Clear();
            txtPriceMin.SendKeys(priceMin.ToString());

            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript("arguments[0].value = '';", txtPriceMax); 
            js.ExecuteScript("arguments[0].value = arguments[1];", txtPriceMax, priceMax.ToString());
        }

        public Dictionary<string, double> GetSearchResultDetails()
        {
            Dictionary<string, double> productsDetails = new Dictionary<string, double>();

            foreach (var product in productList)
            {
                string title = product.FindElement(By.XPath("./a")).GetAttribute("data-default-name");
                string price;
                if (IsElementPresent(priceWithPromotion))
                {
                    price = product.FindElement(By.XPath(".//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price product-item__price_red']/span[@class='price_item']")).Text;
                }
                else
                {
                    if (IsElementPresent(priceWithoutPromotion))
                    {
                        price = product.FindElement(By.XPath(".//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price']/span[@class='price_item']")).Text;
                    }
                    else
                    {
                        price = "Price not available";
                    }
                }
                productsDetails.Add(title, double.Parse(price));
            }
            return productsDetails;
        }

        //public Dictionary<string, double> GetSearchResultDetails()
        //{
        //    Dictionary<string, double> productsDetails = new Dictionary<string, double>();

        //    foreach (var product in productList)
        //    {
        //        string title = product.FindElement(By.XPath("./a")).GetAttribute("data-default-name");

        //        string price;
        //        if (IsElementPresent(priceWithPromotion))
        //        {
        //            string currentPrice = priceWithPromotion.Text;
        //            string oldPrice = OldpriceWithPromotion.Text;
        //            price = $"{currentPrice} (old price: {oldPrice})";
        //        }
        //        else
        //        {
        //            if (IsElementPresent(priceWithoutPromotion))
        //            {
        //                price = priceWithoutPromotion.Text;
        //            }
        //            else
        //            {
        //                price = "Price not available";
        //            }
        //        }
        //        productsDetails.Add(title, double.Parse(price));
        //    }
        //    return productsDetails;
        //}



        private bool IsElementPresent(IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
