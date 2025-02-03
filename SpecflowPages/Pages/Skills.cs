using NUnit.Framework;
using OpenQA.Selenium;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.ExcelLibraryHelper;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.CommonDriver;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.WaitHelpers;


namespace Mars_Onboarding_Specflow.SpecFlowPages.Pages
{
    internal class Skills
    {
        public SkillsObjects skillsObjectsObj;
        public LanguageObjects languageObjectsObj;
        private Languages languagesObj;


        public Skills(Languages languages)
        {
            skillsObjectsObj = new SkillsObjects();
            languageObjectsObj = new LanguageObjects();
            languagesObj = languages;
        }
        public void GoToProfile()
        {
            WaitUntilElementIsPresent(languageObjectsObj.ProfileModule);
            languageObjectsObj.ProfileModule?.Click();

        }
        public void GoToSkillsPage()
        {
            WaitUntilElementIsPresent(skillsObjectsObj?.SkillsPage);
            skillsObjectsObj?.SkillsPage?.Click();
        }
        public void AddSkillWithValidInputs(int skillsIndex)
        {
            try
            {
                if (driver == null)
                    throw new InvalidOperationException("Driver is not initialized.");

                skillsObjectsObj.SkillsAddNewButton?.Click();
                wait(10);

                ExcelLib.PopulateInCollection(ExcelPath, "Skills");
                string? skillsInput = ExcelLib.ReadData(skillsIndex + 1, "skill");
                if (string.IsNullOrEmpty(skillsInput))
                    throw new Exception($"No skill found for index {skillsIndex} in Excel.");

                skillsObjectsObj.AddSkillTextbox?.SendKeys(skillsInput);
                skillsObjectsObj.SkillsLevelDropdown?.Click();
                skillsObjectsObj.SkillsLevelOptionIntermediate?.Click();
                skillsObjectsObj.SkillsAddButton?.Click();

                string message = languagesObj.GetPopUpMessage();
                WaitUntilElementIsClickable(skillsObjectsObj.PopUpCloseButton);
                skillsObjectsObj?.PopUpCloseButton?.Click();
                skillsObjectsObj?.SkillsConfirmationMessages.Add(message);
                TestContext.WriteLine($"Skill '{skillsInput}' added: {message}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to add skill: {ex.Message}");
            }
            wait(15);
        }


        public void AddSkillWithSpecialCharacters()
        {


            ExcelLib.PopulateInCollection(ExcelPath, "Skills");

            // Read language from Excel
            skillsObjectsObj.specialCharactersSkillText = ExcelLib.ReadData(3, "Special Characters Skill");
            WaitUntilElementIsClickable(skillsObjectsObj.SkillsAddNewButton);
            skillsObjectsObj.SkillsAddNewButton?.Click();
            skillsObjectsObj.AddSkillTextbox?.SendKeys(skillsObjectsObj.specialCharactersSkillText);
            skillsObjectsObj.SkillsLevelDropdown?.Click();
            skillsObjectsObj.SkillsLevelOptionBeginner?.Click();
            skillsObjectsObj.SkillsAddButton?.Click();
            skillsObjectsObj.ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();

        }
        public void AddSkillWithLongText()
        {
            ExcelLib.PopulateInCollection(ExcelPath, "Skills");

            // Read language from Excel
            string? SkillSLongText = ExcelLib.ReadData(5, "Long Input");
            wait(20);
            skillsObjectsObj.SkillsAddNewButton?.Click();
            skillsObjectsObj.AddSkillTextbox?.SendKeys(SkillSLongText);
            skillsObjectsObj.SkillsLevelDropdown?.Click();
            skillsObjectsObj.SkillsLevelOptionBeginner?.Click();
            skillsObjectsObj.SkillsAddButton?.Click();
            skillsObjectsObj.ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();
            wait(10);
        }

        public void AddSkillWithSpacesInput()
        {
            string? SkillSpacesText = "          ";
            wait(5);
            skillsObjectsObj.SkillsAddNewButton?.Click();
            skillsObjectsObj.AddSkillTextbox?.SendKeys(SkillSpacesText);
            skillsObjectsObj.SkillsLevelDropdown?.Click();
            skillsObjectsObj.SkillsLevelOptionExpert?.Click();
            skillsObjectsObj.SkillsAddButton?.Click();
            skillsObjectsObj.ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();
            wait(10);

        }


