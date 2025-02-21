using NUnit.Framework;
using OpenQA.Selenium;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.CommonDriver;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.WaitHelpers;
using OpenQA.Selenium.Support.UI;


namespace Mars_Onboarding_Specflow.SpecFlowPages.Pages
{
    internal class Skills
    {
        #region Skills Page Objects
        public List<string> SkillsConfirmationMessages = new List<string>();
        public IWebElement? SkillsPage => driver?.FindElement(By.XPath("//a[contains(text(),\"Skills\")]"));
        public IWebElement? SkillsAddNewButton => driver?.FindElement(By.XPath("//body[1]/div[1]/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[3]/div[1]/div[2]/div[1]/table[1]/thead[1]/tr[1]/th[3]/div[1]"));
        public IWebElement? AddSkillTextbox => driver?.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/div/div[1]/input"));
        public IWebElement? SkillsLevelDropdown => driver?.FindElement(By.XPath("//div[@data-tab='second']//select[@class='ui fluid dropdown']"));
        public IWebElement? SkillsAddCancelButton => driver?.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/div/span/input[2]"));
        public IWebElement? SkillsAddButton => driver?.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/div/span/input[1]"));
        public IWebElement? SkillsTable => driver?.FindElement(By.XPath("//body/div[@id='account-profile-section']/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[3]/div[1]/div[2]/div[1]/table[1]"));
        public IWebElement? SkillsUpdateIconButton => driver?.FindElement(By.XPath("(//section[2]//table/tbody/tr[1]/td[3]/span[1]/i)[last()]"));
        public IWebElement? SkillUpdateTextBox => driver?.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table/tbody/tr/td/div/div[1]/input"));
        public IWebElement? SkillsDeleteButton => driver?.FindElement(By.XPath("(//table//tr[1]//td[3]//span[2]//i[1])[last()]"));
        public IWebElement? SkillsUpdateButton => driver?.FindElement(By.CssSelector("input[value = \"Update\"]"));
        public IWebElement? SkillsUpdateCancelButton => driver?.FindElement(By.CssSelector("input.ui.button:nth-of-type(2)"));
        public IWebElement? SkillsLevelUpdateDropdown => driver?.FindElement(By.XPath("//tbody/tr[1]/td[1]/div[1]/div[2]/select[1]"));
        public IWebElement? SkillsLevelOption => driver?.FindElement(By.XPath("//option[contains(text(),'Skill Level')]"));
        public IWebElement? SkillsLevelOptionIntermediate => driver?.FindElement(By.XPath("//option[contains(text(),'Intermediate')]"));
        public IWebElement? SkillsLevelOptionExpert => driver?.FindElement(By.XPath("//option[contains(text(),'Expert')]"));

        // Property for rows of the skills table
        public IReadOnlyCollection<IWebElement?> SkillTableRows
        {
            get
            {
                if (SkillsTable != null)
                {
                    return SkillsTable.FindElements(By.TagName("tr"));
                }
                else
                {
                    return new List<IWebElement?>(); // Return an empty list if SkillsTable is null
                }
            }
        }
        public string? SkillAddSuccessMessage { get; set; }
        public string? ValidationErrorMessage { get; set; }
        public string? firstColumnValueInSkills { get; set; }
        public string? SkillUpdateSuccessMessage { get; set; }
        public string? SkillUpdateErrorMessage { get; set; }
        public string? DeleteSuccessMessage { get; set; }

        public IReadOnlyCollection<IWebElement>? deleteButtons = driver?.FindElements(By.XPath("//div[@data-tab='first']//tbody/tr/td[3]/span[2]/i"));
        public IReadOnlyCollection<IWebElement>? SkillsTableRows = driver?.FindElements(By.XPath("//section[2]//form//table/tbody/tr"));
        #endregion
        private Languages languagesObj;


        public Skills(Languages languages)
        {
            languagesObj = languages;
        }
        public void GoToProfile()
        {
            WaitUntilElementIsPresent(languagesObj.ProfileModule);
            languagesObj.ProfileModule?.Click();

        }
        public void GoToSkillsPage()
        {
            WaitUntilElementIsPresent(SkillsPage);
            SkillsPage?.Click();
        }

