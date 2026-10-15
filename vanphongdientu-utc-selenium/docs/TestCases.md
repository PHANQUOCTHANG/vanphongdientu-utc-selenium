# Test case – Văn phòng điện tử UTC

**Ứng dụng:** [https://vanphongdientu.utc.edu.vn](https://vanphongdientu.utc.edu.vn)  
**Phạm vi:** trang Đăng nhập và trang Lấy lại mật khẩu (các màn công khai, không cần tài khoản thật)  
**Công cụ tự động hóa:** Selenium WebDriver + NUnit, ngôn ngữ C#

Locator lấy trực tiếp từ HTML của website (không dùng XPath tuyệt đối).

## Locators

### Trang đăng nhập (`/Login`)

| Phần tử | Locator | Giá trị HTML |
| --- | --- | --- |
| Form | `form[action='/Login']` | `method="post"` |
| Tên đăng nhập | `name="username"` | `placeholder="Tên đăng nhập"` |
| Mật khẩu | `name="userpwd"` | `placeholder="Mật khẩu"`, `type="password"` |
| Giữ tôi luôn đăng nhập | `id="persistent"` / `label.check` | checkbox ẩn, click nhãn |
| Nút Đăng nhập | `input.submit_login` | `value="Đăng nhập"` |
| Đăng nhập bằng e-mail UTC | `a.button` | Google OAuth |
| Quên mật khẩu | `div.helps a` | `href="/Login/GetPass"` |
| Thông báo lỗi | `div.error` | text thay đổi theo case |
| Tiêu đề | `h1` | Không chỉ là một giải pháp quản lý |
| Phụ đề | `.caption span` | Làm việc mọi lúc mọi nơi |
| Ảnh minh họa | `div.left img` | flight.png |
| Bản quyền | `.footer .left span.a` | Trường ĐH Giao Thông Vận Tải © 2026 |
| Trung tâm trợ giúp | footer `a[href='http://hotrokythuat.utc.edu.vn']` | `target="_blank"` |
| Ý kiến phản hồi | footer `a[href^='mailto:']` | `mailto:hotrokythuat@utc.edu.vn` |

### Trang lấy lại mật khẩu (`/Login/GetPass`)

| Phần tử | Locator | Giá trị HTML |
| --- | --- | --- |
| Form | `form[action='/Login/Getpass']` | `method="post"` |
| Logo | `img.logo` | logo.png |
| Ảnh captcha | `img[src='/login/index/captcha']` | |
| Mã bảo mật | `name="captcha"` | `placeholder="Mã bảo mật"` |
| Email | `name="email"` | `placeholder="Địa chỉ Email"` |
| Nút Cập nhật | `input[type='submit'][value='Cập nhật']` | |
| Trở lại đăng nhập | `div.helps a` | `href="/Login"` |
| Thông báo lỗi | `div.error` | ví dụ: Mã bảo mật không chính xác |
| Trung tâm trợ giúp | footer `a[href='http://hotrokythuat.utc.edu.vn']` | `target="_blank"` |
| Ý kiến phản hồi | footer `a[href^='mailto:']` | `mailto:hotrokythuat@utc.edu.vn` |

## Danh sách test case

| ID | Tên | Tiền điều kiện | Các bước | Dữ liệu | Kết quả mong đợi |
| --- | --- | --- | --- | --- | --- |
| TC01 | Mở trang đăng nhập | Có kết nối Internet | 1. Mở `/Login` | — | Title = `Đăng nhập`. Có ô username, password, nút Đăng nhập, heading, link quên mật khẩu |
| TC02 | Đăng nhập bỏ trống tên đăng nhập | Đang ở `/Login` | 1. Để trống username và password 2. Bấm Đăng nhập | username= rỗng, password= rỗng | `div.error` = `Bạn chưa nhập tên đăng nhập`. Vẫn ở `/Login` |
| TC03 | Đăng nhập bỏ trống mật khẩu | Đang ở `/Login` | 1. Nhập username 2. Để trống password 3. Bấm Đăng nhập | username=`testuser`, password= rỗng | `div.error` = `Bạn chưa nhập mật khẩu`. Vẫn ở `/Login` |
| TC04 | Đăng nhập sai tài khoản/mật khẩu | Đang ở `/Login` | 1. Nhập username sai 2. Nhập password sai 3. Bấm Đăng nhập | username=`invalid_user_xyz`, password=`wrong_password_xyz` | `div.error` = `Tài khoản hoặc mật khẩu không đúng.` Vẫn ở `/Login` |
| TC05 | Tick Giữ tôi luôn đăng nhập | Đang ở `/Login` | 1. Click `label.check` | — | Checkbox `#persistent` chuyển sang trạng thái selected |
| TC06 | Chuyển sang trang quên mật khẩu | Đang ở `/Login` | 1. Click `Bạn quên mật khẩu đăng nhập ?` | — | URL chứa `/Login/GetPass`. Title = `Lấy lại mật khẩu`. Có captcha, email, nút Cập nhật |
| TC07 | Gửi form quên mật khẩu với captcha sai | Đang ở `/Login/GetPass` | 1. Nhập captcha giả 2. Nhập email 3. Bấm Cập nhật | captcha=`0000`, email=`abc@utc.edu.vn` | `div.error` = `Mã bảo mật không chính xác` |
| TC08 | Trở lại đăng nhập từ trang quên mật khẩu | Đang ở `/Login/GetPass` | 1. Click `Trở lại đăng nhập?` | — | Quay về `/Login`, title `Đăng nhập`, ô username hiển thị |
| TC09 | Link đăng nhập bằng e-mail UTC | Đang ở `/Login` | 1. Đọc `href` của `a.button` | — | Text = `Đăng nhập bằng e-mail UTC`. `href` chứa `accounts.google.com`, `oauth2`, `vanphongdientu.utc.edu.vn` |
| TC10 | Link trợ giúp và phản hồi | Đang ở `/Login` | 1. Đọc `href`/`target` footer | — | Trợ giúp = `http://hotrokythuat.utc.edu.vn` (`_blank`). Phản hồi = `mailto:hotrokythuat@utc.edu.vn`. Bản quyền chứa tên trường |
| TC11 | Thuộc tính form đăng nhập | Đang ở `/Login` | 1. Đọc `action`/`method` của form | — | `action` kết thúc bằng `/Login`, `method` = `post` |
| TC12 | Ô mật khẩu được che | Đang ở `/Login` | 1. Đọc `type` của ô mật khẩu | — | `type` = `password` |
| TC13 | Khối giới thiệu bên trái | Đang ở `/Login` | 1. Đọc heading, phụ đề, ảnh | — | Heading = `Không chỉ là một giải pháp quản lý`, phụ đề = `Làm việc mọi lúc mọi nơi`, ảnh hiển thị |
| TC14 | Checkbox giữ đăng nhập mặc định | Đang ở `/Login` | 1. Đọc trạng thái + thuộc tính checkbox | — | Mặc định chưa tick. `name` = `persistent`, `value` = `1` |
| TC15 | Bật rồi tắt giữ đăng nhập | Đang ở `/Login` | 1. Click nhãn 2 lần | — | Sau lần 1: tick. Sau lần 2: bỏ tick |
| TC16 | Bản quyền footer | Đang ở `/Login` | 1. Đọc text bản quyền | — | Chứa `Trường ĐH Giao Thông Vận Tải` và `2026` |
| TC17 | Mở trang chủ chuyển về đăng nhập | Có kết nối Internet | 1. Mở `/` | — | Title = `Đăng nhập`, form và ô username hiển thị |
| TC18 | Logo trên trang quên mật khẩu | Đang ở `/Login/GetPass` | 1. Kiểm tra logo | — | `img.logo` hiển thị |
| TC19 | Thuộc tính form quên mật khẩu | Đang ở `/Login/GetPass` | 1. Đọc `action`/`method` + ảnh captcha | — | `action` kết thúc bằng `/Login/Getpass`, `method` = `post`, ảnh captcha hiển thị |
| TC20 | Footer trang quên mật khẩu | Đang ở `/Login/GetPass` | 1. Đọc link trợ giúp và phản hồi | — | Trợ giúp = `http://hotrokythuat.utc.edu.vn` (`_blank`). Phản hồi = `mailto:hotrokythuat@utc.edu.vn` |

## Ghi chú

- Không tự động hóa đăng nhập thành công vì không dùng tài khoản thật của nhà trường.
- TC11–TC20 kiểm tra cấu trúc DOM, thuộc tính và điều hướng (kết quả xác định), không phụ thuộc phản hồi động của server.
- Thông báo lỗi (TC02–TC04, TC07) được xác nhận bằng cách thao tác thật trên website trước khi viết script.
