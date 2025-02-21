using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.CommonDriver;
using AventStack.ExtentReports;
using Mars_Onboarding_Specflow.SpecFlowPages.Helpers;

namespace Mars_Onboarding_Specflow.SpecFlowPages.Pages
{
    internal class SignIn
    {

        #region SignIn page Objects
        private static IWebElement? SignInButton => driver?.FindElement(By.XPath("//A[@class='item'][text()='Sign In']"));
        private static IWebElement? EmailTextbox => driver?.FindElement(By.XPath("(//INPUT[@type='text'])[2]"));
        private static IWebElement? PasswordTextbox => driver?.FindElement(By.XPath("//INPUT[@type='password']"));
        private static IWebElement? LoginButton => driver?.FindElement(By.XPath("//BUTTON[@class='fluid ui teal button'][text()='Login']"));

        private const string ProfileUrl = "http://localhost:5003/Account/Profile";

        #endregion
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

                SignInButton?.Click();

                string email = "azra.tabassum02@gmail.com";
                string password = "SP@ssword02";

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    throw new Exception("Email or Password is not set in Sign In.");
                }
                // Enter credentials
                EmailTextbox?.SendKeys(email);
                PasswordTextbox?.SendKeys(password);
                LoginButton?.Click();

                // Wait for the profile page to load
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.Url.Contains(ProfileUrl));

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
