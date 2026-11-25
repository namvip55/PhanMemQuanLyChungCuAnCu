using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using AnCuManagement.DTO;

namespace AnCuManagement.DAL
{
    /// <summary>
    /// Tầng truy xuất dữ liệu Hóa đơn & Thanh toán (UC6)
    /// Do Nguyễn Văn Tiến (MSSV: 24810310274) phụ trách phát triển.
    /// </summary>
    public class HoaDonThanhToanDAL
    {
        public DataTable GetAllHoaDon(int? thang, int? nam, string? trangThai)
        {
            var p = new[]
            {
                new SqlParameter("@Thang", (object?)thang ?? DBNull.Value),
                new SqlParameter("@Nam", (object?)nam ?? DBNull.Value),
                new SqlParameter("@TrangThai", (object?)trangThai ?? DBNull.Value)
            };
            return DbHelper.ExecuteQuery("sp_GetAllHoaDon", p);
        }

        public int LapHoaDon(HoaDonDTO hd)
        {
            var p = new[]
            {
                new SqlParameter("@MaPhong", hd.MaPhong),
                new SqlParameter("@Thang", hd.Thang),
                new SqlParameter("@Nam", hd.Nam),
                new SqlParameter("@TienPhong", hd.TienPhong),
                new SqlParameter("@TienDien", hd.TienDien),
                new SqlParameter("@TienNuoc", hd.TienNuoc),
                new SqlParameter("@TienDichVu", hd.TienDichVu),
                new SqlParameter("@TongTien", hd.TongTien),
                new SqlParameter("@GhiChu", (object?)hd.GhiChu ?? DBNull.Value)
            };
            object? res = DbHelper.ExecuteScalar("sp_LapHoaDon", p);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }

        /// <summary>
        /// Lập hóa đơn kèm toàn bộ các dòng chi tiết hóa đơn trong một SqlTransaction duy nhất (atomic)
        /// </summary>
        public int LapHoaDonKemChiTiet(HoaDonDTO hd, List<ChiTietHoaDonDTO> chiTiets)
        {
            return DbHelper.ExecuteTransaction((conn, trans) =>
            {
                using var cmdHd = new SqlCommand("sp_LapHoaDon", conn, trans);
                cmdHd.CommandType = CommandType.StoredProcedure;
                cmdHd.Parameters.Add(new SqlParameter("@MaPhong", hd.MaPhong));
                cmdHd.Parameters.Add(new SqlParameter("@Thang", hd.Thang));
                cmdHd.Parameters.Add(new SqlParameter("@Nam", hd.Nam));
                cmdHd.Parameters.Add(new SqlParameter("@TienPhong", hd.TienPhong));
                cmdHd.Parameters.Add(new SqlParameter("@TienDien", hd.TienDien));
                cmdHd.Parameters.Add(new SqlParameter("@TienNuoc", hd.TienNuoc));
                cmdHd.Parameters.Add(new SqlParameter("@TienDichVu", hd.TienDichVu));
                cmdHd.Parameters.Add(new SqlParameter("@TongTien", hd.TongTien));
                cmdHd.Parameters.Add(new SqlParameter("@GhiChu", (object?)hd.GhiChu ?? DBNull.Value));

                object? resHd = cmdHd.ExecuteScalar();
                int maHoaDonMoi = resHd != null && resHd != DBNull.Value ? Convert.ToInt32(resHd) : 0;
                if (maHoaDonMoi <= 0)
                {
                    throw new Exception("Không thể khởi tạo mã hóa đơn mới.");
                }

                foreach (var ct in chiTiets)
                {
                    using var cmdCt = new SqlCommand("sp_ThemChiTietHoaDon", conn, trans);
                    cmdCt.CommandType = CommandType.StoredProcedure;
                    cmdCt.Parameters.Add(new SqlParameter("@MaHoaDon", maHoaDonMoi));
                    cmdCt.Parameters.Add(new SqlParameter("@TenKhoanThu", ct.TenKhoanThu));
                    cmdCt.Parameters.Add(new SqlParameter("@SoLuong", ct.SoLuong));
                    cmdCt.Parameters.Add(new SqlParameter("@DonGia", ct.DonGia));
                    cmdCt.Parameters.Add(new SqlParameter("@ThanhTien", ct.ThanhTien));
                    cmdCt.Parameters.Add(new SqlParameter("@GhiChu", (object?)ct.GhiChu ?? DBNull.Value));
                    cmdCt.ExecuteNonQuery();
                }

                return maHoaDonMoi;
            });
        }

        public int CapNhatTrangThaiHoaDon(int maHoaDon, string trangThai)
        {
            var p = new[]
            {
                new SqlParameter("@MaHoaDon", maHoaDon),
                new SqlParameter("@TrangThai", trangThai)
            };
            return DbHelper.ExecuteNonQuery("sp_CapNhatTrangThaiHoaDon", p);
        }

        public DataTable GetChiTietHoaDon(int maHoaDon)
        {
            var p = new[] { new SqlParameter("@MaHoaDon", maHoaDon) };
            return DbHelper.ExecuteQuery("sp_GetChiTietHoaDon", p);
        }

        public int ThemChiTietHoaDon(ChiTietHoaDonDTO ct)
        {
            var p = new[]
            {
                new SqlParameter("@MaHoaDon", ct.MaHoaDon),
                new SqlParameter("@TenKhoanThu", ct.TenKhoanThu),
                new SqlParameter("@SoLuong", ct.SoLuong),
                new SqlParameter("@DonGia", ct.DonGia),
                new SqlParameter("@ThanhTien", ct.ThanhTien),
                new SqlParameter("@GhiChu", (object?)ct.GhiChu ?? DBNull.Value)
            };
            object? res = DbHelper.ExecuteScalar("sp_ThemChiTietHoaDon", p);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }

        public int ThemThanhToan(ThanhToanDTO tt)
        {
            var p = new[]
            {
                new SqlParameter("@MaHoaDon", tt.MaHoaDon),
                new SqlParameter("@SoTienTra", tt.SoTienTra),
                new SqlParameter("@HinhThucThanhToan", tt.HinhThucThanhToan),
                new SqlParameter("@NguoiThu", (object?)tt.NguoiThu ?? DBNull.Value),
                new SqlParameter("@GhiChu", (object?)tt.GhiChu ?? DBNull.Value)
            };
            object? res = DbHelper.ExecuteScalar("sp_ThemThanhToan", p);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }

        public DataTable GetThanhToanTheoHoaDon(int maHoaDon)
        {
            var p = new[] { new SqlParameter("@MaHoaDon", maHoaDon) };
            return DbHelper.ExecuteQuery("sp_GetThanhToanTheoHoaDon", p);
        }

        public DataTable GetHoaDonTheoPhong(int maPhong)
        {
            var p = new[] { new SqlParameter("@MaPhong", maPhong) };
            return DbHelper.ExecuteQuery("sp_GetHoaDonTheoPhong", p);
        }
    }
}
