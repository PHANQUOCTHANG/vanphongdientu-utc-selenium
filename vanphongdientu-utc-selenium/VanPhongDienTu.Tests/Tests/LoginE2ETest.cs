using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using VanPhongDienTu.Tests.Base;
using VanPhongDienTu.Tests.Pages;

namespace VanPhongDienTu.Tests.Tests;

[AllureEpic("Van phong dien tu UTC")]
[AllureFeature("Dang nhap")]
public class LoginE2ETest : BaseTest
{
    [Test]
    [AllureStory("Hien thi form dang nhap")]
    [Description("TC01 - Mo trang dang nhap va kiem tra cac phan tu chinh")]
    public void TC01_OpenLoginPage_ShouldDisplayLoginForm()
    {
        var loginPage = new LoginPage(Driver).Open();

        Assert.That(Driver.Title, Is.EqualTo("Đăng nhập"));
        Assert.That(Driver.Url, Does.Contain("/Login"));
        Assert.That(loginPage.Find(LoginPage.UsernameInput).GetAttribute("placeholder"), Is.EqualTo("Tên đăng nhập"));
        Assert.That(loginPage.Find(LoginPage.PasswordInput).GetAttribute("placeholder"), Is.EqualTo("Mật khẩu"));
        Assert.That(loginPage.Find(LoginPage.LoginButton).GetAttribute("value"), Is.EqualTo("Đăng nhập"));
        Assert.That(loginPage.Find(LoginPage.Heading).Text, Is.EqualTo("Không chỉ là một giải pháp quản lý"));
        Assert.That(loginPage.Find(LoginPage.ForgotPasswordLink).Displayed, Is.True);
    }

    [Test]
    [AllureStory("Validate form dang nhap")]
    [Description("TC02 - Dang nhap khi bo trong ten dang nhap")]
    public void TC02_LoginWithEmptyUsername_ShouldShowUsernameRequiredError()
    {
        var loginPage = new LoginPage(Driver).Open();
        loginPage.ClickLogin();

        Assert.That(loginPage.GetErrorText(), Is.EqualTo("Bạn chưa nhập tên đăng nhập"));
        Assert.That(Driver.Url, Does.Contain("/Login"));
    }

    [Test]
    [AllureStory("Validate form dang nhap")]
    [Description("TC03 - Dang nhap khi bo trong mat khau")]
    public void TC03_LoginWithEmptyPassword_ShouldShowPasswordRequiredError()
    {
        var loginPage = new LoginPage(Driver).Open();
        loginPage.EnterUsername("testuser");
        loginPage.ClickLogin();

        Assert.That(loginPage.GetErrorText(), Is.EqualTo("Bạn chưa nhập mật khẩu"));
        Assert.That(Driver.Url, Does.Contain("/Login"));
    }

    [Test]
    [AllureStory("Dang nhap that bai")]
    [Description("TC04 - Dang nhap voi tai khoan hoac mat khau khong dung")]
    public void TC04_LoginWithInvalidCredentials_ShouldShowInvalidAccountError()
    {
        var loginPage = new LoginPage(Driver).Open();
        loginPage.EnterUsername("invalid_user_xyz");
        loginPage.EnterPassword("wrong_password_xyz");
        loginPage.ClickLogin();

        Assert.That(loginPage.GetErrorText(), Is.EqualTo("Tài khoản hoặc mật khẩu không đúng."));
        Assert.That(Driver.Url, Does.Contain("/Login"));
    }

    [Test]
    [AllureStory("Giu dang nhap")]
    [Description("TC05 - Tick checkbox Giu toi luon dang nhap")]
    public void TC05_ToggleKeepMeSignedIn_ShouldCheckPersistentCheckbox()
    {
        var loginPage = new LoginPage(Driver).Open();

        Assert.That(loginPage.IsKeepMeSignedInChecked(), Is.False);
        loginPage.ToggleKeepMeSignedIn();
        Assert.That(loginPage.IsKeepMeSignedInChecked(), Is.True);
    }

    [Test]
    [AllureStory("Dieu huong quen mat khau")]
    [Description("TC06 - Chuyen sang trang quen mat khau")]
    public void TC06_ClickForgotPassword_ShouldOpenGetPassPage()
    {
        var loginPage = new LoginPage(Driver).Open();
        var forgotPasswordPage = loginPage.ClickForgotPassword();

        Assert.That(Driver.Url, Does.Contain("/Login/GetPass").Or.Contain("/Login/Getpass"));
        Assert.That(Driver.Title, Is.EqualTo("Lấy lại mật khẩu"));
        Assert.That(forgotPasswordPage.Find(ForgotPasswordPage.CaptchaInput).GetAttribute("placeholder"), Is.EqualTo("Mã bảo mật"));
        Assert.That(forgotPasswordPage.Find(ForgotPasswordPage.EmailInput).GetAttribute("placeholder"), Is.EqualTo("Địa chỉ Email"));
        Assert.That(forgotPasswordPage.Find(ForgotPasswordPage.SubmitButton).GetAttribute("value"), Is.EqualTo("Cập nhật"));
        Assert.That(forgotPasswordPage.Find(ForgotPasswordPage.CaptchaImage).Displayed, Is.True);
    }

    [Test]
    [AllureStory("Dang nhap bang email UTC")]
    [Description("TC09 - Link dang nhap bang e-mail UTC tro toi Google OAuth")]
    public void TC09_GoogleLoginLink_ShouldPointToGoogleOAuth()
    {
        var loginPage = new LoginPage(Driver).Open();
        var href = loginPage.Find(LoginPage.GoogleLoginLink).GetAttribute("href") ?? string.Empty;

        Assert.That(loginPage.Find(LoginPage.GoogleLoginLink).Text.Trim(), Is.EqualTo("Đăng nhập bằng e-mail UTC"));
        Assert.That(href, Does.Contain("accounts.google.com"));
        Assert.That(href, Does.Contain("oauth2"));
        Assert.That(href, Does.Contain("vanphongdientu.utc.edu.vn"));
    }

    [Test]
    [AllureStory("Footer")]
    [Description("TC10 - Link Trung tam tro giup va Y kien phan hoi")]
    public void TC10_FooterLinks_ShouldPointToHelpCenterAndFeedbackMail()
    {
        var loginPage = new LoginPage(Driver).Open();
        var help = loginPage.Find(LoginPage.HelpCenterLink);
        var feedback = loginPage.Find(LoginPage.FeedbackLink);

        Assert.That(help.Text.Trim(), Is.EqualTo("Trung tâm trợ giúp"));
        Assert.That(help.GetAttribute("href"), Does.StartWith("http://hotrokythuat.utc.edu.vn"));
        Assert.That(help.GetAttribute("target"), Is.EqualTo("_blank"));
        Assert.That(feedback.Text.Trim(), Is.EqualTo("Ý kiến phản hồi"));
        Assert.That(feedback.GetAttribute("href"), Is.EqualTo("mailto:hotrokythuat@utc.edu.vn"));
        Assert.That(loginPage.Find(LoginPage.Copyright).Text, Does.Contain("Trường ĐH Giao Thông Vận Tải"));
    }

}
