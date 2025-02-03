using Mars_Onboarding_Specflow.SpecFlowPages.Pages;
using TechTalk.SpecFlow;

namespace Mars_Onboarding_Specflow.StepDefinitions
{
    [Binding]
    [Scope(Feature = "Login")]
    internal class LoginStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;
        //public Languages languagesObject;

        public LoginStepDefinitions(ScenarioContext scenarioContext)
        {
           // languagesObject = new Languages();
            _scenarioContext = scenarioContext;
        }
        [Given(@"I login to the website")]
        public void GivenILoginToTheWebsite()
        {
            var scenarioTitle = _scenarioContext.ScenarioInfo.Title;
        }

        [Given(@"I am on Profile page")]
        public void GivenIAmOnProfilePage()
        {
           // languagesObject.GoToProfile();
        }

    }
}

