using BoDi;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Safari;
using TTCtestFramework.Helper;
using TTCtestFramework.Framework;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Threading;
using Protractor;

namespace TTCtestFramework.Utility
{

    /// <summary>
    /// The BaseWebDriver class will implement the functionality related to the Selenuim  
    /// WebDriver object. BaseWebDriver is exposed to the test Page classes through the 
    /// BasePage Class.
    /// </summary>

    /// <remarks>
    /// The BaseWebDriver object will only support ChromeDriver at this time. We will 
    /// implement Firefox, IE and other drivers when we implement the PropertiesManager 
    /// and TestConfiguration supporting classes. 
    /// </remarks>

    public class BaseWebDriver : BaseFramework
    {
        private static readonly Log logger = new Log(typeof(BaseWebDriver));

        private int _DefaultTimeout;
        private bool _UseProtractor = false;
        protected static IWebDriver _IWebDriver = null;

        /// <summary>
        /// Create new BaseWebDriver.
        /// </summary>

        public BaseWebDriver()
        {
        }

        [Obsolete("Use the Driver property instead.")]
        /// <summary>
        /// Return the WebDriver _IWebDriver object. 
        /// </summary>
        /// <returns> Return the WebDriver _IWebDriver object.</returns>
        public IWebDriver GetDriver()
        {
            return Driver;
        }

        /// <summary>
        ///  Return the Webdriver object.
        /// </summary>
        public IWebDriver Driver
        {
            get
            {
                return _IWebDriver;
            }
        }

        /// <summary>
        /// Creates a new instance of WebDriver, setsup initial settings for browser.
        /// </summary>
        /// 
        public void Initialize(IObjectContainer objectContainer, bool protractor = false)
        {
            _DefaultTimeout = testConfiguration.GetBrowserTimeoutSeconds();
            _UseProtractor = protractor;
            string[] optionStrings = testConfiguration.GetBrowserOptions();
            DriverService service;
            DriverOptions options;
            var timeout = TimeSpan.FromSeconds(_DefaultTimeout);

            switch (testConfiguration.GetBrowserType().ToLower())
            {
                case Constants.BrowserTypeFirefox:
                    service = FirefoxDriverService.CreateDefaultService();
                    options = new FirefoxOptions();
                    for (int i = 0; i < optionStrings.Length; i += 1)
                    {
                        (options as FirefoxOptions).AddArgument(optionStrings[i]);
                    }
                    _IWebDriver = new FirefoxDriver(service as FirefoxDriverService, options as FirefoxOptions, timeout);
                    _IWebDriver.Manage().Window.Maximize();
                    _IWebDriver.Manage().Timeouts().PageLoad = timeout;

                    break;

                case Constants.BrowserTypeChrome:
                    service = ChromeDriverService.CreateDefaultService();
                    options = new ChromeOptions();
                    for (int i = 0; i < optionStrings.Length; i += 1)
                    {
                        (options as ChromeOptions).AddArgument(optionStrings[i]);
                    }
                    _IWebDriver = new ChromeDriver(service as ChromeDriverService, options as ChromeOptions, timeout);
                    _IWebDriver.Manage().Window.Maximize();
                    _IWebDriver.Manage().Timeouts().PageLoad = timeout;

                    break;

                case Constants.BrowserTypeIE:
                    service = InternetExplorerDriverService.CreateDefaultService();
                    options = new InternetExplorerOptions
                    {
                        IgnoreZoomLevel = true,
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        EnsureCleanSession = true
                    };
                    _IWebDriver = new InternetExplorerDriver(service as InternetExplorerDriverService, options as InternetExplorerOptions, timeout);
                    _IWebDriver.Manage().Window.Maximize();
                    _IWebDriver.Manage().Timeouts().PageLoad = timeout;

                    break;

                case Constants.BrowserTypeEdge:

                    _IWebDriver = new EdgeDriver();
                    _IWebDriver.Manage().Window.Maximize();
                    _IWebDriver.Manage().Timeouts().PageLoad = timeout;

                    break;

                case Constants.BrowserTypeSafari:

                    _IWebDriver = new SafariDriver();
                    _IWebDriver.Manage().Window.Maximize();
                    _IWebDriver.Manage().Timeouts().PageLoad = timeout;

                    break;

                default:

                    // The browser type from testConfiguration did not match any valid 
                    // options - we will throw the exception

                    string errorMessage = string.Format("Browser type is not valid. The browserType is " +
                        "set in the configuration file or on the command line. browserType: {0}",
                                    testConfiguration.GetBrowserType());

                    logger.Error(errorMessage);

                    throw new Exception(errorMessage);


            }
            if (_UseProtractor)
            {
                _IWebDriver = new NgWebDriver(_IWebDriver);
            }
            objectContainer.RegisterInstanceAs<IWebDriver>(_IWebDriver);
        }


