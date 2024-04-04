using MakeupTesting;
using MakeupTestingModels;
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

        /// <summary>
        /// Converts the given string representation of a price to a double value, or returns the default value if conversion fails.
        /// </summary>
        /// <param name="price">The string representation of the price to convert.</param>
        /// <param name="defaultValue">The default value to return if conversion fails.</param>
        /// <returns>The converted price as a double value, or the default value if conversion fails.</returns>
        private double ConvertToPriceOrDefault(string price, double defaultValue)
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

        /// <summary>
        /// Provides a comparator for comparing double values, sorting them in ascending order. Zero value is considered greater.
        /// </summary>
        public class ProductComparator : IComparer<double>
        {
            public int Compare(double x, double y)
            {
                if (x == 0) return 1;
                return x.CompareTo(y);
            }
        }

        /// <summary>
        /// Retrieves the number of clicks on the right arrow in the testimonials slider.
        /// </summary>
        /// <returns>The number of clicks on the right arrow in the testimonials slider.</returns>
        public int GetNumberOfClicksOnArrowInSlider()
        {
            return numberOfClicksOnArrow;
        }

        /// <summary>
        /// Clicks on the right arrow in the testimonials slider and increments the count of clicks.
        /// </summary>
        public void ClickRightArrowInTestimonialsSlider()
        {
            IWebElement btnArrowSliderRight = WaitUntilWebElementExists(By.XPath("//div[contains(text(), 'Відгуки про Декоративна косметика')]/following-sibling::*//div[@class='slider-button right']"));
            btnArrowSliderRight.Click();
            numberOfClicksOnArrow++;
        }

        /// <summary>
        /// Clicks on the button to add more products.
        /// </summary>
        public void ClickOnBtnAddMoreProducts() => WaitUntilWebElementExists(By.XPath("//div[text()='Більше товарів']")).Click();

        /// <summary>
        /// Selects the specified product by clicking on its link in the web page.
        /// </summary>
        /// <param name="productAddToCart">The name or title of the product to be selected.</param>
        public void SelectProduct(string productAddToCart) => WaitUntilWebElementExists(By.XPath($"//a[text()='{productAddToCart}']")).Click();

        /// <summary>
        /// Retrieves the title of the specified category.
        /// </summary>
        /// <param name="categoryTitle">The title of the category to retrieve.</param>
        /// <returns>The title of the specified category as a string.</returns>
        public string GetCategoryTitle(string categoryTitle) => WaitUntilWebElementExists(By.XPath($"//span[text()='{categoryTitle}']")).Text;

        /// <summary>
        /// Checks the filters by the name of the brand and the type of product on the page.
        /// </summary>
        /// <param name="nameOfBrand">The name of the brand to filter by.</param>
        /// <param name="productName">The name of the product type to filter by.</param>
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

        /// <summary>
        /// Retrieves a list of tuples containing product names and types from the catalog page.
        /// </summary>
        /// <returns>A list of tuples containing product names and types.</returns>
        public List<(string productName, string productType)> GetProductTitle() => webDriver.FindElements(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']//div[@class='info-product-wrapper']/a"))
                .ToList()
                .ConvertAll(e => (e.Text, e.GetAttribute("data-default-name")));

        /// <summary>
        /// Sets the filter by price range on the catalog page.
        /// </summary>
        /// <param name="priceMin">The minimum price value for the filter.</param>
        /// <param name="priceMax">The maximum price value for the filter.</param>
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

        /// <summary>
        /// Retrieves the titles and prices of products listed on the catalog page.
        /// </summary>
        /// <returns>A dictionary containing product titles as keys and their corresponding prices as values.</returns>
        public Dictionary<string, double> GetProductsTitlesAndPrices()
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

                        productsDetails.Add(title, ConvertToPriceOrDefault(price, 0.0));
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

        /// <summary>
        /// Clicks on the dropdown to select sorting options on the catalog page.
        /// </summary>
        public void SelectDropdownSortBy() => WaitUntilWebElementExists(By.XPath("//div[@class='catalog-sort-wrapper']")).Click();

        /// <summary>
        /// Selects sorting by price option and waits until products are sorted accordingly.
        /// </summary>
        public void SelectSortingByPrice()
        {
            WaitUntilWebElementExists(By.XPath($"//label[contains(text(), 'вартістю')]")).Click();
            WaitUntil(e =>
            {
                List<double> values = GetProductsTitlesAndPrices().Values.ToList();
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

        /// <summary>
        /// Removes applied filters by clicking on the remove filters button.
        /// </summary>
        public void RemoveFilters() => WaitUntilWebElementExists(By.XPath("//div[@class='selected-filter-list__item cancel-filter active']")).Click();

        /// <summary>
        /// Checks if the remove filters button is present and active.
        /// </summary>
        /// <returns>True if the remove filters button is present and active, otherwise false.</returns>
        public bool IsRemoveFiltersButtonPresent() => IsElementExists(By.XPath("//div[@class='selected-filter-list__item cancel-filter active']"));

        /// <summary>
        /// Counts the number of products currently listed on the page.
        /// </summary>
        /// <returns>The number of products listed on the page.</returns>
        public int CountProductsInList() => GetProductsTitlesAndPrices().Count;

        /// <summary>
        /// Retrieves the index of the currently active testimonial.
        /// </summary>
        /// <returns>The index of the active testimonial</returns>
        public int GetIndexOfActiveTestimonialPage() => webDriver
                .FindElements(By.XPath("//div[contains(text(), 'Відгуки про Декоративна косметика')]/following-sibling::*//div[@class='slider-button left']/label"))
                .ToList()
                .FindIndex(e => e.GetAttribute("class").Contains("active"));

        /// <summary>
        /// Retrieves the details of products for serialization purposes.
        /// </summary>
        /// <returns>A list of Product objects containing product details.</returns>
        public List<Product> GetProductsDetailsForSerialization()
        {
            List<Product> products = new List<Product>();

            WaitUntil(e =>
            {
                try
                {
                    List<IWebElement> productList = webDriver.FindElements(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']//div[@class='info-product-wrapper']")).ToList();

                    foreach (var productElement in productList)
                    {
                        string title = productElement.FindElement(By.XPath("./a")).GetAttribute("data-default-name");
                        string priceText;

                        try
                        {
                            priceText = productElement.FindElement(By.XPath(".//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price product-item__price_red']/span[@class='price_item']")).Text;
                        }
                        catch (NoSuchElementException)
                        {
                            try
                            {
                                priceText = productElement.FindElement(By.XPath(".//div[@class='simple-slider-list__price_container']//span[@class='simple-slider-list__price']/span[@class='price_item']")).Text;
                            }
                            catch (NoSuchElementException)
                            {
                                priceText = "0";
                            }
                        }

                        double price = ConvertToPriceOrDefault(priceText, 0.0);

                        products.Add(new Product { Name = title, Price = price });
                    }

                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    products.Clear();
                    return false;
                }
            });

            return products;
        }
    }
}
