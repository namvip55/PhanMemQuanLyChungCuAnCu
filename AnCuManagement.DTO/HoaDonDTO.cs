using System;

namespace AnCuManagement.DTO
{
    public class HoaDonDTO
    {
        public int MaHoaDon { get; set; }
        public int MaPhong { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public decimal TienPhong { get; set; }
        public decimal TienDien { get; set; }
        public decimal TienNuoc { get; set; }
        public decimal TienDichVu { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayLap { get; set; }
        public string GhiChu { get; set; }

        public HoaDonDTO() { }
    }
}
