using System;

namespace AnCuManagement.DTO
{
    public class KhachThueDTO
    {
        public int MaKhachThue { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string QueQuan { get; set; }
        public string DiaChiThuongTru { get; set; }
        public int? MaPhong { get; set; }
        public bool LaChuHo { get; set; }
        public bool IsActive { get; set; }
        public DateTime NgayDangKy { get; set; }

        public KhachThueDTO() { }
    }
}
