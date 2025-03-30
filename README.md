Hệ thống Quản lý Thư viện  
🎯 Mục tiêu:  
- Xây dựng API cho quản lý sách, Thủ thư và người mượn.  
- Tạo các tính năng mượn trả sách, tìm kiếm sách theo nhiều tiêu chí.  
🛠 Các tính năng chính:  
- Sách: Thêm, sửa, xoá và tra cứu sách.  
- Mượn sách: Đăng ký mượn, trả sách, theo dõi lịch sử mượn trả.  
- Tìm kiếm & Phân trang: Hỗ trợ tìm kiếm sách theo tiêu đề, thể loại,...
- người dùng: lấy sách và danh sách lượt mượn(Include),Top 5 sách bán chạy nhất
🔧 Công nghệ & kỹ thuật:  
- Entity Framework Core: Kết nối và thao tác với database.  
- AutoMapper: Chuyển đổi giữa entity và DTO.  
- Logging & Exception Handling: Ghi log và xử lý lỗi.  
-AsNoTracking:EF theo dõi(track) các object đã truy vấn để tự động cập nhật chúng nếu ta sửa, khi dùng Include, các list như LoanRecords, User có thể bị track sẵn, gây lỗi hoặc hiệu năng chậm
