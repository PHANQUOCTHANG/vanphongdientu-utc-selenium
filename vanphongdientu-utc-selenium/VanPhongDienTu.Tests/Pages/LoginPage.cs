using Allure.Net.Commons;
using OpenQA.Selenium;
using VanPhongDienTu.Tests.Config;

namespace VanPhongDienTu.Tests.Pages;

public class LoginPage : BasePage
{
    public static readonly By UsernameInput = By.Name("username");
    public static readonly By PasswordInput = By.Name("userpwd");
    public static readonly By PersistentCheckbox = By.Id("persistent");
    public static readonly By PersistentLabel = By.CssSelector("label.check");
    public static readonly By LoginButton = By.CssSelector("input.submit_login");
    public static readonly By GoogleLoginLink = By.CssSelector("a.button");
    public static readonly By ForgotPasswordLink = By.CssSelector("div.helps a");
    public static readonly By HelpCenterLink = By.CssSelector("div.footer a[href='http://hotrokythuat.utc.edu.vn']");
    public static readonly By FeedbackLink = By.CssSelector("div.footer a[href^='mailto:']");
    public static readonly By ErrorMessage = By.CssSelector("div.error");
    public static readonly By Heading = By.CssSelector("h1");
    public static readonly By Caption = By.CssSelector(".caption span");
    public static readonly By Copyright = By.CssSelector(".footer .left span.a");
    public static readonly By LoginForm = By.CssSelector("form[action='/Login']");
    public static readonly By LeftImage = By.CssSelector("div.left img");

    public LoginPage(IWebDriver driver) : base(driver)
    {
    }

    public LoginPage Open()
    {
        AllureApi.Step("Mo trang dang nhap", () =>
        {
            Driver.Navigate().GoToUrl(TestSettings.LoginUrl);
            WaitVisible(UsernameInput);
        });
        return this;
    }

    public LoginPage EnterUsername(string username)
    {
        AllureApi.Step($"Nhap ten dang nhap: {username}", () => Type(UsernameInput, username));
        return this;
    }

    public LoginPage EnterPassword(string password)
    {
        AllureApi.Step("Nhap mat khau", () => Type(PasswordInput, password));
        return this;
    }

    public LoginPage ClickLogin()
    {
        AllureApi.Step("Bam nut Dang nhap", () => Click(LoginButton));
        return this;
    }

    public LoginPage ToggleKeepMeSignedIn()
    {
        AllureApi.Step("Tick Giu toi luon dang nhap", () => Click(PersistentLabel));
        return this;
    }

    public bool IsKeepMeSignedInChecked()
    {
        return Driver.FindElement(PersistentCheckbox).Selected;
    }

    public string? GetPersistentAttribute(string name)
    {
        return Driver.FindElement(PersistentCheckbox).GetAttribute(name);
    }

    public LoginPage OpenBaseUrl()
    {
        AllureApi.Step("Mo trang chu", () =>
        {
            Driver.Navigate().GoToUrl(TestSettings.BaseUrl);
            WaitVisible(UsernameInput);
        });
        return this;
    }

    public string GetErrorText()
    {
        return GetText(ErrorMessage);
    }

    public ForgotPasswordPage ClickForgotPassword()
    {
        AllureApi.Step("Bam link quen mat khau", () =>
        {
            Click(ForgotPasswordLink);
            WaitUrlContains("/Login/GetPass");
        });
        return new ForgotPasswordPage(Driver);
    }
}
