using System;
using System.Data;
using Microsoft.Data.SqlClient;
using AnCuManagement.DTO;

namespace AnCuManagement.DAL
{
    /// <summary>
    /// Tầng truy xuất dữ liệu Chỉ số điện nước (UC5)
    /// Do Lê Ngọc Quyền (MSSV: 24810310259) phụ trách phát triển.
    /// </summary>
    public class ChiSoDienNuocDAL
    {
        public DataTable GetChiSoDienNuoc(int? maPhong, int? thang, int? nam)
        {
            var p = new[]
            {
                new SqlParameter("@MaPhong", (object?)maPhong ?? DBNull.Value),
                new SqlParameter("@Thang", (object?)thang ?? DBNull.Value),
                new SqlParameter("@Nam", (object?)nam ?? DBNull.Value)
            };
            return DbHelper.ExecuteQuery("sp_GetChiSoDienNuoc", p);
        }

        public int ThemChiSoDienNuoc(ChiSoDienNuocDTO cs)
        {
            var p = new[]
            {
                new SqlParameter("@MaPhong", cs.MaPhong),
                new SqlParameter("@Thang", cs.Thang),
                new SqlParameter("@Nam", cs.Nam),
                new SqlParameter("@ChiSoDienCu", cs.ChiSoDienCu),
                new SqlParameter("@ChiSoDienMoi", cs.ChiSoDienMoi),
                new SqlParameter("@ChiSoNuocCu", cs.ChiSoNuocCu),
                new SqlParameter("@ChiSoNuocMoi", cs.ChiSoNuocMoi),
                new SqlParameter("@NguoiGhi", (object?)cs.NguoiGhi ?? DBNull.Value)
            };
            object? res = DbHelper.ExecuteScalar("sp_ThemChiSoDienNuoc", p);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }

        public int CapNhatChiSoDienNuoc(ChiSoDienNuocDTO cs)
        {
            var p = new[]
            {
                new SqlParameter("@MaChiSo", cs.MaChiSo),
                new SqlParameter("@ChiSoDienCu", cs.ChiSoDienCu),
                new SqlParameter("@ChiSoDienMoi", cs.ChiSoDienMoi),
                new SqlParameter("@ChiSoNuocCu", cs.ChiSoNuocCu),
                new SqlParameter("@ChiSoNuocMoi", cs.ChiSoNuocMoi),
                new SqlParameter("@NguoiGhi", (object?)cs.NguoiGhi ?? DBNull.Value)
            };
            return DbHelper.ExecuteNonQuery("sp_CapNhatChiSoDienNuoc", p);
        }

        public DataTable GetChiSoTheoPhongThang(int maPhong, int thang, int nam)
        {
            var p = new[]
            {
                new SqlParameter("@MaPhong", maPhong),
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam)
            };
            return DbHelper.ExecuteQuery("sp_GetChiSoTheoPhongThang", p);
        }
    }
}
