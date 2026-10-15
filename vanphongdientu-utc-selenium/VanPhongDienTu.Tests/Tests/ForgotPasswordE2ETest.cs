using Allure.NUnit.Attributes;
using VanPhongDienTu.Tests.Base;
using VanPhongDienTu.Tests.Pages;

namespace VanPhongDienTu.Tests.Tests;

[AllureEpic("Van phong dien tu UTC")]
[AllureFeature("Quen mat khau")]
public class ForgotPasswordE2ETest : BaseTest
{
    [Test]
    [AllureStory("Captcha khong hop le")]
    [Description("TC07 - Gui form quen mat khau voi captcha sai")]
    public void TC07_SubmitForgotPasswordWithInvalidCaptcha_ShouldShowError()
    {
        var forgotPasswordPage = new ForgotPasswordPage(Driver).Open();
        forgotPasswordPage.EnterCaptcha("0000");
        forgotPasswordPage.EnterEmail("abc@utc.edu.vn");
        forgotPasswordPage.ClickSubmit();

        Assert.That(forgotPasswordPage.GetErrorText(), Is.EqualTo("Mã bảo mật không chính xác"));
    }

    [Test]
    [AllureStory("Tro lai dang nhap")]
    [Description("TC08 - Tro lai dang nhap tu trang quen mat khau")]
    public void TC08_ClickBackToLogin_ShouldReturnToLoginPage()
    {
        var forgotPasswordPage = new ForgotPasswordPage(Driver).Open();
        var loginPage = forgotPasswordPage.ClickBackToLogin();

        Assert.That(Driver.Url, Does.Contain("/Login"));
        Assert.That(Driver.Title, Is.EqualTo("Đăng nhập"));
        Assert.That(loginPage.Find(LoginPage.UsernameInput).Displayed, Is.True);
        Assert.That(loginPage.Find(LoginPage.LoginButton).Displayed, Is.True);
    }

    [Test]
    [AllureStory("Giao dien")]
    [Description("TC18 - Trang quen mat khau hien thi logo")]
    public void TC18_ForgotPasswordPage_ShouldShowLogo()
    {
        var forgotPasswordPage = new ForgotPasswordPage(Driver).Open();

        Assert.That(forgotPasswordPage.Find(ForgotPasswordPage.Logo).Displayed, Is.True);
    }

    [Test]
    [AllureStory("Thuoc tinh form")]
    [Description("TC19 - Form quen mat khau post toi Getpass va co anh captcha")]
    public void TC19_ForgotPasswordForm_ShouldPostToGetpassWithCaptchaImage()
    {
        var forgotPasswordPage = new ForgotPasswordPage(Driver).Open();
        var form = forgotPasswordPage.Find(ForgotPasswordPage.ForgotForm);

        Assert.That(form.GetAttribute("action"), Does.EndWith("/Login/Getpass"));
        Assert.That(form.GetAttribute("method"), Is.EqualTo("post").IgnoreCase);
        Assert.That(forgotPasswordPage.Find(ForgotPasswordPage.CaptchaImage).Displayed, Is.True);
    }

}
