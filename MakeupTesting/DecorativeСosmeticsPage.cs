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
        public DecorativeСosmeticsPage(IWebDriver driver) : base(driver) { }

        private int numberOfClicksOnArrow = 0;

        private double convertToPriceOrDefault(string price, double defaultValue)
        {
            try
            {
                return Convert.ToDouble(price);
            }
            catch (FormatException)
            {
                return defaultValue;
            }
        }

        public class ProductComparator : IComparer<double>
        {
            public int Compare(double x, double y)
            {
                if (x == 0) return 1;
                return x.CompareTo(y);
            }
        }

        public int GetNumberOfClicksOnArrow()
        {
            return numberOfClicksOnArrow;
        }

        public void ClickRightArrowInTestimonialsSlider()
        {
            IWebElement btnArrowSliderRight = webDriver.FindElement(By.XPath("//div[contains(text(), 'Відгуки про Декоративна косметика')]/following-sibling::*//div[@class='slider-button right']"));
            ScrollDownByPixels(9000);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnArrowSliderRight.Displayed);
            btnArrowSliderRight.Click();
            numberOfClicksOnArrow++;
        }

        public void AddMoreProducts()
        {
            IWebElement btnMoreProducts = webDriver.FindElement(By.XPath("//div[text()='Більше товарів']"));

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnMoreProducts.Displayed);
            btnMoreProducts.Click();
        }

        public void SelectProductCard(string productAddToCart) => WaitUntilWebElementExists(By.XPath($"//a[text()='{productAddToCart}']")).Click();

        public string GetCategoryTitleText(string categoryTitle) => webDriver.FindElement(By.XPath($"//span[text()='{categoryTitle}']")).Text;

        public void CheckFiltersByNameAndTypeOfProduct(string nameOfBrand, string productName)
        {
            WaitUntilWebElementExists(By.XPath("//aside[@class='catalog-filter']"));

            IWebElement chbFilterByBrand = webDriver.FindElement(By.XPath($"//div[@class='catalog-filter-list-wrap']//a[text()='{nameOfBrand}']"));
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => chbFilterByBrand.Displayed);
            chbFilterByBrand.Click();

            //ScrollDownByPixels(800);
            IWebElement chbFilterByProduct = webDriver.FindElement(By.XPath($"//div[@class='catalog-filter-list-wrap']//span[text()='{productName}']"));
            wait.Until(x => chbFilterByProduct.Displayed);
            chbFilterByProduct.Click();
        }

        public List<(string productName, string productType)> GetProductTitleText()
        {
            return webDriver
                .FindElements(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']//div[@class='info-product-wrapper']/a"))
                .ToList()
                .ConvertAll(e => (e.Text, e.GetAttribute("data-default-name")));
        }

        public void SetFilterByPrice(double priceMin, double priceMax)
        {
            WaitUntilWebElementExists(By.XPath("//aside[@class='catalog-filter']"));

            //ScrollDownByPixels(1800);
            IWebElement txtPriceMin = webDriver.FindElement(By.XPath("//input[@id='price-from']"));
            txtPriceMin.Clear();
            txtPriceMin.SendKeys(priceMin.ToString());

            IWebElement txtPriceMax = webDriver.FindElement(By.XPath("//input[@id='price-to']"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript("arguments[0].value = '';", txtPriceMax);
            js.ExecuteScript("arguments[0].value = arguments[1];", txtPriceMax, priceMax.ToString());
        }

        public Dictionary<string, double> GetSearchResultDetails()
        {
            Dictionary<string, double> productsDetails = new Dictionary<string, double>();

            List<IWebElement> productList = webDriver.FindElements(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']//div[@class='info-product-wrapper']")).ToList();
            foreach (var product in productList)
            {
                string title = product.FindElement(By.XPath("./a")).GetAttribute("data-default-name");
                string price;
                try
                {
                    price = product.FindElement(By.XPath(".//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price product-item__price_red']/span[@class='price_item']")).Text;
                }
                catch (NoSuchElementException)
                {
                    try
                    {
                        price = product.FindElement(By.XPath(".//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price']/span[@class='price_item']")).Text;
                    }
                    catch (NoSuchElementException)
                    {
                        price = "0";
                    }
                }

                productsDetails.Add(title, convertToPriceOrDefault(price, 0.0));
            }
            return productsDetails;
        }

        public List<double> GetSearchResultPrices()
        {
            List<double> productsDetails = new List<double>();

            List<IWebElement> list = webDriver.FindElements(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']//div[@class='info-product-wrapper']")).ToList();
            foreach (var product in list)
            {
                string price;
                try
                {
                    price = product.FindElement(By.XPath(".//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price product-item__price_red']/span[@class='price_item']")).Text;
                }
                catch (NoSuchElementException ex)
                {
                    try
                    {
                        price = product.FindElement(By.XPath(".//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price']/span[@class='price_item']")).Text;
                    }
                    catch (NoSuchElementException e)
                    {
                        price = "";
                    }
                }

                if (price != "")
                {
                    productsDetails.Add(double.Parse(price));
                }
            }
            return productsDetails;
        }

        public void SelectDropdownSortBy() => WaitUntilWebElementExists(By.XPath("//div[@class='catalog-sort-wrapper']")).Click();
        
        public void SelectValueSortBy(string valueSortBy) => WaitUntilWebElementExists(By.XPath($"//label[contains(text(), '{valueSortBy}')]")).Click();
        
        public void RemoveFilters() => WaitUntilWebElementExists(By.XPath("//div[@class='selected-filter-list__item cancel-filter active']")).Click();

        public bool IsRemoveFiltersButtonPresent() => IsElementExists(By.XPath("//div[@class='selected-filter-list__item cancel-filter active']"));

        public int CountProductsInList() => GetSearchResultDetails().Count;
        
        public int GetIndexOfActiveTestimonialPage()
        {
            return webDriver
                .FindElements(By.XPath("//div[contains(text(), 'Відгуки про Декоративна косметика')]/following-sibling::*//div[@class='slider-button left']/label"))
                .ToList()
                .FindIndex(e => e.GetAttribute("class").Contains("active"));
        }
    }
}
