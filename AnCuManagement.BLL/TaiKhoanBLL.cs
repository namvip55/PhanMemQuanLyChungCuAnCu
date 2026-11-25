using System;
using System.Data;
using AnCuManagement.Common;
using AnCuManagement.DAL;
using AnCuManagement.DTO;

namespace AnCuManagement.BLL
{
    /// <summary>
    /// Tầng xử lý nghiệp vụ Tài khoản & Phân quyền (UC1)
    /// Do Nguyễn Thành Nam (MSSV: 24810310230) phụ trách phát triển.
    /// </summary>
    public class TaiKhoanBLL
    {
        private readonly TaiKhoanDAL _taiKhoanDAL = new();

        /// <summary>
        /// Tự động chuẩn hóa dữ liệu tiếng Việt không dấu nếu phát hiện CSDL cũ bị lỗi font
        /// </summary>
        public void KiemTraVaChuanHoaFont()
        {
            DbHelper.ChuanHoaFontKhongDauNeuCan();
        }

        /// <summary>
        /// Xác thực đăng nhập 3 vai trò: Chủ nhà, Quản lý, Khách thuê
        /// </summary>
        public (TaiKhoanDTO? TaiKhoan, string Message) DangNhap(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                return (null, "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.");
            }

            DataTable dt = _taiKhoanDAL.DangNhap(tenDangNhap.Trim());
            if (dt.Rows.Count == 0)
            {
                return (null, "Tên đăng nhập không tồn tại trong hệ thống.");
            }

            DataRow row = dt.Rows[0];
            int maTK = Convert.ToInt32(row["MaTaiKhoan"]);
            bool isActive = Convert.ToBoolean(row["IsActive"]);
            int soLanSai = Convert.ToInt32(row["SoLanDangNhapSai"]);

            if (!isActive || soLanSai >= AppConstants.MAX_LOGIN_ATTEMPTS)
            {
                return (null, "Tài khoản của bạn đã bị khóa do nhập sai quá 5 lần hoặc bị vô hiệu hóa. Vui lòng liên hệ Quản trị viên.");
            }

            string matKhauHash = row["MatKhauHash"].ToString() ?? "";
            string saltHash = row["SaltHash"].ToString() ?? "";

            bool hopLe = PasswordHasher.VerifyPassword(matKhau, saltHash, matKhauHash);
            if (!hopLe)
            {
                _taiKhoanDAL.TangSoLanDangNhapSai(maTK);
                int conLai = AppConstants.MAX_LOGIN_ATTEMPTS - (soLanSai + 1);
                return (null, $"Mật khẩu không chính xác! Bạn còn {Math.Max(0, conLai)} lần thử.");
            }

            // Đăng nhập thành công -> Reset số lần sai
            _taiKhoanDAL.ResetSoLanDangNhapSai(maTK);

            var tk = new TaiKhoanDTO
            {
                MaTaiKhoan = maTK,
                TenDangNhap = row["TenDangNhap"].ToString() ?? "",
                MatKhauHash = matKhauHash,
                SaltHash = saltHash,
                HoTen = row["HoTen"].ToString() ?? "",
                QuyenHan = row["QuyenHan"].ToString() ?? "",
                SoDienThoai = row["SoDienThoai"] != DBNull.Value ? row["SoDienThoai"].ToString() : null,
                Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                IsActive = isActive,
                MaKhachThue = row["MaKhachThue"] != DBNull.Value ? Convert.ToInt32(row["MaKhachThue"]) : null,
                SoLanDangNhapSai = 0
            };

            return (tk, "Đăng nhập thành công!");
        }

        /// <summary>
        /// Đổi mật khẩu người dùng
        /// </summary>
        public (bool Success, string Message) DoiMatKhau(int maTaiKhoan, string matKhauCu, string matKhauMoi, string xacNhanMoi)
        {
            if (string.IsNullOrWhiteSpace(matKhauCu) || string.IsNullOrWhiteSpace(matKhauMoi))
            {
                return (false, "Vui lòng nhập đầy đủ mật khẩu cũ và mới.");
            }

            if (matKhauMoi.Length < 6)
            {
                return (false, "Mật khẩu mới phải có tối thiểu 6 ký tự.");
            }

            if (matKhauMoi != xacNhanMoi)
            {
                return (false, "Xác nhận mật khẩu mới không trùng khớp.");
            }

            DataTable dt = _taiKhoanDAL.GetAllTaiKhoan();
            DataRow[] rows = dt.Select($"MaTaiKhoan = {maTaiKhoan}");
            if (rows.Length == 0)
            {
                return (false, "Không tìm thấy thông tin tài khoản.");
            }

            // Lấy chi tiết tài khoản từ DB để kiểm tra mật khẩu cũ
            DataTable userDt = _taiKhoanDAL.DangNhap(rows[0]["TenDangNhap"].ToString()!);
            if (userDt.Rows.Count == 0)
            {
                return (false, "Không tìm thấy thông tin tài khoản.");
            }

            string currentHash = userDt.Rows[0]["MatKhauHash"].ToString()!;
            string currentSalt = userDt.Rows[0]["SaltHash"].ToString()!;

            if (!PasswordHasher.VerifyPassword(matKhauCu, currentSalt, currentHash))
            {
                return (false, "Mật khẩu hiện tại không chính xác.");
            }

            string newSalt = PasswordHasher.GenerateSalt();
            string newHash = PasswordHasher.HashPassword(matKhauMoi, newSalt);

            int res = _taiKhoanDAL.DoiMatKhau(maTaiKhoan, newHash, newSalt);
            return res > 0 ? (true, "Đổi mật khẩu thành công!") : (false, "Lỗi khi cập nhật mật khẩu.");
        }

        public DataTable GetAllTaiKhoan()
        {
            return _taiKhoanDAL.GetAllTaiKhoan();
        }

        public (bool Success, string Message, int Id) ThemTaiKhoan(TaiKhoanDTO tk, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tk.TenDangNhap))
            {
                return (false, "Tên đăng nhập không được để trống.", 0);
            }

            if (string.IsNullOrWhiteSpace(matKhau) || matKhau.Length < 6)
            {
                return (false, "Mật khẩu phải từ 6 ký tự trở lên.", 0);
            }

            string salt = PasswordHasher.GenerateSalt();
            string hash = PasswordHasher.HashPassword(matKhau, salt);
            tk.SaltHash = salt;
            tk.MatKhauHash = hash;

            int id = _taiKhoanDAL.ThemTaiKhoan(tk);
            return id > 0 ? (true, "Tạo tài khoản thành công!", id) : (false, "Không thể tạo tài khoản.", 0);
        }
    }
}
