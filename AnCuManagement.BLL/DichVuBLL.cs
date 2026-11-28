using System;
using System.Data;
using AnCuManagement.Common;
using AnCuManagement.DAL;
using AnCuManagement.DTO;

namespace AnCuManagement.BLL
{
    /// <summary>
    /// Tầng xử lý nghiệp vụ Bảng giá Dịch vụ & Giải thuật tính điện nước (UC5)
    /// Do Lê Ngọc Quyền (MSSV: 24810310259) phụ trách phát triển.
    /// </summary>
    public class DichVuBLL
    {
        private readonly DichVuDAL _dichVuDAL = new();

        public DataTable GetAllDichVu()
        {
            return _dichVuDAL.GetAllDichVu();
        }

        public (bool Success, string Message, int Id) ThemDichVu(DichVuDTO dv)
        {
            if (string.IsNullOrWhiteSpace(dv.TenDichVu))
            {
                return (false, "Tên dịch vụ không được để trống.", 0);
            }

            // UC5.2: Kiểm tra trùng lặp tên dịch vụ
            DataTable allDV = _dichVuDAL.GetAllDichVu();
            foreach (DataRow r in allDV.Rows)
            {
                if (string.Equals(r["TenDichVu"]?.ToString()?.Trim(), dv.TenDichVu.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return (false, $"Lỗi: Gói dịch vụ '{dv.TenDichVu}' đã tồn tại trong danh mục!", 0);
                }
            }

            if (dv.DonGia <= 0)
            {
                return (false, "Đơn giá dịch vụ phải lớn hơn 0.", 0);
            }

            int id = _dichVuDAL.ThemDichVu(dv);
            return id > 0 ? (true, "Thêm dịch vụ thành công!", id) : (false, "Không thể thêm dịch vụ.", 0);
        }

        public (bool Success, string Message) CapNhatDichVu(DichVuDTO dv)
        {
            if (dv.MaDichVu <= 0)
            {
                return (false, "Mã dịch vụ không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(dv.TenDichVu))
            {
                return (false, "Tên dịch vụ không được để trống.");
            }

            if (dv.DonGia <= 0)
            {
                return (false, "Đơn giá dịch vụ phải lớn hơn 0.");
            }

            int res = _dichVuDAL.CapNhatDichVu(dv);
            return res > 0 ? (true, "Cập nhật dịch vụ thành công!") : (false, "Cập nhật thất bại.");
        }

        /// <summary>
        /// Giải thuật tính tiền điện sinh hoạt bậc thang lũy tiến theo quy chuẩn EVN 6 bậc:
        /// Bậc 1: 0 - 50 kWh: 1.893 đ
        /// Bậc 2: 51 - 100 kWh: 1.956 đ
        /// Bậc 3: 101 - 200 kWh: 2.271 đ
        /// Bậc 4: 201 - 300 kWh: 2.860 đ
        /// Bậc 5: 301 - 400 kWh: 3.197 đ
        /// Bậc 6: 401+ kWh: 3.302 đ
        /// </summary>
        public decimal TinhTienDien(int soDienTieuThu)
        {
            if (soDienTieuThu <= 0) return 0;

            decimal tongTien = 0;
            int conLai = soDienTieuThu;

            // Bậc 1: Tối đa 50 kWh đầu
            int muc1 = Math.Min(conLai, 50);
            tongTien += muc1 * AppConstants.DIEN_BAC_THANG_DON_GIA[0];
            conLai -= muc1;

            if (conLai > 0)
            {
                // Bậc 2: 50 kWh tiếp theo (51 - 100)
                int muc2 = Math.Min(conLai, 50);
                tongTien += muc2 * AppConstants.DIEN_BAC_THANG_DON_GIA[1];
                conLai -= muc2;
            }

            if (conLai > 0)
            {
                // Bậc 3: 100 kWh tiếp theo (101 - 200)
                int muc3 = Math.Min(conLai, 100);
                tongTien += muc3 * AppConstants.DIEN_BAC_THANG_DON_GIA[2];
                conLai -= muc3;
            }

            if (conLai > 0)
            {
                // Bậc 4: 100 kWh tiếp theo (201 - 300)
                int muc4 = Math.Min(conLai, 100);
                tongTien += muc4 * AppConstants.DIEN_BAC_THANG_DON_GIA[3];
                conLai -= muc4;
            }

            if (conLai > 0)
            {
                // Bậc 5: 100 kWh tiếp theo (301 - 400)
                int muc5 = Math.Min(conLai, 100);
                tongTien += muc5 * AppConstants.DIEN_BAC_THANG_DON_GIA[4];
                conLai -= muc5;
            }

            if (conLai > 0)
            {
                // Bậc 6: Từ 401 kWh trở lên
                tongTien += conLai * AppConstants.DIEN_BAC_THANG_DON_GIA[5];
            }

            return Math.Round(tongTien, 0);
        }

        /// <summary>
        /// Tính tiền nước sinh hoạt theo khối lượng tiêu thụ thực tế
        /// </summary>
        public decimal TinhTienNuoc(int soNuocTieuThu)
        {
            if (soNuocTieuThu <= 0) return 0;
            return soNuocTieuThu * AppConstants.DON_GIA_NUOC;
        }
    }
}
