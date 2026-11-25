-- ==============================================================
-- BO CONG THUONG - TRUONG DAI HOC DIEN LUC
-- BAI TAP LON LAP TRINH .NET - DE TAI: QUAN LY CHUNG CU AN CU
-- File: 00_TaoMoi_ToanBo_CSDL.sql
-- Mo ta: KHOI TAO TOAN BO CO SO DU LIEU MOI 100% (ALL-IN-ONE)
--       - 2 Toa nha, 50 Phong (A101-A505, B101-B505)
--       - 30 Cu dan (Khach thue) thuc te, Tieng Viet khong dau 100%
--       - 21 Hop dong thue con hieu luc
--       - 5 Goi dich vu co ban
--       - Chi so dien nuoc, Hoa don, Chi tiet hoa don, Thanh toan
--       - 3 Tai khoan: admin / quanly01 / khachthue01
--       - Yeu cau bao hong, Thong bao chung cu
--       - Tat ca Stored Procedures & Triggers
-- HUONG DAN CHAY TRONG VISUAL STUDIO:
--   1. Mo Visual Studio 2022
--   2. Bam File -> Open -> File... (Ctrl + O)
--   3. Chon file: 00_TaoMoi_ToanBo_CSDL.sql
--   4. Bam nut tam giac xanh "Execute" (hoac Ctrl + Shift + E)
--   5. Chon Server: .\MSSQLSERVER01 -> Bam Connect
--   6. Xem thong bao "HOAN TAT!" o cua so Messages phia duoi.
-- ==============================================================

USE master;
GO

-- 1. XOA DATABASE CU NEU DANG TON TAI (DONG TAT CA KET NOI DANG MO)
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'QuanLyChungCuAnCu')
BEGIN
    ALTER DATABASE QuanLyChungCuAnCu SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyChungCuAnCu;
    PRINT N'-> Da xoa CSDL cu QuanLyChungCuAnCu.';
END
GO

-- 2. TAO DATABASE MOI
CREATE DATABASE QuanLyChungCuAnCu;
GO

USE QuanLyChungCuAnCu;
GO

PRINT N'-> Da tao moi database QuanLyChungCuAnCu thanh cong.';
GO

-- ==============================================================
-- PHAN 1: TAO 13 BANG (THEO DUNG THU TU RANG BUOC KHOA NGOAI)
-- ==============================================================

-- 1. Bang ToaNha
CREATE TABLE ToaNha (
    MaToaNha        INT IDENTITY(1,1) PRIMARY KEY,
    TenToaNha       NVARCHAR(100)   NOT NULL,
    DiaChi          NVARCHAR(255),
    SoTang          INT,
    MoTa            NVARCHAR(500)
);
GO

-- 2. Bang Phong
CREATE TABLE Phong (
    MaPhong         INT IDENTITY(1,1) PRIMARY KEY,
    SoPhong         NVARCHAR(10)    NOT NULL,
    Tang            INT             NOT NULL,
    DienTich        DECIMAL(10,2),
    GiaThue         DECIMAL(18,0)   NOT NULL,
    TrangThai       NVARCHAR(20)    DEFAULT 'Trong',
    LoaiPhong       NVARCHAR(50),
    MoTa            NVARCHAR(500),
    MaToaNha        INT             NOT NULL,
    CONSTRAINT FK_Phong_ToaNha FOREIGN KEY (MaToaNha) REFERENCES ToaNha(MaToaNha)
);
GO

-- 3. Bang KhachThue
CREATE TABLE KhachThue (
    MaKhachThue     INT IDENTITY(1,1) PRIMARY KEY,
    HoTen           NVARCHAR(100)   NOT NULL,
    CCCD            NVARCHAR(12)    NOT NULL UNIQUE,
    SoDienThoai     NVARCHAR(15),
    Email           NVARCHAR(100),
    NgaySinh        DATE,
    GioiTinh        NVARCHAR(5),
    QueQuan         NVARCHAR(255),
    DiaChiThuongTru NVARCHAR(255),
    MaPhong         INT             NULL,
    LaChuHo         BIT             DEFAULT 0,
    IsActive        BIT             DEFAULT 1,
    NgayDangKy      DATETIME        DEFAULT GETDATE(),
    CONSTRAINT FK_KhachThue_Phong FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong)
);
GO

-- 4. Bang ThietBiPhong
CREATE TABLE ThietBiPhong (
    MaThietBi       INT IDENTITY(1,1) PRIMARY KEY,
    MaPhong         INT             NOT NULL,
    TenThietBi      NVARCHAR(100)   NOT NULL,
    SoLuong         INT             DEFAULT 1,
    TinhTrang       NVARCHAR(20)    DEFAULT 'Tot',
    NgayLapDat      DATE,
    GhiChu          NVARCHAR(500),
    CONSTRAINT FK_ThietBiPhong_Phong FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong)
);
GO

-- 5. Bang HopDong
CREATE TABLE HopDong (
    MaHopDong       INT IDENTITY(1,1) PRIMARY KEY,
    MaPhong         INT             NOT NULL,
    MaKhachThue     INT             NOT NULL,
    NgayBatDau      DATE            NOT NULL,
    NgayKetThuc     DATE            NOT NULL,
    TienCoc         DECIMAL(18,0)   DEFAULT 0,
    GiaThueThucTe   DECIMAL(18,0)   NOT NULL,
    TrangThai       NVARCHAR(20)    DEFAULT 'HieuLuc',
    GhiChu          NVARCHAR(500),
    NgayLap         DATETIME        DEFAULT GETDATE(),
    CONSTRAINT FK_HopDong_Phong FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong),
    CONSTRAINT FK_HopDong_KhachThue FOREIGN KEY (MaKhachThue) REFERENCES KhachThue(MaKhachThue)
);
GO

-- 6. Bang DichVu
CREATE TABLE DichVu (
    MaDichVu        INT IDENTITY(1,1) PRIMARY KEY,
    TenDichVu       NVARCHAR(100)   NOT NULL,
    DonGia          DECIMAL(18,0)   NOT NULL,
    DonViTinh       NVARCHAR(50),
    MoTa            NVARCHAR(500),
    IsActive        BIT             DEFAULT 1
);
GO

-- 7. Bang ChiSoDienNuoc
CREATE TABLE ChiSoDienNuoc (
    MaChiSo         INT IDENTITY(1,1) PRIMARY KEY,
    MaPhong         INT             NOT NULL,
    Thang           INT             NOT NULL,
    Nam             INT             NOT NULL,
    ChiSoDienCu     INT             DEFAULT 0,
    ChiSoDienMoi    INT             DEFAULT 0,
    ChiSoNuocCu     INT             DEFAULT 0,
    ChiSoNuocMoi    INT             DEFAULT 0,
    NgayGhi         DATETIME        DEFAULT GETDATE(),
    NguoiGhi        NVARCHAR(100),
    CONSTRAINT FK_ChiSoDienNuoc_Phong FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong)
);
GO

-- 8. Bang HoaDon
CREATE TABLE HoaDon (
    MaHoaDon        INT IDENTITY(1,1) PRIMARY KEY,
    MaPhong         INT             NOT NULL,
    Thang           INT             NOT NULL,
    Nam             INT             NOT NULL,
    TienPhong       DECIMAL(18,0)   DEFAULT 0,
    TienDien        DECIMAL(18,0)   DEFAULT 0,
    TienNuoc        DECIMAL(18,0)   DEFAULT 0,
    TienDichVu      DECIMAL(18,0)   DEFAULT 0,
    TongTien        DECIMAL(18,0)   DEFAULT 0,
    TrangThai       NVARCHAR(20)    DEFAULT 'ChuaThanhToan',
    NgayLap         DATETIME        DEFAULT GETDATE(),
    GhiChu          NVARCHAR(500),
    CONSTRAINT FK_HoaDon_Phong FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong)
);
GO

-- 9. Bang ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    MaChiTietHD     INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon        INT             NOT NULL,
    TenKhoanThu     NVARCHAR(100),
    SoLuong         DECIMAL(18,2)   DEFAULT 1,
    DonGia          DECIMAL(18,0),
    ThanhTien       DECIMAL(18,0),
    GhiChu          NVARCHAR(500),
    CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon)
);
GO

-- 10. Bang ThanhToan
CREATE TABLE ThanhToan (
    MaThanhToan         INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon            INT             NOT NULL,
    NgayThanhToan       DATETIME        DEFAULT GETDATE(),
    SoTienTra           DECIMAL(18,0)   NOT NULL,
    HinhThucThanhToan   NVARCHAR(20)    DEFAULT 'TienMat',
    NguoiThu            NVARCHAR(100),
    GhiChu              NVARCHAR(500),
    CONSTRAINT FK_ThanhToan_HoaDon FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon)
);
GO

-- 11. Bang TaiKhoan
CREATE TABLE TaiKhoan (
    MaTaiKhoan          INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap         NVARCHAR(50)    NOT NULL UNIQUE,
    MatKhauHash         NVARCHAR(256)   NOT NULL,
    SaltHash            NVARCHAR(256)   NOT NULL,
    HoTen               NVARCHAR(100),
    QuyenHan            NVARCHAR(20)    NOT NULL,
    SoDienThoai         NVARCHAR(15),
    Email               NVARCHAR(100),
    IsActive            BIT             DEFAULT 1,
    MaKhachThue         INT             NULL,
    SoLanDangNhapSai    INT             DEFAULT 0,
    CONSTRAINT FK_TaiKhoan_KhachThue FOREIGN KEY (MaKhachThue) REFERENCES KhachThue(MaKhachThue)
);
GO

-- 12. Bang YeuCau
CREATE TABLE YeuCau (
    MaYeuCau        INT IDENTITY(1,1) PRIMARY KEY,
    MaPhong         INT,
    MaKhachThue     INT,
    LoaiYeuCau      NVARCHAR(20)    DEFAULT 'BaoHong',
    TieuDe          NVARCHAR(200),
    NoiDung         NVARCHAR(2000),
    NgayGui         DATETIME        DEFAULT GETDATE(),
    TrangThai       NVARCHAR(20)    DEFAULT 'MoiGui',
    KetQuaXuLy      NVARCHAR(2000),
    DanhGiaSao      INT             NULL,
    NgayXuLy        DATETIME        NULL,
    CONSTRAINT FK_YeuCau_Phong FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong),
    CONSTRAINT FK_YeuCau_KhachThue FOREIGN KEY (MaKhachThue) REFERENCES KhachThue(MaKhachThue)
);
GO

