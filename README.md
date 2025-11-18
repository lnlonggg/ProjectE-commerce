# Hệ thống Thương mại điện tử (E-commerce System)

Đây là bài tập môn **Lập trình .NET Doanh nghiệp**, xây dựng một hệ thống quản lý thương mại điện tử hoàn chỉnh theo mô hình 3 tầng (3-Layer Architecture) kết hợp Web MVC và RESTful API.

### Thông tin sinh viên
* **Họ và tên:** Lý Ngọc Long
* **Mã sinh viên:** 22103100235

---

### Mục tiêu & Tính năng đã hoàn thành

Dự án xây dựng một hệ thống gồm 3 thành phần chính: **Core** (Xử lý nghiệp vụ), **Web MVC** (Trang quản trị), và **API** (Cung cấp dữ liệu).

### Các tính năng chính:
* **Kiến trúc 3 tầng (Clean Architecture):** Tách biệt rõ ràng Repository, Service, Controller và DTOs.
* **Quản lý (CRUD) toàn diện:** Người dùng, Danh mục (Category), Nhóm sản phẩm (Group), Sản phẩm (Product).
* **Bảo mật:**
    * Xác thực người dùng bằng **Cookie** (cho Web Admin) và **JWT Bearer** (cho API).
    * Mật khẩu được mã hóa an toàn bằng **BCrypt**.
* **RESTful API:** Cung cấp đầy đủ các endpoints, hỗ trợ phân trang, tìm kiếm và tài liệu hóa bằng **Swagger**.
* **Web Admin:** Giao diện quản trị trực quan, tích hợp phân trang Server-side.
* **Tự động hóa:** Seed dữ liệu mẫu (Admin, Danh mục, Sản phẩm) khi khởi chạy lần đầu.

---

## Cài đặt và chạy

### 1. Yêu cầu môi trường
* .NET 9 SDK.
* SQL Server (LocalDB hoặc bản Full).

### 2. Cấu hình Database
Mở file `appsettings.json` trong cả 2 thư mục `MyShop.Web` và `MyShop.Api`, cập nhật chuỗi kết nối `DefaultConnection` trỏ đến SQL Server của bạn.

### 3. Khởi tạo Database và Dữ liệu mẫu
Mở Terminal tại thư mục gốc của dự án và chạy lệnh sau để tạo database và nạp dữ liệu mẫu (Admin + 20 sản phẩm):

```bash
dotnet ef database update --startup-project MyShop.Web -p MyShop.Core
```

### 4.Chạy dự án
Chạy trang Web Admin (MVC):
```bash
dotnet run --project MyShop.Web
```
Truy cập: https://localhost:xxxx (Cổng sẽ hiện trong terminal)

Chạy API (REST):

```bash
dotnet run --project MyShop.Api
```
Truy cập Swagger: https://localhost:yyyy/swagger

### Tài khoản Admin (Mặc định)
Hệ thống sẽ tự động tạo tài khoản này khi chạy lần đầu :
## Username: admin
## Password: Admin@123
---
