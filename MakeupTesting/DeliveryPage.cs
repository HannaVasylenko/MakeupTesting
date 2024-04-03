using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{   /// <summary>
    /// The class displays a separate Delivery page
    /// </summary>
    public class DeliveryPage : BasePage
    {
        public DeliveryPage(IWebDriver driver) : base(driver) {}

        /// <summary>
        /// Selects the delivery city from the dropdown list.
        /// </summary>
        public void SelectDeliveryCity() => WaitUntilWebElementExists(By.XPath("//div[@class='animated-input-group']//div[@class='search-value__container']//ul[@class='search-value__list scrolling expanded']//li[1]")).Click();

        /// <summary>
        /// Retrieves the currently selected delivery city.
        /// </summary>
        /// <returns>The name of the currently selected delivery city.</returns>
        public string GetDeliveryCity() => WaitUntilWebElementExists(By.XPath("//input[@id='city-id-selected']")).GetAttribute("title");

        /// <summary>
        /// Enters the specified text as the delivery city.
        /// </summary>
        /// <param name="text">The text representing the delivery city to be entered.</param>
        public void InputDeliveryCity(string text)
        {
            IWebElement txtSelectCity = WaitUntilWebElementExists(By.XPath("//input[@id='select-city']"));
            txtSelectCity.Click();
            txtSelectCity.SendKeys(text);
        }
    }
}
