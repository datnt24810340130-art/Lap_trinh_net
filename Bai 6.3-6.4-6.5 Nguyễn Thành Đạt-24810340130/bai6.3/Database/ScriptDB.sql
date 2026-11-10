IF DB_ID(N'ProjectTaskDB') IS NULL
BEGIN
	CREATE DATABASE ProjectTaskDB;
END;
GO

USE ProjectTaskDB;
GO

IF OBJECT_ID(N'dbo.PhanCong', N'U') IS NOT NULL DROP TABLE dbo.PhanCong;
IF OBJECT_ID(N'dbo.CongViec', N'U') IS NOT NULL DROP TABLE dbo.CongViec;
IF OBJECT_ID(N'dbo.ThanhVien', N'U') IS NOT NULL DROP TABLE dbo.ThanhVien;
IF OBJECT_ID(N'dbo.DuAn', N'U') IS NOT NULL DROP TABLE dbo.DuAn;
GO

CREATE TABLE dbo.DuAn
(
	MaDuAn INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_DuAn PRIMARY KEY,
	TenDuAn NVARCHAR(150) NOT NULL,
	NgayBatDau DATETIME NULL,
	NgayKetThuc DATETIME NULL,
	TrangThai NVARCHAR(50) NULL,
	CONSTRAINT CK_DuAn_Ngay CHECK (NgayKetThuc IS NULL OR NgayBatDau IS NULL OR NgayKetThuc >= NgayBatDau)
);

CREATE TABLE dbo.ThanhVien
(
	MaThanhVien INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ThanhVien PRIMARY KEY,
	HoTen NVARCHAR(100) NOT NULL,
	Email VARCHAR(100) NOT NULL CONSTRAINT UQ_ThanhVien_Email UNIQUE,
	ChuyenMon NVARCHAR(100) NULL
);

CREATE TABLE dbo.CongViec
(
	MaTask INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_CongViec PRIMARY KEY,
	MaDuAn INT NOT NULL,
	TenTask NVARCHAR(200) NOT NULL,
	MoTa NVARCHAR(MAX) NULL,
	HanNop DATETIME NULL,
	DoUuTien NVARCHAR(20) NULL,
	TrangThai NVARCHAR(50) NOT NULL CONSTRAINT DF_CongViec_TrangThai DEFAULT N'To Do',
	CONSTRAINT FK_CongViec_DuAn FOREIGN KEY (MaDuAn) REFERENCES dbo.DuAn(MaDuAn) ON DELETE CASCADE,
	CONSTRAINT CK_CongViec_DoUuTien CHECK (DoUuTien IS NULL OR DoUuTien IN (N'Cao', N'Trung bình', N'Thấp')),
	CONSTRAINT CK_CongViec_TrangThai CHECK (TrangThai IN (N'To Do', N'In Progress', N'Review', N'Done'))
);

CREATE TABLE dbo.PhanCong
(
	MaTask INT NOT NULL,
	MaThanhVien INT NOT NULL,
	NgayGiao DATETIME NOT NULL CONSTRAINT DF_PhanCong_NgayGiao DEFAULT GETDATE(),
	CONSTRAINT PK_PhanCong PRIMARY KEY (MaTask, MaThanhVien),
	CONSTRAINT FK_PhanCong_CongViec FOREIGN KEY (MaTask) REFERENCES dbo.CongViec(MaTask) ON DELETE CASCADE,
	CONSTRAINT FK_PhanCong_ThanhVien FOREIGN KEY (MaThanhVien) REFERENCES dbo.ThanhVien(MaThanhVien) ON DELETE CASCADE
);
GO

INSERT INTO dbo.DuAn (TenDuAn, NgayBatDau, NgayKetThuc, TrangThai)
VALUES
(N'Website bán hàng', GETDATE(), DATEADD(DAY, 60, GETDATE()), N'Đang chạy'),
(N'Ứng dụng quản lý nội bộ', DATEADD(DAY, -30, GETDATE()), DATEADD(DAY, 30, GETDATE()), N'Đang chạy'),
(N'Nâng cấp hạ tầng', DATEADD(DAY, -90, GETDATE()), DATEADD(DAY, -10, GETDATE()), N'Hoàn thành');

INSERT INTO dbo.ThanhVien (HoTen, Email, ChuyenMon)
VALUES
(N'Nguyễn Minh Anh', 'minhanh@example.com', N'Frontend'),
(N'Trần Quốc Bảo', 'quocbao@example.com', N'Backend'),
(N'Lê Thu Hà', 'thuha@example.com', N'Kiểm thử');

INSERT INTO dbo.CongViec (MaDuAn, TenTask, MoTa, HanNop, DoUuTien, TrangThai)
VALUES
(1, N'Thiết kế giao diện trang chủ', N'Hoàn thiện bố cục trang chủ responsive.', DATEADD(DAY, 7, GETDATE()), N'Cao', N'In Progress'),
(1, N'Xây dựng API sản phẩm', N'Tạo API danh sách và chi tiết sản phẩm.', DATEADD(DAY, 14, GETDATE()), N'Cao', N'To Do'),
(1, N'Kiểm thử luồng đặt hàng', N'Kiểm thử các trường hợp đặt hàng chính.', DATEADD(DAY, -2, GETDATE()), N'Trung bình', N'Review'),
(2, N'Phân tích yêu cầu', N'Tổng hợp yêu cầu từ các phòng ban.', DATEADD(DAY, -10, GETDATE()), N'Thấp', N'Done');

INSERT INTO dbo.PhanCong (MaTask, MaThanhVien)
VALUES (1, 1), (2, 2), (3, 3), (3, 1), (4, 2);
GO
