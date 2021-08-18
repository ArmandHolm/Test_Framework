using OpenQA.Selenium;


namespace Test_Framework.Tests

{
    public class IceHockeyPage
    {
        #region locators
        private By iHAccessories = By.LinkText("Ice Hockey Accessories");
        private By iHABauer = By.LinkText("Bauer");
        private By btnAddToBag = By.Id("aAddToBag");
        private By btnBag = By.Id("bagQuantityContainer");
        private By txtProdName = By.Id("lblProductName");
        

        #endregion
        public IceHockeyPage(IWebDriver driver)
        { pageDriver = driver; }

        public IWebDriver pageDriver { get; }

        /// <summary>
        /// Clicks "Hockey Accessories"
        /// </summary>
        public void ClickHockeyAccessories()
        {
            UI.ClickElement(pageDriver, iHAccessories);
        }

        /// <summary>
        /// Clicks "Hockey Bauer"
        /// </summary>
        public void ClickHockeyBauer()
        {
            UI.ClickElement(pageDriver, iHABauer);
        }

        /// <summary>
        /// Gets the name of the product
        /// </summary>
        /// <returns>Returns name of the product</returns>
        public string GetProductName()
        {
            UI.WaitForElementVisible(pageDriver, txtProdName);
            return UI.GetText(pageDriver.FindElement(txtProdName));
        }

        /// <summary>
        /// Clicks "Add to bag" button
        /// </summary>
        public void ClickAddToBag()
        {
            UI.ClickElement(pageDriver, btnAddToBag);
        }

        /// <summary>
        /// Clicks "Bag" button
        /// </summary>
        public void ClickBag()
        {
            UI.ClickElement(pageDriver, btnBag);
        }

        /// <summary>
        /// Chooses the skate size
        /// </summary>
        /// <param name="sizeNmbr"></param>
        public void ChooseSkateSize(string sizeNmbr)
        {
            UI.ClickElement(pageDriver, By.XPath("//ul[@id='ulSizes']/li[" + sizeNmbr + "]/a"));
        }


    }
}