IF DB_ID(N'InventoryDB') IS NULL
	CREATE DATABASE InventoryDB;
GO

USE InventoryDB;
GO

IF OBJECT_ID(N'dbo.NhaCungCap', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.NhaCungCap
	(
		MaNCC int IDENTITY(1,1) NOT NULL CONSTRAINT PK_NhaCungCap PRIMARY KEY,
		TenNCC nvarchar(150) NOT NULL,
		DienThoai varchar(15) NULL,
		DiaChi nvarchar(255) NULL
	);
	CREATE UNIQUE INDEX UX_NhaCungCap_DienThoai
		ON dbo.NhaCungCap(DienThoai) WHERE DienThoai IS NOT NULL;
END;
GO

IF OBJECT_ID(N'dbo.VatTu', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.VatTu
	(
		MaVatTu varchar(20) NOT NULL CONSTRAINT PK_VatTu PRIMARY KEY,
		TenVatTu nvarchar(150) NOT NULL,
		DVT nvarchar(20) NULL,
		SoLuongTon decimal(18,2) NOT NULL CONSTRAINT DF_VatTu_SoLuongTon DEFAULT (0),
		CONSTRAINT CK_VatTu_SoLuongTon CHECK (SoLuongTon >= 0)
	);
END;
GO

IF OBJECT_ID(N'dbo.DinhMucTonKho', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.DinhMucTonKho
	(
		MaVatTu varchar(20) NOT NULL CONSTRAINT PK_DinhMucTonKho PRIMARY KEY,
		SoLuongToiThieu decimal(18,2) NOT NULL CONSTRAINT DF_DinhMucTonKho_SoLuong DEFAULT (0),
		CONSTRAINT CK_DinhMucTonKho_SoLuong CHECK (SoLuongToiThieu >= 0),
		CONSTRAINT FK_DinhMucTonKho_VatTu FOREIGN KEY (MaVatTu) REFERENCES dbo.VatTu(MaVatTu)
	);
END;
GO

IF OBJECT_ID(N'dbo.PhieuKho', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.PhieuKho
	(
		MaPhieu int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuKho PRIMARY KEY,
		LoaiPhieu nvarchar(10) NOT NULL,
		NgayLap datetime NOT NULL CONSTRAINT DF_PhieuKho_NgayLap DEFAULT (GETDATE()),
		MaNCC int NULL,
		TongTien decimal(18,2) NOT NULL CONSTRAINT DF_PhieuKho_TongTien DEFAULT (0),
		CONSTRAINT CK_PhieuKho_LoaiPhieu CHECK (LoaiPhieu IN (N'NHAP', N'XUAT')),
		CONSTRAINT FK_PhieuKho_NhaCungCap FOREIGN KEY (MaNCC) REFERENCES dbo.NhaCungCap(MaNCC)
	);
END;
GO

IF OBJECT_ID(N'dbo.ChiTietPhieuKho', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.ChiTietPhieuKho
	(
		MaPhieu int NOT NULL,
		MaVatTu varchar(20) NOT NULL,
		SoLuong decimal(18,2) NOT NULL,
		DonGia decimal(18,2) NOT NULL,
		CONSTRAINT PK_ChiTietPhieuKho PRIMARY KEY (MaPhieu, MaVatTu),
		CONSTRAINT CK_ChiTietPhieuKho_SoLuong CHECK (SoLuong > 0),
		CONSTRAINT CK_ChiTietPhieuKho_DonGia CHECK (DonGia >= 0),
		CONSTRAINT FK_ChiTietPhieuKho_PhieuKho FOREIGN KEY (MaPhieu) REFERENCES dbo.PhieuKho(MaPhieu),
		CONSTRAINT FK_ChiTietPhieuKho_VatTu FOREIGN KEY (MaVatTu) REFERENCES dbo.VatTu(MaVatTu)
	);
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.NhaCungCap WHERE TenNCC = N'Nhà cung cấp mẫu')
	INSERT dbo.NhaCungCap (TenNCC, DienThoai, DiaChi)
	VALUES (N'Nhà cung cấp mẫu', '0900000000', N'Hà Nội');

IF NOT EXISTS (SELECT 1 FROM dbo.VatTu WHERE MaVatTu = 'VT001')
BEGIN
	INSERT dbo.VatTu (MaVatTu, TenVatTu, DVT, SoLuongTon)
	VALUES ('VT001', N'Vật tư mẫu', N'Cái', 0);
	INSERT dbo.DinhMucTonKho (MaVatTu, SoLuongToiThieu)
	VALUES ('VT001', 5);
END;
GO
