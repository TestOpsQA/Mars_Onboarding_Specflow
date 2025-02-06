using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.CommonDriver;
using OpenQA.Selenium;

namespace Mars_Onboarding_Specflow.SpecFlowPages.Pages
{
    internal class LanguageObjects
    {
        #region Language Page Objects

        public List<string> LanguageConfirmationMessages = new List<string>();
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


        public IWebElement? UpdateLanguageTextbox => driver?.FindElement(By.XPath("//tbody/tr[1]/td[1]/div[1]/div[1]/input"));

        public IWebElement? UpdateLevelDropdown => driver?.FindElement(By.XPath("//tbody/tr[1]/td[1]/div[1]/div[2]/select[1]"));
        public IWebElement? UpdateButton => driver?.FindElement(By.XPath("//tbody/tr[1]/td[1]/div[1]/span[1]/input[1]"));
        public IWebElement? UpdateCancelButton => driver?.FindElement(By.XPath("//tbody/tr[1]/td[1]/div[1]/span[1]/input[2]"));
        public IWebElement? DeleteLanguageIcon => driver?.FindElement(By.XPath("(//i[@class=\"remove icon\"])[1]"));
        public IWebElement? PopUpBox => driver?.FindElement(By.ClassName("ns-box-inner"));

        public IWebElement? PopUpCloseButton => driver?.FindElement(By.XPath("//body/div[1]/a[1]"));


        public string? AddLanguageSuccessMessage { get; set; }
        public string? UpdateLanguageSuccessMessage { get; set; }


        public string? UpdateLanguageCharacterInput { get; set; }
        public string? LanguageSpecialCharactersInput { get; set; }
        public string? LongLanguageInputText { get; set; }
        public string? ValidationErrorMessage { get; set; }
        public string? UpdateValidationErrorMessage { get; set; }

        public string? UpdateLanguageInput { get; set; }

        public string? FirstColumnValue { get; set; }
        public string? LastColumnValue { get; set; }
        public string? DeleteLanguageSuccessMessage { get; set; }

        #endregion

    }
}
