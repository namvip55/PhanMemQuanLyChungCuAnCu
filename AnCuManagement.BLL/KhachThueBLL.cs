using System;
using System.Data;
using AnCuManagement.Common;
using AnCuManagement.DAL;
using AnCuManagement.DTO;

namespace AnCuManagement.BLL
{
    /// <summary>
    /// Tầng xử lý nghiệp vụ Quản lý Khách thuê & Cư dân (UC3)
    /// Do Phan Công Thành Đạt (MSSV: 24810310223) phụ trách phát triển.
    /// </summary>
    public class KhachThueBLL
    {
        private readonly KhachThueDAL _khachThueDAL = new();
        private readonly TaiKhoanDAL _taiKhoanDAL = new();

        public DataTable GetAllKhachThue()
        {
            return _khachThueDAL.GetAllKhachThue();
        }

        public (bool Success, string Message, int Id) ThemKhachThue(KhachThueDTO kt, bool taoTaiKhoan = true)
        {
            if (string.IsNullOrWhiteSpace(kt.HoTen))
            {
                return (false, "Họ và tên cư dân không được để trống.", 0);
            }

            if (!ValidationHelper.IsValidCCCD(kt.CCCD))
            {
                return (false, "Số CCCD không hợp lệ! Vui lòng nhập đúng 12 chữ số.", 0);
            }

            // UC3.2: Kiểm tra trùng số CCCD cư dân toàn bảng (kể cả khách đã xóa mềm)
            if (_khachThueDAL.KiemTraTrungCCCD(kt.CCCD.Trim()))
            {
                return (false, $"Lỗi: Số CCCD '{kt.CCCD}' đã tồn tại trong hệ thống quản lý cư dân!", 0);
            }

            if (!string.IsNullOrWhiteSpace(kt.SoDienThoai) && !ValidationHelper.IsValidSoDienThoai(kt.SoDienThoai))
            {
                return (false, "Số điện thoại không hợp lệ! Vui lòng nhập 10 chữ số bắt đầu bằng 0.", 0);
            }

            if (!string.IsNullOrWhiteSpace(kt.Email) && !ValidationHelper.IsValidEmail(kt.Email))
            {
                return (false, "Địa chỉ Email không đúng định dạng.", 0);
            }

            DataTable allKhach = _khachThueDAL.GetAllKhachThue();

            // UC3.3: Bảo đảm mỗi phòng chỉ có tối đa 1 chủ hộ đại diện
            if (kt.LaChuHo && kt.MaPhong.HasValue && kt.MaPhong.Value > 0)
            {
                foreach (DataRow r in allKhach.Rows)
                {
                    if (r["MaPhong"] != DBNull.Value && Convert.ToInt32(r["MaPhong"]) == kt.MaPhong.Value)
                    {
                        bool isChuHo = Convert.ToBoolean(r["LaChuHo"]);
                        if (isChuHo)
                        {
                            string tenChuHo = r["HoTen"]?.ToString() ?? "Cư dân khác";
                            return (false, $"Lỗi: Phòng này hiện đã có chủ hộ là '{tenChuHo}'. Mỗi phòng chỉ được có tối đa 1 chủ hộ đại diện!", 0);
                        }
                    }
                }
            }

            int id = _khachThueDAL.ThemKhachThue(kt);
            if (id <= 0)
            {
                return (false, "Không thể thêm hồ sơ khách thuê.", 0);
            }

            string resultMsg = "Thêm hồ sơ khách thuê thành công!";

            // UML UC3.2: Tự động cấp tài khoản đăng nhập cho cư dân
            if (taoTaiKhoan)
            {
                try
                {
                    string username = !string.IsNullOrWhiteSpace(kt.SoDienThoai) ? kt.SoDienThoai.Trim() : kt.CCCD.Trim();
                    string defaultPass = kt.CCCD.Trim().Length >= 6 ? kt.CCCD.Trim().Substring(kt.CCCD.Trim().Length - 6) : "123456";

                    // Kiểm tra username đã tồn tại chưa
                    bool usernameExists = false;
                    DataTable dtTk = _taiKhoanDAL.GetAllTaiKhoan();
                    foreach (DataRow r in dtTk.Rows)
                    {
                        if (string.Equals(r["TenDangNhap"]?.ToString()?.Trim(), username, StringComparison.OrdinalIgnoreCase))
                        {
                            usernameExists = true;
                            break;
                        }
                    }

                    if (!usernameExists)
                    {
                        string salt = PasswordHasher.GenerateSalt();
                        string hash = PasswordHasher.HashPassword(defaultPass, salt);
                        var tk = new TaiKhoanDTO
                        {
                            TenDangNhap = username,
                            MatKhauHash = hash,
                            SaltHash = salt,
                            HoTen = kt.HoTen,
                            QuyenHan = AppConstants.QUYEN_KHACH_THUE,
                            SoDienThoai = kt.SoDienThoai,
                            Email = kt.Email,
                            MaKhachThue = id
                        };
                        _taiKhoanDAL.ThemTaiKhoan(tk);
                        resultMsg += $"\nĐã tạo tài khoản cư dân (Tên đăng nhập: {username}, Mật khẩu mặc định: {defaultPass}).";
                    }
                }
                catch { }
            }

            return (true, resultMsg, id);
        }

