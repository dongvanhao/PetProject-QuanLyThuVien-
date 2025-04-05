# 📚 Hệ Thống Quản Lý Thư Viện

## 🎯 Mục Tiêu
Xây dựng hệ thống API hỗ trợ:
- Quản lý sách, thủ thư và người mượn
- Xử lý mượn/trả sách, tìm kiếm sách theo nhiều tiêu chí
- Thống kê và phân tích dữ liệu mượn sách

---

## 🛠️ Tính Năng Chính

### 📘 Quản Lý Sách
- Thêm, sửa, xoá sách
- Tra cứu sách theo tiêu đề, thể loại, tác giả,...
- Phân trang & lọc nâng cao

### 📄 Mượn & Trả Sách
- Đăng ký mượn sách
- Trả sách và cập nhật trạng thái
- Theo dõi lịch sử mượn trả của người dùng

### 🔍 Tìm Kiếm & Thống Kê
- Tìm kiếm sách theo tiêu đề, thể loại, tác giả,...
- Phân trang kết quả tìm kiếm
- Thống kê **Top 5 sách được mượn nhiều nhất**

### 👤 Người Dùng
- Lấy danh sách sách đã mượn theo người dùng (`Include`)
- Lịch sử mượn sách và chi tiết theo dõi

---

## ⚙️ Công Nghệ Sử Dụng

| Công Nghệ               | Mô Tả                                                                 |
|------------------------|------------------------------------------------------------------------|
| **ASP.NET Core Web API**  | Nền tảng chính cho hệ thống backend                                    |
| **Entity Framework Core** | ORM để thao tác với cơ sở dữ liệu quan hệ                              |
| **AutoMapper**            | Ánh xạ giữa Entity và DTO giúp code sạch và dễ bảo trì                |
| **SQL Server**            | Hệ quản trị cơ sở dữ liệu chính                                        |
| **AsNoTracking**          | Truy vấn tối ưu không theo dõi để cải thiện hiệu năng với các `Include` |
| **Serilog / ILogger**     | Ghi log phục vụ theo dõi và debug                                     |

---

## ❗ Xử Lý Ngoại Lệ (Exception Handling)

> "Xử lý lỗi đúng cách là chìa khóa giữ cho hệ thống ổn định và dễ bảo trì."

### 🎯 Mục Tiêu
- Cung cấp phản hồi rõ ràng cho client
- Tránh để lộ thông tin nội bộ
- Ghi lại log cho các lỗi phát sinh

### ✅ Mẫu Exception Handling được sử dụng
- Sử dụng **Middleware toàn cục** (`UseExceptionHandler`) để bắt và xử lý mọi lỗi
- Tạo các `CustomException` như:
  - `NotFoundException`
  - `BadRequestException`
  - `ConflictException`
- Trả về `ErrorResponse` chuẩn hóa cho mọi lỗi
