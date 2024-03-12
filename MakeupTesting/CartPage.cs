using MakeupTesting;
using MakeupTestingModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
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
        private IWebElement txtQuantityProductInCart => webDriver.FindElement(By.XPath("//input[@name='count[]']")); // atrib value
        private IWebElement txtProductPrice => webDriver.FindElement(By.XPath("//div[@class='product__price']"));
        private IWebElement btnProductIncrease => webDriver.FindElement(By.XPath("//div[@class='product__button-increase']"));
        private IWebElement btnProductDecrease => webDriver.FindElement(By.XPath("//div[@class='product__button-decrease']"));
        private IWebElement txtTotalPriceInOrder => webDriver.FindElement(By.XPath("//div[@class='total']/span"));
        private List<IWebElement> productListInCart => webDriver.FindElements(By.XPath("//div[@class='cart-content-wrapper scrolling']//ul[@class='product-list scrolling']/li")).ToList();
        private IWebElement btnPlaceAnOrder => webDriver.FindElement(By.XPath("//div[text()='Оформити замовлення']"));
        private IWebElement btnContinueShopping => webDriver.FindElement(By.XPath("//span[text()='Продовжити покупки']"));



        public string GetCartProductTitleText()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => titleCartProduct.Displayed);
            return titleCartProduct.Text;
        }
        public void DeleteProduct()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnDelete.Displayed);
            btnDelete.Click();
        }
        public string GetProductsInCart()
        {
            string cartSize = cartProductCounter.Text;
            if (!string.IsNullOrEmpty(cartSize) && int.TryParse(cartSize, out int size) && size > 0)
            {
                return cartSize;
            }
            else
            {
                return "Cart is Empty";
            }
        }

        public string GetQuantityProductsInCart()
        {
            string quantityProduct = txtQuantityProductInCart.GetAttribute("value");
            return quantityProduct;
        }

        public string GetQuantityProductsPriceInCart()
        {
            return txtProductPrice.Text.Replace("&nbsp;₴", "");
        }

        public string GetTotalOrderPriceInCart()
        {
            return txtTotalPriceInOrder.Text.Replace("₴", "").Replace(" ", ""); //.Replace("&nbsp;", "")
        }

        public void IncreaseQuantityProductInOrder()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnProductIncrease.Displayed);
            btnProductIncrease.Click();
        }

        public void DecreaseQuantityProductInOrder()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnProductDecrease.Displayed);
            btnProductDecrease.Click();
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
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnPlaceAnOrder.Displayed);
            btnPlaceAnOrder.Click();
        }
        public void ClickOnbtnContinueShopping()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnContinueShopping.Displayed);
            btnContinueShopping.Click();
        }
    }
}
