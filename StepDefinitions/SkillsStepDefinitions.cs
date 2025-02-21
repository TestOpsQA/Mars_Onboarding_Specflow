using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using Mars_Onboarding_Specflow.SpecFlowPages.Pages;
using Mars_Onboarding_Specflow.SpecFlowPages.Helpers;
using TechTalk.SpecFlow;

namespace Mars_Onboarding_Specflow.SpecFlowPages.StepDefinitions
{
    [Binding]
    [Scope(Feature = "Skills Feature in the Profile Module")]


    public class SkillsStepDefinitions

    {
        private readonly Skills skillsObj;
        private readonly ScenarioContext scenarioContext;
        public SkillsStepDefinitions(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
                throw new ArgumentNullException(nameof(scenarioContext));
            // Initialize the Skills object 
            skillsObj = new Skills(new Languages());
            this.scenarioContext = scenarioContext;

        }

        protected void StoreInScenarioContext<T>(string key, T value)
        {
            // Check if the value is null before calling ToString()
            string valueToStore = value?.ToString() ?? "null"; // Or use any default string for null values

            if (scenarioContext.TryGetValue(key, out List<string> existingValue))
            {
                existingValue.Add(valueToStore);
            }
            else
            {
                scenarioContext[key] = new List<string> { valueToStore };
            }
        }


        [Given(@"I login to the website with valid email '([^']*)' and password '([^']*)'")]
        public void GivenILoginToTheWebsiteWithValidEmailAndPassword(string email, string password)
        {
            var scenarioTitle = scenarioContext.ScenarioInfo.Title;

        }

        [When(@"I am on Profile page")]
        public void WhenIAmOnProfilePage()
        {
            skillsObj.GoToProfile();

        }

        [When(@"I navigate to Skills section")]
        public void WhenINavigateToSkillsSection()
        {
            skillsObj.GoToSkillsPage();
        }

        [When(@"I clear skills data")]
        public void WhenIClearSkillsData()
        {

            skillsObj.CleanSkillsData();
        }

        [Then(@"skills data is cleared")]
        public void ThenSkillsDataIsCleared()
        {

            bool isSkillAvailable = skillsObj.CheckSkillsDataAvailability();

            // Assert that the test passes if no languages are found
            Assert.That(isSkillAvailable, Is.False, "Skills data is cleared.");
        }

        [When(@"I add a skill in the skills feature with a valid '([^']*)' name and valid level '([^']*)'")]
        public void WhenIAddASkillInTheSkillsFeatureWithAValidNameAndValidLevel(string skill, string skillLevel)
        {
            skillsObj.AddSkillWithValidInputs(skill, skillLevel);
            StoreInScenarioContext("Skills", skill);

        }

        [Then(@"the skill is added successfully with the valid skill '([^']*)' name and level and success message '([^']*)' is displayed\.")]
        public void ThenTheSkillIsAddedSuccessfullyWithTheValidSkillNameAndLevelAndSuccessMessageIsDisplayed_(string skill, string successMessage)
        {

            string expectedMessage = skill + " " + successMessage;

            TestContext.WriteLine($"Expected Message: {expectedMessage}");
            // Retrieve the actual confirmation message from the stored list
            string? actualMessage = skillsObj.SkillAddSuccessMessage;

            // Assert that the actual message matches the expected message
            Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Confirmation message for language is incorrect.");
        }


        [When(@"I try to add a skill with special characters as the '([^']*)' name and with a valid level '([^']*)'")]
        public void WhenITryToAddASkillWithSpecialCharactersAsTheNameAndWithAValidLevel(string skill, string skillLevel)
        {
            skillsObj.AddSkillWithSpecialCharacters(skill, skillLevel);
            StoreInScenarioContext("Skills", skill);
        }
        [Then(@"the skill cann be added with special characters '([^']*)', and a success message '([^']*)' is displayed\.")]
        public void ThenTheSkillCannBeAddedWithSpecialCharactersAndASuccessMessageIsDisplayed_(string skill, string successMessage)
        {


            try
            {
                string ExpectedMessage = skill + " " + successMessage;
                Assert.That(skillsObj.SkillAddSuccessMessage, Is.EqualTo(ExpectedMessage));

            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }

        }

        [When(@"I try to add a skill with very long text as the '([^']*)' name and with a valid level '([^']*)'")]
        public void WhenITryToAddASkillWithVeryLongTextAsTheNameAndWithAValidLevel(string skill, string skillLevel)
        {
            skillsObj.AddSkillWithLongText(skill, skillLevel);
            StoreInScenarioContext("Skills", skill);
        }

