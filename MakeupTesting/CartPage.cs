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
        public CartPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement titleCartProduct => webDriver.FindElement(By.XPath("//div[@class='product__header']"));
        private IWebElement btnDelete => webDriver.FindElement(By.XPath("//div[@class='product__button-remove']"));
        private IWebElement cartProductCounter => webDriver.FindElement(By.XPath("//span[contains(@class,'header-counter')]"));
        private IWebElement txtQuantityProductInCart => webDriver.FindElement(By.XPath("//input[@name='count[]']"));
        private IWebElement txtProductPrice => webDriver.FindElement(By.XPath("//div[@class='product__price']"));
        private IWebElement btnProductIncrease => webDriver.FindElement(By.XPath("//div[@class='product__button-increase']"));
        private IWebElement btnProductDecrease => webDriver.FindElement(By.XPath("//div[@class='product__button-decrease']"));
        private IWebElement txtTotalPriceInOrder => webDriver.FindElement(By.XPath("//div[@class='total']/span"));
        private List<IWebElement> productListInCart => webDriver.FindElements(By.XPath("//div[@class='cart-content-wrapper scrolling']//ul[@class='product-list scrolling']/li")).ToList();
        private IWebElement btnPlaceAnOrder => webDriver.FindElement(By.XPath("//div[text()='Оформити замовлення']"));
        private IWebElement btnContinueShopping => webDriver.FindElement(By.XPath("//span[text()='Продовжити покупки']"));
        private IWebElement btnCart => webDriver.FindElement(By.XPath("//div[@class='header-basket empty']"));
        private IWebElement cartWindow => webDriver.FindElement(By.XPath("//div[@class='popup__window']"));


        public string GetCartProductTitleText()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => titleCartProduct.Displayed);
            return titleCartProduct.Text;
        }
        public void DeleteProduct()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            wait.Until(x => cartWindow.Displayed);

            wait.Until(x => btnDelete.Displayed);
            btnDelete.Click();

            wait.Until(x => !cartWindow.Displayed);
        }
        public int GetCartSize()
        {
            string cartSize = cartProductCounter.Text;
            return cartSize == "" ? 0 : int.Parse(cartSize);
        }

        public string GetQuantityProductsInCart()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            wait.Until(x => cartWindow.Displayed);
            
            wait.Until(x => txtQuantityProductInCart.Displayed);
            return txtQuantityProductInCart.GetAttribute("value");
        }

        public string GetQuantityProductsPriceInCart()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            wait.Until(x => cartWindow.Displayed);
            return txtProductPrice.Text.Replace("&nbsp;₴", "");
        }

        public string GetTotalOrderPriceInCart()
        {
            return txtTotalPriceInOrder.Text.Replace("₴", "").Replace(" ", "");
        }

        public void IncreaseQuantityProductInOrder()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            wait.Until(x => cartWindow.Displayed);

            string before = GetQuantityProductsInCart();
            
            wait.Until(x => btnProductIncrease.Displayed);
            btnProductIncrease.Click();

            wait.Until(e => !before.Equals(GetQuantityProductsInCart()));
        }

        public void DecreaseQuantityProductInOrder()
        {
            string before = GetQuantityProductsInCart();

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnProductDecrease.Displayed);
            btnProductDecrease.Click();

            wait.Until(e => !before.Equals(GetQuantityProductsInCart()));
        }

        public double GetProductsPricesSum()
        {
            double sum = 0;
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            foreach (IWebElement actualProduct in productListInCart)
            {
                wait.Until(x => actualProduct.FindElement(By.XPath($".//div[@class='product__price']")).Displayed);
                sum += double.Parse(actualProduct.FindElement(By.XPath($".//div[@class='product__price']")).Text.Replace("&nbsp;₴", "").Replace("₴", ""));
            }
            return sum;
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
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            wait.Until(x => cartWindow.Displayed);

            wait.Until(x => btnPlaceAnOrder.Displayed);
            btnPlaceAnOrder.Click();
        }
        public void ClickOnBtnContinueShopping()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnContinueShopping.Displayed);
            btnContinueShopping.Click();
        }
    }
}