-- 13. Bang ThongBao
CREATE TABLE ThongBao (
    MaThongBao      INT IDENTITY(1,1) PRIMARY KEY,
    TieuDe          NVARCHAR(200)   NOT NULL,
    NoiDung         NVARCHAR(2000),
    NgayDang        DATETIME        DEFAULT GETDATE(),
    NguoiDang       NVARCHAR(100),
    LoaiThongBao    NVARCHAR(20)    DEFAULT 'ToanToaNha',
    MaPhong         INT             NULL,
    IsActive        BIT             DEFAULT 1,
    CONSTRAINT FK_ThongBao_Phong FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong)
);
GO

PRINT N'-> Da tao xong 13 bang.';
GO

-- ==============================================================
-- PHAN 2: RANG BUOC TOAN VEN & CHECK CONSTRAINTS
-- ==============================================================

-- RANG BUOC TRANG THAI PHONG: Chap nhan ca chu khong dau va co dau de tuyet doi khong bao gio xung dot
ALTER TABLE Phong
ADD CONSTRAINT CK_Phong_TrangThai
CHECK (TrangThai IN ('Trong', 'DangThue', 'SuaChua', 'DatCoc', N'Trống', N'ĐangThuê', N'SửaChữa', N'ĐặtCọc'));
GO

ALTER TABLE Phong
ADD CONSTRAINT UQ_Phong_SoPhong_ToaNha
UNIQUE (SoPhong, MaToaNha);
GO

ALTER TABLE HopDong
ADD CONSTRAINT CK_HopDong_TrangThai
CHECK (TrangThai IN ('HieuLuc', 'HetHan', 'DaThanhLy'));
GO

ALTER TABLE HopDong
ADD CONSTRAINT CK_HopDong_NgayKetThuc
CHECK (NgayKetThuc > NgayBatDau);
GO

ALTER TABLE HoaDon
ADD CONSTRAINT CK_HoaDon_TrangThai
CHECK (TrangThai IN ('ChuaThanhToan', 'DaThanhToan', 'QuaHan'));
GO

ALTER TABLE HoaDon
ADD CONSTRAINT CK_HoaDon_Thang
CHECK (Thang BETWEEN 1 AND 12);
GO

ALTER TABLE HoaDon
ADD CONSTRAINT UQ_HoaDon_Phong_Thang_Nam
UNIQUE (MaPhong, Thang, Nam);
GO

ALTER TABLE YeuCau
ADD CONSTRAINT CK_YeuCau_TrangThai
CHECK (TrangThai IN ('MoiGui', 'DangXuLy', 'HoanThanh', 'TuChoi'));
GO

ALTER TABLE YeuCau
ADD CONSTRAINT CK_YeuCau_LoaiYeuCau
CHECK (LoaiYeuCau IN ('BaoHong', 'YeuCauKhac', 'GiaHanHopDong', 'ChamDutHopDong'));
GO

ALTER TABLE YeuCau
ADD CONSTRAINT CK_YeuCau_DanhGiaSao
CHECK (DanhGiaSao IS NULL OR (DanhGiaSao BETWEEN 1 AND 5));
GO

ALTER TABLE ChiSoDienNuoc
ADD CONSTRAINT CK_ChiSoDienNuoc_Dien
CHECK (ChiSoDienMoi >= ChiSoDienCu);
GO

ALTER TABLE ChiSoDienNuoc
ADD CONSTRAINT CK_ChiSoDienNuoc_Nuoc
CHECK (ChiSoNuocMoi >= ChiSoNuocCu);
GO

ALTER TABLE ChiSoDienNuoc
ADD CONSTRAINT CK_ChiSoDienNuoc_Thang
CHECK (Thang BETWEEN 1 AND 12);
GO

ALTER TABLE ChiSoDienNuoc
ADD CONSTRAINT UQ_ChiSoDienNuoc_Phong_Thang_Nam
UNIQUE (MaPhong, Thang, Nam);
GO

ALTER TABLE KhachThue
ADD CONSTRAINT CK_KhachThue_CCCD_Length
CHECK (LEN(CCCD) = 12);
GO

ALTER TABLE TaiKhoan
ADD CONSTRAINT CK_TaiKhoan_QuyenHan
CHECK (QuyenHan IN ('ChuNha', 'QuanLy', 'KhachThue'));
GO

ALTER TABLE ThanhToan
ADD CONSTRAINT CK_ThanhToan_HinhThuc
CHECK (HinhThucThanhToan IN ('TienMat', 'ChuyenKhoan', 'TheNganHang'));
GO

PRINT N'-> Da tao xong toan bo rang buoc CHECK & UNIQUE.';
GO

-- ==============================================================
-- PHAN 3: TRIGGERS TU DONG CAP NHAT
-- ==============================================================

-- Trigger 1: Cap nhat phong sang DangThue khi them Hop dong
CREATE OR ALTER TRIGGER trg_CapNhatTrangThaiPhong_SauThemHopDong
ON HopDong
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE p
    SET p.TrangThai = 'DangThue'
    FROM Phong p
    INNER JOIN inserted i ON p.MaPhong = i.MaPhong
    WHERE i.TrangThai = 'HieuLuc';
END;
GO

-- Trigger 2: Cap nhat phong sang Trong khi thanh ly Hop dong
CREATE OR ALTER TRIGGER trg_CapNhatTrangThaiPhong_SauChamDutHopDong
ON HopDong
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF UPDATE(TrangThai)
    BEGIN
        UPDATE p
        SET p.TrangThai = 'Trong'
        FROM Phong p
        INNER JOIN inserted i ON p.MaPhong = i.MaPhong
        INNER JOIN deleted d ON i.MaHopDong = d.MaHopDong
        WHERE i.TrangThai = 'DaThanhLy'
          AND d.TrangThai <> 'DaThanhLy'
          AND NOT EXISTS (
              SELECT 1 FROM HopDong hd
              WHERE hd.MaPhong = i.MaPhong
                AND hd.MaHopDong <> i.MaHopDong
                AND hd.TrangThai = 'HieuLuc'
          );
    END;
END;
GO

-- Trigger 3: Tu dong tinh tong tien hoa don theo ChiTietHoaDon
CREATE OR ALTER TRIGGER trg_TinhTongTienHoaDon
ON ChiTietHoaDon
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    ;WITH AffectedBills AS (
        SELECT MaHoaDon FROM inserted
        UNION
        SELECT MaHoaDon FROM deleted
    ),
    BillTotals AS (
        SELECT ab.MaHoaDon, ISNULL(SUM(ct.ThanhTien), 0) AS Tong
        FROM AffectedBills ab
        LEFT JOIN ChiTietHoaDon ct ON ab.MaHoaDon = ct.MaHoaDon
        GROUP BY ab.MaHoaDon
    )
    UPDATE hd
    SET hd.TongTien = bt.Tong
    FROM HoaDon hd
    INNER JOIN BillTotals bt ON hd.MaHoaDon = bt.MaHoaDon;
END;
GO

PRINT N'-> Da tao xong 3 Triggers.';
GO

-- ==============================================================
-- PHAN 4: 35 STORED PROCEDURES
-- ==============================================================
IF OBJECT_ID('sp_DangNhap', 'P') IS NOT NULL DROP PROCEDURE sp_DangNhap;
GO
CREATE PROCEDURE sp_DangNhap
    @TenDangNhap NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaTaiKhoan, TenDangNhap, MatKhauHash, SaltHash, HoTen, QuyenHan, SoDienThoai, Email, IsActive, MaKhachThue, SoLanDangNhapSai
    FROM TaiKhoan
    WHERE TenDangNhap = @TenDangNhap OR (SoDienThoai IS NOT NULL AND SoDienThoai = @TenDangNhap);
END;
GO

IF OBJECT_ID('sp_DoiMatKhau', 'P') IS NOT NULL DROP PROCEDURE sp_DoiMatKhau;
GO
CREATE PROCEDURE sp_DoiMatKhau
    @MaTaiKhoan INT,
    @MatKhauHashMoi NVARCHAR(256),
    @SaltHashMoi NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE TaiKhoan
    SET MatKhauHash = @MatKhauHashMoi,
        SaltHash = @SaltHashMoi,
        SoLanDangNhapSai = 0
    WHERE MaTaiKhoan = @MaTaiKhoan;
END;
GO

IF OBJECT_ID('sp_TangSoLanDangNhapSai', 'P') IS NOT NULL DROP PROCEDURE sp_TangSoLanDangNhapSai;
GO
CREATE PROCEDURE sp_TangSoLanDangNhapSai
    @MaTaiKhoan INT
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE TaiKhoan
    SET SoLanDangNhapSai = SoLanDangNhapSai + 1,
        IsActive = CASE WHEN SoLanDangNhapSai + 1 >= 5 THEN 0 ELSE IsActive END
    WHERE MaTaiKhoan = @MaTaiKhoan;
END;
GO

IF OBJECT_ID('sp_ResetSoLanDangNhapSai', 'P') IS NOT NULL DROP PROCEDURE sp_ResetSoLanDangNhapSai;
GO
CREATE PROCEDURE sp_ResetSoLanDangNhapSai
    @MaTaiKhoan INT
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE TaiKhoan
    SET SoLanDangNhapSai = 0
    WHERE MaTaiKhoan = @MaTaiKhoan;
END;
GO

IF OBJECT_ID('sp_GetAllTaiKhoan', 'P') IS NOT NULL DROP PROCEDURE sp_GetAllTaiKhoan;
GO
CREATE PROCEDURE sp_GetAllTaiKhoan
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaTaiKhoan, TenDangNhap, HoTen, QuyenHan, SoDienThoai, Email, IsActive, MaKhachThue, SoLanDangNhapSai
    FROM TaiKhoan;
END;
GO

IF OBJECT_ID('sp_ThemTaiKhoan', 'P') IS NOT NULL DROP PROCEDURE sp_ThemTaiKhoan;
GO
CREATE PROCEDURE sp_ThemTaiKhoan
    @TenDangNhap NVARCHAR(50),
    @MatKhauHash NVARCHAR(256),
    @SaltHash NVARCHAR(256),
    @HoTen NVARCHAR(100),
    @QuyenHan NVARCHAR(20),
    @SoDienThoai NVARCHAR(15) = NULL,
    @Email NVARCHAR(100) = NULL,
    @MaKhachThue INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO TaiKhoan (TenDangNhap, MatKhauHash, SaltHash, HoTen, QuyenHan, SoDienThoai, Email, IsActive, MaKhachThue, SoLanDangNhapSai)
    VALUES (@TenDangNhap, @MatKhauHash, @SaltHash, @HoTen, @QuyenHan, @SoDienThoai, @Email, 1, @MaKhachThue, 0);
    SELECT SCOPE_IDENTITY();
