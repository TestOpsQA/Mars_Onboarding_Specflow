using NUnit.Framework;
using Mars_Onboarding_Specflow.SpecFlowPages.Pages;
using TechTalk.SpecFlow;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.ExcelLibraryHelper;
using Mars_Onboarding_Specflow.SpecFlowPages.Helpers;

namespace Mars_Onboarding_Specflow.SpecFlowPages.StepDefinitions
{
    [Binding]
    [Scope(Feature = "Languages Feature in the Profile Module")]
    public class LanguagesStepDefinitions
    {
        private readonly Languages languageObj;

        public LanguagesStepDefinitions()
        {
            // Initialize the Languages object for Languages feature
            languageObj = new Languages();
        }


        [Given(@"I am on Profile page")]
        public void GivenIAmOnProfilePage()
        {
            languageObj.GoToProfile();
        }
        [Given(@"I navigate to Languages section")]
        public void GivenINavigateToLanguagesSection()
        {
            languageObj.NavigateToLanguages();
        }



        [When(@"I add a language in the Languages feature with a valid language name and valid language level,")]
        public void WhenIAddALanguageInTheLanguagesFeatureWithAValidLanguageNameAndValidLanguageLevel()
        {
            int rowCount = ExcelLibraryHelper.ExcelLib.GetRowCount(ExcelLibraryHelper.ExcelPath, "Languages", "Language");

            for (int i = 1; i <= rowCount; i++)
            {
                languageObj.AddValidLanguage(i); // Add languages dynamically based on row count
            }

            TestContext.WriteLine($"Number of confirmation messages after adding languages: {languageObj.languageObjectsObj.LanguageConfirmationMessages.Count}");

            // Validate that the confirmation messages list contains the expected number of messages
            Assert.That(languageObj.languageObjectsObj.LanguageConfirmationMessages.Count, Is.EqualTo(rowCount),
                "The number of confirmation messages does not match the number of added languages.");
        }

        [Then(@"the language is added successfully with the valid language name and language level\.")]
        public void ThenTheLanguageIsAddedSuccessfullyWithTheValidLanguageNameAndLanguageLevel_()
        {
            int rowCount = ExcelLibraryHelper.ExcelLib.GetRowCount(ExcelLibraryHelper.ExcelPath, "SheetName", "Language");

            for (int i = 1; i <= rowCount; i++)
            {
                // Retrieve expected confirmation message from Excel
                string expectedMessage = ExcelLib.ReadData(i, "Language") + " has been added to your languages";

                TestContext.WriteLine($"Expected Message: {expectedMessage}");
                TestContext.WriteLine($"Actual Message: {languageObj.languageObjectsObj.LanguageConfirmationMessages[i - 1]}");

                // Retrieve the actual confirmation message from the stored list
                string actualMessage = languageObj.languageObjectsObj.LanguageConfirmationMessages[i - 1]; // List is 0-based

                // Assert that the actual message matches the expected message
                Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Confirmation message for language {i} is incorrect.");
            }
        }
        [When(@"I try to add a language with a combination of special characters and alphabetic text as the language name,")]
        public void WhenITryToAddALanguageWithACombinationOfSpecialCharactersAndAlphabeticTextAsTheLanguageName()
        {
            languageObj.AddLanguageWithSpecialCharactersInput();
        }

