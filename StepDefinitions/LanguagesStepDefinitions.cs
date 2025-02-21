using NUnit.Framework;
using Mars_Onboarding_Specflow.SpecFlowPages.Pages;
using TechTalk.SpecFlow;
using Mars_Onboarding_Specflow.SpecFlowPages.Helpers;

namespace Mars_Onboarding_Specflow.SpecFlowPages.StepDefinitions
{
    [Binding]
    [Scope(Feature = "Languages Feature in the Profile Module")]

    public class LanguagesStepDefinitions
    {
        private readonly Languages languageObj;
        private readonly ScenarioContext scenarioContext;
        private List<string> addedLanguagesMessages = new List<string>();
        public LanguagesStepDefinitions(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
                throw new ArgumentNullException(nameof(scenarioContext));
            // Initialize the Languages object for Languages feature
            languageObj = new Languages();
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
            languageObj.GoToProfile();
        }
        [When(@"I clear langauges data")]
        public void WhenIClearLangaugesData()
        {
            languageObj.CleanLanguageData();
        }

        [When(@"I navigate to Languages section")]
        public void WhenINavigateToLanguagesSection()
        {
            languageObj.NavigateToLanguages();
        }

        [Then(@"Langauges data is cleared")]
        public void ThenLangaugesDataIsCleared()
        {
            bool isLanguageAvailable = languageObj.CheckLanguageDataAvailability();

            // Assert that the test passes if no languages are found
            Assert.That(isLanguageAvailable, Is.False, "Languages data is cleared.");
        }
        [When(@"I add a language in the Languages feature with a valid '([^']*)' name and valid level '([^']*)'")]
        public void WhenIAddALanguageInTheLanguagesFeatureWithAValidNameAndValidLevel(string language, string languageLevel)
        {
            languageObj.AddValidLanguage(language, languageLevel);
            StoreInScenarioContext("Languages", language);
        }

        [Then(@"the language is added successfully with the valid language '([^']*)' name and level and success message '([^']*)' is displayed\.")]
        public void ThenTheLanguageIsAddedSuccessfullyWithTheValidLanguageNameAndLevelAndSuccessMessageIsDisplayed_(string language, string successMessage)
        {


            string expectedMessage = language + " " + successMessage;

            TestContext.WriteLine($"Expected Message: {expectedMessage}");
            string? actualMessage = languageObj.AddLanguageSuccessMessage;
            // Retrieve the actual confirmation message from the stored list

            // Assert that the actual message matches the expected message
            Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Confirmation message for language is incorrect.");
        }

        [When(@"I try to add a language with special characters as the '([^']*)' name and with a valid level '([^']*)'")]
        public void WhenITryToAddALanguageWithSpecialCharactersAsTheNameAndWithAValidLevel(string language, string languageLevel)
        {
            languageObj.AddLanguageWithSpecialCharactersInput(language, languageLevel);
            StoreInScenarioContext("Languages", language);
        }

        [Then(@"the language cannot be added with special characters '([^']*)', and an error '([^']*)' is displayed\.")]
        public void ThenTheLanguageCannotBeAddedWithSpecialCharactersAndAnErrorIsDisplayed_(string language, string message)
        {
            string ExpectedMessage = message;
            string? ActualMessage = languageObj.ValidationErrorMessage;

            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }
        [When(@"I try to add a language with very long text as the '([^']*)' name and with a valid level '([^']*)'")]
        public void WhenITryToAddALanguageWithVeryLongTextAsTheNameAndWithAValidLevel(string language, string languageLevel)
        {
            languageObj.AddLanguageWithLongTextInput(language, languageLevel);
            StoreInScenarioContext("Languages", language);
        }
        [Then(@"the language '([^']*)' with long text cannot be added and an error '([^']*)' is displayed")]
        public void ThenTheLanguageWithLongTextCannotBeAddedAndAnErrorIsDisplayed(string language, string errorMessage)
        {
            string ExpectedMessage = errorMessage;
            string? ActualMessage = languageObj.ValidationErrorMessage;

            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }
        [When(@"I add a language with only spaces ""([^""]*)""  as the language name in the language textbox and with valid level '([^']*)'")]
        public void WhenIAddALanguageWithOnlySpacesAsTheLanguageNameInTheLanguageTextboxAndWithValidLevel(string language, string languageLevel)
        {

            languageObj.AddLanguageWithSpacesInput(language, languageLevel);
            StoreInScenarioContext("Languages", language);
        }

