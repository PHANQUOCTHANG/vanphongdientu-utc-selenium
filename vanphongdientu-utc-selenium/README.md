# Kiểm thử Selenium – Văn phòng điện tử UTC

Project NUnit + Selenium WebDriver (C#) cho website [vanphongdientu.utc.edu.vn](https://vanphongdientu.utc.edu.vn).

Báo cáo dùng **Allure** (Allure.NUnit), CI/CD bằng **GitHub Actions**.

## Cấu trúc

```
VanPhongDienTu.Tests/
|-- Base/
|   |-- BaseTest.cs            // Khoi tao WebDriver, timeout, screenshot Allure
|   |-- DriverFactory.cs       // Chrome / Edge
|-- Pages/                     // Page Object
|   |-- BasePage.cs            // Wait, click, type
|   |-- LoginPage.cs           // Form dang nhap
|   |-- ForgotPasswordPage.cs  // Lay lai mat khau
|-- Tests/                     // Test scripts (NUnit)
|   |-- LoginE2ETest.cs
|   |-- ForgotPasswordE2ETest.cs
|-- Config/
|   |-- TestSettings.cs
|-- allureConfig.json
.github/workflows/ci.yml
docs/TestCases.md
```

## Yêu cầu

- .NET 8 SDK
- Google Chrome (mặc định) hoặc Microsoft Edge
- Java 17+ và [Allure Commandline](https://allurereport.org/docs/install/) nếu muốn xem report local
- Kết nối Internet tới `vanphongdientu.utc.edu.vn`

## Chạy test

```powershell
$env:BROWSER = "edge"
$env:HEADLESS = "true"
dotnet test VanPhongDienTu.Tests.sln
```

Chrome:

```powershell
$env:BROWSER = "chrome"
$env:HEADLESS = "true"
dotnet test VanPhongDienTu.Tests.sln
```

## Allure report

Sau khi chạy test, kết quả nằm ở thư mục `allure-results`.

```powershell
allure generate allure-results --clean -o allure-report
allure open allure-report
```

Hoặc:

```powershell
allure serve allure-results
```

Trên GitHub: tab **Actions** → workflow **CI** → artifact `allure-report`. Tải về, giải nén, mở `index.html`.

## CI/CD

Mỗi push/PR lên `main` sẽ:

1. Restore + `dotnet test` (Chrome headless)
2. Generate Allure report
3. Upload artifact `allure-report`, `allure-results`, `trx-results`
