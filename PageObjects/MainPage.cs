using OpenQA.Selenium;


namespace Test_Framework.Tests

{
    public class MainPage
    {
        #region locators
        private By logo = By.Id("dnn_dnnLogo_imgSDLogo");


        #endregion
        public MainPage(IWebDriver driver)
        { pageDriver = driver; }

        public IWebDriver pageDriver { get; }

        /// <summary>
        /// Navigates to a specified URL
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        public void NavigateTo(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            UI.WaitForPageToLoad(driver);
        }

        /// <summary>
        /// Gets the Current URL
        /// </summary>
        /// <returns>Current URL</returns>
        public string  GetCurrentURL()
        {
           return pageDriver.Url.ToString();
        }

    }
}