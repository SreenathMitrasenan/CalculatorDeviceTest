using CoreAutomation.Extensions;
using CoreAutomation.TestFixture;
using CoreAutomation.Utilities;
using EmployeeManagement.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CoreAutomation.Utilities.ReportLog;

namespace EmployeeManagement.StepDefinitions
{
    [Binding]   
    
    public class HomeSteps 
    {

        private readonly IWebDriver driver;
        private readonly HomePage _homePage; 
        private readonly string className ;
        private readonly int defaultWait;
        private readonly string swagAppUrl;
        private readonly string CalcApp;
        private readonly string emAppUrl;
      
        public HomeSteps(ScenarioContext scenarioContext)
        {
            driver = scenarioContext.Get<IWebDriver>("driver");
            swagAppUrl = scenarioContext.Get<string>("swagappUrl");
            CalcApp = scenarioContext.Get<string>("CalcApp");
            emAppUrl = scenarioContext.Get<string>("emappUrl");
            _homePage = new HomePage(driver);
            className = this.GetType().Name.Replace("Steps", "");
            defaultWait = TestSettings.DefaultWaitTime;
        }


        [Given(@"I launch calculator application")]
        public void GivenILaunchCalcApplication()
        {
            driver.Navigate().GoToUrl(CalcApp);
        }

        [Then(@"I click on (.*) webelement present in Home page")]
        public void ThenIClickOn_WebelementPresentInHomePage(string webElement)
        {
           string txtToType = string.Empty;
           if (_homePage.GetControlInfo(webElement) != null)
            {
                PerformAction(_homePage.GetWebElement(webElement), _homePage.GetControlInfo(webElement), txtToType, className);
            }     
            else
            {
                ReportLog.ReportStep(Status.Fail, String.Format("Web element {0} not found in {1} page  ", webElement, className));
            }
        }

        [Then(@"I delete the employee having (.*) property as (.*) on Home page")]
        public void ThenIDeleteTheEmployeeHaving_PropertyAs_OnHomePage(string pName, string pValue)
        {
            string dValue = pValue.Trim();
            string dName = pName.Trim();
            int idRowNum = 0;
            string idColName= string.Empty;
            IWebElement table = _homePage.employeeRecords;
            var dd = CoreAutomation.Extensions.HtmlTableExtension.ReadTable(table);
            foreach (var item in dd)
            {
                var cName = item.ColumnName;
                var cValue = item.ColumnValue;
                if ((cValue.Equals(dValue))&&(cName.Equals(dName)))
                {
                    idColName = item.ColumnName;
                    idRowNum = item.RowNumber;
                    break;
                }
            }
            ReportLog.ReportStep(Status.Info, String.Format("Table property '{0}' and value '{1}'  identified in row '{2}' and column '{3}' ", dName, dValue, idRowNum, idColName));
            // Delete Record
            var tbldelete = string.Format(HomePage.deleteRow, idRowNum);
            driver.FindElement(By.XPath(tbldelete)).ClickElement("Delete","button", "", className);

        }


        private void PerformAction(IWebElement iElement, object[] objProperties, string value, string className)
        {
            string controlName = objProperties[0].ToString();
            string controlType = objProperties[1].ToString().ToLower().Trim();
            By locator = (By)objProperties[3];
            string txtToSend = value;
            string pageName = className;
            SeleniumExtensions.WaitForElementToLoad(driver, locator, controlName, pageName, true, defaultWait);
            try
            {
                switch (controlType)
                {
                    case "button":
                        iElement.ClickElement(controlName, controlType, txtToSend, pageName); ;
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
                    default:
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
