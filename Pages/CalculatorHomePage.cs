
using OpenQA.Selenium;
namespace CalculatorDeviceTest.StepDefinitions
{


    public class CalculatorHomePage
    {
        private readonly IWebDriver _driver;
        public CalculatorHomePage(IWebDriver driver) => _driver = driver;

        public IWebElement cancelButton => _driver.FindElement(By.XPath("//div/button[text()='C']"));
        public IWebElement leftParanthesisButton => _driver.FindElement(By.XPath("//div/button[text()='(']"));
        public IWebElement rightParanthesisButton => _driver.FindElement(By.XPath("//div/button[text()=')']"));
        public IWebElement divideButton => _driver.FindElement(By.XPath("//div/button[text()='÷']"));
        public IWebElement multiplyButton => _driver.FindElement(By.XPath("//div/button[text()='×']"));
        public IWebElement minusButton => _driver.FindElement(By.XPath("//div/button[text()='−']"));
        public IWebElement plusButton => _driver.FindElement(By.XPath("//div/button[text()='+']"));
        public IWebElement oneButton => _driver.FindElement(By.XPath("//div/button[text()='1']"));
        public IWebElement twoButton => _driver.FindElement(By.XPath("//div/button[text()='2']"));
        public IWebElement threeButton => _driver.FindElement(By.XPath("(//button[@onclick=\"append('0')\"])[1]"));
        public IWebElement zeroButton => _driver.FindElement(By.XPath("//button[@onclick=\"append('0')\" and text()='0']"));

        public IWebElement fourButton => _driver.FindElement(By.XPath("//div/button[text()='4']"));
        public IWebElement fiveButton => _driver.FindElement(By.XPath("//div/button[text()='5']"));
        public IWebElement sixButton => _driver.FindElement(By.XPath("//div/button[text()='6']"));
        public IWebElement sevenButton => _driver.FindElement(By.XPath("//div/button[text()='7']"));
        public IWebElement eightButton => _driver.FindElement(By.XPath("//div/button[text()='8']"));
        public IWebElement nineButton => _driver.FindElement(By.XPath("//div/button[text()='9']"));
        public IWebElement dotButton => _driver.FindElement(By.XPath("//div/button[text()='.']"));
        public IWebElement equalButton => _driver.FindElement(By.XPath("//div/button[text()='=']"));
        public IWebElement sinButton => _driver.FindElement(By.XPath("//div/button[text()='sin']"));
        public IWebElement cosButton => _driver.FindElement(By.XPath("//div/button[text()='cos']"));
        public IWebElement tanButton => _driver.FindElement(By.XPath("//div/button[text()='tan']"));
        public IWebElement logButton => _driver.FindElement(By.XPath("//div/button[text()='log']"));
        public IWebElement sqrtButton => _driver.FindElement(By.XPath("//div/button[text()='√']"));
        public object[] GetControlInfo(string key)
        {
            Dictionary<string, object[]> controls = new Dictionary<string, object[]>();
            controls.Add("cancelButton", new object[] { "C", "Button", "Click", By.XPath("//div/button[text()='C']") });
            controls.Add("leftParanthesisButton", new object[] { "(", "Button", "Click", By.XPath("//div/button[text()='(']") });
            controls.Add("rightParanthesisButton", new object[] { ")", "Button", "Click", By.XPath("//div/button[text()=')']") });
            controls.Add("divideButton", new object[] { "÷", "Button", "Click", By.XPath("//div/button[text()='÷']") });
            controls.Add("multiplyButton", new object[] { "×", "Button", "Click", By.XPath("//div/button[text()='×']") });
            controls.Add("minusButton", new object[] { "-", "Button", "Click", By.XPath("//div/button[text()='-']") });
            controls.Add("plusButton", new object[] { "+", "Button", "Click", By.XPath("//div/button[text()='+']") });
            controls.Add("zeroButton", new object[] { "0", "Button", "Click", By.XPath("//div/button[text()='0']") });
            controls.Add("oneButton", new object[] { "1", "Button", "Click", By.XPath("//div/button[text()='1']") });
            controls.Add("twoButton", new object[] { "2", "Button", "Click", By.XPath("//div/button[text()='2']") });
            controls.Add("threeButton", new object[] { "3", "Button", "Click", By.XPath("//div/button[text()='3']") });
            controls.Add("fourButton", new object[] { "4", "Button", "Click", By.XPath("//div/button[text()='4']") });
            controls.Add("fiveButton", new object[] { "5", "Button", "Click", By.XPath("//div/button[text()='5']") });
            controls.Add("sixButton", new object[] { "6", "Button", "Click", By.XPath("//div/button[text()='6']") });
            controls.Add("sevenButton", new object[] { "7", "Button", "Click", By.XPath("//div/button[text()='7']") });
            controls.Add("eightButton", new object[] { "8", "Button", "Click", By.XPath("//div/button[text()='8']") });
            controls.Add("nineButton", new object[] { "9", "Button", "Click", By.XPath("//div/button[text()='9']") });
            controls.Add("dotButton", new object[] { ".", "Button", "Click", By.XPath("//div/button[text()='.']") });
            controls.Add("equalButton", new object[] { "=", "Button", "Click", By.XPath("//div/button[text()='=']") });
            controls.Add("sinButton", new object[] { "Sin", "Button", "Click", By.XPath("//div/button[text()='sin']") });
            controls.Add("cosButton", new object[] { "Cos", "Button", "Click", By.XPath("//div/button[text()='cos']") });
            controls.Add("tanButton", new object[] { "Tan", "Button", "Click", By.XPath("//div/button[text()='tan']") });
            controls.Add("logButton", new object[] { "Log", "Button", "Click", By.XPath("//div/button[text()='log']") });
            controls.Add("sqrtButton", new object[] { "v", "Button", "Click", By.XPath("//div/button[text()='v']") });
            if (controls.ContainsKey(key))
                return controls[key];
            else
                return null;
        }

        public IWebElement GetWebElement(string key)
        {
            Dictionary<string, IWebElement> elementDictionary = new Dictionary<string, IWebElement>();
            elementDictionary.Add("cancelButton", cancelButton);
            elementDictionary.Add("leftParanthesisButton", leftParanthesisButton);
            elementDictionary.Add("rightParanthesisButton", rightParanthesisButton);
            elementDictionary.Add("divideButton", divideButton);
            elementDictionary.Add("multiplyButton", multiplyButton);
            elementDictionary.Add("minusButton", minusButton);
            elementDictionary.Add("plusButton", plusButton);
            elementDictionary.Add("zeroButton", zeroButton);
            elementDictionary.Add("oneButton", oneButton);
            elementDictionary.Add("twoButton", twoButton);
            elementDictionary.Add("threeButton", threeButton);
            elementDictionary.Add("fourButton", fourButton);
            elementDictionary.Add("fiveButton", fiveButton);
            elementDictionary.Add("sixButton", sixButton);
            elementDictionary.Add("sevenButton", sevenButton);
            elementDictionary.Add("eightButton", eightButton);
            elementDictionary.Add("nineButton", nineButton);
            elementDictionary.Add("dotButton", dotButton);
            elementDictionary.Add("equalButton", equalButton);
            elementDictionary.Add("sinButton", sinButton);
            elementDictionary.Add("cosButton", cosButton);
            elementDictionary.Add("tanButton", tanButton);
            elementDictionary.Add("logButton", logButton);
            elementDictionary.Add("sqrtButton", sqrtButton);
            return elementDictionary.TryGetValue(key, out IWebElement webElement) ? webElement : null;
        }

    }
}