        public void AddSkillWithMaliciuosText()
        {
            string? SkillMaliciousText = "<img src=x onerror=alert(Hi)>";
            wait(20);
            skillsObjectsObj.SkillsAddNewButton?.Click();
            skillsObjectsObj.AddSkillTextbox?.SendKeys(SkillMaliciousText);
            skillsObjectsObj.SkillsLevelDropdown?.Click();
            skillsObjectsObj.SkillsLevelOptionIntermediate?.Click();
            skillsObjectsObj.SkillsAddButton?.Click();
            skillsObjectsObj.ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();
            wait(10);
        }
        public void AddEmptySkillsFields()
        {
            WaitUntilElementIsPresent(skillsObjectsObj.SkillsAddNewButton);
            skillsObjectsObj.SkillsAddNewButton?.Click();
            skillsObjectsObj.SkillsAddButton?.Click();
            skillsObjectsObj.ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();
            skillsObjectsObj?.SkillsAddCancelButton?.Click();
            wait(10);
        }

        public void AddOnlySkillLevel()
        {

            skillsObjectsObj.SkillsAddNewButton?.Click();
            skillsObjectsObj.SkillsLevelDropdown?.Click();
            wait(10);
            skillsObjectsObj.SkillsLevelOptionExpert?.Click();
            skillsObjectsObj.SkillsAddButton?.Click();
            skillsObjectsObj.ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();
            skillsObjectsObj?.SkillsAddCancelButton?.Click();
            wait(20);
        }

        public void AddOnlySkillName()
        {

            ExcelLib.PopulateInCollection(ExcelPath, "Skills");

            // Read language from Excel
            string? skill = ExcelLib.ReadData(2, "new Skill");
            if (string.IsNullOrEmpty(skill))
            {
                throw new Exception($"No language found in Excel.");
            }

            // Add new skill
            wait(10);
            skillsObjectsObj.SkillsAddNewButton?.Click();
            skillsObjectsObj.AddSkillTextbox?.SendKeys(skill);
            skillsObjectsObj.SkillsAddButton?.Click();
            skillsObjectsObj.ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();
            skillsObjectsObj?.SkillsAddCancelButton?.Click();
            wait(10);

        }



        public void AddSkillWithDuplicateInput()
        {
            // Read language from Excel
            List<string> skills = GetSkillsFromTable();  // Get the list of skills
            string randomSkill = GetRandomSkill(skills); // Get a random skill from the list
            skillsObjectsObj.SkillsAddNewButton?.Click();
            skillsObjectsObj.AddSkillTextbox?.SendKeys(randomSkill);
            skillsObjectsObj.SkillsLevelDropdown?.Click();
            wait(3);
            skillsObjectsObj.SkillsLevelOptionBeginner?.Click();
            skillsObjectsObj.SkillsAddButton?.Click();
            skillsObjectsObj.ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();

            try
            {
                skillsObjectsObj?.SkillsAddCancelButton?.Click();
            }
            catch (NoSuchElementException)
            {
                TestContext.WriteLine("Cancel button not found. Skipping click action.");
            }
            wait(20);
        }

        public void CheckSkillAvailability()
        {
            if (driver == null) throw new InvalidOperationException("Driver is not initialized.");

            List<string> skillList = GetSkillsFromTable();
            if (!skillList.Any()) AddNewSkillIfNotAvailable("HTML");

            foreach (var skill in skillList) TestContext.WriteLine(skill);
        }

        public List<string> GetSkillsFromTable()
        {
            if (driver == null) throw new InvalidOperationException("Driver is not initialized.");
            List<string> skillList = new List<string>();

            IList<IWebElement> rows = driver.FindElements(By.XPath("//table//tr"));

            foreach (var row in rows)
            {
                try
                {
                    var skill = row.FindElement(By.XPath("td[1]")).Text.Trim();
                    if (!string.IsNullOrEmpty(skill) && !skillList.Contains(skill)) skillList.Add(skill);
                }
                catch (Exception) { continue; }
            }

            return skillList;
        }
        public string GetRandomSkill(List<string> skillList)
        {
            if (skillList == null || skillList.Count == 0)
            {
                throw new InvalidOperationException("The skill list is empty.");
            }

            Random random = new Random();
            int randomIndex = random.Next(skillList.Count);  // Generate a random index within the list range
            return skillList[randomIndex];  // Return the skill at the random index
        }

