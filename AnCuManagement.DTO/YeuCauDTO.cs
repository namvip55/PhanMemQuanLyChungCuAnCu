using System;

namespace AnCuManagement.DTO
{
    public class YeuCauDTO
    {
        public int MaYeuCau { get; set; }
        public int MaPhong { get; set; }
        public int MaKhachThue { get; set; }
        public string LoaiYeuCau { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayGui { get; set; }
        public string TrangThai { get; set; }
        public string KetQuaXuLy { get; set; }
        public int? DanhGiaSao { get; set; }
        public DateTime? NgayXuLy { get; set; }

        public YeuCauDTO() { }
    }
}
