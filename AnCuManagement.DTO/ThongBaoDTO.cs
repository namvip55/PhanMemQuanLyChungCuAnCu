using System;

namespace AnCuManagement.DTO
{
    public class ThongBaoDTO
    {
        public int MaThongBao { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayDang { get; set; }
        public string NguoiDang { get; set; }
        public string LoaiThongBao { get; set; }
        public int? MaPhong { get; set; }
        public bool IsActive { get; set; }

        public ThongBaoDTO() { }
    }
}