        private void AddNewSkillIfNotAvailable(string skill)
        {

            TestContext.WriteLine("No skills found. Adding a new skill.");
            WaitUntilElementIsPresent(skillsObjectsObj.SkillsAddNewButton);
            skillsObjectsObj?.SkillsAddNewButton?.Click();
            WaitUntilElementIsPresent(skillsObjectsObj?.AddSkillTextbox);
            skillsObjectsObj?.AddSkillTextbox?.SendKeys(skill);
            skillsObjectsObj?.SkillsLevelDropdown?.Click();
            skillsObjectsObj?.SkillsLevelOptionExpert?.Click();
            skillsObjectsObj?.SkillsAddButton?.Click();
            languagesObj.ClosePopUp();
            TestContext.WriteLine($"Skill '{skill}' added.");
        }
        private bool IsElementInViewport(IWebElement? element)
        {
            try
            {
                if (driver == null)
                {

                    throw new Exception("driver is null");
                }
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                var isVisible = js.ExecuteScript(@"
                var rect = arguments[0].getBoundingClientRect();
                return (
                    rect.top >= 0 &&
                    rect.left >= 0 &&
                    rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
                    rect.right <= (window.innerWidth || document.documentElement.clientWidth)
                );", element);
                return Convert.ToBoolean(isVisible);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in checking visibility: " + e.Message);
                return false;
            }
        }

        // Method to scroll to the element if it's not visible
        public void ScrollToElement(IWebElement? element)
        {
            try
            {
                // Check if the element is null
                if (element == null)
                {
                    Console.WriteLine("The element is null, cannot perform scroll.");
                    return;
                }

                // Only scroll if the element is not visible
                if (!IsElementInViewport(element))
                {
                    if (driver == null)
                    {

                        throw new Exception("driver is null");
                    }
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'nearest'});", element);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to scroll to the element: " + e.Message);
            }
        }

        public void DeleteSkill()
        {
            var skilList = GetSkillsListForLastValue();
            TestContext.WriteLine(skillsObjectsObj.firstColumnValueInSkills ?? "No valid data found.");
            skilList.ForEach(Console.WriteLine);

            ScrollToElement(skillsObjectsObj.SkillsDeleteButton);
            if (skillsObjectsObj.SkillsDeleteButton != null)

                ExecuteDeletion();
            else
                throw new Exception("Delete button not found.");
            wait(10);
        }

        public List<string> GetSkillsList()
        {
            var rows = driver?.FindElements(By.XPath("//table/tbody/tr"));
            if (rows == null || rows.Count == 0) throw new Exception("No rows available.");

            HashSet<string> skillSet = new HashSet<string>(); // HashSet to automatically eliminate duplicates
            foreach (var row in rows)
            {
                try
                {
                    string skill = row.FindElement(By.XPath("td[1]")).Text.Trim();
                    if (!string.IsNullOrEmpty(skill))
                    {
                        skillSet.Add(skill); // HashSet handles duplicates internally
                        if (skillsObjectsObj.firstColumnValueInSkills == null)
                            skillsObjectsObj.firstColumnValueInSkills = skill;
                    }
                }
                catch (Exception) { continue; }
            }
            return skillSet.ToList(); // Convert HashSet back to List
        }

        private void ExecuteDeletion()
        {
            ScrollToElement(skillsObjectsObj.SkillsDeleteButton);
            skillsObjectsObj.SkillsDeleteButton?.Click();
            skillsObjectsObj.DeleteSuccessMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();
            if (string.IsNullOrEmpty(skillsObjectsObj?.DeleteSuccessMessage))
                throw new Exception("Update failed.");
            wait(10);
        }

