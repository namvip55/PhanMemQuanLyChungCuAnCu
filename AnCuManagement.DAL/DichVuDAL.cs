using System;
using System.Data;
using Microsoft.Data.SqlClient;
using AnCuManagement.DTO;

namespace AnCuManagement.DAL
{
    /// <summary>
    /// Tầng truy xuất dữ liệu Danh mục Dịch vụ (UC5)
    /// Do Lê Ngọc Quyền (MSSV: 24810310259) phụ trách phát triển.
    /// </summary>
    public class DichVuDAL
    {
        public DataTable GetAllDichVu()
        {
            return DbHelper.ExecuteQuery("sp_GetAllDichVu");
        }

        public int ThemDichVu(DichVuDTO dv)
        {
            var p = new[]
            {
                new SqlParameter("@TenDichVu", dv.TenDichVu),
                new SqlParameter("@DonGia", dv.DonGia),
                new SqlParameter("@DonViTinh", (object?)dv.DonViTinh ?? DBNull.Value),
                new SqlParameter("@MoTa", (object?)dv.MoTa ?? DBNull.Value)
            };
            object? res = DbHelper.ExecuteScalar("sp_ThemDichVu", p);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }

        public int CapNhatDichVu(DichVuDTO dv)
        {
            var p = new[]
            {
                new SqlParameter("@MaDichVu", dv.MaDichVu),
                new SqlParameter("@TenDichVu", dv.TenDichVu),
                new SqlParameter("@DonGia", dv.DonGia),
                new SqlParameter("@DonViTinh", (object?)dv.DonViTinh ?? DBNull.Value),
                new SqlParameter("@MoTa", (object?)dv.MoTa ?? DBNull.Value),
                new SqlParameter("@IsActive", dv.IsActive)
            };
            return DbHelper.ExecuteNonQuery("sp_CapNhatDichVu", p);
        }
    }
}
