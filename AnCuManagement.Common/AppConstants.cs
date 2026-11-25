namespace AnCuManagement.Common
{
    /// <summary>
    /// Lớp chứa các hằng số dùng chung trong toàn hệ thống Quản lý Chung cư An Cư.
    /// </summary>
    public static class AppConstants
    {
        // ===== QUYỀN NGƯỜI DÙNG =====
        /// <summary>Quyền Chủ nhà</summary>
        public const string QUYEN_CHU_NHA = "ChuNha";
        /// <summary>Quyền Quản lý</summary>
        public const string QUYEN_QUAN_LY = "QuanLy";
        /// <summary>Quyền Khách thuê</summary>
        public const string QUYEN_KHACH_THUE = "KhachThue";

        // ===== TRẠNG THÁI PHÒNG =====
        /// <summary>Phòng trống, chưa có người thuê</summary>
        public const string TRANG_THAI_PHONG_TRONG = "Trống";
        public const string TRANG_THAI_PHONG_TRONG_KD = "Trong";

        /// <summary>Phòng đang được thuê</summary>
        public const string TRANG_THAI_PHONG_DANG_THUE = "ĐangThuê";
        public const string TRANG_THAI_PHONG_DANG_THUE_KD = "DangThue";

        /// <summary>Phòng đang sửa chữa, bảo trì</summary>
        public const string TRANG_THAI_PHONG_SUA_CHUA = "SửaChữa";
        public const string TRANG_THAI_PHONG_SUA_CHUA_KD = "SuaChua";

        /// <summary>Phòng đã đặt cọc</summary>
        public const string TRANG_THAI_PHONG_DAT_COC = "ĐặtCọc";
        public const string TRANG_THAI_PHONG_DAT_COC_KD = "DatCoc";

        public static bool IsDangThue(string? s) => s == TRANG_THAI_PHONG_DANG_THUE || s == TRANG_THAI_PHONG_DANG_THUE_KD || (s != null && s.IndexOf("DangThue", StringComparison.OrdinalIgnoreCase) >= 0);
        public static bool IsTrong(string? s) => s == TRANG_THAI_PHONG_TRONG || s == TRANG_THAI_PHONG_TRONG_KD || (s != null && s.IndexOf("Trong", StringComparison.OrdinalIgnoreCase) >= 0);
        public static bool IsSuaChua(string? s) => s == TRANG_THAI_PHONG_SUA_CHUA || s == TRANG_THAI_PHONG_SUA_CHUA_KD || (s != null && s.IndexOf("SuaChua", StringComparison.OrdinalIgnoreCase) >= 0);
        public static bool IsDatCoc(string? s) => s == TRANG_THAI_PHONG_DAT_COC || s == TRANG_THAI_PHONG_DAT_COC_KD || (s != null && s.IndexOf("DatCoc", StringComparison.OrdinalIgnoreCase) >= 0);

        // ===== TRẠNG THÁI HỢP ĐỒNG =====
        /// <summary>Hợp đồng đang có hiệu lực</summary>
        public const string TRANG_THAI_HD_HIEU_LUC = "HieuLuc";
        public const string TRANG_THAI_HOP_DONG_HIEU_LUC = "HieuLuc";
        /// <summary>Hợp đồng đã hết hạn</summary>
        public const string TRANG_THAI_HD_HET_HAN = "HetHan";
        public const string TRANG_THAI_HOP_DONG_HET_HAN = "HetHan";
        /// <summary>Hợp đồng đã thanh lý</summary>
        public const string TRANG_THAI_HD_DA_THANH_LY = "DaThanhLy";
        public const string TRANG_THAI_HOP_DONG_DA_THANH_LY = "DaThanhLy";

        // ===== TRẠNG THÁI HÓA ĐƠN =====
        /// <summary>Hóa đơn chưa thanh toán</summary>
        public const string TRANG_THAI_HOA_DON_CHUA_TT = "ChuaThanhToan";
        /// <summary>Hóa đơn đã thanh toán</summary>
        public const string TRANG_THAI_HOA_DON_DA_TT = "DaThanhToan";
        /// <summary>Hóa đơn quá hạn thanh toán</summary>
        public const string TRANG_THAI_HOA_DON_QUA_HAN = "QuaHan";

        // ===== TRẠNG THÁI YÊU CẦU =====
        /// <summary>Yêu cầu mới gửi</summary>
        public const string TRANG_THAI_YEU_CAU_MOI = "MoiGui";
        /// <summary>Yêu cầu đang xử lý</summary>
        public const string TRANG_THAI_YEU_CAU_DANG_XU_LY = "DangXuLy";
        /// <summary>Yêu cầu đã hoàn thành</summary>
        public const string TRANG_THAI_YEU_CAU_HOAN_THANH = "HoanThanh";
        /// <summary>Yêu cầu bị từ chối</summary>
        public const string TRANG_THAI_YEU_CAU_TU_CHOI = "TuChoi";

        // ===== LOẠI YÊU CẦU =====
        /// <summary>Báo hỏng, sửa chữa thiết bị</summary>
        public const string LOAI_YEU_CAU_BAO_HONG = "BaoHong";
        /// <summary>Khiếu nại, phản ánh dịch vụ</summary>
        public const string LOAI_YEU_CAU_KHIEU_NAI = "KhieuNai";
        /// <summary>Yêu cầu dịch vụ</summary>
        public const string LOAI_YEU_CAU_DICH_VU = "DichVu";
        /// <summary>Yêu cầu hợp đồng (gia hạn / chấm dứt)</summary>
        public const string LOAI_YEU_CAU_HOP_DONG = "HopDong";

        // ===== HÌNH THỨC THANH TOÁN =====
        /// <summary>Thanh toán bằng tiền mặt</summary>
        public const string HINH_THUC_TIEN_MAT = "TienMat";
        /// <summary>Thanh toán bằng chuyển khoản ngân hàng</summary>
        public const string HINH_THUC_CHUYEN_KHOAN = "ChuyenKhoan";

        // ===== BIỂU GIÁ ĐIỆN BẬC THANG (EVN) =====
        /// <summary>
        /// Giới hạn trên của từng bậc thang điện (kWh).
        /// Bậc 1: 0-50, Bậc 2: 51-100, Bậc 3: 101-200, Bậc 4: 201-300, Bậc 5: 301-400, Bậc 6: 401+
        /// </summary>
        public static readonly int[] DIEN_BAC_THANG_GIOI_HAN = { 50, 100, 200, 300, 400, int.MaxValue };

        /// <summary>
        /// Đơn giá điện theo từng bậc thang (VND/kWh) - theo biểu giá EVN.
        /// Bậc 1: 1.893đ, Bậc 2: 1.956đ, Bậc 3: 2.271đ, Bậc 4: 2.860đ, Bậc 5: 3.197đ, Bậc 6: 3.302đ
        /// </summary>
        public static readonly decimal[] DIEN_BAC_THANG_DON_GIA = { 1893m, 1956m, 2271m, 2860m, 3197m, 3302m };

        // ===== ĐƠN GIÁ NƯỚC =====
        /// <summary>Đơn giá nước (VND/m³)</summary>
        public const decimal DON_GIA_NUOC = 15000m;

        // ===== BẢO MẬT =====
        /// <summary>Số lần đăng nhập sai tối đa trước khi khóa tài khoản</summary>
        public const int MAX_LOGIN_ATTEMPTS = 5;
    }
}
