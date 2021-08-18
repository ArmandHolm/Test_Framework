using System;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Test_Framework.Tests
{
    class SportDirect
    {
        [TestFixture]
        [Parallelizable]
        public class Fixture_1 : TestBase
        {
            [TestCase]
            [Category("Regression"), Category("Functional")]
            public void Test001_Add_Product_To_Card()
            {
                var main = new MainPage(DRIVER);
                //Step 1. Navigates to the URL that is stated in the Settings file
                main.NavigateTo(DRIVER, Settings.BaseURL);
                //Step 2. Gets the current URL
                var currentURL = main.GetCurrentURL();
                var menu = new MenuNavigation(DRIVER);
                //Step 3. Navigates to Sports -> Ice Hockey section via the top menu
                menu.FocusMenu("SPORTS")
                    .ClickMenu("ICE_HOCKEY");
                var ice = new IceHockeyPage(DRIVER);
                //Step 4. Clicks "Hockey Bauer" option in the left menu
                ice.ClickHockeyBauer();
                //Step 5. Waits for page to load
                UI.WaitForPageToLoad(DRIVER);
                Random random = new Random();
                //Step 6. Finds products and puts the elements into list
                var products = DRIVER.FindElements(By.XPath("//ul[@id='navlist']//a/div/img"));
                int randomNumber = random.Next(0, products.Count);
                //Step 7. Clicks a random product from the ones available
                products[randomNumber].Click();
                //Step 8. Gets the name of the porduct chosen in the previous step
                var productName = ice.GetProductName();
                //Step 9. Chooses the skate size (1 - 3) whenre 1 is smallest and 3 is largest
                ice.ChooseSkateSize("1");
                //Step 10. Clicks "Add product to bag"
                ice.ClickAddToBag();
                //Step 11. Clicks "Back"
                ice.ClickBag();
                var bag = new BagPage(DRIVER);
                //Step 12. Gets the product name when in the "bag"
                var productNameInBag = bag.GetProductName();
                Assert.Multiple(() =>
                {
                    //Asserts that there were no redirections while navigating to the site.
                    StringAssert.AreEqualIgnoringCase("https://lv.sportsdirect.com/", currentURL);
                    //Asserts that the product chosen equals to the product in the "bag" / "shopping cart"
                    StringAssert.AreEqualIgnoringCase("Bauer "+productName, productNameInBag);
                });
            }
        }

    }
}
