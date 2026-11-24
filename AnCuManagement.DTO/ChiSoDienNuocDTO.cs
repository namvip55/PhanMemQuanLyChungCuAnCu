using System;

namespace AnCuManagement.DTO
{
    public class ChiSoDienNuocDTO
    {
        public int MaChiSo { get; set; }
        public int MaPhong { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public int ChiSoDienCu { get; set; }
        public int ChiSoDienMoi { get; set; }
        public int ChiSoNuocCu { get; set; }
        public int ChiSoNuocMoi { get; set; }
        public DateTime NgayGhi { get; set; }
        public string NguoiGhi { get; set; }

        public ChiSoDienNuocDTO() { }
    }
}
