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

}
