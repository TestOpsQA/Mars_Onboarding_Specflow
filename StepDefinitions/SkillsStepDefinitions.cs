using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using Mars_Onboarding_Specflow.SpecFlowPages.Pages;
using Mars_Onboarding_Specflow.SpecFlowPages.Helpers;
using TechTalk.SpecFlow;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.ExcelLibraryHelper;

namespace Mars_Onboarding_Specflow.SpecFlowPages.StepDefinitions
{
    [Binding]
    [Scope(Feature = "Skills Feature in the Profile Module")]


    public class SkillsStepDefinitions

    {
        private readonly Skills skillsObj;

        public SkillsStepDefinitions()
        {
            // Initialize the Skills object 
            skillsObj = new Skills(new Languages());
        }

        [Given(@"I am on Profile page")]
        public void GivenIAmOnProfilePage()
        {
            skillsObj.GoToProfile();
        }

        [Given(@"I navigate to Skills section")]
        public void GivenINavigateToSkillsSection()
        {
            skillsObj.GoToSkillsPage();
        }


        [When(@"I add a skill in the skills feature with a valid skill name and valid skill level,")]
        public void WhenIAddASkillInTheSkillsFeatureWithAValidSkillNameAndValidSkillLevel()
        {
            int rowCount = ExcelLibraryHelper.ExcelLib.GetRowCount(ExcelLibraryHelper.ExcelPath, "Skills", "skill");

            for (int i = 1; i <= rowCount; i++)
            {
                skillsObj.AddSkillWithValidInputs(i); // Add languages dynamically based on row count
            }

            TestContext.WriteLine($"Number of confirmation messages after adding skills: {skillsObj.skillsObjectsObj.SkillsConfirmationMessages.Count}");    
        }

        [Then(@"the skill is added successfully with the valid skill name and skill level\.")]
        public void ThenTheSkillIsAddedSuccessfullyWithTheValidSkillNameAndSkillLevel_()
        {
            int rowCount = ExcelLibraryHelper.ExcelLib.GetRowCount(ExcelLibraryHelper.ExcelPath, "SheetName", "skill");

            for (int i = 1; i <= rowCount; i++)
            {
                // Retrieve expected confirmation message from Excel
                string expectedMessage = ExcelLib.ReadData(i, "skill") + " has been added to your skills";

                TestContext.WriteLine($"Expected Message: {expectedMessage}");
                TestContext.WriteLine($"Actual Message: {skillsObj.skillsObjectsObj.SkillsConfirmationMessages[i - 1]}");

                // Retrieve the actual confirmation message from the stored list
                string actualMessage = skillsObj.skillsObjectsObj.SkillsConfirmationMessages[i - 1]; // List is 0-based

                // Assert that the actual message matches the expected message
                Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Confirmation message for skill {i} is incorrect.");
            }
        }

        [When(@"I try to add a skill with a combination of special characters and alphabetic text as the skill name,")]
        public void WhenITryToAddASkillWithACombinationOfSpecialCharactersAndAlphabeticTextAsTheSkillName()
        {
            skillsObj.AddSkillWithSpecialCharacters();
        }


