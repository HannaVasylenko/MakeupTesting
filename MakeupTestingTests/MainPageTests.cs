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
    public class MainPageTests : BaseTest
    {
        [Test]
        public void ChangePageLanguage()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string titleMainPageru = config["titleMainPageru"];
            string titleMainPageUA = config["titleMainPageUA"];
            
            Header header = new Header(driver);
            header.SwitchLanguageToru();
            ClassicAssert.AreEqual(driver.Title, titleMainPageru, "The page is displayed in a different language");
            
            header.SwitchLanguageToUA();
            ClassicAssert.AreEqual(driver.Title, titleMainPageUA, "The page is displayed in a different language");
        }

        [Test]
        public void SelectSubCategory()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string titleSubCategory = config["titleSubCategory"];
            
            Header header = new Header(driver);
            InitPage initPage = new InitPage(driver);
            Actions actions = new Actions(driver);
            actions.MoveToElement(header.GetDecorativeСosmeticsElement(config["category"])).Build().Perform();
            initPage.SelectSubCategory(config["subCategory"]);
            
            ClassicAssert.AreEqual(titleSubCategory, initPage.GetSubCategoryTitleText(config["subCategoryTitle"]), "The titles do not match");
        }

        [Test]
        public void VerifyScrollUpButton()
        {
            InitPage initPage = new InitPage(driver);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            double scrollPositionBeforeClick = Convert.ToDouble(js.ExecuteScript("return window.pageYOffset;"));
            initPage.ScrollUp();
            double scrollPositionAfterClick = Convert.ToDouble(js.ExecuteScript("return window.pageYOffset;"));
            
            ClassicAssert.IsTrue(scrollPositionAfterClick < scrollPositionBeforeClick, "The position on the screen did not change after pressing the scroll up button.");
        }

        [Test]
        public void SelectDeliveryVariantByCity()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string deliveryCity = config["deliveryCity"];
            
            Header header = new Header(driver);
            header.OpenDeliveryPage();
            DeliveryPage deliveryPage = new DeliveryPage(driver);
            deliveryPage.SelectDeliveryCity();
            deliveryPage.InputDeliveryCity(deliveryCity);
            deliveryPage.SelectFirstDeliveryCity();
            
            StringAssert.Contains(deliveryCity.ToLower(), deliveryPage.GetDeliveryCityText().ToLower(), "Another delivery city is selected");
        }

        [Test]
        public void NavigateToBeautyClub()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string titleBeautyClub = config["linkBeautyClub"];

            Header header = new Header(driver);
            header.SelectBeautyClub();
            
            ClassicAssert.AreEqual(titleBeautyClub, driver.Title, "The BeautyClub page is not displayed");
        }

        [Test]
        public void NavigateToYouTube()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string titleYouTube = config["titleYouTube"];

            Footer footer = new Footer(driver);
            string mainPageHandle = driver.CurrentWindowHandle;
            footer.SelectYouTube();
            var allWindowHandles = driver.WindowHandles.ToList();
            string secondWindow = allWindowHandles.Where(x => x != mainPageHandle).Select(x => x).FirstOrDefault();
            driver.SwitchTo().Window(secondWindow);

            ClassicAssert.AreEqual(titleYouTube, driver.Title, "Another page is displayed");
        }

        [Test]
        public void CheckDisplayOfTooltip()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string hintText = config["hintText"];

            Header header = new Header(driver);
            Actions actions = new Actions(driver);
            actions.MoveToElement(header.GetHintFeatures()).Perform();

            ClassicAssert.AreEqual(hintText, header.GetHintText(), "The tooltip is not displayed");
        }

        [Test]
        public void VerifySubscriptionErrorDisplay()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            Footer footer = new Footer(driver);
            footer.InputEmail(config["emailSubscription"]);
            footer.ClickBtnEmailSubscription();

            ClassicAssert.IsTrue(footer.IsEmailSubscriptionErrorDisplayed(), "The email subscription error is not displayed or is not invalid.");
        }
    }
}