END;
GO

-- ==============================================================
-- 2. UC2: QUẢN LÝ PHÒNG & THIẾT BỊ (Phan Công Thành Đạt)
-- ==============================================================

IF OBJECT_ID('sp_GetAllToaNha', 'P') IS NOT NULL DROP PROCEDURE sp_GetAllToaNha;
GO
CREATE PROCEDURE sp_GetAllToaNha
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaToaNha, TenToaNha, DiaChi, SoTang, MoTa
    FROM ToaNha
    ORDER BY MaToaNha;
END;
GO

IF OBJECT_ID('sp_GetAllPhong', 'P') IS NOT NULL DROP PROCEDURE sp_GetAllPhong;
GO
CREATE PROCEDURE sp_GetAllPhong
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.MaPhong, p.SoPhong, p.Tang, p.DienTich, p.GiaThue, p.TrangThai, p.LoaiPhong, p.MoTa, p.MaToaNha, t.TenToaNha
    FROM Phong p
    INNER JOIN ToaNha t ON p.MaToaNha = t.MaToaNha
    ORDER BY p.Tang, p.SoPhong;
END;
GO

IF OBJECT_ID('sp_ThemPhong', 'P') IS NOT NULL DROP PROCEDURE sp_ThemPhong;
GO
CREATE PROCEDURE sp_ThemPhong
    @SoPhong NVARCHAR(10),
    @Tang INT,
    @DienTich DECIMAL(10,2),
    @GiaThue DECIMAL(18,0),
    @TrangThai NVARCHAR(20),
    @LoaiPhong NVARCHAR(50),
    @MoTa NVARCHAR(500),
    @MaToaNha INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Phong (SoPhong, Tang, DienTich, GiaThue, TrangThai, LoaiPhong, MoTa, MaToaNha)
    VALUES (@SoPhong, @Tang, @DienTich, @GiaThue, @TrangThai, @LoaiPhong, @MoTa, @MaToaNha);
    SELECT SCOPE_IDENTITY();
END;
GO

IF OBJECT_ID('sp_CapNhatPhong', 'P') IS NOT NULL DROP PROCEDURE sp_CapNhatPhong;
GO
CREATE PROCEDURE sp_CapNhatPhong
    @MaPhong INT,
    @SoPhong NVARCHAR(10),
    @Tang INT,
    @DienTich DECIMAL(10,2),
    @GiaThue DECIMAL(18,0),
    @TrangThai NVARCHAR(20),
    @LoaiPhong NVARCHAR(50),
    @MoTa NVARCHAR(500),
    @MaToaNha INT
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE Phong
    SET SoPhong = @SoPhong,
        Tang = @Tang,
        DienTich = @DienTich,
        GiaThue = @GiaThue,
        TrangThai = @TrangThai,
        LoaiPhong = @LoaiPhong,
        MoTa = @MoTa,
        MaToaNha = @MaToaNha
    WHERE MaPhong = @MaPhong;
END;
GO

IF OBJECT_ID('sp_CapNhatTrangThaiPhong', 'P') IS NOT NULL DROP PROCEDURE sp_CapNhatTrangThaiPhong;
GO
CREATE PROCEDURE sp_CapNhatTrangThaiPhong
    @MaPhong INT,
    @TrangThai NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE Phong
    SET TrangThai = @TrangThai
    WHERE MaPhong = @MaPhong;
END;
GO

IF OBJECT_ID('sp_TimKiemPhong', 'P') IS NOT NULL DROP PROCEDURE sp_TimKiemPhong;
GO
CREATE PROCEDURE sp_TimKiemPhong
    @Tang INT = NULL,
    @TrangThai NVARCHAR(20) = NULL,
    @GiaMin DECIMAL(18,0) = NULL,
    @GiaMax DECIMAL(18,0) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.MaPhong, p.SoPhong, p.Tang, p.DienTich, p.GiaThue, p.TrangThai, p.LoaiPhong, p.MoTa, p.MaToaNha, t.TenToaNha
    FROM Phong p
    INNER JOIN ToaNha t ON p.MaToaNha = t.MaToaNha
    WHERE (@Tang IS NULL OR p.Tang = @Tang)
      AND (@TrangThai IS NULL OR p.TrangThai = @TrangThai)
      AND (@GiaMin IS NULL OR p.GiaThue >= @GiaMin)
      AND (@GiaMax IS NULL OR p.GiaThue <= @GiaMax)
    ORDER BY p.Tang, p.SoPhong;
END;
GO

IF OBJECT_ID('sp_GetPhongTheoMa', 'P') IS NOT NULL DROP PROCEDURE sp_GetPhongTheoMa;
GO
CREATE PROCEDURE sp_GetPhongTheoMa
    @MaPhong INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.MaPhong, p.SoPhong, p.Tang, p.DienTich, p.GiaThue, p.TrangThai, p.LoaiPhong, p.MoTa, p.MaToaNha, t.TenToaNha
    FROM Phong p
    INNER JOIN ToaNha t ON p.MaToaNha = t.MaToaNha
    WHERE p.MaPhong = @MaPhong;
END;
GO

-- Thiết bị phòng
IF OBJECT_ID('sp_GetThietBiTheoPhong', 'P') IS NOT NULL DROP PROCEDURE sp_GetThietBiTheoPhong;
GO
CREATE PROCEDURE sp_GetThietBiTheoPhong
    @MaPhong INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaThietBi, MaPhong, TenThietBi, SoLuong, TinhTrang, NgayLapDat, GhiChu
    FROM ThietBiPhong
    WHERE MaPhong = @MaPhong;
END;
GO

IF OBJECT_ID('sp_ThemThietBi', 'P') IS NOT NULL DROP PROCEDURE sp_ThemThietBi;
GO
CREATE PROCEDURE sp_ThemThietBi
    @MaPhong INT,
    @TenThietBi NVARCHAR(100),
    @SoLuong INT,
    @TinhTrang NVARCHAR(20),
    @NgayLapDat DATE,
    @GhiChu NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO ThietBiPhong (MaPhong, TenThietBi, SoLuong, TinhTrang, NgayLapDat, GhiChu)
    VALUES (@MaPhong, @TenThietBi, @SoLuong, @TinhTrang, @NgayLapDat, @GhiChu);
    SELECT SCOPE_IDENTITY();
END;
GO

IF OBJECT_ID('sp_CapNhatThietBi', 'P') IS NOT NULL DROP PROCEDURE sp_CapNhatThietBi;
GO
CREATE PROCEDURE sp_CapNhatThietBi
    @MaThietBi INT,
    @TenThietBi NVARCHAR(100),
    @SoLuong INT,
    @TinhTrang NVARCHAR(20),
    @NgayLapDat DATE,
    @GhiChu NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE ThietBiPhong
    SET TenThietBi = @TenThietBi,
        SoLuong = @SoLuong,
        TinhTrang = @TinhTrang,
        NgayLapDat = @NgayLapDat,
        GhiChu = @GhiChu
    WHERE MaThietBi = @MaThietBi;
END;
GO

IF OBJECT_ID('sp_XoaThietBi', 'P') IS NOT NULL DROP PROCEDURE sp_XoaThietBi;
GO
CREATE PROCEDURE sp_XoaThietBi
    @MaThietBi INT
AS
BEGIN
    SET NOCOUNT OFF;
    DELETE FROM ThietBiPhong WHERE MaThietBi = @MaThietBi;
END;
GO

-- ==============================================================
-- 3. UC3: QUẢN LÝ KHÁCH THUÊ (Phan Công Thành Đạt)
-- ==============================================================

IF OBJECT_ID('sp_GetAllKhachThue', 'P') IS NOT NULL DROP PROCEDURE sp_GetAllKhachThue;
GO
CREATE PROCEDURE sp_GetAllKhachThue
AS
BEGIN
    SET NOCOUNT ON;
    SELECT kt.MaKhachThue, kt.HoTen, kt.CCCD, kt.SoDienThoai, kt.Email, kt.NgaySinh, kt.GioiTinh,
           kt.QueQuan, kt.DiaChiThuongTru, kt.MaPhong, p.SoPhong, kt.LaChuHo, kt.IsActive, kt.NgayDangKy
    FROM KhachThue kt
    LEFT JOIN Phong p ON kt.MaPhong = p.MaPhong
    WHERE kt.IsActive = 1
    ORDER BY kt.HoTen;
END;
GO

IF OBJECT_ID('sp_ThemKhachThue', 'P') IS NOT NULL DROP PROCEDURE sp_ThemKhachThue;
GO
CREATE PROCEDURE sp_ThemKhachThue
    @HoTen NVARCHAR(100),
    @CCCD NVARCHAR(12),
    @SoDienThoai NVARCHAR(15),
    @Email NVARCHAR(100),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(5),
    @QueQuan NVARCHAR(255),
    @DiaChiThuongTru NVARCHAR(255),
    @MaPhong INT = NULL,
    @LaChuHo BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO KhachThue (HoTen, CCCD, SoDienThoai, Email, NgaySinh, GioiTinh, QueQuan, DiaChiThuongTru, MaPhong, LaChuHo, IsActive, NgayDangKy)
    VALUES (@HoTen, @CCCD, @SoDienThoai, @Email, @NgaySinh, @GioiTinh, @QueQuan, @DiaChiThuongTru, @MaPhong, @LaChuHo, 1, GETDATE());
    SELECT SCOPE_IDENTITY();
END;
GO

IF OBJECT_ID('sp_CapNhatKhachThue', 'P') IS NOT NULL DROP PROCEDURE sp_CapNhatKhachThue;
GO
CREATE PROCEDURE sp_CapNhatKhachThue
    @MaKhachThue INT,
    @HoTen NVARCHAR(100),
    @CCCD NVARCHAR(12),
    @SoDienThoai NVARCHAR(15),
    @Email NVARCHAR(100),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(5),
    @QueQuan NVARCHAR(255),
    @DiaChiThuongTru NVARCHAR(255),
    @MaPhong INT = NULL,
    @LaChuHo BIT = 0
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE KhachThue
    SET HoTen = @HoTen,
        CCCD = @CCCD,
        SoDienThoai = @SoDienThoai,
        Email = @Email,
        NgaySinh = @NgaySinh,
        GioiTinh = @GioiTinh,
        QueQuan = @QueQuan,
        DiaChiThuongTru = @DiaChiThuongTru,
        MaPhong = @MaPhong,
        LaChuHo = @LaChuHo
    WHERE MaKhachThue = @MaKhachThue;
