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
        private List<IWebElement> productList => webDriver.FindElements(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']//div[@class='info-product-wrapper']")).ToList();
        private IWebElement linkProductTitle(string productAddToCart) => webDriver.FindElement(By.XPath($"//a[text()='{productAddToCart}']"));
        private IWebElement txtPriceMin => webDriver.FindElement(By.XPath("//input[@id='price-from']"));
        private IWebElement txtPriceMax => webDriver.FindElement(By.XPath("//input[@id='price-to']"));
        private IWebElement ddlSortBy => webDriver.FindElement(By.XPath("//div[@class='catalog-sort-wrapper']"));
        private IWebElement linkValueSortBy(string valueSortBy) => webDriver.FindElement(By.XPath($"//label[contains(text(), '{valueSortBy}')]"));
        private IWebElement btnRemoveFilters => webDriver.FindElement(By.XPath("//div[@class='selected-filter-list__item cancel-filter active']"));
        private IWebElement btnMoreProducts => webDriver.FindElement(By.XPath("//div[text()='Більше товарів']"));
        private List<IWebElement> testimonialsList => webDriver.FindElements(By.XPath("//div[contains(text(), 'Відгуки про Декоративна косметика')]/following-sibling::*//div[@class='slider-button left']/label")).ToList();
        private IWebElement btnArrowSliderRight => webDriver.FindElement(By.XPath("//div[contains(text(), 'Відгуки про Декоративна косметика')]/following-sibling::*//div[@class='slider-button right']"));
        private IWebElement catalogSort => webDriver.FindElement(By.XPath("//ul[@class='catalog-sort-list']"));

        private int numberOfClicksOnArrow = 0;

        public int GetNumberOfClicksOnArrow()
        {
            return numberOfClicksOnArrow;
        }
        public void ClickRightArrowInTestimonialsSlider()
        {
            ScrollDownByPixels(9000);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnArrowSliderRight.Displayed);
            btnArrowSliderRight.Click();
            numberOfClicksOnArrow++;
        }
       
        public void AddMoreProducts()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnMoreProducts.Displayed);
            wait.Until(ExpectedConditions.ElementToBeClickable(btnMoreProducts));
            btnMoreProducts.Click();
        }

        public void SelectProductCard(string productAddToCart)
        {
            //WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            //wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => linkProductTitle(productAddToCart).Displayed);
            linkProductTitle(productAddToCart).Click();
        }

        public string GetCategoryTitleText(string categoryTitle) => titleDecorativeCosmetics(categoryTitle).Text;

        public void CheckFiltersByNameAndTypeOfProduct(string nameOfBrand, string productName)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            chbFilterByBrand(nameOfBrand).Click();
            //wait.Until(ExpectedConditions.ElementToBeSelected(chbFilterByBrand(nameOfBrand)));
            wait.Until(x => chbFilterByBrand(nameOfBrand).Displayed);
            ScrollDownByPixels(800);
            chbFilterByProduct(productName).Click();
            //wait.Until(ExpectedConditions.ElementToBeSelected(chbFilterByProduct(productName)));
            wait.Until(x => chbFilterByProduct(productName).Displayed);
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
            ScrollDownByPixels(1800);
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

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']//div[@class='info-product-wrapper']")));

            List<IWebElement> list =  webDriver.FindElements(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']//div[@class='info-product-wrapper']")).ToList();
            foreach (var product in list)
            {
                string title = product.FindElement(By.XPath("./a")).GetAttribute("data-default-name");
                string price;
                try
                {
                    price = product.FindElement(By.XPath(".//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price product-item__price_red']/span[@class='price_item']")).Text;
                }
                catch (NoSuchElementException ex)
                {
                    price = product.FindElement(By.XPath(".//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price']/span[@class='price_item']")).Text;
                }

                productsDetails.Add(title, double.Parse(price));
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

        public void SelectDropdownSortBy()
        {
            ScrollDownByPixels(50);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => ddlSortBy.Displayed);
            ddlSortBy.Click();
        }
        public void SelectValueSortBy(string valueSortBy)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => linkValueSortBy(valueSortBy).Displayed);
            linkValueSortBy(valueSortBy).Click();
        }
        public void RemoveFilters()
        {
            ScrollDownByPixels(50);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnRemoveFilters.Displayed);
            btnRemoveFilters.Click();
        }

        public bool IsRemoveFiltersButtonPresent()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                IWebElement button = wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='selected-filter-list__item cancel-filter active']")));
                return button != null && button.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public int CountProductsInList()
        {
            ScrollDownByPixels(8600);
            return productList.Count;
        }
        public int GetIndexOfActiveTestimonialPage()
        {
            for (int i = 0; i < testimonialsList.Count; i++)
            {
                if (testimonialsList[i].GetAttribute("class").Contains("active"))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
