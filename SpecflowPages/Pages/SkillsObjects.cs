using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.CommonDriver;
using OpenQA.Selenium;

namespace Mars_Onboarding_Specflow.SpecFlowPages.Pages
{
    internal class SkillsObjects
    {
        #region Skills Page Objects

        public List<string> SkillsConfirmationMessages = new List<string>();
        public IWebElement? SkillsPage => driver?.FindElement(By.XPath("//a[contains(text(),\"Skills\")]"));
        public IWebElement? SkillsAddNewButton => driver?.FindElement(By.XPath("//body[1]/div[1]/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[3]/div[1]/div[2]/div[1]/table[1]/thead[1]/tr[1]/th[3]/div[1]"));
        public IWebElement? AddSkillTextbox => driver?.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/div/div[1]/input"));
        public IWebElement? SkillsLevelDropdown => driver?.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/div/div[2]/select"));
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
        public IWebElement? SkillsLevelOptionBeginner => driver?.FindElement(By.XPath("//option[contains(text(),'Beginner')]"));
        public IWebElement? SkillsLevelOptionIntermediate => driver?.FindElement(By.XPath("//option[contains(text(),'Intermediate')]"));
        public IWebElement? SkillsLevelOptionExpert => driver?.FindElement(By.XPath("//option[contains(text(),'Expert')]"));

        public IWebElement? PopUpCloseButton => driver?.FindElement(By.XPath("//body/div[1]/a[1]"));

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
        public string? specialCharactersSkillText { get; set; }
        public string? ValidationErrorMessage { get; set; }
        public string? skillsNewInput { get; set; }
        public string? EditSkillsInput { get; set; }
        public string? EditSkillsCharacterInput { get; set; }
        public string? firstColumnValueInSkills { get; set; }
        public string? lastValidSkill { get; set; }

        public string? SkillUpdateSuccessMessage { get; set; }
        public string? SkillUpdateErrorMessage { get; set; }
        public string? DeleteSuccessMessage { get; set; }

    }
    #endregion
}

