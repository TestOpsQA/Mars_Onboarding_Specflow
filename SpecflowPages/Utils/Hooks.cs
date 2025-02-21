using Mars_Onboarding_Specflow.SpecFlowPages.Pages;
using AventStack.ExtentReports;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.CommonMethods;
using Mars_Onboarding_Specflow.SpecFlowPages.Helpers;
using TechTalk.SpecFlow;
using static Mars_Onboarding_Specflow.SpecFlowPages.Helpers.CommonDriver;
using NUnit.Framework;


namespace Mars_Onboarding_Specflow.SpecFlowPages.Utils
{

    [Binding]
    
    public class Hooks
    {
        private readonly ScenarioContext scenarioContext;
        private readonly Languages? languagesObj;
        private readonly Skills? skillsObj;
        public Hooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            languagesObj = new Languages();
            skillsObj = new Skills(new Languages());
        }
        [BeforeFeature]
        public static void StartFeature()
        {
           // Initialize reports

           InitializeExtentReports();
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
            SignIn.SigninStep();

        }

        [AfterScenario]
        public void TearDown()
        {
            try
            {
                TestContext.WriteLine(scenarioContext.ScenarioExecutionStatus);
                if (driver == null) return;

                // Capture screenshot if test failed
                string screenshotName = SanitizeFileName(
                    scenarioContext.TestError != null ? $"FailedScenario_{scenarioContext.ScenarioInfo.Title}"
                                                      : $"Screenshot_{scenarioContext.ScenarioInfo.Title}");

                string imgPath = SaveScreenshot(driver, screenshotName);
                Test?.Log(Status.Info, "Snapshot: ").AddScreenCaptureFromPath(imgPath);

                // Log test result
                if (scenarioContext.TestError != null)
                    Test?.Fail($"Scenario failed: {scenarioContext.TestError.Message}");
                else
                    Test?.Pass("Scenario executed successfully.");
            }
            catch (Exception ex)
            {
                Test?.Log(Status.Warning, $"Error capturing screenshot: {ex.Message}");
            }

            try
            {
                // Delete stored languages and skills if they exist
                if (languagesObj != null) DeleteStoredData("Languages", languagesObj.DeleteLanguage);
                if (skillsObj != null) DeleteStoredData("Skills", skillsObj.DeleteSkill);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during AfterScenario: " + ex.Message);
            }
            finally
            {
                // Generate report and cleanup
                if (scenarioContext != null)
                    CreateXmlReport(Path.Combine(ConstantHelpers.ReportXMLPath, $"TestReport_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xml"), scenarioContext);

                Extent?.Flush();
                driver?.Quit();
                driver = null;
            }
        }

        // Helper method to delete stored scenario data
        private void DeleteStoredData(string key, Action<string> deleteAction)
        {
            if (scenarioContext?.ContainsKey(key) == true)
            {
                var list = scenarioContext[key] as List<string>;
                if (list?.Any() == true)
                {
                    TestContext.WriteLine($"{key} to delete: " + string.Join(", ", list));
                    list.ForEach(deleteAction);
                }
                else
                {
                    TestContext.WriteLine($"No {key.ToLower()} to delete.");
                }
            }
        }


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

