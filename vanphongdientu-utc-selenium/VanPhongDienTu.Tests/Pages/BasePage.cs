using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using VanPhongDienTu.Tests.Config;

namespace VanPhongDienTu.Tests.Pages;

public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    protected readonly WebDriverWait Wait;

    protected BasePage(IWebDriver driver)
    {
        Driver = driver;
        Wait = new WebDriverWait(driver, TestSettings.DefaultTimeout);
    }

    protected IWebElement WaitVisible(By locator)
    {
        return Wait.Until(driver =>
        {
            var element = driver.FindElement(locator);
            return element.Displayed ? element : null;
        })!;
    }

    protected void Click(By locator)
    {
        WaitVisible(locator).Click();
    }

    protected void Type(By locator, string text)
    {
        var input = WaitVisible(locator);
        input.Clear();
        input.SendKeys(text);
    }

    protected string GetText(By locator)
    {
        return WaitVisible(locator).Text.Trim();
    }

    public IWebElement Find(By locator)
    {
        return WaitVisible(locator);
    }

    public bool WaitUrlContains(string fragment)
    {
        return Wait.Until(driver =>
            driver.Url.Contains(fragment, StringComparison.OrdinalIgnoreCase));
    }
}