        /// <summary>
        /// Clear Browser Cache
        /// </summary>
        public void ClearBrowserCache()
        {
            switch (testConfiguration.GetBrowserType().ToLower())
            {
                case Constants.BrowserTypeFirefox:

                    logger.Error("Clear browser cache not supported for browser type: {0}", testConfiguration.GetBrowserType().ToLower());
                    break;

                case Constants.BrowserTypeChrome:

                    Driver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
                    Thread.Sleep(5000);
                    Driver.SwitchTo().ActiveElement();
                    Driver.FindElement(By.CssSelector("* /deep/ #clearBrowsingDataConfirm")).Click();
                    Thread.Sleep(5000);
                    break;

                case Constants.BrowserTypeIE:

                    logger.Error("Clear browser cache not supported for browser type: {0}", testConfiguration.GetBrowserType().ToLower());
                    break;

                case Constants.BrowserTypeEdge:

                    logger.Error("Clear browser cache not supported for browser type: {0}", testConfiguration.GetBrowserType().ToLower());
                    break;

                case Constants.BrowserTypeSafari:

                    logger.Error("Clear browser cache not supported for browser type: {0}", testConfiguration.GetBrowserType().ToLower());
                    break;

                default:

                    // The browser type from testConfiguration did not match any valid 
                    // options - we will throw the exception

                    string errorMessage = string.Format("Browser type is not valid. The browserType is " +
                        "set in the configuration file or on the command line. browserType: {0}",
                                    testConfiguration.GetBrowserType());

                    logger.Error(errorMessage);

                    throw new Exception(errorMessage);
            }
        }

        public WebDriverWait Wait(int seconds)
        {
            var timeout = TimeSpan.FromSeconds(seconds > 0 ? seconds : _DefaultTimeout);
            return new WebDriverWait(Driver, timeout);
        }

        /// <summary>
        /// Waits for an element to be visible for seconds specified.
        /// </summary>
        /// <param name="by">element locator</param>
        /// <param name="waitInSeconds">wait time in seconds</param>
        /// <returns>WebElement</returns>
        public IWebElement WaitForElementVisible(By by, int waitInSeconds = 0)
        {
            return Wait(waitInSeconds).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }

        /// <summary>
        /// Waits for a list of elements to be visible that match the locator for seconds specified.
        /// </summary>
        /// <param name="by">element locator</param>
        /// <param name="waitInSeconds">wait time in seconds</param>
        /// <returns>WebElement</returns>
        public ReadOnlyCollection<IWebElement> WaitForElementsVisible(By by, int waitInSeconds = 0)
        {
            return Wait(waitInSeconds).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }

        /// <summary>
        /// Waits for a list of elements to be visible that match the locator for seconds specified.
        /// </summary>
        /// <param name="webElements">list of web elements</param>
        /// <param name="waitInSeconds">wait time in seconds</param>
        /// <returns>WebElement</returns>
        public ReadOnlyCollection<IWebElement> WaitForElementsVisible(ReadOnlyCollection<IWebElement> webElements, int waitInSeconds = 0)
        {
            return Wait(waitInSeconds).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(webElements));
        }

        /// <summary>
        /// Waits for an element to be invisible for seconds specified.
        /// </summary>
        /// <param name="by">element locator</param>
        /// <param name="waitInSeconds">wait time in seconds</param>
        public void WaitForElementInvisible(By by, int waitInSeconds = 0)
        {
            Wait(waitInSeconds).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
        }

        /// <summary>
        /// Waits for an element to be clickable for seconds specified.
        /// </summary>
        /// <param name="by">element locator</param>
        /// <param name="waitInSeconds">wait rime in seconds</param>
        /// <returns>WebElement</returns>
        public IWebElement WaitForElementClickable(By by, int waitInSeconds = 0)
        {
            return Wait(waitInSeconds).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }

        /// <summary>
        /// Waits for an element to be clickable for seconds specified.
        /// </summary>
        /// <param name="webElement">web element</param>
        /// <param name="waitInSeconds">wait time in seconds</param>
        /// <returns>WebElement</returns>
        public IWebElement WaitForElementClickable(IWebElement webElement, int waitInSeconds = 0)
        {
            return Wait(waitInSeconds).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement));
        }

        /// <summary>
        /// Waits for an element to appear in the DOM for seconds specified.
        /// </summary>
        /// <param name="by">element locator</param>
        /// <param name="waitInSeconds">wait time in seconds</param>
        /// <returns></returns>
        public IWebElement WaitForElement(By by, int waitInSeconds = 0)
        {
            return Wait(waitInSeconds).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
        }

        /// <summary>
        ///  Waits for a list of elements to appear in the DOM that match the locator for seconds specified.
        /// </summary>
        /// <param name="by">elements locator</param>
        /// <param name="waitInSeconds">wait time in seconds</param>
        /// <returns>List of WebElements</returns>
        public ReadOnlyCollection<IWebElement> WaitForElements(By by, int waitInSeconds = 0)
        {
            return Wait(waitInSeconds).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
        }

        /// <summary>
        /// Quit WebDriver, closing all assosiated windows. 
        /// </summary>
        public void Teardown()
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
