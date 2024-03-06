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
            
            InitPage initPage = new InitPage(driver);
            
            initPage.SwitchLanguageToru();
            ClassicAssert.AreEqual(driver.Title, titleMainPageru, "The page is displayed in a different language");
            
            initPage.SwitchLanguageToUA();
            ClassicAssert.AreEqual(driver.Title, titleMainPageUA, "The page is displayed in a different language");
        }

        [Test]
        public void SelectSubCategory()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            string titleSubCategory = config["titleSubCategory"];
            InitPage initPage = new InitPage(driver);
            Actions actions = new Actions(driver);
            actions.MoveToElement(initPage.GetDecorativeСosmeticsElement(config["category"])).Perform();
            initPage.SelectSubCategory(config["subCategory"]);
            string titleText = initPage.GetSubCategoryTitleText(config["subCategoryTitle"]);
            ClassicAssert.AreEqual(titleSubCategory, titleText, "The titles do not match");
        }

        [Test]
        public void VerifyScrollUpButton()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            InitPage initPage = new InitPage(driver);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            double scrollPositionBeforeClick = Convert.ToDouble(js.ExecuteScript("return window.pageYOffset;"));
            initPage.ScrollUp();
            double scrollPositionAfterClick = Convert.ToDouble(js.ExecuteScript("return window.pageYOffset;"));
            ClassicAssert.IsTrue(scrollPositionAfterClick < scrollPositionBeforeClick, "The position on the screen did not change after pressing the scroll up button.");
        }
    }
}
