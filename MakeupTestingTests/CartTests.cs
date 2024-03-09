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
            string productCartTitle = config["productAddToCart"];

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetics.SelectProductCard(config["productAddToCart"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            CartPage cartPage = new CartPage(driver);
            StringAssert.Contains(productCartTitle, cartPage.GetCartProductTitleText(), "Search title text do not match");
        }

        [Test]
        public void VerifyDeleteProductFromCart()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            Header header = new Header(driver);

            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetics.SelectProductCard(config["productAddToCart"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            CartPage cartPage = new CartPage(driver);
            cartPage.DeleteProduct();
            StringAssert.Contains("Cart is Empty", cartPage.GetProductsInCart(), "Cart is not empty");
        }
    }
}
