using System;
using System.Data;
using Microsoft.Data.SqlClient;
using AnCuManagement.DTO;

namespace AnCuManagement.DAL
{
    /// <summary>
    /// Tầng truy xuất dữ liệu Thiết bị & Tiện nghi phòng (UC2)
    /// Do Phan Công Thành Đạt (MSSV: 24810310223) phụ trách phát triển.
    /// </summary>
    public class ThietBiPhongDAL
    {
        public DataTable GetThietBiTheoPhong(int maPhong)
        {
            var p = new[] { new SqlParameter("@MaPhong", maPhong) };
            return DbHelper.ExecuteQuery("sp_GetThietBiTheoPhong", p);
        }

        public int ThemThietBi(ThietBiPhongDTO tb)
        {
            var p = new[]
            {
                new SqlParameter("@MaPhong", tb.MaPhong),
                new SqlParameter("@TenThietBi", tb.TenThietBi),
                new SqlParameter("@SoLuong", tb.SoLuong),
                new SqlParameter("@TinhTrang", tb.TinhTrang),
                new SqlParameter("@NgayLapDat", tb.NgayLapDat),
                new SqlParameter("@GhiChu", (object?)tb.GhiChu ?? DBNull.Value)
            };
            object? res = DbHelper.ExecuteScalar("sp_ThemThietBi", p);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }

        public int CapNhatThietBi(ThietBiPhongDTO tb)
        {
            var p = new[]
            {
                new SqlParameter("@MaThietBi", tb.MaThietBi),
                new SqlParameter("@TenThietBi", tb.TenThietBi),
                new SqlParameter("@SoLuong", tb.SoLuong),
                new SqlParameter("@TinhTrang", tb.TinhTrang),
                new SqlParameter("@NgayLapDat", tb.NgayLapDat),
                new SqlParameter("@GhiChu", (object?)tb.GhiChu ?? DBNull.Value)
            };
            return DbHelper.ExecuteNonQuery("sp_CapNhatThietBi", p);
        }

        public int XoaThietBi(int maThietBi)
        {
            var p = new[] { new SqlParameter("@MaThietBi", maThietBi) };
            return DbHelper.ExecuteNonQuery("sp_XoaThietBi", p);
        }
    }
}
