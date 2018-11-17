using TTCtestFramework.Helper;
using TTCtestFramework.Utility;
using System;
using TechTalk.SpecFlow;

namespace TTCtestFramework.Framework
{
    /// <summary>
    /// The BaseFramework class provides a location for objects that will only be created once 
    /// during the scope of a test execution. 
    /// 
    /// Classes like TestConfiguration and Globals only need to be instantiated once for 
    /// the life of the application process. 
    /// 
    /// Other base classes like BasePage, BaseStepDefinition and BaseRestClient will inherit 
    /// from the BaseFramework class. This will allow us to manage how object are created. 
    /// Another option would be to use dependecy injections to manage these objects. 
    /// </summary>

    public class BaseFramework : Steps
    {
        private static readonly Log logger = new Log(typeof(BaseFramework));

        public static readonly int ShortIntervalInMilliseconds = 1000;
        public static readonly int MediumIntervalInMilliseconds = 3000;
        public static readonly int LongIntervalInMilliseconds = 5000;

        private static int refCount = 0;

        // Test configuration will contain data used to execute the test
        // The testConfiguration object is created once below
        protected static TestConfiguration testConfiguration = null;

        // The globals object is used to store data that can be across various 
        // step definition classes and other object when needed. 
        // The globals object is created once below
        protected static Globals globals = null;

        /// <summary>
        /// Default constructor for BaseFramework will be used to manage the objects that 
        /// are created in the BaseFramework class
        /// </summary>
        public BaseFramework()
        {
            // This is making a lot of noice in the logs
            // logger.Debug("Inside: BaseFramework::BaseFramework(). refCount: {0}", refCount);

            if (testConfiguration == null)
            {
                // We will only write the test configuration to the log once
                if (refCount == 0)
                {
                    // This info log will only be written once during the test execution
                    logger.Info("--------------------------------------------------------------------------------");
                    logger.Info("Start the Test Automation Process");
                    logger.Info("Create the TestConfiguration");

                    // Create the testConfiguration object once
                    testConfiguration = new TestConfiguration();

                    // Create the globals object once
                    globals = new Globals();

                    // Write configuration to the log
                    testConfiguration.GetConfigurationManager().WriteConfigurationToLog();
                }
            }
            else
            {
                // This is making a lot of noice in the logs
                // logger.Debug("Attempted to create TestConfiguration - object already created!");
            }

            refCount++;
        }

        /// <summary>
        /// Pause code execution for scpesified time period.
        /// </summary>
        /// <param name="TimeoutInMilliSeconds"></param>
        public void DelayForNexAction(int TimeoutInMilliSeconds)
        {
            try
            {
                System.Threading.Thread.Sleep(TimeoutInMilliSeconds);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
    }
}