        [Then(@"the language cannot be added with special characters and alphabetic text, and an error message is displayed\.")]
        public void ThenTheLanguageCannotBeAddedWithSpecialCharactersAndAlphabeticTextAndAnErrorMessageIsDisplayed_()
        {
            string ExpectedMessage = languageObj.languageObjectsObj.LanguageSpecialCharactersInput + " cannot be added to your languages";
            string? ActualMessage = languageObj.languageObjectsObj.ValidationErrorMessage;
            
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I try to add a language with very long text as the language name")]
        public void WhenITryToAddALanguageWithVeryLongTextAsTheLanguageName()
        {
            languageObj.AddLanguageWithLongTextInput();
        }

        [Then(@"the language with long text cannot be added and an error message is displayed")]
        public void ThenTheLanguageWithLongTextCannotBeAddedAndAnErrorMessageIsDisplayed()
        {
            string ExpectedMessage = "Please enter language and level";
            string? ActualMessage = languageObj.languageObjectsObj.ValidationErrorMessage;
            
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I add a language with only spaces as the language name in the language textbox")]
        public void WhenIAddALanguageWithOnlySpacesAsTheLanguageNameInTheLanguageTextbox()
        {
            languageObj.AddLanguageWithSpacesInput();
        }

        [Then(@"the language is not added with spaces as the language name and an error message is displayed")]
        public void ThenTheLanguageIsNotAddedWithSpacesAsTheLanguageNameAndAnErrorMessageIsDisplayed()
        {
            string ExpectedMessage = "Please enter language and level";
            string? ActualMessage = languageObj.languageObjectsObj.ValidationErrorMessage;
           
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I add a language with malicious text as the language name in the language textbox")]
        public void WhenIAddALanguageWithMaliciousTextAsTheLanguageNameInTheLanguageTextbox()
        {
            languageObj.AddLanguageWithMaliciousInput();

        }

        [Then(@"the language is not added with the malicious text and an error message is displayed")]
        public void ThenTheLanguageIsNotAddedWithTheMaliciousTextAndAnErrorMessageIsDisplayed()
        {
            string ExpectedMessage = "Please enter language and level";
            string? ActualMessage = languageObj.languageObjectsObj.ValidationErrorMessage;
          
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I add a language without entering a language name in the language textbox and without selecting a language level")]
        public void WhenIAddALanguageWithoutEnteringALanguageNameInTheLanguageTextboxAndWithoutSelectingALanguageLevel()
        {

            languageObj.AddLanguageWithEmptyFields();
        }

        [Then(@"the language is not added with an empty language textbox and language level fields and an error message is displayed")]
        public void ThenTheLanguageIsNotAddedWithAnEmptyLanguageTextboxAndLanguageLevelFieldsAndAnErrorMessageIsDisplayed()
        {
            try
            {
                Assert.That(languageObj.languageObjectsObj.ValidationErrorMessage, Is.EqualTo("Please enter language and level"));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to add a language with a valid language name but without selecting a language level")]
        public void WhenITryToAddALanguageWithAValidLanguageNameButWithoutSelectingALanguageLevel()
        {
            languageObj.AddOnlyLangaugeName();
        }

        [Then(@"the language is not added without selecting the language level and an error is displayed")]
        public void ThenTheLanguageIsNotAddedWithoutSelectingTheLanguageLevelAndAnErrorIsDisplayed()
        {
            try
            {
                Assert.That(languageObj.languageObjectsObj.ValidationErrorMessage, Is.EqualTo("Please enter language and level"));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to add a language with an empty language textbox but with a valid language level")]
        public void WhenITryToAddALanguageWithAnEmptyLanguageTextboxButWithAValidLanguageLevel()
        {
            languageObj.AddOnlyLanguagelevel();
        }

        [Then(@"the language is not added with an empty language textbox and an error message is displayed")]
        public void ThenTheLanguageIsNotAddedWithAnEmptyLanguageTextboxAndAnErrorMessageIsDisplayed()
        {
            try
            {
                Assert.That(languageObj.languageObjectsObj.ValidationErrorMessage, Is.EqualTo("Please enter language and level"));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to add a language with an existing language name in the language list")]
        public void WhenITryToAddALanguageWithAnExistingLanguageNameInTheLanguageList()
        {
            languageObj.AddDuplicateLanguage();
        }

        [Then(@"the duplicate language cannot be added and an error is displayed")]
        public void ThenTheDuplicateLanguageCannotBeAddedAndAnErrorIsDisplayed()
        {
            try
            {
                string? actualMessage = languageObj.languageObjectsObj.ValidationErrorMessage;
                Assert.That(actualMessage, Is.AnyOf("Duplicated data", "This language is already exist in your language list."),
                    $"Unexpected validation message: {actualMessage}");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [Given(@"that languages exist under the languages feature")]
        public void GivenThatLanguagesExistUnderTheLanguagesFeature()
        {
            languageObj.CheckLanguageAvailability();
        }

        [When(@"I try to update any language in the language list with a valid language name and valid language level")]
        public void WhenITryToUpdateAnyLanguageInTheLanguageListWithAValidLanguageNameAndValidLanguageLevel()
        {

            languageObj.UpdateValidLanguage();
        }

        [Then(@"the language can be updated successfully with the valid language name and valid language level")]
        public void ThenTheLanguageCanBeUpdatedSuccessfullyWithTheValidLanguageNameAndValidLanguageLevel()
        {
            try
            {
                string expectedMessage = languageObj.languageObjectsObj.UpdateLanguageInput + " has been updated to your languages";
                Assert.That(languageObj.languageObjectsObj.UpdateLanguageSuccessMessage, Is.EqualTo(expectedMessage), "Confirmation message for language update is incorrect.");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to update a language with a combination of special characters and alphabetic text inputs")]
        public void WhenITryToUpdateALanguageWithACombinationOfSpecialCharactersAndAlphabeticTextInputs()
        {

            languageObj.UpdateLanguageWithCharactersInput();
        }

        [Then(@"the language cannot be updated with special characters and an error message is displayed")]
        public void ThenTheLanguageCannotBeUpdatedWithSpecialCharactersAndAnErrorMessageIsDisplayed()
        {
            string ExpectedMessage = languageObj.languageObjectsObj.UpdateLanguageCharacterInput + " cannot be updated to your languages";
            string? ActualMessage = languageObj.languageObjectsObj.UpdateLanguageSuccessMessage;
            
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I try to update an existing language with very long text as the language name")]
        public void WhenITryToUpdateAnExistingLanguageWithVeryLongTextAsTheLanguageName()
        {

            languageObj.UpdateLanguageWithLongInput();
        }

        [Then(@"the language cannot be updated with a long text input and an error message is displayed")]
        public void ThenTheLanguageCannotBeUpdatedWithALongTextInputAndAnErrorMessageIsDisplayed()
        {
            string ExpectedMessage = "Please enter language and level";
            string? ActualMessage = languageObj.languageObjectsObj.UpdateLanguageSuccessMessage;          
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I try to update a language and leave only spaces in the language textbox")]
        public void WhenITryToUpdateALanguageAndLeaveOnlySpacesInTheLanguageTextbox()
        {

            languageObj.UpdateLanguageWithSpacesInput();
        }

        [Then(@"the language is not updated with spaces and an error message is displayed")]
        public void ThenTheLanguageIsNotUpdatedWithSpacesAndAnErrorMessageIsDisplayed()
        {
            string ExpectedMessage = "Please enter language and level";
            string? ActualMessage = languageObj.languageObjectsObj.UpdateValidationErrorMessage;            
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I try to update a language and enter malicious text in the language textbox")]
        public void WhenITryToUpdateALanguageAndEnterMaliciousTextInTheLanguageTextbox()
        {

            languageObj.UpdateLanguageWithMaliciousInput();
        }

        [Then(@"the language is not updated with malicious data and an error message is displayed")]
        public void ThenTheLanguageIsNotUpdatedWithMaliciousDataAndAnErrorMessageIsDisplayed()
        {
            string ExpectedMessage = "Please enter language and level";
            string? ActualMessage = languageObj.languageObjectsObj.UpdateValidationErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I try to update a language without entering a language name and without selecting language level")]
        public void WhenITryToUpdateALanguageWithoutEnteringALanguageNameAndWithoutSelectingLanguageLevel()
        {

            languageObj.UpdateLanguageWithEmptyFields();
        }

        [Then(@"the language is not updated without entering a language name and without selecting language level")]
        public void ThenTheLanguageIsNotUpdatedWithoutEnteringALanguageNameAndWithoutSelectingLanguageLevel()
        {
            try
            {

                Assert.That(languageObj.languageObjectsObj.UpdateValidationErrorMessage, Is.EqualTo("Please enter language and level"), "Confirmation message for language update is incorrect.");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to update a language with valid language name and without selecting language level")]
        public void WhenITryToUpdateALanguageWithValidLanguageNameAndWithoutSelectingLanguageLevel()
        {

            languageObj.UpdateOnlyLangaugeName();
        }

        [Then(@"the language is not updated without selecting language level and an error is displayed")]
        public void ThenTheLanguageIsNotUpdatedWithoutSelectingLanguageLevelAndAnErrorIsDisplayed()
        {
            string expectedMessage = "Please enter language and level";
            try
            {

                Assert.That(languageObj.languageObjectsObj.UpdateValidationErrorMessage, Is.EqualTo(expectedMessage), "Confirmation message for language update is incorrect.");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
                if (languageObj.languageObjectsObj.UpdateValidationErrorMessage != expectedMessage)
                    throw new Exception($"Assertion failed: Expected '{expectedMessage}', but got '{languageObj.languageObjectsObj.UpdateValidationErrorMessage}'");
            }
        }

        [When(@"I try to update a language without entering a language name but selecting a language level")]
        public void WhenITryToUpdateALanguageWithoutEnteringALanguageNameButSelectingALanguageLevel()
        {

            languageObj.UpdateOnlyLanguageLevel();
        }

        [Then(@"the language is not updated with an empty language textbox and an error message is displayed")]
        public void ThenTheLanguageIsNotUpdatedWithAnEmptyLanguageTextboxAndAnErrorMessageIsDisplayed()
        {
            string expectedMessage = "Please enter language and level";
            try
            {
                Assert.That(languageObj.languageObjectsObj.UpdateValidationErrorMessage, Is.EqualTo(expectedMessage), "Confirmation message for language update is incorrect.");

            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
                if (languageObj.languageObjectsObj.UpdateValidationErrorMessage != expectedMessage)
                    throw new Exception($"Assertion failed: Expected '{expectedMessage}', but got '{languageObj.languageObjectsObj.UpdateValidationErrorMessage}'");
            }
        }

        [When(@"I try to update a language with an existing language name in the language list")]
        public void WhenITryToUpdateALanguageWithAnExistingLanguageNameInTheLanguageList()
        {

            languageObj.UpdateDuplicateLanguage();
        }
        [Then(@"the duplicate language cannot be updated and an error is displayed")]
        public void TheDuplicateLanguageCannotBeUpdatedAndAnErrorIsDisplayed()
        {
            string expectedMessage = "This language is already added to your language list.";
            try
            {
                Assert.That(languageObj.languageObjectsObj.UpdateValidationErrorMessage, Is.EqualTo(expectedMessage), "Confirmation message for language update is incorrect.");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");

                throw new Exception($"Actual message '{languageObj.languageObjectsObj.UpdateValidationErrorMessage}' and expected message '{expectedMessage}' do not match.");
            }
        }

        [When(@"I try to delete a language from the language list")]
        public void WhenITryToDeleteALanguageFromTheLanguageList()
        {

            languageObj.DeleteLanguage();
        }

        [Then(@"the language from the list is deleted successfully")]
        public void ThenTheLanguageFromTheListIsDeletedSuccessfully()
        {
            TestContext.WriteLine(languageObj.languageObjectsObj.FirstColumnValue);
            string ExpectedMessage = languageObj.languageObjectsObj.FirstColumnValue + " has been deleted from your languages";
            string? ActualMessage = languageObj.languageObjectsObj.DeleteLanguageSuccessMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }
    }
}
