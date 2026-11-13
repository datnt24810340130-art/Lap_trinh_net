/*
   Idempotent setup/migration for the database used by App.config:
   Data Source=LAPTOP-UNQQA3HP;Initial Catalog=LibraryDB1;...
*/
IF DB_ID(N'LibraryDB1') IS NULL
BEGIN
	CREATE DATABASE LibraryDB1;
END;
GO

USE LibraryDB1;
GO

IF OBJECT_ID(N'dbo.DocGia', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.DocGia
	(
		MaDocGia INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_DocGia PRIMARY KEY,
		HoTen NVARCHAR(150) NOT NULL,
		NgaySinh DATE NULL,
		GioiTinh NVARCHAR(20) NULL,
		DiaChi NVARCHAR(300) NULL,
		SoDienThoai VARCHAR(20) NULL,
		Email VARCHAR(254) NULL,
		NgayHetHanThe DATE NOT NULL
	);
END;
GO

IF COL_LENGTH(N'dbo.DocGia', N'NgaySinh') IS NULL
	ALTER TABLE dbo.DocGia ADD NgaySinh DATE NULL;
IF COL_LENGTH(N'dbo.DocGia', N'GioiTinh') IS NULL
	ALTER TABLE dbo.DocGia ADD GioiTinh NVARCHAR(20) NULL;
IF COL_LENGTH(N'dbo.DocGia', N'DiaChi') IS NULL
	ALTER TABLE dbo.DocGia ADD DiaChi NVARCHAR(300) NULL;
GO

IF OBJECT_ID(N'dbo.DauSach', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.DauSach
	(
		MaSach INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_DauSach PRIMARY KEY,
		TenSach NVARCHAR(200) NOT NULL,
		TacGia NVARCHAR(150) NULL,
		TheLoai NVARCHAR(100) NOT NULL,
		NhaXuatBan NVARCHAR(150) NULL,
		NamXuatBan INT NULL,
		SoLuong INT NOT NULL,
		SoLuongCon INT NOT NULL,
		CONSTRAINT CK_DauSach_SoLuong CHECK (SoLuong > 0),
		CONSTRAINT CK_DauSach_SoLuongCon CHECK (SoLuongCon >= 0 AND SoLuongCon <= SoLuong)
	);
END;
GO

/* Add nullable first; later batches can safely reference the new columns. */
IF COL_LENGTH(N'dbo.DauSach', N'NhaXuatBan') IS NULL
	ALTER TABLE dbo.DauSach ADD NhaXuatBan NVARCHAR(150) NULL;
IF COL_LENGTH(N'dbo.DauSach', N'SoLuong') IS NULL
	ALTER TABLE dbo.DauSach ADD SoLuong INT NULL;
IF COL_LENGTH(N'dbo.DauSach', N'SoLuongCon') IS NULL
	ALTER TABLE dbo.DauSach ADD SoLuongCon INT NULL;
GO

/* Preserve existing inventory where possible and normalize values before checks. */
UPDATE dbo.DauSach
SET SoLuong = CASE
				  WHEN SoLuong > 0 THEN SoLuong
				  WHEN SoLuongCon > 0 THEN SoLuongCon
				  ELSE 1
			  END;
UPDATE dbo.DauSach
SET SoLuongCon = CASE
					 WHEN SoLuongCon IS NULL OR SoLuongCon < 0 THEN 0
					 WHEN SoLuongCon > SoLuong THEN SoLuong
					 ELSE SoLuongCon
				 END;
GO

ALTER TABLE dbo.DauSach ALTER COLUMN SoLuong INT NOT NULL;
ALTER TABLE dbo.DauSach ALTER COLUMN SoLuongCon INT NOT NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.DauSach') AND name = N'CK_DauSach_SoLuong')
	ALTER TABLE dbo.DauSach ADD CONSTRAINT CK_DauSach_SoLuong CHECK (SoLuong > 0);
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.DauSach') AND name = N'CK_DauSach_SoLuongCon')
	ALTER TABLE dbo.DauSach ADD CONSTRAINT CK_DauSach_SoLuongCon CHECK (SoLuongCon >= 0 AND SoLuongCon <= SoLuong);
GO

IF OBJECT_ID(N'dbo.PhieuMuon', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.PhieuMuon
	(
		MaPhieu INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuMuon PRIMARY KEY,
		MaDocGia INT NOT NULL,
		NgayMuon DATE NOT NULL,
		HanTra DATE NOT NULL,
		NgayTra DATE NULL,
		TrangThai NVARCHAR(30) NOT NULL CONSTRAINT DF_PhieuMuon_TrangThai DEFAULT N'Đang mượn',
		CONSTRAINT FK_PhieuMuon_DocGia FOREIGN KEY (MaDocGia) REFERENCES dbo.DocGia(MaDocGia),
		CONSTRAINT CK_PhieuMuon_HanTra CHECK (HanTra >= NgayMuon),
		CONSTRAINT CK_PhieuMuon_TrangThai CHECK (TrangThai IN (N'Đang mượn', N'Đã trả'))
	);
END;
GO

IF COL_LENGTH(N'dbo.PhieuMuon', N'TrangThai') IS NULL
	ALTER TABLE dbo.PhieuMuon ADD TrangThai NVARCHAR(30) NOT NULL CONSTRAINT DF_PhieuMuon_TrangThai_Migration DEFAULT N'Đang mượn' WITH VALUES;
GO

IF OBJECT_ID(N'dbo.ChiTietMuon', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.ChiTietMuon
	(
		MaPhieu INT NOT NULL,
		MaSach INT NOT NULL,
		SoLuong INT NOT NULL,
		CONSTRAINT PK_ChiTietMuon PRIMARY KEY (MaPhieu, MaSach),
		CONSTRAINT FK_ChiTietMuon_PhieuMuon FOREIGN KEY (MaPhieu) REFERENCES dbo.PhieuMuon(MaPhieu),
		CONSTRAINT FK_ChiTietMuon_DauSach FOREIGN KEY (MaSach) REFERENCES dbo.DauSach(MaSach),
		CONSTRAINT CK_ChiTietMuon_SoLuong CHECK (SoLuong > 0)
	);
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.DocGia)
BEGIN
	INSERT dbo.DocGia (HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, NgayHetHanThe)
	VALUES
		(N'Nguyễn Văn An', '2000-05-10', N'Nam', N'Hà Nội', '0900000001', 'an@example.com', DATEADD(YEAR, 1, CONVERT(date, GETDATE()))),
		(N'Trần Thị Bình', '1999-08-20', N'Nữ', N'Hà Nội', '0900000002', 'binh@example.com', DATEADD(YEAR, 1, CONVERT(date, GETDATE())));
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.DauSach)
BEGIN
	INSERT dbo.DauSach (TenSach, TacGia, TheLoai, NhaXuatBan, NamXuatBan, SoLuong, SoLuongCon)
	VALUES
		(N'Lập trình C# cơ bản', N'Nguyễn Văn A', N'Công nghệ', N'Giáo dục', 2023, 10, 8),
		(N'Nhập môn cơ sở dữ liệu', N'Trần Văn B', N'Công nghệ', N'Khoa học', 2022, 8, 8),
		(N'Cho tôi xin một vé đi tuổi thơ', N'Nguyễn Nhật Ánh', N'Văn học', N'Trẻ', 2019, 12, 12);
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.PhieuMuon)
BEGIN
	DECLARE @DocGiaId INT = (SELECT TOP (1) MaDocGia FROM dbo.DocGia ORDER BY MaDocGia);
	DECLARE @SachId INT = (SELECT TOP (1) MaSach FROM dbo.DauSach ORDER BY MaSach);
	DECLARE @MaPhieu INT;

	INSERT dbo.PhieuMuon (MaDocGia, NgayMuon, HanTra, TrangThai)
	VALUES (@DocGiaId, DATEADD(DAY, -30, CONVERT(date, GETDATE())), DATEADD(DAY, -2, CONVERT(date, GETDATE())), N'Đang mượn');

	SET @MaPhieu = CONVERT(INT, SCOPE_IDENTITY());
	INSERT dbo.ChiTietMuon (MaPhieu, MaSach, SoLuong)
	VALUES (@MaPhieu, @SachId, 2);
END;
GO