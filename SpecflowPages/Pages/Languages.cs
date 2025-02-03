using AventStack.ExtentReports;
using NUnit.Framework;
using Mars_Onboarding_Specflow.SpecFlowPages.Helpers;
using OpenQA.Selenium;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.ExcelLibraryHelper;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.WaitHelpers;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.CommonDriver;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Mars_Onboarding_Specflow.SpecFlowPages.Pages
{
    internal class Languages
    {
        public LanguageObjects languageObjectsObj;
        public SkillsObjects skillsObjectsObj;
        public Skills skillsObject;

        public Languages()
        {
            skillsObject = new Skills(this); 
            languageObjectsObj = new LanguageObjects();
            skillsObjectsObj = new SkillsObjects();
        }

        public void GoToProfile()
        {
            WaitUntilElementIsPresent(languageObjectsObj.ProfileModule);
            languageObjectsObj.ProfileModule?.Click();

        }
        public void NavigateToLanguages()
        {
            WaitUntilElementIsPresent(languageObjectsObj.LanguagesPage);
            languageObjectsObj?.LanguagesPage?.Click();
        }

        public void AddValidLanguage(int languageIndex)
        {
            try
            {

                if (driver == null) throw new InvalidOperationException("Driver is not initialized.");

                // Populate Excel data and read language

                ExcelLib.PopulateInCollection(ExcelPath, "Languages");
                string language = ExcelLib.ReadData(languageIndex + 1, "Language")
                                   ?? throw new Exception($"No language found for index {languageIndex} in Excel.");

                // Add language and select level
                languageObjectsObj.AddNewButton?.Click();
                WaitUntilElementIsPresent(languageObjectsObj.LanguageNameTextbox);
                languageObjectsObj.LanguageNameTextbox?.SendKeys(language);
                SelectLanguageLevel(languageIndex);
                languageObjectsObj?.AddButton?.Click();

                // Capture and log the confirmation message
                string confirmationMessage = GetPopUpMessage();
                languageObjectsObj?.LanguageConfirmationMessages.Add(confirmationMessage);
                ClosePopUp();
                TestContext.WriteLine($"Language '{language}' added with message: {confirmationMessage}");
                wait(5);

            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error adding language: {ex.Message}");
                Assert.Fail($"Failed to add language at index {languageIndex}: {ex.Message}");
            }

        }

        public void SelectLanguageLevel(int languageIndex)
        {
            try
            {
                if (driver == null || languageObjectsObj?.LanguageLevelOptionBasic == null || languageObjectsObj?.LanguageLevelOptionConversational == null ||
                    languageObjectsObj?.LanguageLevelOptionFluent == null || languageObjectsObj?.LanguageLevelOptionNative == null)
                {
                    throw new InvalidOperationException("Driver or level options are not initialized.");
                }

                // Use array for easy level selection
                IWebElement languageLevelOption = languageIndex switch
                {
                    1 => languageObjectsObj.LanguageLevelOptionBasic,
                    2 => languageObjectsObj.LanguageLevelOptionConversational,
                    3 => languageObjectsObj.LanguageLevelOptionFluent,
                    4 => languageObjectsObj.LanguageLevelOptionNative,
                    _ => throw new InvalidOperationException("Invalid language index.")
                };

                languageLevelOption.Click(); // Select the level
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Unable to select level: {ex.Message}");
               
            }
        }
        public void ClosePopUp()
        {
            try
            {
                // Ensure element exists before checking Displayed
                if (skillsObjectsObj.PopUpCloseButton != null && skillsObjectsObj.PopUpCloseButton.Displayed)
                {
                    WaitUntilElementIsClickable(skillsObjectsObj.PopUpCloseButton);
                    skillsObjectsObj.PopUpCloseButton.Click();
                }
            }
            catch (NoSuchElementException)
            {
                TestContext.WriteLine("Pop-up close button not found. Continuing test...");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            wait(10);

        }

        public void AddLanguageWithSpecialCharactersInput()
        {


            ExcelLib.PopulateInCollection(ExcelPath, "Languages");
            //Read language from Excel
            string? specialCharacterLanguage = ExcelLib.ReadData(2, "Special Characters");
            WaitUntilElementIsPresent(languageObjectsObj.AddNewButton);
            languageObjectsObj.AddNewButton?.Click();
            languageObjectsObj.LanguageNameTextbox?.SendKeys(specialCharacterLanguage);
            languageObjectsObj.ChooseLanguageLevelDropdown?.Click();
            languageObjectsObj.LanguageLevelOptionFluent?.Click();
            languageObjectsObj.AddButton?.Click();
            languageObjectsObj.ValidationErrorMessage = GetPopUpMessage();
            ClosePopUp();

        }
        public void AddLanguageWithLongTextInput()
        {

            ExcelLib.PopulateInCollection(ExcelPath, "Languages");
            // Read language from Excel
            string? LongTextLanguage = ExcelLib.ReadData(2, "LongText");
            WaitUntilElementIsPresent(languageObjectsObj.AddNewButton);
            languageObjectsObj.AddNewButton?.Click();
            languageObjectsObj.LanguageNameTextbox?.SendKeys(LongTextLanguage);
            languageObjectsObj.ChooseLanguageLevelDropdown?.Click();
            languageObjectsObj.LanguageLevelOptionFluent?.Click();
            languageObjectsObj.AddButton?.Click();
            languageObjectsObj.ValidationErrorMessage = GetPopUpMessage();
            ClosePopUp();

        }
        public void AddLanguageWithSpacesInput()
        {
            string? SpacesTextInput = "          ";
            WaitUntilElementIsPresent(languageObjectsObj.AddNewButton);
            languageObjectsObj.AddNewButton?.Click();

            languageObjectsObj.LanguageNameTextbox?.SendKeys(SpacesTextInput);
            languageObjectsObj.ChooseLanguageLevelDropdown?.Click();
            languageObjectsObj.LanguageLevelOptionConversational?.Click();
            languageObjectsObj.AddButton?.Click();
            languageObjectsObj.ValidationErrorMessage = GetPopUpMessage();
            ClosePopUp();
        }
        public void AddLanguageWithMaliciousInput()
        {
            string? MaliciousText = "<img src=x onerror=alert(1)>";
            WaitUntilElementIsPresent(languageObjectsObj.AddNewButton);
            languageObjectsObj.AddNewButton?.Click();
            WaitUntilElementIsPresent(languageObjectsObj.LanguageNameTextbox);
            languageObjectsObj.LanguageNameTextbox?.SendKeys(MaliciousText);
            languageObjectsObj.ChooseLanguageLevelDropdown?.Click();
            languageObjectsObj.LanguageLevelOptionBasic?.Click();
            languageObjectsObj.AddButton?.Click();
            AcceptAlert();
            languageObjectsObj.ValidationErrorMessage = GetPopUpMessage();

            ClosePopUp();
            wait(10);
            DeleteLanguage();
        }
        public void AcceptAlert()
        {
            if (driver == null)
                throw new InvalidOperationException("WebDriver instance is not initialized.");

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); // Adjust timeout as needed
                wait.Until(ExpectedConditions.AlertIsPresent());

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                TestContext.WriteLine("Alert accepted.");
            }
            catch (WebDriverTimeoutException)
            {
                TestContext.WriteLine("No alert present.");
            }
            catch (NoAlertPresentException)
            {
                TestContext.WriteLine("No alert found when trying to switch.");
            }
        }

        public string GetPopUpMessage()
        {
            try
            {
                if (driver == null)
                {
                    throw new InvalidOperationException("Driver is not initialized.");
                }

                // Wait for the pop-up 
                WaitUntilElementIsPresent(languageObjectsObj.PopUpBox);
                string? message = languageObjectsObj?.PopUpBox?.Text?.Trim();
                TestContext.WriteLine($"Captured pop up message: {message}");
                if (message == null)
                {
                    message = "No message found";  // Default value or handle how you want
                }
                return message;

            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error getting the message: {ex.Message}");
                return string.Empty; // Or you can throw an exception if this is a required step
            }
        }

        public void AddLanguageWithEmptyFields()
        {
            WaitUntilElementIsPresent(languageObjectsObj.AddNewButton);
            languageObjectsObj.AddNewButton?.Click();
            languageObjectsObj.AddButton?.Click();
            languageObjectsObj.ValidationErrorMessage = GetPopUpMessage();
            ClosePopUp();
            languageObjectsObj.AddCancelButton?.Click();
        }

        public void AddOnlyLanguagelevel()
        {
            WaitUntilElementIsPresent(languageObjectsObj.AddNewButton);
            languageObjectsObj.AddNewButton?.Click();
            languageObjectsObj.ChooseLanguageLevelDropdown?.Click();
            WaitUntilElementIsPresent(languageObjectsObj.LanguageLevelOptionNative);
            languageObjectsObj.LanguageLevelOptionNative?.Click();
            languageObjectsObj.AddButton?.Click();
            languageObjectsObj.ValidationErrorMessage = GetPopUpMessage();
            ClosePopUp();
            languageObjectsObj.AddCancelButton?.Click();
        }

        public void AddOnlyLangaugeName()
        {

            string language = "Spanish";
            // Add new language
            WaitUntilElementIsPresent(languageObjectsObj.AddNewButton);
            languageObjectsObj.AddNewButton?.Click();
            languageObjectsObj.LanguageNameTextbox?.SendKeys(language);
            languageObjectsObj.AddButton?.Click();
            languageObjectsObj.ValidationErrorMessage = GetPopUpMessage();
            languageObjectsObj.AddCancelButton?.Click();
            ClosePopUp();
        }
        public void AddDuplicateLanguage()
        {

            List<string> langauges = GetLanguagesFromTable();  // Get the list of skills
            string randomLanguage = GetRandomLanguages(langauges);
            WaitUntilElementIsPresent(languageObjectsObj.AddNewButton);
            languageObjectsObj.AddNewButton?.Click();
            languageObjectsObj.LanguageNameTextbox?.SendKeys(randomLanguage);
            languageObjectsObj.ChooseLanguageLevelDropdown?.Click();
            WaitUntilElementIsPresent(languageObjectsObj.LanguageLevelOptionConversational);
            languageObjectsObj.LanguageLevelOptionConversational?.Click();
            languageObjectsObj.AddButton?.Click();
            languageObjectsObj.ValidationErrorMessage = GetPopUpMessage();
            ClosePopUp();
            ClickCancelButton();
            wait(10);
        }
        public void ClickCancelButton()
        {
            try
            {
                languageObjectsObj.AddCancelButton?.Click();
            }
            catch (NoSuchElementException)
            {
                TestContext.WriteLine("Cancel button not found. Skipping click action.");
            }

            wait(20);
        }

        public void CheckLanguageAvailability()
        {
            if (driver == null)
                throw new InvalidOperationException("Driver is not initialized.");

            // Fetch language list once
            List<string> languageList = GetLanguagesFromTable();

            // Add new language only if the list is empty
            if (!languageList.Any())
            {
                AddNewLanguageIfNotAvailable("Tamil");
                languageList = GetLanguagesFromTable(); 
            }

            // Print languages
            TestContext.WriteLine(string.Join(", ", languageList));
        }

        private List<string> GetLanguagesFromTable()
        {
            var languageList = new List<string>();

            if (driver == null)
                return languageList;

            try
            {
                // Fetch all rows at once
                var rows = driver.FindElements(By.XPath("//table//tr"));

                foreach (var row in rows)
                {
                    try
                    {
                        // Get the first <td> in each row 
                        var columns = row.FindElements(By.TagName("td"));
                        if (columns.Count > 0)
                        {
                            string language = columns[0].Text.Trim();
                            if (!string.IsNullOrEmpty(language) && !languageList.Contains(language))
                                languageList.Add(language);
                        }
                    }
                    catch (StaleElementReferenceException)
                    {
                        TestContext.WriteLine("Skipping stale row...");
                    }
                }

            }
            catch (NoSuchElementException)
            {
                TestContext.WriteLine("Table or rows not found.");
            }

            return languageList;
        }

        private string GetRandomLanguages(List<string> languageList)
        {
            if (languageList == null || languageList.Count == 0)
                throw new InvalidOperationException("The skill list is empty.");

            return languageList[new Random().Next(languageList.Count)];
        }
        public void ClickEditButtonOfLastRow()
        {
            var rows = driver?.FindElements(By.XPath("//table//tr"));
            var languageList = new List<string>();
            if (rows != null)
                for (int i = rows.Count - 1; i >= 0; i--)
                {
                    var columns = rows[i].FindElements(By.TagName("td"));
                    if (columns.Count > 0)
                    {
                        string language = columns[0].Text.Trim();
                        if (!string.IsNullOrEmpty(language))
                        {
                            languageList.Add(language);
                            var editButton = rows[i].FindElement(By.XPath("td[3]/span[1]/i[1]"));
                            editButton.Click();
                            break;
                        }
                    }
                }

            // Print out the grabbed languages
            languageList.ForEach(lang => TestContext.WriteLine(lang));
        }

        public void AddNewLanguageIfNotAvailable(string language)
        {
            TestContext.WriteLine($"No languages found. Adding '{language}'.");
            WaitUntilElementIsPresent(languageObjectsObj?.AddNewButton);
            languageObjectsObj?.AddNewButton?.Click();
            languageObjectsObj?.LanguageNameTextbox?.SendKeys(language);
            languageObjectsObj?.ChooseLanguageLevelDropdown?.Click();
            languageObjectsObj?.LanguageLevelOptionFluent?.Click();
            languageObjectsObj?.AddButton?.Click();
            ClosePopUp();
        }


        public void UpdateValidLanguage()
        {
            try
            {
                languageObjectsObj.UpdateLanguageIcon?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.Clear();
                wait(10);

                languageObjectsObj.UpdateLanguageInput = "German";
                if (string.IsNullOrEmpty(languageObjectsObj.UpdateLanguageInput))
                    throw new Exception("The 'Edit Language' value from Excel is null or empty.");

                languageObjectsObj.UpdateLanguageTextbox?.SendKeys(languageObjectsObj.UpdateLanguageInput);
                languageObjectsObj.UpdateLevelDropdown?.Click();
                languageObjectsObj.LanguageLevelOptionConversational?.Click();
                languageObjectsObj.UpdateButton?.Click();
                wait(10);

                // Fetch the success message
                languageObjectsObj.UpdateLanguageSuccessMessage = GetPopUpMessage();
                ClosePopUp();
                if (string.IsNullOrEmpty(languageObjectsObj.UpdateLanguageSuccessMessage))
                    throw new Exception("Update failed. No success message received.");

                // Log the success message
                TestContext.WriteLine($"Captured pop up message: {languageObjectsObj.UpdateLanguageSuccessMessage}");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error in EditLanguage: {ex.Message}");
            }

            wait(20);

        }

        public void UpdateLanguageWithCharactersInput()
        {
            try
            {
                languageObjectsObj.UpdateLanguageIcon?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.Clear();
                wait(10);

                ExcelLib.PopulateInCollection(ExcelPath, "Languages");
                string? editLanguageCharacter = ExcelLib.ReadData(3, "Special Characters");

                languageObjectsObj.UpdateLanguageTextbox?.SendKeys(editLanguageCharacter);
                languageObjectsObj.UpdateLevelDropdown?.Click();
                languageObjectsObj.LanguageLevelOptionConversational?.Click();

                languageObjectsObj.UpdateButton?.Click();
                WaitUntilElementIsPresent(languageObjectsObj.PopUpBox);

                languageObjectsObj.UpdateLanguageSuccessMessage = GetPopUpMessage();
                ClosePopUp();
                if (string.IsNullOrEmpty(languageObjectsObj.UpdateLanguageSuccessMessage))
                {
                    throw new Exception("Update failed. No success message received.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditLanguage: {ex.Message}");
            }
            wait(20);

        }

        public void UpdateLanguageWithLongInput()
        {
            try
            {
                languageObjectsObj.UpdateLanguageIcon?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.Clear();
                wait(10);

                ExcelLib.PopulateInCollection(ExcelPath, "Languages");
                string? editLanguageLongText = ExcelLib.ReadData(2, "LongText");

                languageObjectsObj.UpdateLanguageTextbox?.SendKeys(editLanguageLongText);
                languageObjectsObj.UpdateLevelDropdown?.Click();
                languageObjectsObj.LanguageLevelOptionConversational?.Click();

                languageObjectsObj.UpdateButton?.Click();
                wait(15);
                WaitUntilElementIsPresent(languageObjectsObj.PopUpBox);
                languageObjectsObj.UpdateLanguageSuccessMessage = GetPopUpMessage();
                ClosePopUp();
                if (string.IsNullOrEmpty(languageObjectsObj.UpdateLanguageSuccessMessage))
                {
                    throw new Exception("Update failed. No success message received.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditLanguage: {ex.Message}");
            }
            wait(20);

        }

        public void UpdateLanguageWithSpacesInput()
        {
            try
            {

                languageObjectsObj.UpdateLanguageIcon?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.Clear();
                wait(10);

                string spacesText = "       ";
                languageObjectsObj.UpdateLanguageTextbox?.SendKeys(spacesText);
                languageObjectsObj.UpdateLevelDropdown?.Click();
                languageObjectsObj.LanguageLevelOptionFluent?.Click();
                wait(5);
                languageObjectsObj.UpdateButton?.Click();



                languageObjectsObj.UpdateValidationErrorMessage = GetPopUpMessage();

                if (string.IsNullOrEmpty(languageObjectsObj.UpdateValidationErrorMessage))
                    throw new Exception("Update failed. No error message received.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error: {ex.Message}");
            }
            CancelUpdate();
            ClosePopUp();
        }

        public void UpdateLanguageWithMaliciousInput()
        {
            try
            {
                languageObjectsObj.UpdateLanguageIcon?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.Clear();
                wait(10);

                string maliciousText = "<script>alert('Hacked!');</script>";
                languageObjectsObj.UpdateLanguageTextbox?.SendKeys(maliciousText);
                languageObjectsObj.UpdateLevelDropdown?.Click();
                languageObjectsObj.LanguageLevelOptionFluent?.Click();
                languageObjectsObj.UpdateButton?.Click();
                wait(15);

                languageObjectsObj.UpdateValidationErrorMessage = GetPopUpMessage();

                if (string.IsNullOrEmpty(languageObjectsObj.UpdateValidationErrorMessage))
                    throw new Exception("Update failed. No error message received.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error: {ex.Message}");
            }
            ClosePopUp();
        }



        public void UpdateLanguageWithEmptyFields()
        {
            try
            {
                // Click edit buttons
                WaitUntilElementIsClickable(languageObjectsObj.UpdateLanguageIcon);
                languageObjectsObj.UpdateLanguageIcon?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.Clear();
                wait(5);
                // Select the level
                languageObjectsObj.UpdateLevelDropdown?.Click();
                languageObjectsObj.ChooseLanguageLevelOption?.Click();
                // Submit the update
                languageObjectsObj.UpdateButton?.Click();
                wait(10);
                // Validate the update success message
                languageObjectsObj.UpdateValidationErrorMessage = GetPopUpMessage();

                if (string.IsNullOrEmpty(languageObjectsObj.UpdateValidationErrorMessage))
                {
                    throw new Exception("Update failed. No success message received.");
                }
                wait(10);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditLanguage: {ex.Message}");
            }
            CancelUpdate();
            ClosePopUp();
            wait(20);
        }

        public void UpdateOnlyLangaugeName()
        {
            try
            {
                ClickEditButtonOfLastRow();
                WaitUntilElementIsClickable(languageObjectsObj.UpdateLanguageTextbox);
                languageObjectsObj.UpdateLanguageTextbox?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.Clear();
                languageObjectsObj.UpdateLanguageInput = "Portugese";
                languageObjectsObj.UpdateLanguageTextbox?.SendKeys(languageObjectsObj.UpdateLanguageInput);
                languageObjectsObj.UpdateLevelDropdown?.Click();
                languageObjectsObj.ChooseLanguageLevelOption?.Click();
                languageObjectsObj.UpdateButton?.Click();
                languageObjectsObj.UpdateValidationErrorMessage = GetPopUpMessage();
                wait(5);
                if (string.IsNullOrEmpty(languageObjectsObj.UpdateValidationErrorMessage))
                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditLanguage: {ex.Message}");
            }
            wait(5);
            CancelUpdate();
            ClosePopUp();
            wait(20);
        }
        public void CancelUpdate()
        {
            try
            {
                // Ensure element exists before checking Displayed
                if (languageObjectsObj.UpdateCancelButton != null && languageObjectsObj.UpdateCancelButton.Displayed)
                {
                    WaitUntilElementIsClickable(languageObjectsObj.UpdateCancelButton);
                    languageObjectsObj.UpdateCancelButton.Click();
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

        public void UpdateOnlyLanguageLevel()
        {
            try
            {
                languageObjectsObj.UpdateLanguageIcon?.Click();
                languageObjectsObj.UpdateLanguageTextbox?.SendKeys(Keys.Control + "a");
                languageObjectsObj.UpdateLanguageTextbox?.SendKeys(Keys.Delete);
                languageObjectsObj.LanguagesSectionHeader?.Click();
                wait(10);

                languageObjectsObj.UpdateLevelDropdown?.Click();
                languageObjectsObj.LanguageLevelOptionFluent?.Click();
                languageObjectsObj.UpdateButton?.Click();
                wait(15);

                languageObjectsObj.UpdateValidationErrorMessage = GetPopUpMessage();
                wait(5);

                if (string.IsNullOrEmpty(languageObjectsObj.UpdateValidationErrorMessage))
                    throw new Exception("Update failed. No success message received.");

            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error: {ex.Message}");
            }
            CancelUpdate();
            ClosePopUp();
            wait(15);
        }

        public void UpdateDuplicateLanguage()
        {
            try
            {
                languageObjectsObj.UpdateLanguageIcon?.Click();
                string? currentValue = languageObjectsObj.UpdateLanguageTextbox?.GetDomProperty("value");

                languageObjectsObj.UpdateLanguageTextbox?.SendKeys(Keys.Control + "a");
                languageObjectsObj.UpdateLanguageTextbox?.SendKeys(Keys.Delete);
                wait(10);
                languageObjectsObj.UpdateLanguageTextbox?.SendKeys(currentValue);

                languageObjectsObj.UpdateButton?.Click();
                wait(20);

                languageObjectsObj.UpdateValidationErrorMessage = GetPopUpMessage();

                if (string.IsNullOrEmpty(languageObjectsObj.UpdateValidationErrorMessage))
                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error: {ex.Message}");
            }
            CancelUpdate();
            ClosePopUp();
            wait(15);
        }


        public void DeleteLanguage()
        {
            List<string> languageList = GetUniqueLanguages();
            if (languageObjectsObj.FirstColumnValue != null)
                Console.WriteLine($"First column value: {languageObjectsObj.FirstColumnValue}");
            Console.WriteLine("Languages: " + string.Join(", ", languageList));

            TryDeleteLanguage();
            wait(15);
        }

        public List<string> GetUniqueLanguages()
        {
            List<string> languageList = new List<string>();
            languageObjectsObj.LanguageTableRows = driver?.FindElements(By.XPath("//table/tbody/tr"));

            if (languageObjectsObj.LanguageTableRows != null && languageObjectsObj.LanguageTableRows.Count > 0)
            {
                foreach (var row in languageObjectsObj.LanguageTableRows)
                {
                    try
                    {
                        var language = row.FindElement(By.XPath("td[1]")).Text.Trim();
                        if (!string.IsNullOrEmpty(language) && !languageList.Contains(language))
                            languageList.Add(language);

                        if (languageObjectsObj.FirstColumnValue == null)
                            languageObjectsObj.FirstColumnValue = language;
                    }
                    catch { continue; }
                }
            }
            else throw new Exception("No rows available.");

            return languageList;
        }



        private void TryDeleteLanguage()
        {
            try
            {
                languageObjectsObj.DeleteLanguageIcon?.Click();
                AcceptAlert();
                languageObjectsObj.DeleteLanguageSuccessMessage = GetPopUpMessage();
                ClosePopUp();
                if (string.IsNullOrEmpty(languageObjectsObj.DeleteLanguageSuccessMessage))
                    throw new Exception("No success message.");

                wait(15);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error: {ex.Message}");
            }
            wait(15);
        }
        public void LogScenario()
        {
            try
            {
                CommonMethods.LogTestStep("Successfully signed in", Status.Pass);
            }
            catch (Exception ex)
            {
                CommonMethods.LogTestStep($"Sign-in failed: {ex.Message}", Status.Fail);
                throw;
            }
            finally
            {
                // Finalize reports
                CommonMethods.FinalizeExtentReports();
                TestContext.WriteLine($"Reports Path: {ConstantHelpers.ReportsPath}");
            }
        }
    }
}