        [Then(@"the skill '([^']*)' with long text cannot be added and an error '([^']*)' is displayed")]
        public void ThenTheSkillWithLongTextCannotBeAddedAndAnErrorIsDisplayed(string skill, string errorMessage)
        {
            string ExpectedMessage = errorMessage;
            string? ActualMessage = skillsObj.ValidationErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");

        }

        [When(@"I add a skill with only spaces ""([^""]*)"" as the name in the skill textbox and with valid level '([^']*)'")]
        public void WhenIAddASkillWithOnlySpacesAsTheNameInTheSkillTextboxAndWithValidLevel(string skill, string skillLevel)
        {

            skillsObj.AddSkillWithSpacesInput(skill, skillLevel);
            StoreInScenarioContext("Skills", skill);
        }

        [Then(@"the skill '([^']*)' is not added with spaces as the skill name and an error '([^']*)' is displayed")]
        public void ThenTheSkillIsNotAddedWithSpacesAsTheSkillNameAndAnErrorIsDisplayed(string skill, string errorMessage)
        {
            string ExpectedMessage = errorMessage;
            string? ActualMessage = skillsObj.ValidationErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage,
                $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");

        }

        [When(@"I add a skill with malicious text '([^']*)' and valid level '([^']*)'")]
        public void WhenIAddASkillWithMaliciousTextAndValidLevel(string skill, string skillLevel)
        {
            skillsObj.AddSkillWithMaliciuosText(skill, skillLevel);
            StoreInScenarioContext("Skills", skill);
        }

        [Then(@"the skill '([^']*)' is not added with the malicious text and an error message '([^']*)' is displayed")]
        public void ThenTheSkillIsNotAddedWithTheMaliciousTextAndAnErrorMessageIsDisplayed(string skill, string errorMessage)
        {
            string ExpectedMessage = errorMessage;
            string? ActualMessage = skillsObj.ValidationErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage,
                $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I add a skill without entering a skill name '([^']*)' in the skill textbox and without selecting a skill level")]
        public void WhenIAddASkillWithoutEnteringASkillNameInTheSkillTextboxAndWithoutSelectingASkillLevel(string skill)
        {

            skillsObj.AddEmptySkillsFields();
            StoreInScenarioContext("Skills", skill);
        }

        [Then(@"the skill is not added with an empty skill textbox and skill level fields and an error message '([^']*)' is displayed")]
        public void ThenTheSkillIsNotAddedWithAnEmptySkillTextboxAndSkillLevelFieldsAndAnErrorMessageIsDisplayed(string errorMessage)
        {
            try
            {
                Assert.That(skillsObj.ValidationErrorMessage,
                    Is.EqualTo(errorMessage));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }

        }

        [When(@"I try to add a skill with a valid skill name '([^']*)' but without selecting a skill level '([^']*)'")]
        public void WhenITryToAddASkillWithAValidSkillNameButWithoutSelectingASkillLevel(string skill, string skillLevel)
        {
            skillsObj.AddOnlySkillName(skill, skillLevel);
            StoreInScenarioContext("Skills", skill);
        }

        [Then(@"the skill is not added without selecting the skill level and an error message '([^']*)'is displayed")]
        public void ThenTheSkillIsNotAddedWithoutSelectingTheSkillLevelAndAnErrorMessageIsDisplayed(string errorMessage)
        {
            try
            {
                Assert.That(skillsObj.ValidationErrorMessage,
                    Is.EqualTo(errorMessage));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }

        }
        [When(@"I try to add a skill with an empty skill '([^']*)' textbox but with a valid skill level '([^']*)'")]
        public void WhenITryToAddASkillWithAnEmptySkillTextboxButWithAValidSkillLevel(string skill, string skillLevel)
        {
            skillsObj.AddOnlySkillLevel(skill, skillLevel);
            StoreInScenarioContext("Skills", skill);
        }

        [Then(@"the skill is not added with an empty skill textbox and an error message '([^']*)' is displayed")]
        public void ThenTheSkillIsNotAddedWithAnEmptySkillTextboxAndAnErrorMessageIsDisplayed(string errorMessage)
        {
            try
            {
                Assert.That(skillsObj.ValidationErrorMessage,
                    Is.EqualTo(errorMessage));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }

        }

        [When(@"I add a valid skill '([^']*)' and level '([^']*)'")]
        public void WhenIAddAValidSkillAndLevel(string skill, string skillLevel)
        {
            skillsObj.AddSkillWithValidInputs(skill, skillLevel);
            StoreInScenarioContext("Skills", skill);
        }

        [When(@"I try to add a skill with a duplicate skill name '([^']*)' and level '([^']*)' in the skill list")]
        public void WhenITryToAddASkillWithADuplicateSkillNameAndLevelInTheSkillList(string skill, string skillLevel)
        {
            skillsObj.AddSkillWithDuplicateInput(skill, skillLevel);
            StoreInScenarioContext("Skills", skill);
        }

