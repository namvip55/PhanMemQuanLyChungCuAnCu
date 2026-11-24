using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AnCuManagement.DAL
{
    /// <summary>
    /// Tầng truy xuất dữ liệu Báo cáo & Thống kê (UC8)
    /// Do Nguyễn Văn Tiến (MSSV: 24810310274) phụ trách phát triển.
    /// </summary>
    public class BaoCaoThongKeDAL
    {
        public DataTable BaoCaoDoanhThu(int thang, int nam)
        {
            var p = new[]
            {
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam)
            };
            return DbHelper.ExecuteQuery("sp_BaoCaoDoanhThu", p);
        }

        public DataTable BaoCaoTinhTrangPhong()
        {
            return DbHelper.ExecuteQuery("sp_BaoCaoTinhTrangPhong");
        }

        public DataTable BaoCaoCongNo(int? thang, int? nam)
        {
            var p = new[]
            {
                new SqlParameter("@Thang", (object?)thang ?? DBNull.Value),
                new SqlParameter("@Nam", (object?)nam ?? DBNull.Value)
            };
            return DbHelper.ExecuteQuery("sp_BaoCaoCongNo", p);
        }

        public DataTable BaoCaoKhachThue()
        {
            return DbHelper.ExecuteQuery("sp_BaoCaoKhachThue");
        }

        public DataTable ThongKeDoanhThuTheoThang(int nam)
        {
            var p = new[] { new SqlParameter("@Nam", nam) };
            return DbHelper.ExecuteQuery("sp_ThongKeDoanhThuTheoThang", p);
        }
    }
}
