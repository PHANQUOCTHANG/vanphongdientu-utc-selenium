# Kiểm thử Selenium – Văn phòng điện tử UTC

Project kiểm thử tự động (C#) cho website [vanphongdientu.utc.edu.vn](https://vanphongdientu.utc.edu.vn),
tập trung vào **trang Đăng nhập** và **trang Lấy lại mật khẩu** (các màn công khai, không cần tài khoản thật).

- **Ngôn ngữ:** C# (.NET 8)
- **Framework test:** NUnit
- **Tự động hóa trình duyệt:** Selenium WebDriver (Chrome / Edge)
- **Mô hình:** Page Object Model (POM)
- **Báo cáo:** Allure (Allure.NUnit)
- **CI/CD:** GitHub Actions

Locator được lấy trực tiếp từ HTML thật của website (ưu tiên `name`, `id`, CSS selector; không dùng XPath tuyệt đối).

## Cấu trúc

```
VanPhongDienTu.Tests/
|-- Base/
|   |-- BaseTest.cs            // Khoi tao WebDriver, timeout, chup screenshot khi fail
|   |-- DriverFactory.cs       // Tao Chrome / Edge, ho tro headless
|-- Pages/                     // Page Object
|   |-- BasePage.cs            // Wait, click, type dung chung
|   |-- LoginPage.cs           // Trang dang nhap
|   |-- ForgotPasswordPage.cs  // Trang lay lai mat khau
|-- Tests/                     // Test script (NUnit)
|   |-- LoginE2ETest.cs        // TC01-TC06, TC09-TC17
|   |-- ForgotPasswordE2ETest.cs // TC07, TC08, TC18-TC20
|-- Config/
|   |-- TestSettings.cs        // URL, timeout
|-- allureConfig.json
.github/workflows/ci.yml       // Pipeline CI
docs/TestCases.md              // Dac ta locator + 20 test case
```

## Yêu cầu

- .NET 8 SDK
- Google Chrome (mặc định) hoặc Microsoft Edge
- Java 17+ và [Allure Commandline](https://allurereport.org/docs/install/) nếu muốn xem report ở máy
- Kết nối Internet tới `vanphongdientu.utc.edu.vn`

## Chạy test

Chrome (headless):

```powershell
$env:BROWSER = "chrome"
$env:HEADLESS = "true"
dotnet test VanPhongDienTu.Tests.sln
```

Edge:

```powershell
$env:BROWSER = "edge"
$env:HEADLESS = "true"
dotnet test VanPhongDienTu.Tests.sln
```

Bỏ biến `HEADLESS` (hoặc đặt `false`) để xem trình duyệt chạy trực tiếp.

## Danh sách test case

Chi tiết các bước và dữ liệu xem trong [docs/TestCases.md](docs/TestCases.md).

### Trang đăng nhập (`/Login`)

| ID | Nội dung |
| --- | --- |
| TC01 | Mở trang đăng nhập, kiểm tra form và các phần tử chính |
| TC02 | Báo lỗi khi bỏ trống tên đăng nhập |
| TC03 | Báo lỗi khi bỏ trống mật khẩu |
| TC04 | Báo lỗi khi sai tài khoản / mật khẩu |
| TC05 | Tick "Giữ tôi luôn đăng nhập" |
| TC06 | Điều hướng sang trang quên mật khẩu |
| TC09 | Link "Đăng nhập bằng e-mail UTC" trỏ tới Google OAuth |
| TC10 | Link Trung tâm trợ giúp và Ý kiến phản hồi ở footer |
| TC11 | Thuộc tính `action` / `method` của form đăng nhập |
| TC12 | Ô mật khẩu được che (`type=password`) |
| TC13 | Khối giới thiệu bên trái (heading, phụ đề, ảnh) |
| TC14 | Checkbox giữ đăng nhập mặc định chưa tick, đúng thuộc tính |
| TC15 | Bật rồi tắt giữ đăng nhập trở về chưa tick |
| TC16 | Bản quyền footer hiển thị năm và tên trường |
| TC17 | Mở trang chủ `/` chuyển về trang đăng nhập |

### Trang lấy lại mật khẩu (`/Login/GetPass`)

| ID | Nội dung |
| --- | --- |
| TC07 | Gửi form với captcha sai → báo lỗi |
| TC08 | Trở lại trang đăng nhập |
| TC18 | Hiển thị logo |
| TC19 | Thuộc tính form + ảnh captcha |
| TC20 | Link Trung tâm trợ giúp và Ý kiến phản hồi ở footer |

> Không tự động hóa đăng nhập thành công vì không dùng tài khoản thật của nhà trường.
> TC11–TC20 kiểm tra cấu trúc DOM, thuộc tính và điều hướng (kết quả xác định).

## Allure report

Sau khi chạy test, kết quả nằm ở thư mục `allure-results`.

```powershell
allure generate allure-results --clean -o allure-report
allure open allure-report
```

Hoặc xem nhanh:

```powershell
allure serve allure-results
```

Trên GitHub: tab **Actions** → workflow **CI** → artifact `allure-report`. Tải về, giải nén, mở `index.html`.

## CI/CD

Mỗi push / pull request lên `main` sẽ:

1. Restore + `dotnet test` (Chrome headless)
2. Generate Allure report
3. Upload artifact `allure-report`, `allure-results`, `trx-results`

## Quy ước commit

- 1 commit khởi tạo project (scaffold).
- Mỗi test case là 1 commit riêng (`test: TCxx - ...`), tổng cộng 21 commit.
