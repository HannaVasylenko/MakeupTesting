using MakeupTestingPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

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
            Assert.That(driver.Title, Is.EqualTo(titleMainPageru), "The page is displayed in a different language");
            
            header.SwitchLanguageToUA();
            Assert.That(driver.Title, Is.EqualTo(titleMainPageUA), "The page is displayed in a different language");
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

            Assert.That(initPage.GetSubCategoryTitle(config["subCategoryTitle"]), Is.EqualTo(titleSubCategory), "The titles do not match");
        }

        [Test]
        public void VerifyScrollUpButton()
        {
            InitPage initPage = new InitPage(driver);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            double scrollPositionBeforeClick = Convert.ToDouble(js.ExecuteScript("return window.pageYOffset;"));
            initPage.ClickOnScrollUpArrow();
            double scrollPositionAfterClick = Convert.ToDouble(js.ExecuteScript("return window.pageYOffset;"));

            Assert.That(scrollPositionAfterClick < scrollPositionBeforeClick, Is.True, "The position on the screen did not change after pressing the scroll up button");
        }

        [Test]
        public void SelectDeliveryVariantByCity()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string deliveryCity = config["deliveryCity"];
            
            Header header = new Header(driver);
            header.OpenDeliveryPage();
            DeliveryPage deliveryPage = new DeliveryPage(driver);
            deliveryPage.InputDeliveryCity(deliveryCity);
            deliveryPage.SelectDeliveryCity();
            
            StringAssert.Contains(deliveryCity.ToLower(), deliveryPage.GetDeliveryCity().ToLower(), "Another delivery city is selected");
        }

        [Test]
        public void NavigateToBeautyClub()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string titleBeautyClub = config["linkBeautyClub"];

            Header header = new Header(driver);
            header.SelectBeautyClub();

            Assert.That(driver.Title, Is.EqualTo(titleBeautyClub), "The BeautyClub page is not displayed");
        }

        [Test]
        public void NavigateToYouTube()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string titleYouTube = config["titleYouTube"];

            Footer footer = new Footer(driver);
            footer.SelectYouTube();
            string secondWindow = footer.GetAllWindows().FirstOrDefault(x => x != footer.GetCurrentWindow(), "");
            footer.SwitchToWindow(secondWindow);

            Assert.That(driver.Title, Is.EqualTo(titleYouTube), "Another page is displayed");
        }

        [Test]
        public void CheckDisplayOfTooltip()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string hintText = config["hintText"];

            Header header = new Header(driver);
            Actions actions = new Actions(driver);
            actions.MoveToElement(header.GetHintFeatures()).Perform();

            Assert.That(header.GetHintText(), Is.EqualTo(hintText), "The tooltip is not displayed");
        }

        [Test]
        public void VerifySubscriptionErrorDisplay()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            Footer footer = new Footer(driver);
            footer.InputEmail(config["emailSubscription"]);
            footer.ClickBtnEmailSubscription();

            Assert.That(footer.IsEmailSubscriptionErrorDisplayed(), Is.True, "The email subscription error is not displayed or is not invalid.");
        }
    }
}