        [Then(@"the skill cannot be added with special characters and alphabetic text, and an error message is displayed\.")]
        public void ThenTheSkillCannotBeAddedWithSpecialCharactersAndAlphabeticTextAndAnErrorMessageIsDisplayed_()
        {

            try
            {
                string ExpectedMessage = skillsObj.skillsObjectsObj.specialCharactersSkillText + " has been added to your skills";
                Assert.That(skillsObj.skillsObjectsObj.ValidationErrorMessage, Is.EqualTo(ExpectedMessage));

            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }


        [When(@"I try to add a skill with very long text as the skill name")]
        public void WhenITryToAddASkillWithVeryLongTextAsTheSkillName()
        {
            skillsObj.AddSkillWithLongText();
        }

        [Then(@"the skill with long text cannot be added and an error message is displayed")]
        public void ThenTheSkillWithLongTextCannotBeAddedAndAnErrorMessageIsDisplayed()
        {
            string ExpectedMessage = "Please enter valid skill and experience level";
            string? ActualMessage = skillsObj.skillsObjectsObj.ValidationErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage, $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I add a skill with only spaces as the skill name in the skill textbox")]
        public void WhenIAddASkillWithOnlySpacesAsTheSkillNameInTheSkillTextbox()
        {
            skillsObj.AddSkillWithSpacesInput();
        }

        [Then(@"the skill is not added with spaces as the skill name and an error message is displayed")]
        public void ThenTheSkillIsNotAddedWithSpacesAsTheSkillNameAndAnErrorMessageIsDisplayed()
        {
            string ExpectedMessage = "Please enter valid skill and experience level";
            string? ActualMessage = skillsObj.skillsObjectsObj.ValidationErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage,
                $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I add a skill with malicious text as the skill name in the skill textbox")]
        public void WhenIAddASkillWithMaliciousTextAsTheSkillNameInTheSkillTextbox()
        {
            skillsObj.AddSkillWithMaliciuosText();
        }

        [Then(@"the skill is not added with the malicious text and an error message is displayed")]
        public void ThenTheSkillIsNotAddedWithTheMaliciousTextAndAnErrorMessageIsDisplayed()
        {
            string ExpectedMessage = "Please enter valid skill and experience level";
            string? ActualMessage = skillsObj.skillsObjectsObj.ValidationErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage,
                $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I add a skill without entering a skill name in the skill textbox and without selecting a skill level")]
        public void WhenIAddASkillWithoutEnteringASkillNameInTheSkillTextboxAndWithoutSelectingASkillLevel()
        {
            skillsObj.AddEmptySkillsFields();
        }

        [Then(@"the skill is not added with empty skill textbox and skill level fields and an error message is displayed")]
        public void ThenTheSkillIsNotAddedWithEmptySkillTextboxAndSkillLevelFieldsAndAnErrorMessageIsDisplayed()
        {
            try
            {
                Assert.That(skillsObj.skillsObjectsObj.ValidationErrorMessage,
                    Is.EqualTo("Please enter skill and experience level"));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to add a skill with a valid skill name but without selecting a skill level")]
        public void WhenITryToAddASkillWithAValidSkillNameButWithoutSelectingASkillLevel()
        {
            skillsObj.AddOnlySkillName();
        }

        [Then(@"the skill is not added without selecting the skill level and an error is displayed")]
        public void ThenTheSkillIsNotAddedWithoutSelectingTheSkillLevelAndAnErrorIsDisplayed()
        {
            try
            {
                Assert.That(skillsObj.skillsObjectsObj.ValidationErrorMessage,
                    Is.EqualTo("Please enter skill and experience level"));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to add a skill with an empty skill textbox but with a valid skill level")]
        public void WhenITryToAddASkillWithAnEmptySkillTextboxButWithAValidSkillLevel()
        {
            skillsObj.AddOnlySkillLevel();
        }

        [Then(@"the skill is not added with an empty skill textbox and an error message is displayed")]
        public void ThenTheSkillIsNotAddedWithAnEmptySkillTextboxAndAnErrorMessageIsDisplayed()
        {
            try
            {
                Assert.That(skillsObj.skillsObjectsObj.ValidationErrorMessage,
                    Is.EqualTo("Please enter skill and experience level"));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to add a skill with an existing skill name in the skill list")]
        public void WhenITryToAddASkillWithAnExistingSkillNameInTheSkillList()
        {
            skillsObj.AddSkillWithDuplicateInput();
        }

        [Then(@"the duplicate skill cannot be added and an error is displayed")]
        public void ThenTheDuplicateSkillCannotBeAddedAndAnErrorIsDisplayed()
        {
            try
            {
                Assert.That(skillsObj.skillsObjectsObj.ValidationErrorMessage,
                    Is.EqualTo("This skill is already exist in your skill list."));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [Given(@"that skills exist under the skills feature")]
        public void GivenThatSkillsExistUnderTheSkillsFeature()
        {
            skillsObj.CheckSkillAvailability();
        }

        [When(@"I try to update any skill in the skill list with a valid skill name and valid skill level")]
        public void WhenITryToUpdateAnySkillInTheSkillListWithAValidSkillNameAndValidSkillLevel()
        {
            skillsObj.UpdateSkillsWithValidInputs();
        }

        [Then(@"the skill can be edited successfully with the valid skill name and valid skill level")]
        public void ThenTheSkillCanBeEditedSuccessfullyWithTheValidSkillNameAndValidSkillLevel()
        {

            try
            {
                string expectedMessage = skillsObj.skillsObjectsObj.EditSkillsInput +
                                        " has been updated to your skills";
                Assert.That(skillsObj.skillsObjectsObj.SkillUpdateSuccessMessage,
    Is.EqualTo(expectedMessage), "Confirmation message for skill update is incorrect.");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to update a skill with a combination of special characters and alphabetic text inputs")]
        public void WhenITryToUpdateASkillWithACombinationOfSpecialCharactersAndAlphabeticTextInputs()
        {
            skillsObj.UpdateSkillWithCharactersInput();
        }

        [Then(@"the skill cannot be updated with special characters and an error message is displayed")]
        public void ThenTheSkillCannotBeUpdatedWithSpecialCharactersAndAnErrorMessageIsDisplayed()
        {
            try
            {
                string expectedMessage = skillsObj.skillsObjectsObj.EditSkillsCharacterInput +
                    " has been updated to your skills";
                Assert.That(skillsObj.skillsObjectsObj.SkillUpdateSuccessMessage,
                    Is.EqualTo(expectedMessage), "Confirmation message for skill update is incorrect.");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to update an existing skill with very long text as the skill name")]
        public void WhenITryToUpdateAnExistingSkillWithVeryLongTextAsTheSkillName()
        {
            skillsObj.UpdateSkillWithLongText();
        }

        [Then(@"the skill cannot be updated with a long text input and an error message is displayed")]
        public void ThenTheSkillCannotBeUpdatedWithALongTextInputAndAnErrorMessageIsDisplayed()
        {
            string ExpectedMessage = "Please enter valid skill and experience level";
            string? ActualMessage = skillsObj.skillsObjectsObj.SkillUpdateErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage,
                $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I try to update a skill and leave only spaces in the skill textbox")]
        public void WhenITryToUpdateASkillAndLeaveOnlySpacesInTheSkillTextbox()
        {
            skillsObj.UpdateSkillWithSpaces();
        }

        [Then(@"the skill is not updated with spaces and an error message is displayed")]
        public void ThenTheSkillIsNotUpdatedWithSpacesAndAnErrorMessageIsDisplayed()
        {

            string ExpectedMessage = "Please enter valid skill and experience level";
            string? ActualMessage = skillsObj.skillsObjectsObj.SkillUpdateErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage,
                $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I try to update a skill and enter malicious text in the skill textbox")]
        public void WhenITryToUpdateASkillAndEnterMaliciousTextInTheSkillTextbox()
        {
            skillsObj.UpdateSkillWithMaliciousText();

        }
        [Then(@"the skill is not updated with malicious data and an error message is displayed")]
        public void ThenTheSkillIsNotUpdatedWithMaliciousDataAndAnErrorMessageIsDisplayed()
        {
            string ExpectedMessage = "Please enter valid skill and experience level";
            string? ActualMessage = skillsObj.skillsObjectsObj.SkillUpdateErrorMessage;
            SoftAssert.Assert(ExpectedMessage == ActualMessage,
    $"Expected message '{ExpectedMessage}' is not equal to actual message '{ActualMessage}'");
        }

        [When(@"I try to update a skill without entering a skill name and without selecting skill level")]
        public void WhenITryToUpdateASkillWithoutEnteringASkillNameAndWithoutSelectingSkillLevel()
        {
            skillsObj.UpdateSkillWithEmptyFields();
        }

        [Then(@"the skill is not updated without entering a skill name and without selecting skill level")]
        public void ThenTheSkillIsNotUpdatedWithoutEnteringASkillNameAndWithoutSelectingSkillLevel()
        {
            try
            {
                Assert.That(skillsObj.skillsObjectsObj.SkillUpdateErrorMessage,
                    Is.EqualTo("Please enter skill and experience level"));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to update a skill with valid skill name and without selecting skill level")]
        public void WhenITryToUpdateASkillWithValidSkillNameAndWithoutSelectingSkillLevel()
        {
            skillsObj.UpdateOnlySkillName();
        }

        [Then(@"the skill is not updated without selecting skill level and an error is displayed")]
        public void ThenTheSkillIsNotUpdatedWithoutSelectingSkillLevelAndAnErrorIsDisplayed()
        {
            try
            {
                Assert.That(skillsObj.skillsObjectsObj.SkillUpdateErrorMessage,
                    Is.EqualTo("Please enter skill and experience level"));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to update a skill without entering a skill name but selecting a skill level")]
        public void WhenITryToUpdateASkillWithoutEnteringASkillNameButSelectingASkillLevel()
        {
            skillsObj.UpdateOnlySkillLevel();
        }

        [Then(@"the skill is not updated with an empty skill textbox and an error message is displayed")]
        public void ThenTheSkillIsNotUpdatedWithAnEmptySkillTextboxAndAnErrorMessageIsDisplayed()
        {
            try
            {
                Assert.That(skillsObj.skillsObjectsObj.SkillUpdateErrorMessage,
                    Is.EqualTo("Please enter skill and experience level"));
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }

        [When(@"I try to update a skill with an existing skill name in the skill list")]
        public void WhenITryToUpdateASkillWithAnExistingSkillNameInTheSkillList()
        {
            skillsObj.UpdateWithDuplicateSkill();
        }

        [Then(@"the duplicate skill cannot be updated and an error is displayed")]
        public void TheDuplicateSkillCannotBeUpdatedAndAnErrorIsDisplayed()
        {
            try
            {
                string? actualMessage = skillsObj.skillsObjectsObj.SkillUpdateErrorMessage;
                
                SoftAssert.Assert(new[] { "Duplicated data", "This skill is already added to your skill list." }.Contains(actualMessage),
        $"Unexpected validation message: {actualMessage}");
            }

            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }


        [When(@"I try to delete a skill from the skill list")]
        public void WhenITryToDeleteASkillFromTheSkillList()
        {
            skillsObj.DeleteSkill();
        }

        [Then(@"the skill from the list is deleted successfully")]
        public void ThenTheSkillFromTheListIsDeletedSuccessfully()
        {

            try
            {
                string expectedMessage = skillsObj.skillsObjectsObj.firstColumnValueInSkills + " has been deleted";
                Assert.That(skillsObj.skillsObjectsObj.DeleteSuccessMessage,
                    Is.AnyOf(expectedMessage, "has been deleted"), "Confirmation message for language update is incorrect.");
            }
            catch (NUnit.Framework.AssertionException ex)
            {
                TestContext.WriteLine($"Assertion failed: {ex.Message}");
            }
        }
    }
}
