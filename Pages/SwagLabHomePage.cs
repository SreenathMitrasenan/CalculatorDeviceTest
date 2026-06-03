
using OpenQA.Selenium;
namespace EmployeeManagement.StepDefinitions
{


public class SwagLabHomePage
{
	private readonly IWebDriver _driver;
	public SwagLabHomePage(IWebDriver driver)  => _driver = driver;

	public IWebElement burgerButton => _driver.FindElement(By.Id( "react-burger-menu-btn" ));
	public IWebElement allItems  => _driver.FindElement(By.Id( "inventory_sidebar_link" ));
	public IWebElement about => _driver.FindElement(By.Id( "about_sidebar_link" ));
	public IWebElement logout => _driver.FindElement(By.Id( "logout_sidebar_link" ));
	public IWebElement resetAppState => _driver.FindElement(By.Id( "reset_sidebar_link" ));
	public IWebElement shoppingContainer => _driver.FindElement(By.Id( "shopping_cart_container" ));
	public IWebElement sauceLabsBackPack_addToCart => _driver.FindElement(By.XPath( "//div[contains(text(),'Sauce Labs Backpack')]//ancestor::div[@class='inventory_item_description']//button[text()='Add to cart']" ));
	public object[] GetControlInfo(string key)
	{
		Dictionary<string, object[]> controls = new Dictionary<string, object[]>();
		controls.Add("burgerButton", new object[]{"Left Burger Button", "Button", "Click", By.Id("react-burger-menu-btn")});
		controls.Add("allItems ", new object[]{"All Items", "Button", "Click", By.Id("inventory_sidebar_link")});
		controls.Add("about", new object[]{"About", "Button", "Click", By.Id("about_sidebar_link")});
		controls.Add("logout", new object[]{"Logout", "Button", "Click", By.Id("logout_sidebar_link")});
		controls.Add("resetAppState", new object[]{"Reset App State", "Button", "Click", By.Id("reset_sidebar_link")});
		controls.Add("shoppingContainer", new object[]{"shopping_cart_container", "Label", "Click", By.Id("shopping_cart_container")});
		controls.Add("sauceLabsBackPack_addToCart", new object[]{"Sauce Labs Backpack_ Add cart", "Button", "Click", By.XPath("//div[contains(text(),'Sauce Labs Backpack')]//ancestor::div[@class='inventory_item_description']//button[text()='Add to cart']")});
	if (controls.ContainsKey(key))
	return controls[key];
	else 
	return null; 
	}

	public IWebElement GetWebElement(string key)
	{
		Dictionary<string, IWebElement> elementDictionary = new Dictionary<string, IWebElement>();
		elementDictionary.Add("burgerButton", burgerButton);
		elementDictionary.Add("allItems ", allItems );
		elementDictionary.Add("about", about);
		elementDictionary.Add("logout", logout);
		elementDictionary.Add("resetAppState", resetAppState);
		elementDictionary.Add("shoppingContainer", shoppingContainer);
		elementDictionary.Add("sauceLabsBackPack_addToCart", sauceLabsBackPack_addToCart);
	  return elementDictionary.TryGetValue(key, out IWebElement webElement) ? webElement : null;
	}

}
}

