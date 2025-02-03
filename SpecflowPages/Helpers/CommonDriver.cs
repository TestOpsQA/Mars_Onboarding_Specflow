using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;

namespace Mars_Onboarding_Specflow.SpecFlowPages.Helpers
{
    public class CommonDriver
    {
        public static IWebDriver? driver { get; set; }
        public static int Browser = 2;
        public static void Initialize()
        {
            try
            {
                // Ensuring Browser variable is set correctly
                if (Browser != 1 && Browser != 2)
                {
                    throw new ArgumentException("Invalid Browser value. Expected 1 for Firefox or 2 for Chrome.");
                }

                // Initialize driver based on the browser choice
                switch (Browser)
                {
                    case 1:
                        driver = new FirefoxDriver();
                        break;

                    case 2:
                        driver = new ChromeDriver();
                        driver.Manage().Window.Maximize();
                        break;

                    default:
                        throw new ArgumentException("Invalid Browser value.");
                }

                // Ensure driver is initialized
                if (driver == null)
                {
                    throw new NullReferenceException("driver initialization failed.");
                }

            }
            catch (TimeoutException e)
            {

                Assert.Ignore(e.Message);
            }
            TurnOnWait();

            //Maximise the window
            driver.Manage().Window.Maximize();
        }
        public static string BaseUrl
        {
            get { return ConstantHelpers.Url; }
        }


        //Implicit Wait
        public static void TurnOnWait()
        {

            if (driver != null)
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

        }

        public static void NavigateUrl()
        {
            driver?.Navigate().GoToUrl(BaseUrl);
        }

        //Close the browser
        public void Close()
        {
            driver?.Quit();
        }
    }
}
