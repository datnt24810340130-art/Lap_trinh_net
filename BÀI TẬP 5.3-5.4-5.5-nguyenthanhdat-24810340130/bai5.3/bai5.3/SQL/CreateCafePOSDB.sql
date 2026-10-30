IF DB_ID(N'CafePOSDB') IS NULL
BEGIN
	CREATE DATABASE CafePOSDB;
END;
GO

USE CafePOSDB;
GO

SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;
GO

IF OBJECT_ID(N'dbo.BanAn', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.BanAn
	(
		MaBan int IDENTITY(1,1) NOT NULL CONSTRAINT PK_BanAn PRIMARY KEY,
		TenBan nvarchar(50) NOT NULL,
		TrangThai nvarchar(20) NOT NULL CONSTRAINT DF_BanAn_TrangThai DEFAULT N'Trống',
		CONSTRAINT CK_BanAn_TrangThai CHECK (TrangThai IN (N'Trống', N'Có khách'))
	);
END;
GO

IF OBJECT_ID(N'dbo.DanhMuc', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.DanhMuc
	(
		MaDanhMuc int IDENTITY(1,1) NOT NULL CONSTRAINT PK_DanhMuc PRIMARY KEY,
		TenDanhMuc nvarchar(100) NOT NULL
	);
END;
GO

IF OBJECT_ID(N'dbo.MonAn', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.MonAn
	(
		MaMon int IDENTITY(1,1) NOT NULL CONSTRAINT PK_MonAn PRIMARY KEY,
		TenMon nvarchar(100) NOT NULL,
		MaDanhMuc int NOT NULL,
		DonGia decimal(18,2) NOT NULL,
		CONSTRAINT FK_MonAn_DanhMuc FOREIGN KEY (MaDanhMuc) REFERENCES dbo.DanhMuc(MaDanhMuc),
		CONSTRAINT CK_MonAn_DonGia CHECK (DonGia >= 0)
	);
END;
GO

IF OBJECT_ID(N'dbo.HoaDon', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.HoaDon
	(
		MaHoaDon int IDENTITY(1,1) NOT NULL CONSTRAINT PK_HoaDon PRIMARY KEY,
		MaBan int NOT NULL,
		GioVao datetime NOT NULL CONSTRAINT DF_HoaDon_GioVao DEFAULT GETDATE(),
		GioRa datetime NULL,
		TongTien decimal(18,2) NOT NULL CONSTRAINT DF_HoaDon_TongTien DEFAULT 0,
		TrangThai bit NOT NULL CONSTRAINT DF_HoaDon_TrangThai DEFAULT 0,
		CONSTRAINT FK_HoaDon_BanAn FOREIGN KEY (MaBan) REFERENCES dbo.BanAn(MaBan),
		CONSTRAINT CK_HoaDon_TongTien CHECK (TongTien >= 0)
	);
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_HoaDon_BanAn_ChuaThanhToan' AND object_id = OBJECT_ID(N'dbo.HoaDon'))
BEGIN
	CREATE UNIQUE INDEX UX_HoaDon_BanAn_ChuaThanhToan ON dbo.HoaDon(MaBan) WHERE TrangThai = 0;
END;
GO

IF OBJECT_ID(N'dbo.ChiTietHoaDon', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.ChiTietHoaDon
	(
		MaHoaDon int NOT NULL,
		MaMon int NOT NULL,
		SoLuong int NOT NULL,
		DonGia decimal(18,2) NOT NULL,
		CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY (MaHoaDon, MaMon),
		CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY (MaHoaDon) REFERENCES dbo.HoaDon(MaHoaDon),
		CONSTRAINT FK_ChiTietHoaDon_MonAn FOREIGN KEY (MaMon) REFERENCES dbo.MonAn(MaMon),
		CONSTRAINT CK_ChiTietHoaDon_SoLuong CHECK (SoLuong > 0),
		CONSTRAINT CK_ChiTietHoaDon_DonGia CHECK (DonGia >= 0)
	);
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.BanAn)
BEGIN
	INSERT INTO dbo.BanAn (TenBan) VALUES (N'Bàn 01'), (N'Bàn 02'), (N'Bàn 03'), (N'Bàn 04'), (N'Bàn 05'), (N'Bàn 06');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DanhMuc)
BEGIN
	INSERT INTO dbo.DanhMuc (TenDanhMuc) VALUES (N'Cà phê'), (N'Trà sữa'), (N'Đồ ăn nhẹ');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.MonAn)
BEGIN
	INSERT INTO dbo.MonAn (TenMon, MaDanhMuc, DonGia)
	SELECT N'Cà phê đen', MaDanhMuc, 25000 FROM dbo.DanhMuc WHERE TenDanhMuc = N'Cà phê'
	UNION ALL SELECT N'Cà phê sữa', MaDanhMuc, 30000 FROM dbo.DanhMuc WHERE TenDanhMuc = N'Cà phê'
	UNION ALL SELECT N'Trà sữa truyền thống', MaDanhMuc, 35000 FROM dbo.DanhMuc WHERE TenDanhMuc = N'Trà sữa'
	UNION ALL SELECT N'Bánh ngọt', MaDanhMuc, 20000 FROM dbo.DanhMuc WHERE TenDanhMuc = N'Đồ ăn nhẹ';
END;
GO