END;
GO

IF OBJECT_ID('sp_XoaMemKhachThue', 'P') IS NOT NULL DROP PROCEDURE sp_XoaMemKhachThue;
GO
CREATE PROCEDURE sp_XoaMemKhachThue
    @MaKhachThue INT
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE KhachThue
    SET IsActive = 0, MaPhong = NULL
    WHERE MaKhachThue = @MaKhachThue;
END;
GO

IF OBJECT_ID('sp_KiemTraTrungCCCD', 'P') IS NOT NULL DROP PROCEDURE sp_KiemTraTrungCCCD;
GO
CREATE PROCEDURE sp_KiemTraTrungCCCD
    @CCCD NVARCHAR(12),
    @ExcludeMaKhachThue INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) FROM KhachThue
    WHERE CCCD = @CCCD
      AND (@ExcludeMaKhachThue IS NULL OR MaKhachThue <> @ExcludeMaKhachThue);
END;
GO

IF OBJECT_ID('sp_TimKiemKhachThue', 'P') IS NOT NULL DROP PROCEDURE sp_TimKiemKhachThue;
GO
CREATE PROCEDURE sp_TimKiemKhachThue
    @Keyword NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT kt.MaKhachThue, kt.HoTen, kt.CCCD, kt.SoDienThoai, kt.Email, kt.NgaySinh, kt.GioiTinh,
           kt.QueQuan, kt.DiaChiThuongTru, kt.MaPhong, p.SoPhong, kt.LaChuHo, kt.IsActive, kt.NgayDangKy
    FROM KhachThue kt
    LEFT JOIN Phong p ON kt.MaPhong = p.MaPhong
    WHERE kt.IsActive = 1
      AND (@Keyword IS NULL 
           OR kt.HoTen LIKE N'%' + @Keyword + N'%'
           OR kt.CCCD LIKE N'%' + @Keyword + N'%'
           OR kt.SoDienThoai LIKE N'%' + @Keyword + N'%'
           OR p.SoPhong LIKE N'%' + @Keyword + N'%')
    ORDER BY kt.HoTen;
END;
GO

-- ==============================================================
-- 4. UC4: QUẢN LÝ HỢP ĐỒNG (Lê Ngọc Quyền)
-- ==============================================================

IF OBJECT_ID('sp_GetAllHopDong', 'P') IS NOT NULL DROP PROCEDURE sp_GetAllHopDong;
GO
CREATE PROCEDURE sp_GetAllHopDong
AS
BEGIN
    SET NOCOUNT ON;
    SELECT hd.MaHopDong, hd.MaPhong, p.SoPhong, hd.MaKhachThue, kt.HoTen AS TenKhachThue, kt.CCCD, kt.SoDienThoai,
           hd.NgayBatDau, hd.NgayKetThuc, hd.TienCoc, hd.GiaThueThucTe, hd.TrangThai, hd.GhiChu, hd.NgayLap
    FROM HopDong hd
    INNER JOIN Phong p ON hd.MaPhong = p.MaPhong
    INNER JOIN KhachThue kt ON hd.MaKhachThue = kt.MaKhachThue
    ORDER BY hd.NgayLap DESC;
END;
GO

IF OBJECT_ID('sp_ThemHopDong', 'P') IS NOT NULL DROP PROCEDURE sp_ThemHopDong;
GO
CREATE PROCEDURE sp_ThemHopDong
    @MaPhong INT,
    @MaKhachThue INT,
    @NgayBatDau DATE,
    @NgayKetThuc DATE,
    @TienCoc DECIMAL(18,0),
    @GiaThueThucTe DECIMAL(18,0),
    @GhiChu NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO HopDong (MaPhong, MaKhachThue, NgayBatDau, NgayKetThuc, TienCoc, GiaThueThucTe, TrangThai, GhiChu, NgayLap)
        VALUES (@MaPhong, @MaKhachThue, @NgayBatDau, @NgayKetThuc, @TienCoc, @GiaThueThucTe, N'HieuLuc', @GhiChu, GETDATE());

        DECLARE @NewId INT = SCOPE_IDENTITY();

        -- Gán phòng cho khách và cập nhật phòng thành ĐangThuê
        UPDATE KhachThue SET MaPhong = @MaPhong, LaChuHo = 1 WHERE MaKhachThue = @MaKhachThue;
        UPDATE Phong SET TrangThai = N'ĐangThuê' WHERE MaPhong = @MaPhong;

        COMMIT TRANSACTION;
        SELECT @NewId;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

IF OBJECT_ID('sp_GiaHanHopDong', 'P') IS NOT NULL DROP PROCEDURE sp_GiaHanHopDong;
GO
CREATE PROCEDURE sp_GiaHanHopDong
    @MaHopDong INT,
    @NgayKetThucMoi DATE,
    @GiaMoi DECIMAL(18,0) = NULL
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE HopDong
    SET NgayKetThuc = @NgayKetThucMoi,
        GiaThueThucTe = ISNULL(@GiaMoi, GiaThueThucTe),
        TrangThai = N'HieuLuc'
    WHERE MaHopDong = @MaHopDong;
END;
GO

IF OBJECT_ID('sp_ChamDutHopDong', 'P') IS NOT NULL DROP PROCEDURE sp_ChamDutHopDong;
GO
CREATE PROCEDURE sp_ChamDutHopDong
    @MaHopDong INT
AS
BEGIN
    SET NOCOUNT OFF;
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @MaPhong INT, @MaKhachThue INT;
        SELECT @MaPhong = MaPhong, @MaKhachThue = MaKhachThue FROM HopDong WHERE MaHopDong = @MaHopDong;

        UPDATE HopDong SET TrangThai = N'DaThanhLy' WHERE MaHopDong = @MaHopDong;
        UPDATE Phong SET TrangThai = N'Trống' WHERE MaPhong = @MaPhong;
        -- Gỡ liên kết phòng và hủy vai trò chủ hộ của toàn bộ cư dân trong phòng khi thanh lý hợp đồng
        UPDATE KhachThue SET MaPhong = NULL, LaChuHo = 0 WHERE MaPhong = @MaPhong;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

IF OBJECT_ID('sp_GetHopDongTheoPhong', 'P') IS NOT NULL DROP PROCEDURE sp_GetHopDongTheoPhong;
GO
CREATE PROCEDURE sp_GetHopDongTheoPhong
    @MaPhong INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT hd.MaHopDong, hd.MaPhong, p.SoPhong, hd.MaKhachThue, kt.HoTen AS TenKhachThue,
           hd.NgayBatDau, hd.NgayKetThuc, hd.TienCoc, hd.GiaThueThucTe, hd.TrangThai, hd.GhiChu, hd.NgayLap
    FROM HopDong hd
    INNER JOIN Phong p ON hd.MaPhong = p.MaPhong
    INNER JOIN KhachThue kt ON hd.MaKhachThue = kt.MaKhachThue
    WHERE hd.MaPhong = @MaPhong;
END;
GO

IF OBJECT_ID('sp_GetHopDongTheoKhach', 'P') IS NOT NULL DROP PROCEDURE sp_GetHopDongTheoKhach;
GO
CREATE PROCEDURE sp_GetHopDongTheoKhach
    @MaKhachThue INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT hd.MaHopDong, hd.MaPhong, p.SoPhong, hd.MaKhachThue, kt.HoTen AS TenKhachThue,
           hd.NgayBatDau, hd.NgayKetThuc, hd.TienCoc, hd.GiaThueThucTe, hd.TrangThai, hd.GhiChu, hd.NgayLap
    FROM HopDong hd
    INNER JOIN Phong p ON hd.MaPhong = p.MaPhong
    INNER JOIN KhachThue kt ON hd.MaKhachThue = kt.MaKhachThue
    WHERE hd.MaKhachThue = @MaKhachThue;
END;
GO

-- ==============================================================
-- 5. UC5: QUẢN LÝ ĐIỆN NƯỚC & DỊCH VỤ (Lê Ngọc Quyền)
-- ==============================================================

IF OBJECT_ID('sp_GetAllDichVu', 'P') IS NOT NULL DROP PROCEDURE sp_GetAllDichVu;
GO
CREATE PROCEDURE sp_GetAllDichVu
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaDichVu, TenDichVu, DonGia, DonViTinh, MoTa, IsActive
    FROM DichVu
    ORDER BY TenDichVu;
END;
GO

IF OBJECT_ID('sp_ThemDichVu', 'P') IS NOT NULL DROP PROCEDURE sp_ThemDichVu;
GO
CREATE PROCEDURE sp_ThemDichVu
    @TenDichVu NVARCHAR(100),
    @DonGia DECIMAL(18,0),
    @DonViTinh NVARCHAR(50),
    @MoTa NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO DichVu (TenDichVu, DonGia, DonViTinh, MoTa, IsActive)
    VALUES (@TenDichVu, @DonGia, @DonViTinh, @MoTa, 1);
    SELECT SCOPE_IDENTITY();
END;
GO

IF OBJECT_ID('sp_CapNhatDichVu', 'P') IS NOT NULL DROP PROCEDURE sp_CapNhatDichVu;
GO
CREATE PROCEDURE sp_CapNhatDichVu
    @MaDichVu INT,
    @TenDichVu NVARCHAR(100),
    @DonGia DECIMAL(18,0),
    @DonViTinh NVARCHAR(50),
    @MoTa NVARCHAR(500) = NULL,
    @IsActive BIT = 1
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE DichVu
    SET TenDichVu = @TenDichVu,
        DonGia = @DonGia,
        DonViTinh = @DonViTinh,
        MoTa = @MoTa,
        IsActive = @IsActive
    WHERE MaDichVu = @MaDichVu;
END;
GO

IF OBJECT_ID('sp_GetChiSoDienNuoc', 'P') IS NOT NULL DROP PROCEDURE sp_GetChiSoDienNuoc;
GO
CREATE PROCEDURE sp_GetChiSoDienNuoc
    @MaPhong INT = NULL,
    @Thang INT = NULL,
    @Nam INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT cs.MaChiSo, cs.MaPhong, p.SoPhong, cs.Thang, cs.Nam,
           cs.ChiSoDienCu, cs.ChiSoDienMoi, (cs.ChiSoDienMoi - cs.ChiSoDienCu) AS DienTieuThu,
           cs.ChiSoNuocCu, cs.ChiSoNuocMoi, (cs.ChiSoNuocMoi - cs.ChiSoNuocCu) AS NuocTieuThu,
           cs.NgayGhi, cs.NguoiGhi
    FROM ChiSoDienNuoc cs
    INNER JOIN Phong p ON cs.MaPhong = p.MaPhong
    WHERE (@MaPhong IS NULL OR cs.MaPhong = @MaPhong)
      AND (@Thang IS NULL OR cs.Thang = @Thang)
      AND (@Nam IS NULL OR cs.Nam = @Nam)
    ORDER BY cs.Nam DESC, cs.Thang DESC, p.SoPhong;
