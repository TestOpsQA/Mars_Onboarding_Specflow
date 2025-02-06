using System;
using OpenQA.Selenium.DevTools.V130.HeapProfiler;


namespace Mars_Onboarding_Specflow.SpecFlowPages.Helpers
{
    internal class ConstantHelpers
    {
        // Base URL
        public static string Url = "http://localhost:5003";

        // Screenshot Path
        public static string ScreenshotPath => GetPath("Screenshots");

        // ExtentReports Path
        public static string ReportsPath => GetPath("Reports");

        // Report XML Path
        public static string ReportXMLPath => GetPath("ReportXML");

        // Helper method to resolve paths dynamically
        private static string GetPath(string folderName)
        {
            //Gets current directory
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo? parent1 = Directory.GetParent(currentDirectory);
            DirectoryInfo? parent2 = parent1?.Parent;
            DirectoryInfo? parent3 = parent2?.Parent;

            if (parent3 == null)
            {
                throw new Exception("Solution directory could not be resolved. Check your directory structure.");
            }

            string solutionDir = parent3.FullName;
            string targetPath = Path.Combine(solutionDir, "TestReports", folderName);

            // Ensure the directory exists
            Directory.CreateDirectory(targetPath);

            return targetPath;
        }
    }
}