        public void CleanSkillsData()
        {
            try
            {
                // If no delete buttons are found, exit the method gracefully
                if (deleteButtons == null || deleteButtons.Count == 0)
                {
                    Console.WriteLine("No languages found. Skipping cleanup.");
                    return;
                }

                // Loop through and delete all languages
                while (true)
                {
                    try
                    {
                        if (deleteButtons.Count == 0)
                        {
                            Console.WriteLine("No more delete buttons found. Cleanup complete.");
                            break;
                        }

                        WaitUntilElementIsInteractable(deleteButtons.Last()); // Wait until the last delete button is clickable
                        deleteButtons.Last().Click();
                        languagesObj.ClosePopUp();
                        wait(2);
                    }
                    catch (StaleElementReferenceException)
                    {
                        Console.WriteLine("Element disappeared, retrying...");
                        continue;
                    }
                    catch (WebDriverTimeoutException)
                    {
                        Console.WriteLine("Delete button took too long to appear. Exiting cleanup.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CleanLanguageData: {ex.Message}");
            }
        }

        public void chooseSkillLevel(string skillLevel)
        {
            //Choose Lanuage Level
            SkillsLevelDropdown?.Click();
            if (SkillsLevelDropdown == null)
            {
                throw new Exception("dropdownLanguage is null. Ensure it is initialized before use.");
            }
            var selectLanguageLevelDropdown = new SelectElement(SkillsLevelDropdown);

            selectLanguageLevelDropdown.SelectByValue(skillLevel);
        }
        public void AddSkillWithValidInputs(string skill, string skillLevel)
        {
            try
            {
                if (driver == null)
                    throw new InvalidOperationException("Driver is not initialized.");

                SkillsAddNewButton?.Click();


                AddSkillTextbox?.SendKeys(skill);
                SkillsLevelDropdown?.Click();
                chooseSkillLevel(skillLevel);
                SkillsAddButton?.Click();

                SkillAddSuccessMessage = languagesObj.GetPopUpMessage();
                languagesObj.ClosePopUp();
                SkillsConfirmationMessages.Add(SkillAddSuccessMessage);
                TestContext.WriteLine($"Skill '{skill}' added: {SkillAddSuccessMessage}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to add skill: {ex.Message}");
            }

        }


        public void AddSkillWithSpecialCharacters(string skill, string skillLevel)
        {

            WaitUntilElementIsClickable(SkillsAddNewButton);
            SkillsAddNewButton?.Click();
            AddSkillTextbox?.SendKeys(skill);
            SkillsLevelDropdown?.Click();
            chooseSkillLevel(skillLevel);
            SkillsAddButton?.Click();
            SkillAddSuccessMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();

        }
        public void AddSkillWithLongText(string skill, string skillLevel)
        {

            SkillsAddNewButton?.Click();
            AddSkillTextbox?.SendKeys(skill);
            SkillsLevelDropdown?.Click();
            chooseSkillLevel(skillLevel);
            SkillsAddButton?.Click();
            ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();
        }

        public void AddSkillWithSpacesInput(string skill, string skillLevel)
        {

            SkillsAddNewButton?.Click();
            AddSkillTextbox?.SendKeys(skill);
            SkillsLevelDropdown?.Click();
            chooseSkillLevel(skillLevel);
            SkillsAddButton?.Click();
            ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();

        }


        public void AddSkillWithMaliciuosText(string skill, string skillLevel)
        {

            SkillsAddNewButton?.Click();
            AddSkillTextbox?.SendKeys(skill);
            SkillsLevelDropdown?.Click();
            chooseSkillLevel(skillLevel);
            SkillsAddButton?.Click();
            languagesObj.AcceptAlert();
            ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();

        }
        public void AddEmptySkillsFields()
        {
            WaitUntilElementIsPresent(SkillsAddNewButton);
            SkillsAddNewButton?.Click();
            SkillsAddButton?.Click();
            ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();
            SkillsAddCancelButton?.Click();
            languagesObj.ClosePopUp();
        }

        public void AddOnlySkillLevel(string skill, string skillLevel)
        {

            SkillsAddNewButton?.Click();
            SkillsLevelDropdown?.Click();
            chooseSkillLevel(skillLevel);
            SkillsAddButton?.Click();
            ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();
            SkillsAddCancelButton?.Click();

        }

        public void AddOnlySkillName(string skill, string skillLevel)
        {

            SkillsAddNewButton?.Click();
            AddSkillTextbox?.SendKeys(skill);
            SkillsAddButton?.Click();
            ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();
            SkillsAddCancelButton?.Click();
            languagesObj.ClosePopUp();

        }



        public void AddSkillWithDuplicateInput(string skill, string skillLevel)
        {

            SkillsAddNewButton?.Click();
            AddSkillTextbox?.SendKeys(skill);
            SkillsLevelDropdown?.Click();
            chooseSkillLevel(skillLevel);
            SkillsAddButton?.Click();
            ValidationErrorMessage = languagesObj.GetPopUpMessage();
            languagesObj.ClosePopUp();

            try
            {
                SkillsAddCancelButton?.Click();
            }
            catch (NoSuchElementException)
            {
                TestContext.WriteLine("Cancel button not found. Skipping click action.");
            }

        }
        public bool CheckSkillsDataAvailability()
        {
            if (driver == null)
                throw new InvalidOperationException("Driver is not initialized.");

            // Fetch language list and return its availability status
            return GetSkillsFromTable().Count > 0;
        }

        private List<string> GetSkillsFromTable()
        {
            var skillList = new List<string>();

            if (driver == null)
                return skillList;

            try
            {
                // Fetch all first <td> elements at once
                var skillElements = driver.FindElements(By.XPath("//table//tr/td[1]"));

                foreach (var element in skillElements)
                {
                    try
                    {
                        string language = element.Text.Trim();
                        if (!string.IsNullOrEmpty(language) && !skillList.Contains(language))
                        {
                            skillList.Add(language);
                        }
                    }
                    catch (StaleElementReferenceException)
                    {
                        TestContext.WriteLine("Skipping stale element...");
                    }
                }
            }
            catch (NoSuchElementException)
            {
                TestContext.WriteLine("Table or rows not found.");
            }

            return skillList;
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


        public void DeleteSkill(string skill)
        {
            List<string> skillList = GetUniqueSkills();

            if (firstColumnValueInSkills != null)
                TestContext.WriteLine($"First column value: {firstColumnValueInSkills}");
            TestContext.WriteLine("Skills: " + string.Join(", ", skillList));

            TryDeleteSkill();
        }

        private void TryDeleteSkill()
        {
            try
            {
                // Check if the delete button is present before clicking
                if (SkillsDeleteButton == null || !SkillsDeleteButton.Displayed)
                {
                    TestContext.WriteLine("No delete button found. Skipping deletion.");
                    return;
                }

                SkillsDeleteButton.Click();
                wait(2);
                languagesObj.AcceptAlert();
                DeleteSuccessMessage = languagesObj.GetPopUpMessage();
                languagesObj.ClosePopUp();

                if (string.IsNullOrEmpty(DeleteSuccessMessage))
                    throw new Exception("No success message.");
            }
            catch (NoSuchElementException)
            {
                TestContext.WriteLine("Delete button not found. Skipping deletion.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error: {ex.Message}");
            }
        }

        public List<string> GetUniqueSkills()
        {
            List<string> skillList = new List<string>();
            SkillsTableRows = driver?.FindElements(By.XPath("//table/tbody/tr"));

            if (SkillsTableRows != null && SkillsTableRows.Count > 0)
            {
                foreach (var row in SkillsTableRows)
                {
                    try
                    {
                        var skill = row.FindElement(By.XPath("td[1]")).Text.Trim();
                        if (!string.IsNullOrEmpty(skill) && !skillList.Contains(skill))
                            skillList.Add(skill);

                        if (firstColumnValueInSkills == null)
                            firstColumnValueInSkills = skill;
                    }
                    catch { continue; }
                }
            }
            else throw new Exception("No rows available.");

            return skillList;
        }


        public void UpdateSkillsWithValidInputs(string skillUpdate, string skillLevelUpdate)
        {
            try
            {

                ScrollToElement(SkillsUpdateIconButton);
                WaitUntilElementIsClickable(SkillsUpdateIconButton);
                SkillsUpdateIconButton?.Click();
                SkillUpdateTextBox?.Clear();

                SkillUpdateTextBox?.SendKeys(skillUpdate);
                SkillsLevelUpdateDropdown?.Click();
                chooseSkillLevel(skillLevelUpdate);
                SkillsUpdateButton?.Click();
                SkillUpdateSuccessMessage = languagesObj.GetPopUpMessage();
                // Validate update success message
                if (string.IsNullOrEmpty(SkillUpdateSuccessMessage))
                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditSkill: {ex.Message}");
            }
            languagesObj.ClosePopUp();

        }


        public void UpdateSkillWithCharactersInput(string skillUpdate, string skillLevelUpdate)
        {
            try
            {
                ScrollToElement(SkillsUpdateIconButton);
                WaitUntilElementIsClickable(SkillsUpdateIconButton);
                SkillsUpdateIconButton?.Click();
                SkillUpdateTextBox?.Click();
                SkillUpdateTextBox?.Clear();

                SkillUpdateTextBox?.SendKeys(skillUpdate);
                SkillsLevelUpdateDropdown?.Click();
                chooseSkillLevel(skillLevelUpdate);
                SkillsUpdateButton?.Click();
                if (string.IsNullOrEmpty(SkillUpdateSuccessMessage = languagesObj.GetPopUpMessage()))

                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditSkill: {ex.Message}");
            }
            languagesObj.ClosePopUp();

        }


        public void UpdateSkillWithLongText(string skillUpdate, string skillLevelUpdate)
        {
            try
            {
                ScrollToElement(SkillsUpdateIconButton);
                WaitUntilElementIsClickable(SkillsUpdateIconButton);
                SkillsUpdateIconButton?.Click();
                SkillUpdateTextBox?.Click();
                SkillUpdateTextBox?.Clear();

                SkillUpdateTextBox?.SendKeys(skillUpdate);
                SkillsLevelUpdateDropdown?.Click();
                chooseSkillLevel(skillLevelUpdate);

                SkillsUpdateButton?.Click();
                if (string.IsNullOrEmpty(SkillUpdateErrorMessage = languagesObj.GetPopUpMessage()))

                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateSkillWithLongText: {ex.Message}");
            }
            languagesObj.ClosePopUp();
            wait(10);
        }

        public void UpdateSkillWithSpaces(string skillUpdate, string skillLevelUpdate)
        {
            try
            {
                ScrollToElement(SkillsUpdateIconButton);
                WaitUntilElementIsClickable(SkillsUpdateIconButton);
                SkillsUpdateIconButton?.Click();
                SkillUpdateTextBox?.Click();
                SkillUpdateTextBox?.Clear();
                SkillUpdateTextBox?.SendKeys(skillUpdate);
                SkillsLevelUpdateDropdown?.Click();
                chooseSkillLevel(skillLevelUpdate);
                SkillsUpdateButton?.Click();
                // Validate update success
                if (string.IsNullOrEmpty(SkillUpdateErrorMessage = languagesObj.GetPopUpMessage()))
                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateSkillWithLongText: {ex.Message}");
            }
            languagesObj.ClosePopUp();
        }

        public void UpdateSkillWithMaliciousText(string skillUpdate, string skillLevelUpdate)
        {
            try
            {
                ScrollToElement(SkillsUpdateIconButton);
                WaitUntilElementIsClickable(SkillsUpdateIconButton);
                SkillsUpdateIconButton?.Click();
                SkillUpdateTextBox?.Click();
                SkillUpdateTextBox?.Clear();

                SkillUpdateTextBox?.SendKeys(skillUpdate);
                SkillsLevelUpdateDropdown?.Click();
                SkillsLevelOptionIntermediate?.Click();

                SkillsUpdateButton?.Click();
                if (string.IsNullOrEmpty(SkillUpdateErrorMessage = languagesObj.GetPopUpMessage()))

                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateSkillWithLongText: {ex.Message}");
            }

            languagesObj.ClosePopUp();

        }
        public void UpdateSkillWithEmptyFields()
        {
            try
            {
                if (SkillsUpdateIconButton != null)
                {

                    // Click edit buttons
                    ScrollToElement(SkillsUpdateIconButton);
                    WaitUntilElementIsClickable(SkillsUpdateIconButton);
                    SkillsUpdateIconButton?.Click();
                    SkillUpdateTextBox?.Click();
                    SkillUpdateTextBox?.Clear();
                    SkillsLevelUpdateDropdown?.Click();
                    SkillsLevelOption?.Click();

                    // Submit the update
                    SkillsUpdateButton?.Click();
                }


                // Validate the update success message
                SkillUpdateErrorMessage = languagesObj.GetPopUpMessage();

                if (string.IsNullOrEmpty(SkillUpdateErrorMessage))
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

        }

        public void UpdateOnlySkillName(string skillUpdate, string skillLevelUpdate)
        {
            try
            {
                ScrollToElement(SkillsUpdateIconButton);
                WaitUntilElementIsClickable(SkillsUpdateIconButton);
                SkillsUpdateIconButton?.Click();
                SkillUpdateTextBox?.Clear();

                SkillUpdateTextBox?.SendKeys(skillUpdate);
                SkillsLevelUpdateDropdown?.Click();
                SkillsLevelOption?.Click();

                SkillsUpdateButton?.Click();
                if (string.IsNullOrEmpty(SkillUpdateErrorMessage = languagesObj.GetPopUpMessage()))

                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditSkill: {ex.Message}");
            }
            CancelUpdate();
            languagesObj.ClosePopUp();

        }


        public void UpdateOnlySkillLevel(string skillUpdate, string skillLevelUpdate)
        {
            try
            {
                ScrollToElement(SkillsUpdateIconButton);
                WaitUntilElementIsClickable(SkillsUpdateIconButton);
                SkillsUpdateIconButton?.Click();
                SkillUpdateTextBox?.SendKeys(Keys.Control + "a");
                SkillUpdateTextBox?.SendKeys(Keys.Delete);
                SkillsLevelUpdateDropdown?.Click();
                chooseSkillLevel(skillLevelUpdate);

                SkillsUpdateButton?.Click();
                if (string.IsNullOrEmpty(SkillUpdateErrorMessage = languagesObj.GetPopUpMessage()))


                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditSkill: {ex.Message}");
            }
            CancelUpdate();
            languagesObj.ClosePopUp();

        }
        public void CancelUpdate()
        {
            try
            {
                // Ensure element exists before checking Displayed
                if (SkillsUpdateCancelButton != null && SkillsUpdateCancelButton.Displayed)
                {
                    WaitUntilElementIsClickable(SkillsUpdateCancelButton);
                    SkillsUpdateCancelButton.Click();
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
            wait(5);

        }

        public void UpdateWithDuplicateSkill(string skill, string skillLevel)
        {
            try
            {
                ScrollToElement(SkillsUpdateIconButton);
                WaitUntilElementIsClickable(SkillsUpdateIconButton);
                SkillsUpdateIconButton?.Click();
                SkillUpdateTextBox?.Click();

                string? currentValue = SkillUpdateTextBox?.GetDomProperty("value");
                SkillUpdateTextBox?.SendKeys(Keys.Control + "a");
                SkillUpdateTextBox?.SendKeys(Keys.Delete);
                SkillUpdateTextBox?.SendKeys(currentValue);
                SkillsLevelUpdateDropdown?.Click();
                chooseSkillLevel(skillLevel);
                SkillsUpdateButton?.Click();
                if (string.IsNullOrEmpty(SkillUpdateErrorMessage = languagesObj.GetPopUpMessage()))
                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditSkill: {ex.Message}");
            }
            CancelUpdate();
            languagesObj.ClosePopUp();

        }
    }
}
