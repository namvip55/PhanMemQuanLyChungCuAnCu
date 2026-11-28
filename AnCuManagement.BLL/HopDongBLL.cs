using System;
using System.Data;
using AnCuManagement.Common;
using AnCuManagement.DAL;
using AnCuManagement.DTO;

namespace AnCuManagement.BLL
{
    /// <summary>
    /// Tầng xử lý nghiệp vụ Quản lý Hợp đồng thuê phòng (UC4)
    /// Do Lê Ngọc Quyền (MSSV: 24810310259) phụ trách phát triển.
    /// </summary>
    public class HopDongBLL
    {
        private readonly HopDongDAL _hopDongDAL = new();

        public DataTable GetAllHopDong()
        {
            return _hopDongDAL.GetAllHopDong();
        }

        public (bool Success, string Message, int Id) ThemHopDong(HopDongDTO hd)
        {
            if (hd.MaPhong <= 0)
            {
                return (false, "Vui lòng chọn phòng cần cho thuê.", 0);
            }

            if (hd.MaKhachThue <= 0)
            {
                return (false, "Vui lòng chọn khách thuê đại diện hợp đồng.", 0);
            }

            if (hd.TienCoc < 0)
            {
                return (false, "Tiền đặt cọc không được là số âm.", 0);
            }

            // UC4.1: Kiểm tra trạng thái phòng (chỉ cho phép Trống hoặc Đặt cọc)
            var phongDAL = new PhongDAL();
            DataTable dtPhong = phongDAL.GetPhongTheoMa(hd.MaPhong);
            if (dtPhong.Rows.Count > 0)
            {
                string status = dtPhong.Rows[0]["TrangThai"]?.ToString() ?? "";
                if (AppConstants.IsDangThue(status))
                {
                    return (false, "Lỗi: Phòng này hiện đang có người thuê, không thể lập thêm hợp đồng mới!", 0);
                }
                if (AppConstants.IsSuaChua(status))
                {
                    return (false, "Lỗi: Phòng này đang ở trạng thái 'Sửa chữa', không thể lập hợp đồng thuê mới!", 0);
                }
                if (!AppConstants.IsTrong(status) && !AppConstants.IsDatCoc(status))
                {
                    return (false, $"Lỗi: Phòng đang ở trạng thái '{status}', chỉ có thể lập hợp đồng cho phòng 'Trống' hoặc 'Đặt cọc'!", 0);
                }
            }

            if (hd.NgayKetThuc <= hd.NgayBatDau)
            {
                return (false, "Ngày kết thúc hợp đồng phải sau ngày bắt đầu.", 0);
            }

            if (hd.GiaThueThucTe <= 0)
            {
                return (false, "Giá thuê thực tế phải lớn hơn 0.", 0);
            }

            int id = _hopDongDAL.ThemHopDong(hd);
            return id > 0 ? (true, "Lập hợp đồng thuê mới thành công!", id) : (false, "Không thể lập hợp đồng.", 0);
        }

        public (bool Success, string Message) GiaHanHopDong(int maHopDong, DateTime ngayKetThucMoi, decimal? giaMoi)
        {
            if (maHopDong <= 0)
            {
                return (false, "Mã hợp đồng không hợp lệ.");
            }

            // UC4.3: Kiểm tra thời hạn mới phải sau ngày hết hạn cũ
            DataTable allHD = _hopDongDAL.GetAllHopDong();
            foreach (DataRow r in allHD.Rows)
            {
                if (Convert.ToInt32(r["MaHopDong"]) == maHopDong)
                {
                    if (DateTime.TryParse(r["NgayKetThuc"]?.ToString(), out DateTime oldEndDate))
                    {
                        if (ngayKetThucMoi <= oldEndDate)
                        {
                            return (false, $"Lỗi: Ngày kết thúc mới ({ngayKetThucMoi:dd/MM/yyyy}) phải sau ngày kết thúc hiện tại ({oldEndDate:dd/MM/yyyy})!");
                        }
                    }
                    string trangThai = r["TrangThai"]?.ToString() ?? "";
                    if (trangThai != AppConstants.TRANG_THAI_HOP_DONG_HIEU_LUC)
                    {
                        return (false, "Lỗi: Hợp đồng đã thanh lý hoặc hết hiệu lực, không đủ điều kiện gia hạn!");
                    }
                    break;
                }
            }

            if (giaMoi.HasValue && giaMoi.Value <= 0)
            {
                return (false, "Giá thuê mới phải lớn hơn 0.");
            }

            int res = _hopDongDAL.GiaHanHopDong(maHopDong, ngayKetThucMoi, giaMoi);
            return res > 0 ? (true, "Gia hạn hợp đồng thành công!") : (false, "Gia hạn thất bại.");
        }

        public (bool Success, string Message) ChamDutHopDong(int maHopDong)
        {
            if (maHopDong <= 0)
            {
                return (false, "Mã hợp đồng không hợp lệ.");
            }

            // UC4.4: Kiểm tra công nợ tồn đọng của phòng trước khi thanh lý
            DataTable allHD = _hopDongDAL.GetAllHopDong();
            int maPhong = 0;
            foreach (DataRow r in allHD.Rows)
            {
                if (Convert.ToInt32(r["MaHopDong"]) == maHopDong)
                {
                    maPhong = Convert.ToInt32(r["MaPhong"]);
                    break;
                }
            }

            if (maPhong > 0)
            {
                var hoaDonDAL = new HoaDonThanhToanDAL();
                DataTable dtHD = hoaDonDAL.GetHoaDonTheoPhong(maPhong);
                foreach (DataRow r in dtHD.Rows)
                {
                    string trangThaiHD = r["TrangThai"]?.ToString() ?? "";
                    if (trangThaiHD == AppConstants.TRANG_THAI_HOA_DON_CHUA_TT || trangThaiHD == AppConstants.TRANG_THAI_HOA_DON_QUA_HAN)
                    {
                        return (false, "Cảnh báo: Căn hộ này còn hóa đơn tiền nhà / dịch vụ chưa thanh toán hoặc đã quá hạn! Vui lòng tất toán toàn bộ công nợ trước khi thanh lý hợp đồng.");
                    }
                }
            }

            int res = _hopDongDAL.ChamDutHopDong(maHopDong);
            return res > 0 ? (true, "Thanh lý hợp đồng và trả phòng thành công!") : (false, "Thanh lý thất bại.");
        }

        public DataTable GetHopDongTheoPhong(int maPhong)
        {
            return _hopDongDAL.GetHopDongTheoPhong(maPhong);
        }

        public DataTable GetHopDongTheoKhach(int maKhachThue)
        {
            return _hopDongDAL.GetHopDongTheoKhach(maKhachThue);
        }
    }
}
