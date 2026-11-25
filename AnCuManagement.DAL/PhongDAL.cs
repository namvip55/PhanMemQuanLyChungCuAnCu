using System;
using System.Data;
using Microsoft.Data.SqlClient;
using AnCuManagement.DTO;

namespace AnCuManagement.DAL
{
    /// <summary>
    /// Tầng truy xuất dữ liệu Phòng (UC2)
    /// Do Phan Công Thành Đạt (MSSV: 24810310223) phụ trách phát triển.
    /// </summary>
    public class PhongDAL
    {
        public DataTable GetAllPhong()
        {
            return DbHelper.ExecuteQuery("sp_GetAllPhong");
        }

        public DataTable GetAllToaNha()
        {
            try
            {
                return DbHelper.ExecuteQuery("sp_GetAllToaNha");
            }
            catch
            {
                return DbHelper.ExecuteQueryText("SELECT MaToaNha, TenToaNha FROM ToaNha ORDER BY MaToaNha");
            }
        }

        public int ThemPhong(PhongDTO phong)
        {
            var p = new[]
            {
                new SqlParameter("@SoPhong", phong.SoPhong),
                new SqlParameter("@Tang", phong.Tang),
                new SqlParameter("@DienTich", phong.DienTich),
                new SqlParameter("@GiaThue", phong.GiaThue),
                new SqlParameter("@TrangThai", phong.TrangThai),
                new SqlParameter("@LoaiPhong", (object?)phong.LoaiPhong ?? DBNull.Value),
                new SqlParameter("@MoTa", (object?)phong.MoTa ?? DBNull.Value),
                new SqlParameter("@MaToaNha", phong.MaToaNha)
            };
            object? res = DbHelper.ExecuteScalar("sp_ThemPhong", p);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }

        public int CapNhatPhong(PhongDTO phong)
        {
            var p = new[]
            {
                new SqlParameter("@MaPhong", phong.MaPhong),
                new SqlParameter("@SoPhong", phong.SoPhong),
                new SqlParameter("@Tang", phong.Tang),
                new SqlParameter("@DienTich", phong.DienTich),
                new SqlParameter("@GiaThue", phong.GiaThue),
                new SqlParameter("@TrangThai", phong.TrangThai),
                new SqlParameter("@LoaiPhong", (object?)phong.LoaiPhong ?? DBNull.Value),
                new SqlParameter("@MoTa", (object?)phong.MoTa ?? DBNull.Value),
                new SqlParameter("@MaToaNha", phong.MaToaNha)
            };
            return DbHelper.ExecuteNonQuery("sp_CapNhatPhong", p);
        }

        public int CapNhatTrangThaiPhong(int maPhong, string trangThai)
        {
            var p = new[]
            {
                new SqlParameter("@MaPhong", maPhong),
                new SqlParameter("@TrangThai", trangThai)
            };
            return DbHelper.ExecuteNonQuery("sp_CapNhatTrangThaiPhong", p);
        }

        public DataTable TimKiemPhong(int? tang, string? trangThai, decimal? giaMin, decimal? giaMax)
        {
            var p = new[]
            {
                new SqlParameter("@Tang", (object?)tang ?? DBNull.Value),
                new SqlParameter("@TrangThai", (object?)trangThai ?? DBNull.Value),
                new SqlParameter("@GiaMin", (object?)giaMin ?? DBNull.Value),
                new SqlParameter("@GiaMax", (object?)giaMax ?? DBNull.Value)
            };
            return DbHelper.ExecuteQuery("sp_TimKiemPhong", p);
        }

        public DataTable GetPhongTheoMa(int maPhong)
        {
            var p = new[] { new SqlParameter("@MaPhong", maPhong) };
            return DbHelper.ExecuteQuery("sp_GetPhongTheoMa", p);
        }
    }
}