        private List<string> GetSkillsListForLastValue()
        {
            var rows = driver?.FindElements(By.XPath("//table/tbody/tr"));
            if (rows == null || rows.Count == 0) throw new Exception("No rows available.");

            var skillSet = new HashSet<string>();
            string lastValidSkill = string.Empty;

            foreach (var row in rows)
            {
                ExtractSkillsFromRow(row, skillSet, ref lastValidSkill);
            }

            SetFirstValidSkill(lastValidSkill);
            return skillSet.ToList();
        }

        private void ExtractSkillsFromRow(IWebElement row, HashSet<string> skillSet, ref string lastValidSkill)
        {
            try
            {
                string skill = row.FindElement(By.XPath("td[1]")).Text.Trim();

                if (!string.IsNullOrEmpty(skill))
                {
                    skillSet.Add(skill); // Add valid skills
                    lastValidSkill = skill; // Update the last valid skill
                }
            }
            catch (Exception) { }
        }

        private void SetFirstValidSkill(string lastValidSkill)
        {
            if (skillsObjectsObj.firstColumnValueInSkills == null && !string.IsNullOrEmpty(lastValidSkill))
            {
                skillsObjectsObj.firstColumnValueInSkills = lastValidSkill;
            }
        }

        public void UpdateSkillsWithValidInputs()
        {
            try
            {

                ScrollToElement(skillsObjectsObj.SkillsUpdateIconButton);
                WaitUntilElementIsClickable(skillsObjectsObj.SkillsUpdateIconButton);
                skillsObjectsObj.SkillsUpdateIconButton?.Click();
                skillsObjectsObj.SkillUpdateTextBox?.Clear();
                skillsObjectsObj.EditSkillsInput = "API Testing";
                skillsObjectsObj.SkillUpdateTextBox?.SendKeys(skillsObjectsObj.EditSkillsInput);
                skillsObjectsObj.SkillsLevelUpdateDropdown?.Click();
                skillsObjectsObj.SkillsLevelOptionBeginner?.Click();
                skillsObjectsObj.SkillsUpdateButton?.Click();
                skillsObjectsObj.SkillUpdateSuccessMessage = languagesObj.GetPopUpMessage();
                // Validate update success message
                if (string.IsNullOrEmpty(skillsObjectsObj?.SkillUpdateSuccessMessage))
                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditSkill: {ex.Message}");
            }
            languagesObj.ClosePopUp();
            wait(10);
        }


        public void UpdateSkillWithCharactersInput()
        {
            try
            {
                ScrollToElement(skillsObjectsObj.SkillsUpdateIconButton);
                WaitUntilElementIsClickable(skillsObjectsObj.SkillsUpdateIconButton);
                skillsObjectsObj.SkillsUpdateIconButton?.Click();
                skillsObjectsObj.SkillUpdateTextBox?.Click();
                skillsObjectsObj.SkillUpdateTextBox?.Clear();
                ExcelLib.PopulateInCollection(ExcelPath, "Skills");
                // Read language from Excel
                skillsObjectsObj.EditSkillsCharacterInput = ExcelLib.ReadData(2, "Special Characters Skill");
                skillsObjectsObj.SkillUpdateTextBox?.SendKeys(skillsObjectsObj.EditSkillsCharacterInput);
                skillsObjectsObj.SkillsLevelUpdateDropdown?.Click();
                skillsObjectsObj.SkillsLevelOptionExpert?.Click();
                skillsObjectsObj.SkillsUpdateButton?.Click();
                if (string.IsNullOrEmpty(skillsObjectsObj.SkillUpdateSuccessMessage = languagesObj.GetPopUpMessage()))

                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditSkill: {ex.Message}");
            }
            languagesObj.ClosePopUp();
            wait(20);
        }


