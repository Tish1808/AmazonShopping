using AmazonShopping.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AmazonShopping.StepDefinitions
{
    [Binding]
    public sealed class AmazonStepDefinitions : Helper
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        IWebElement actualTitle;
        IList<IWebElement> actualType;
        [Given(@"I navigate to (.*)")]
        public void GivenINavigateToHttpsWww_Amazon_Co_Uk(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        [Then(@"I am on (.*)")]
        public void ThenIAmOnHttpsWww_Amazon_Co_Uk(string expectedurl)
        {
            var actualUrl = driver.Url;
            Assert.AreEqual(expectedurl,actualUrl);
        }

        [When(@"I search for '([^']*)'")]
        public void WhenISearchFor(string searchTerm)
        {
            var searchField = driver.FindElement(By.Id("twotabsearchtextbox"));
            searchField.SendKeys(searchTerm + Keys.Enter);
        }

        [Then(@"the page first items has the title '([^']*)'")]
        public void ThenThePageFirstItemsHasTheTitle(string expectedTitle)
        {
            var acceptCookies = 
                driver.FindElement(By.Id("sp-cc-accept"));
            if (!acceptCookies.Displayed)
            {
                wait.Until(x => x.FindElement(By.Id("sp-cc-accept"))).Click();
            }
            else
            {
                acceptCookies.Click();
            }
            actualTitle = 
                driver.FindElement(By.XPath("//a/span[@class='a-size-medium a-color-base a-text-normal']"));
            Assert.IsTrue(actualTitle.Text.Contains(expectedTitle));
        }
        

        [Then(@"the select type is '([^']*)'")]
        public void ThenTheSelectTypeIs(string expectedType)
        {
            Thread.Sleep(2000);
            actualType =
                driver.FindElements(By.XPath("//div[contains(@class, 's-padding-right-small s-title-instructions-style')]"));

            if (actualType.Count >= 1 )
            {
                Assert.IsTrue(actualType[0].Text.Contains(expectedType), "type not as expected");
            }
            else
            {
                driver.Navigate().Refresh();
                Thread.Sleep(2000);
                Assert.IsTrue(actualType[0].Text.Contains(expectedType), "type not as expected");
            }
        }

        [Then(@"the price is not null")]
        public void ThenThePriceIs()
        {
            Assert.NotNull(actualType[1].Text, "price not null as it is dynamic");
        }

        [When(@"I add book to basket")]
        public void WhenIAddBookToBasket()
        {
            var ttitleValue = actualTitle.Text;
            actualTitle.Click();
            var addToBasket = 
                driver.FindElement(By.CssSelector("input[id='add-to-cart-button']"));
            wait.Until(_ => addToBasket.Displayed);
            addToBasket.Click();
            Thread.Sleep(2000);
            wait.Until(_ => _.FindElement(By.XPath("//*[@id='sw-atc-buy-box']//a"))).Click();
        }

        [Then(@"I verify title of book is displayed as '(.*)' and quantity is (.*)")]
        public void ThenIVerifyTitleOfBookIsDisplayedAndQuantityIs(string title,int count)
        {
            var productTitle =
                driver.FindElement(By.XPath("(//span[contains(@class, 'sc-product-title')])[1]/span")).Text;
            var quantity = driver.FindElement(By.XPath("//span[@data-a-class='quantity']")).Text.Split(':')[1];
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(productTitle.Contains(title));
                Assert.AreEqual(1, int.Parse(quantity));
            });
        }
    }
}