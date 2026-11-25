using System;
using System.Data;
using Microsoft.Data.SqlClient;
using AnCuManagement.DTO;

namespace AnCuManagement.DAL
{
    /// <summary>
    /// Tầng truy xuất dữ liệu Hợp đồng thuê phòng (UC4)
    /// Do Lê Ngọc Quyền (MSSV: 24810310259) phụ trách phát triển.
    /// </summary>
    public class HopDongDAL
    {
        public DataTable GetAllHopDong()
        {
            return DbHelper.ExecuteQuery("sp_GetAllHopDong");
        }

        public int ThemHopDong(HopDongDTO hd)
        {
            var p = new[]
            {
                new SqlParameter("@MaPhong", hd.MaPhong),
                new SqlParameter("@MaKhachThue", hd.MaKhachThue),
                new SqlParameter("@NgayBatDau", hd.NgayBatDau),
                new SqlParameter("@NgayKetThuc", hd.NgayKetThuc),
                new SqlParameter("@TienCoc", hd.TienCoc),
                new SqlParameter("@GiaThueThucTe", hd.GiaThueThucTe),
                new SqlParameter("@GhiChu", (object?)hd.GhiChu ?? DBNull.Value)
            };
            object? res = DbHelper.ExecuteScalar("sp_ThemHopDong", p);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }

        public int GiaHanHopDong(int maHopDong, DateTime ngayKetThucMoi, decimal? giaMoi)
        {
            var p = new[]
            {
                new SqlParameter("@MaHopDong", maHopDong),
                new SqlParameter("@NgayKetThucMoi", ngayKetThucMoi),
                new SqlParameter("@GiaMoi", (object?)giaMoi ?? DBNull.Value)
            };
            return DbHelper.ExecuteNonQuery("sp_GiaHanHopDong", p);
        }

        public int ChamDutHopDong(int maHopDong)
        {
            var p = new[] { new SqlParameter("@MaHopDong", maHopDong) };
            return DbHelper.ExecuteNonQuery("sp_ChamDutHopDong", p);
        }

        public DataTable GetHopDongTheoPhong(int maPhong)
        {
            var p = new[] { new SqlParameter("@MaPhong", maPhong) };
            return DbHelper.ExecuteQuery("sp_GetHopDongTheoPhong", p);
        }

        public DataTable GetHopDongTheoKhach(int maKhachThue)
        {
            var p = new[] { new SqlParameter("@MaKhachThue", maKhachThue) };
            return DbHelper.ExecuteQuery("sp_GetHopDongTheoKhach", p);
        }
    }
}
