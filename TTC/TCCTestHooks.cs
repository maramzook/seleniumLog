using BoDi;
using TTCtestFramework.Helper;
using TTCtestFramework.Utility;
using System.Diagnostics;
using TechTalk.SpecFlow;

namespace TTC
{
    [Binding]
    public sealed class TCCTestHooks
    {
        private static readonly Log logger = new Log(typeof(TCCTestHooks));
        private ScreenCapture screenCapture;
        private TestConfiguration testConfiguration = new TestConfiguration();

        private static string featureTitle;
        private string scenarioTitle;
        private string stepInfoText;

        private BaseWebDriver webDriver = new BaseWebDriver();
        private IObjectContainer objectContainer;

        public TCCTestHooks(IObjectContainer objectContainer) => this.objectContainer = objectContainer;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            CleanUpChromeDriverProcess();
        }

        [AfterTestRun(Order = 10)]
        public static void AfterTestRun()
        {
            CleanUpChromeDriverProcess();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            featureTitle = FeatureContext.Current.FeatureInfo.Title;
            logger.Info("Feature Title: " + featureTitle);
        }

        [AfterFeature]
        public static void AfterFeature()
        {

        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            scenarioTitle = ScenarioContext.Current.ScenarioInfo.Title;
            logger.Info("Scenario Title: " + scenarioTitle);

            webDriver.Initialize(objectContainer);
            screenCapture = new ScreenCapture(webDriver.Driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            webDriver.Teardown();
            CleanUpChromeDriverProcess();
        }

        [BeforeStep]
        public void BeforeStep()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            stepInfoText = ScenarioStepContext.Current.StepInfo.Text;

            logger.Info("Step: " + stepType + " " + stepInfoText);
        }

        [AfterStep]
        public void AfterStep()
        {
            string testResultStatus = ScenarioContext.Current.ScenarioExecutionStatus.ToString();
            logger.Info("Step Done: " + testResultStatus);

            string screenShotNamePrefix = string.Format("{0}_{1}", featureTitle, scenarioTitle);

            if ((ScenarioContext.Current.TestError != null) || (testConfiguration.GetScreenCaptureLogLevel().Equals(LogLevel.All)))
            {
                screenCapture.TakeScreenshot(screenShotNamePrefix);
                logger.Info("Test Error Exception(if Any): " + ScenarioContext.Current.TestError);
            }
        }

        internal static void CleanUpChromeDriverProcess()
        {
            Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");
            foreach (var chromeDriverProcess in chromeDriverProcesses)
            {
                chromeDriverProcess.Kill();
            }
        }

    }
}
