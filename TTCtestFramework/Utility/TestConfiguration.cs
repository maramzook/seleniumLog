using TTCtestFramework.Helper;
using System;

namespace TTCtestFramework.Utility
{

    public class TestConfiguration
    {
        protected ConfigurationManager configurationManager;


        // Platform     
        private string platformOS;
        private string platformVersion;

        // Browser
        private string browserType;
        private string browserVersion;
        private int browserTimeoutSeconds;
        private string[] browserOptions;

        // Application 
        private string applicationName;
        private string applicationComment;
        private string applicationURL;
        private string applicationURL2;

        // User
        private string userName;
        private string userPassword;

        // Screen Capture 
        private Boolean screenCaptureEnable;
        private LogLevel screenCaptureLogLevel;


        public TestConfiguration()
        {
            configurationManager = new ConfigurationManager();
            configurationManager.ReadConfigFile();

            // set the test configuration values

            // Platform     
            platformOS = configurationManager.GetPlatformOS();
            platformVersion = configurationManager.GetPlatformVersion();

            // Browser
            browserType = configurationManager.GetBrowserType();
            browserVersion = configurationManager.GetBrowserVersion();
            browserTimeoutSeconds = Int32.Parse(configurationManager.GetBrowserTimeoutSeconds());
            browserOptions = configurationManager.GetBrowserOptions();

            // Application 
            applicationName = configurationManager.GetApplicationName();
            applicationComment = configurationManager.GetApplicationComment();
            applicationURL = configurationManager.GetApplicationURL();
            applicationURL2 = configurationManager.GetApplicationURL2();

            // User
            userName = configurationManager.GetUserName();
            userPassword = configurationManager.GetUserPassword();

            // Screen Capture 
            screenCaptureEnable = configurationManager.GetScreenCaptureEnable();
            screenCaptureLogLevel = configurationManager.GetScreenCaptureLogLevel();
        }

        public ConfigurationManager GetConfigurationManager()
        {
            return configurationManager;
        }

        #region Platform configuration  
        // Platform configuration

        /// <summary>
        /// Get the platform OS
        /// </summary>
        /// <returns>string platform OS</returns>
        public string GetPlatformOS()
        {
            return platformOS;
        }

        /// <summary>
        /// Get the platform version
        /// </summary>
        /// <returns>string platform version</returns>
        public string GetPlatformVersion()
        {
            return platformVersion;
        }
        #endregion

        #region Browser configuration
        // Browser configuration

        /// <summary>
        /// Get the browser type
        /// </summary>
        /// <returns>string browser type</returns>
        public string GetBrowserType()
        {
            return browserType;
        }

        /// <summary>
        /// Get the browser version
        /// </summary>
        /// <returns>string browser version</returns>
        public string GetBrowserVersion()
        {
            return browserVersion;
        }

        /// <summary>
        /// Get the browser timeout in seconds
        /// </summary>
        /// <returns>int browser timeout in seconds</returns>
        public int GetBrowserTimeoutSeconds()
        {
            return browserTimeoutSeconds;
        }

        /// <summary>
        /// Get the browser options.
        /// </summary>
        public string[] GetBrowserOptions()
        {
            return browserOptions;
        }
        #endregion

        #region Application configuration
        // Application configuration

        /// <summary>
        /// Get the application name
        /// </summary>
        /// <returns>string application name</returns>
        public string GetApplicationName()
        {
            return applicationName;
        }

        /// <summary>
        /// Get the application comment
        /// </summary>
        /// <returns>string application comment</returns>
        public string GetApplicationComment()
        {
            return applicationComment;
        }

        /// <summary>
        /// Get the application URL
        /// </summary>
        /// <returns>string application URL</returns>
        public string GetApplicationURL()
        {
            return applicationURL;
        }

        /// <summary>
        /// Get the application URL 2
        /// </summary>
        /// <returns>string application URL</returns>
        public string GetApplicationURL2()
        {
            return applicationURL2;
        }
        #endregion

        #region User configuration
        // User configuration

        /// <summary>
        /// Get the user name
        /// </summary>
        /// <returns>string user name</returns>
        public string GetUserName()
        {
            return userName;
        }

        /// <summary>
        /// Get the user password
        /// </summary>
        /// <returns>string user password</returns>
        public string GetUserPassword()
        {
            return userPassword;
        }
        #endregion

        #region ScreenCapture
        // Screen Capture

        /// <summary>
        /// Get the screen capture enable value from the json configuration object
        /// </summary>
        /// <returns>string screen capture enable value</returns>
        public Boolean GetScreenCaptureEnable()
        {
            return screenCaptureEnable;
        }

        /// <summary>
        /// Get the screen capture log level value from the json configuration object
        /// </summary>
        /// <returns>string screen capture log level value</returns>
        public LogLevel GetScreenCaptureLogLevel()
        {
            return screenCaptureLogLevel;
        }
        #endregion
    }
}
