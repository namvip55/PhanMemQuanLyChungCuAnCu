using System;

namespace AnCuManagement.DTO
{
    public class TaiKhoanDTO
    {
        public int MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhauHash { get; set; } = string.Empty;
        public string SaltHash { get; set; } = string.Empty;
        public string? HoTen { get; set; }
        public string QuyenHan { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; } = true;
        public int? MaKhachThue { get; set; }
        public int SoLanDangNhapSai { get; set; } = 0;

        public TaiKhoanDTO() { }
    }
}
