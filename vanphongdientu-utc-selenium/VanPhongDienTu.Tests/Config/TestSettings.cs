namespace VanPhongDienTu.Tests.Config;

public static class TestSettings
{
    public const string BaseUrl = "https://vanphongdientu.utc.edu.vn";
    public const string LoginUrl = BaseUrl + "/Login";
    public const string ForgotPasswordUrl = BaseUrl + "/Login/GetPass";
    public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(15);
}