        public (bool Success, string Message) CapNhatKhachThue(KhachThueDTO kt)
        {
            if (kt.MaKhachThue <= 0)
            {
                return (false, "Mã khách thuê không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(kt.HoTen))
            {
                return (false, "Họ và tên không được để trống.");
            }

            if (!ValidationHelper.IsValidCCCD(kt.CCCD))
            {
                return (false, "Số CCCD không hợp lệ! Vui lòng nhập đúng 12 chữ số.");
            }

            // Kiểm tra trùng CCCD với cư dân khác
            if (_khachThueDAL.KiemTraTrungCCCD(kt.CCCD.Trim(), kt.MaKhachThue))
            {
                return (false, $"Lỗi: Số CCCD '{kt.CCCD}' đã tồn tại trong hệ thống quản lý cư dân!");
            }

            if (!string.IsNullOrWhiteSpace(kt.SoDienThoai) && !ValidationHelper.IsValidSoDienThoai(kt.SoDienThoai))
            {
                return (false, "Số điện thoại không hợp lệ! Vui lòng nhập 10 chữ số.");
            }

            if (!string.IsNullOrWhiteSpace(kt.Email) && !ValidationHelper.IsValidEmail(kt.Email))
            {
                return (false, "Địa chỉ Email không đúng định dạng.");
            }

            // UC3.3: Bảo đảm mỗi phòng chỉ có tối đa 1 chủ hộ đại diện
            if (kt.LaChuHo && kt.MaPhong.HasValue && kt.MaPhong.Value > 0)
            {
                DataTable allKhach = _khachThueDAL.GetAllKhachThue();
                foreach (DataRow r in allKhach.Rows)
                {
                    int maKTCu = Convert.ToInt32(r["MaKhachThue"]);
                    if (maKTCu != kt.MaKhachThue && r["MaPhong"] != DBNull.Value && Convert.ToInt32(r["MaPhong"]) == kt.MaPhong.Value)
                    {
                        bool isChuHo = Convert.ToBoolean(r["LaChuHo"]);
                        if (isChuHo)
                        {
                            string tenChuHo = r["HoTen"]?.ToString() ?? "Cư dân khác";
                            return (false, $"Lỗi: Phòng này hiện đã có chủ hộ là '{tenChuHo}'. Mỗi phòng chỉ được có tối đa 1 chủ hộ đại diện!");
                        }
                    }
                }
            }

            int res = _khachThueDAL.CapNhatKhachThue(kt);
            return res > 0 ? (true, "Cập nhật hồ sơ cư dân thành công!") : (false, "Cập nhật thất bại.");
        }

        public (bool Success, string Message) XoaMemKhachThue(int maKhachThue)
        {
            if (maKhachThue <= 0)
            {
                return (false, "Mã khách thuê không hợp lệ.");
            }

            // UC3.4: Ràng buộc khách đang có hợp đồng thuê hiệu lực thì không được xóa
            var hopDongDAL = new HopDongDAL();
            DataTable dtHD = hopDongDAL.GetHopDongTheoKhach(maKhachThue);
            foreach (DataRow r in dtHD.Rows)
            {
                if (r["TrangThai"]?.ToString() == AppConstants.TRANG_THAI_HOP_DONG_HIEU_LUC)
                {
                    return (false, "Cảnh báo: Khách thuê đang đứng tên hợp đồng thuê có hiệu lực! Không thể xóa hồ sơ.");
                }
            }

            int res = _khachThueDAL.XoaMemKhachThue(maKhachThue);
            return res > 0 ? (true, "Đã xóa hồ sơ khách thuê khỏi danh sách cư dân đang ở.") : (false, "Xóa thất bại.");
        }

        /// <summary>
        /// UC3.5: Xác thực khách thuê qua số CCCD hoặc SĐT
        /// </summary>
        public (bool Success, string Message, DataRow? KhachThue) XacThucKhachThue(string cccdOrPhone)
        {
            if (string.IsNullOrWhiteSpace(cccdOrPhone))
            {
                return (false, "Vui lòng nhập số CCCD hoặc Số điện thoại để xác thực.", null);
            }

            string keyword = cccdOrPhone.Trim();
            DataTable dt = _khachThueDAL.GetAllKhachThue();
            foreach (DataRow r in dt.Rows)
            {
                string cccd = r["CCCD"]?.ToString()?.Trim() ?? "";
                string sdt = r["SoDienThoai"]?.ToString()?.Trim() ?? "";
                if (cccd == keyword || sdt == keyword)
                {
                    string hoTen = r["HoTen"]?.ToString() ?? "";
                    string soPhong = r["SoPhong"]?.ToString() ?? "Chưa gán";
                    return (true, $"Xác thực thành công cư dân: {hoTen} (CCCD: {cccd}, Phòng: {soPhong})", r);
                }
            }

            return (false, "Không tìm thấy hồ sơ cư dân khớp với thông tin định danh!", null);
        }

        public DataTable TimKiemKhachThue(string? keyword)
        {
            return _khachThueDAL.TimKiemKhachThue(keyword);
        }

        /// <summary>
        /// Lấy mã phòng của khách thuê đang lưu trú hoặc đứng tên hợp đồng hiệu lực
        /// </summary>
        public int LayMaPhongCuaKhach(int maKhachThue)
        {
            if (maKhachThue <= 0) return 0;

            DataTable dt = _khachThueDAL.GetAllKhachThue();
            foreach (DataRow r in dt.Rows)
            {
                if (Convert.ToInt32(r["MaKhachThue"]) == maKhachThue && r["MaPhong"] != DBNull.Value)
                {
                    return Convert.ToInt32(r["MaPhong"]);
                }
            }

            var hopDongDAL = new HopDongDAL();
            DataTable dtHD = hopDongDAL.GetHopDongTheoKhach(maKhachThue);
            foreach (DataRow r in dtHD.Rows)
            {
                string tt = r["TrangThai"]?.ToString() ?? "";
                if (tt == AppConstants.TRANG_THAI_HOP_DONG_HIEU_LUC && r["MaPhong"] != DBNull.Value)
                {
                    return Convert.ToInt32(r["MaPhong"]);
                }
            }

            return 0;
        }

        /// <summary>
        /// Lấy mã khách thuê (ưu tiên chủ hộ) đang cư trú tại phòng
        /// </summary>
        public int LayMaKhachThueTheoPhong(int maPhong)
        {
            if (maPhong <= 0) return 0;
            DataTable dt = _khachThueDAL.GetAllKhachThue();
            int firstTenant = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["MaPhong"] != DBNull.Value && Convert.ToInt32(r["MaPhong"]) == maPhong)
                {
                    int ktId = Convert.ToInt32(r["MaKhachThue"]);
                    if (firstTenant == 0) firstTenant = ktId;
                    if (r["LaChuHo"] != DBNull.Value && Convert.ToBoolean(r["LaChuHo"]))
                    {
                        return ktId;
                    }
                }
            }
            return firstTenant;
        }
    }
}
