﻿using MakeupTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    public class ProductPage : BasePage
    {
        public ProductPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement btnBuy => webDriver.FindElement(By.XPath("//div[@class='button buy']"));
        private IWebElement titleProductVariant(string productVariant) => webDriver.FindElement(By.XPath($"//label[@class='product-variant-selected']//span[contains(text(),'{productVariant}')]"));

        private IWebElement ddProductVariants => webDriver.FindElement(By.XPath("//div[@class='select']"));

        private List<IWebElement> productVariantsList => webDriver.FindElements(By.XPath("//div[@class='variants scrolling full-width']/div")).ToList();
        
        private List<IWebElement> productImagesList => webDriver.FindElements(By.XPath("//div[@id='product-image']//ul[@class='simple-slider-list']/li/label")).ToList();
        private IWebElement imgVariant(int imgNumber) => webDriver.FindElement(By.XPath($"//div[@id='product-image']//ul[@class='simple-slider-list']/li/label[contains(@for, 'product-slider_id_{imgNumber}')]"));

        public string GetProductVariantText(string productVariant) => titleProductVariant(productVariant).Text;
        private IWebElement linkProductBreadCrumbs(string linkVariant) => webDriver.FindElement(By.XPath($"//div[@class='bread-crumbs']//span[contains(text(), '{linkVariant}')]")); // Туш для вій

        public void AddProductToCart()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            //WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            //wait.Until(x => btnBuy.Displayed);
            btnBuy.Click();
        }
       
        public void SelectImage(int imgNumber)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => imgVariant(imgNumber).Displayed);
            imgVariant(imgNumber).Click();
        }

        public void ClickBreadCrumbs(string linkVariant)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => linkProductBreadCrumbs(linkVariant).Displayed);
            linkProductBreadCrumbs(linkVariant).Click();
        }

        public void SelectProductVariants(string variantText)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => ddProductVariants.Displayed);
            ddProductVariants.Click();

            foreach (var product in productVariantsList)
            {
                string productTitle = product.FindElement(By.XPath("./span")).Text;
                if (productTitle.Contains(variantText))
                {
                    product.Click();
                    break;
                }
            }
        }

        public int GetActiveProductImageIndex()
        {
            for (int i = 0; i < productImagesList.Count; i++)
            {
                if (productImagesList[i].GetAttribute("class").Contains("active"))
                {
                    return i + 1;
                }
            }
            return -1;
        }
    }
}
