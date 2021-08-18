using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Test_Framework.Tests
{
    class UI
    {
        /// <summary>
        /// Scrolls to Element
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        /// <returns>The element</returns>
        public static IWebElement ScrollToElement(IWebDriver driver, IWebElement element)
        {
            var js = (IJavaScriptExecutor)driver;
            try
            {
                    var elem = element;
                if (elem.Location.Y > 200)
                {
                    js.ExecuteScript($"window.scrollTo({0}, {element.Location.Y - 200 })");
                }
                return element;
            }
                catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets text
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetText(IWebElement element)
        {
            for (int i = 0; i < 5; i++)
            try
            {
                return element.Text;
            }

            catch (StaleElementReferenceException) { }
            return null;
        }

        /// <summary>
        /// Focuses on the element
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        public static void FocusElement(IWebDriver driver, By locator)
        {

            UI.WaitForElementVisible(driver, locator);

            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(locator)).Perform();
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Clicks an Element
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        public static void ClickElement(IWebDriver driver, By locator)
        {
            new Actions(driver).MoveToElement(driver.FindElement(locator)).Perform();
            Exception lastException = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    WaitForElementVisible(driver, locator);
                    driver.FindElement(locator).Click();
                    break;
                }
                catch (Exception e)
                {
                    lastException = e;
                }
            }

            if (lastException is WebDriverTimeoutException)
            {
                throw lastException;
            }
        }

        /// <summary>
        /// Waits for page to load
        /// </summary>
        /// <param name="drv"></param>
        public static void WaitForPageToLoad(IWebDriver drv)
        {
            IJavaScriptExecutor jsExec = (IJavaScriptExecutor)drv;
            WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(drv, Configuration.DefaultElementStatusCheckTimeout);

            bool jQueryDefined = (bool)jsExec.ExecuteScript("return typeof jQuery != 'undefined'");
            if (jQueryDefined)
            {
                Thread.Sleep(Configuration.DefaultPageLoadCheckStabilizationTimeout);

                wait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return (typeof jQuery !== 'undefined') && jQuery.active == 0"));

                wait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return document.readyState").ToString().Equals("complete"));

                Thread.Sleep(Configuration.DefaultPageLoadCheckStabilizationTimeout);
            }
        }

        /// <summary>
        /// Waits for element to be visible
        /// </summary>
        /// <param name="drv"></param>
        /// <param name="by"></param>
        public static void WaitForElementVisible(IWebDriver drv, By by)
        {
            WebDriverWait wait = new WebDriverWait(drv, Configuration.DefaultElementStatusCheckTimeout);
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }

        /// <summary>
        /// Creates a list of strings
        /// </summary>
        /// <param name="elements"></param>
        /// <returns>Returns list of strings</returns>
        public IList<string> GetItemsToList(IReadOnlyCollection<IWebElement> elements)
        {
            return elements.Select(item => item.Text).ToList();
        }
    }
}
