using System;
using System.Data;
using Microsoft.Data.SqlClient;
using AnCuManagement.DTO;

namespace AnCuManagement.DAL
{
    /// <summary>
    /// Tầng truy xuất dữ liệu Yêu cầu sự cố & Thông báo (UC7)
    /// Do Nguyễn Văn Tiến (MSSV: 24810310274) phụ trách phát triển.
    /// </summary>
    public class YeuCauThongBaoDAL
    {
        public DataTable GetAllYeuCau(string? trangThai)
        {
            var p = new[] { new SqlParameter("@TrangThai", (object?)trangThai ?? DBNull.Value) };
            return DbHelper.ExecuteQuery("sp_GetAllYeuCau", p);
        }

        public int ThemYeuCau(YeuCauDTO yc)
        {
            var p = new[]
            {
                new SqlParameter("@MaPhong", yc.MaPhong),
                new SqlParameter("@MaKhachThue", yc.MaKhachThue),
                new SqlParameter("@LoaiYeuCau", yc.LoaiYeuCau),
                new SqlParameter("@TieuDe", yc.TieuDe),
                new SqlParameter("@NoiDung", yc.NoiDung)
            };
            object? res = DbHelper.ExecuteScalar("sp_ThemYeuCau", p);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }

        public int CapNhatTrangThaiYeuCau(int maYeuCau, string trangThai, string? ketQua)
        {
            var p = new[]
            {
                new SqlParameter("@MaYeuCau", maYeuCau),
                new SqlParameter("@TrangThai", trangThai),
                new SqlParameter("@KetQuaXuLy", (object?)ketQua ?? DBNull.Value)
            };
            return DbHelper.ExecuteNonQuery("sp_CapNhatTrangThaiYeuCau", p);
        }

        public int DanhGiaYeuCau(int maYeuCau, int soSao)
        {
            var p = new[]
            {
                new SqlParameter("@MaYeuCau", maYeuCau),
                new SqlParameter("@DanhGiaSao", soSao)
            };
            return DbHelper.ExecuteNonQuery("sp_DanhGiaYeuCau", p);
        }

        public DataTable GetYeuCauTheoPhong(int maPhong)
        {
            var p = new[] { new SqlParameter("@MaPhong", maPhong) };
            return DbHelper.ExecuteQuery("sp_GetYeuCauTheoPhong", p);
        }

        public DataTable GetAllThongBao()
        {
            return DbHelper.ExecuteQuery("sp_GetAllThongBao");
        }

        public int ThemThongBao(ThongBaoDTO tb)
        {
            var p = new[]
            {
                new SqlParameter("@TieuDe", tb.TieuDe),
                new SqlParameter("@NoiDung", tb.NoiDung),
                new SqlParameter("@NguoiDang", (object?)tb.NguoiDang ?? DBNull.Value),
                new SqlParameter("@LoaiThongBao", (object?)tb.LoaiThongBao ?? "ToanToaNha"),
                new SqlParameter("@MaPhong", (object?)tb.MaPhong ?? DBNull.Value)
            };
            object? res = DbHelper.ExecuteScalar("sp_ThemThongBao", p);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }

        public DataTable GetThongBaoTheoPhong(int? maPhong)
        {
            var p = new[] { new SqlParameter("@MaPhong", (object?)maPhong ?? DBNull.Value) };
            return DbHelper.ExecuteQuery("sp_GetThongBaoTheoPhong", p);
        }
    }
}
