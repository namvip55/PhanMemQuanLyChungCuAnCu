using System;

namespace AnCuManagement.DTO
{
    public class DichVuDTO
    {
        public int MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public decimal DonGia { get; set; }
        public string DonViTinh { get; set; }
        public string MoTa { get; set; }
        public bool IsActive { get; set; }

        public DichVuDTO() { }
    }
}
