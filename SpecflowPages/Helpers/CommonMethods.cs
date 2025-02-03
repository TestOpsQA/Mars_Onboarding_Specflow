using OpenQA.Selenium;
using System.Xml;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using TechTalk.SpecFlow;

namespace Mars_Onboarding_Specflow.SpecFlowPages.Helpers
{
    internal class CommonMethods
    {
        #region Screenshots
        public static string SaveScreenshot(IWebDriver driver, string ScreenShotFileName)
        {
            var folderLocation = ConstantHelpers.ScreenshotPath;

            // Ensure the Screenshots folder exists
            if (!Directory.Exists(folderLocation))
            {
                Directory.CreateDirectory(folderLocation);
            }

            // Construct the full file path with timestamp
            var fileName = Path.Combine(folderLocation, $"{ScreenShotFileName}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.jpeg");

            // Save the screenshot directly as a file
            var screenShot = ((ITakesScreenshot)driver).GetScreenshot();
            screenShot.SaveAsFile(fileName);

            return fileName;
        }
        #endregion


        #region Reports
        public static ExtentTest? Test;
        public static ExtentReports? Extent;

        public static void InitializeExtentReports()
        {
            var reportFolder = ConstantHelpers.ReportsPath;
            if (!Directory.Exists(reportFolder))
            {
                Directory.CreateDirectory(reportFolder);
            }

            // Initialize ExtentReports with the ExtentSparkReporter for HTML report
            var htmlReportFilePath = Path.Combine(reportFolder, $"TestReport_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.html");
            var sparkReporter = new ExtentSparkReporter(htmlReportFilePath);

            sparkReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Standard;
            sparkReporter.Config.ReportName = "Automation Test Results";
            sparkReporter.Config.DocumentTitle = "Test Execution Report";

            Extent = new ExtentReports();
            Extent.AttachReporter(sparkReporter);
        }

        public static void FinalizeExtentReports()
        {
            Extent?.Flush();
        }

        // Method to create an XML report after scenario execution
        public static void CreateXmlReport(string xmlReportFilePath, ScenarioContext scenarioContext)
        {
            using (var writer = XmlWriter.Create(xmlReportFilePath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("TestReports");

                writer.WriteStartElement("Test");
                writer.WriteElementString("TestName", scenarioContext.ScenarioInfo.Title);

                if (scenarioContext.TestError != null)
                {
                    writer.WriteElementString("Status", "Failed");
                    writer.WriteElementString("ErrorMessage", scenarioContext.TestError.Message);
                }
                else
                {
                    writer.WriteElementString("Status", "Passed");
                }

                writer.WriteElementString("StartTime", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
                writer.WriteElementString("EndTime", DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd_HH-mm-ss"));
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public static void LogTestStep(string stepName, Status status)
        {
            Test?.Log(status, stepName);
        }
        #endregion
    }
}
