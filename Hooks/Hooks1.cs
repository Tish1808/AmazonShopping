using AmazonShopping.Drivers;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace AmazonShopping.Hooks
{
    [Binding]
    public sealed class Hooks1 : Helper
    {
        [BeforeScenario]
        public void FirstBeforeScenario()
        {
            var option = new ChromeOptions();
            option.AddArguments("start-maximized", "incognito");
            driver = new ChromeDriver(option);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(15);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            driver = null;
        }
    }
}