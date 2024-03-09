using MakeupTesting;
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
        private IWebElement cartProductCounter => webDriver.FindElement(By.XPath("//div[@class='product__button-remove']"));


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
    }
}