END;
GO

IF OBJECT_ID('sp_ThemChiSoDienNuoc', 'P') IS NOT NULL DROP PROCEDURE sp_ThemChiSoDienNuoc;
GO
CREATE PROCEDURE sp_ThemChiSoDienNuoc
    @MaPhong INT,
    @Thang INT,
    @Nam INT,
    @ChiSoDienCu INT,
    @ChiSoDienMoi INT,
    @ChiSoNuocCu INT,
    @ChiSoNuocMoi INT,
    @NguoiGhi NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO ChiSoDienNuoc (MaPhong, Thang, Nam, ChiSoDienCu, ChiSoDienMoi, ChiSoNuocCu, ChiSoNuocMoi, NgayGhi, NguoiGhi)
    VALUES (@MaPhong, @Thang, @Nam, @ChiSoDienCu, @ChiSoDienMoi, @ChiSoNuocCu, @ChiSoNuocMoi, GETDATE(), @NguoiGhi);
    SELECT SCOPE_IDENTITY();
END;
GO

IF OBJECT_ID('sp_CapNhatChiSoDienNuoc', 'P') IS NOT NULL DROP PROCEDURE sp_CapNhatChiSoDienNuoc;
GO
CREATE PROCEDURE sp_CapNhatChiSoDienNuoc
    @MaChiSo INT,
    @ChiSoDienCu INT,
    @ChiSoDienMoi INT,
    @ChiSoNuocCu INT,
    @ChiSoNuocMoi INT,
    @NguoiGhi NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE ChiSoDienNuoc
    SET ChiSoDienCu = @ChiSoDienCu,
        ChiSoDienMoi = @ChiSoDienMoi,
        ChiSoNuocCu = @ChiSoNuocCu,
        ChiSoNuocMoi = @ChiSoNuocMoi,
        NgayGhi = GETDATE(),
        NguoiGhi = ISNULL(@NguoiGhi, NguoiGhi)
    WHERE MaChiSo = @MaChiSo;
END;
GO

IF OBJECT_ID('sp_GetChiSoTheoPhongThang', 'P') IS NOT NULL DROP PROCEDURE sp_GetChiSoTheoPhongThang;
GO
CREATE PROCEDURE sp_GetChiSoTheoPhongThang
    @MaPhong INT,
    @Thang INT,
    @Nam INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 MaChiSo, MaPhong, Thang, Nam, ChiSoDienCu, ChiSoDienMoi, ChiSoNuocCu, ChiSoNuocMoi, NgayGhi, NguoiGhi
    FROM ChiSoDienNuoc
    WHERE MaPhong = @MaPhong AND Thang = @Thang AND Nam = @Nam;
END;
GO

-- ==============================================================
-- 6. UC6: HÓA ĐƠN & THANH TOÁN (Nguyễn Văn Tiến)
-- ==============================================================

IF OBJECT_ID('sp_GetAllHoaDon', 'P') IS NOT NULL DROP PROCEDURE sp_GetAllHoaDon;
GO
CREATE PROCEDURE sp_GetAllHoaDon
    @Thang INT = NULL,
    @Nam INT = NULL,
    @TrangThai NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT hd.MaHoaDon, hd.MaPhong, p.SoPhong, hd.Thang, hd.Nam,
           hd.TienPhong, hd.TienDien, hd.TienNuoc, hd.TienDichVu, hd.TongTien,
           hd.TrangThai, hd.NgayLap, hd.GhiChu,
           ISNULL((SELECT SUM(SoTienTra) FROM ThanhToan WHERE MaHoaDon = hd.MaHoaDon), 0) AS DaThanhToan,
           (hd.TongTien - ISNULL((SELECT SUM(SoTienTra) FROM ThanhToan WHERE MaHoaDon = hd.MaHoaDon), 0)) AS ConNo
    FROM HoaDon hd
    INNER JOIN Phong p ON hd.MaPhong = p.MaPhong
    WHERE (@Thang IS NULL OR hd.Thang = @Thang)
      AND (@Nam IS NULL OR hd.Nam = @Nam)
      AND (@TrangThai IS NULL OR hd.TrangThai = @TrangThai)
    ORDER BY hd.Nam DESC, hd.Thang DESC, p.SoPhong;
END;
GO

IF OBJECT_ID('sp_LapHoaDon', 'P') IS NOT NULL DROP PROCEDURE sp_LapHoaDon;
GO
CREATE PROCEDURE sp_LapHoaDon
    @MaPhong INT,
    @Thang INT,
    @Nam INT,
    @TienPhong DECIMAL(18,0),
    @TienDien DECIMAL(18,0),
    @TienNuoc DECIMAL(18,0),
    @TienDichVu DECIMAL(18,0),
    @TongTien DECIMAL(18,0),
    @GhiChu NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO HoaDon (MaPhong, Thang, Nam, TienPhong, TienDien, TienNuoc, TienDichVu, TongTien, TrangThai, NgayLap, GhiChu)
    VALUES (@MaPhong, @Thang, @Nam, @TienPhong, @TienDien, @TienNuoc, @TienDichVu, @TongTien, N'ChuaThanhToan', GETDATE(), @GhiChu);
    SELECT SCOPE_IDENTITY();
END;
GO

IF OBJECT_ID('sp_CapNhatTrangThaiHoaDon', 'P') IS NOT NULL DROP PROCEDURE sp_CapNhatTrangThaiHoaDon;
GO
CREATE PROCEDURE sp_CapNhatTrangThaiHoaDon
    @MaHoaDon INT,
    @TrangThai NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE HoaDon
    SET TrangThai = @TrangThai
    WHERE MaHoaDon = @MaHoaDon;
END;
GO

IF OBJECT_ID('sp_GetChiTietHoaDon', 'P') IS NOT NULL DROP PROCEDURE sp_GetChiTietHoaDon;
GO
CREATE PROCEDURE sp_GetChiTietHoaDon
    @MaHoaDon INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaChiTietHD, MaHoaDon, TenKhoanThu, SoLuong, DonGia, ThanhTien, GhiChu
    FROM ChiTietHoaDon
    WHERE MaHoaDon = @MaHoaDon;
END;
GO

IF OBJECT_ID('sp_ThemChiTietHoaDon', 'P') IS NOT NULL DROP PROCEDURE sp_ThemChiTietHoaDon;
GO
CREATE PROCEDURE sp_ThemChiTietHoaDon
    @MaHoaDon INT,
    @TenKhoanThu NVARCHAR(100),
    @SoLuong DECIMAL(18,2),
    @DonGia DECIMAL(18,0),
    @ThanhTien DECIMAL(18,0),
    @GhiChu NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO ChiTietHoaDon (MaHoaDon, TenKhoanThu, SoLuong, DonGia, ThanhTien, GhiChu)
    VALUES (@MaHoaDon, @TenKhoanThu, @SoLuong, @DonGia, @ThanhTien, @GhiChu);
    SELECT SCOPE_IDENTITY();
END;
GO

IF OBJECT_ID('sp_ThemThanhToan', 'P') IS NOT NULL DROP PROCEDURE sp_ThemThanhToan;
GO
CREATE PROCEDURE sp_ThemThanhToan
    @MaHoaDon INT,
    @SoTienTra DECIMAL(18,0),
    @HinhThucThanhToan NVARCHAR(20),
    @NguoiThu NVARCHAR(100),
    @GhiChu NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO ThanhToan (MaHoaDon, NgayThanhToan, SoTienTra, HinhThucThanhToan, NguoiThu, GhiChu)
        VALUES (@MaHoaDon, GETDATE(), @SoTienTra, @HinhThucThanhToan, @NguoiThu, @GhiChu);

        DECLARE @NewId INT = SCOPE_IDENTITY();

        -- Kiểm tra tổng thanh toán
        DECLARE @TongTien DECIMAL(18,0), @DaTra DECIMAL(18,0);
        SELECT @TongTien = TongTien FROM HoaDon WHERE MaHoaDon = @MaHoaDon;
        SELECT @DaTra = ISNULL(SUM(SoTienTra), 0) FROM ThanhToan WHERE MaHoaDon = @MaHoaDon;

        IF @DaTra >= @TongTien
            UPDATE HoaDon SET TrangThai = N'DaThanhToan' WHERE MaHoaDon = @MaHoaDon;

        COMMIT TRANSACTION;
        SELECT @NewId;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

IF OBJECT_ID('sp_GetThanhToanTheoHoaDon', 'P') IS NOT NULL DROP PROCEDURE sp_GetThanhToanTheoHoaDon;
GO
CREATE PROCEDURE sp_GetThanhToanTheoHoaDon
    @MaHoaDon INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaThanhToan, MaHoaDon, NgayThanhToan, SoTienTra, HinhThucThanhToan, NguoiThu, GhiChu
    FROM ThanhToan
    WHERE MaHoaDon = @MaHoaDon
    ORDER BY NgayThanhToan DESC;
END;
GO

IF OBJECT_ID('sp_GetHoaDonTheoPhong', 'P') IS NOT NULL DROP PROCEDURE sp_GetHoaDonTheoPhong;
GO
CREATE PROCEDURE sp_GetHoaDonTheoPhong
    @MaPhong INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT hd.MaHoaDon, hd.MaPhong, p.SoPhong, hd.Thang, hd.Nam,
           hd.TienPhong, hd.TienDien, hd.TienNuoc, hd.TienDichVu, hd.TongTien,
           hd.TrangThai, hd.NgayLap, hd.GhiChu,
           ISNULL((SELECT SUM(SoTienTra) FROM ThanhToan WHERE MaHoaDon = hd.MaHoaDon), 0) AS DaThanhToan,
           (hd.TongTien - ISNULL((SELECT SUM(SoTienTra) FROM ThanhToan WHERE MaHoaDon = hd.MaHoaDon), 0)) AS ConNo
    FROM HoaDon hd
    INNER JOIN Phong p ON hd.MaPhong = p.MaPhong
    WHERE hd.MaPhong = @MaPhong
    ORDER BY hd.Nam DESC, hd.Thang DESC;
