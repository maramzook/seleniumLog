using NUnit.Framework;
using TTCtestFramework.Helper;
using TTCtestFramework.Utility;
using System;
using TechTalk.SpecFlow;
using TTC.Pages;
using TTC.StepDefinitions;

namespace TTCtestFramework.StepDefinition
{
    [Binding]
    public class PeopleStepDefinition : BaseTTCStepDefinitions
    {
        private static readonly Log logger = new Log(typeof(PeopleStepDefinition));
        private PeoplePage peoplePage;

        public PeopleStepDefinition(BaseWebDriver webDriver)
        {
            peoplePage = new PeoplePage(webDriver);
        }



        [Given(@"I have prepared my presentation")]
        public void GivenIHavePreparedMyPresentation()
        {
            logger.Debug("Navigate to the website.");
            peoplePage.NavigateToUrl(testConfiguration.GetApplicationURL());
        }


        [When(@"I demo the presentation")]
        public void WhenIDemoThePresentation()
        {
            logger.Info("Clicking the MENU link");
            peoplePage.GetMenuLink().Click();

            logger.Info("Clicking the People link");
            peoplePage.GetPeopleLink().Click();
        }


        [Then(@"the good-job-offer should be in my email")]
        public void ThenTheGood_Job_OfferShouldBeInMyEmail()
        {
            logger.Info("Getting the number of employees, should be 25");
            Assert.AreEqual(25, peoplePage.GetTotalPeople());

        }






    }
}
