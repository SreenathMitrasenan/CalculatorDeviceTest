
using CoreAutomation.Extensions;
using CoreAutomation.TestFixture;
using CoreAutomation.Utilities;
using EmployeeManagement.Pages;
using OpenQA.Selenium;
using static CoreAutomation.Utilities.ReportLog;
namespace EmployeeManagement.StepDefinitions
{


[Binding]
public class SwagLabHomeSteps
{
	private readonly IWebDriver _driver;
	private readonly SwagLabHomePage _swaglabhomePage;
	private readonly String className ;
	private readonly int defaultWait;
	public SwagLabHomeSteps(ScenarioContext scenarioContext)
	{
		_driver = scenarioContext.Get<IWebDriver>("driver");
		_swaglabhomePage = new SwagLabHomePage(_driver);
		className = this.GetType().Name.Replace("Steps","");
		defaultWait = TestSettings.DefaultWaitTime;
	}


	[Then(@"I click on (.*) webelement present in SwagLabHome page")]
	public void ThenIClickOn_WebelementPresentInSwagLabHomePage(string webElement)
	{
	string txtToType = string.Empty;
		if (_swaglabhomePage.GetControlInfo(webElement) != null) 
		{
			PerformAction(_swaglabhomePage.GetWebElement(webElement), _swaglabhomePage.GetControlInfo(webElement), txtToType, className);
		}
		else
		{
			ReportLog.ReportStep(Status.Fail, String.Format("Web element {0} not found in {1} page  ", webElement, className));
		}
	}

	[Then(@"I set below values in SwagLabHome page")]
	public void ThenISetBelowValuesInSwagLabHomePage(Table table)
	{
		foreach (var row in table.Rows)
			{
				var key = row["key"];
				var value = row["value"].Trim();
				if (_swaglabhomePage.GetControlInfo(key) != null)
				{
					PerformAction(_swaglabhomePage.GetWebElement(key),_swaglabhomePage.GetControlInfo(key), value, className);
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
			SeleniumExtensions.WaitForElementToLoad(_driver, locator, controlName, pageName,true,20);
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

