using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AnCuManagement.DAL
{
    /// <summary>
    /// Lop tu dong khoi tao va cap nhat CSDL sach 100% khong loi font
    /// </summary>
    public static class DbInitializer
    {
        public static void KiemTraVaKhoiTaoTrietDe()
        {
            try
            {
                using var conn = DbHelper.GetConnection();
                conn.Open();

                // Kiem tra xem CSDL hien tai co can khoi tao lai khong
                // (Neu so luong khach thue > 30 hoac con ky tu loi font moji-bake nhu Ä, Ã, á, ?)
                bool canKhoiTao = false;
                using (var cmdCheck = new SqlCommand(@"
                    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'KhachThue')
                        SELECT 1;
                    ELSE IF (SELECT COUNT(*) FROM KhachThue) <> 30
                        SELECT 1;
                    ELSE IF EXISTS (SELECT 1 FROM Phong WHERE TrangThai LIKE '%Ä%' OR TrangThai LIKE '%Trá»%')
                        SELECT 1;
                    ELSE IF EXISTS (SELECT 1 FROM KhachThue WHERE HoTen LIKE '%Ä%' OR HoTen LIKE '%Ã%' OR HoTen LIKE '%á%')
                        SELECT 1;
                    ELSE
                        SELECT 0;
                ", conn))
                {
                    object? res = cmdCheck.ExecuteScalar();
                    if (res != null && Convert.ToInt32(res) == 1)
                    {
                        canKhoiTao = true;
                    }
                }

                if (!canKhoiTao) return;

                // Thuc hien khoi tao lai toan bo du lieu sach (30 cu dan, Tieng Viet khong dau 100%)
                ChayScriptKhoiTao(conn);
            }
            catch
            {
                // Bo qua loi neu khong the ket noi CSDL luc khoi dong
            }
        }

        private static void ChayScriptKhoiTao(SqlConnection conn)
        {
            string[] sqlBatches = new string[]
            {
                // 1. Vo hieu hoa rang buoc va triggers
                @"
                ALTER TABLE Phong NOCHECK CONSTRAINT ALL;
                ALTER TABLE HopDong NOCHECK CONSTRAINT ALL;
                ALTER TABLE KhachThue NOCHECK CONSTRAINT ALL;
                ALTER TABLE ChiSoDienNuoc NOCHECK CONSTRAINT ALL;
                ALTER TABLE HoaDon NOCHECK CONSTRAINT ALL;
                ALTER TABLE ChiTietHoaDon NOCHECK CONSTRAINT ALL;
                ALTER TABLE ThanhToan NOCHECK CONSTRAINT ALL;
                ALTER TABLE ThietBiPhong NOCHECK CONSTRAINT ALL;
                ALTER TABLE YeuCau NOCHECK CONSTRAINT ALL;
                ALTER TABLE ThongBao NOCHECK CONSTRAINT ALL;
                ALTER TABLE TaiKhoan NOCHECK CONSTRAINT ALL;
                ALTER TABLE ToaNha NOCHECK CONSTRAINT ALL;
                ALTER TABLE DichVu NOCHECK CONSTRAINT ALL;

                IF EXISTS (SELECT 1 FROM sys.triggers WHERE name = 'trg_CapNhatTrangThaiPhong_SauThemHopDong')
                    DISABLE TRIGGER trg_CapNhatTrangThaiPhong_SauThemHopDong ON HopDong;
                IF EXISTS (SELECT 1 FROM sys.triggers WHERE name = 'trg_CapNhatTrangThaiPhong_SauChamDutHopDong')
                    DISABLE TRIGGER trg_CapNhatTrangThaiPhong_SauChamDutHopDong ON HopDong;
                IF EXISTS (SELECT 1 FROM sys.triggers WHERE name = 'trg_TinhTongTienHoaDon')
                    DISABLE TRIGGER trg_TinhTongTienHoaDon ON ChiTietHoaDon;
                ",

                // 2. Xoa sach du lieu cu
                @"
                DELETE FROM ThanhToan;
                DELETE FROM ChiTietHoaDon;
                DELETE FROM HoaDon;
                DELETE FROM ChiSoDienNuoc;
                DELETE FROM HopDong;
                DELETE FROM YeuCau;
                DELETE FROM ThongBao;
                DELETE FROM TaiKhoan;
                DELETE FROM ThietBiPhong;
                DELETE FROM KhachThue;
                DELETE FROM Phong;
                DELETE FROM ToaNha;
                DELETE FROM DichVu;
                ",

                // 3. Reset Identity ve 0
                @"
                DBCC CHECKIDENT ('ToaNha', RESEED, 0);
                DBCC CHECKIDENT ('DichVu', RESEED, 0);
                DBCC CHECKIDENT ('Phong', RESEED, 0);
                DBCC CHECKIDENT ('ThietBiPhong', RESEED, 0);
                DBCC CHECKIDENT ('KhachThue', RESEED, 0);
                DBCC CHECKIDENT ('HopDong', RESEED, 0);
                DBCC CHECKIDENT ('ChiSoDienNuoc', RESEED, 0);
                DBCC CHECKIDENT ('HoaDon', RESEED, 0);
                DBCC CHECKIDENT ('ChiTietHoaDon', RESEED, 0);
                DBCC CHECKIDENT ('ThanhToan', RESEED, 0);
                DBCC CHECKIDENT ('TaiKhoan', RESEED, 0);
                DBCC CHECKIDENT ('YeuCau', RESEED, 0);
                DBCC CHECKIDENT ('ThongBao', RESEED, 0);
                ",

                // 4. Sua check constraint CK_Phong_TrangThai
                @"
                IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Phong_TrangThai')
                    ALTER TABLE Phong DROP CONSTRAINT CK_Phong_TrangThai;
                ALTER TABLE Phong ADD CONSTRAINT CK_Phong_TrangThai 
                CHECK (TrangThai IN ('Trong', 'DangThue', 'SuaChua', 'DatCoc', N'Trống', N'ĐangThuê', N'SửaChữa', N'ĐặtCọc'));
                ",

                // 5. Nap Toa Nha & Dich Vu
                @"
                INSERT INTO ToaNha (TenToaNha, DiaChi, SoTang, MoTa) VALUES 
                ('Toa A - Hoa Sen', '235 Hoang Quoc Viet, Co Nhue, Bac Tu Liem, Ha Noi', 10, 'Toa can ho cao cap gom 5 tang mau voi 25 can ho tien nghi'),
                ('Toa B - Huong Duong', '235 Hoang Quoc Viet, Co Nhue, Bac Tu Liem, Ha Noi', 10, 'Toa can ho dich vu cao cap gom 5 tang mau voi 25 can ho studio va 2PN');

                INSERT INTO DichVu (TenDichVu, DonGia, DonViTinh, MoTa, IsActive) VALUES 
                ('Phi Quan ly & Ve sinh chung', 150000, 'dong/thang', 'Thu don rac, ve sinh hanh lang, bao an 24/7', 1),
                ('Phi Bao tri Thang may', 80000, 'dong/phong', 'Van hanh va bao duong ky thuat thang may dinh ky', 1),
                ('Gui xe may', 100000, 'dong/xe/thang', 'Trong giu xe may tang ham the tu an ninh', 1),
                ('Gui xe o to', 1200000, 'dong/xe/thang', 'Bai do xe o to tang ham B1 co camera giam sat', 1),
                ('Internet Cap quang toc do cao', 220000, 'dong/thang', 'Goi cap quang Viettel 150Mbps rieng tung phong', 1);
                ",

                // 6. Nap 50 Phong (Toa A & Toa B)
                @"
                INSERT INTO Phong (SoPhong, Tang, DienTich, GiaThue, TrangThai, LoaiPhong, MoTa, MaToaNha) VALUES
                ('A101', 1, 45.0, 5500000, 'DangThue', 'Phong Don', 'Can goc tang 1, ban cong thoang', 1),
                ('A102', 1, 55.0, 7000000, 'DangThue', 'Phong Doi', 'Can 2 ngu 1 khach', 1),
                ('A103', 1, 35.0, 4500000, 'Trong', 'Studio', 'Day du noi that co ban', 1),
                ('A104', 1, 45.0, 5500000, 'DangThue', 'Phong Don', 'View san vuon noi khu', 1),
                ('A105', 1, 65.0, 8500000, 'DangThue', 'Phong VIP', 'Can VIP 2 phong ngu lon, 2 WC', 1),

                ('A201', 2, 45.0, 5500000, 'DangThue', 'Phong Don', 'Tang 2 huong Nam mat me', 1),
                ('A202', 2, 55.0, 7000000, 'DangThue', 'Phong Doi', 'Can 2 ngu thoang', 1),
                ('A203', 2, 35.0, 4500000, 'DangThue', 'Studio', 'Studio nho gon hien dai', 1),
                ('A204', 2, 45.0, 5500000, 'Trong', 'Phong Don', 'Phong moi sua sach dep', 1),
                ('A205', 2, 65.0, 8500000, 'DangThue', 'Phong VIP', 'VIP day du do dien may', 1),

                ('A301', 3, 45.0, 5600000, 'DangThue', 'Phong Don', 'Tang 3 view duong lon', 1),
                ('A302', 3, 55.0, 7200000, 'DangThue', 'Phong Doi', 'Noi that go soi cao cap', 1),
                ('A303', 3, 35.0, 4600000, 'DangThue', 'Studio', 'Studio view thoang', 1),
                ('A304', 3, 45.0, 5600000, 'SuaChua', 'Phong Don', 'Dang son lai tuong va lat san', 1),
                ('A305', 3, 65.0, 8800000, 'DangThue', 'Phong VIP', 'Can goc 2 mat thoang', 1),

                ('A401', 4, 45.0, 5600000, 'Trong', 'Phong Don', 'Yen tinh, thoang mat', 1),
                ('A402', 4, 55.0, 7200000, 'Trong', 'Phong Doi', 'Ban cong rong trong cay', 1),
                ('A403', 4, 35.0, 4600000, 'Trong', 'Studio', 'Khach vua tra phong', 1),
                ('A404', 4, 45.0, 5600000, 'Trong', 'Phong Don', 'Can ho tieu chuan', 1),
                ('A405', 4, 65.0, 8800000, 'DatCoc', 'Phong VIP', 'Khach da coc giu cho don vao dau thang', 1),

                ('A501', 5, 45.0, 5800000, 'Trong', 'Phong Don', 'Tang cao khong khi trong lanh', 1),
                ('A502', 5, 55.0, 7500000, 'Trong', 'Phong Doi', '2 ngu view dep', 1),
                ('A503', 5, 35.0, 4800000, 'Trong', 'Studio', 'Phong co bep tu, hut mui', 1),
                ('A504', 5, 45.0, 5800000, 'Trong', 'Phong Don', 'San sang ban giao ngay', 1),
                ('A505', 5, 65.0, 9000000, 'Trong', 'Phong VIP', 'Penthouse mini co san vuon nho', 1);

                INSERT INTO Phong (SoPhong, Tang, DienTich, GiaThue, TrangThai, LoaiPhong, MoTa, MaToaNha) VALUES
                ('B101', 1, 40.0, 5000000, 'DangThue', 'Studio', 'Can ho dich vu tien loi', 2),
                ('B102', 1, 50.0, 6500000, 'DangThue', 'Phong Doi', '2 phong ngu thoang sang', 2),
                ('B103', 1, 40.0, 5000000, 'Trong', 'Studio', 'Phong trong san do', 2),
                ('B104', 1, 50.0, 6500000, 'DangThue', 'Phong Doi', 'Dang cho ho gia dinh thue', 2),
                ('B105', 1, 60.0, 8000000, 'DangThue', 'Phong VIP', 'Trang bi may rua bat, lo nuong', 2),

                ('B201', 2, 40.0, 5200000, 'DangThue', 'Studio', 'Noi that thong minh', 2),
                ('B202', 2, 50.0, 6600000, 'DangThue', 'Phong Doi', 'View cong vien cay xanh', 2),
                ('B203', 2, 40.0, 5200000, 'DangThue', 'Studio', 'Phong yen tinh', 2),
                ('B204', 2, 50.0, 6600000, 'SuaChua', 'Phong Doi', 'Dang thay thiet bi ve sinh', 2),
                ('B205', 2, 60.0, 8200000, 'DangThue', 'Phong VIP', 'VIP day du do noi that', 2),

                ('B301', 3, 40.0, 5200000, 'DangThue', 'Studio', 'Studio hien dai', 2),
                ('B302', 3, 50.0, 6700000, 'Trong', 'Phong Doi', 'Tang 3 huong Dong Nam', 2),
                ('B303', 3, 40.0, 5200000, 'Trong', 'Studio', 'San sang cho thue', 2),
                ('B304', 3, 50.0, 6700000, 'Trong', 'Phong Doi', 'Phong dep', 2),
                ('B305', 3, 60.0, 8200000, 'Trong', 'Phong VIP', 'Can ho cao cap view thoang', 2),

                ('B401', 4, 40.0, 5300000, 'Trong', 'Studio', 'Studio tang 4', 2),
                ('B402', 4, 50.0, 6800000, 'Trong', 'Phong Doi', 'Phong 2 ngu', 2),
                ('B403', 4, 40.0, 5300000, 'Trong', 'Studio', 'Da trang bi may giat rieng', 2),
                ('B404', 4, 50.0, 6800000, 'Trong', 'Phong Doi', 'Khach vua chuyen cong tac', 2),
                ('B405', 4, 60.0, 8500000, 'Trong', 'Phong VIP', 'Ban cong ngam phao hoa', 2),

                ('B501', 5, 40.0, 5500000, 'Trong', 'Studio', 'Tang ap mai tran cao', 2),
                ('B502', 5, 50.0, 7000000, 'Trong', 'Phong Doi', 'Can doi thiet ke Bac Au', 2),
                ('B503', 5, 40.0, 5500000, 'Trong', 'Studio', 'Phong trong sach dep', 2),
                ('B504', 5, 50.0, 7000000, 'Trong', 'Phong Doi', 'San sang vao o ngay', 2),
                ('B505', 5, 60.0, 8800000, 'Trong', 'Phong VIP', 'Can ho cao cap nhat toa B', 2);
                ",

                // 7. Thiet bi phong
                @"
                INSERT INTO ThietBiPhong (MaPhong, TenThietBi, SoLuong, TinhTrang, NgayLapDat, GhiChu) VALUES
                (1, 'Dieu hoa Panasonic 12000BTU Inverter', 1, 'Tot', '2026-01-15', 'Bao hanh den 2027'),
                (1, 'Binh nong lanh Ariston 20L', 1, 'Tot', '2026-01-15', 'Da kiem tra chong giat'),
                (1, 'Tu lanh Sharp 180L', 1, 'Tot', '2026-01-16', 'Noi that ban giao'),
                (1, 'Giuong ngu go soi 1.8x2m', 1, 'Tot', '2026-01-16', 'Kem dem cao su'),
                (2, 'Dieu hoa Daikin 9000BTU', 2, 'Tot', '2026-02-10', '2 phong ngu'),
                (2, 'May giat Electrolux 8kg', 1, 'Tot', '2026-02-10', 'Cua ngang cao cap'),
                (2, 'Bep tu doi Sunhouse', 1, 'Tot', '2026-02-11', 'Lap am ban da');
                ",

                // 8. 30 Cu dan (Khach thue)
                @"
                INSERT INTO KhachThue (HoTen, CCCD, SoDienThoai, Email, NgaySinh, GioiTinh, QueQuan, DiaChiThuongTru, MaPhong, LaChuHo, IsActive, NgayDangKy) VALUES
                ('Nguyen Van An', '001200001234', '0912345678', 'an.nguyen@gmail.com', '1995-03-12', 'Nam', 'Ha Noi', 'So 12 pho Hue, Hoan Kiem, Ha Noi', 1, 1, 1, '2024-01-01'),
                ('Tran Thi Bich', '001300002345', '0987654321', 'bich.tran@gmail.com', '1996-07-20', 'Nu', 'Hai Phong', '25 Lach Tray, Ngo Quyen, Hai Phong', 1, 0, 1, '2024-01-01'),
                ('Le Hoang Cuong', '038200003456', '0903456789', 'cuong.le@yahoo.com', '1992-11-05', 'Nam', 'Thanh Hoa', 'Phuong Lam Son, TP Thanh Hoa', 2, 1, 1, '2024-01-15'),
                ('Pham Thu Dung', '038300004567', '0934567890', 'dung.pham@outlook.com', '1994-04-18', 'Nu', 'Thanh Hoa', 'Phuong Dong Tho, TP Thanh Hoa', 2, 0, 1, '2024-01-15'),
                ('Vu Dinh Em', '024200005678', '0978901234', 'em.vu@gmail.com', '1998-09-25', 'Nam', 'Bac Ninh', 'Phuong Tien An, TP Bac Ninh', 4, 1, 1, '2024-02-01'),
                ('Do Thi Giang', '033300006789', '0967890123', 'giang.do@gmail.com', '1997-12-30', 'Nu', 'Hung Yen', 'My Hao, Hung Yen', 5, 1, 1, '2024-02-10'),
                ('Do Van Hung', '033200044567', '0933445566', 'hung.dv@gmail.com', '1996-03-15', 'Nam', 'Hung Yen', 'My Hao, Hung Yen', 5, 0, 1, '2024-02-15'),
                ('Hoang Van Hai', '022200007890', '0945678901', 'hai.hoang@gmail.com', '1990-08-14', 'Nam', 'Quang Ninh', 'Bai Chay, Ha Long, Quang Ninh', 6, 1, 1, '2024-02-15'),
                ('Hoang Van Manh', '022200045678', '0944556677', 'manh.hv@gmail.com', '1992-06-20', 'Nam', 'Quang Ninh', 'Bai Chay, Ha Long, Quang Ninh', 6, 0, 1, '2024-02-20'),
                ('Ngo Thi Yen', '035300008901', '0912389012', 'yen.ngo@gmail.com', '1993-02-28', 'Nu', 'Ha Nam', 'Phu Ly, Ha Nam', 7, 1, 1, '2024-03-01'),
                ('Bui Minh Khang', '030200009012', '0988123456', 'khang.bui@gmail.com', '1999-05-19', 'Nam', 'Hai Duong', 'TP Hai Duong', 8, 1, 1, '2024-03-05'),
                ('Dinh Thi Lan', '037300010123', '0977234567', 'lan.dinh@gmail.com', '1995-10-10', 'Nu', 'Ninh Binh', 'TP Ninh Binh', 10, 1, 1, '2024-03-10'),
                ('Luong Van Minh', '001200011234', '0966345678', 'minh.luong@gmail.com', '1991-01-17', 'Nam', 'Ha Noi', 'Dong Da, Ha Noi', 11, 1, 1, '2024-04-01'),
                ('Luong Thi Ha', '001300046789', '0955667788', 'ha.lt@gmail.com', '1994-09-12', 'Nu', 'Ha Noi', 'Dong Da, Ha Noi', 11, 0, 1, '2024-04-05'),
                ('Doan Thi Nga', '027300012345', '0944456789', 'nga.doan@gmail.com', '1996-06-22', 'Nu', 'Bac Giang', 'TP Bac Giang', 12, 1, 1, '2024-04-10'),
                ('Trinh Quoc Oai', '034200013456', '0933567890', 'oai.trinh@gmail.com', '1998-03-03', 'Nam', 'Thai Binh', 'Kien Xuong, Thai Binh', 13, 1, 1, '2024-04-15'),
                ('Tran Van Kien', '034200047890', '0966778899', 'kien.tv@gmail.com', '1999-11-28', 'Nam', 'Thai Binh', 'Kien Xuong, Thai Binh', 13, 0, 1, '2024-04-20'),
                ('Duong Hoai Phuc', '019200014567', '0922678901', 'phuc.duong@gmail.com', '1994-12-12', 'Nam', 'Phu Tho', 'Viet Tri, Phu Tho', 15, 1, 1, '2024-05-01'),
                ('Dang Tien Dat', '026200022345', '0923456789', 'dat.dang@gmail.com', '1994-05-15', 'Nam', 'Thai Nguyen', 'TP Thai Nguyen', 26, 1, 1, '2024-01-10'),
                ('Le Quynh Chi', '020300023456', '0912567890', 'chi.le@gmail.com', '1997-09-18', 'Nu', 'Lang Son', 'TP Lang Son', 27, 1, 1, '2024-01-20'),
                ('Le Minh Tuan', '020200051234', '0910111213', 'tuan.lm@gmail.com', '1998-04-30', 'Nam', 'Lang Son', 'TP Lang Son', 27, 0, 1, '2024-01-25'),
                ('Nguyen Thanh Long', '001200024567', '0901678901', 'long.nguyen@gmail.com', '1993-03-30', 'Nam', 'Ha Noi', 'Nam Tu Liem, Ha Noi', 29, 1, 1, '2024-02-01'),
                ('Nguyen Hoang Anh', '001200052345', '0921222324', 'anh.nh@gmail.com', '1995-10-15', 'Nam', 'Ha Noi', 'Nam Tu Liem, Ha Noi', 29, 0, 1, '2024-02-05'),
                ('Pham Phuong Thao', '036300025678', '0982789012', 'thao.pham@gmail.com', '1995-12-05', 'Nu', 'Nam Dinh', 'TP Nam Dinh', 30, 1, 1, '2024-02-15'),
                ('Vo Hoang Son', '045200026789', '0973890123', 'son.vo@gmail.com', '1991-08-22', 'Nam', 'Quang Tri', 'Dong Ha, Quang Tri', 31, 1, 1, '2024-03-01'),
                ('Vo Thanh Truc', '045300053456', '0932333435', 'truc.vt@gmail.com', '1993-01-08', 'Nu', 'Quang Tri', 'Dong Ha, Quang Tri', 31, 0, 1, '2024-03-05'),
                ('Do Dieu Linh', '001300027890', '0964901234', 'linh.do@gmail.com', '1999-06-16', 'Nu', 'Ha Noi', 'Tay Ho, Ha Noi', 32, 1, 1, '2024-03-15'),
                ('Truong Minh Duc', '048200028901', '0946012345', 'duc.truong@gmail.com', '1996-01-25', 'Nam', 'Da Nang', 'Hai Chau, Da Nang', 33, 1, 1, '2024-04-01'),
                ('Ho Bich Ngoc', '046300029012', '0935123456', 'ngoc.ho@gmail.com', '1998-10-02', 'Nu', 'Thua Thien Hue', 'TP Hue', 35, 1, 1, '2024-04-20'),
                ('Phan Thanh Tung', '038200030123', '0926234567', 'tung.phan@gmail.com', '1992-04-12', 'Nam', 'Thanh Hoa', 'Sam Son, Thanh Hoa', 36, 1, 1, '2024-05-01');
                ",

                // 9. 21 Hop dong thue
                @"
                INSERT INTO HopDong (MaPhong, MaKhachThue, NgayBatDau, NgayKetThuc, TienCoc, GiaThueThucTe, TrangThai, GhiChu, NgayLap) VALUES
                (1, 1, '2026-01-01', '2027-01-01', 5500000, 5500000, 'HieuLuc', 'Hop dong 1 nam can A101', '2026-01-01'),
                (2, 3, '2026-01-15', '2027-01-15', 7000000, 7000000, 'HieuLuc', 'Hop dong 1 nam can A102', '2026-01-15'),
                (4, 5, '2026-02-01', '2027-02-01', 5500000, 5500000, 'HieuLuc', 'Can A104', '2026-02-01'),
                (5, 6, '2026-02-10', '2027-02-10', 8500000, 8500000, 'HieuLuc', 'Can VIP A105', '2026-02-10'),
                (6, 8, '2026-02-15', '2027-02-15', 5500000, 5500000, 'HieuLuc', 'Can A201', '2026-02-15'),
                (7, 10, '2026-03-01', '2027-03-01', 7000000, 7000000, 'HieuLuc', 'Can A202', '2026-03-01'),
                (8, 11, '2026-03-05', '2027-03-05', 4500000, 4500000, 'HieuLuc', 'Can Studio A203', '2026-03-05'),
                (10, 12, '2026-03-10', '2027-03-10', 8500000, 8500000, 'HieuLuc', 'Can VIP A205', '2026-03-10'),
                (11, 13, '2026-04-01', '2027-04-01', 5600000, 5600000, 'HieuLuc', 'Can A301', '2026-04-01'),
                (12, 15, '2026-04-10', '2027-04-10', 7200000, 7200000, 'HieuLuc', 'Can A302', '2026-04-10'),
                (13, 16, '2026-04-15', '2027-04-15', 4600000, 4600000, 'HieuLuc', 'Can Studio A303', '2026-04-15'),
                (15, 18, '2026-05-01', '2027-05-01', 8800000, 8800000, 'HieuLuc', 'Can VIP A305', '2026-05-01'),

                (26, 19, '2026-01-10', '2027-01-10', 5000000, 5000000, 'HieuLuc', 'Can B101', '2026-01-10'),
                (27, 20, '2026-01-20', '2027-01-20', 6500000, 6500000, 'HieuLuc', 'Can B102', '2026-01-20'),
                (29, 22, '2026-02-01', '2027-02-01', 6500000, 6500000, 'HieuLuc', 'Can B104', '2026-02-01'),
                (30, 24, '2026-02-15', '2027-02-15', 8000000, 8000000, 'HieuLuc', 'Can B105 VIP', '2026-02-15'),
                (31, 25, '2026-03-01', '2027-03-01', 5200000, 5200000, 'HieuLuc', 'Can B201', '2026-03-01'),
                (32, 27, '2026-03-15', '2027-03-15', 6600000, 6600000, 'HieuLuc', 'Can B202', '2026-03-15'),
                (33, 28, '2026-04-01', '2027-04-01', 5200000, 5200000, 'HieuLuc', 'Can B203', '2026-04-01'),
                (35, 29, '2026-04-20', '2027-04-20', 8200000, 8200000, 'HieuLuc', 'Can B205 VIP', '2026-04-20'),
                (36, 30, '2026-05-01', '2027-05-01', 5200000, 5200000, 'HieuLuc', 'Can B301', '2026-05-01');
                ",

                // 10. Chi so dien nuoc
                @"
                INSERT INTO ChiSoDienNuoc (MaPhong, Thang, Nam, ChiSoDienCu, ChiSoDienMoi, ChiSoNuocCu, ChiSoNuocMoi, NgayGhi, NguoiGhi) VALUES
                (1, 6, 2026, 120, 245, 15, 23, '2026-06-30', 'Le Ngoc Quyen'),
                (2, 6, 2026, 210, 380, 25, 38, '2026-06-30', 'Le Ngoc Quyen'),
                (4, 6, 2026, 150, 275, 18, 26, '2026-06-30', 'Le Ngoc Quyen'),
                (5, 6, 2026, 300, 510, 30, 48, '2026-06-30', 'Le Ngoc Quyen'),
                (6, 6, 2026, 100, 215, 12, 19, '2026-06-30', 'Le Ngoc Quyen'),

                (1, 7, 2026, 245, 378, 23, 32, '2026-07-30', 'Le Ngoc Quyen'),
                (2, 7, 2026, 380, 560, 38, 52, '2026-07-30', 'Le Ngoc Quyen'),
                (4, 7, 2026, 275, 410, 26, 35, '2026-07-30', 'Le Ngoc Quyen'),
                (5, 7, 2026, 510, 735, 48, 67, '2026-07-30', 'Le Ngoc Quyen'),
                (6, 7, 2026, 215, 335, 19, 27, '2026-07-30', 'Le Ngoc Quyen'),

                (1, 8, 2026, 378, 498, 32, 40, '2026-08-30', 'Le Ngoc Quyen'),
                (2, 8, 2026, 560, 725, 52, 65, '2026-08-30', 'Le Ngoc Quyen'),
                (4, 8, 2026, 410, 530, 35, 43, '2026-08-30', 'Le Ngoc Quyen'),
                (5, 8, 2026, 735, 940, 67, 85, '2026-08-30', 'Le Ngoc Quyen'),
                (6, 8, 2026, 335, 445, 27, 34, '2026-08-30', 'Le Ngoc Quyen'),

                (1, 9, 2026, 498, 610, 40, 48, '2026-09-29', 'Le Ngoc Quyen'),
                (2, 9, 2026, 725, 880, 65, 77, '2026-09-29', 'Le Ngoc Quyen'),
                (4, 9, 2026, 530, 642, 43, 50, '2026-09-29', 'Le Ngoc Quyen'),
                (5, 9, 2026, 940, 1135, 85, 102, '2026-09-29', 'Le Ngoc Quyen'),
                (6, 9, 2026, 445, 550, 34, 41, '2026-09-29', 'Le Ngoc Quyen'),

                (1, 10, 2026, 610, 730, 48, 56, '2026-10-30', 'Le Ngoc Quyen'),
                (2, 10, 2026, 880, 1045, 77, 90, '2026-10-30', 'Le Ngoc Quyen'),
                (4, 10, 2026, 642, 760, 50, 58, '2026-10-30', 'Le Ngoc Quyen'),
                (5, 10, 2026, 1135, 1340, 102, 120, '2026-10-30', 'Le Ngoc Quyen'),
                (6, 10, 2026, 550, 660, 41, 48, '2026-10-30', 'Le Ngoc Quyen'),

                (1, 11, 2026, 730, 855, 56, 65, '2026-11-20', 'Le Ngoc Quyen'),
                (2, 11, 2026, 1045, 1215, 90, 104, '2026-11-20', 'Le Ngoc Quyen'),
                (4, 11, 2026, 760, 880, 58, 66, '2026-11-20', 'Le Ngoc Quyen'),
                (5, 11, 2026, 1340, 1555, 120, 139, '2026-11-20', 'Le Ngoc Quyen'),
                (6, 11, 2026, 660, 775, 48, 55, '2026-11-20', 'Le Ngoc Quyen');
                ",

                // 11. Hoa don & Chi tiet hoa don & Thanh toan
                @"
                INSERT INTO HoaDon (MaPhong, Thang, Nam, TienPhong, TienDien, TienNuoc, TienDichVu, TongTien, TrangThai, NgayLap, GhiChu) VALUES
                (1, 10, 2026, 5500000, 275000, 120000, 450000, 6345000, 'DaThanhToan', '2026-10-31', 'Hoa don thang 10/2026 phong A101'),
                (2, 10, 2026, 7000000, 395000, 195000, 450000, 8040000, 'DaThanhToan', '2026-10-31', 'Hoa don thang 10/2026 phong A102'),
                (4, 10, 2026, 5500000, 268000, 120000, 450000, 6338000, 'DaThanhToan', '2026-10-31', 'Hoa don thang 10/2026 phong A104'),
                (5, 10, 2026, 8500000, 520000, 270000, 450000, 9740000, 'DaThanhToan', '2026-10-31', 'Hoa don thang 10/2026 phong A105'),
                (6, 10, 2026, 5500000, 245000, 105000, 450000, 6300000, 'DaThanhToan', '2026-10-31', 'Hoa don thang 10/2026 phong A201'),

                (1, 11, 2026, 5500000, 249225, 135000, 450000, 6334225, 'DaThanhToan', '2026-11-20', 'Hoa don thang 11/2026 phong A101'),
                (2, 11, 2026, 7000000, 351420, 210000, 450000, 8011420, 'ChuaThanhToan', '2026-11-20', 'Hoa don thang 11/2026 phong A102 - Con no'),
                (4, 11, 2026, 5500000, 237870, 120000, 450000, 6307870, 'DaThanhToan', '2026-11-20', 'Hoa don thang 11/2026 phong A104'),
                (5, 11, 2026, 8500000, 462450, 285000, 450000, 9697450, 'ChuaThanhToan', '2026-11-20', 'Hoa don thang 11/2026 phong A105 - Con no'),
                (6, 11, 2026, 5500000, 226515, 105000, 450000, 6281515, 'QuaHan', '2026-11-01', 'Hoa don thang 11/2026 phong A201 - Qua han');

                INSERT INTO ChiTietHoaDon (MaHoaDon, TenKhoanThu, SoLuong, DonGia, ThanhTien, GhiChu) VALUES
                (6, 'Tien thue phong A101 thang 11/2026', 1, 5500000, 5500000, 'Theo hop dong'),
                (6, 'Tien dien (125 kWh)', 125, 1994, 249225, 'EVN 6 bac thang luy tien'),
                (6, 'Tien nuoc sach (9 m3)', 9, 15000, 135000, '15.000 d/m3'),
                (6, 'Phi Quan ly & Ve sinh chung', 1, 150000, 150000, 'Co dinh hang thang'),
                (6, 'Phi Bao tri Thang may', 1, 80000, 80000, 'Co dinh hang thang'),
                (6, 'Internet Cap quang toc do cao', 1, 220000, 220000, 'Goi toc do cao');

                INSERT INTO ThanhToan (MaHoaDon, NgayThanhToan, SoTienTra, HinhThucThanhToan, NguoiThu, GhiChu) VALUES
                (1, '2026-11-02', 6345000, 'ChuyenKhoan', 'Nguyen Thanh Nam', 'Chuyen khoan Vietcombank'),
                (2, '2026-11-02', 8040000, 'ChuyenKhoan', 'Nguyen Thanh Nam', 'Chuyen khoan MBBank'),
                (3, '2026-11-03', 6338000, 'TienMat', 'Phan Cong Thanh Dat', 'Nop tien mat tai phong quan ly'),
                (4, '2026-11-04', 9740000, 'ChuyenKhoan', 'Nguyen Thanh Nam', 'Chuyen khoan Techcombank'),
                (5, '2026-11-05', 6300000, 'TienMat', 'Phan Cong Thanh Dat', 'Tien mat'),
                (6, '2026-11-21', 6334225, 'ChuyenKhoan', 'Nguyen Thanh Nam', 'Chuyen khoan BIDV phong A101'),
                (8, '2026-11-21', 6307870, 'ChuyenKhoan', 'Nguyen Thanh Nam', 'Chuyen khoan Vietcombank phong A104');
                ",

                // 12. Tai khoan & Yeu cau & Thong bao
                @"
                INSERT INTO TaiKhoan (TenDangNhap, MatKhauHash, SaltHash, HoTen, QuyenHan, SoDienThoai, Email, IsActive, MaKhachThue, SoLanDangNhapSai) VALUES
                ('admin', '6htNRE6rHgHe8pMAEYAcmT2cITQGwXDVNcP+J/FocmI=', 'KioqKioqKioqKioqKioqKg==', 'Nguyen Thanh Nam (Chu nha)', 'ChuNha', '0901234567', 'admin.ancu@gmail.com', 1, NULL, 0),
                ('quanly01', 'a5VnN9OQKX2DlKEhuB9bbW6YYBMW/5ef1enJIrYO3uE=', 'KysrKysrKysrKysrKysrKw==', 'Phan Cong Thanh Dat (Quan ly)', 'QuanLy', '0987654321', 'dat.quanly@gmail.com', 1, NULL, 0),
                ('khachthue01', 'TLIxrShaMakmNl+DMTWRCK7A0OXn2hEUJt9GucXanVw=', 'LCwsLCwsLCwsLCwsLCwsLA==', 'Nguyen Van An (Cu dan A101)', 'KhachThue', '0912345678', 'an.nguyen@gmail.com', 1, 1, 0);

                INSERT INTO YeuCau (MaPhong, MaKhachThue, LoaiYeuCau, TieuDe, NoiDung, NgayGui, TrangThai, KetQuaXuLy, DanhGiaSao, NgayXuLy) VALUES
                (1, 1, 'BaoHong', 'Voi sen nha tam bi ro ri nuoc', 'Voi hoa sen tai phong tam master bi ro nuoc o chan van, nho BQL kiem tra thay gioang cao su', '2026-11-10 09:30:00', 'HoanThanh', 'Ky thuat vien da thay gioang cao su va siet lai van nuoc luc 14:00', 5, '2026-11-10 14:30:00'),
                (2, 3, 'BaoHong', 'Dieu hoa phong ngu nho khong mat', 'Dieu hoa Daikin bat 16 do nhung chi co gio thoang, nghi ngo het gas', '2026-11-12 11:20:00', 'HoanThanh', 'Da nap bo sung gas R32 va ve sinh luoi loc', 5, '2026-11-13 10:00:00'),
                (5, 6, 'YeuCauKhac', 'Dang ky them the xe may thu hai', 'Phong co them thanh vien chuyen vao o can dang ky 01 the tu xe may moi bien so 29E1-88888', '2026-11-18 16:45:00', 'DangXuLy', 'BQL dang in the tu moi, hen nhan vao thu 2 tuan toi', NULL, '2026-11-19 08:30:00'),
                (6, 8, 'BaoHong', 'Den chieu sang ban cong chap chon', 'Bong den LED ban cong bat len nhap nhay lien tuc roi tat han', '2026-11-21 19:15:00', 'MoiGui', NULL, NULL, NULL);

                INSERT INTO ThongBao (TieuDe, NoiDung, NgayDang, NguoiDang, LoaiThongBao, MaPhong, IsActive) VALUES
                ('Thong bao: Lich kiem tra he thong PCCC dinh ky Quy 4/2026', 'Ban Quan ly chung cu An Cu xin thong bao toi toan the cu dan: Vao hoi 09:00 - 11:30 ngay 25/11/2026, BQL se phoi hop voi Canh sat PCCC quan Bac Tu Liem kiem tra chuong bao chay, voi phun Sprinkler tai cac hanh lang tang 1 den tang 5. Trong thoi gian kiem tra, chuong bao dong co the vang len ngat quang, kinh mong Quy cu dan khong hoang mang.', '2026-11-15 08:00:00', 'Ban Quan ly An Cu', 'ToanToaNha', NULL, 1),
                ('Thong bao: Tam ngung cap nuoc sinh hoat de suc rua be ngam', 'De dam bao chat luong nguon nuoc sach sinh hoat, BQL se tien hanh suc rua be nuoc ngam va be mai toa A tu 23:00 ngay 28/11/2026 den 04:00 sang ngay 29/11/2026. Kinh de nghi cu dan chu dong tich tru nuoc su dung trong khung gio tren.', '2026-11-18 10:00:00', 'Ban Quan ly An Cu', 'ToanToaNha', NULL, 1),
                ('Thong bao rieng: Nhac lich nop phi thue phong va dich vu thang 11/2026', 'Hoa don tien nha va dich vu thang 11/2026 cua phong A102 hien chua hoan tat thanh toan. De nghi chu ho lien he BQL hoac chuyen khoan qua tai khoan ngan hang de hoan tat truoc ngay 30/11/2026.', '2026-11-21 14:00:00', 'Ke toan An Cu', 'TheoPhong', 2, 1);
                ",

                // 13. Bat lai rang buoc va triggers
                @"
                ALTER TABLE Phong CHECK CONSTRAINT ALL;
                ALTER TABLE HopDong CHECK CONSTRAINT ALL;
                ALTER TABLE KhachThue CHECK CONSTRAINT ALL;
                ALTER TABLE ChiSoDienNuoc CHECK CONSTRAINT ALL;
                ALTER TABLE HoaDon CHECK CONSTRAINT ALL;
                ALTER TABLE ChiTietHoaDon CHECK CONSTRAINT ALL;
                ALTER TABLE ThanhToan CHECK CONSTRAINT ALL;
                ALTER TABLE ThietBiPhong CHECK CONSTRAINT ALL;
                ALTER TABLE YeuCau CHECK CONSTRAINT ALL;
                ALTER TABLE ThongBao CHECK CONSTRAINT ALL;
                ALTER TABLE TaiKhoan CHECK CONSTRAINT ALL;
                ALTER TABLE ToaNha CHECK CONSTRAINT ALL;
                ALTER TABLE DichVu CHECK CONSTRAINT ALL;

                IF EXISTS (SELECT 1 FROM sys.triggers WHERE name = 'trg_CapNhatTrangThaiPhong_SauThemHopDong')
                    ENABLE TRIGGER trg_CapNhatTrangThaiPhong_SauThemHopDong ON HopDong;
                IF EXISTS (SELECT 1 FROM sys.triggers WHERE name = 'trg_CapNhatTrangThaiPhong_SauChamDutHopDong')
                    ENABLE TRIGGER trg_CapNhatTrangThaiPhong_SauChamDutHopDong ON HopDong;
                IF EXISTS (SELECT 1 FROM sys.triggers WHERE name = 'trg_TinhTongTienHoaDon')
                    ENABLE TRIGGER trg_TinhTongTienHoaDon ON ChiTietHoaDon;
                "
            };

            foreach (var sql in sqlBatches)
            {
                using var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 120;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
