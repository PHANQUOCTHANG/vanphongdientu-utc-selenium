using Allure.Net.Commons;
using Allure.NUnit;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using VanPhongDienTu.Tests.Config;

namespace VanPhongDienTu.Tests.Base;

[AllureNUnit]
public abstract class BaseTest
{
    protected IWebDriver Driver = null!;

    [SetUp]
    public void SetUp()
    {
        Driver = DriverFactory.Create();
        Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        Driver.Manage().Timeouts().AsynchronousJavaScript = TestSettings.DefaultTimeout;
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            if (Driver is ITakesScreenshot camera &&
                TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                AllureApi.AddAttachment(
                    "Screenshot khi fail",
                    "image/png",
                    camera.GetScreenshot().AsByteArray,
                    ".png");
            }
        }
        finally
        {
            Driver?.Quit();
            Driver?.Dispose();
        }
    }
}
