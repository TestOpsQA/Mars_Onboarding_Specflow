using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.ExcelLibraryHelper;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.CommonDriver;
using AventStack.ExtentReports;
using Mars_Onboarding_Specflow.SpecFlowPages.Helpers;

namespace Mars_Onboarding_Specflow.SpecFlowPages.Pages
{
    internal class SignIn
    {

        private static IWebElement? SignInBtn => driver?.FindElement(By.XPath("//A[@class='item'][text()='Sign In']"));
        private static IWebElement? Email => driver?.FindElement(By.XPath("(//INPUT[@type='text'])[2]"));
        private static IWebElement? Password => driver?.FindElement(By.XPath("//INPUT[@type='password']"));
        private static IWebElement? LoginBtn => driver?.FindElement(By.XPath("//BUTTON[@class='fluid ui teal button'][text()='Login']"));

        private const string ExcelSheetName = "Credentials";
        private const string ProfileUrlFragment = "Profile";

        public static void SigninStep()
        {
            try
            {
                if (driver == null)
                {
                    throw new Exception("WebDriver instance is NULL. Ensure it is initialized before calling SigninStep.");
                }

                // Navigate to URL
                NavigateUrl();

                // Click "Sign In"
                SignInBtn?.Click();

                // Fetch credentials from Excel
                ExcelLib.PopulateInCollection(MarsExcelPath, ExcelSheetName);
                string? username = ExcelLib.ReadData(2, "username");
                string? password = ExcelLib.ReadData(2, "password");

                // Enter credentials
                Email?.SendKeys(username);
                Password?.SendKeys(password);
                LoginBtn?.Click();

                // Wait for the profile page to load
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
                wait.Until(d => d.Url.Contains(ProfileUrlFragment));

                // Log success
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
