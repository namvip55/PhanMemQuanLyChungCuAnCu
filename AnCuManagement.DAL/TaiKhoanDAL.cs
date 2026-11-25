using System;
using System.Data;
using Microsoft.Data.SqlClient;
using AnCuManagement.DTO;

namespace AnCuManagement.DAL
{
    /// <summary>
    /// Tầng truy xuất dữ liệu Tài khoản & Phân quyền (UC1)
    /// Do Nguyễn Thành Nam (MSSV: 24810310230) phụ trách phát triển.
    /// </summary>
    public class TaiKhoanDAL
    {
        public DataTable DangNhap(string tenDangNhap)
        {
            var p = new[] { new SqlParameter("@TenDangNhap", tenDangNhap) };
            return DbHelper.ExecuteQuery("sp_DangNhap", p);
        }

        public int DoiMatKhau(int maTaiKhoan, string matKhauHashMoi, string saltHashMoi)
        {
            var p = new[]
            {
                new SqlParameter("@MaTaiKhoan", maTaiKhoan),
                new SqlParameter("@MatKhauHashMoi", matKhauHashMoi),
                new SqlParameter("@SaltHashMoi", saltHashMoi)
            };
            return DbHelper.ExecuteNonQuery("sp_DoiMatKhau", p);
        }

        public int TangSoLanDangNhapSai(int maTaiKhoan)
        {
            var p = new[] { new SqlParameter("@MaTaiKhoan", maTaiKhoan) };
            return DbHelper.ExecuteNonQuery("sp_TangSoLanDangNhapSai", p);
        }

        public int ResetSoLanDangNhapSai(int maTaiKhoan)
        {
            var p = new[] { new SqlParameter("@MaTaiKhoan", maTaiKhoan) };
            return DbHelper.ExecuteNonQuery("sp_ResetSoLanDangNhapSai", p);
        }

        public DataTable GetAllTaiKhoan()
        {
            return DbHelper.ExecuteQuery("sp_GetAllTaiKhoan");
        }

        public int ThemTaiKhoan(TaiKhoanDTO tk)
        {
            var p = new[]
            {
                new SqlParameter("@TenDangNhap", tk.TenDangNhap),
                new SqlParameter("@MatKhauHash", tk.MatKhauHash),
                new SqlParameter("@SaltHash", tk.SaltHash),
                new SqlParameter("@HoTen", (object?)tk.HoTen ?? DBNull.Value),
                new SqlParameter("@QuyenHan", tk.QuyenHan),
                new SqlParameter("@SoDienThoai", (object?)tk.SoDienThoai ?? DBNull.Value),
                new SqlParameter("@Email", (object?)tk.Email ?? DBNull.Value),
                new SqlParameter("@MaKhachThue", (object?)tk.MaKhachThue ?? DBNull.Value)
            };
            object? res = DbHelper.ExecuteScalar("sp_ThemTaiKhoan", p);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }
    }
}
