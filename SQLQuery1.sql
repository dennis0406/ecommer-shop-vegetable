create database QLNONGSAN;
USE QLNONGSAN;

CREATE TABLE Loai_nong_san (
	ma_nong_san INT identity(1,1)  NOT NULL ,
	ten_loai NVARCHAR (50) NOT NULL,
	CONSTRAINT pk_LNS PRIMARY KEY (ma_nong_san)
);


CREATE TABLE San_pham (
	ma_san_pham INT identity(1,1) NOT NULL ,
	ten_san_pham NVARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	hinh_anh varchar(100),
	ma_nong_san INT  NOT NULL,
	gia decimal(18,2),
	don_vi_tinh nvarchar(50),
	so_luong INT,
	ngay_them datetime,
	CONSTRAINT fk_SP_LNS FOREIGN KEY (ma_nong_san) REFERENCES Loai_nong_san(ma_nong_san) ON DELETE CASCADE,
	CONSTRAINT pk_SP PRIMARY KEY (ma_san_pham)
);


CREATE TABLE Khach_hang (
	ma_khach_hang INT NOT NULL identity(1,1),
	email varchar(50) not null,
	mat_khau varchar(10) not null,
	ten_khach_hang NVARCHAR (50),
	so_dien_thoai VARCHAR (20),
	dia_chi NVARCHAR (100),
	CONSTRAINT pk_KH PRIMARY KEY (ma_khach_hang)
);

CREATE TABLE Hoa_don(
	ma_hoa_don INT NOT NULL identity(1,1),
	ngay_dat_hang DATETIME,
	ma_khach_hang INT,
	tong decimal(18,2),
	trang_thai nvarchar(255),
	ghi_chu NVARCHAR (255),
	nhan_xet NVARCHAR(100),
	phi_van_chuyen decimal(18,2),
	CONSTRAINT fk_HD_KH FOREIGN KEY (ma_khach_hang) REFERENCES Khach_hang(ma_khach_hang) ON DELETE CASCADE ,
	CONSTRAINT pk_HD PRIMARY KEY (ma_hoa_don) 
);


CREATE TABLE CT_hoa_don(
	ma_CT_hoa_don INT NOT NULL identity(1,1),
	ma_san_pham INT,
	ma_hoa_don INT,
	so_luong INT,
	thanh_tien decimal(18,2),
	CONSTRAINT fk_CTHD_SP FOREIGN KEY (ma_san_pham) REFERENCES San_pham(ma_san_pham) ON DELETE CASCADE,
	CONSTRAINT fk_CTHD_HD FOREIGN KEY (ma_hoa_don) REFERENCES Hoa_don(ma_hoa_don) ON DELETE CASCADE,
	CONSTRAINT pk_CTHD PRIMARY KEY (ma_CT_hoa_don) 
);



CREATE TABLE _Admin(
	ma_admin INT NOT NULL identity(1,1),
	ten_dang_nhap NVARCHAR (50) NOT NULL,
	mat_khau VARCHAR (20),
	email varchar(50),
	CONSTRAINT pk_AD PRIMARY KEY (ma_admin)
);

-- Insert data into table

INSERT INTO Loai_nong_san
VALUES 
(N'Đồ gia vị'), -- 1
(N'Gạo'), --2
(N'Rau củ'), --3
(N'Hoa quả'), --4
(N'Đồ khô'); --5



--(N'Súp lơ',3,70000,10),
--(N'Cà rốt',3,200000,50),
--(N'Nấm rơm',3,43000,20),
--(N'Nấm kim',3,7000,90),
--(N'Tỏi',1,200000,230),
--(N'Gạo Bắc Hương',2,17000,40),
--(N'Đường',1,90000,29),
--(N'Muối',1,43000,50),
--(N'Ớt bột',1,750000,10),
--(N'Cam',4,430000,50),
--(N'quýt',4,63000,30),
--(N'Táo',4,350000,10),
--(N'Mận',4,430000,50);

Insert into Khach_hang 
values
('mylinh@gmail.com','123456',N'Nguyễn Thị Mỹ Linh',0986542312,N'Quảng Bình'),
('phuong@gmail.com','123456',N'Hồ Thị Phượng',0865421532,N'Quảng Trị'),
('ngoclinh@gmail.com','123456',N'Nguyễn Thị Ngọc Linh',08886351243,N'Quảng Bình'),
('tuyetgiang@gmail.com','123456',N'Nguyễn Thị Tuyết Giang',0896575416,N'Quảng Nam');

Insert into _Admin 
values
('laple','1234','vanlap1702@gmail.com')


select * from Hoa_don
 

