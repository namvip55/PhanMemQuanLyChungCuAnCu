using System;

namespace AnCuManagement.DTO
{
    public class PhongDTO
    {
        public int MaPhong { get; set; }
        public string SoPhong { get; set; }
        public int Tang { get; set; }
        public decimal DienTich { get; set; }
        public decimal GiaThue { get; set; }
        public string TrangThai { get; set; }
        public string LoaiPhong { get; set; }
        public string MoTa { get; set; }
        public int MaToaNha { get; set; }

        public PhongDTO() { }
    }
}
