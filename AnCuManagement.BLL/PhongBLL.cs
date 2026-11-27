using System;
using System.Data;
using AnCuManagement.Common;
using AnCuManagement.DAL;
using AnCuManagement.DTO;

namespace AnCuManagement.BLL
{
    /// <summary>
    /// Tầng xử lý nghiệp vụ Quản lý phòng (UC2)
    /// Do Phan Công Thành Đạt (MSSV: 24810310223) phụ trách phát triển.
    /// </summary>
    public class PhongBLL
    {
        private readonly PhongDAL _phongDAL = new();

        public DataTable GetAllPhong()
        {
            return _phongDAL.GetAllPhong();
        }

        public DataTable GetAllToaNha()
        {
            return _phongDAL.GetAllToaNha();
        }

        public (bool Success, string Message, int Id) ThemPhong(PhongDTO phong)
        {
            if (string.IsNullOrWhiteSpace(phong.SoPhong))
            {
                return (false, "Số phòng không được để trống.", 0);
            }

            if (phong.Tang <= 0)
            {
                return (false, "Số tầng phải lớn hơn 0.", 0);
            }

            if (phong.GiaThue <= 0)
            {
                return (false, "Giá thuê phòng phải lớn hơn 0.", 0);
            }

            if (phong.DienTich <= 0)
            {
                return (false, "Diện tích phòng phải lớn hơn 0 m².", 0);
            }

            // UC2.1: Kiểm tra trùng số phòng trong tòa nhà
            DataTable allPhong = _phongDAL.GetAllPhong();
            foreach (DataRow r in allPhong.Rows)
            {
                if (string.Equals(r["SoPhong"]?.ToString()?.Trim(), phong.SoPhong.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    Convert.ToInt32(r["MaToaNha"]) == phong.MaToaNha)
                {
                    return (false, $"Lỗi: Phòng số '{phong.SoPhong}' đã tồn tại trong tòa nhà này!", 0);
                }
            }

            if (string.IsNullOrWhiteSpace(phong.TrangThai))
            {
                phong.TrangThai = AppConstants.TRANG_THAI_PHONG_TRONG;
            }

            int id = _phongDAL.ThemPhong(phong);
            return id > 0 ? (true, "Thêm phòng mới thành công!", id) : (false, "Không thể thêm phòng.", 0);
        }

        public (bool Success, string Message) CapNhatPhong(PhongDTO phong)
        {
            if (phong.MaPhong <= 0)
            {
                return (false, "Mã phòng không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(phong.SoPhong))
            {
                return (false, "Số phòng không được để trống.");
            }

            if (phong.Tang <= 0)
            {
                return (false, "Số tầng phải lớn hơn 0.");
            }

            if (phong.DienTich <= 0)
            {
                return (false, "Diện tích phòng phải lớn hơn 0 m².");
            }

            if (phong.GiaThue <= 0)
            {
                return (false, "Giá thuê phòng phải lớn hơn 0.");
            }

            // Kiểm tra trùng số phòng trong tòa nhà (bỏ qua chính phòng đang sửa)
            DataTable allPhong = _phongDAL.GetAllPhong();
            foreach (DataRow r in allPhong.Rows)
            {
                int rMaPhong = Convert.ToInt32(r["MaPhong"]);
                if (rMaPhong != phong.MaPhong &&
                    string.Equals(r["SoPhong"]?.ToString()?.Trim(), phong.SoPhong.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    Convert.ToInt32(r["MaToaNha"]) == phong.MaToaNha)
                {
                    return (false, $"Lỗi: Phòng số '{phong.SoPhong}' đã tồn tại trong tòa nhà này!");
                }
            }

            // Kiểm tra nếu chuyển sang Trống / SửaChữa mà phòng đang có hợp đồng hiệu lực
            if (AppConstants.IsTrong(phong.TrangThai) || AppConstants.IsSuaChua(phong.TrangThai))
            {
                var hopDongDAL = new HopDongDAL();
                DataTable dtHD = hopDongDAL.GetHopDongTheoPhong(phong.MaPhong);
                foreach (DataRow r in dtHD.Rows)
                {
                    if (r["TrangThai"]?.ToString() == AppConstants.TRANG_THAI_HOP_DONG_HIEU_LUC)
                    {
                        return (false, "Cảnh báo: Phòng đang có hợp đồng thuê còn hiệu lực! Vui lòng thanh lý hợp đồng trước khi chuyển trạng thái.");
                    }
                }
            }

            int res = _phongDAL.CapNhatPhong(phong);
            return res > 0 ? (true, "Cập nhật thông tin phòng thành công!") : (false, "Cập nhật thất bại.");
        }

        public (bool Success, string Message) CapNhatTrangThaiPhong(int maPhong, string trangThai)
        {
            if (maPhong <= 0)
            {
                return (false, "Mã phòng không hợp lệ.");
            }

            // UC2.3: Kiểm tra ràng buộc hợp đồng liên quan khi chuyển sang Trống / Sửa chữa
            if (AppConstants.IsTrong(trangThai) || AppConstants.IsSuaChua(trangThai))
            {
                var hopDongDAL = new HopDongDAL();
                DataTable dtHD = hopDongDAL.GetHopDongTheoPhong(maPhong);
                foreach (DataRow r in dtHD.Rows)
                {
                    if (r["TrangThai"]?.ToString() == AppConstants.TRANG_THAI_HOP_DONG_HIEU_LUC)
                    {
                        return (false, "Cảnh báo: Phòng đang có hợp đồng thuê còn hiệu lực! Vui lòng thanh lý hợp đồng trước khi chuyển trạng thái.");
                    }
                }
            }

            int res = _phongDAL.CapNhatTrangThaiPhong(maPhong, trangThai);
            return res > 0 ? (true, "Cập nhật trạng thái phòng thành công!") : (false, "Cập nhật thất bại.");
        }

        /// <summary>
        /// UC2.5: Quản lý giá thuê phòng linh hoạt
        /// </summary>
        public (bool Success, string Message) CapNhatGiaThue(int maPhong, decimal giaThueMoi)
        {
            if (maPhong <= 0)
            {
                return (false, "Mã phòng không hợp lệ.");
            }

            if (giaThueMoi <= 0)
            {
                return (false, "Giá thuê phòng phải lớn hơn 0 VNĐ.");
            }

            DataTable dt = _phongDAL.GetPhongTheoMa(maPhong);
            if (dt.Rows.Count == 0)
            {
                return (false, "Không tìm thấy phòng tương ứng.");
            }

            DataRow row = dt.Rows[0];
            var phong = new PhongDTO
            {
                MaPhong = maPhong,
                SoPhong = row["SoPhong"].ToString() ?? "",
                Tang = Convert.ToInt32(row["Tang"]),
                DienTich = Convert.ToDecimal(row["DienTich"]),
                GiaThue = giaThueMoi,
                TrangThai = row["TrangThai"].ToString() ?? AppConstants.TRANG_THAI_PHONG_TRONG,
                LoaiPhong = row["LoaiPhong"]?.ToString(),
                MoTa = row["MoTa"]?.ToString(),
                MaToaNha = Convert.ToInt32(row["MaToaNha"])
            };

            int res = _phongDAL.CapNhatPhong(phong);
            return res > 0 ? (true, $"Cập nhật biểu giá thuê phòng thành công: {FormatHelper.FormatTienVND(giaThueMoi)}") : (false, "Cập nhật giá thuê thất bại.");
        }

        public DataTable TimKiemPhong(int? tang, string? trangThai, decimal? giaMin, decimal? giaMax)
        {
            return _phongDAL.TimKiemPhong(tang, trangThai, giaMin, giaMax);
        }

        public DataTable GetPhongTheoMa(int maPhong)
        {
            return _phongDAL.GetPhongTheoMa(maPhong);
        }
    }
}
