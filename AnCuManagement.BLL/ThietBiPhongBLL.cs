using System;
using System.Data;
using AnCuManagement.DAL;
using AnCuManagement.DTO;

namespace AnCuManagement.BLL
{
    /// <summary>
    /// Tầng xử lý nghiệp vụ Thiết bị & Tiện nghi phòng (UC2)
    /// Do Phan Công Thành Đạt (MSSV: 24810310223) phụ trách phát triển.
    /// </summary>
    public class ThietBiPhongBLL
    {
        private readonly ThietBiPhongDAL _thietBiDAL = new();

        public DataTable GetThietBiTheoPhong(int maPhong)
        {
            return _thietBiDAL.GetThietBiTheoPhong(maPhong);
        }

        public (bool Success, string Message, int Id) ThemThietBi(ThietBiPhongDTO tb)
        {
            if (tb.MaPhong <= 0)
            {
                return (false, "Phòng không hợp lệ.", 0);
            }

            if (string.IsNullOrWhiteSpace(tb.TenThietBi))
            {
                return (false, "Tên thiết bị không được để trống.", 0);
            }

            if (tb.SoLuong <= 0)
            {
                return (false, "Số lượng phải lớn hơn 0.", 0);
            }

            int id = _thietBiDAL.ThemThietBi(tb);
            return id > 0 ? (true, "Thêm thiết bị thành công!", id) : (false, "Không thể thêm thiết bị.", 0);
        }

        public (bool Success, string Message) CapNhatThietBi(ThietBiPhongDTO tb)
        {
            if (tb.MaThietBi <= 0)
            {
                return (false, "Mã thiết bị không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(tb.TenThietBi))
            {
                return (false, "Tên thiết bị không được để trống.");
            }

            int res = _thietBiDAL.CapNhatThietBi(tb);
            return res > 0 ? (true, "Cập nhật thiết bị thành công!") : (false, "Cập nhật thất bại.");
        }

        public (bool Success, string Message) XoaThietBi(int maThietBi)
        {
            if (maThietBi <= 0)
            {
                return (false, "Mã thiết bị không hợp lệ.");
            }

            int res = _thietBiDAL.XoaThietBi(maThietBi);
            return res > 0 ? (true, "Xóa thiết bị thành công!") : (false, "Không thể xóa thiết bị.");
        }
    }
}
