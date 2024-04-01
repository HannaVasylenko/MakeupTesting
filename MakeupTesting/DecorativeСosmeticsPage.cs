using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{
    /// <summary>
    /// The class is a page of a separate category - Decorative cosmetics.
    /// </summary>
    public class DecorativeСosmeticsPage : BasePage
    {
        private int numberOfClicksOnArrow = 0;

        public DecorativeСosmeticsPage(IWebDriver driver) : base(driver) { }

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
            IWebElement btnArrowSliderRight = WaitUntilWebElementExists(By.XPath("//div[contains(text(), 'Відгуки про Декоративна косметика')]/following-sibling::*//div[@class='slider-button right']"));
            btnArrowSliderRight.Click();
            numberOfClicksOnArrow++;
        }

        public void AddMoreProducts() => WaitUntilWebElementExists(By.XPath("//div[text()='Більше товарів']")).Click();

        public void SelectProductCard(string productAddToCart) => WaitUntilWebElementExists(By.XPath($"//a[text()='{productAddToCart}']")).Click();

        public string GetCategoryTitleText(string categoryTitle) => WaitUntilWebElementExists(By.XPath($"//span[text()='{categoryTitle}']")).Text;

        public void CheckFiltersByNameAndTypeOfProduct(string nameOfBrand, string productName)
        {
            WaitUntilWebElementExists(By.XPath("//aside[@class='catalog-filter']"));

            IWebElement chbFilterByBrand = webDriver.FindElement(By.XPath($"//div[@class='catalog-filter-list-wrap']//a[text()='{nameOfBrand}']"));
            WaitUntil(x => chbFilterByBrand.Displayed);
            chbFilterByBrand.Click();

            IWebElement chbFilterByProduct = webDriver.FindElement(By.XPath($"//div[@class='catalog-filter-list-wrap']//span[text()='{productName}']"));
            WaitUntil(x => chbFilterByProduct.Displayed);
            chbFilterByProduct.Click();
        }

        public List<(string productName, string productType)> GetProductTitleText() => webDriver.FindElements(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']//div[@class='info-product-wrapper']/a"))
                .ToList()
                .ConvertAll(e => (e.Text, e.GetAttribute("data-default-name")));

        public void SetFilterByPrice(double priceMin, double priceMax)
        {
            WaitUntilWebElementExists(By.XPath("//aside[@class='catalog-filter']"));

            IWebElement txtPriceMin = WaitUntilWebElementExists(By.XPath("//input[@id='price-from']"));
            txtPriceMin.Clear();
            txtPriceMin.SendKeys(priceMin.ToString());
            WaitUntil(e => txtPriceMin.GetAttribute("value").Equals(priceMin.ToString()));

            IWebElement txtPriceMax = WaitUntilWebElementExists(By.XPath("//input[@id='price-to']"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript("arguments[0].value = '';", txtPriceMax);
            js.ExecuteScript("arguments[0].value = arguments[1];", txtPriceMax, priceMax.ToString());
            WaitUntil(e => txtPriceMax.GetAttribute("value").Equals(priceMax.ToString()));
            txtPriceMax.Click();
        }

        public Dictionary<string, double> GetSearchResultDetails()
        {
            Dictionary<string, double> productsDetails = new Dictionary<string, double>();

            WaitUntil(e =>
            {
                try
                {
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
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    productsDetails.Clear();
                    return false;
                }
            });

            return productsDetails;
        }

        public void SelectDropdownSortBy() => WaitUntilWebElementExists(By.XPath("//div[@class='catalog-sort-wrapper']")).Click();

        public void SelectValueSortByPrice(string valueSortBy)
        {
            WaitUntilWebElementExists(By.XPath($"//label[contains(text(), 'вартістю')]")).Click();
            WaitUntil(e =>
            {
                List<double> values = GetSearchResultDetails().Values.ToList();
                for (int i = 0; i < values.Count - 1; i++)
                {
                    if (values[i] != 0 && values[i + 1] != 0 && values[i] > values[i + 1])
                    {
                        return false;
                    }
                }

                return true;
            });
        }

        public void RemoveFilters() => WaitUntilWebElementExists(By.XPath("//div[@class='selected-filter-list__item cancel-filter active']")).Click();

        public bool IsRemoveFiltersButtonPresent() => IsElementExists(By.XPath("//div[@class='selected-filter-list__item cancel-filter active']"));

        public int CountProductsInList() => GetSearchResultDetails().Count;

        public int GetIndexOfActiveTestimonialPage() => webDriver
                .FindElements(By.XPath("//div[contains(text(), 'Відгуки про Декоративна косметика')]/following-sibling::*//div[@class='slider-button left']/label"))
                .ToList()
                .FindIndex(e => e.GetAttribute("class").Contains("active"));
    }
}
