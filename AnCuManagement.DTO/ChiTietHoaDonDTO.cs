using System;

namespace AnCuManagement.DTO
{
    public class ChiTietHoaDonDTO
    {
        public int MaChiTietHD { get; set; }
        public int MaHoaDon { get; set; }
        public string TenKhoanThu { get; set; }
        public decimal SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string GhiChu { get; set; }

        public ChiTietHoaDonDTO() { }
    }
}
