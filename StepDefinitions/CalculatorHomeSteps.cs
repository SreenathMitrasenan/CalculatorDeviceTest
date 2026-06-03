
using CoreAutomation.Extensions;
using CoreAutomation.TestFixture;
using CoreAutomation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static CoreAutomation.Utilities.ReportLog;
namespace CalculatorDeviceTest.StepDefinitions
{


    [Binding]
    public class CalculatorHomeSteps
    {
        private readonly IWebDriver _driver;
        private readonly CalculatorHomePage _calculatorhomePage;
        private readonly String className;
        private readonly int defaultWait;
        private readonly WebDriverWait _wait;
        public CalculatorHomeSteps(ScenarioContext scenarioContext)
        {
            _driver = scenarioContext.Get<IWebDriver>("driver");
            _calculatorhomePage = new CalculatorHomePage(_driver);
            className = this.GetType().Name.Replace("Steps", "");
            defaultWait = TestSettings.DefaultWaitTime;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(defaultWait));
        }


        [Then(@"I click on (.*) webelement present in CalculatorHome page")]
        public void ThenIClickOn_WebelementPresentInCalculatorHomePage(string webElement)
        {
            string txtToType = string.Empty;
            if (_calculatorhomePage.GetControlInfo(webElement) != null)
            {
                PerformAction(_calculatorhomePage.GetWebElement(webElement), _calculatorhomePage.GetControlInfo(webElement), txtToType, className);
            }
            else
            {
                ReportLog.ReportStep(Status.Fail, String.Format("Web element {0} not found in {1} page  ", webElement, className));
            }
        }

        [When(@"I verify the element is displayed on screen as (.*)")]
        public void WhenIVerifyTheElementIsDisplayedOnScreenAs(string expectedValue)
        {
            By displayLocator = By.Id("display");

            // Wait until the element exists in the DOM
            IWebElement displayElement = _wait.Until(ExpectedConditions.ElementExists(displayLocator));

            // 2. Extract the actual display value using the 'value' attribute
            string actualValue = displayElement.GetAttribute("value");

            // 3. Robust Fallback: If GetAttribute returns null/empty, extract via JavaScript Executor
            if (string.IsNullOrEmpty(actualValue))
            {
                try
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                    actualValue = js.ExecuteScript("return arguments.value;", displayElement)?.ToString();
                }
                catch (Exception ex)
                {
                    ReportLog.ReportStep(Status.Fail, $"Failed to retrieve value via JavaScript execution: {ex.Message}");
                }
            }

            // Clean strings for exact match validation
            actualValue = actualValue?.Trim();
            expectedValue = expectedValue?.Trim();

            // 4. Reporting & Assertion Lifecycle
            if (actualValue == expectedValue)
            {
                ReportLog.ReportStep(Status.Pass, $"Validation Successful: Calculator display successfully shows the expected value: '{expectedValue}'.");
            }
            else
            {
                ReportLog.ReportStep(Status.Fail, $"Validation Mismatch! Expected UI Display: '{expectedValue}', but actual system value caught was: '{actualValue}'.");
            }

            Assert.AreEqual(expectedValue, actualValue, $"The calculator display did not contain the expected execution total.");

        }




        [Then(@"I set below values in CalculatorHome page")]
        public void ThenISetBelowValuesInCalculatorHomePage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var key = row["key"];
                var value = row["value"].Trim();
                if (_calculatorhomePage.GetControlInfo(key) != null)
                {
                    PerformAction(_calculatorhomePage.GetWebElement(key), _calculatorhomePage.GetControlInfo(key), value, className);
                }
                else
                {
                    ReportLog.ReportStep(Status.Fail, String.Format("Web element {0} not found in {1} page  ", key, className));
                }
            }
        }

        private void PerformAction(IWebElement iElement, object[] objProperties, string value, string className)
        {
            string controlName = objProperties[0].ToString();
            string controlType = objProperties[1].ToString().ToLower().Trim();
            By locator = (By)objProperties[3];
            string txtToSend = value;
            string pageName = className;
            SeleniumExtensions.WaitForElementToLoad(_driver, locator, controlName, pageName, true, 20);
            try
            {
                switch (controlType)
                {
                    case "button":
                        iElement.ClickElement(controlName, controlType, txtToSend, pageName);
                        break;
                    case "textbox":
                        iElement.SendKeysToElement(controlName, txtToSend, pageName);
                        break;
                    case "radio":
                        iElement.ClickElement(controlName, controlType, txtToSend, pageName);
                        break;
                    case "dropdown":
                        iElement.SelectElement(controlName, "value", txtToSend, pageName);
                        break;
                    case "":
                        ReportLog.ReportStep(Status.Fail, String.Format("Control type not found for element {0} ", controlName));
                        break;
                    case "default":
                        break;
                }
            }
            catch (Exception ex)
            {
                ReportLog.ReportStep(Status.Fail, String.Format("Execption occured while performing action, message {0} ", ex.Message));
            }
        }

    }
}