END;
GO

-- ==============================================================
-- 7. UC7: YÊU CẦU & THÔNG BÁO (Nguyễn Văn Tiến)
-- ==============================================================

IF OBJECT_ID('sp_GetAllYeuCau', 'P') IS NOT NULL DROP PROCEDURE sp_GetAllYeuCau;
GO
CREATE PROCEDURE sp_GetAllYeuCau
    @TrangThai NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT yc.MaYeuCau, yc.MaPhong, p.SoPhong, yc.MaKhachThue, kt.HoTen AS TenKhachThue,
           yc.LoaiYeuCau, yc.TieuDe, yc.NoiDung, yc.NgayGui, yc.TrangThai, yc.KetQuaXuLy, yc.DanhGiaSao, yc.NgayXuLy
    FROM YeuCau yc
    LEFT JOIN Phong p ON yc.MaPhong = p.MaPhong
    LEFT JOIN KhachThue kt ON yc.MaKhachThue = kt.MaKhachThue
    WHERE (@TrangThai IS NULL OR yc.TrangThai = @TrangThai)
    ORDER BY yc.NgayGui DESC;
END;
GO

IF OBJECT_ID('sp_ThemYeuCau', 'P') IS NOT NULL DROP PROCEDURE sp_ThemYeuCau;
GO
CREATE PROCEDURE sp_ThemYeuCau
    @MaPhong INT,
    @MaKhachThue INT,
    @LoaiYeuCau NVARCHAR(20),
    @TieuDe NVARCHAR(200),
    @NoiDung NVARCHAR(2000)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO YeuCau (MaPhong, MaKhachThue, LoaiYeuCau, TieuDe, NoiDung, NgayGui, TrangThai)
    VALUES (@MaPhong, @MaKhachThue, @LoaiYeuCau, @TieuDe, @NoiDung, GETDATE(), N'MoiGui');
    SELECT SCOPE_IDENTITY();
END;
GO

IF OBJECT_ID('sp_CapNhatTrangThaiYeuCau', 'P') IS NOT NULL DROP PROCEDURE sp_CapNhatTrangThaiYeuCau;
GO
CREATE PROCEDURE sp_CapNhatTrangThaiYeuCau
    @MaYeuCau INT,
    @TrangThai NVARCHAR(20),
    @KetQuaXuLy NVARCHAR(2000) = NULL
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE YeuCau
    SET TrangThai = @TrangThai,
        KetQuaXuLy = ISNULL(@KetQuaXuLy, KetQuaXuLy),
        NgayXuLy = GETDATE()
    WHERE MaYeuCau = @MaYeuCau;
END;
GO

IF OBJECT_ID('sp_DanhGiaYeuCau', 'P') IS NOT NULL DROP PROCEDURE sp_DanhGiaYeuCau;
GO
CREATE PROCEDURE sp_DanhGiaYeuCau
    @MaYeuCau INT,
    @DanhGiaSao INT
AS
BEGIN
    SET NOCOUNT OFF;
    UPDATE YeuCau
    SET DanhGiaSao = @DanhGiaSao
    WHERE MaYeuCau = @MaYeuCau;
END;
GO

IF OBJECT_ID('sp_GetYeuCauTheoPhong', 'P') IS NOT NULL DROP PROCEDURE sp_GetYeuCauTheoPhong;
GO
CREATE PROCEDURE sp_GetYeuCauTheoPhong
    @MaPhong INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT yc.MaYeuCau, yc.MaPhong, p.SoPhong, yc.MaKhachThue, kt.HoTen AS TenKhachThue,
           yc.LoaiYeuCau, yc.TieuDe, yc.NoiDung, yc.NgayGui, yc.TrangThai, yc.KetQuaXuLy, yc.DanhGiaSao, yc.NgayXuLy
    FROM YeuCau yc
    LEFT JOIN Phong p ON yc.MaPhong = p.MaPhong
    LEFT JOIN KhachThue kt ON yc.MaKhachThue = kt.MaKhachThue
    WHERE yc.MaPhong = @MaPhong
    ORDER BY yc.NgayGui DESC;
END;
GO

IF OBJECT_ID('sp_GetAllThongBao', 'P') IS NOT NULL DROP PROCEDURE sp_GetAllThongBao;
GO
CREATE PROCEDURE sp_GetAllThongBao
AS
BEGIN
    SET NOCOUNT ON;
    SELECT tb.MaThongBao, tb.TieuDe, tb.NoiDung, tb.NgayDang, tb.NguoiDang, tb.LoaiThongBao, tb.MaPhong, p.SoPhong, tb.IsActive
    FROM ThongBao tb
    LEFT JOIN Phong p ON tb.MaPhong = p.MaPhong
    WHERE tb.IsActive = 1
    ORDER BY tb.NgayDang DESC;
END;
GO

IF OBJECT_ID('sp_ThemThongBao', 'P') IS NOT NULL DROP PROCEDURE sp_ThemThongBao;
GO
CREATE PROCEDURE sp_ThemThongBao
    @TieuDe NVARCHAR(200),
    @NoiDung NVARCHAR(2000),
    @NguoiDang NVARCHAR(100),
    @LoaiThongBao NVARCHAR(20) = N'ToanToaNha',
    @MaPhong INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO ThongBao (TieuDe, NoiDung, NgayDang, NguoiDang, LoaiThongBao, MaPhong, IsActive)
    VALUES (@TieuDe, @NoiDung, GETDATE(), @NguoiDang, @LoaiThongBao, @MaPhong, 1);
    SELECT SCOPE_IDENTITY();
END;
GO

IF OBJECT_ID('sp_GetThongBaoTheoPhong', 'P') IS NOT NULL DROP PROCEDURE sp_GetThongBaoTheoPhong;
GO
CREATE PROCEDURE sp_GetThongBaoTheoPhong
    @MaPhong INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT tb.MaThongBao, tb.TieuDe, tb.NoiDung, tb.NgayDang, tb.NguoiDang, tb.LoaiThongBao, tb.MaPhong, p.SoPhong, tb.IsActive
    FROM ThongBao tb
    LEFT JOIN Phong p ON tb.MaPhong = p.MaPhong
    WHERE tb.IsActive = 1
      AND (tb.LoaiThongBao = N'ToanToaNha' OR (@MaPhong IS NOT NULL AND tb.MaPhong = @MaPhong))
    ORDER BY tb.NgayDang DESC;
END;
GO

-- ==============================================================
-- 8. UC8: BÁO CÁO & THỐNG KÊ (Nguyễn Văn Tiến)
-- ==============================================================

IF OBJECT_ID('sp_BaoCaoDoanhThu', 'P') IS NOT NULL DROP PROCEDURE sp_BaoCaoDoanhThu;
GO
CREATE PROCEDURE sp_BaoCaoDoanhThu
    @Thang INT,
    @Nam INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        COUNT(hd.MaHoaDon) AS TongSoHoaDon,
        ISNULL(SUM(hd.TienPhong), 0) AS TongTienPhong,
        ISNULL(SUM(hd.TienDien), 0) AS TongTienDien,
        ISNULL(SUM(hd.TienNuoc), 0) AS TongTienNuoc,
        ISNULL(SUM(hd.TienDichVu), 0) AS TongTienDichVu,
        ISNULL(SUM(hd.TongTien), 0) AS TongDoanhThu,
        ISNULL((SELECT SUM(tt.SoTienTra) FROM ThanhToan tt INNER JOIN HoaDon h ON tt.MaHoaDon = h.MaHoaDon WHERE h.Thang = @Thang AND h.Nam = @Nam), 0) AS DaThu,
        (ISNULL(SUM(hd.TongTien), 0) - ISNULL((SELECT SUM(tt.SoTienTra) FROM ThanhToan tt INNER JOIN HoaDon h ON tt.MaHoaDon = h.MaHoaDon WHERE h.Thang = @Thang AND h.Nam = @Nam), 0)) AS ConNo
    FROM HoaDon hd
    WHERE hd.Thang = @Thang AND hd.Nam = @Nam;
END;
GO

IF OBJECT_ID('sp_BaoCaoTinhTrangPhong', 'P') IS NOT NULL DROP PROCEDURE sp_BaoCaoTinhTrangPhong;
GO
CREATE PROCEDURE sp_BaoCaoTinhTrangPhong
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TrangThai, COUNT(*) AS SoLuong,
           CAST(COUNT(*) * 100.0 / NULLIF((SELECT COUNT(*) FROM Phong), 0) AS DECIMAL(5,2)) AS TyLePhanTram
    FROM Phong
    GROUP BY TrangThai;
END;
GO

IF OBJECT_ID('sp_BaoCaoCongNo', 'P') IS NOT NULL DROP PROCEDURE sp_BaoCaoCongNo;
GO
CREATE PROCEDURE sp_BaoCaoCongNo
    @Thang INT = NULL,
    @Nam INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT hd.MaHoaDon, hd.MaPhong, p.SoPhong, hd.Thang, hd.Nam, hd.TongTien,
           ISNULL((SELECT SUM(SoTienTra) FROM ThanhToan WHERE MaHoaDon = hd.MaHoaDon), 0) AS DaThanhToan,
           (hd.TongTien - ISNULL((SELECT SUM(SoTienTra) FROM ThanhToan WHERE MaHoaDon = hd.MaHoaDon), 0)) AS ConNo,
           kt.HoTen AS ChuHo, kt.SoDienThoai, hd.NgayLap,
           CASE 
               WHEN DATEDIFF(DAY, hd.NgayLap, GETDATE()) <= 5 THEN N'Trong hạn'
               WHEN DATEDIFF(DAY, hd.NgayLap, GETDATE()) BETWEEN 6 AND 20 THEN N'Quá hạn 1-15 ngày'
               ELSE N'Quá hạn >15 ngày'
           END AS PhanLoaiNo
    FROM HoaDon hd
    INNER JOIN Phong p ON hd.MaPhong = p.MaPhong
    LEFT JOIN KhachThue kt ON p.MaPhong = kt.MaPhong AND kt.LaChuHo = 1 AND kt.IsActive = 1
    WHERE hd.TrangThai <> N'DaThanhToan'
      AND (@Thang IS NULL OR hd.Thang = @Thang)
      AND (@Nam IS NULL OR hd.Nam = @Nam)
    ORDER BY ConNo DESC;
END;
GO

