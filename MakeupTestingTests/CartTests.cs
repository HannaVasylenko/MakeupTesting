using MakeupTesting;
using MakeupTestingPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingTests
{
    public class CartTests : BaseTest
    {
        [Test]
        public void VerifyAddProductToCart()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            
            string productCard = config["productAddToCart1"];
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetic.SelectProductCard(config["productAddToCart1"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            CartPage cartPage = new CartPage(driver);

            ClassicAssert.AreEqual(productCard, cartPage.GetCartProductTitleText(), "Product name does not match the selected one");
        }

        [Test]
        public void VerifyDeleteProductFromCart()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetic.SelectProductCard(config["productAddToCart1"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            CartPage cartPage = new CartPage(driver);
            cartPage.DeleteProduct();

            ClassicAssert.AreEqual(0, cartPage.GetCartSize(), "Cart is not empty");
        }

        // 1 complete remove Thread.Sleep(3000)
        [Test]
        public void VerifyIncreaseQuantityProductInOrder()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetic.SelectProductCard(config["productAddToCart4"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            CartPage cartPage = new CartPage(driver);
            int currentQuantity = int.Parse(cartPage.GetQuantityProductsInCart());
            int expectedQuantity = currentQuantity + 1;
            cartPage.IncreaseQuantityProductInOrder();
            //Thread.Sleep(3000);

            ClassicAssert.AreEqual(expectedQuantity.ToString(), cartPage.GetQuantityProductsInCart(), "The quantity of the product in the cart did not increase by 1 after clicking the button to increase the quantity");
        }

        // 2 complete remove Thread.Sleep(3000)
        [Test]
        public void VerifyDecreaseQuantityProductInOrder()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetic.SelectProductCard(config["productAddToCart4"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            CartPage cartPage = new CartPage(driver);
            cartPage.IncreaseQuantityProductInOrder();
            Thread.Sleep(3000);
            int currentQuantity = int.Parse(cartPage.GetQuantityProductsInCart());
            int expectedQuantity = currentQuantity - 1;
            cartPage.DecreaseQuantityProductInOrder();
            Thread.Sleep(3000);

            ClassicAssert.AreEqual(expectedQuantity.ToString(), cartPage.GetQuantityProductsInCart(), "The quantity of the product in the cart did not decrease by 1 after clicking the button to decrease the quantity");
        }

        // 3 complete remove Thread.Sleep(3000)
        [Test]
        public void CalculateTotalProductPriceInOrder()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetic.SelectProductCard(config["productAddToCart1"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            Thread.Sleep(3000);
            driver.Navigate().Back();
            Thread.Sleep(3000);
            decorativeCosmetic.SelectProductCard(config["productAddToCart2"]);
            Thread.Sleep(3000);
            productPage.AddProductToCart();
            Thread.Sleep(3000);
            CartPage cartPage = new CartPage(driver);

            ClassicAssert.AreEqual(cartPage.GetTotalOrderPriceInCart(), cartPage.GetProductsPricesSum().ToString(), "The total cost of the order does not equal the sum of the cost of the products");
        }

        [Test]
        public void VerifyPossibilityOfPostponingOrder()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string titleMainPageUA = config["titleMainPageUA"];

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetic.SelectProductCard(config["productAddToCart1"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            CartPage cartPage = new CartPage(driver);
            cartPage.ClickOnPlaceAnOrderBtn();
            cartPage.ClickOnbtnContinueShopping();

            ClassicAssert.AreEqual(titleMainPageUA, driver.Title, "The titles do not match");
        }
    }
}
