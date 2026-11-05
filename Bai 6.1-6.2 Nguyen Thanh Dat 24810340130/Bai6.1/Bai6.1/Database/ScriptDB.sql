IF DB_ID(N'SalesDB') IS NULL
	CREATE DATABASE SalesDB;
GO

USE SalesDB;
GO

IF OBJECT_ID(N'dbo.ChiTietHoaDon', N'U') IS NOT NULL DROP TABLE dbo.ChiTietHoaDon;
IF OBJECT_ID(N'dbo.HoaDon', N'U') IS NOT NULL DROP TABLE dbo.HoaDon;
IF OBJECT_ID(N'dbo.SanPham', N'U') IS NOT NULL DROP TABLE dbo.SanPham;
IF OBJECT_ID(N'dbo.KhachHang', N'U') IS NOT NULL DROP TABLE dbo.KhachHang;
IF OBJECT_ID(N'dbo.NhanVien', N'U') IS NOT NULL DROP TABLE dbo.NhanVien;
GO

CREATE TABLE dbo.NhanVien
(
	MaNV int IDENTITY(1,1) NOT NULL CONSTRAINT PK_NhanVien PRIMARY KEY,
	TenNV nvarchar(100) NOT NULL,
	SoDienThoai varchar(15) NULL CONSTRAINT UQ_NhanVien_SoDienThoai UNIQUE,
	ChucVu nvarchar(50) NULL
);

CREATE TABLE dbo.KhachHang
(
	MaKH int IDENTITY(1,1) NOT NULL CONSTRAINT PK_KhachHang PRIMARY KEY,
	TenKH nvarchar(100) NOT NULL,
	DienThoai varchar(15) NULL CONSTRAINT UQ_KhachHang_DienThoai UNIQUE,
	DiemTichLuy int NOT NULL CONSTRAINT DF_KhachHang_DiemTichLuy DEFAULT (0)
		CONSTRAINT CK_KhachHang_DiemTichLuy CHECK (DiemTichLuy >= 0)
);

CREATE TABLE dbo.SanPham
(
	MaSP varchar(20) NOT NULL CONSTRAINT PK_SanPham PRIMARY KEY,
	TenSP nvarchar(150) NOT NULL,
	GiaBan decimal(18,2) NOT NULL CONSTRAINT CK_SanPham_GiaBan CHECK (GiaBan > 0),
	SoLuongTon int NOT NULL CONSTRAINT CK_SanPham_SoLuongTon CHECK (SoLuongTon >= 0)
);

CREATE TABLE dbo.HoaDon
(
	MaHD int IDENTITY(1,1) NOT NULL CONSTRAINT PK_HoaDon PRIMARY KEY,
	NgayLap datetime NOT NULL CONSTRAINT DF_HoaDon_NgayLap DEFAULT (GETDATE()),
	MaNV int NOT NULL,
	MaKH int NULL,
	TongTien decimal(18,2) NOT NULL CONSTRAINT CK_HoaDon_TongTien CHECK (TongTien >= 0),
	CONSTRAINT FK_HoaDon_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV),
	CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH)
);

CREATE TABLE dbo.ChiTietHoaDon
(
	MaHD int NOT NULL,
	MaSP varchar(20) NOT NULL,
	SoLuong int NOT NULL CONSTRAINT CK_ChiTietHoaDon_SoLuong CHECK (SoLuong > 0),
	DonGia decimal(18,2) NOT NULL CONSTRAINT CK_ChiTietHoaDon_DonGia CHECK (DonGia > 0),
	CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY (MaHD, MaSP),
	CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY (MaHD) REFERENCES dbo.HoaDon(MaHD),
	CONSTRAINT FK_ChiTietHoaDon_SanPham FOREIGN KEY (MaSP) REFERENCES dbo.SanPham(MaSP)
);
GO

INSERT INTO dbo.NhanVien (TenNV, SoDienThoai, ChucVu)
VALUES (N'Nguyễn Thị Lan', '0900000001', N'Thu ngân'),
	   (N'Trần Văn Minh', '0900000002', N'Quản lý');

INSERT INTO dbo.KhachHang (TenKH, DienThoai)
VALUES (N'Khách hàng mẫu', '0910000001'),
	   (N'Nguyễn Văn An', '0910000002');

INSERT INTO dbo.SanPham (MaSP, TenSP, GiaBan, SoLuongTon)
VALUES ('SP001', N'Nước suối 500ml', 5000, 100),
	   ('SP002', N'Sữa tươi 1L', 32000, 50),
	   ('SP003', N'Mì ăn liền', 4500, 200),
	   ('SP004', N'Bánh quy', 18000, 75),
	   ('SP005', N'Cà phê hòa tan', 25000, 60);
GO