IF OBJECT_ID('sp_BaoCaoKhachThue', 'P') IS NOT NULL DROP PROCEDURE sp_BaoCaoKhachThue;
GO
CREATE PROCEDURE sp_BaoCaoKhachThue
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        (SELECT COUNT(*) FROM KhachThue WHERE IsActive = 1) AS TongKhachThue,
        (SELECT COUNT(*) FROM KhachThue WHERE IsActive = 1 AND LaChuHo = 1) AS TongChuHo,
        (SELECT COUNT(*) FROM KhachThue WHERE IsActive = 1 AND GioiTinh = N'Nam') AS SoNam,
        (SELECT COUNT(*) FROM KhachThue WHERE IsActive = 1 AND GioiTinh = N'Nữ') AS SoNu,
        (SELECT COUNT(*) FROM KhachThue WHERE IsActive = 1 AND DATEDIFF(DAY, NgayDangKy, GETDATE()) <= 30) AS KhachMoiThangNay;
END;
GO

IF OBJECT_ID('sp_ThongKeDoanhThuTheoThang', 'P') IS NOT NULL DROP PROCEDURE sp_ThongKeDoanhThuTheoThang;
GO
CREATE PROCEDURE sp_ThongKeDoanhThuTheoThang
    @Nam INT
AS
BEGIN
    SET NOCOUNT ON;
    WITH ThangList AS (
        SELECT 1 AS Thang UNION SELECT 2 UNION SELECT 3 UNION SELECT 4
        UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8
        UNION SELECT 9 UNION SELECT 10 UNION SELECT 11 UNION SELECT 12
    )
    SELECT tl.Thang,
           ISNULL(SUM(hd.TongTien), 0) AS TongDoanhThu,
           ISNULL(SUM(hd.TienPhong), 0) AS TienPhong,
           ISNULL(SUM(hd.TienDien), 0) AS TienDien,
           ISNULL(SUM(hd.TienNuoc), 0) AS TienNuoc,
           ISNULL(SUM(hd.TienDichVu), 0) AS TienDichVu
    FROM ThangList tl
    LEFT JOIN HoaDon hd ON tl.Thang = hd.Thang AND hd.Nam = @Nam
    GROUP BY tl.Thang
    ORDER BY tl.Thang;
END;
GO

PRINT N'✅ Đã khởi tạo hoàn tất toàn bộ Stored Procedures cho 8 Use Cases!';
GO

-- ==============================================================
-- PHAN 5: NAP DU LIEU MAU CHUAN (30 CU DAN, TIENG VIET KHONG DAU 100%)
-- ==============================================================

-- 1. TOA NHA
INSERT INTO ToaNha (TenToaNha, DiaChi, SoTang, MoTa) VALUES 
('Toa A - Hoa Sen', '235 Hoang Quoc Viet, Co Nhue, Bac Tu Liem, Ha Noi', 10, 'Toa can ho cao cap gom 5 tang mau voi 25 can ho tien nghi'),
('Toa B - Huong Duong', '235 Hoang Quoc Viet, Co Nhue, Bac Tu Liem, Ha Noi', 10, 'Toa can ho dich vu cao cap gom 5 tang mau voi 25 can ho studio va 2PN');
GO

-- 2. DICH VU
INSERT INTO DichVu (TenDichVu, DonGia, DonViTinh, MoTa, IsActive) VALUES 
('Phi Quan ly & Ve sinh chung', 150000, 'dong/thang', 'Thu don rac, ve sinh hanh lang, bao an 24/7', 1),
('Phi Bao tri Thang may', 80000, 'dong/phong', 'Van hanh va bao duong ky thuat thang may dinh ky', 1),
('Gui xe may', 100000, 'dong/xe/thang', 'Trong giu xe may tang ham the tu an ninh', 1),
('Gui xe o to', 1200000, 'dong/xe/thang', 'Bai do xe o to tang ham B1 co camera giam sat', 1),
('Internet Cap quang toc do cao', 220000, 'dong/thang', 'Goi cap quang Viettel 150Mbps rieng tung phong', 1);
GO

-- 3. 50 PHONG (25 phong Toa A, 25 phong Toa B)
-- Toa A (MaToaNha = 1): A101 - A505
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

-- Toa B (MaToaNha = 2): B101 - B505
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
GO

-- 4. THIET BI PHONG MAU
INSERT INTO ThietBiPhong (MaPhong, TenThietBi, SoLuong, TinhTrang, NgayLapDat, GhiChu) VALUES
(1, 'Dieu hoa Panasonic 12000BTU Inverter', 1, 'Tot', '2026-01-15', 'Bao hanh den 2027'),
(1, 'Binh nong lanh Ariston 20L', 1, 'Tot', '2026-01-15', 'Da kiem tra chong giat'),
(1, 'Tu lanh Sharp 180L', 1, 'Tot', '2026-01-16', 'Noi that ban giao'),
(1, 'Giuong ngu go soi 1.8x2m', 1, 'Tot', '2026-01-16', 'Kem dem cao su'),
(2, 'Dieu hoa Daikin 9000BTU', 2, 'Tot', '2026-02-10', '2 phong ngu'),
(2, 'May giat Electrolux 8kg', 1, 'Tot', '2026-02-10', 'Cua ngang cao cap'),
(2, 'Bep tu doi Sunhouse', 1, 'Tot', '2026-02-11', 'Lap am ban da');
GO

-- 5. DUNG 30 KHACH THUE (CU DAN THUC TE, TIENG VIET KHONG DAU 100%)
INSERT INTO KhachThue (HoTen, CCCD, SoDienThoai, Email, NgaySinh, GioiTinh, QueQuan, DiaChiThuongTru, MaPhong, LaChuHo, IsActive, NgayDangKy) VALUES
-- Toa A:
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

-- Toa B:
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
GO

-- 6. 21 HOP DONG THUE PHONG CON HIEU LUC (CHU HO)
INSERT INTO HopDong (MaPhong, MaKhachThue, NgayBatDau, NgayKetThuc, TienCoc, GiaThueThucTe, TrangThai, GhiChu, NgayLap) VALUES
-- Toa A
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

-- Toa B
(26, 19, '2026-01-10', '2027-01-10', 5000000, 5000000, 'HieuLuc', 'Can B101', '2026-01-10'),
(27, 20, '2026-01-20', '2027-01-20', 6500000, 6500000, 'HieuLuc', 'Can B102', '2026-01-20'),
(29, 22, '2026-02-01', '2027-02-01', 6500000, 6500000, 'HieuLuc', 'Can B104', '2026-02-01'),
(30, 24, '2026-02-15', '2027-02-15', 8000000, 8000000, 'HieuLuc', 'Can B105 VIP', '2026-02-15'),
(31, 25, '2026-03-01', '2027-03-01', 5200000, 5200000, 'HieuLuc', 'Can B201', '2026-03-01'),
(32, 27, '2026-03-15', '2027-03-15', 6600000, 6600000, 'HieuLuc', 'Can B202', '2026-03-15'),
(33, 28, '2026-04-01', '2027-04-01', 5200000, 5200000, 'HieuLuc', 'Can B203', '2026-04-01'),
(35, 29, '2026-04-20', '2027-04-20', 8200000, 8200000, 'HieuLuc', 'Can B205 VIP', '2026-04-20'),
(36, 30, '2026-05-01', '2027-05-01', 5200000, 5200000, 'HieuLuc', 'Can B301', '2026-05-01');
GO

-- 7. CHI SO DIEN NUOC (CAC THANG GAN NHAT 2026)
INSERT INTO ChiSoDienNuoc (MaPhong, Thang, Nam, ChiSoDienCu, ChiSoDienMoi, ChiSoNuocCu, ChiSoNuocMoi, NgayGhi, NguoiGhi) VALUES
-- Thang 6/2026
(1, 6, 2026, 120, 245, 15, 23, '2026-06-30', 'Le Ngoc Quyen'),
(2, 6, 2026, 210, 380, 25, 38, '2026-06-30', 'Le Ngoc Quyen'),
(4, 6, 2026, 150, 275, 18, 26, '2026-06-30', 'Le Ngoc Quyen'),
(5, 6, 2026, 300, 510, 30, 48, '2026-06-30', 'Le Ngoc Quyen'),
(6, 6, 2026, 100, 215, 12, 19, '2026-06-30', 'Le Ngoc Quyen'),

-- Thang 7/2026
(1, 7, 2026, 245, 378, 23, 32, '2026-07-30', 'Le Ngoc Quyen'),
(2, 7, 2026, 380, 560, 38, 52, '2026-07-30', 'Le Ngoc Quyen'),
(4, 7, 2026, 275, 410, 26, 35, '2026-07-30', 'Le Ngoc Quyen'),
(5, 7, 2026, 510, 735, 48, 67, '2026-07-30', 'Le Ngoc Quyen'),
(6, 7, 2026, 215, 335, 19, 27, '2026-07-30', 'Le Ngoc Quyen'),

-- Thang 8/2026
(1, 8, 2026, 378, 498, 32, 40, '2026-08-30', 'Le Ngoc Quyen'),
(2, 8, 2026, 560, 725, 52, 65, '2026-08-30', 'Le Ngoc Quyen'),
(4, 8, 2026, 410, 530, 35, 43, '2026-08-30', 'Le Ngoc Quyen'),
(5, 8, 2026, 735, 940, 67, 85, '2026-08-30', 'Le Ngoc Quyen'),
(6, 8, 2026, 335, 445, 27, 34, '2026-08-30', 'Le Ngoc Quyen'),

-- Thang 9/2026
(1, 9, 2026, 498, 610, 40, 48, '2026-09-29', 'Le Ngoc Quyen'),
(2, 9, 2026, 725, 880, 65, 77, '2026-09-29', 'Le Ngoc Quyen'),
(4, 9, 2026, 530, 642, 43, 50, '2026-09-29', 'Le Ngoc Quyen'),
(5, 9, 2026, 940, 1135, 85, 102, '2026-09-29', 'Le Ngoc Quyen'),
(6, 9, 2026, 445, 550, 34, 41, '2026-09-29', 'Le Ngoc Quyen'),

-- Thang 10/2026
(1, 10, 2026, 610, 730, 48, 56, '2026-10-30', 'Le Ngoc Quyen'),
(2, 10, 2026, 880, 1045, 77, 90, '2026-10-30', 'Le Ngoc Quyen'),
(4, 10, 2026, 642, 760, 50, 58, '2026-10-30', 'Le Ngoc Quyen'),
(5, 10, 2026, 1135, 1340, 102, 120, '2026-10-30', 'Le Ngoc Quyen'),
(6, 10, 2026, 550, 660, 41, 48, '2026-10-30', 'Le Ngoc Quyen'),

