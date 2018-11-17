using OpenQA.Selenium;
using TTCtestFramework.Page;

namespace TTC.Pages
{
    class BaseTTCPage : BasePage
    {
        /// <summary>
        ///  Will return true or false to validate if an element is displayed
        /// </summary>
        /// <param name="inputElement"></param>
        /// <returns>true or false</returns>
        public bool IsElementDisplayed(IWebElement inputElement)
        {
            try
            {
                return inputElement.Displayed;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  Click the back button
        /// </summary>
        /// <returns></returns>
        public void ClickBackButton()
        {
            webDriver.Driver.Navigate().Back();
        }

    }
}
