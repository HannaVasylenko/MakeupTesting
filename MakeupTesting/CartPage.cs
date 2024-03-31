using MakeupTesting;
using MakeupTestingModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    public class CartPage : BasePage
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        private List<IWebElement> productListInCart => webDriver.FindElements(By.XPath("//div[@class='cart-content-wrapper scrolling']//ul[@class='product-list scrolling']/li")).ToList();
     
        public enum WebElementState
        {
            OPENED,
            CLOSED
        }
        public void WaitCartWindow(WebElementState state)
        {
            IWebElement cartWindow = webDriver.FindElement(By.XPath("//div[@class='popup__window']"));
            switch (state)
            {
                case WebElementState.OPENED:
                    WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
                    wait.Until(x => cartWindow.Displayed);
                    break;
                case WebElementState.CLOSED:
                    WebDriverWait waits = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
                    waits.Until(x => !cartWindow.Displayed);
                    break;
            }
        }

        public string GetCartProductTitleText()
        {
            WaitCartWindow(WebElementState.OPENED);

            IWebElement titleCartProduct = webDriver.FindElement(By.XPath("//div[@class='product__header']"));

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => titleCartProduct.Displayed);
            return titleCartProduct.Text;
        }

        public void DeleteProduct()
        {
            IWebElement btnDelete = webDriver.FindElement(By.XPath("//div[@class='product__button-remove']"));
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            wait.Until(x => btnDelete.Displayed);
            btnDelete.Click();

        }

        public int GetCartSize()
        {
            string cartSize = webDriver.FindElement(By.XPath("//span[contains(@class,'header-counter')]")).Text;
            return cartSize == "" ? 0 : int.Parse(cartSize);
        }

        public string GetQuantityProductsInCart()
        {
            WaitCartWindow(WebElementState.OPENED);
            IWebElement txtQuantityProductInCart = webDriver.FindElement(By.XPath("//input[@name='count[]']"));
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            wait.Until(x => txtQuantityProductInCart.Displayed);
            return txtQuantityProductInCart.GetAttribute("value");
        }

        public string GetQuantityProductsPriceInCart()
        {
            return webDriver.FindElement(By.XPath("//div[@class='product__price']"))
                .Text
                .Replace("&nbsp;₴", "");
        }

        public string GetTotalOrderPriceInCart()
        {
            return webDriver.FindElement(By.XPath("//div[@class='total']/span"))
                .Text
                .Replace("₴", "")
                .Replace(" ", "");
        }

        public void IncreaseQuantityProductInOrder()
        {
            WaitCartWindow(WebElementState.OPENED);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));

            string before = GetQuantityProductsInCart();
            IWebElement btnProductIncrease = webDriver.FindElement(By.XPath("//div[@class='product__button-increase']"));
            wait.Until(x => btnProductIncrease.Displayed);
            
            btnProductIncrease.Click();

            wait.Until(e => !before.Equals(GetQuantityProductsInCart()));
        }

        public void DecreaseQuantityProductInOrder()
        {
            string before = GetQuantityProductsInCart();
            IWebElement btnProductDecrease = webDriver.FindElement(By.XPath("//div[@class='product__button-decrease']"));
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnProductDecrease.Displayed);
            btnProductDecrease.Click();

            wait.Until(e => !before.Equals(GetQuantityProductsInCart()));
        }

        public double GetProductsPricesSum()
        {
            return webDriver.FindElements(
                By.XPath("//div[@class='cart-content-wrapper scrolling']//ul[@class='product-list scrolling']/li//div[@class='product__price']"))
                .Sum(e => double.Parse(e.Text.Replace("&nbsp;₴", "").Replace("₴", "")));
        }

        public List<Product> GetCartProductsDetails()
        {
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
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            IWebElement btnPlaceAnOrder = webDriver.FindElement(By.XPath("//div[text()='Оформити замовлення']"));
            wait.Until(x => btnPlaceAnOrder.Displayed);
            btnPlaceAnOrder.Click();
        }

        public void ClickOnBtnContinueShopping()
        {
            IWebElement btnContinueShopping = webDriver.FindElement(By.XPath("//span[text()='Продовжити покупки']"));
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnContinueShopping.Displayed);
            btnContinueShopping.Click();
        }
    }
}
