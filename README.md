# BỘ CÔNG THƯƠNG — TRƯỜNG ĐẠI HỌC ĐIỆN LỰC
## KHOA ĐIỆN TOÁN VÀ CÔNG NGHỆ SỐ NĂNG LƯỢNG
***

# KẾ HOẠCH VÀ PHÂN CÔNG NHIỆM VỤ BÀI TẬP LỚN
## ĐỀ TÀI: XÂY DỰNG PHẦN MỀM QUẢN LÝ CHUNG CƯ AN CƯ

- **Học phần**: Lập trình .Net
- **Nền tảng công nghệ**: .NET 10.0 (C# 14) – Windows Forms – 3-Tier Architecture – SQL Server 2025 (SSMS 22)
- **Nhóm thực hiện**: Nhóm 2
- **Lớp chuyên ngành**: D19CNPM3
- **Giảng viên hướng dẫn**: ThS. Cấn Đức Điệp
- **Thời gian thực hiện**: 03 tuần công tác tập trung

---

## 1. BẢNG PHÂN CHIA NHIỆM VỤ, USE CASE, GIAO DIỆN & NHÁNH GIT TỪNG THÀNH VIÊN

> **Quy định bắt buộc của nhóm**: Mỗi thành viên chịu trách nhiệm độc lập cho nhóm Use Case được giao, tự thiết kế và lập trình hoàn chỉnh **đúng ba (03) giao diện Windows Forms riêng biệt** do mình trực tiếp sở hữu (tổng cộng 12 Form cho toàn hệ thống), làm việc trên nhánh tính năng (`feature/...`) tách trực tiếp từ nhánh `develop`, và kiểm thử chéo trước khi tích hợp.

| STT | Thành viên (MSSV) | Vai trò | Use Case phụ trách chính | Giao diện WinForms sở hữu (3 Form/người) | Nhánh Git làm việc (từ `develop`) | Trách nhiệm kỹ thuật chi tiết |
| :---: | :--- | :--- | :--- | :--- | :--- | :--- |
| **1** | **Nguyễn Thành Nam**<br>*(MSSV: 24810310230)* | **Nhóm trưởng** | **UC1: Đăng nhập hệ thống & Phân quyền** | **1. `FormLogin.cs`** (Xác thực 3 vai trò)<br>**2. `FormChangePassword.cs`** (Đổi mật khẩu)<br>**3. `FormMain.cs`** (Khung điều hướng phân quyền) | `feature/core-auth-uc1` | • Quản lý tiến độ tổng thể đề tài trong 03 tuần.<br>• Thiết lập kiến trúc 3 tầng chuẩn (.NET 10.0 / C# 14): GUI, BLL, DAL, DTO, Common.<br>• Thiết kế mô hình CSDL SQL Server 2025 (11 bảng chuẩn 3NF, quan hệ PK/FK, Stored Procedures, Filegroup).<br>• Lập trình tầng DAL cơ sở `DbHelper` (ADO.NET, tham số hóa `SqlParameter`, `SqlTransaction`).<br>• Lập trình 03 giao diện WinForms sở hữu: `FormLogin.cs`, `FormChangePassword.cs` và `FormMain.cs`.<br>• Lập trình **UC1**: Xác thực đăng nhập 3 tác nhân (Chủ chung cư, Quản lý, Khách thuê), mã hóa mật khẩu SHA-256.<br>• Quản trị nhánh `main`/`develop`, review Pull Request, giải quyết xung đột mã nguồn và chủ trì soạn thảo Báo cáo toàn văn EPU. |
| **2** | **Phan Công Thành Đạt**<br>*(MSSV: 24810310223)* | **Thành viên** | **UC2: Phân hệ Quản lý phòng**<br>*(UC2.1 đến UC2.5)*<br>**UC3: Phân hệ Quản lý khách thuê**<br>*(UC3.1 đến UC3.5)* | **1. `FormPhong.cs`** (Sơ đồ mặt bằng, danh sách phòng, trạng thái, giá thuê)<br>**2. `FormThietBiPhong.cs`** (Quản lý thiết bị, tài sản tiện nghi phòng)<br>**3. `FormKhachThue.cs`** (Hồ sơ cư dân, tìm kiếm, xác thực CCCD 12 số) | `feature/room-tenant-uc2-uc3` | • Khảo sát và đối chiếu 32 sơ đồ UML đặc tả chi tiết của UC2 và UC3 (Sơ đồ tuần tự, cộng tác, hoạt động).<br>• Thiết kế và lập trình tầng DTO, DAL, BLL (.NET 10.0) cho các thực thể `Phong`, `ToaNha`, `KhachThue`, `ThietBiPhong`.<br>• Lập trình 03 giao diện WinForms sở hữu: `FormPhong.cs`, `FormThietBiPhong.cs` và `FormKhachThue.cs`.<br>• Cài đặt logic nghiệp vụ: Thêm/sửa phòng, cập nhật trạng thái phòng (Trống/Đang thuê/Sửa chữa), kiểm tra trùng số phòng, quản lý tiện nghi tài sản bàn giao, quản lý hồ sơ nhân khẩu, kiểm tra CCCD 12 số, chủ hộ duy nhất.<br>• Viết nội dung Chương 1 và đặc tả kỹ thuật UC2, UC3 trong Báo cáo. |
| **3** | **Lê Ngọc Quyền**<br>*(MSSV: 24810310259)* | **Thành viên** | **UC4: Phân hệ Quản lý hợp đồng thuê**<br>*(UC4.1 đến UC4.5)*<br>**UC5: Phân hệ Quản lý điện, nước và dịch vụ**<br>*(UC5.1 đến UC5.5)* | **1. `FormHopDong.cs`** (Lập, xem, gia hạn, chấm dứt, in hợp đồng)<br>**2. `FormChiSoDienNuoc.cs`** (Ghi chốt số công tơ điện nước ngày 29-30)<br>**3. `FormDichVu.cs`** (Bảng giá dịch vụ: thang máy, vệ sinh, xe, internet) | `feature/contract-utility-uc4-uc5` | • Khảo sát và đối chiếu 32 sơ đồ UML đặc tả chi tiết của UC4 và UC5 (Sơ đồ tuần tự, cộng tác, hoạt động).<br>• Thiết kế và lập trình tầng DTO, DAL, BLL (.NET 10.0) cho các thực thể `HopDong`, `DichVu`, `ChiSoDienNuoc`.<br>• Lập trình 03 giao diện WinForms sở hữu: `FormHopDong.cs`, `FormChiSoDienNuoc.cs` và `FormDichVu.cs`.<br>• Cài đặt giải thuật nghiệp vụ: Tính tiền điện sinh hoạt bậc thang lũy tiến theo quy chuẩn EVN, tính tiền nước sạch theo định mức, bẫy lỗi chỉ số mới $\ge$ cũ, quản lý cọc và xuất/in hợp đồng thuê nhà.<br>• Viết nội dung kỹ thuật Chương 2 về mô hình hóa nghiệp vụ tính phí và hợp đồng. |
| **4** | **Nguyễn Văn Tiến**<br>*(MSSV: 24810310274)* | **Thành viên** | **UC6: Quản lý hóa đơn & thanh toán**<br>*(UC6.1 đến UC6.5)*<br>**UC7: Quản lý yêu cầu & thông báo**<br>*(UC7.1 đến UC7.5)*<br>**UC8: Báo cáo & thống kê**<br>*(UC8.1 đến UC8.5)* | **1. `FormHoaDonThanhToan.cs`** (Hóa đơn tháng, đối soát, phiếu thu)<br>**2. `FormYeuCauThongBao.cs`** (Tiếp nhận sự cố, đăng tin thông báo)<br>**3. `FormBaoCaoThongKe.cs`** (Dashboard biểu đồ doanh thu, tỷ lệ phòng) | `feature/billing-ticket-report-uc6-uc7-uc8` | • Khảo sát và đối chiếu 48 sơ đồ UML đặc tả chi tiết của UC6, UC7, UC8 (Sơ đồ tuần tự, cộng tác, hoạt động).<br>• Thiết kế và lập trình tầng DTO, DAL, BLL (.NET 10.0) cho các thực thể `HoaDon`, `ChiTietHoaDon`, `ThanhToan`, `YeuCau`, `ThongBao`, `BaoCao`.<br>• Lập trình 03 giao diện WinForms sở hữu: `FormHoaDonThanhToan.cs`, `FormYeuCauThongBao.cs`, `FormBaoCaoThongKe.cs` (biểu đồ Chart).<br>• Xây dựng tầng tiện ích Common (`ValidationHelper`, `FormatHelper`, `ExcelExportHelper` qua ClosedXML).<br>• Thiết lập bộ dữ liệu mẫu (Dummy Database) đầy đủ trên SQL Server 2025 phục vụ demo 8 Use Cases.<br>• Xây dựng ma trận Test Cases, thực hiện kiểm thử toàn diện, thiết kế Slide PowerPoint bảo vệ và hoàn thiện `README.md`. |

---

## 2. PHẠM VI 8 USE CASES KHÓA CỨNG (CHUẨN HÓA MÔ HÌNH HƯỚNG ĐỐI TƯỢNG)

Hệ thống phục vụ **03 tác nhân (Actor)**:
1. **Chủ chung cư**: Quản trị vĩ mô, xem báo cáo doanh thu tài chính, tỷ lệ lấp đầy phòng, đối soát công nợ toàn tòa nhà.
2. **Quản lý tòa nhà**: Quản lý danh mục phòng, khách thuê, hợp đồng, chốt chỉ số điện nước, lập hóa đơn, thu tiền và xử lý sự cố báo hỏng.
3. **Khách thuê phòng**: Đăng nhập tra cứu hợp đồng, theo dõi lịch sử số điện nước, xem hóa đơn tiền nhà, gửi yêu cầu báo hỏng và xem thông báo.

### Bảng chi tiết 8 Use Cases chính và các ca sử dụng con

| Mã UC | Tên Phân hệ Use Case | Tác nhân tác động | Ca sử dụng con chuẩn hóa (Hệ thống 115 sơ đồ UML đặc tả) | Người phụ trách & Giao diện |
| :---: | :--- | :--- | :--- | :--- |
| **UC1** | **Đăng nhập hệ thống** | Chủ chung cư, Quản lý, Khách thuê | • Xác thực danh tính qua Tên đăng nhập/SĐT và Mật khẩu (SHA-256).<br>• Phân quyền ma trận 3 vai trò và điều hướng giao diện phù hợp.<br>• Chức năng đổi mật khẩu người dùng và khóa tài khoản khi nhập sai.<br>*Hồ sơ thiết kế UML: Biểu đồ Tuần tự, Biểu đồ Cộng tác, Biểu đồ Hoạt động UC1*. | **Nguyễn Thành Nam**<br>`FormLogin.cs`<br>`FormChangePassword.cs`<br>`FormMain.cs` |
| **UC2** | **Quản lý phòng** | Quản lý, Chủ chung cư | • **UC2.1**: Thêm phòng mới (số phòng, tầng, diện tích m2, thiết bị, đơn giá).<br>• **UC2.2**: Cập nhật thông tin phòng (giá niêm yết, nội thất đi kèm).<br>• **UC2.3**: Cập nhật trạng thái phòng (Trống, Đang thuê, Sửa chữa, Đặt cọc).<br>• **UC2.4**: Tra cứu phòng (tìm kiếm theo tầng, diện tích, khoảng giá, trạng thái).<br>• **UC2.5**: Quản lý giá thuê linh hoạt theo loại phòng.<br>*Hồ sơ thiết kế UML: 15 sơ đồ Trình tự, Cộng tác, Hoạt động (UC2.1 đến UC2.5)*. | **Phan Công Thành Đạt**<br>`FormPhong.cs`<br>`FormThietBiPhong.cs` |
| **UC3** | **Quản lý khách thuê** | Quản lý, Chủ chung cư | • **UC3.1**: Xem và tìm kiếm khách thuê (theo họ tên, số CCCD, SĐT, phòng).<br>• **UC3.2**: Thêm mới hồ sơ khách thuê (thông tin cá nhân, CCCD 12 số, tạm trú).<br>• **UC3.3**: Cập nhật hồ sơ khách thuê (số liên lạc, thông tin thành viên cùng phòng).<br>• **UC3.4**: Xóa hồ sơ khách thuê (xóa mềm khi khách chuyển đi, lưu lịch sử).<br>• **UC3.5**: Xác thực khách thuê (đối soát giấy tờ tùy thân, chủ hộ).<br>*Hồ sơ thiết kế UML: 15 sơ đồ Trình tự, Cộng tác, Hoạt động (UC3.1 đến UC3.5)*. | **Phan Công Thành Đạt**<br>`FormKhachThue.cs` |
| **UC4** | **Quản lý hợp đồng thuê** | Quản lý, Chủ chung cư, Khách thuê | • **UC4.1**: Lập hợp đồng mới (gán phòng, khách đại diện, hạn hợp đồng, tiền cọc, giá thuê).<br>• **UC4.2**: Xem chi tiết hợp đồng (tra cứu điều khoản, danh sách người ở, phụ lục).<br>• **UC4.3**: Gia hạn hợp đồng (điều chỉnh thời hạn hợp đồng, cập nhật giá mới).<br>• **UC4.4**: Chấm dứt hợp đồng (thanh lý hợp đồng, kiểm kê tài sản, quyết toán cọc).<br>• **UC4.5**: In hợp đồng thuê nhà chuẩn pháp lý ra file PDF/máy in.<br>*Hồ sơ thiết kế UML: 15 sơ đồ Trình tự, Cộng tác, Hoạt động (UC4.1 đến UC4.5)*. | **Lê Ngọc Quyền**<br>`FormHopDong.cs` |
| **UC5** | **Quản lý điện, nước và dịch vụ** | Quản lý, Khách thuê | • **UC5.1**: Ghi chỉ số (nhập và chốt số công tơ điện nước ngày 29-30 hàng tháng; kiểm tra chỉ số mới $\ge$ cũ).<br>• **UC5.2**: Quản lý dịch vụ (thiết lập danh mục dịch vụ: vệ sinh, thang máy, gửi xe, internet).<br>• **UC5.3**: Tính tiền (tự động tính tiền điện bậc thang EVN, tiền nước và phí dịch vụ).<br>• **UC5.4**: Xem chỉ số điện nước (Khách thuê tra cứu lịch sử tiêu thụ điện nước).<br>• **UC5.5**: Xem dịch vụ đang dùng (tra cứu danh sách dịch vụ đăng ký theo phòng).<br>*Hồ sơ thiết kế UML: 15 sơ đồ Trình tự, Cộng tác, Hoạt động (UC5.1 đến UC5.5)*. | **Lê Ngọc Quyền**<br>`FormChiSoDienNuoc.cs`<br>`FormDichVu.cs` |
| **UC6** | **Quản lý hóa đơn và thanh toán** | Quản lý, Khách thuê | • **UC6.1**: Lập hóa đơn (tự động gom tiền phòng, điện, nước, dịch vụ thành hóa đơn tổng hợp tháng).<br>• **UC6.2**: Xác nhận thanh toán (Quản lý đối soát tiền mặt hoặc chuyển khoản, đổi trạng thái Đã thanh toán).<br>• **UC6.3**: Kiểm tra công nợ (lọc danh sách các phòng chưa thanh toán, tính nợ quá hạn).<br>• **UC6.4**: Xem hóa đơn (Khách thuê tra cứu chi tiết từng khoản thu và tải hóa đơn điện tử).<br>• **UC6.5**: Thanh toán (ghi nhận giao dịch, lập phiếu thu tiền mặt / biên nhận điện tử).<br>*Hồ sơ thiết kế UML: 15 sơ đồ Trình tự, Cộng tác, Hoạt động (UC6.1 đến UC6.5)*. | **Nguyễn Văn Tiến**<br>`FormHoaDonThanhToan.cs` |
| **UC7** | **Quản lý yêu cầu và thông báo** | Quản lý, Khách thuê | • **UC7.1**: Gửi yêu cầu (Khách thuê báo hỏng điện, nước, điều hòa, an ninh kèm mô tả hiện trường).<br>• **UC7.2**: Tiếp nhận xử lý (Quản lý tiếp nhận, phân công kỹ thuật viên và hẹn lịch sửa chữa).<br>• **UC7.3**: Gửi phản hồi (cập nhật kết quả sửa chữa và ghi nhận đánh giá hài lòng 1-5 sao).<br>• **UC7.4**: Quản lý thông báo (Ban quản lý đăng thông báo cắt điện nước, tổng vệ sinh, quy định PCCC).<br>• **UC7.5**: Xem thông báo (Khách thuê xem thông báo chung tòa nhà và thông báo riêng cho phòng).<br>*Hồ sơ thiết kế UML: 15 sơ đồ Trình tự, Cộng tác, Hoạt động (UC7.1 đến UC7.5)*. | **Nguyễn Văn Tiến**<br>`FormYeuCauThongBao.cs` |
| **UC8** | **Báo cáo và thống kê** | Chủ chung cư, Quản lý | • **UC8.1**: Báo cáo doanh thu (tổng hợp doanh thu tiền phòng, điện, nước, dịch vụ theo tháng/quý/năm).<br>• **UC8.2**: Báo cáo tình trạng phòng (thống kê tỷ lệ lấp đầy phòng Occupancy Rate, danh sách phòng trống).<br>• **UC8.3**: Báo cáo công nợ (danh sách chi tiết các phòng còn nợ tiền nhà, thời gian quá hạn).<br>• **UC8.4**: Báo cáo khách thuê (số lượng khách đang thuê, biến động tăng giảm cư dân trong kỳ).<br>• **UC8.5**: Xuất báo cáo (trích xuất bảng kê và biểu đồ ra file Excel qua ClosedXML hoặc in PDF).<br>*Hồ sơ thiết kế UML: 15 sơ đồ Trình tự, Cộng tác, Hoạt động (UC8.1 đến UC8.5)*. | **Nguyễn Văn Tiến**<br>`FormBaoCaoThongKe.cs` |

---

## 3. KẾ HOẠCH TRIỂN KHAI CHI TIẾT 03 TUẦN (PHÂN CHIA TỪNG NGƯỜI)

### 3.1. Tuần 1: Khảo sát hiện trạng, phân tích nghiệp vụ 8 Use Case, thiết kế CSDL SQL Server 2025 và thiết lập kiến trúc 3-Tier (.NET 10.0)

- **Mục tiêu chung Tuần 1**: Khảo sát hiện trạng nghiệp vụ quản lý chung cư An Cư; khóa cứng phạm vi 8 Use Case và 3 tác nhân; thiết kế hoàn chỉnh mô hình CSDL quan hệ trên SQL Server 2025 (11 bảng chuẩn 3NF, quan hệ PK/FK, Stored Procedures); khởi tạo repository GitHub và khung Solution Visual Studio 3-Tier (.NET 10.0) kết nối thông suốt CSDL qua lớp `DbHelper`.
- **Phân công chi tiết từng người trong Tuần 1**:

| Thành viên | Nhánh Git | Giao diện phác thảo (3 Form/người) | Công việc cụ thể Tuần 1 |
| :--- | :--- | :--- | :--- |
| **Nguyễn Thành Nam** | `develop`<br>`feature/core-auth-uc1` | `FormLogin.cs`<br>`FormChangePassword.cs`<br>`FormMain.cs` | • Khởi tạo Git repository trên GitHub, cấu hình `.gitignore` .NET 10.0, tạo nhánh `develop`.<br>• Thiết kế mô hình CSDL ERD tổng thể; viết script `01_Create_Database.sql` và `02_Create_Tables.sql` trên SQL Server 2025.<br>• Xây dựng Solution Visual Studio 3-Tier (.NET 10.0), thiết lập lớp `DbHelper` kết nối ADO.NET an toàn.<br>• Phân tích bảng `TaiKhoan`; phác thảo Wireframe layout cho 03 giao diện `FormLogin.cs`, `FormChangePassword.cs`, `FormMain.cs`; lập trình khung đăng nhập UC1 và phân quyền 3 tác nhân. |
| **Phan Công Thành Đạt** | `feature/room-tenant-uc2-uc3` | `FormPhong.cs`<br>`FormThietBiPhong.cs`<br>`FormKhachThue.cs` | • Khảo sát chi tiết nghiệp vụ Quản lý phòng (UC2) và Quản lý khách thuê (UC3).<br>• Thiết kế chi tiết cấu trúc các bảng `Phong`, `ToaNha`, `KhachThue`, `ThietBiPhong`; viết script ràng buộc khóa ngoại.<br>• Thiết kế và chuẩn hóa 32 sơ đồ UML đặc tả của UC2 và UC3 (Tuần tự, Cộng tác, Hoạt động).<br>• Phác thảo Wireframe layout cho 03 giao diện: `FormPhong.cs`, `FormThietBiPhong.cs` và `FormKhachThue.cs`; soạn nội dung Chương 1. |
| **Lê Ngọc Quyền** | `feature/contract-utility-uc4-uc5` | `FormHopDong.cs`<br>`FormChiSoDienNuoc.cs`<br>`FormDichVu.cs` | • Khảo sát chi tiết nghiệp vụ Hợp đồng thuê (UC4) và Điện nước dịch vụ (UC5).<br>• Thiết kế cấu trúc các bảng `HopDong`, `DichVu`, `ChiSoDienNuoc`; lập công thức tính điện bậc thang EVN và nước sạch.<br>• Thiết kế và chuẩn hóa 32 sơ đồ UML đặc tả của UC4 và UC5 (Tuần tự, Cộng tác, Hoạt động).<br>• Phác thảo Wireframe layout cho 03 giao diện: `FormHopDong.cs`, `FormChiSoDienNuoc.cs` và `FormDichVu.cs`. |
| **Nguyễn Văn Tiến** | `feature/billing-ticket-report-uc6-uc7-uc8` | `FormHoaDonThanhToan.cs`<br>`FormYeuCauThongBao.cs`<br>`FormBaoCaoThongKe.cs` | • Khảo sát chi tiết nghiệp vụ Hóa đơn (UC6), Yêu cầu (UC7) và Báo cáo (UC8).<br>• Thiết kế cấu trúc các bảng `HoaDon`, `ChiTietHoaDon`, `ThanhToan`, `YeuCau`, `ThongBao`.<br>• Thiết kế và chuẩn hóa 48 sơ đồ UML đặc tả của UC6, UC7, UC8 (Tuần tự, Cộng tác, Hoạt động).<br>• Cài đặt môi trường .NET 10.0 SDK, Visual Studio, SSMS 22 cho cả nhóm; phác thảo Wireframe layout cho 03 giao diện: `FormHoaDonThanhToan.cs`, `FormYeuCauThongBao.cs` và `FormBaoCaoThongKe.cs`. |

- **Sản phẩm bàn giao Tuần 1**:
  - Tài liệu đặc tả 8 Use Cases được cả nhóm và GVHD thống nhất.
  - File script SQL tạo CSDL `QuanLyChungCuAnCu` gồm 11 bảng chuẩn 3NF.
  - Solution Visual Studio 3-Tier (.NET 10.0) biên dịch thành công, kết nối được CSDL qua `DbHelper`.
  - Repository GitHub sẵn sàng với nhánh `main` và nhánh `develop`.
- **Tiêu chí nghiệm thu Tuần 1**: CSDL không lỗi khóa ngoại; `DbHelper` kết nối thành công; cả 4 thành viên clone dự án, biên dịch .NET 10.0 thành công và push thử nghiệm lên nhánh riêng.

---

### 3.2. Tuần 2: Phân tích thiết kế hướng đối tượng UML, Lập trình mã nguồn 3-Tier (.NET 10.0) – Checkpoint Báo cáo tiến độ Giữa kỳ

- **Mục tiêu chung Tuần 2**: Hoàn thiện 100% tài liệu Báo cáo tiến độ Bài tập lớn (Chương 1 & Chương 2) tích hợp đầy đủ hệ thống 115 sơ đồ UML đặc tả chi tiết; hoàn thành việc lập trình các tầng DTO, DAL, BLL và giao diện WinForms cho toàn bộ 8 phân hệ Use Case trên nền tảng .NET 10.0; tích hợp phiên bản Alpha liên hoàn trên nhánh `develop`.
- **Phân công chi tiết từng người trong Tuần 2**:

| Thành viên | Nhánh Git | Giao diện hoàn thành (3 Form/người) | Công việc cụ thể Tuần 2 |
| :--- | :--- | :--- | :--- |
| **Nguyễn Thành Nam** | `feature/core-auth-uc1`<br>merge $\to$ `develop` | **`FormLogin.cs`**<br>**`FormChangePassword.cs`**<br>**`FormMain.cs`** | • Hoàn thiện Biểu đồ Use Case tổng quát, Biểu đồ lớp (Class Diagram) kết nối 3 tầng và sơ đồ tuần tự UC1 Đăng nhập.<br>• Lập trình hoàn thiện 03 giao diện: `FormLogin.cs`, `FormChangePassword.cs`, `FormMain.cs` điều hướng phân quyền 3 tác nhân, `TaiKhoanBLL`, `TaiKhoanDAL`.<br>• Tối ưu hóa `DbHelper`, cài đặt `SqlTransaction` xử lý lưu hóa đơn nhiều dòng chi tiết an toàn.<br>• Chủ trì tổng hợp, soát xét và hoàn thiện tài liệu Báo cáo tiến độ bài tập lớn nộp Giảng viên hướng dẫn. |
| **Phan Công Thành Đạt** | `feature/room-tenant-uc2-uc3`<br>merge $\to$ `develop` | **`FormPhong.cs`**<br>**`FormThietBiPhong.cs`**<br>**`FormKhachThue.cs`** | • Tích hợp đầy đủ các sơ đồ UML của UC2 và UC3 vào tài liệu Báo cáo tiến độ Chương 2.<br>• Lập trình hoàn thiện tầng DTO, DAL, BLL và 03 giao diện WinForms: `FormPhong.cs` (UC2.1-UC2.5), `FormThietBiPhong.cs` (UC2 Quản lý tài sản nội thất tiện nghi) và `FormKhachThue.cs` (UC3.1-UC3.5).<br>• Cài đặt tính năng hiển thị DataGridView, lọc theo tầng, đổi màu trạng thái phòng, quản lý tiện nghi phòng, tìm kiếm khách thuê theo CCCD/SĐT.<br>• Kiểm thử chức năng UC2, UC3 trên máy cục bộ và tạo Pull Request vào nhánh `develop`. |
| **Lê Ngọc Quyền** | `feature/contract-utility-uc4-uc5`<br>merge $\to$ `develop` | **`FormHopDong.cs`**<br>**`FormChiSoDienNuoc.cs`**<br>**`FormDichVu.cs`** | • Tích hợp đầy đủ các sơ đồ UML của UC4 và UC5 vào tài liệu Báo cáo tiến độ Chương 2.<br>• Lập trình hoàn thiện tầng DTO, DAL, BLL và 03 giao diện GUI: `FormHopDong.cs` (UC4.1-UC4.5), `FormChiSoDienNuoc.cs` (UC5 Ghi chốt chỉ số điện nước) và `FormDichVu.cs` (UC5 Danh mục & biểu phí dịch vụ).<br>• Cài đặt thuật toán tính tiền điện bậc thang EVN, tiền nước sạch và chức năng xuất/in hợp đồng thuê phòng.<br>• Kiểm thử tính toán điện nước và luồng hợp đồng trên máy cục bộ, tạo Pull Request vào `develop`. |
| **Nguyễn Văn Tiến** | `feature/billing-ticket-report-uc6-uc7-uc8`<br>merge $\to$ `develop` | **`FormHoaDonThanhToan.cs`**<br>**`FormYeuCauThongBao.cs`**<br>**`FormBaoCaoThongKe.cs`** | • Tích hợp đầy đủ các sơ đồ UML của UC6, UC7, UC8 vào tài liệu Báo cáo tiến độ Chương 2.<br>• Lập trình DTO, DAL, BLL và 03 giao diện GUI: `FormHoaDonThanhToan.cs` (UC6), `FormYeuCauThongBao.cs` (UC7) và `FormBaoCaoThongKe.cs` (UC8).<br>• Xây dựng bộ tiện ích `ValidationHelper` (CCCD, SĐT, Email), `FormatHelper` (VNĐ) và `ExcelExportHelper` (ClosedXML).<br>• Hỗ trợ Trưởng nhóm kiểm tra tính nhất quán hình ảnh sơ đồ UML theo chuẩn EPU (rõ nét, kích thước $\le 15\text{ cm}$). |

- **Sản phẩm bàn giao Tuần 2**:
  - **Tài liệu Báo cáo tiến độ bài tập lớn (Chương 1 & Chương 2)** chuẩn quy cách học thuật EPU (.docx và .pdf).
  - Bộ 115 sơ đồ UML thiết kế hệ thống chất lượng cao, chuẩn quy cách trình bày EPU.
  - **Phiên bản Alpha (.NET 10.0)** tích hợp đầy đủ 12 Form giao diện của 4 thành viên (mỗi thành viên hoàn thành đúng 3 Form), chạy liên hoàn từ màn hình chính `FormMain`, kết nối trực tiếp CSDL SQL Server 2025.
  - Toàn bộ các nhánh feature được review và merge thành công vào nhánh `develop`.
- **Tiêu chí nghiệm thu Tuần 2**: Báo cáo tiến độ đạt chuẩn quy cách EPU; ứng dụng Alpha chạy thông suốt toàn bộ 12 màn hình giao diện liên kết qua `FormMain`: Đăng nhập $\to$ Thêm phòng & tiện nghi $\to$ Thêm khách $\to$ Lập hợp đồng $\to$ Ghi điện nước & dịch vụ $\to$ Lập hóa đơn & báo cáo.

---

### 3.3. Tuần 3: Kiểm thử toàn diện theo Test Cases, tối ưu UI/UX, đóng gói phát hành và bảo vệ đề tài

- **Mục tiêu chung Tuần 3**: Thực hiện kiểm thử phần mềm toàn diện theo ma trận Test Cases (chức năng 8 phân hệ, 12 giao diện, dữ liệu biên, phân quyền bảo mật); sửa triệt để các lỗi phát hiện; tối ưu hóa trải nghiệm người dùng; đóng gói bộ cài đặt thực thi độc lập .NET 10.0; hoàn thiện Báo cáo học phần chính thức toàn văn, Slide thuyết trình và kịch bản demo phục vụ bảo vệ.
- **Phân công chi tiết từng người trong Tuần 3**:

| Thành viên | Nhánh Git | Giao diện tối ưu & kiểm thử (3 Form/người) | Công việc cụ thể Tuần 3 |
| :--- | :--- | :--- | :--- |
| **Nguyễn Thành Nam** | `develop`<br>merge $\to$ `main`<br>(tag `v1.0-final`) | Toàn bộ hệ thống & 03 Form phụ trách:<br>`FormLogin.cs`<br>`FormChangePassword.cs`<br>`FormMain.cs` | • Tiến hành Code Review tổng thể toàn bộ dự án, chuẩn hóa chú thích và cấu trúc mã nguồn theo Clean Code.<br>• Đóng gói chương trình thành file thực thi độc lập (.NET 10.0 Standalone Release kèm file cấu hình `App.config`).<br>• Xuất bản script SQL trọn gói kèm bộ dữ liệu mẫu 50 phòng, 120 khách thuê, chỉ số 6 tháng.<br>• Chủ trì tổng hợp, hoàn thiện Báo cáo học phần chính thức toàn văn (.docx và .pdf) chuẩn EPU; đánh tag `v1.0-final` trên GitHub. |
| **Phan Công Thành Đạt** | `develop` | 03 Form sở hữu:<br>`FormPhong.cs`<br>`FormThietBiPhong.cs`<br>`FormKhachThue.cs` | • Kiểm thử chuyên sâu phân hệ UC2 và UC3 trên 03 Form sở hữu: Thử nghiệm các ca kiểm thử trùng CCCD, trùng số phòng, quản lý tiện nghi phòng, xóa phòng đang thuê.<br>• Tối ưu hóa trải nghiệm nhập liệu: Phím tắt điều hướng nhanh, bẫy lỗi nhập sai định dạng số điện thoại, ngày sinh.<br>• Bổ sung và hoàn thiện nội dung Chương 3 (Cài đặt chương trình) và Chương 4 (Kiểm thử) thuộc phần việc phụ trách.<br>• Chuẩn bị nội dung trả lời câu hỏi phản biện liên quan đến phân tích thiết kế CSDL, quản lý phòng và khách thuê. |
| **Lê Ngọc Quyền** | `develop` | 03 Form sở hữu:<br>`FormHopDong.cs`<br>`FormChiSoDienNuoc.cs`<br>`FormDichVu.cs` | • Kiểm thử chuyên sâu phân hệ UC4 và UC5 trên 03 Form sở hữu: Thử nghiệm ca kiểm thử chỉ số mới nhỏ hơn cũ, số liệu cực lớn, thanh lý hợp đồng trước hạn, thay đổi đơn giá dịch vụ.<br>• Đối chiếu kết quả tính tiền điện nước tự động của phần mềm với công thức tính thủ công để xác nhận độ chính xác 100%.<br>• Bổ sung nội dung báo cáo thuộc phân hệ phụ trách; chụp ảnh minh chứng kết quả kiểm thử.<br>• Chuẩn bị nội dung trả lời câu hỏi phản biện liên quan đến giải thuật tính toán chi phí và nghiệp vụ hợp đồng. |
| **Nguyễn Văn Tiến** | `develop` | 03 Form sở hữu:<br>`FormHoaDonThanhToan.cs`<br>`FormYeuCauThongBao.cs`<br>`FormBaoCaoThongKe.cs` | • Thực hiện toàn bộ ma trận kiểm thử phần mềm cho 8 Use Cases trên 12 Form; lập Bảng kết quả kiểm thử (Test Results Matrix) đưa vào báo cáo.<br>• Thiết kế bộ Slide thuyết trình PowerPoint chuyên nghiệp, sinh động phục vụ bảo vệ đề tài.<br>• Xây dựng Kịch bản Demo chương trình chi tiết từng bước cho cả 3 vai trò (Chủ nhà, Quản lý, Khách thuê).<br>• Hoàn thiện file `README.md` hướng dẫn cài đặt .NET 10.0 Runtime, cấu hình chuỗi kết nối SQL Server và hướng dẫn vận hành. |

- **Sản phẩm bàn giao Tuần 3**:
  - Gói phần mềm hoàn chỉnh: File thực thi `AnCuManagement.exe` (.NET 10.0 Standalone), `App.config`, script SQL Server kèm dữ liệu mẫu.
  - Mã nguồn sạch trên GitHub với release tag `v1.0-final`.
  - **Báo cáo học phần chính thức toàn văn** (.docx và .pdf) đạt 100% quy chuẩn định dạng EPU.
  - Bộ Slide thuyết trình PowerPoint và Kịch bản demo phân quyền 3 tác nhân.
- **Tiêu chí nghiệm thu Tuần 3**: Phần mềm vượt qua 100% các ca kiểm thử nghiệm thu, không còn lỗi sập ứng dụng; nhóm tổ chức bảo vệ thử nội bộ thành công, tự tin trả lời lưu loát các câu hỏi phản biện của Hội đồng chấm thi.

---

## 4. QUY TRÌNH QUẢN LÝ MÃ NGUỒN GIT/GITHUB TỪ NHÁNH `develop`

### 4.1. Sơ đồ cây phân nhánh Git (Feature Branching Workflow)

```text
main (Production / Stable Releases)
 │   └── v1.0-final (Tag phát hành chính thức nộp bảo vệ đề tài cuối Tuần 3)
 │
 └── develop (Nhánh tích hợp liên tục & Kiểm thử chung của nhóm)
       │
       ├── feature/core-auth-uc1            (Nguyễn Thành Nam phụ trách: Khung 3-Tier .NET 10.0, DAL DbHelper, UC1 Đăng nhập & Phân quyền)
       ├── feature/room-tenant-uc2-uc3      (Phan Công Thành Đạt phụ trách: UC2 Quản lý phòng & UC3 Quản lý khách thuê)
       ├── feature/contract-utility-uc4-uc5 (Lê Ngọc Quyền phụ trách: UC4 Hợp đồng thuê & UC5 Điện, nước và dịch vụ)
       └── feature/billing-ticket-report-uc6-uc7-uc8 (Nguyễn Văn Tiến phụ trách: UC6 Hóa đơn thanh toán, UC7 Yêu cầu thông báo & UC8 Báo cáo thống kê)
```


## 5. CẤU TRÚC THƯ MỤC DỰ ÁN MẪU (.NET 10.0 / 3-TIER VISUAL STUDIO SOLUTION)

Dưới đây là cấu trúc thư mục mẫu toàn diện của giải pháp Visual Studio .NET 10.0, thể hiện rõ giao diện WinForms và các lớp nghiệp vụ do từng thành viên sở hữu:

```text
AnCuApartmentManagement/                                     # Thư mục gốc toàn bộ giải pháp (.NET 10.0)
├── .gitignore                                              # Cấu hình bỏ qua bin, obj, .vs, file tạm Visual Studio
├── README.md                                               # Hướng dẫn cấu hình ConnectionString, build và vận hành
├── AnCuManagement.sln                                      # Solution File (.NET 10.0 / C# 14 Visual Studio 2026/2022)
├── database/                                               # Kịch bản T-SQL chạy trên SQL Server 2025 qua SSMS 22
│   ├── 01_Create_Database.sql                              # Kịch bản tạo CSDL QuanLyChungCuAnCu, cấu hình Filegroup
│   ├── 02_Create_Tables.sql                                # Khởi tạo 11 bảng dữ liệu quan hệ chuẩn hóa 3NF
│   ├── 03_Constraints_ForeignKeys.sql                      # Ràng buộc toàn vẹn khóa ngoại (FK), CHECK, UNIQUE
│   ├── 04_Stored_Procedures.sql                            # Toàn bộ thủ tục lưu trữ CRUD và tính toán hóa đơn 8 Use Cases
│   ├── 05_Triggers_AutoUpdate.sql                          # Trigger tự động cập nhật trạng thái phòng và tổng tiền
│   └── 06_Sample_Data.sql                                  # Dữ liệu thử nghiệm chuẩn: 50 phòng, 120 khách thuê, chỉ số 6 tháng
├── src/                                                    # Toàn bộ mã nguồn 5 project theo mô hình 3-Tier
│   ├── AnCuManagement.GUI/                                 # Tầng Giao diện Windows Forms (net10.0-windows)
│   │   ├── AnCuManagement.GUI.csproj                       # Cấu hình project .NET 10.0 Windows Forms SDK
│   │   ├── Program.cs                                      # Điểm khởi chạy ứng dụng, cấu hình DPI và khởi tạo FormLogin
│   │   ├── App.config                                      # Chuỗi kết nối SQL Server ConnectionString & tham số hệ thống
│   │   ├── Forms/                                          # 12 Form màn hình nghiệp vụ (mỗi thành viên sở hữu đúng 3 Form)
│   │   │   ├── FormMain.cs                                 # [Nguyễn Thành Nam - Form 1/3] Cửa sổ chính: Phân quyền Chủ nhà, Quản lý, Khách thuê
│   │   │   ├── FormLogin.cs                                # [Nguyễn Thành Nam - Form 2/3] UC1: Đăng nhập tài khoản và xác thực danh tính
│   │   │   ├── FormChangePassword.cs                       # [Nguyễn Thành Nam - Form 3/3] UC1: Đổi mật khẩu người dùng
│   │   │   ├── FormPhong.cs                                # [Phan Công Thành Đạt - Form 1/3] UC2: Quản lý danh mục phòng, trạng thái, giá thuê
│   │   │   ├── FormThietBiPhong.cs                         # [Phan Công Thành Đạt - Form 2/3] UC2: Quản lý trang thiết bị, tiện nghi bàn giao phòng
│   │   │   ├── FormKhachThue.cs                            # [Phan Công Thành Đạt - Form 3/3] UC3: Quản lý hồ sơ cư dân, tìm kiếm, xác thực CCCD
│   │   │   ├── FormHopDong.cs                              # [Lê Ngọc Quyền - Form 1/3] UC4: Quản lý hợp đồng thuê, gia hạn, chấm dứt, in hợp đồng
│   │   │   ├── FormChiSoDienNuoc.cs                        # [Lê Ngọc Quyền - Form 2/3] UC5: Ghi chốt chỉ số điện nước ngày 29-30 hàng tháng
│   │   │   ├── FormDichVu.cs                               # [Lê Ngọc Quyền - Form 3/3] UC5: Quản lý bảng giá & danh mục dịch vụ chung cư
│   │   │   ├── FormHoaDonThanhToan.cs                      # [Nguyễn Văn Tiến - Form 1/3] UC6: Hóa đơn tổng hợp tháng, đối soát thanh toán, phiếu thu
│   │   │   ├── FormYeuCauThongBao.cs                       # [Nguyễn Văn Tiến - Form 2/3] UC7: Tiếp nhận báo hỏng sự cố & Quản lý thông báo tòa nhà
│   │   │   └── FormBaoCaoThongKe.cs                        # [Nguyễn Văn Tiến - Form 3/3] UC8: Dashboard biểu đồ doanh thu, tỷ lệ phòng, xuất Excel
│   │   ├── UserControls/                                   # Các điều khiển giao diện tùy biến tái sử dụng
│   │   │   ├── UCDashboardCard.cs                          # Thẻ thống kê chỉ số tổng quan trên Bảng điều khiển (UC8)
│   │   │   └── UCRoomStatusCard.cs                         # Thẻ hiển thị trực quan trạng thái phòng theo màu sắc (UC2)
│   │   └── Resources/                                      # Tài nguyên đồ họa của chương trình (Icons, Images, Banner)
│   ├── AnCuManagement.BLL/                                 # Tầng Xử lý nghiệp vụ (Business Logic Layer - net10.0)
│   │   ├── AnCuManagement.BLL.csproj                       # Project Class Library .NET 10.0
│   │   ├── TaiKhoanBLL.cs                                  # [Nguyễn Thành Nam] UC1: Xác thực đăng nhập, phân quyền 3 tác nhân
│   │   ├── PhongBLL.cs                                     # [Phan Công Thành Đạt] UC2: Nghiệp vụ quản lý phòng, kiểm tra trạng thái
│   │   ├── ThietBiPhongBLL.cs                              # [Phan Công Thành Đạt] UC2: Nghiệp vụ trang thiết bị và tiện nghi phòng
│   │   ├── KhachThueBLL.cs                                 # [Phan Công Thành Đạt] UC3: Nghiệp vụ khách thuê, kiểm tra CCCD 12 số
│   │   ├── HopDongBLL.cs                                   # [Lê Ngọc Quyền] UC4: Nghiệp vụ hợp đồng thuê, gia hạn, thanh lý
│   │   ├── ChiSoDienNuocBLL.cs                             # [Lê Ngọc Quyền] UC5: Thuật toán kiểm tra chỉ số điện nước và lũy kế
│   │   ├── DichVuBLL.cs                                    # [Lê Ngọc Quyền] UC5: Thuật toán tính tiền điện bậc thang EVN, tiền nước, dịch vụ
│   │   ├── HoaDonThanhToanBLL.cs                           # [Nguyễn Văn Tiến] UC6: Nghiệp vụ tổng hợp chi phí, phát hành hóa đơn
│   │   ├── YeuCauThongBaoBLL.cs                            # [Nguyễn Văn Tiến] UC7: Nghiệp vụ tiếp nhận báo hỏng và phân phối thông báo
│   │   └── BaoCaoThongKeBLL.cs                             # [Nguyễn Văn Tiến] UC8: Nghiệp vụ tổng hợp doanh thu, tỷ lệ lấp đầy
│   ├── AnCuManagement.DAL/                                 # Tầng Truy xuất dữ liệu (Data Access Layer - net10.0)
│   │   ├── AnCuManagement.DAL.csproj                       # Project Class Library .NET 10.0 (ADO.NET)
│   │   ├── DbHelper.cs                                     # [Nguyễn Thành Nam] Kết nối SqlConnection, SqlTransaction, SqlParameter
│   │   ├── TaiKhoanDAL.cs                                  # [Nguyễn Thành Nam] UC1: Truy vấn bảng TaiKhoan qua Stored Procedures
│   │   ├── PhongDAL.cs                                     # [Phan Công Thành Đạt] UC2: Truy vấn bảng Phong, ToaNha qua ADO.NET
│   │   ├── ThietBiPhongDAL.cs                              # [Phan Công Thành Đạt] UC2: Truy vấn bảng ThietBiPhong qua ADO.NET
│   │   ├── KhachThueDAL.cs                                 # [Phan Công Thành Đạt] UC3: Truy vấn bảng KhachThue qua ADO.NET
│   │   ├── HopDongDAL.cs                                   # [Lê Ngọc Quyền] UC4: Truy vấn bảng HopDong
│   │   ├── ChiSoDienNuocDAL.cs                             # [Lê Ngọc Quyền] UC5: Truy vấn bảng ChiSoDienNuoc
│   │   ├── DichVuDAL.cs                                    # [Lê Ngọc Quyền] UC5: Truy vấn bảng DichVu
│   │   ├── HoaDonThanhToanDAL.cs                           # [Nguyễn Văn Tiến] UC6: Truy vấn bảng HoaDon, ChiTietHoaDon, ThanhToan
│   │   ├── YeuCauThongBaoDAL.cs                            # [Nguyễn Văn Tiến] UC7: Truy vấn bảng YeuCau, ThongBao
│   │   └── BaoCaoThongKeDAL.cs                             # [Nguyễn Văn Tiến] UC8: Truy vấn dữ liệu thống kê tài chính và vận hành
│   ├── AnCuManagement.DTO/                                 # Tầng Thực thể dữ liệu (Data Transfer Objects - net10.0)
│   │   ├── AnCuManagement.DTO.csproj                       # Project Class Library .NET 10.0 (POCO Classes)
│   │   ├── TaiKhoanDTO.cs                                  # Thuộc tính: MaTK, TenDangNhap, MatKhauHash, QuyenHan
│   │   ├── PhongDTO.cs                                     # Thuộc tính: MaPhong, SoPhong, Tang, DienTich, GiaThue, TrangThai
│   │   ├── KhachThueDTO.cs                                 # Thuộc tính: MaKhach, HoTen, CCCD, SDT, Email, QueQuan, MaPhong
│   │   ├── HopDongDTO.cs                                   # Thuộc tính: SoHopDong, MaPhong, MaKhach, NgayBD, NgayKT, TienCoc
│   │   ├── DichVuDTO.cs                                    # Thuộc tính: MaDV, TenDV, DonGia, DonViTinh
│   │   ├── ChiSoDienNuocDTO.cs                             # Thuộc tính: MaChiSo, MaPhong, ThangNam, ChiSoDien, ChiSoNuoc
│   │   ├── HoaDonDTO.cs                                    # Thuộc tính: MaHD, MaPhong, ThangNam, TongTien, TrangThai, NgayLap
│   │   ├── ChiTietHoaDonDTO.cs                             # Thuộc tính: MaChiTiet, MaHD, MaDV, SoLuong, DonGia, ThanhTien
│   │   ├── ThanhToanDTO.cs                                 # Thuộc tính: MaTT, MaHD, NgayTT, SoTien, HinhThuc
│   │   ├── YeuCauDTO.cs                                    # Thuộc tính: MaYeuCau, MaPhong, NoiDung, NgayGui, TrangThai, KetQua
│   │   └── ThongBaoDTO.cs                                  # Thuộc tính: MaTB, TieuDe, NoiDung, NgayDang, NguoiNhan
│   └── AnCuManagement.Common/                              # Tầng Tiện ích dùng chung (Cross-cutting - net10.0)
│       ├── AnCuManagement.Common.csproj                    # Project Class Library .NET 10.0
│       ├── AppConstants.cs                                 # Hằng số hệ thống, chuỗi vai trò (Chủ nhà, Quản lý, Khách thuê)
│       ├── PasswordHasher.cs                               # Hàm băm mật khẩu SHA-256 kèm Salt bảo mật
│       ├── ValidationHelper.cs                             # Kiểm tra Regex CCCD 12 số, SĐT 10 số, Email, số dương
│       ├── FormatHelper.cs                                 # Định dạng tiền tệ VNĐ (ví dụ: 1.500.000 đ), ngày dd/MM/yyyy
│       └── ExcelExportHelper.cs                            # Xuất danh sách và báo cáo tài chính ra Excel qua ClosedXML
├── documents/                                              # Toàn bộ tài liệu học thuật theo quy chuẩn EPU
│   ├── BaoCaoTienDo/                                       # Báo cáo tiến độ Giữa kỳ (Cuối Tuần 2)
│   │   ├── BaoCaoTienDo_QuanLyChungCuAnCu.docx             # File Word báo cáo tiến độ chuẩn quy cách EPU
│   │   └── BaoCaoTienDo_QuanLyChungCuAnCu.pdf              # File PDF xuất bản tương ứng
│   ├── BaoCaoChinhThuc/                                    # Báo cáo học phần chính thức toàn văn (Cuối Tuần 3)
│   │   ├── BaoCaoHocPhan_QuanLyChungCuAnCu.docx            # Toàn văn báo cáo học phần đầy đủ 4 chương chuẩn EPU
│   │   └── BaoCaoHocPhan_QuanLyChungCuAnCu.pdf             # Bản PDF lưu trữ chính thức
│   ├── ThietKeUML/                                         # Bộ 115 sơ đồ UML đặc tả thiết kế hệ thống chi tiết
│   │   ├── 01_UseCase_TongQuat.png                         # Biểu đồ Use Case tổng quát toàn hệ thống
│   │   ├── UC1_TrinhTu.png, UC1_CongTac.png, UC1_HoatDong.png
│   │   ├── UC2_PhanHePhong_UML/ (16 file sơ đồ)
│   │   ├── UC3_PhanHeKhachThue_UML/ (16 file sơ đồ)
│   │   ├── UC4_PhanHeHopDong_UML/ (16 file sơ đồ)
│   │   ├── UC5_PhanHeDienNuocDichVu_UML/ (16 file sơ đồ)
│   │   ├── UC6_PhanHeHoaDonThanhToan_UML/ (16 file sơ đồ)
│   │   ├── UC7_PhanHeYeuCauThongBao_UML/ (16 file sơ đồ)
│   │   └── UC8_PhanHeBaoCaoThongKe_UML/ (16 file sơ đồ)
│   └── SlideThuyetTrinh/                                   # Slide PowerPoint bảo vệ đề tài
│       └── Slide_BaoVe_QuanLyChungCuAnCu.pptx              # Bộ slide thuyết trình thiết kế chuyên nghiệp
├── tests/                                                  # Hồ sơ kiểm thử chất lượng phần mềm
│   ├── TestPlan_KeHoachKiemThu.docx                        # Bản kế hoạch kiểm thử phần mềm 8 Use Cases
│   ├── TestCases_Matrix_QuanLyChungCu.xlsx                 # Ma trận Test Cases chi tiết (chức năng, UI, biên, bảo mật)
│   └── TestReport_KetQuaKiemThu.docx                       # Báo cáo tổng kết kết quả kiểm thử và nhật ký sửa lỗi
└── dist/                                                   # Gói phần mềm phát hành độc lập (.NET 10.0 Standalone Release)
    ├── AnCuManagement.exe                                  # File thực thi chương trình chính
    ├── AnCuManagement.dll                                  # Thư viện biên dịch cốt lõi
    ├── App.config                                          # File cấu hình kết nối CSDL khi triển khai máy mới
    └── README_HuongDanCaiDat.txt                           # Hướng dẫn đính kèm cho người dùng cuối
```

---

## 6. MỐC KIỂM SOÁT NGHIỆM THU HỌC THUẬT (05 MỐC TRONG 03 TUẦN)

| Thời điểm | Mốc kiểm soát học thuật | Tiêu chí hoàn thành & Sản phẩm bàn giao cụ thể |
| :---: | :--- | :--- |
| **Cuối Tuần 1** | **MỐC 1: Chốt đặc tả 8 Use Case & Khung kiến trúc 3-Tier (.NET 10.0)** | • Đề cương chi tiết và danh mục 8 Use Cases chuẩn hóa được thống nhất 100%.<br>• File script SQL Server tạo CSDL `QuanLyChungCuAnCu` gồm 11 bảng chuẩn 3NF hoạt động chuẩn xác.<br>• Solution Visual Studio 3-Tier (.NET 10.0) biên dịch thành công, kết nối CSDL qua `DbHelper`.<br>• Git repository được thiết lập chuẩn, cả 4 thành viên push code thử nghiệm thành công lên `develop`. |
| **Giữa Tuần 2** | **MỐC 2: BÁO CÁO TIẾN ĐỘ BÀI TẬP LỚN (Checkpoint Giữa kỳ)** | • Hoàn thành 100% tài liệu Báo cáo tiến độ bài tập lớn (.docx và .pdf) đúng chuẩn quy cách học thuật EPU.<br>• Hoàn thành Chương 1 và Chương 2: Tích hợp đầy đủ hệ thống 115 sơ đồ UML đặc tả chi tiết (Sequence, Collaboration, Activity), Kiến trúc 3 tầng và Class Diagram.<br>• Chốt 100% chữ ký hàm giữa các tầng và cấu trúc DTO. |
| **Cuối Tuần 2** | **MỐC 3: Phát hành Phiên bản Alpha (.NET 10.0 - Tích hợp liên hoàn)** | • Toàn bộ 12 Form chức năng (mỗi thành viên sở hữu đúng 3 Form) hoạt động liên hoàn từ cửa sổ chính `FormMain` theo đúng phân quyền 3 tác nhân.<br>• Dữ liệu CRUD được cập nhật bền vững vào SQL Server 2025.<br>• Giải thuật tính toán tiền điện bậc thang EVN, tiền nước và tổng hợp hóa đơn phản hồi chính xác.<br>• Toàn bộ các nhánh feature được review và merge hoàn tất vào nhánh `develop`. |
| **Giữa Tuần 3** | **MỐC 4: Phát hành Phiên bản Beta (Sửa lỗi & Tối ưu UI/UX)** | • Hoàn thành 100% các ca kiểm thử theo ma trận Test Cases cho 8 Use Cases (chức năng, giao diện, dữ liệu biên).<br>• Xử lý triệt để các bug phát hiện; ứng dụng không còn bất kỳ ngoại lệ nào làm crash chương trình.<br>• Giao diện được trau chuốt, bố cục cân đối, màu sắc hài hòa và thông báo người dùng thân thiện. |
| **Cuối Tuần 3** | **MỐC 5: Bản nộp chính thức (Final Release v1.0 & Bảo vệ đề tài)** | • Đóng gói thành công file thực thi độc lập (.NET 10.0 Standalone) kèm file cấu hình `App.config`.<br>• Báo cáo học phần chính thức toàn văn hoàn chỉnh (.docx và .pdf) đạt 100% quy chuẩn định dạng EPU.<br>• Bộ Slide PowerPoint thuyết trình bảo vệ chuyên nghiệp; Kịch bản demo hệ thống chạy thông suốt cả 3 vai trò.<br>• Đánh release tag `v1.0-final` trên GitHub và nộp bài đúng hạn theo quy định của Khoa và Nhà trường. |

---
