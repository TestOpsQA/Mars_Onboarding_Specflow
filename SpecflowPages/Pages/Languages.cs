using NUnit.Framework;
using OpenQA.Selenium;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.WaitHelpers;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.CommonDriver;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Mars_Onboarding_Specflow.SpecFlowPages.Pages
{
    internal class Languages
    {

        #region Languages page Objects
        public IWebElement? ProfileModule => driver?.FindElement(By.LinkText("Profile"));

        public IWebElement? LanguagesPage => driver?.FindElement(By.XPath("//a[contains(text(),'Languages')]"));

        public IWebElement? AddNewButton => driver?.FindElement(By.XPath("//section[2]//form//table/thead/tr/th[3]/div"));


        public IWebElement? LanguageNameTextbox => driver?.FindElement(By.XPath("//section[2]//form//div[2]//input[1]"));

        public IWebElement? ChooseLanguageLevelDropdown => driver?.FindElement(By.XPath("//section[2]//form//div[2]//div[2]//select[1]\r\n"));

        public IWebElement? ChooseLanguageLevelOption => driver?.FindElement(By.XPath("//option[contains(text(),'Language Level')]"));
        public IWebElement? LanguageLevelOptionBasic => driver?.FindElement(By.XPath("//option[contains(text(),'Basic')]"));
        public IWebElement? LanguageLevelOptionConversational => driver?.FindElement(By.XPath("//option[contains(text(),'Conversational')]"));
        public IWebElement? LanguageLevelOptionFluent => driver?.FindElement(By.XPath("//option[contains(text(),'Fluent')]"));
        public IWebElement? LanguageLevelOptionNative => driver?.FindElement(By.XPath("//option[contains(text(),'Native/Bilingual')]"));

        public IWebElement? AddButton => driver?.FindElement(By.XPath("//section[2]//form//div[2]//div[3]/input[1]"));

        public IWebElement? AddCancelButton => driver?.FindElement(By.XPath("//section[2]//form//div[2]//div[3]/input[2]"));


        public IWebElement? UpdateLanguageIcon => driver?.FindElement(By.XPath("//tbody[1]/tr[1]/td[3]/span[1]/i[1]"));

        public IWebElement? LanguagesSectionHeader => driver?.FindElement(By.XPath("//h3[contains(text(),'Languages')]"));

        public IReadOnlyCollection<IWebElement>? LanguageTableRows = driver?.FindElements(By.XPath("//section[2]//form//table/tbody/tr"));

        public IReadOnlyCollection<IWebElement>? deleteButtons = driver?.FindElements(By.XPath("//div[@data-tab='first']//tbody/tr/td[3]/span[2]/i"));

        public IWebElement? UpdateLanguageTextbox => driver?.FindElement(By.XPath("//tbody/tr[1]/td[1]/div[1]/div[1]/input"));

        public IWebElement? UpdateLevelDropdown => driver?.FindElement(By.XPath("//tbody/tr[1]/td[1]/div[1]/div[2]/select[1]"));
        public IWebElement? UpdateButton => driver?.FindElement(By.XPath("//tbody/tr[1]/td[1]/div[1]/span[1]/input[1]"));
        public IWebElement? UpdateCancelButton => driver?.FindElement(By.XPath("//tbody/tr[1]/td[1]/div[1]/span[1]/input[2]"));
        public IWebElement? DeleteLanguageIcon => driver?.FindElement(By.XPath("//div[@data-tab='first']//tbody[last()]/tr/td[3]/span[2]/i"));


        public IWebElement? PopUpBox => driver?.FindElement(By.ClassName("ns-box-inner"));

        public IWebElement? PopUpCloseButton => driver?.FindElement(By.XPath("//body/div[1]/a[1]"));


        public string? AddLanguageSuccessMessage { get; set; }
        public string? UpdateLanguageSuccessMessage { get; set; }





        public string? ValidationErrorMessage { get; set; }
        public string? UpdateLanguageErrorMessage { get; set; }

        public string? FirstColumnValue { get; set; }

        public string? DeleteLanguageSuccessMessage { get; set; }


        public Skills skillsObject;
        #endregion
        public Languages()
        {
            skillsObject = new Skills(this);

        }

        public void GoToProfile()
        {
            WaitUntilElementIsClickable(ProfileModule);
            ProfileModule?.Click();

        }
        public void NavigateToLanguages()
        {
            WaitUntilElementIsPresent(LanguagesPage);
            LanguagesPage?.Click();
        }
        public void CleanLanguageData()
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
                        ClosePopUp();
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



        public void AddValidLanguage(string language, string languageLevel)
        {
            try
            {

                if (driver == null) throw new InvalidOperationException("Driver is not initialized.");

                // Add language and select level
                AddNewButton?.Click();
                WaitUntilElementIsPresent(LanguageNameTextbox);
                LanguageNameTextbox?.SendKeys(language);
                
                //Choose Lanuage Level
                chooseLanguageLevel(languageLevel);
                AddButton?.Click();

                // Capture and log the confirmation message
                AddLanguageSuccessMessage = GetPopUpMessage();

                ClosePopUp();
                TestContext.WriteLine($"Language '{language}' added with message: {AddLanguageSuccessMessage}");

            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error adding language: {ex.Message}");
            }

        }


        public void SelectLanguageLevel(int languageIndex)
        {
            try
            {
                if (driver == null || LanguageLevelOptionBasic == null || LanguageLevelOptionConversational == null ||
                    LanguageLevelOptionFluent == null || LanguageLevelOptionNative == null)
                {
                    throw new InvalidOperationException("Driver or level options are not initialized.");
                }

                // Using array for easy level selection
                IWebElement languageLevelOption = languageIndex switch
                {
                    1 => LanguageLevelOptionBasic,
                    2 => LanguageLevelOptionConversational,
                    3 => LanguageLevelOptionFluent,
                    4 => LanguageLevelOptionNative,
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
                if (PopUpCloseButton != null && PopUpCloseButton.Displayed)
                {
                    WaitUntilElementIsClickable(PopUpCloseButton);
                    PopUpCloseButton.Click();
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
            wait(3);
        }

        public void AddLanguageWithSpecialCharactersInput(string language, string languageLevel)
        {

            WaitUntilElementIsPresent(AddNewButton);
            AddNewButton?.Click();
            LanguageNameTextbox?.SendKeys(language);
            ChooseLanguageLevelDropdown?.Click();
            if (ChooseLanguageLevelDropdown == null)
            {
                throw new Exception("dropdownLanguage is null. Ensure it is initialized before use.");
            }
            var selectLanguageLevelDropdown = new SelectElement(ChooseLanguageLevelDropdown);

            selectLanguageLevelDropdown.SelectByValue(languageLevel);
            AddButton?.Click();
            ValidationErrorMessage = GetPopUpMessage();
            ClosePopUp();
        }
        public void AddLanguageWithLongTextInput(string language, string languageLevel)
        {

            WaitUntilElementIsPresent(AddNewButton);
            AddNewButton?.Click();
            LanguageNameTextbox?.SendKeys(language);
            ChooseLanguageLevelDropdown?.Click();
            if (ChooseLanguageLevelDropdown == null)
            {
                throw new Exception("dropdownLanguage is null. Ensure it is initialized before use.");
            }
            var selectLanguageLevelDropdown = new SelectElement(ChooseLanguageLevelDropdown);

            selectLanguageLevelDropdown.SelectByValue(languageLevel);
            AddButton?.Click();
            ValidationErrorMessage = GetPopUpMessage();
            ClosePopUp();

        }
        public void AddLanguageWithSpacesInput(string language, string languageLevel)
        {

            WaitUntilElementIsPresent(AddNewButton);
            AddNewButton?.Click();
            LanguageNameTextbox?.SendKeys(language);
            ChooseLanguageLevelDropdown?.Click();
            ChooseLanguageLevelDropdown?.Click();
            if (ChooseLanguageLevelDropdown == null)
            {
                throw new Exception("dropdownLanguage is null. Ensure it is initialized before use.");
            }
            var selectLanguageLevelDropdown = new SelectElement(ChooseLanguageLevelDropdown);

            selectLanguageLevelDropdown.SelectByValue(languageLevel);
            AddButton?.Click();
            ValidationErrorMessage = GetPopUpMessage();
            ClosePopUp();
        }
        public void chooseLanguageLevel(string languageLevel)
        {
            //Choose Lanuage Level
            ChooseLanguageLevelDropdown?.Click();
            if (ChooseLanguageLevelDropdown == null)
            {
                throw new Exception("dropdownLanguage is null. Ensure it is initialized before use.");
            }
            var selectLanguageLevelDropdown = new SelectElement(ChooseLanguageLevelDropdown);

            selectLanguageLevelDropdown.SelectByValue(languageLevel);
        }
        public void AddLanguageWithMaliciousInput(string language, string languageLevel)
        {

            WaitUntilElementIsPresent(AddNewButton);
            AddNewButton?.Click();
            WaitUntilElementIsPresent(LanguageNameTextbox);
            LanguageNameTextbox?.SendKeys(language);
            ChooseLanguageLevelDropdown?.Click();
            chooseLanguageLevel(languageLevel);
            AddButton?.Click();
            AcceptAlert();
            ValidationErrorMessage = GetPopUpMessage();

            ClosePopUp();

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
                WaitUntilElementIsPresent(PopUpBox);
                string? message = PopUpBox?.Text?.Trim();
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
            WaitUntilElementIsPresent(AddNewButton);
            AddNewButton?.Click();
            AddButton?.Click();
            ValidationErrorMessage = GetPopUpMessage();
            ClosePopUp();
            AddCancelButton?.Click();
        }

        public void AddOnlyLanguagelevel(string languageLevel)
        {
            WaitUntilElementIsPresent(AddNewButton);
            AddNewButton?.Click();
            ChooseLanguageLevelDropdown?.Click();
            chooseLanguageLevel(languageLevel);
            AddButton?.Click();
            ValidationErrorMessage = GetPopUpMessage();
            ClosePopUp();
            AddCancelButton?.Click();
        }

        public void AddOnlyLangaugeName(string language)
        {


            // Add new language
            WaitUntilElementIsPresent(AddNewButton);
            AddNewButton?.Click();
            LanguageNameTextbox?.SendKeys(language);
            AddButton?.Click();
            ValidationErrorMessage = GetPopUpMessage();
            AddCancelButton?.Click();
            ClosePopUp();
        }
        public void AddDuplicateLanguage(string language, string languageLevel)
        {


            WaitUntilElementIsPresent(AddNewButton);
            AddNewButton?.Click();
            LanguageNameTextbox?.SendKeys(language);
            chooseLanguageLevel(languageLevel);
            AddButton?.Click();
            ValidationErrorMessage = GetPopUpMessage();
            ClosePopUp();
            ClickCancelButton();
            wait(10);
        }
        public void ClickCancelButton()
        {
            try
            {
                AddCancelButton?.Click();
            }
            catch (NoSuchElementException)
            {
                TestContext.WriteLine("Cancel button not found. Skipping click action.");
            }

        }

        public bool CheckLanguageDataAvailability()
        {
            if (driver == null)
                throw new InvalidOperationException("Driver is not initialized.");

            // Fetch language list and return its availability status
            return GetLanguagesFromTable().Count > 0;
        }

        private List<string> GetLanguagesFromTable()
        {
            var languageList = new List<string>();

            if (driver == null)
                return languageList;

            try
            {
                // Fetch all first <td> elements at once
                var languageElements = driver.FindElements(By.XPath("//table//tr/td[1]"));

                foreach (var element in languageElements)
                {
                    try
                    {
                        string language = element.Text.Trim();
                        if (!string.IsNullOrEmpty(language) && !languageList.Contains(language))
                        {
                            languageList.Add(language);
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

            return languageList;
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

        public void AddNewLanguageIfNotAvailable(string language, string languageLevel)
        {
            TestContext.WriteLine($"No languages found. Adding '{language}'.");
            WaitUntilElementIsPresent(AddNewButton);
            AddNewButton?.Click();
            LanguageNameTextbox?.SendKeys(language);

            chooseLanguageLevel(languageLevel);
            AddButton?.Click();
            ClosePopUp();
        }


        public void UpdateValidLanguage(string languageUpdate, string updateLanguageLevel)
        {
            try
            {
                UpdateLanguageIcon?.Click();
                UpdateLanguageTextbox?.Click();
                UpdateLanguageTextbox?.Clear();
                wait(5);


                UpdateLanguageTextbox?.SendKeys(languageUpdate);
                UpdateLevelDropdown?.Click();
                string languageLevel = updateLanguageLevel;
                //updateLanguageLevel = languageLevel;
                chooseLanguageLevel(languageLevel);
                UpdateButton?.Click();
                wait(5);

                // Fetch the success message
                UpdateLanguageSuccessMessage = GetPopUpMessage();
                ClosePopUp();
                if (string.IsNullOrEmpty(UpdateLanguageSuccessMessage))
                    throw new Exception("Update failed. No success message received.");

                // Log the success message
                TestContext.WriteLine($"Captured pop up message: {UpdateLanguageSuccessMessage}");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error in EditLanguage: {ex.Message}");
            }

        }

        public void UpdateLanguageWithCharactersInput(string languageUpdate, string updateLanguageLevel)
        {
            try
            {
                UpdateLanguageIcon?.Click();
                UpdateLanguageTextbox?.Click();
                UpdateLanguageTextbox?.Clear();

                UpdateLanguageTextbox?.SendKeys(languageUpdate);
                string languageLevel = updateLanguageLevel;
                chooseLanguageLevel(languageLevel);
                UpdateButton?.Click();
                WaitUntilElementIsPresent(PopUpBox);

                UpdateLanguageErrorMessage = GetPopUpMessage();
                ClosePopUp();
                if (string.IsNullOrEmpty(UpdateLanguageErrorMessage))
                {
                    throw new Exception("Update failed. No success message received.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditLanguage: {ex.Message}");
            }
            wait(5);

        }

        public void UpdateLanguageWithLongInput(string languageUpdate, string languageLevelUpdate)
        {
            try
            {
                UpdateLanguageIcon?.Click();
                UpdateLanguageTextbox?.Click();
                UpdateLanguageTextbox?.Clear();
                wait(3);


                UpdateLanguageTextbox?.SendKeys(languageUpdate);
                UpdateLevelDropdown?.Click();
                string languageLevel = languageLevelUpdate;
                chooseLanguageLevel(languageLevel);

                UpdateButton?.Click();
                WaitUntilElementIsPresent(PopUpBox);
                UpdateLanguageErrorMessage = GetPopUpMessage();
                ClosePopUp();
                if (string.IsNullOrEmpty(UpdateLanguageErrorMessage))
                {
                    throw new Exception("Update failed. No success message received.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditLanguage: {ex.Message}");
            }

        }

        public void UpdateLanguageWithSpacesInput(string languageUpdate, string languageLevelUpdate)
        {
            try
            {

                UpdateLanguageIcon?.Click();
                UpdateLanguageTextbox?.Click();
                string? currentValue = UpdateLanguageTextbox?.GetDomProperty("value");
                UpdateLanguageTextbox?.SendKeys(Keys.Control + "a");
                UpdateLanguageTextbox?.SendKeys(Keys.Delete);

                UpdateLanguageTextbox?.SendKeys(languageUpdate);
                UpdateLevelDropdown?.Click();
                string languageLevel = languageLevelUpdate;
                chooseLanguageLevel(languageLevel);

                UpdateButton?.Click();

                UpdateLanguageErrorMessage = GetPopUpMessage();

                if (string.IsNullOrEmpty(UpdateLanguageErrorMessage))
                    throw new Exception("Update failed. No error message received.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error: {ex.Message}");
            }
            ClosePopUp();
            CancelUpdate();

        }

        public void UpdateLanguageWithMaliciousInput(string languageUpdate, string languageLevelUpdate)
        {
            try
            {
                UpdateLanguageIcon?.Click();
                UpdateLanguageTextbox?.Click();
                UpdateLanguageTextbox?.Clear();



                UpdateLanguageTextbox?.SendKeys(languageUpdate);
                UpdateLevelDropdown?.Click();
                string languageLevel = languageLevelUpdate;
                chooseLanguageLevel(languageLevel);
                UpdateButton?.Click();

                UpdateLanguageErrorMessage = GetPopUpMessage();

                if (string.IsNullOrEmpty(UpdateLanguageErrorMessage))
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
                WaitUntilElementIsClickable(UpdateLanguageIcon);
                UpdateLanguageIcon?.Click();
                UpdateLanguageTextbox?.Click();
                UpdateLanguageTextbox?.Clear();
                wait(5);
                // Select the level
                UpdateLevelDropdown?.Click();
                ChooseLanguageLevelOption?.Click();
                // Submit the update
                UpdateButton?.Click();

                // Validate the update success message
                UpdateLanguageErrorMessage = GetPopUpMessage();

                if (string.IsNullOrEmpty(UpdateLanguageErrorMessage))
                {
                    throw new Exception("Update failed. No success message received.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditLanguage: {ex.Message}");
            }
            CancelUpdate();
            ClosePopUp();

        }

        public void UpdateOnlyLanguageName(string languageUpdate, string languageLevelUpdate)
        {
            try
            {
                ClickEditButtonOfLastRow();
                WaitUntilElementIsClickable(UpdateLanguageTextbox);
                UpdateLanguageTextbox?.Click();
                UpdateLanguageTextbox?.Clear();

                UpdateLanguageTextbox?.SendKeys(languageUpdate);
                UpdateLevelDropdown?.Click();
                chooseLanguageLevel(languageLevelUpdate);
                UpdateButton?.Click();
                UpdateLanguageErrorMessage = GetPopUpMessage();

                if (string.IsNullOrEmpty(UpdateLanguageErrorMessage))
                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditLanguage: {ex.Message}");
            }

            CancelUpdate();
            ClosePopUp();
            wait(5);
        }
        public void CancelUpdate()
        {
            try
            {
                // Ensure element exists before checking Displayed
                if (UpdateCancelButton != null && UpdateCancelButton.Displayed)
                {
                    WaitUntilElementIsClickable(UpdateCancelButton);
                    UpdateCancelButton.Click();
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
            wait(3);
        }

        public void UpdateOnlyLanguageLevel(string languageUpdate, string languageLevelUpdate)
        {
            try
            {
                UpdateLanguageIcon?.Click();
                UpdateLanguageTextbox?.SendKeys(Keys.Control + "a");
                UpdateLanguageTextbox?.SendKeys(Keys.Delete);
                LanguagesSectionHeader?.Click();


                UpdateLevelDropdown?.Click();
                string languageLevel = languageLevelUpdate;
                chooseLanguageLevel(languageLevel);
                UpdateButton?.Click();


                UpdateLanguageErrorMessage = GetPopUpMessage();


                if (string.IsNullOrEmpty(UpdateLanguageErrorMessage))
                    throw new Exception("Update failed. No success message received.");

            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error: {ex.Message}");
            }
            CancelUpdate();
            ClosePopUp();

        }

        public void UpdateDuplicateLanguage(string language, string languageLevel)
        {
            try
            {
                UpdateLanguageIcon?.Click();
                string? currentValue = UpdateLanguageTextbox?.GetDomProperty("value");

                UpdateLanguageTextbox?.SendKeys(Keys.Control + "a");
                UpdateLanguageTextbox?.SendKeys(Keys.Delete);
                UpdateLanguageTextbox?.SendKeys(currentValue);
                chooseLanguageLevel(languageLevel);
                UpdateButton?.Click();
                UpdateLanguageErrorMessage = GetPopUpMessage();

                if (string.IsNullOrEmpty(UpdateLanguageErrorMessage))
                    throw new Exception("Update failed. No success message received.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error: {ex.Message}");
            }
            CancelUpdate();
            ClosePopUp();

        }


        public void DeleteLanguage(string language)
        {
            List<string> languageList = GetUniqueLanguages();

            if (FirstColumnValue != null)
                TestContext.WriteLine($"First column value: {FirstColumnValue}");
            TestContext.WriteLine("Languages: " + string.Join(", ", languageList));

            TryDeleteLanguage();
        }

        private void TryDeleteLanguage()
        {
            try
            {
                // Check if the DeleteLanguageIcon is present before clicking
                if (DeleteLanguageIcon == null || !DeleteLanguageIcon.Displayed)
                {
                    TestContext.WriteLine("No delete button found. Skipping deletion.");
                    return;
                }

                DeleteLanguageIcon.Click();
                wait(2);
                AcceptAlert();
                DeleteLanguageSuccessMessage = GetPopUpMessage();
                ClosePopUp();

                if (string.IsNullOrEmpty(DeleteLanguageSuccessMessage))
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


        public List<string> GetUniqueLanguages()
        {
            List<string> languageList = new List<string>();
            LanguageTableRows = driver?.FindElements(By.XPath("//table/tbody/tr"));

            if (LanguageTableRows != null && LanguageTableRows.Count > 0)
            {
                foreach (var row in LanguageTableRows)
                {
                    try
                    {
                        var language = row.FindElement(By.XPath("td[1]")).Text.Trim();
                        if (!string.IsNullOrEmpty(language) && !languageList.Contains(language))
                            languageList.Add(language);

                        if (FirstColumnValue == null)
                            FirstColumnValue = language;
                    }
                    catch { continue; }
                }
            }
            else throw new Exception("No rows available.");

            return languageList;
        }

    }
}
