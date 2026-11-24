using System;

namespace AnCuManagement.DTO
{
    public class HopDongDTO
    {
        public int MaHopDong { get; set; }
        public int MaPhong { get; set; }
        public int MaKhachThue { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public decimal TienCoc { get; set; }
        public decimal GiaThueThucTe { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayLap { get; set; }

        public HopDongDTO() { }
    }
}