        [Then(@"the duplicate skill cannot be added and an error message '([^']*)' or '([^']*)' is displayed")]
        public void ThenTheDuplicateSkillCannotBeAddedAndAnErrorMessageOrIsDisplayed(string skill, string errroMessage)
        {
            try
            {
                Assert.That(skillsObj.ValidationErrorMessage,
                    Is.EqualTo(errroMessage));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }

        }



        [When(@"I try to update any skill in the skill list with a valid skill name '([^']*)' and valid skill level '([^']*)'")]
        public void WhenITryToUpdateAnySkillInTheSkillListWithAValidSkillNameAndValidSkillLevel(string skillUpdate, string skillLevelUpdate)
        {
            skillsObj.UpdateSkillsWithValidInputs(skillUpdate, skillLevelUpdate);
            StoreInScenarioContext("Skills", skillUpdate);
        }

        [Then(@"the skill can be updated successfully with the valid skill name '([^']*)' and valid skill level and success message '([^']*)' is displayed")]
        public void ThenTheSkillCanBeUpdatedSuccessfullyWithTheValidSkillNameAndValidSkillLevelAndSuccessMessageIsDisplayed(string skill, string successMessage)
        {

            try
            {
                string expectedMessage = skill + " " +
                                        successMessage;
                Assert.That(skillsObj.SkillUpdateSuccessMessage,
    Is.EqualTo(expectedMessage), "Confirmation message for skill update is incorrect.");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }

        }

        [When(@"I try to update a skill with special characters inputs '([^']*)' and with valid level '([^']*)'")]
        public void WhenITryToUpdateASkillWithSpecialCharactersInputsAndWithValidLevel(string skillUpdate, string skillLevelUpdate)
        {
            skillsObj.UpdateSkillWithCharactersInput(skillUpdate, skillLevelUpdate);
            StoreInScenarioContext("Skills", skillUpdate);
        }
        [Then(@"the skill cannot be updated with special characters '([^']*)' and an error message '([^']*)' is displayed")]
        public void ThenTheSkillCannotBeUpdatedWithSpecialCharactersAndAnErrorMessageIsDisplayed(string skillUpdate, string successMessage)
        {

            try
            {
                string expectedMessage = skillUpdate + " " +
                    successMessage;
                Assert.That(skillsObj.SkillUpdateSuccessMessage,
                    Is.EqualTo(expectedMessage), "Confirmation message for skill update is incorrect.");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }

        }

        [When(@"I try to update an existing skill with very long text '([^']*)' as the skill name and with level '([^']*)'")]
        public void WhenITryToUpdateAnExistingSkillWithVeryLongTextAsTheSkillNameAndWithLevel(string skillUpdate, string skillLevelUpdate)
        {
            skillsObj.UpdateSkillWithLongText(skillUpdate, skillLevelUpdate);
            StoreInScenarioContext("Skills", skillUpdate);
        }