        public void UpdateSkillWithLongText()
        {
            try
            {
                ScrollToElement(skillsObjectsObj.SkillsUpdateIconButton);
                WaitUntilElementIsClickable(skillsObjectsObj.SkillsUpdateIconButton);
                skillsObjectsObj.SkillsUpdateIconButton?.Click();
                skillsObjectsObj.SkillUpdateTextBox?.Click();
                skillsObjectsObj.SkillUpdateTextBox?.Clear();
                ExcelLib.PopulateInCollection(ExcelPath, "Skills");
                string? skillLongText = ExcelLib.ReadData(4, "Long Input");
                skillsObjectsObj.SkillUpdateTextBox?.SendKeys(skillLongText);
                skillsObjectsObj.SkillsLevelUpdateDropdown?.Click();
                skillsObjectsObj.SkillsLevelOption?.Click();
                skillsObjectsObj.SkillsLevelOptionExpert?.Click();
                wait(5);
                skillsObjectsObj.SkillsUpdateButton?.Click();
                if (string.IsNullOrEmpty(skillsObjectsObj.SkillUpdateErrorMessage = languagesObj.GetPopUpMessage()))

                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateSkillWithLongText: {ex.Message}");
            }
            languagesObj.ClosePopUp();
            wait(10);
        }

        public void UpdateSkillWithSpaces()
        {
            try
            {
                string? skillSpacesText = "              ";
                ScrollToElement(skillsObjectsObj.SkillsUpdateIconButton);
                WaitUntilElementIsClickable(skillsObjectsObj.SkillsUpdateIconButton);
                skillsObjectsObj.SkillsUpdateIconButton?.Click();
                skillsObjectsObj.SkillUpdateTextBox?.Click();
                skillsObjectsObj.SkillUpdateTextBox?.Clear();
                skillsObjectsObj.SkillUpdateTextBox?.SendKeys(skillSpacesText);
                skillsObjectsObj.SkillsLevelUpdateDropdown?.Click();
                skillsObjectsObj.SkillsLevelOptionIntermediate?.Click();
                skillsObjectsObj.SkillsUpdateButton?.Click();
                // Validate update success
                if (string.IsNullOrEmpty(skillsObjectsObj.SkillUpdateErrorMessage = languagesObj.GetPopUpMessage()))
                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateSkillWithLongText: {ex.Message}");
            }
            languagesObj.ClosePopUp();
            wait(10);
        }

