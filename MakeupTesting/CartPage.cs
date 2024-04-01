using MakeupTesting;
using MakeupTestingModels;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{
    public class CartPage : BasePage
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        private enum WebElementState
        {
            OPENED,
            CLOSED
        }
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

        public string GetCartProductTitleText()
        {
            WaitCartWindow(WebElementState.OPENED);
            return WaitUntilWebElementExists(By.XPath("//div[@class='product__header']")).Text;
        }

        public void DeleteProduct()
        {
            WaitCartWindow(WebElementState.OPENED);
            WaitUntilWebElementExists(By.XPath("//div[@class='product__button-remove']")).Click();
            WaitCartWindow(WebElementState.CLOSED);
        }

        public int GetCartSize()
        {
            string cartSize = WaitUntilWebElementExists(By.XPath("//span[contains(@class,'header-counter')]")).Text;
            return cartSize == "" ? 0 : int.Parse(cartSize);
        }

        public string GetQuantityProductsInCart()
        {
            WaitCartWindow(WebElementState.OPENED); 
            return WaitUntilWebElementExists(By.XPath("//input[@name='count[]']")).GetAttribute("value");
        }

        public string GetQuantityProductsPriceInCart() => webDriver.FindElement(By.XPath("//div[@class='product__price']"))
                .Text
                .Replace("&nbsp;₴", "");

        public string GetTotalOrderPriceInCart() => WaitUntilWebElementExists(By.XPath("//div[@class='total']/span"))
                .Text
                .Replace("₴", "")
                .Replace(" ", "");

        public void IncreaseQuantityProductInOrder()
        {
            WaitCartWindow(WebElementState.OPENED);
            string before = GetQuantityProductsInCart();
            WaitUntilWebElementExists(By.XPath("//div[@class='product__button-increase']")).Click();
            WaitUntil(e => !before.Equals(GetQuantityProductsInCart()));
        }

        public void DecreaseQuantityProductInOrder()
        {
            string before = GetQuantityProductsInCart();
            WaitUntilWebElementExists(By.XPath("//div[@class='product__button-decrease']")).Click();
            WaitUntil(e => !before.Equals(GetQuantityProductsInCart()));
        }

        public double GetProductsPricesSum() => webDriver.FindElements(By.XPath("//div[@class='cart-content-wrapper scrolling']//ul[@class='product-list scrolling']/li//div[@class='product__price']"))
                .Sum(e => double.Parse(e.Text.Replace("&nbsp;₴", "").Replace("₴", "")));

        public List<Product> GetCartProductsDetails()
        {
            List<IWebElement> productListInCart = webDriver.FindElements(By.XPath("//div[@class='cart-content-wrapper scrolling']//ul[@class='product-list scrolling']/li")).ToList();
            List<Product> products = new List<Product>();

            foreach (IWebElement actualProduct in productListInCart)
            {
                Product product = new Product();
                product.Name = actualProduct.FindElement(By.XPath(".//div[@class='product__header']")).Text;
                product.Price = double.Parse(actualProduct.FindElement(By.XPath($".//div[@class='product__price']")).Text.Replace("&nbsp;₴", ""));
                products.Add(product);
            }
            return products;
        }

        public void ClickOnPlaceAnOrderBtn()
        {
            WaitCartWindow(WebElementState.OPENED);
            WaitUntilWebElementExists(By.XPath("//div[text()='Оформити замовлення']")).Click();
        }

        public void ClickOnBtnContinueShopping() => WaitUntilWebElementExists(By.XPath("//span[text()='Продовжити покупки']")).Click();
    }
}
