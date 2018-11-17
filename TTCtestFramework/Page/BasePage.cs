using OpenQA.Selenium;
using TTCtestFramework.Framework;
using TTCtestFramework.Helper;
using TTCtestFramework.Utility;
using System;
using System.Collections.ObjectModel;

namespace TTCtestFramework.Page
{

    /// <summary>
    /// The BasePage class will implement the functionality related to the page 
    /// object. Page classes that support specific webpages will inherit from this class.
    /// </summary>
    public class BasePage : BaseFramework
    {
        protected BaseWebDriver webDriver;

        protected IWebElement element;

        protected ReadOnlyCollection<IWebElement> elements;

        private static readonly Log logger = new Log(typeof(BasePage));

        /// <summary>
        /// Get the BaseWebDriver 
        /// </summary>
        /// <returns>BaseWebDriver</returns>
        public BaseWebDriver GetBaseWebDriver()
        {
            return webDriver;
        }

        /// <summary>
        /// Navigates to a specified Url.
        /// </summary>
        /// <param name="Url"></param>
        public void NavigateToUrl(string Url)
        {
            webDriver.Driver.Navigate().GoToUrl(Url);
            logger.Debug("Navigated to: '{0}'", Url);
        }


        /// <summary>
        /// Navigates to a specified Url.
        /// </summary>
        /// <param name="Url"></param>
        public void NavigateBack()
        {
            webDriver.Driver.Navigate().Back();
            logger.Debug("Navigated back to previous page");
        }

        /// <summary>
        /// Get Page title
        /// </summary>
        /// <returns>string page title text</returns>
        public string GetPageTitle()
        {
            return webDriver.Driver.Title;
        }

        /// <summary>
        /// switches to new window handles
        /// </summary>
        public void SwitchToNewWindowHandle()
        {
            // get original window handle
            string winHandleOriginal = webDriver.Driver.CurrentWindowHandle.ToString();

            // account for slow window....
            for (int i = 0; i < testConfiguration.GetBrowserTimeoutSeconds(); i++)
            {
                // get all window handles
                ReadOnlyCollection<String> handleList = webDriver.Driver.WindowHandles;

                // Switch to the new window
                foreach (String handleStr in handleList)
                {
                    if (handleStr != winHandleOriginal)
                    {
                        webDriver.Driver.SwitchTo().Window(handleStr);
                        return;
                    }
                }
                if (i == testConfiguration.GetBrowserTimeoutSeconds() - 1)
                {
                    throw new NoSuchWindowException("New window did not appear. Please check the link that should be triggering the new window.");
                }
                else
                {
                    DelayForNexAction(ShortIntervalInMilliseconds);
                }
            }
        }

        /// <summary>
        /// switches to window handles by index
        /// first opened window handle index = 0
        /// 2nd opened window handle index = 1
        /// 3rd opened window handle index = 2
        /// and so on...
        /// </summary>
        /// <param name="windowsHandleNumber">window handle number to switch on</param>
        public void SwitchToWindowHandleByIndex(int windowsHandleNumber)
        {
            //string winHandleOriginal = GetBaseWebDriver().GetDriver().CurrentWindowHandle.ToString();

            // get all window handles, accounting for slow response of expected window....           
            for (int i = 0; i < testConfiguration.GetBrowserTimeoutSeconds(); i++)
            {
                ReadOnlyCollection<String> handleList = webDriver.Driver.WindowHandles;
                if (handleList.Count >= windowsHandleNumber + 1)
                {
                    webDriver.Driver.SwitchTo().Window(handleList[windowsHandleNumber]);
                    return;
                }
                else if (i == testConfiguration.GetBrowserTimeoutSeconds() - 1)
                {
                    throw new NoSuchWindowException("Given window handle index number not found. Window handle " +
                        "index start with 0 (means first window). Please check how many windows are opened and which window you want to switch on!!");
                }
                else
                {
                    DelayForNexAction(ShortIntervalInMilliseconds);
                }
            }
        }

        /// <summary>
        /// Accepts pop-up alert warning
        /// </summary>
        public void AcceptAlert()
        {
            webDriver.Driver.SwitchTo().Alert().Accept();
        }

        /// <summary>
        /// Returns alert text
        /// </summary>
        /// <returns></returns>
        public String GetAlertText()
        {
            return webDriver.Driver.SwitchTo().Alert().Text;
        }


        /// <summary>
        /// gets the current url
        /// /// </summary>
        public string CurrentUrl
        {
            get { return webDriver.Driver.Url; }
        }


    }
}
