using System;
using System.Data;
using Microsoft.Data.SqlClient;
using AnCuManagement.DTO;

namespace AnCuManagement.DAL
{
    /// <summary>
    /// Tầng truy xuất dữ liệu Khách thuê (UC3)
    /// Do Phan Công Thành Đạt (MSSV: 24810310223) phụ trách phát triển.
    /// </summary>
    public class KhachThueDAL
    {
        public DataTable GetAllKhachThue()
        {
            return DbHelper.ExecuteQuery("sp_GetAllKhachThue");
        }

        public int ThemKhachThue(KhachThueDTO kt)
        {
            var p = new[]
            {
                new SqlParameter("@HoTen", kt.HoTen),
                new SqlParameter("@CCCD", kt.CCCD),
                new SqlParameter("@SoDienThoai", (object?)kt.SoDienThoai ?? DBNull.Value),
                new SqlParameter("@Email", (object?)kt.Email ?? DBNull.Value),
                new SqlParameter("@NgaySinh", kt.NgaySinh),
                new SqlParameter("@GioiTinh", (object?)kt.GioiTinh ?? DBNull.Value),
                new SqlParameter("@QueQuan", (object?)kt.QueQuan ?? DBNull.Value),
                new SqlParameter("@DiaChiThuongTru", (object?)kt.DiaChiThuongTru ?? DBNull.Value),
                new SqlParameter("@MaPhong", (object?)kt.MaPhong ?? DBNull.Value),
                new SqlParameter("@LaChuHo", kt.LaChuHo)
            };
            object? res = DbHelper.ExecuteScalar("sp_ThemKhachThue", p);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }

        public int CapNhatKhachThue(KhachThueDTO kt)
        {
            var p = new[]
            {
                new SqlParameter("@MaKhachThue", kt.MaKhachThue),
                new SqlParameter("@HoTen", kt.HoTen),
                new SqlParameter("@CCCD", kt.CCCD),
                new SqlParameter("@SoDienThoai", (object?)kt.SoDienThoai ?? DBNull.Value),
                new SqlParameter("@Email", (object?)kt.Email ?? DBNull.Value),
                new SqlParameter("@NgaySinh", kt.NgaySinh),
                new SqlParameter("@GioiTinh", (object?)kt.GioiTinh ?? DBNull.Value),
                new SqlParameter("@QueQuan", (object?)kt.QueQuan ?? DBNull.Value),
                new SqlParameter("@DiaChiThuongTru", (object?)kt.DiaChiThuongTru ?? DBNull.Value),
                new SqlParameter("@MaPhong", (object?)kt.MaPhong ?? DBNull.Value),
                new SqlParameter("@LaChuHo", kt.LaChuHo)
            };
            return DbHelper.ExecuteNonQuery("sp_CapNhatKhachThue", p);
        }

        public int XoaMemKhachThue(int maKhachThue)
        {
            var p = new[] { new SqlParameter("@MaKhachThue", maKhachThue) };
            return DbHelper.ExecuteNonQuery("sp_XoaMemKhachThue", p);
        }

        public DataTable TimKiemKhachThue(string? keyword)
        {
            var p = new[] { new SqlParameter("@Keyword", (object?)keyword ?? DBNull.Value) };
            return DbHelper.ExecuteQuery("sp_TimKiemKhachThue", p);
        }

        public bool KiemTraTrungCCCD(string cccd, int? excludeMaKhachThue = null)
        {
            var p = new[]
            {
                new SqlParameter("@CCCD", cccd),
                new SqlParameter("@ExcludeMaKhachThue", (object?)excludeMaKhachThue ?? DBNull.Value)
            };
            object? res = DbHelper.ExecuteScalar("sp_KiemTraTrungCCCD", p);
            return res != null && Convert.ToInt32(res) > 0;
        }
    }
}
