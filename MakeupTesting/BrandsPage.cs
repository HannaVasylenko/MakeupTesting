using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{
    public class BrandsPage : BasePage
    {
        public BrandsPage(IWebDriver driver) : base(driver) {}
        public void SelectBrandVariant(string brandVariant) => WaitUntilWebElementExists(By.XPath($"//li[contains(text(), '{brandVariant}')]")).Click();

        public List<string> GetBrandText() => webDriver.FindElements(By.XPath("//div[@class='brands__column active']/ul[@class='brands__list']/li/a"))
                    .ToList()
                    .ConvertAll(e => e.Text);
    }
}
