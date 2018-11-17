using OpenQA.Selenium;
using TTCtestFramework.Framework;
using TTCtestFramework.Helper;
using System;
using System.IO;

namespace TTCtestFramework.Utility
{
    /// <summary>
    /// The ScreenCapture class will provide the functionality to create a screenshot 
    /// of a UI component during test. The image file will be used in the report when a 
    /// test has failed.
    /// 
    /// </summary>

    public class ScreenCapture : BaseFramework
    {
        private static readonly Log logger = new Log(typeof(ScreenCapture));

        private IWebDriver driver = null;

        /// <summary>
        /// Constrcutor to set the IWebDriver
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        public ScreenCapture(IWebDriver driver)
        {
            this.driver = driver;
        }


        /// <summary>
        /// Take a screenshot of the page referenced by the IWebDriver. In this version of the 
        /// TakeScreenshot() method we will test to see if the feature is enabled in the test 
        /// configuration and also evaluate the log level selected for the TakeScreenshot. 
        /// 
        /// </summary>
        public void TakeScreenshot(string screenShotNamePrefix)
        {
            int maxPathFilenameLength = 100;

            try
            {
                // Test to see if the screenshot is enabled in the test configuration 
                if (testConfiguration.GetScreenCaptureEnable())
                {

                    // The fully qualified file name must be less than 260 characters, 
                    // and the directory name must be less than 248 characters.

                    if (screenShotNamePrefix.Length > maxPathFilenameLength)
                    {
                        screenShotNamePrefix = screenShotNamePrefix.Substring(0, maxPathFilenameLength);
                    }

                    // Create the filename
                    string fileNameBase = string.Format("error_{0}_{1}",
                                                        screenShotNamePrefix,
                                                        DateTime.Now.ToString("yyyyMMdd_HHmmss_ffffff"));

                    // We will save the file to the project folder
                    string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));

                    var artifactDirectory = Path.Combine(projectPath, "testresults");

                    if (!Directory.Exists(artifactDirectory))
                    {
                        Directory.CreateDirectory(artifactDirectory);
                    }

                    // Create the takesScreenshot object
                    ITakesScreenshot takesScreenshot = driver as ITakesScreenshot;

                    if (takesScreenshot != null)
                    {
                        // Get the screenshot
                        var screenshot = takesScreenshot.GetScreenshot();

                        // Create the file path
                        string screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + "_screenshot.png");

                        // Write the image to the file system
                        screenshot.SaveAsFile(screenshotFilePath);

                        // Write the image file path to the console. This will be used to build the 
                        // test execution report
                        logger.Info("Screenshot: {0}", new Uri(screenshotFilePath));
                    }
                }
            }
            catch (UnhandledAlertException)
            {
                //TODO: do nothing for now....selenium doesn't support sceen shot when alter window pops up...we can implement to take a shot of entire page...
                logger.Warn("Alert window poped up. We are taking screen shot when therre is an alert for now. We implement to take screen shot for alert case later.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while taking screenshot: {0}", ex);
            }
        }
    }
}

