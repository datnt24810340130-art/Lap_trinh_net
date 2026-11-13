IF DB_ID(N'BilliardsDB') IS NULL
BEGIN
	CREATE DATABASE BilliardsDB;
END;
GO

USE BilliardsDB;
GO

IF OBJECT_ID(N'dbo.ChiTietHoaDon', N'U') IS NOT NULL DROP TABLE dbo.ChiTietHoaDon;
IF OBJECT_ID(N'dbo.HoaDon', N'U') IS NOT NULL DROP TABLE dbo.HoaDon;
IF OBJECT_ID(N'dbo.MenuDichVu', N'U') IS NOT NULL DROP TABLE dbo.MenuDichVu;
IF OBJECT_ID(N'dbo.BanBida', N'U') IS NOT NULL DROP TABLE dbo.BanBida;
GO

CREATE TABLE dbo.BanBida
(
	MaBan INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_BanBida PRIMARY KEY,
	TenBan NVARCHAR(50) NOT NULL,
	TrangThai NVARCHAR(20) NOT NULL CONSTRAINT DF_BanBida_TrangThai DEFAULT N'Trống',
	CONSTRAINT CK_BanBida_TrangThai CHECK (TrangThai IN (N'Trống', N'Đang chơi'))
);

CREATE TABLE dbo.MenuDichVu
(
	MaDV INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_MenuDichVu PRIMARY KEY,
	TenDV NVARCHAR(100) NOT NULL,
	DonGia DECIMAL(18,2) NOT NULL,
	TonKho INT NOT NULL CONSTRAINT DF_MenuDichVu_TonKho DEFAULT 0,
	CONSTRAINT CK_MenuDichVu_DonGia CHECK (DonGia >= 0),
	CONSTRAINT CK_MenuDichVu_TonKho CHECK (TonKho >= 0)
);

CREATE TABLE dbo.HoaDon
(
	MaHD INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_HoaDon PRIMARY KEY,
	MaBan INT NOT NULL,
	GioBatDau DATETIME2(0) NOT NULL CONSTRAINT DF_HoaDon_GioBatDau DEFAULT SYSDATETIME(),
	GioKetThuc DATETIME2(0) NULL,
	GiaTheoGio DECIMAL(18,2) NOT NULL,
	SoGio DECIMAL(10,2) NULL,
	TienGioChoi DECIMAL(18,2) NULL,
	TienDichVu DECIMAL(18,2) NULL,
	TongTien DECIMAL(18,2) NULL,
	TrangThai NVARCHAR(20) NOT NULL CONSTRAINT DF_HoaDon_TrangThai DEFAULT N'Đang mở',
	CONSTRAINT FK_HoaDon_BanBida FOREIGN KEY (MaBan) REFERENCES dbo.BanBida(MaBan),
	CONSTRAINT CK_HoaDon_TrangThai CHECK (TrangThai IN (N'Đang mở', N'Đã thanh toán'))
);

CREATE TABLE dbo.ChiTietHoaDon
(
	MaHD INT NOT NULL,
	MaDV INT NOT NULL,
	SoLuong INT NOT NULL,
	DonGia DECIMAL(18,2) NOT NULL,
	ThanhTien AS (CONVERT(DECIMAL(18,2), SoLuong * DonGia)) PERSISTED,
	CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY (MaHD, MaDV),
	CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY (MaHD) REFERENCES dbo.HoaDon(MaHD),
	CONSTRAINT FK_ChiTietHoaDon_MenuDichVu FOREIGN KEY (MaDV) REFERENCES dbo.MenuDichVu(MaDV),
	CONSTRAINT CK_ChiTietHoaDon_SoLuong CHECK (SoLuong > 0)
);
GO

IF NOT EXISTS (SELECT 1 FROM dbo.BanBida)
BEGIN
	INSERT INTO dbo.BanBida (TenBan, TrangThai) VALUES
	(N'Bàn 01', N'Trống'), (N'Bàn 02', N'Trống'), (N'Bàn 03', N'Trống'),
	(N'Bàn 04', N'Trống'), (N'Bàn 05', N'Trống'), (N'Bàn 06', N'Trống'),
	(N'Bàn 07', N'Trống'), (N'Bàn 08', N'Trống'), (N'Bàn 09', N'Trống'),
	(N'Bàn 10', N'Trống');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.MenuDichVu)
BEGIN
	INSERT INTO dbo.MenuDichVu (TenDV, DonGia, TonKho) VALUES
	(N'Nước ngọt', 15000, 100),
	(N'Nước suối', 10000, 100),
	(N'Trà đá', 5000, 100),
	(N'Mì xào', 35000, 30),
	(N'Khăn lạnh', 5000, 50),
	(N'Phấn bida', 10000, 25),
	(N'Bao tay bida', 25000, 20);
END;
GO
