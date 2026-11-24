using System;

namespace AnCuManagement.DTO
{
    public class ThanhToanDTO
    {
        public int MaThanhToan { get; set; }
        public int MaHoaDon { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public decimal SoTienTra { get; set; }
        public string HinhThucThanhToan { get; set; }
        public string NguoiThu { get; set; }
        public string GhiChu { get; set; }

        public ThanhToanDTO() { }
    }
}
