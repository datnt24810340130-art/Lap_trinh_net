IF DB_ID(N'GarageDB') IS NULL
BEGIN
	CREATE DATABASE GarageDB;
END;
GO

USE GarageDB;
GO

IF OBJECT_ID(N'dbo.KhachHang', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.KhachHang
	(
		MaKH int IDENTITY(1,1) NOT NULL CONSTRAINT PK_KhachHang PRIMARY KEY,
		TenKH nvarchar(100) NOT NULL,
		DienThoai varchar(15) NULL,
		DiaChi nvarchar(200) NULL
	);
END;
GO

IF OBJECT_ID(N'dbo.Xe', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.Xe
	(
		BienSo varchar(20) NOT NULL CONSTRAINT PK_Xe PRIMARY KEY,
		MaKH int NOT NULL,
		HieuXe nvarchar(50) NULL,
		NamSanXuat int NULL,
		CONSTRAINT FK_Xe_KhachHang FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH)
	);
END;
GO

IF OBJECT_ID(N'dbo.PhuTung', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.PhuTung
	(
		MaPhuTung int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhuTung PRIMARY KEY,
		TenPhuTung nvarchar(150) NOT NULL,
		DonGia decimal(18,2) NOT NULL CONSTRAINT DF_PhuTung_DonGia DEFAULT (0),
		SoLuongTon int NOT NULL CONSTRAINT DF_PhuTung_SoLuongTon DEFAULT (0),
		CONSTRAINT CK_PhuTung_DonGia CHECK (DonGia >= 0),
		CONSTRAINT CK_PhuTung_SoLuongTon CHECK (SoLuongTon >= 0)
	);
END;
GO

IF OBJECT_ID(N'dbo.PhieuSuaChua', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.PhieuSuaChua
	(
		MaPhieu int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuSuaChua PRIMARY KEY,
		BienSo varchar(20) NOT NULL,
		NgayNhan datetime NOT NULL CONSTRAINT DF_PhieuSuaChua_NgayNhan DEFAULT (GETDATE()),
		NoiDungSua nvarchar(max) NULL,
		TongTien decimal(18,2) NOT NULL CONSTRAINT DF_PhieuSuaChua_TongTien DEFAULT (0),
		TrangThai nvarchar(30) NOT NULL CONSTRAINT DF_PhieuSuaChua_TrangThai DEFAULT (N'Đang sửa'),
		CONSTRAINT FK_PhieuSuaChua_Xe FOREIGN KEY (BienSo) REFERENCES dbo.Xe(BienSo),
		CONSTRAINT CK_PhieuSuaChua_TongTien CHECK (TongTien >= 0)
	);
END;
GO

IF OBJECT_ID(N'dbo.ChiTietSuaChua', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.ChiTietSuaChua
	(
		MaPhieu int NOT NULL,
		MaPhuTung int NOT NULL,
		SoLuong int NOT NULL,
		TienCong decimal(18,2) NOT NULL CONSTRAINT DF_ChiTietSuaChua_TienCong DEFAULT (0),
		CONSTRAINT PK_ChiTietSuaChua PRIMARY KEY (MaPhieu, MaPhuTung),
		CONSTRAINT FK_ChiTietSuaChua_Phieu FOREIGN KEY (MaPhieu) REFERENCES dbo.PhieuSuaChua(MaPhieu),
		CONSTRAINT FK_ChiTietSuaChua_PhuTung FOREIGN KEY (MaPhuTung) REFERENCES dbo.PhuTung(MaPhuTung),
		CONSTRAINT CK_ChiTietSuaChua_SoLuong CHECK (SoLuong > 0),
		CONSTRAINT CK_ChiTietSuaChua_TienCong CHECK (TienCong >= 0)
	);
END;
GO

IF EXISTS
(
	SELECT 1
	FROM sys.key_constraints
	WHERE parent_object_id = OBJECT_ID(N'dbo.KhachHang')
	  AND name = N'UQ_KhachHang_DienThoai'
)
BEGIN
	ALTER TABLE dbo.KhachHang DROP CONSTRAINT UQ_KhachHang_DienThoai;
END;

IF NOT EXISTS
(
	SELECT 1
	FROM sys.indexes
	WHERE object_id = OBJECT_ID(N'dbo.KhachHang')
	  AND name = N'UX_KhachHang_DienThoai'
)
BEGIN
	CREATE UNIQUE INDEX UX_KhachHang_DienThoai
	ON dbo.KhachHang(DienThoai)
	WHERE DienThoai IS NOT NULL;
END;

IF NOT EXISTS (SELECT 1 FROM dbo.KhachHang WHERE DienThoai = '0900000001')
	INSERT INTO dbo.KhachHang (TenKH, DienThoai, DiaChi)
	VALUES (N'Nguyễn Văn An', '0900000001', N'Hà Nội');

DECLARE @MaKH int = (SELECT MaKH FROM dbo.KhachHang WHERE DienThoai = '0900000001');
IF NOT EXISTS (SELECT 1 FROM dbo.Xe WHERE BienSo = '30A-12345')
	INSERT INTO dbo.Xe (BienSo, MaKH, HieuXe, NamSanXuat)
	VALUES ('30A-12345', @MaKH, N'Toyota', 2020);

IF NOT EXISTS (SELECT 1 FROM dbo.PhuTung WHERE TenPhuTung = N'Lọc dầu')
	INSERT INTO dbo.PhuTung (TenPhuTung, DonGia, SoLuongTon) VALUES (N'Lọc dầu', 250000, 20);
IF NOT EXISTS (SELECT 1 FROM dbo.PhuTung WHERE TenPhuTung = N'Bugi')
	INSERT INTO dbo.PhuTung (TenPhuTung, DonGia, SoLuongTon) VALUES (N'Bugi', 180000, 30);
IF NOT EXISTS (SELECT 1 FROM dbo.PhuTung WHERE TenPhuTung = N'Dầu động cơ')
	INSERT INTO dbo.PhuTung (TenPhuTung, DonGia, SoLuongTon) VALUES (N'Dầu động cơ', 450000, 15);
GO
