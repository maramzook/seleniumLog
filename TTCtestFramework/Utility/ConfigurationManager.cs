using System;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using TTCtestFramework.Helper;

namespace TTCtestFramework.Utility
{

    public class ConfigurationManager
    {
        private static readonly Log logger = new Log(typeof(ConfigurationManager));

        // Filename
        private const string DefaultAutomationConfigFileName = "AppAutomationConfig.json";

        // Platform     
        private const string DefaultPlatformOS = "Linux";
        private const string DefaultPlatformVersion = "12.34";

        // Browser
        private const string DefaultBrowserType = Constants.BrowserTypeChrome;
        private const string DefaultBrowserVersion = "22.6";
        private const int DefaultBrowserTimeoutSeconds = Constants.BrowserTimeoutSeconds;
        private const string[] DefaultBrowserOptions = null;

        // Application 
        private const string DefaultApplicationName = "General UI Test - Sample Test Project";
        private const string DefaultApplicationComment = "UI test cases to test framework components (SampleTestProject)";
        private const string DefaultApplicationURL = "https://google.com";
        private const string DefaultApplicationURL2 = "http://somewhere.com/";

        // User
        private const string DefaultUserName = "";
        private const string DefaultUserPassword = "";

        // Screen Capture
        private const Boolean DefaultScreenCaptureEnable = true;
        private const string DefaultScreenCaptureLogLevel = "Error";

        // JSON object with configuration data
        protected JObject appConfigJObject;

        // Automation configuration file location
        private string automationConfigFileName;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ConfigurationManager()
        {
            // We will read from the project folder
            string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            automationConfigFileName = projectPath + DefaultAutomationConfigFileName;
        }

        /// <summary>
        /// This will read the application config file into memory 
        /// We might want to just put this in the constrcutor
        /// </summary>
        public void ReadConfigFile()
        {
            try
            {
                // read JSON directly from a file
                logger.Debug("Read configuration file.");
                using (StreamReader file = File.OpenText(automationConfigFileName))
                using (JsonTextReader jsonTextReader = new JsonTextReader(file))
                {
                    appConfigJObject = (JObject)JToken.ReadFrom(jsonTextReader);
                }
            }
            catch (FileNotFoundException ex)
            {
                // Configuration file not found

                // Create a default configuration file when none found
                CreateDefaultConfigFile();

                string errorMessage = string.Format("The Automation Application Configuration file was not found. " +
                    "The test will end. A default file will be created with default values. The configuration " +
                    "should be updated. Configuration file location: {0} Exception message: {1}",
                    automationConfigFileName,
                    ex.Message);

                logger.Error(errorMessage);

                throw new System.Exception(errorMessage);
            }
            catch (Exception ex)
            {
                // Handle a general exception

                string errorMessage = string.Format("An exception was handled while reading the Automation Application " +
                    "Configuration file. The test will end. Have a nice day. Exception message: {0}",
                    ex.Message);

                logger.Error(errorMessage);

                throw new System.Exception(errorMessage);
            }
        }

