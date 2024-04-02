using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{
    public class Header : BasePage
    {
        public Header(IWebDriver driver) : base(driver) {}

        public void SelectBeautyClub() => WaitUntilWebElementExists(By.XPath("//a[@class='header-top-list__link bc-about-link']")).Click();

        public void SelectBrandsPage() => WaitUntilWebElementExists(By.XPath("//a[text()='Бренди']")).Click();

        public void OpenDeliveryPage() => WaitUntilWebElementExists(By.XPath("//li[@class='header-top-list__item']/a[text()='Доставка та Оплата']")).Click();

        public void SwitchLanguageToru() => WaitUntilWebElementExists(By.XPath("//header//a[text()='Рус']")).Click();

        public void SwitchLanguageToUA() => WaitUntilWebElementExists(By.XPath("//header//a[text()='Укр']")).Click();

        public void SelectCategory(string category) => GetDecorativeСosmeticsElement(category).Click();

        public void ClickOnSearchField() => WaitUntilWebElementExists(By.XPath("//div[@data-popup-handler='search']")).Click();

        public IWebElement GetDecorativeСosmeticsElement(string category) => WaitUntilWebElementExists(By.XPath($"//a[text()='{category}']"));
        
        public void InputProductName(string text)
        {
            IWebElement txtSearch = WaitUntilWebElementExists(By.XPath("//input[@itemprop='query-input']"));
            txtSearch.SendKeys(text);
            txtSearch.SendKeys(Keys.Enter);
        }
        
        public IWebElement GetHintFeatures() => WaitUntilWebElementExists(By.XPath("//span[text()='Безкоштовна доставка по Україні!']"));
        
        public string GetHintText() => WaitUntilWebElementExists(By.XPath("//span[text()='Безкоштовна доставка по Україні!']/parent::*")).GetAttribute("data-text");
    }
}
