using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using CoreAutomation.Extensions;
using CoreAutomation.Managers;
using CoreAutomation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Net;
using System.Net.Sockets;

//[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace EmployeeManagement.Hooks
{
    [Binding]
    public class SpecflowHooks
    {
        private static ExtentTest? featureName;
        private static ExtentTest? scenario;
        public static ExtentReports? extent;
        private readonly FeatureContext featureContext;
        public IWebDriver driver;
        private ScenarioContext _scenarioContext;
        private string stepType;
        private string stepInfo;
        private string cstepInfo;

        // Thread-safe running counter for standard scenarios
        private static int scenarioCounter = 0;

        public SpecflowHooks(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.featureContext = featureContext;
            _scenarioContext = scenarioContext;
            _scenarioContext.Set(FileSystem.GetAPIEndpoint, "apiEndPoint");
            _scenarioContext.Set(FileSystem.GetSwagAppUrl, "swagappUrl");
            _scenarioContext.Set(FileSystem.GetEMAppUrl, "emappUrl");
            _scenarioContext.Set(FileSystem.GetCalcAppUrl, "CalcApp");
        }

        [BeforeScenario]
        void InitializeReportTitle()
        {
            // Clean-reset the logging counts right when a new scenario session kicks off
            ReportLog.ResetStepCounter();

            DriverFactory driverfactory = new DriverFactory(_scenarioContext);
            _scenarioContext.Set(driverfactory.GetDriver(), "driver");

            string runningScenarioTitle = _scenarioContext.ScenarioInfo.Title;

            // Check if the scenario belongs to a Scenario Outline with parameters
            if (_scenarioContext.ScenarioInfo.Arguments != null && _scenarioContext.ScenarioInfo.Arguments.Count > 0)
            {
                // Dynamic mapping: Extracts individual row values (e.g., "zeroButton", "0") to prevent duplicate names
                var argumentValues = _scenarioContext.ScenarioInfo.Arguments.Values.Cast<object>().Select(v => v.ToString());
                string variants = string.Join(", ", argumentValues);
                runningScenarioTitle += $" ({variants})";
            }
            else
            {
                // Fallback sequential index counter for standard running scenarios
                Interlocked.Increment(ref scenarioCounter);
                runningScenarioTitle += $" - Run #{scenarioCounter}";
            }

            scenario = featureName.CreateNode<Scenario>(runningScenarioTitle);
        }

        [AfterScenario]
        void AfterScenario()
        {
            Thread.Sleep(500);
            _scenarioContext.Get<IWebDriver>("driver").Quit();
            extent.Flush();
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            string threadID = "GUI";//Thread.CurrentThread.ManagedThreadId.ToString();
            string reportExtPath = FileSystem.GetReportPath() + $"\\ExtentReport_{threadID}.html";
            string reportKlovPath = FileSystem.GetReportPath() + $"\\ExtentKlovReport_{threadID}.html";

            var htmlReporter = new ExtentSparkReporter(reportExtPath);
            htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Config.DocumentTitle = "EMAutomation";
            htmlReporter.Config.ReportName = "Automated Execution Reports";
            htmlReporter.Config.CSS = ".extent .card-panel-test { font-family: 'Barlow', sans-serif !important; font-size: 16px; color: #333; }";

            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AddSystemInfo("Author", "Sreenath Mitrasenan");
            extent.AddSystemInfo("Machine Name", Environment.MachineName);
            extent.AddSystemInfo("Username", Environment.UserName);
            extent.AddSystemInfo("OS Version", Environment.OSVersion.ToString());
            extent.AddSystemInfo("Processor Count", Environment.ProcessorCount.ToString());
            extent.AddSystemInfo("Process ID", Environment.ProcessId.ToString());
            extent.AddSystemInfo("System Dir", Environment.SystemDirectory);
            extent.AddSystemInfo("Virtual Memory Size", Environment.SystemPageSize.ToString() + "MB");
            extent.AddSystemInfo("Environment", "EM Test [ QAT ]");
            extent.AddSystemInfo("Run Time STart", DateTime.Now.ToString());

            extent.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            string[] featureTags = featureContext.FeatureInfo.Tags.Select(s => s.ToUpperInvariant()).ToArray();
            if (featureTags.Contains("IGNORE")) featureName.Skip("Skipped");
        }

        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenariocontext)
        {
            this._scenarioContext = scenariocontext;
            driver = _scenarioContext.Get<IWebDriver>("driver");

            string stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepInfo = _scenarioContext.StepContext.StepInfo.Text;
            var table = ReportLog.GetLogTable();

            // Determine if either SpecFlow threw an exception OR our custom ReportLog registered a failure
            bool customLogFailed = ReportLog.HasFailedSteps();
            bool stepHasFailed = _scenarioContext.TestError != null || customLogFailed;

            if (!stepHasFailed)
            {
                // --- PASS LOGIC ---
                if (stepType == "Given")
                    scenario.CreateNode<Given>(" " + stepInfo).Pass("PASS").Log(Status.Pass, table);
                else if (stepType == "When")
                    scenario.CreateNode<When>(" " + stepInfo).Pass("PASS").Log(Status.Pass, table);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(" " + stepInfo).Pass("PASS").Log(Status.Pass, table);
                else if (stepType == "And")
                    scenario.CreateNode<And>(" " + stepInfo).Pass("PASS").Log(Status.Pass, table);
            }
            else
            {
                // --- FAIL LOGIC ---
                // Extract message from either the system exception or your framework's log message
                string errorMessage = _scenarioContext.TestError != null
                    ? _scenarioContext.TestError.Message
                    : "Framework Action Failure tracked in ReportLog.";

                // Capture screenshot path safely
                string screenshotPath = CaptureScreen.TakeSnap(driver);

                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepType + ": " + stepInfo).Fail(errorMessage).Fail("FAIL", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepType + ": " + stepInfo).Fail(errorMessage).Fail("FAIL", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepType + ": " + stepInfo).Fail(errorMessage).Fail("FAIL", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                else if (stepType == "And")
                    scenario.CreateNode<And>(stepType + ": " + stepInfo).Fail(errorMessage).Fail("FAIL", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());

                // Crucial Framework Protection: If your custom action swallowed an exception, force SpecFlow to stop running subsequent steps
                if (_scenarioContext.TestError == null)
                {
                    Assert.Fail(errorMessage);
                }
            }

            ReportLog.Clear();
        }
    }
}