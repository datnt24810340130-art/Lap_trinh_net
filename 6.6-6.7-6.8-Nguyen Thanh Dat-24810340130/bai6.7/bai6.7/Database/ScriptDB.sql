IF DB_ID(N'ClinicDB') IS NULL
BEGIN
	CREATE DATABASE ClinicDB;
END;
GO

USE ClinicDB;
GO

IF OBJECT_ID(N'dbo.BenhNhan', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.BenhNhan
	(
		MaBN INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_BenhNhan PRIMARY KEY,
		HoTen NVARCHAR(150) NOT NULL,
		SoDienThoai NVARCHAR(20) NOT NULL,
		NgaySinh DATE NULL,
		GioiTinh NVARCHAR(20) NULL,
		DiaChi NVARCHAR(300) NULL,
		CONSTRAINT UQ_BenhNhan_SoDienThoai UNIQUE (SoDienThoai)
	);
END;
GO

IF OBJECT_ID(N'dbo.Thuoc', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.Thuoc
	(
		MaThuoc INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Thuoc PRIMARY KEY,
		TenThuoc NVARCHAR(150) NOT NULL,
		DonViTinh NVARCHAR(30) NOT NULL,
		DonGia DECIMAL(18, 2) NOT NULL,
		SoLuongTon INT NOT NULL CONSTRAINT DF_Thuoc_SoLuongTon DEFAULT 0,
		CONSTRAINT CK_Thuoc_DonGia CHECK (DonGia >= 0),
		CONSTRAINT CK_Thuoc_SoLuongTon CHECK (SoLuongTon >= 0)
	);
END;
GO

IF OBJECT_ID(N'dbo.HoSoKham', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.HoSoKham
	(
		MaHoSo INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_HoSoKham PRIMARY KEY,
		MaBN INT NOT NULL,
		NgayKham DATETIME2 NOT NULL CONSTRAINT DF_HoSoKham_NgayKham DEFAULT SYSDATETIME(),
		ChanDoan NVARCHAR(500) NOT NULL,
		TienKham DECIMAL(18, 2) NOT NULL CONSTRAINT DF_HoSoKham_TienKham DEFAULT 0,
		TrangThai NVARCHAR(30) NOT NULL CONSTRAINT DF_HoSoKham_TrangThai DEFAULT N'Chưa thanh toán',
		CONSTRAINT FK_HoSoKham_BenhNhan FOREIGN KEY (MaBN) REFERENCES dbo.BenhNhan(MaBN),
		CONSTRAINT CK_HoSoKham_TienKham CHECK (TienKham >= 0)
	);
END;
GO

IF OBJECT_ID(N'dbo.DonThuoc', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.DonThuoc
	(
		MaDon INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_DonThuoc PRIMARY KEY,
		MaHoSo INT NOT NULL,
		NgayKe DATETIME2 NOT NULL CONSTRAINT DF_DonThuoc_NgayKe DEFAULT SYSDATETIME(),
		GhiChu NVARCHAR(500) NULL,
		CONSTRAINT FK_DonThuoc_HoSoKham FOREIGN KEY (MaHoSo) REFERENCES dbo.HoSoKham(MaHoSo)
	);
END;
GO

IF OBJECT_ID(N'dbo.ChiTietDonThuoc', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.ChiTietDonThuoc
	(
		MaDon INT NOT NULL,
		MaThuoc INT NOT NULL,
		SoLuong INT NOT NULL,
		DonGia DECIMAL(18, 2) NOT NULL,
		CONSTRAINT PK_ChiTietDonThuoc PRIMARY KEY (MaDon, MaThuoc),
		CONSTRAINT FK_ChiTietDonThuoc_DonThuoc FOREIGN KEY (MaDon) REFERENCES dbo.DonThuoc(MaDon),
		CONSTRAINT FK_ChiTietDonThuoc_Thuoc FOREIGN KEY (MaThuoc) REFERENCES dbo.Thuoc(MaThuoc),
		CONSTRAINT CK_ChiTietDonThuoc_SoLuong CHECK (SoLuong > 0),
		CONSTRAINT CK_ChiTietDonThuoc_DonGia CHECK (DonGia >= 0)
	);
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Thuoc)
BEGIN
	INSERT dbo.Thuoc (TenThuoc, DonViTinh, DonGia, SoLuongTon)
	VALUES
		(N'Paracetamol 500mg', N'Viên', 1500, 500),
		(N'Amoxicillin 500mg', N'Viên', 2500, 300),
		(N'Vitamin C 500mg', N'Viên', 1200, 400),
		(N'Oresol', N'Gói', 5000, 150),
		(N'Cetirizine 10mg', N'Viên', 2000, 250);
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.BenhNhan)
BEGIN
	INSERT dbo.BenhNhan (HoTen, SoDienThoai, NgaySinh, GioiTinh, DiaChi)
	VALUES
		(N'Nguyễn Minh Anh', N'0901234567', '1995-04-12', N'Nữ', N'Hà Nội'),
		(N'Trần Văn Bình', N'0912345678', '1988-09-23', N'Nam', N'Hà Nội'),
		(N'Lê Thu Hà', N'0987654321', '2001-01-05', N'Nữ', N'Bắc Ninh');
END;
GO
