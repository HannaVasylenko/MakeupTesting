using MakeupTestingPageObjects;
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
            string titleMainPageru = "MAKEUP - интернет-магазин косметики и парфюмерии №1";
            string titleMainPageUA = "MAKEUP - інтернет-магазин косметики та парфумерії №1";
            
            InitPage initPage = new InitPage(driver);
            
            initPage.SwitchLanguageToru();
            ClassicAssert.AreEqual(driver.Title, titleMainPageru, "The page is displayed in a different language");
            
            initPage.SwitchLanguageToUA();
            ClassicAssert.AreEqual(driver.Title, titleMainPageUA, "The page is displayed in a different language");
        }

        [Test]
        public void SelectSubCategory()
        {
            string title = "Косметика для очей";
            InitPage initPage = new InitPage(driver);
            Actions actions = new Actions(driver);
            actions.MoveToElement(initPage.GetDecorativeСosmeticsElement()).Perform();
            initPage.SelectSubCategory();
            string titleText = initPage.GetSubCategoryTitleText();
            ClassicAssert.AreEqual(title, titleText, "The titles do not match");
        }
    }
}
