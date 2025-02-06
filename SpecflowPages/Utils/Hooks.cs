using Mars_Onboarding_Specflow.SpecFlowPages.Pages;
using AventStack.ExtentReports;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.CommonMethods;
using Mars_Onboarding_Specflow.SpecFlowPages.Helpers;
using TechTalk.SpecFlow;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.CommonDriver;
using NUnit.Framework;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.ExcelLibraryHelper;




namespace Mars_Onboarding_Specflow.SpecFlowPages.Utils
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext scenarioContext;
        
        // Constructor to initialize ScenarioContext
        public Hooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }
        [BeforeFeature]
        public static void StartFeature()
        {
            // Initialize Excel data (only once for the feature)
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\SpecflowTests\Data\Mars.xlsx");
            ExcelLib.PopulateInCollection(path, "Credentials");

            // Launch the browser and perform one-time sign-in
            Initialize(); // Launch browser
            InitializeExtentReports();
            SignIn.SigninStep(); // Sign in once per feature
        }

        [BeforeScenario]
        public void Setup()
        {

            // Start Test for the current scenario in Extent Reports
            Test = Extent?.CreateTest(scenarioContext.ScenarioInfo.Title);

            // Reuse the browser session (already initialized in BeforeFeature)
            if (driver == null)
            {
                Initialize(); // Ensure browser is initialized if null
            }
        }

        [AfterScenario]
        public void TearDown()
        {
            try
            {
                // Log the scenario execution status (Pass or Fail)
                TestContext.WriteLine(scenarioContext.ScenarioExecutionStatus);
                if (driver == null) return;

                // Capture a screenshot for the scenario
                string screenshotName = scenarioContext.TestError != null
                    ? $"FailedScenario_{scenarioContext.ScenarioInfo.Title}"
                    : $"Screenshot_{scenarioContext.ScenarioInfo.Title}";

                screenshotName = SanitizeFileName(screenshotName);

                // Save the screenshot and attach it to the Extent report
                string imgPath = SaveScreenshot(driver, screenshotName);
                Test?.Log(Status.Info, "Snapshot: ").AddScreenCaptureFromPath(imgPath);
                if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)

                    Test?.Fail($"Scenario failed: {scenarioContext?.TestError?.Message}");
                if (scenarioContext?.TestError != null)
                {

                    Test?.Fail($"Scenario failed: {scenarioContext.TestError.Message}");
                }
                else
                {
                    Test?.Pass("Scenario executed successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions
                Test?.Log(Status.Warning, $"Error capturing screenshot: {ex.Message}");
            }
            finally
            {

                string xmlReportFilePath = Path.Combine(ConstantHelpers.ReportXMLPath, $"TestReport_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xml");
                CreateXmlReport(xmlReportFilePath, scenarioContext);

                Extent?.Flush();
            }
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            try
            {
                // Quit the browser after the entire feature
                if (driver != null)
                {
                    driver.Quit();
                    driver = null; // Clean up the reference
                }
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Failed during feature-level cleanup: {ex.Message}");
            }
        }

        // Helper method to sanitize file names by replacing invalid characters
        private static string SanitizeFileName(string name)
        {
            foreach (var invalidChar in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(invalidChar, '_');
            }
            return name;
        }

    }
}

