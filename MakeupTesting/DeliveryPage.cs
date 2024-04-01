using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{
    public class DeliveryPage : BasePage
    {
        public DeliveryPage(IWebDriver driver) : base(driver) {}

        public void SelectFirstDeliveryCity() => WaitUntilWebElementExists(By.XPath("//div[@class='animated-input-group']//div[@class='search-value__container']//ul[@class='search-value__list scrolling expanded']//li[1]")).Click();

        public string GetDeliveryCityText() => WaitUntilWebElementExists(By.XPath("//input[@id='city-id-selected']")).GetAttribute("title");

        public void InputDeliveryCity(string text)
        {
            IWebElement txtSelectCity = WaitUntilWebElementExists(By.XPath("//input[@id='select-city']"));
            txtSelectCity.Click();
            txtSelectCity.SendKeys(text);
        }
    }
}