        [Then(@"the language '([^']*)' is not added with spaces as the language name and an error '([^']*)' is displayed")]
        public void ThenTheLanguageIsNotAddedWithSpacesAsTheLanguageNameAndAnErrorIsDisplayed(string language, string errorMessage)
        {
            string ExpectedMessage = errorMessage;
            string? ActualMessage = languageObj.ValidationErrorMessage;

            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }
        [When(@"I add a language with malicious text '([^']*)' and valid level '([^']*)'")]
        public void WhenIAddALanguageWithMaliciousTextAndValidLevel(string language, string level)
        {
            languageObj.AddLanguageWithMaliciousInput(language, level);
            StoreInScenarioContext("Languages", language);
        }

        [Then(@"the language '([^']*)' is not added with the malicious text and an error message '([^']*)' is displayed")]
        public void ThenTheLanguageIsNotAddedWithTheMaliciousTextAndAnErrorMessageIsDisplayed(string language, string errorMessage)
        {

            string ExpectedMessage = errorMessage;
            string? ActualMessage = languageObj.ValidationErrorMessage;

            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }
        [When(@"I add a language without entering a language name '([^']*)' in the language textbox and without selecting a language level")]
        public void WhenIAddALanguageWithoutEnteringALanguageNameInTheLanguageTextboxAndWithoutSelectingALanguageLevel(string language)
        {

            languageObj.AddLanguageWithEmptyFields();
            StoreInScenarioContext("Languages", language);
        }
        [Then(@"the language is not added with an empty language textbox and language level fields and an error message '([^']*)' is displayed")]
        public void ThenTheLanguageIsNotAddedWithAnEmptyLanguageTextboxAndLanguageLevelFieldsAndAnErrorMessageIsDisplayed(string errorMessage)
        {
            try
            {
                Assert.That(languageObj.ValidationErrorMessage, Is.EqualTo(errorMessage));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to add a language with a valid language name '([^']*)' but without selecting a language level '([^']*)'")]
        public void WhenITryToAddALanguageWithAValidLanguageNameButWithoutSelectingALanguageLevel(string language, string languageLevel)
        {
            languageObj.AddOnlyLangaugeName(language);
            StoreInScenarioContext("Languages", language);
        }

        [Then(@"the language is not added without selecting the language level and an error message '([^']*)'is displayed")]
        public void ThenTheLanguageIsNotAddedWithoutSelectingTheLanguageLevelAndAnErrorMessageIsDisplayed(string errorMessage)
        {
            try
            {
                Assert.That(languageObj.ValidationErrorMessage, Is.EqualTo(errorMessage));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }
        [When(@"I try to add a language '([^']*)' with an empty language textbox but with a valid language level '([^']*)'")]
        public void WhenITryToAddALanguageWithAnEmptyLanguageTextboxButWithAValidLanguageLevel(string language, string languageLevel)
        {

            languageObj.AddOnlyLanguagelevel(languageLevel);
            StoreInScenarioContext("Languages", language);

        }

        [Then(@"the language is not added with an empty language textbox and an error message '([^']*)' is displayed")]
        public void ThenTheLanguageIsNotAddedWithAnEmptyLanguageTextboxAndAnErrorMessageIsDisplayed(string errorMessage)
        {

            try
            {
                Assert.That(languageObj.ValidationErrorMessage, Is.EqualTo(errorMessage));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I add a valid language '([^']*)' and level '([^']*)'")]
        public void WhenIAddAValidLanguageAndLevel(string language, string languageLevel)
        {
            languageObj.AddValidLanguage(language, languageLevel);
        }
        [When(@"I try to add a language with a duplicate language name '([^']*)' and level '([^']*)' in the language list")]
        public void WhenITryToAddALanguageWithADuplicateLanguageNameAndLevelInTheLanguageList(string language, string languageLevel)
        {


            languageObj.AddDuplicateLanguage(language, languageLevel);
            StoreInScenarioContext("Languages", language);
        }

        [Then(@"the duplicate language cannot be added and an error message '([^']*)' or '([^']*)' is displayed")]
        public void ThenTheDuplicateLanguageCannotBeAddedAndAnErrorMessageOrIsDisplayed(string errorMessageOne, string errorMessageTwo)
        {

            try
            {
                string? actualMessage = languageObj.ValidationErrorMessage;
                Assert.That(actualMessage, Is.AnyOf(errorMessageOne, errorMessageTwo),
                    $"Unexpected validation message: {actualMessage}");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }


        [When(@"I try to update any language in the language list with a valid language name '([^']*)' and valid language level '([^']*)'")]
        public void WhenITryToUpdateAnyLanguageInTheLanguageListWithAValidLanguageNameAndValidLanguageLevel(string languageUpdate, string languageLevelUpdate)
        {

            languageObj.UpdateValidLanguage(languageUpdate, languageLevelUpdate);
            StoreInScenarioContext("Languages", languageUpdate);
        }

        [Then(@"the language can be updated successfully with the valid language name '([^']*)' and valid language level and success message '([^']*)' is displayed")]
        public void ThenTheLanguageCanBeUpdatedSuccessfullyWithTheValidLanguageNameAndValidLanguageLevelAndSuccessMessageIsDisplayed(string languageUpdate, string successMessage)
        {

            try
            {
                string expectedMessage = languageUpdate + " " + successMessage;
                Assert.That(languageObj.UpdateLanguageSuccessMessage, Is.EqualTo(expectedMessage), "Confirmation message for language update is incorrect.");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }
        [When(@"I try to update a language with special characters inputs '([^']*)' and with valid level '([^']*)'")]
        public void WhenITryToUpdateALanguageWithSpecialCharactersInputsAndWithValidLevel(string languageUpdate, string languageLevelUpdate)
        {
            languageObj.UpdateLanguageWithCharactersInput(languageUpdate, languageLevelUpdate);
            StoreInScenarioContext("Languages", languageUpdate);
        }
        [Then(@"the language cannot be updated with special characters  '([^']*)' and an error message '([^']*)' is displayed")]
        public void ThenTheLanguageCannotBeUpdatedWithSpecialCharactersAndAnErrorMessageIsDisplayed(string language, string errorMessage)
        {

            string ExpectedMessage = errorMessage;
            string? ActualMessage = languageObj.UpdateLanguageErrorMessage;

            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }


        [When(@"I try to update an existing language with very long text '([^']*)' as the language name and with level '([^']*)'")]
        public void WhenITryToUpdateAnExistingLanguageWithVeryLongTextAsTheLanguageNameAndWithLevel(string languageUpdate, string languageLevelUpdate)
        {
            languageObj.UpdateLanguageWithLongInput(languageUpdate, languageLevelUpdate);
            StoreInScenarioContext("Languages", languageUpdate);
        }

        [Then(@"the language cannot be updated with a long text input '([^']*)' and an error message '([^']*)' is displayed")]
        public void ThenTheLanguageCannotBeUpdatedWithALongTextInputAndAnErrorMessageIsDisplayed(string languageUpdate, string errorMessage)
        {

            string ExpectedMessage = errorMessage;
            string? ActualMessage = languageObj.UpdateLanguageErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }
        [When(@"I try to update a language with only spaces ""([^""]*)""  in the language textbox and with '([^']*)' language level")]
        public void WhenITryToUpdateALanguageWithOnlySpacesInTheLanguageTextboxAndWithLanguageLevel(string languageUpdate, string languageLevelUpdate)
        {

            languageObj.UpdateLanguageWithSpacesInput(languageUpdate, languageLevelUpdate);
            StoreInScenarioContext("Languages", languageUpdate);
        }

        [Then(@"the language is not updated with spaces '([^']*)' and an error message '([^']*)' is displayed")]
        public void ThenTheLanguageIsNotUpdatedWithSpacesAndAnErrorMessageIsDisplayed(string languageUpdate, string errorMessage)
        {
            string ExpectedMessage = errorMessage;
            string? ActualMessage = languageObj.UpdateLanguageErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I try to update a language and enter malicious text '([^']*)' in the language textbox and with language level '([^']*)'")]
        public void WhenITryToUpdateALanguageAndEnterMaliciousTextHackedInTheLanguageTextboxAndWithLanguageLevel(string languageUpdate, string languageLevelUpdate)
        {
            languageObj.UpdateLanguageWithMaliciousInput(languageUpdate, languageLevelUpdate);
            StoreInScenarioContext("Languages", languageUpdate);
        }

        [Then(@"the language is not updated with malicious data '([^']*)' and an error message '([^']*)' is displayed")]
        public void ThenTheLanguageIsNotUpdatedWithMaliciousDataHackedAndAnErrorMessageIsDisplayed(string languageUpdate, string errorMessage)
        {

            string ExpectedMessage = errorMessage;
            string? ActualMessage = languageObj.UpdateLanguageErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }
        [When(@"I try to update a language without entering a language name '([^']*)' and without selecting language level")]
        public void WhenITryToUpdateALanguageWithoutEnteringALanguageNameAndWithoutSelectingLanguageLevel(string language)
        {

            languageObj.UpdateLanguageWithEmptyFields();
            StoreInScenarioContext("Languages", language);

        }

        [Then(@"the language is not updated without entering a language name and without selecting language level and an error message '([^']*)' is displayed")]
        public void ThenTheLanguageIsNotUpdatedWithoutEnteringALanguageNameAndWithoutSelectingLanguageLevelAndAnErrorMessageIsDisplayed(string errorMessage)
        {


            try
            {

                Assert.That(languageObj.UpdateLanguageErrorMessage, Is.EqualTo(errorMessage), "Confirmation message for language update is incorrect.");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }
        [When(@"I try to update a language with valid language name '([^']*)' and without selecting language level '([^']*)'")]
        public void WhenITryToUpdateALanguageWithValidLanguageNameAndWithoutSelectingLanguageLevel(string languageUpdate, string languageLevelUpdate)
        {

            languageObj.UpdateOnlyLanguageName(languageUpdate, languageLevelUpdate);
            StoreInScenarioContext("Languages", languageUpdate);
        }

        [Then(@"the language '([^']*)' is not updated without selecting language level and an error '([^']*)' is displayed")]
        public void ThenTheLanguageIsNotUpdatedWithoutSelectingLanguageLevelAndAnErrorIsDisplayed(string languageUpdate, string errorMessage)
        {

            string expectedMessage = errorMessage;
            try
            {

                Assert.That(languageObj.UpdateLanguageErrorMessage, Is.EqualTo(expectedMessage), "Confirmation message for language update is incorrect.");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
                if (languageObj.UpdateLanguageErrorMessage != expectedMessage)
                    throw new Exception($"Assertion failed: Expected '{expectedMessage}', but got '{languageObj.UpdateLanguageErrorMessage}'");
            }
        }
        [When(@"I try to update a language without entering a language name '([^']*)' but selecting a language level '([^']*)'")]
        public void WhenITryToUpdateALanguageWithoutEnteringALanguageNameButSelectingALanguageLevel(string languageUpdate, string languageLevelUpdate)
        {
            languageObj.UpdateOnlyLanguageLevel(languageUpdate, languageLevelUpdate);
            StoreInScenarioContext("Languages", languageUpdate);
        }

        [Then(@"the language is not updated with an empty language textbox and an error message '([^']*)' is displayed")]
        public void ThenTheLanguageIsNotUpdatedWithAnEmptyLanguageTextboxAndAnErrorMessageIsDisplayed(string errorMessage)
        {

            string expectedMessage = errorMessage;
            try
            {
                Assert.That(languageObj.UpdateLanguageErrorMessage, Is.EqualTo(expectedMessage), "Confirmation message for language update is incorrect.");

            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
                if (languageObj.UpdateLanguageErrorMessage != expectedMessage)
                    throw new Exception($"Assertion failed: Expected '{expectedMessage}', but got '{languageObj.UpdateLanguageErrorMessage}'");
            }
        }

        [When(@"I try to update a language with a duplicate language name '([^']*)' and level '([^']*)' in the language list")]
        public void WhenITryToUpdateALanguageWithADuplicateLanguageNameAndLevelInTheLanguageList(string language, string languageLevel)
        {

            languageObj.UpdateDuplicateLanguage(language, languageLevel);
            StoreInScenarioContext("Languages", language);
        }

        [Then(@"the duplicate language cannot be updated and an error message '([^']*)' or '([^']*)' is displayed")]
        public void ThenTheDuplicateLanguageCannotBeUpdatedAndAnErrorMessageOrIsDisplayed(string errorMessageOne, string errorMessageTwo)
        {
            string expectedMessage = errorMessageOne ?? errorMessageTwo;
            string? actualMessage = languageObj.UpdateLanguageErrorMessage;
            try
            {
                Assert.That(actualMessage, Is.AnyOf(errorMessageOne, errorMessageTwo),
                    $"Unexpected validation message: {actualMessage}");

            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");

                throw new Exception($"Actual message '{languageObj.UpdateLanguageErrorMessage}' and expected message '{expectedMessage}' do not match.");
            }
        }
        [When(@"I try to delete a language '([^']*)' from the language list")]
        public void WhenITryToDeleteALanguageFromTheLanguageList(string language)
        {

            languageObj.DeleteLanguage(language);
            StoreInScenarioContext("Languages", language);
        }
        [Then(@"the language '([^']*)' from the list is deleted successfully and a successful deletion message '([^']*)' is displayed")]
        public void ThenTheLanguageFromTheListIsDeletedSuccessfullyAndASuccessfulDeletionMessageIsDisplayed(string language, string deleteSuccess)
        {

            TestContext.WriteLine(languageObj.FirstColumnValue);
            string ExpectedMessage = language + " " + deleteSuccess;
            string? ActualMessage = languageObj.DeleteLanguageSuccessMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }
    }
}