        /// <summary>
        /// The CreateDefaultConfigFile() will create a default configuration file. This 
        /// method will be called when we attempt to read the configuration file and 
        /// it does not exist. 
        /// </summary>
        private void CreateDefaultConfigFile()
        {
            // create the default JSON configuration file

            try
            {
                logger.Debug("Create default configuration file.");
                JObject platform = new JObject(
                new JProperty("platformOS", DefaultPlatformOS),
                new JProperty("platformVersion", DefaultPlatformVersion));

                JObject browser = new JObject(
                new JProperty("browserType", DefaultBrowserType),
                new JProperty("browserVersion", DefaultBrowserVersion),
                new JProperty("browserTimeoutSeconds", DefaultBrowserTimeoutSeconds.ToString()),
                new JProperty("browserOptions", DefaultBrowserOptions));

                JObject application = new JObject(
                new JProperty("applicationName", DefaultApplicationName),
                new JProperty("applicationComment", DefaultApplicationComment),
                new JProperty("applicationURL", DefaultApplicationURL),
                new JProperty("applicationURL2", DefaultApplicationURL2));

                JObject user = new JObject(
                new JProperty("userName", DefaultUserName),
                new JProperty("userPassword", DefaultUserPassword));

                JObject screenCapture = new JObject(
                new JProperty("enable", DefaultScreenCaptureEnable),
                new JProperty("logLevel", DefaultScreenCaptureLogLevel));

                JObject configurationRoot = new JObject(
                new JProperty("platform", platform),
                new JProperty("browser", browser),
                new JProperty("application", application),
                new JProperty("user", user),
                new JProperty("screenCapture", screenCapture));

                File.WriteAllText(automationConfigFileName, JToken.FromObject(configurationRoot).ToString());

                using (StreamReader file = File.OpenText(automationConfigFileName))
                using (JsonTextReader jsonTextReader = new JsonTextReader(file))
                {
                    appConfigJObject = (JObject)JToken.ReadFrom(jsonTextReader);
                }
            }
            catch (JsonException ex)
            {
                // Json exception

                string errorMessage = string.Format("A JSON exception was handled while creating the reading " +
                    "the Automation Application Configuration file. The test will end. Have a nice day. Exception message: {0}",
                    ex.Message);

                logger.Error(errorMessage);

                throw new System.Exception(errorMessage);
            }
            catch (Exception ex)
            {
                // Handle general exception

                string errorMessage = string.Format("An exception was handled while creating the Automation Application " +
                    "Configuration file. The test will end. Have a nice day. Exception message: {0}",
                    ex.Message);

                logger.Error(errorMessage);

                throw new System.Exception(errorMessage);
            }
        }

        /// <summary>
        /// The WriteConfigurationToLog() method will write the current configuration to 
        /// the Log object. 
        /// </summary>
        public void WriteConfigurationToLog()
        {
            logger.Info("Current application configuration settings.");

            // Configuration file location    
            logger.Info("Configuration file: {0}", automationConfigFileName);

            // Application platform
            logger.Info("Platform OS: {0}", GetPlatformOS());
            logger.Info("Platform Version: {0}", GetPlatformVersion());

            // Browser
            logger.Info("Browser Type: {0}", GetBrowserType());
            logger.Info("Browser Version: {0}", GetBrowserVersion());
            logger.Info("Browser Timeout (sec): {0}", GetBrowserTimeoutSeconds());
            logger.Info("Browser Options: {0}", GetBrowserOptions());

            // Application Name
            logger.Info("Application Name: {0}", GetApplicationName());
            logger.Info("Application Comment: {0}", GetApplicationComment());
            logger.Info("Application URL: {0}", GetApplicationURL());
            logger.Info("Application URL2: {0}", GetApplicationURL2());

            // User configuration 
            logger.Info("User Name: {0}", GetUserName());
            logger.Info("User Password: {0}", GetUserPassword());

            // Screen Capture 
            logger.Info("ScreenCapture Enable: {0}", GetScreenCaptureEnable());
            logger.Info("ScreenCapture LogLevel: {0}", GetScreenCaptureLogLevel());
        }

        #region Platform configuration
        // Platform configuration

        /// <summary>
        /// Get the platform OS value from the json configuration object
        /// </summary>
        /// <returns>string platform OS</returns>
        public string GetPlatformOS()
        {
            return (string)appConfigJObject["platform"]["platformOS"];
        }

        /// <summary>
        /// Get the platform version value from the json configuration object
        /// </summary>
        /// <returns>string platform version</returns>
        public string GetPlatformVersion()
        {
            return (string)appConfigJObject["platform"]["platformVersion"];
        }
        #endregion

        #region Browser configuration
        // Browser configuration

        /// <summary>
        /// Get the browser type value from the json configuration object
        /// </summary>
        /// <returns>string browser type</returns>
        public string GetBrowserType()
        {
            return (string)appConfigJObject["browser"]["browserType"];
        }

        /// <summary>
        /// Get the browser version value from the json configuration object
        /// </summary>
        /// <returns>string browser version</returns>
        public string GetBrowserVersion()
        {
            return (string)appConfigJObject["browser"]["browserVersion"];
        }

        /// <summary>
        /// Get the browser timeout in seconds value from the json configuration object
        /// </summary>
        /// <returns>string browser timeout seconds</returns>
        public string GetBrowserTimeoutSeconds()
        {
            return (string)appConfigJObject["browser"]["browserTimeoutSeconds"];
        }

