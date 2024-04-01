using MakeupTestingPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Legacy;

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

            ClassicAssert.AreEqual(expectedQuantity.ToString(), cartPage.GetQuantityProductsInCart(), "The quantity of the product in the cart did not increase by 1 after clicking the button to increase the quantity");
        }

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
            int currentQuantity = int.Parse(cartPage.GetQuantityProductsInCart());
            int expectedQuantity = currentQuantity - 1;
            cartPage.DecreaseQuantityProductInOrder();

            ClassicAssert.AreEqual(expectedQuantity.ToString(), cartPage.GetQuantityProductsInCart(), "The quantity of the product in the cart did not decrease by 1 after clicking the button to decrease the quantity");
        }

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
            driver.Navigate().Back();
            decorativeCosmetic.SelectProductCard(config["productAddToCart2"]);
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
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetic.SelectProductCard(config["productAddToCart1"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.AddProductToCart();
            CartPage cartPage = new CartPage(driver);
            cartPage.ClickOnPlaceAnOrderBtn();
            cartPage.ClickOnBtnContinueShopping();

            ClassicAssert.AreEqual(titleMainPageUA, driver.Title, "The titles do not match");
        }
    }
}
