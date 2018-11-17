using OpenQA.Selenium;
using TTCtestFramework.Utility;

namespace TTC.Pages
{
    class PeoplePage : BaseTTCPage
    {
        /// <summary>
        /// The EmployerBillingDetailPage class will support the Employer Billing Detail page. This class will provide access
        /// to web elements on the page and helper methods. 
        /// </summary>
            public PeoplePage(BaseWebDriver webDriver) => this.webDriver = webDriver;

            /////////////////////////////////////////////////////////
            //SELECTORS
            ////////////////////////////////////////////////////////

            /// <summary>
            /// Returns group name
            /// </summary>
            /// <returns>IWebElement group name</returns>
            public IWebElement GetMenuLink()
            {
                var element = webDriver.WaitForElement(By.XPath("//div[text()='Menu']"));
                return element;
            }


        public IWebElement GetPeopleLink()
        {
            var element = webDriver.WaitForElement(By.CssSelector("a[href='our-people']"));
            return element;
        }


        public int GetTotalPeople()
        {
            var elements = webDriver.WaitForElements(By.CssSelector("h4[class='profile-name']"));
            return elements.Count;
        }






    }
}