        /// <summary>
        /// Get the browser options. These are passed along when the web driver is initialized.
        /// </summary>
        /// <returns>array of browser options</returns>
        public string[] GetBrowserOptions()
        {
            var optionsBlock = appConfigJObject["browser"]["browserOptions"];
            BaseList<string> optionsList = new BaseList<string>();
            if (optionsBlock != null)
            {
                foreach (var optionValue in optionsBlock.Values<string>())
                {
                    optionsList.Add((string)optionValue);
                }
            }

            return optionsList.ToArray();
        }
        #endregion

        #region Application configuration
        // Application configuration

        /// <summary>
        /// Get the application name value from the json configuration object
        /// </summary>
        /// <returns>string application name</returns>
        public string GetApplicationName()
        {
            return (string)appConfigJObject["application"]["applicationName"];
        }

        /// <summary>
        /// Get the application comment value from the json configuration object
        /// </summary>
        /// <returns>string application comment</returns>
        public string GetApplicationComment()
        {
            return (string)appConfigJObject["application"]["applicationComment"];
        }

        /// <summary>
        /// Get the application URL value from the json configuration object
        /// </summary>
        /// <returns>string application URL</returns>
        public string GetApplicationURL()
        {
            return (string)appConfigJObject["application"]["applicationURL"];
        }

        /// <summary>
        /// Get the application URL 2 value from the json configuration object
        /// </summary>
        /// <returns>string application URL</returns>
        public string GetApplicationURL2()
        {
            return (string)appConfigJObject["application"]["applicationURL2"];
        }
        #endregion

        #region User configuration
        // User configuration

        /// <summary>
        /// Get the user name value from the json configuration object
        /// </summary>
        /// <returns>string user name</returns>
        public string GetUserName()
        {
            return (string)appConfigJObject["user"]["userName"];
        }

        /// <summary>
        /// Get the user password value from the json configuration object
        /// </summary>
        /// <returns>string user password</returns>
        public string GetUserPassword()
        {
            return (string)appConfigJObject["user"]["userPassword"];
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
            return Convert.ToBoolean((string)appConfigJObject["screenCapture"]["enable"]);
        }

        /// <summary>
        /// Get the screen capture enable value from the json configuration object
        /// 
        /// We will convert the string from the JSON configuration value to a LogLevel
        /// enum. There is a way to serialize this using JSON - we will look into it. 
        /// </summary>
        /// <returns>LogLevel screen capture enable value</returns>
        public LogLevel GetScreenCaptureLogLevel()
        {
            LogLevel enumLogLevel = LogLevel.Error;

            string strLogLevel = (string)appConfigJObject["screenCapture"]["logLevel"];

            if (strLogLevel.ToLower().CompareTo(LogLevel.All.ToString().ToLower()) == 0)
            {
                enumLogLevel = LogLevel.All;
            }
            else if (strLogLevel.ToLower().CompareTo(LogLevel.Debug.ToString().ToLower()) == 0)
            {
                enumLogLevel = LogLevel.Debug;
            }
            else if (strLogLevel.ToLower().CompareTo(LogLevel.Error.ToString().ToLower()) == 0)
            {
                enumLogLevel = LogLevel.Error;
            }
            else if (strLogLevel.ToLower().CompareTo(LogLevel.Fatal.ToString().ToLower()) == 0)
            {
                enumLogLevel = LogLevel.Fatal;
            }
            else if (strLogLevel.ToLower().CompareTo(LogLevel.Info.ToString().ToLower()) == 0)
            {
                enumLogLevel = LogLevel.Info;
            }
            else if (strLogLevel.ToLower().CompareTo(LogLevel.Off.ToString().ToLower()) == 0)
            {
                enumLogLevel = LogLevel.Off;
            }
            else if (strLogLevel.ToLower().CompareTo(LogLevel.Warn.ToString().ToLower()) == 0)
            {
                enumLogLevel = LogLevel.Warn;
            }
            else
            {
                string errorMessage = string.Format("Screen Capture LogLevel is not valid. strLogLevel: {0} " +
                    "Valid screen capture logLevel are: All, Debug, Info, Warn, Error, Fatal and Off",
                    strLogLevel);

                logger.Error(errorMessage);

                throw new System.Exception(errorMessage);
            }

            return enumLogLevel;
        }

        #endregion
    }
}