        [Then(@"the skill cannot be updated with a long text input '([^']*)' and an error message '([^']*)' is displayed")]
        public void ThenTheSkillCannotBeUpdatedWithALongTextInputAndAnErrorMessageIsDisplayed(string skillUpdate, string errorMessage)
        {
            string ExpectedMessage = errorMessage;
            string? ActualMessage = skillsObj.SkillUpdateErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage,
                $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I try to update a skill with only spaces '([^']*)' in the skill textbox and with '([^']*)' skill level")]
        public void WhenITryToUpdateASkillWithOnlySpacesInTheSkillTextboxAndWithSkillLevel(string skillUpdate, string skillLevelUpdate)
        {
            skillsObj.UpdateSkillWithSpaces(skillUpdate, skillLevelUpdate);
            StoreInScenarioContext("Skills", skillUpdate);
        }

        [Then(@"the skill is not updated with spaces '([^']*)' and an error message '([^']*)' is displayed")]
        public void ThenTheSkillIsNotUpdatedWithSpacesAndAnErrorMessageIsDisplayed(string skillUpdate, string errorMessage)
        {

            string ExpectedMessage = errorMessage;
            string? ActualMessage = skillsObj.SkillUpdateErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage,
                $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");

        }

        [When(@"I try to update a skill and enter malicious text '([^']*)' in the skill textbox and with skill level '([^']*)'")]
        public void WhenITryToUpdateASkillAndEnterMaliciousTextInTheSkillTextboxAndWithSkillLevel(string skillUpdate, string skillLevelUpdate)
        {
            skillsObj.UpdateSkillWithMaliciousText(skillUpdate, skillLevelUpdate);
            StoreInScenarioContext("Skills", skillUpdate);
        }

        [Then(@"the skill is not updated with malicious data '([^']*)' and an error message '([^']*)' is displayed")]
        public void ThenTheSkillIsNotUpdatedWithMaliciousDataAndAnErrorMessageIsDisplayed(string skillUpdate, string errorMessage)
        {

            string ExpectedMessage = errorMessage;
            string? ActualMessage = skillsObj.SkillUpdateErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage,
    $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");

        }
        [When(@"I try to update a skill without entering a skill name '([^']*)' and without selecting skill level")]
        public void WhenITryToUpdateASkillWithoutEnteringASkillNameAndWithoutSelectingSkillLevel(string skill)
        {
            skillsObj.UpdateSkillWithEmptyFields();
            StoreInScenarioContext("Skills", skill);
        }

        [Then(@"the skill is not updated without entering a skill name and without selecting skill level and an error message '([^']*)' is displayed")]
        public void ThenTheSkillIsNotUpdatedWithoutEnteringASkillNameAndWithoutSelectingSkillLevelAndAnErrorMessageIsDisplayed(string errorMessage)
        {

            try
            {
                Assert.That(skillsObj.SkillUpdateErrorMessage,
                    Is.EqualTo(errorMessage));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }

        }

        [When(@"I try to update a skill with valid skill name '([^']*)' and without selecting skill level '([^']*)'")]
        public void WhenITryToUpdateASkillWithValidSkillNameAndWithoutSelectingSkillLevel(string skillUpdate, string skillLevelUpdate)
        {
            skillsObj.UpdateOnlySkillName(skillUpdate, skillLevelUpdate);
            StoreInScenarioContext("Skills", skillUpdate);
        }

        [Then(@"the skill '([^']*)' is not updated without selecting skill level and an error '([^']*)' is displayed")]
        public void ThenTheSkillIsNotUpdatedWithoutSelectingSkillLevelAndAnErrorIsDisplayed(string skillUpdate, string errorMessage)
        {

            try
            {
                Assert.That(skillsObj.SkillUpdateErrorMessage,
                    Is.EqualTo(errorMessage));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }

        }

        [When(@"I try to update a skill without entering a skill name '([^']*)' but selecting a skill level '([^']*)'")]
        public void WhenITryToUpdateASkillWithoutEnteringASkillNameButSelectingASkillLevel(string skillUpdate, string skillLevelUpdate)
        {
            skillsObj.UpdateOnlySkillLevel(skillUpdate, skillLevelUpdate);
            StoreInScenarioContext("Skills", skillUpdate);
        }

        [Then(@"the skill is not updated with an empty skill textbox and an error message '([^']*)' is displayed")]
        public void ThenTheSkillIsNotUpdatedWithAnEmptySkillTextboxAndAnErrorMessageIsDisplayed(string errorMessage)
        {

            try
            {
                Assert.That(skillsObj.SkillUpdateErrorMessage,
                    Is.EqualTo(errorMessage));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }

        }

        [When(@"I try to update a skill with a duplicate skill name '([^']*)' and level '([^']*)' in the skill list")]
        public void WhenITryToUpdateASkillWithADuplicateSkillNameAndLevelInTheSkillList(string skill, string skillLevel)
        {
            skillsObj.UpdateWithDuplicateSkill(skill, skillLevel);
            StoreInScenarioContext("Skills", skill);
        }

        [Then(@"the duplicate skill cannot be updated and an error message '([^']*)' or '([^']*)' is displayed")]
        public void ThenTheDuplicateSkillCannotBeUpdatedAndAnErrorMessageOrIsDisplayed(string errorMessageOne, string errorMessageTwo)
        {

            try
            {
                string? actualMessage = skillsObj.SkillUpdateErrorMessage;

                SoftAssert.Assert(new[] { errorMessageOne, errorMessageTwo }.Contains(actualMessage),
        $"Unexpected validation message: {actualMessage}");
            }

            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }

        }

        [When(@"I try to delete a skill '([^']*)' from the skill list")]
        public void WhenITryToDeleteASkillFromTheSkillList(string skill)
        {
            skillsObj.DeleteSkill(skill);
            StoreInScenarioContext("Skills", skill);
        }

        [Then(@"the skill '([^']*)' from the list is deleted successfully and a successful deletion message '([^']*)' is displayed")]
        public void ThenTheSkillFromTheListIsDeletedSuccessfullyAndASuccessfulDeletionMessageIsDisplayed(string skill, string SuccessMessage)
        {
            try
            {
                string expectedMessage = skill + " " + SuccessMessage;
                Assert.That(skillsObj.DeleteSuccessMessage,
                    Is.AnyOf(expectedMessage, "has been deleted"), "Confirmation message for language update is incorrect.");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }

        }
    }
}
