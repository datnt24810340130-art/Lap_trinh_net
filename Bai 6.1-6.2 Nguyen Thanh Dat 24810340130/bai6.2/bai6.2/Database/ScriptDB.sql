IF DB_ID(N'HRMDB') IS NULL
BEGIN
	CREATE DATABASE HRMDB;
END;
GO

USE HRMDB;
GO

IF OBJECT_ID(N'dbo.PhongBan', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.PhongBan
	(
		MaPB int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhongBan PRIMARY KEY,
		TenPB nvarchar(100) NOT NULL,
		Mota nvarchar(255) NULL
	);
END;
GO

IF OBJECT_ID(N'dbo.NhanVien', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.NhanVien
	(
		MaNV int IDENTITY(1,1) NOT NULL CONSTRAINT PK_NhanVien PRIMARY KEY,
		HoTen nvarchar(100) NOT NULL,
		NgaySinh datetime NULL,
		CCCD varchar(20) NULL,
		MaPB int NULL,
		LuongCoBan decimal(18,2) NOT NULL,
		HeSoLuong float NOT NULL CONSTRAINT DF_NhanVien_HeSoLuong DEFAULT (1.0),
		CONSTRAINT CK_NhanVien_LuongCoBan CHECK (LuongCoBan > 0),
		CONSTRAINT FK_NhanVien_PhongBan FOREIGN KEY (MaPB) REFERENCES dbo.PhongBan(MaPB)
	);
END;
GO

IF NOT EXISTS
(
	SELECT 1 FROM sys.indexes
	WHERE object_id = OBJECT_ID(N'dbo.NhanVien') AND name = N'UX_NhanVien_CCCD'
)
BEGIN
	CREATE UNIQUE INDEX UX_NhanVien_CCCD ON dbo.NhanVien(CCCD) WHERE CCCD IS NOT NULL;
END;
GO

IF OBJECT_ID(N'dbo.BangChamCong', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.BangChamCong
	(
		MaCong int IDENTITY(1,1) NOT NULL CONSTRAINT PK_BangChamCong PRIMARY KEY,
		MaNV int NOT NULL,
		ThangNam varchar(7) NOT NULL,
		SoNgayCong float NOT NULL CONSTRAINT DF_BangChamCong_SoNgayCong DEFAULT (0),
		SoGioOT float NOT NULL CONSTRAINT DF_BangChamCong_SoGioOT DEFAULT (0),
		IsLocked bit NOT NULL CONSTRAINT DF_BangChamCong_IsLocked DEFAULT (0),
		CONSTRAINT CK_BangChamCong_SoNgayCong CHECK (SoNgayCong >= 0 AND SoNgayCong <= 31),
		CONSTRAINT CK_BangChamCong_SoGioOT CHECK (SoGioOT >= 0),
		CONSTRAINT FK_BangChamCong_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV),
		CONSTRAINT UQ_BangChamCong_NhanVien_Thang UNIQUE (MaNV, ThangNam)
	);
END;
GO

IF COL_LENGTH(N'dbo.BangChamCong', N'IsLocked') IS NULL
BEGIN
	ALTER TABLE dbo.BangChamCong
	ADD IsLocked bit NOT NULL
		CONSTRAINT DF_BangChamCong_IsLocked DEFAULT (0) WITH VALUES;
END;
GO

IF OBJECT_ID(N'dbo.BangLuong', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.BangLuong
	(
		MaLuong int IDENTITY(1,1) NOT NULL,
		MaNV int NOT NULL,
		ThangNam varchar(7) NOT NULL,
		ThucLinh decimal(18,2) NOT NULL,
		CONSTRAINT PK_BangLuong PRIMARY KEY (MaNV, ThangNam),
		CONSTRAINT UQ_BangLuong_MaLuong UNIQUE (MaLuong),
		CONSTRAINT FK_BangLuong_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV)
	);
END;
GO

CREATE OR ALTER TRIGGER dbo.TR_BangChamCong_PreventLockedChanges
ON dbo.BangChamCong
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM deleted WHERE IsLocked = 1)
		OR EXISTS
		(
			SELECT 1
			FROM inserted AS i
			WHERE i.IsLocked = 0
			  AND EXISTS
			  (
				  SELECT 1 FROM dbo.BangChamCong AS closed
				  WHERE closed.ThangNam = i.ThangNam AND closed.IsLocked = 1
			  )
		)
		OR EXISTS
		(
			SELECT 1 FROM inserted AS i
			WHERE i.IsLocked = 1
			  AND NOT EXISTS (SELECT 1 FROM deleted AS d WHERE d.MaCong = i.MaCong)
		)
	BEGIN
		;THROW 51000, N'Bảng chấm công đã chốt không thể thay đổi.', 1;
	END;
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.PhongBan)
BEGIN
	INSERT INTO dbo.PhongBan (TenPB, Mota)
	VALUES (N'Nhân sự', N'Quản lý nhân sự và tiền lương'),
		   (N'Kế toán', N'Quản lý tài chính và kế toán'),
		   (N'Kỹ thuật', N'Phát triển và vận hành hệ thống');
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.NhanVien)
BEGIN
	INSERT INTO dbo.NhanVien (HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong)
	SELECT N'Nguyễn Minh Anh', '1995-03-12', '001095000001', MaPB, 12000000, 1.2 FROM dbo.PhongBan WHERE TenPB = N'Nhân sự'
	UNION ALL
	SELECT N'Trần Quốc Bảo', '1992-08-25', '001092000002', MaPB, 15000000, 1.4 FROM dbo.PhongBan WHERE TenPB = N'Kế toán'
	UNION ALL
	SELECT N'Lê Thu Hà', '1998-11-05', '001098000003', MaPB, 11000000, 1.1 FROM dbo.PhongBan WHERE TenPB = N'Kỹ thuật';
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.BangChamCong)
BEGIN
	INSERT INTO dbo.BangChamCong (MaNV, ThangNam, SoNgayCong, SoGioOT)
	SELECT MaNV, '11/2026', 22, 2 FROM dbo.NhanVien;
END;
GO
