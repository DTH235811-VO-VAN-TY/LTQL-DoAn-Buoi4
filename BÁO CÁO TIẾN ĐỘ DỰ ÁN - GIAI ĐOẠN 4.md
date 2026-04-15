**BÁO CÁO TIẾN ĐỘ DỰ ÁN - GIAI ĐOẠN 4 (BUỔI 4)**

**1. Mục tiêu giai đoạn 4**

Tập trung nâng cấp và đồng bộ hóa toàn bộ giao diện các phân hệ quản lý (Sinh viên, Giảng viên, Lớp học), triển khai nghiệp vụ phức tạp theo mô hình Container - Chi tiết (Quản lý điểm), đồng thời thiết lập hệ thống ràng buộc dữ liệu (Validation) và tối ưu hóa khả năng truy xuất thông tin thông qua các tính năng tìm kiếm, sắp xếp nâng cao.

**2. Nội dung công việc đã hoàn thành**

**2.1. Xây dựng và nâng cấp hệ thống giao diện đa phân hệ**

Đã hoàn thiện và chuẩn hóa giao diện cho một loạt các biểu mẫu quan trọng, đảm bảo tính thống nhất về trải nghiệm người dùng:

•	Phân hệ quản lý cốt lõi: Xây dựng giao diện cho các trang Sinh viên, Giảng viên, Lớp học phần và Lớp hành chính. Các form đều được thiết lập thuộc tính StartPosision = CenterScreen để hiển thị cân đối giữa màn hình.

•	Phân hệ Quản lý điểm (Container - Chi tiết): Triển khai mô hình quản lý hai lớp tương tự như cấu trúc Hóa đơn - Hóa đơn chi tiết.

o	Trang Container: Hiển thị danh sách điểm tổng quát của sinh viên với cột "Xem chi tiết" sử dụng loại DataGridViewLinkColumn để điều hướng.

o	Trang Chi tiết: Cho phép xem và chỉnh sửa điểm thành phần, điểm thi của từng môn học cụ thể cho sinh viên được chọn.

•	Điều hướng và truyền dữ liệu: Đã lập trình cơ chế truyền mã định danh (như mã sv của sinh viên hoặc lớp) từ trang tổng quan sang trang chi tiết thông qua hàm khởi tạo (Constructor) để tải đúng dữ liệu cần xử lý.

**2.2. Hoàn thiện nghiệp vụ CRUD và xử lý dữ liệu thông minh**

•	Cập nhật ComboBox: Nâng cấp các hàm load dữ liệu cho ComboBox (Lớp, Khoa, Môn học) sử dụng các thuộc tính ValueMember và DisplayMember để đảm bảo tính chính xác khi liên kết dữ liệu.

•	Tự động hóa nhập liệu: Tích hợp sự kiện SelectionChangeCommitted khi chọn thông tin (ví dụ: chọn môn học) để hệ thống tự động tải các thông tin liên quan, giảm thiểu thao tác nhập tay cho người dùng.

•	Đồng bộ hóa CSDL: Sử dụng phương thức context.SaveChanges() để lưu trữ các thay đổi. Đối với nghiệp vụ cập nhật chi tiết điểm, hệ thống thực hiện xóa các bản ghi cũ và thêm lại bản ghi mới (RemoveRange và Add) để đảm bảo tính toàn vẹn của dữ liệu.

**2.3. Ràng buộc dữ liệu (Validation) và Tối ưu hóa truy xuất**

•	Kiểm tra tính hợp lệ: Thiết lập các thông báo lỗi qua MessageBox khi người dùng bỏ trống các trường bắt buộc (string.IsNullOrWhiteSpace) hoặc nhập dữ liệu không hợp lệ (điểm số, số lượng phải lớn hơn 0).

•	Tìm kiếm và Sắp xếp: Triển khai tính năng tìm kiếm nhanh trên lưới dữ liệu. Đồng thời, nâng cấp khả năng sắp xếp (Tăng/Giảm) cho các cột quan trọng trên DataGridView giúp việc quản lý danh sách sinh viên, giảng viên và điểm số trở nên linh hoạt hơn.

**3.CHI TIẾT CÁC NÂNG CẤP CỦA GIAI ĐOẠN 4**

**3.1. Tìm Kiếm Nâng Cao (Advanced Search)**

•	Loại tìm kiếm đa dạng: Mã Giảng Viên, Họ Tên, Giới Tính, Số Điện Thoại

•	ComboBox Loại Tìm Kiếm (cboLoaiTK) để chọn tiêu chí tìm kiếm

•	Logic switch-case xử lý tìm kiếm theo từng tiêu chí khác nhau

•	Tìm kiếm case-insensitive (không phân biệt hoa/thường)

**3.2. Sắp Xếp Dữ Liệu (Data Sorting)**

•	ComboBox Kiểu Sắp Xếp (cboKieuSX) với 3 tiêu chí: Mã GV, Họ Tên, Ngày Sinh

•	Sắp xếp tăng/giảm qua RadioButton (radTang / radGiam)

•	Sử dụng OrderBy() và OrderByDescending() từ LINQ

•	Tự động reload dữ liệu khi thay đổi sắp xếp

**3.3. Xử Lý RadioButton Giới Tính Đặc Biệt**

•	Hàm BsGiangVien\_CurrentChanged() để đồng bộ giữa TextBox và RadioButton

•	Khi chuyển dòng, tự động cập nhật lựa chọn Nam/Nữ

•	Ngăn chặn sự kiện xuLyThem gây ra xung đột

**3.4. Kiểm Tra Ràng Buộc Dữ Liệu Chi Tiết**

•	Kiểm tra Email: Phải đúng format abc@xyz.com (regex validation)

•	Kiểm tra Số Điện Thoại: Bắt đầu bằng 0 và đủ 10 số (regex: ^0\\d{9}$)

•	Kiểm tra Tuổi: Giảng viên phải ≥ 18 tuổi

•	Xử lý lỗi nhập liệu chi tiết hơn, focus vào ô lỗi

**3.5. Quản Lý Binding Source Nâng Cao**

•	Clear \& Tạo lại binding trước mỗi lần LoadData để tránh lỗi

•	Đảm bảo dữ liệu luôn đồng bộ giữa UI và Database

•	Xử lý CurrentChanged event của BindingSource

**3.6. Nút Chức Năng Bổ Sung**

•	btnShowAll: Hiển thị toàn bộ dữ liệu (xóa filter, reset sắp xếp)

•	btnTimKiem: Tìm kiếm với điều kiện đã chọn

•	Các nút được kích hoạt/vô hiệu hóa động (BatTatChucNang())

**3.7. Tối Ưu Hiệu Năng với LINQ AsQueryable**

•	context.GiangVien.AsQueryable() để xây dựng query động

•	Chỉ tải dữ liệu cần thiết từ DB (.ToList() ở cuối)

•	Tránh load toàn bộ dữ liệu vào RAM

**3.8. Xử Lý Exception Cụ Thể**

•	Bắt lỗi khi xóa dữ liệu (khóa ngoại)

•	Bắt lỗi khi lưu dữ liệu

•	Thông báo lỗi chi tiết cho người dùng

**4. Định hướng công việc tiếp theo**

•	Module Nhập/Xuất: Xây dựng chức năng xuất dữ liệu ra file Excel phục vụ công tác báo cáo và lưu trữ văn phòng.