-- Thang 11/2026
(1, 11, 2026, 730, 855, 56, 65, '2026-11-20', 'Le Ngoc Quyen'),
(2, 11, 2026, 1045, 1215, 90, 104, '2026-11-20', 'Le Ngoc Quyen'),
(4, 11, 2026, 760, 880, 58, 66, '2026-11-20', 'Le Ngoc Quyen'),
(5, 11, 2026, 1340, 1555, 120, 139, '2026-11-20', 'Le Ngoc Quyen'),
(6, 11, 2026, 660, 775, 48, 55, '2026-11-20', 'Le Ngoc Quyen');
GO

-- 8. HOA DON & CHI TIET HOA DON
-- Thang 10/2026
INSERT INTO HoaDon (MaPhong, Thang, Nam, TienPhong, TienDien, TienNuoc, TienDichVu, TongTien, TrangThai, NgayLap, GhiChu) VALUES
(1, 10, 2026, 5500000, 275000, 120000, 450000, 6345000, 'DaThanhToan', '2026-10-31', 'Hoa don thang 10/2026 phong A101'),
(2, 10, 2026, 7000000, 395000, 195000, 450000, 8040000, 'DaThanhToan', '2026-10-31', 'Hoa don thang 10/2026 phong A102'),
(4, 10, 2026, 5500000, 268000, 120000, 450000, 6338000, 'DaThanhToan', '2026-10-31', 'Hoa don thang 10/2026 phong A104'),
(5, 10, 2026, 8500000, 520000, 270000, 450000, 9740000, 'DaThanhToan', '2026-10-31', 'Hoa don thang 10/2026 phong A105'),
(6, 10, 2026, 5500000, 245000, 105000, 450000, 6300000, 'DaThanhToan', '2026-10-31', 'Hoa don thang 10/2026 phong A201'),

-- Thang 11/2026
(1, 11, 2026, 5500000, 249225, 135000, 450000, 6334225, 'DaThanhToan', '2026-11-20', 'Hoa don thang 11/2026 phong A101'),
(2, 11, 2026, 7000000, 351420, 210000, 450000, 8011420, 'ChuaThanhToan', '2026-11-20', 'Hoa don thang 11/2026 phong A102 - Con no'),
(4, 11, 2026, 5500000, 237870, 120000, 450000, 6307870, 'DaThanhToan', '2026-11-20', 'Hoa don thang 11/2026 phong A104'),
(5, 11, 2026, 8500000, 462450, 285000, 450000, 9697450, 'ChuaThanhToan', '2026-11-20', 'Hoa don thang 11/2026 phong A105 - Con no'),
(6, 11, 2026, 5500000, 226515, 105000, 450000, 6281515, 'QuaHan', '2026-11-01', 'Hoa don thang 11/2026 phong A201 - Qua han');
GO

-- Chi tiet hoa don
INSERT INTO ChiTietHoaDon (MaHoaDon, TenKhoanThu, SoLuong, DonGia, ThanhTien, GhiChu) VALUES
(6, 'Tien thue phong A101 thang 11/2026', 1, 5500000, 5500000, 'Theo hop dong'),
(6, 'Tien dien (125 kWh)', 125, 1994, 249225, 'EVN 6 bac thang luy tien'),
(6, 'Tien nuoc sach (9 m3)', 9, 15000, 135000, '15.000 d/m3'),
(6, 'Phi Quan ly & Ve sinh chung', 1, 150000, 150000, 'Co dinh hang thang'),
(6, 'Phi Bao tri Thang may', 1, 80000, 80000, 'Co dinh hang thang'),
(6, 'Internet Cap quang toc do cao', 1, 220000, 220000, 'Goi toc do cao');
GO

-- 9. THANH TOAN
INSERT INTO ThanhToan (MaHoaDon, NgayThanhToan, SoTienTra, HinhThucThanhToan, NguoiThu, GhiChu) VALUES
(1, '2026-11-02', 6345000, 'ChuyenKhoan', 'Nguyen Thanh Nam', 'Chuyen khoan Vietcombank'),
(2, '2026-11-02', 8040000, 'ChuyenKhoan', 'Nguyen Thanh Nam', 'Chuyen khoan MBBank'),
(3, '2026-11-03', 6338000, 'TienMat', 'Phan Cong Thanh Dat', 'Nop tien mat tai phong quan ly'),
(4, '2026-11-04', 9740000, 'ChuyenKhoan', 'Nguyen Thanh Nam', 'Chuyen khoan Techcombank'),
(5, '2026-11-05', 6300000, 'TienMat', 'Phan Cong Thanh Dat', 'Tien mat'),
(6, '2026-11-21', 6334225, 'ChuyenKhoan', 'Nguyen Thanh Nam', 'Chuyen khoan BIDV phong A101'),
(8, '2026-11-21', 6307870, 'ChuyenKhoan', 'Nguyen Thanh Nam', 'Chuyen khoan Vietcombank phong A104');
GO

-- 10. TAI KHOAN HE THONG (admin123, quanly123, khach123)
INSERT INTO TaiKhoan (TenDangNhap, MatKhauHash, SaltHash, HoTen, QuyenHan, SoDienThoai, Email, IsActive, MaKhachThue, SoLanDangNhapSai) VALUES
('admin', '6htNRE6rHgHe8pMAEYAcmT2cITQGwXDVNcP+J/FocmI=', 'KioqKioqKioqKioqKioqKg==', 'Nguyen Thanh Nam (Chu nha)', 'ChuNha', '0901234567', 'admin.ancu@gmail.com', 1, NULL, 0),
('quanly01', 'a5VnN9OQKX2DlKEhuB9bbW6YYBMW/5ef1enJIrYO3uE=', 'KysrKysrKysrKysrKysrKw==', 'Phan Cong Thanh Dat (Quan ly)', 'QuanLy', '0987654321', 'dat.quanly@gmail.com', 1, NULL, 0),
('khachthue01', 'TLIxrShaMakmNl+DMTWRCK7A0OXn2hEUJt9GucXanVw=', 'LCwsLCwsLCwsLCwsLCwsLA==', 'Nguyen Van An (Cu dan A101)', 'KhachThue', '0912345678', 'an.nguyen@gmail.com', 1, 1, 0);
GO

-- 11. YEU CAU & BAO HONG
INSERT INTO YeuCau (MaPhong, MaKhachThue, LoaiYeuCau, TieuDe, NoiDung, NgayGui, TrangThai, KetQuaXuLy, DanhGiaSao, NgayXuLy) VALUES
(1, 1, 'BaoHong', 'Voi sen nha tam bi ro ri nuoc', 'Voi hoa sen tai phong tam master bi ro nuoc o chan van, nho BQL kiem tra thay gioang cao su', '2026-11-10 09:30:00', 'HoanThanh', 'Ky thuat vien da thay gioang cao su va siet lai van nuoc luc 14:00', 5, '2026-11-10 14:30:00'),
(2, 3, 'BaoHong', 'Dieu hoa phong ngu nho khong mat', 'Dieu hoa Daikin bat 16 do nhung chi co gio thoang, nghi ngo het gas', '2026-11-12 11:20:00', 'HoanThanh', 'Da nap bo sung gas R32 va ve sinh luoi loc', 5, '2026-11-13 10:00:00'),
(5, 6, 'YeuCauKhac', 'Dang ky them the xe may thu hai', 'Phong co them thanh vien chuyen vao o can dang ky 01 the tu xe may moi bien so 29E1-88888', '2026-11-18 16:45:00', 'DangXuLy', 'BQL dang in the tu moi, hen nhan vao thu 2 tuan toi', NULL, '2026-11-19 08:30:00'),
(6, 8, 'BaoHong', 'Den chieu sang ban cong chap chon', 'Bong den LED ban cong bat len nhap nhay lien tuc roi tat han', '2026-11-21 19:15:00', 'MoiGui', NULL, NULL, NULL);
GO

-- 12. THONG BAO TOA NHA
INSERT INTO ThongBao (TieuDe, NoiDung, NgayDang, NguoiDang, LoaiThongBao, MaPhong, IsActive) VALUES
('Thong bao: Lich kiem tra he thong PCCC dinh ky Quy 4/2026', 'Ban Quan ly chung cu An Cu xin thong bao toi toan the cu dan: Vao hoi 09:00 - 11:30 ngay 25/11/2026, BQL se phoi hop voi Canh sat PCCC quan Bac Tu Liem kiem tra chuong bao chay, voi phun Sprinkler tai cac hanh lang tang 1 den tang 5. Trong thoi gian kiem tra, chuong bao dong co the vang len ngat quang, kinh mong Quy cu dan khong hoang mang.', '2026-11-15 08:00:00', 'Ban Quan ly An Cu', 'ToanToaNha', NULL, 1),
('Thong bao: Tam ngung cap nuoc sinh hoat de suc rua be ngam', 'De dam bao chat luong nguon nuoc sach sinh hoat, BQL se tien hanh suc rua be nuoc ngam va be mai toa A tu 23:00 ngay 28/11/2026 den 04:00 sang ngay 29/11/2026. Kinh de nghi cu dan chu dong tich tru nuoc su dung trong khung gio tren.', '2026-11-18 10:00:00', 'Ban Quan ly An Cu', 'ToanToaNha', NULL, 1),
('Thong bao rieng: Nhac lich nop phi thue phong va dich vu thang 11/2026', 'Hoa don tien nha va dich vu thang 11/2026 cua phong A102 hien chua hoan tat thanh toan. De nghi chu ho lien he BQL hoac chuyen khoan qua tai khoan ngan hang de hoan tat truoc ngay 30/11/2026.', '2026-11-21 14:00:00', 'Ke toan An Cu', 'TheoPhong', 2, 1);
GO

-- THONG BAO HOAN TAT
PRINT N'==============================================================';
PRINT N'HOAN TAT! CSDL QuanLyChungCuAnCu DA DUOC TAO MOI VA NAP THANH CONG!';
PRINT N'- 2 Toa nha, 50 Can ho (A101-A505, B101-B505)';
PRINT N'- 30 Cu dan (Khach thue) Tieng Viet khong dau 100%, khong loi font';
PRINT N'- 21 Hop dong thue con hieu luc';
PRINT N'- 5 Goi dich vu co ban';
PRINT N'- Chi so dien nuoc, Hoa don & Thanh toan tieu bieu';
PRINT N'- 3 Tai khoan: admin / quanly01 / khachthue01';
PRINT N'==============================================================';
GO
