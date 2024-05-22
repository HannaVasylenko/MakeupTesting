using MakeupTesting;
using MakeupTestingModels;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{   /// <summary>
    /// The class displays a separate Cart page
    /// </summary>
    public class CartPage : BasePage
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        private enum WebElementState
        {
            OPENED,
            CLOSED
        }

        /// <summary>
        /// Waits for the Cart window to reach the specified state.
        /// </summary>
        /// <param name="state">The desired state of the Cart window (OPENED or CLOSED).</param>
        private void WaitCartWindow(WebElementState state)
        {
            IWebElement cartWindow = webDriver.FindElement(By.XPath("//div[@class='popup__window']"));
            switch (state)
            {
                case WebElementState.OPENED:
                    WaitUntil(x => cartWindow.Displayed);
                    break;
                case WebElementState.CLOSED:
                    WaitUntil(x => !cartWindow.Displayed);
                    break;
            }
        }

        /// <summary>
        /// Retrieves the title of the product displayed in the Cart.
        /// </summary>
        /// <returns>The title of the product in the Cart.</returns>
        public string GetProductTitleInCart()
        {
            WaitCartWindow(WebElementState.OPENED);
            return WaitUntilWebElementExists(By.XPath("//div[@class='product__header']")).Text;
        }

        /// <summary>
        /// Deletes the currently selected product from the Cart.
        /// </summary>
        public void DeleteProductFromCart()
        {
            WaitCartWindow(WebElementState.OPENED);
            WaitUntilWebElementExists(By.XPath("//div[@class='product__button-remove']")).Click();
            WaitCartWindow(WebElementState.CLOSED);
        }

        /// <summary>
        /// Retrieves the number of items currently in the Cart.
        /// </summary>
        /// <returns>The number of items in the shopping cart.</returns>
        public int GetCartSize()
        {
            string cartSize = WaitUntilWebElementExists(By.XPath("//span[contains(@class,'header-counter')]")).Text;
            return cartSize == "" ? 0 : int.Parse(cartSize);
        }

        /// <summary>
        /// Retrieves the quantity of the selected product in the Cart.
        /// </summary>
        /// <returns>The quantity of the selected product in the shopping cart.</returns>
        public string GetQuantityProductsInCart()
        {
            WaitCartWindow(WebElementState.OPENED); 
            return WaitUntilWebElementExists(By.XPath("//input[@name='count[]']")).GetAttribute("value");
        }

        /// <summary>
        /// Retrieves the total price of the order in the Cart, removes currency symbol and spaces, and returns it as a string.
        /// </summary>
        /// <returns>A string representing the total order price.</returns>
        public string GetTotalOrderPriceInCart() => WaitUntilWebElementExists(By.XPath("//div[@class='total']/span"))
                .Text
                .Replace("₴", "")
                .Replace(" ", "");

        /// <summary>
        /// Increases the quantity of a product in the order within the Cart.
        /// </summary>
        public void IncreaseQuantityProductInOrder()
        {
            WaitCartWindow(WebElementState.OPENED);
            string before = GetQuantityProductsInCart();
            WaitUntilWebElementExists(By.XPath("//div[@class='product__button-increase']")).Click();
            WaitUntil(e => !before.Equals(GetQuantityProductsInCart()));
        }

        /// <summary>
        /// Decreases the quantity of a product in the order within the Cart.
        /// </summary>
        public void DecreaseQuantityProductInOrder()
        {
            string before = GetQuantityProductsInCart();
            WaitUntilWebElementExists(By.XPath("//div[@class='product__button-decrease']")).Click();
            WaitUntil(e => !before.Equals(GetQuantityProductsInCart()));
        }

        /// <summary>
        /// Calculates and retrieves the sum of prices of all products in the Cart.
        /// </summary>
        /// <returns>The total sum of product prices in the Cart.</returns>
        public double GetProductsPricesSumInCart()
        {
            WaitCartWindow(WebElementState.OPENED);
            return webDriver.FindElements(By.XPath("//div[@class='cart-content-wrapper scrolling']//ul[@class='product-list scrolling']/li//div[@class='product__price']"))
                .Sum(e => double.Parse(e.Text.Replace("&nbsp;₴", "").Replace("₴", "")));
        }

        /// <summary>
        /// Clicks on the "Place an order" button within the Cart window.
        /// </summary>
        public void ClickOnPlaceAnOrderBtn()
        {
            WaitCartWindow(WebElementState.OPENED);
            WaitUntilWebElementExists(By.XPath("//div[text()='Оформити замовлення']")).Click();
        }

        /// <summary>
        /// Clicks on the "Continue shopping" button within the Cart window.
        /// </summary>
        public void ClickOnBtnContinueShopping() => WaitUntilWebElementExists(By.XPath("//span[text()='Продовжити покупки']")).Click();
    }
}
