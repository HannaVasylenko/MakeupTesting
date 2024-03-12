using MakeupTestingPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Legacy;
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
            
            string productCartTitle = config["productAddToCart1"];
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetics.SelectProductCard(config["productAddToCart1"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            CartPage cartPage = new CartPage(driver);

            ClassicAssert.AreEqual(productCartTitle, cartPage.GetCartProductTitleText(), "Search title text do not match");
        }

        [Test]
        public void VerifyDeleteProductFromCart()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetics.SelectProductCard(config["productAddToCart1"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            CartPage cartPage = new CartPage(driver);
            cartPage.DeleteProduct();
            Thread.Sleep(3000);

            ClassicAssert.AreEqual("Cart is Empty", cartPage.GetProductsInCart(), "Cart is not empty");
        }

        [Test]
        public void VerifyIncreaseQuantityProductInOrder()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetics.SelectProductCard(config["productAddToCart2"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            CartPage cartPage = new CartPage(driver);
            Thread.Sleep(3000);
            int currentQuantity = int.Parse(cartPage.GetQuantityProductsInCart());
            int expectedQuantity = currentQuantity + 1;
            cartPage.IncreaseQuantityProductInOrder();
            Thread.Sleep(3000);

            ClassicAssert.AreEqual(expectedQuantity.ToString(), cartPage.GetQuantityProductsInCart(), "The quantity of the product in the cart did not increase by 1 after clicking the button to increase the quantity");
        }

        [Test]
        public void VerifyDecreaseQuantityProductInOrder()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetics.SelectProductCard(config["productAddToCart2"]);
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

        [Test]
        public void CalculateTotalProductPriceInOrder()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetics.SelectProductCard(config["productAddToCart1"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            driver.Navigate().Back();
            decorativeCosmetics.SelectProductCard(config["productAddToCart2"]);
            productPage.AddProductToCart();
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
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetics.SelectProductCard(config["productAddToCart1"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            CartPage cartPage = new CartPage(driver);
            cartPage.ClickOnPlaceAnOrderBtn();
            cartPage.ClickOnbtnContinueShopping();

            ClassicAssert.AreEqual(titleMainPageUA, driver.Title, "The titles do not match");
        }
    }
}
