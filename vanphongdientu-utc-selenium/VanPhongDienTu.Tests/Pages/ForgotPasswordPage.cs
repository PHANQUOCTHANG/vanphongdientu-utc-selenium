using Allure.Net.Commons;
using OpenQA.Selenium;
using VanPhongDienTu.Tests.Config;

namespace VanPhongDienTu.Tests.Pages;

public class ForgotPasswordPage : BasePage
{
    public static readonly By CaptchaImage = By.CssSelector("img[src='/login/index/captcha']");
    public static readonly By CaptchaInput = By.Name("captcha");
    public static readonly By EmailInput = By.Name("email");
    public static readonly By SubmitButton = By.CssSelector("input[type='submit'][value='Cập nhật']");
    public static readonly By BackToLoginLink = By.CssSelector("div.helps a");
    public static readonly By ErrorMessage = By.CssSelector("div.error");
    public static readonly By Logo = By.CssSelector("img.logo");
    public static readonly By Heading = By.CssSelector("h1");
    public static readonly By ForgotForm = By.CssSelector("form[action='/Login/Getpass']");
    public static readonly By HelpCenterLink = By.CssSelector("div.footer a[href='http://hotrokythuat.utc.edu.vn']");
    public static readonly By FeedbackLink = By.CssSelector("div.footer a[href^='mailto:']");
    public static readonly By Copyright = By.CssSelector(".footer .left span.a");

    public ForgotPasswordPage(IWebDriver driver) : base(driver)
    {
    }

    public ForgotPasswordPage Open()
    {
        AllureApi.Step("Mo trang lay lai mat khau", () =>
        {
            Driver.Navigate().GoToUrl(TestSettings.ForgotPasswordUrl);
            WaitVisible(CaptchaInput);
        });
        return this;
    }

    public ForgotPasswordPage EnterCaptcha(string captcha)
    {
        AllureApi.Step($"Nhap ma bao mat: {captcha}", () => Type(CaptchaInput, captcha));
        return this;
    }

    public ForgotPasswordPage EnterEmail(string email)
    {
        AllureApi.Step($"Nhap email: {email}", () => Type(EmailInput, email));
        return this;
    }

    public ForgotPasswordPage ClickSubmit()
    {
        AllureApi.Step("Bam nut Cap nhat", () => Click(SubmitButton));
        return this;
    }

    public string GetErrorText()
    {
        return GetText(ErrorMessage);
    }

    public LoginPage ClickBackToLogin()
    {
        AllureApi.Step("Bam Tro lai dang nhap", () =>
        {
            Click(BackToLoginLink);
            WaitVisible(LoginPage.UsernameInput);
        });
        return new LoginPage(Driver);
    }
}
