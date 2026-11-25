using System;

namespace AnCuManagement.DTO
{
    public class ThietBiPhongDTO
    {
        public int MaThietBi { get; set; }
        public int MaPhong { get; set; }
        public string TenThietBi { get; set; }
        public int SoLuong { get; set; }
        public string TinhTrang { get; set; }
        public DateTime NgayLapDat { get; set; }
        public string GhiChu { get; set; }

        public ThietBiPhongDTO() { }
    }
}