        public void UpdateSkillWithMaliciousText()
        {
            try
            {
                ScrollToElement(skillsObjectsObj.SkillsUpdateIconButton);
                WaitUntilElementIsClickable(skillsObjectsObj.SkillsUpdateIconButton);
                skillsObjectsObj.SkillsUpdateIconButton?.Click();
                skillsObjectsObj.SkillUpdateTextBox?.Click();
                skillsObjectsObj.SkillUpdateTextBox?.Clear();
                string? skillSpacesText = "<script>alert('XSS');</script>";
                skillsObjectsObj.SkillUpdateTextBox?.SendKeys(skillSpacesText);
                skillsObjectsObj.SkillsLevelUpdateDropdown?.Click();
                skillsObjectsObj.SkillsLevelOptionIntermediate?.Click();
                wait(5);
                skillsObjectsObj.SkillsUpdateButton?.Click();
                if (string.IsNullOrEmpty(skillsObjectsObj.SkillUpdateErrorMessage = languagesObj.GetPopUpMessage()))

                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateSkillWithLongText: {ex.Message}");
            }

            languagesObj.ClosePopUp();
            wait(10);
        }
        public void UpdateSkillWithEmptyFields()
        {
            try
            {
                if (skillsObjectsObj.SkillsUpdateIconButton != null)
                {
              
                    // Click edit buttons
                    ScrollToElement(skillsObjectsObj.SkillsUpdateIconButton);
                    WaitUntilElementIsClickable(skillsObjectsObj.SkillsUpdateIconButton);
                    skillsObjectsObj.SkillsUpdateIconButton?.Click();
                    skillsObjectsObj.SkillUpdateTextBox?.Click();
                    skillsObjectsObj.SkillUpdateTextBox?.Clear();
                    wait(10);

                    // Select the level
                    skillsObjectsObj.SkillsLevelUpdateDropdown?.Click();
                    skillsObjectsObj.SkillsLevelOption?.Click();
                    wait(5);

                    // Submit the update
                    skillsObjectsObj.SkillsUpdateButton?.Click();
                }
                wait(15);

                // Validate the update success message
                skillsObjectsObj.SkillUpdateErrorMessage = languagesObj.GetPopUpMessage();

                if (string.IsNullOrEmpty(skillsObjectsObj?.SkillUpdateErrorMessage))
                {
                    throw new Exception("Update failed. No success message received.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditSkill: {ex.Message}");
            }
            CancelUpdate();
            languagesObj.ClosePopUp();
            wait(20);
        }

        public void UpdateOnlySkillName()
        {
            try
            {
                ScrollToElement(skillsObjectsObj.SkillsUpdateIconButton);
                WaitUntilElementIsClickable(skillsObjectsObj.SkillsUpdateIconButton);
                skillsObjectsObj.SkillsUpdateIconButton?.Click();
                skillsObjectsObj.SkillUpdateTextBox?.Clear();
                skillsObjectsObj.EditSkillsInput = "Rest API";
                skillsObjectsObj.SkillUpdateTextBox?.SendKeys(skillsObjectsObj.EditSkillsInput);
                skillsObjectsObj.SkillsLevelUpdateDropdown?.Click();
                skillsObjectsObj.SkillsLevelOption?.Click();
                wait(5);
                skillsObjectsObj.SkillsUpdateButton?.Click();
                if (string.IsNullOrEmpty(skillsObjectsObj.SkillUpdateErrorMessage = languagesObj.GetPopUpMessage()))

                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditSkill: {ex.Message}");
            }
            CancelUpdate();
            languagesObj.ClosePopUp();
            wait(20);
        }


        public void UpdateOnlySkillLevel()
        {
            try
            {
                ScrollToElement(skillsObjectsObj.SkillsUpdateIconButton);
                WaitUntilElementIsClickable(skillsObjectsObj.SkillsUpdateIconButton);
                skillsObjectsObj.SkillsUpdateIconButton?.Click();
                skillsObjectsObj.SkillUpdateTextBox?.SendKeys(Keys.Control + "a");
                skillsObjectsObj.SkillUpdateTextBox?.SendKeys(Keys.Delete);
                skillsObjectsObj.SkillsLevelUpdateDropdown?.Click();
                skillsObjectsObj.SkillsLevelOptionBeginner?.Click();
                wait(5);
                skillsObjectsObj.SkillsUpdateButton?.Click();
                if (string.IsNullOrEmpty(skillsObjectsObj.SkillUpdateErrorMessage = languagesObj.GetPopUpMessage()))


                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditSkill: {ex.Message}");
            }
            CancelUpdate();
            languagesObj.ClosePopUp();
            wait(10);
        }
        public void CancelUpdate()
        {
            try
            {
                // Ensure element exists before checking Displayed
                if (skillsObjectsObj.SkillsUpdateCancelButton != null && skillsObjectsObj.SkillsUpdateCancelButton.Displayed)
                {
                    WaitUntilElementIsClickable(skillsObjectsObj.SkillsUpdateCancelButton);
                    skillsObjectsObj.SkillsUpdateCancelButton.Click();
                }
            }
            catch (NoSuchElementException)
            {
                TestContext.WriteLine("Update cancel button not found. Continuing test...");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            wait(10);

        }

        public void UpdateWithDuplicateSkill()
        {
            try
            {
                ScrollToElement(skillsObjectsObj.SkillsUpdateIconButton);
                WaitUntilElementIsClickable(skillsObjectsObj.SkillsUpdateIconButton);
                skillsObjectsObj.SkillsUpdateIconButton?.Click();
                skillsObjectsObj.SkillUpdateTextBox?.Click();
                string? currentValue = skillsObjectsObj.SkillUpdateTextBox?.GetDomProperty("value");
                skillsObjectsObj.SkillUpdateTextBox?.SendKeys(Keys.Control + "a");
                skillsObjectsObj.SkillUpdateTextBox?.SendKeys(Keys.Delete);
                skillsObjectsObj.SkillUpdateTextBox?.SendKeys(currentValue);
                skillsObjectsObj.SkillsLevelUpdateDropdown?.Click();
                skillsObjectsObj.SkillsLevelOptionExpert?.Click();
                skillsObjectsObj.SkillsUpdateButton?.Click();
                if (string.IsNullOrEmpty(skillsObjectsObj.SkillUpdateErrorMessage = languagesObj.GetPopUpMessage())) 
                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditSkill: {ex.Message}");
            }
            CancelUpdate();
            languagesObj.ClosePopUp();
            wait(10);
        }
    }
}
