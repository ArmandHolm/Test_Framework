using OpenQA.Selenium;


namespace Test_Framework.Tests

{
    public class BagPage
    {
        #region locators
        private By txtProductName = By.Id("dhypProductLink");


        #endregion
        public BagPage(IWebDriver driver)
        { pageDriver = driver; }

        public IWebDriver pageDriver { get; }

        /// <summary>
        /// Gets the name of the product
        /// </summary>
        /// <returns>Returns the name of the product</returns>
        public string GetProductName()
        {
            UI.WaitForElementVisible(pageDriver, txtProductName);
            return UI.GetText(pageDriver.FindElement(txtProductName));
        }
    }
}