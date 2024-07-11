CREATE DATABASE QLyBanDienThoai
GO 
USE QLyBanDienThoai
GO
CREATE TABLE tblAccount
(
	username NVARCHAR(20) PRIMARY KEY,
	passwordd NVARCHAR(20),
	rolee NVARCHAR(20),
	id int identity
);
GO
CREATE TABLE tblEmployee 
(	
	idEmployee INT PRIMARY KEY IDENTITY,
	username NVARCHAR(20) FOREIGN KEY REFERENCES tblAccount(username),
	fullName NVARCHAR(50),
	gender NVARCHAR(3),
	birthday DATE,
	adress NVARCHAR(50),
	phoneNumber VARCHAR(10)
);
GO
CREATE TABLE tblCustomer 
(	
	idCustomer INT PRIMARY KEY IDENTITY,
	fullName NVARCHAR(50),
	birthday DATE,
	adress NVARCHAR(50),
	phoneNumber VARCHAR(10),
	gender NVARCHAR(3)
);
GO
CREATE TABLE tblDevice
(
	idDevice INT PRIMARY KEY IDENTITY,
	deviceName NVARCHAR(30),
	screen VARCHAR(30),
	operatingSystem NVARCHAR(30),
	camera VARCHAR(20),
	CPU VARCHAR(30),
	RAM VARCHAR(10),
	memory VARCHAR(10),
	SIM BIT,
	battery VARCHAR(10),
	color NVARCHAR(15),
	price FLOAT	
);
GO
CREATE TABLE tblBill
(	
	idBill INT PRIMARY KEY IDENTITY,
	idCustomer INT FOREIGN KEY REFERENCES tblCustomer(idCustomer),
	idEmployee INT FOREIGN KEY REFERENCES tblEmployee(idEmployee),
	datee DATE,
	totalPrice FLOAT,
	statuss BIT,
	amount INT
);
GO
CREATE TABLE tblWarranty
(	
	idWarranty INT PRIMARY KEY IDENTITY,
	typee NVARCHAR(30),
	dateEnd DATE,
	support NVARCHAR(50),
	note NVARCHAR(50),
	timeWarranty INT,
	dateStart DATE
);
CREATE VIEW v_Warranty
AS
SELECT idWarranty,datee
FROM tblBill
INNER JOIN tblBillDetails ON tblBill.idBill = tblBillDetails.idBill
GROUP BY idWarranty,datee;

UPDATE tblWarranty
SET timeWarranty = DATEDIFF(Month,dateStart,dateEnd);
---------------------------------------------------------------------------HIẾU----------------------------------
DECLARE DATESTARTCURSOR CURSOR FOR SELECT idWarranty FROM tblWarranty

OPEN DATESTARTCURSOR
DECLARE @idWarranty INT

FETCH NEXT FROM DATESTARTCURSOR INTO @idWarranty
WHILE @@FETCH_STATUS=0
BEGIN
	UPDATE tblWarranty
	SET dateStart = (SELECT datee FROM v_Warranty WHERE v_Warranty.idWarranty = @idWarranty) WHERE tblWarranty.idWarranty = @idWarranty
	FETCH NEXT FROM DATESTARTCURSOR INTO @idWarranty
END
CLOSE DATESTARTCURSOR
DEALLOCATE DATESTARTCURSOR;
UPDATE tblWarranty
SET dateStart = (SELECT datee FROM ((tblWarranty INNER JOIN tblBillDetails ON (tblWarranty.idWarranty = tblBillDetails.idWarranty) INNER JOIN tblBill ON(tblBillDetails.idBill = tblBill.idBill))));
-------------------------------------HIẾU----------------------------------------------------------
----------------------HƠN------------------------------------------
--UPDATE tblWarranty
--SET dateStart = b.datee
--FROM tblWarranty w
--INNER JOIN tblBillDetails bd ON w.idWarranty = bd.idWarranty
--INNER JOIN tblBill b ON bd.idBill = b.idBill;
----------------------HƠN------------------------------------------
--------------------TUẤN---------------------------------------------
UPDATE tblWarranty
SET dateStart = (SELECT datee FROM ((tblWarranty INNER JOIN tblBillDetails ON (tblWarranty.idWarranty = tblBillDetails.idWarranty) INNER JOIN tblBill ON(tblBillDetails.idBill = tblBill.idBill))));
--------------------TUẤN---------------------------------------------
GO
CREATE TABLE tblBillDetails
(	
	idBillDetails INT PRIMARY KEY IDENTITY,
	idBill INT FOREIGN KEY REFERENCES tblBill(idBill),
	idDevice INT FOREIGN KEY REFERENCES tblDevice(idDevice),
	idWarranty INT FOREIGN KEY REFERENCES tblWarranty(idWarranty),
	price FLOAT,
	VAT FLOAT,
	sale float
); 
--price = ((1-sale)*price(tblDevice))*(1+VAT)

--INSERT 
INSERT INTO tblAccount
VALUES
('user1', 'password1', N'Quản lý'),
('user2', 'password2', N'Quản lý'),
('user3', 'password3', N'Nhân viên'),
('user4', 'password4', N'Nhân viên'),
('user5', 'password5', N'Nhân viên'),
('user6', 'password6', N'Nhân viên'),
('user7', 'password7', N'Nhân viên'),
('user8', 'password8', N'Nhân viên'),
('user9', 'password9', N'Nhân viên'),
('user10', 'password10', N'Nhân viên');

INSERT INTO tblEmployee 
VALUES
( 'user1', N'Nguyễn Lam Phương', N'Nam', '1990-05-15', N'Quận 7,TP.HCM', '0123456789'),
( 'user2', N'Trần Thu Ngọc', N'Nữ', '1985-09-20', N'Long Biên ,Hà Nội', '0234567890'),
( 'user3', N'Lê Gia Vinh', N'Nam', '1988-03-10', N'Quận 3, TP.HCM', '0345678901'),
( 'user4', N'Phạm Phương Chi', N'Nữ', '1992-07-25', N'Từ Sơn, Bắc Ninh', '0456789012'),
( 'user5', N'Hoàng Đức Tuấn', N'Nam', '1980-12-05', N'Đông Hưng, Thái Bình', '0567890123'),
( 'user6', N'Đặng Thục Nhi', N'Nữ', '1995-01-30', N'Hoàng Mai, Hà Nội', '0678901234'),
( 'user7', N'Võ Hữu Bảo', N'Nam', '1983-11-18', N'Hoa Lư, Ninh Bình', '0789012345'),
( 'user8', N'Ngô Thúy Trang', N'Nữ', '1998-04-22', N'Kim Bôi, Hòa Bình', '0890123456'),
( 'user9', N'Mai Minh Đức', N'Nam', '1993-08-12', N'Đồ Sơn, Hải Phòng', '0901234567'),
( 'user10', N'Lý Mai Hiền', N'Nữ', '1987-06-08', N'Yên Phong, Bắc Ninh', '0912345678');
GO 
INSERT INTO tblCustomer 
VALUES
( N'Trần Thị An', '1990-05-15', N'Ba Đình, Hà Nội', '0123456789', N'Nữ'),
( N'Nguyễn Vũ Dũng', '1985-09-20', N'An Khê, Gia Lai', '0234567890', N'Nam'),
(N'Lê Thị Cúc', '1988-03-10', N'Long Biên, Hà Nội', '0345678901', N'Nữ'),
(N'Phạm Văn Long', '1992-07-25', N'Long Biên, Hà Nội', '0456789012', N'Nam'),
( N'Nguyễn Thị Hải Anh', '1980-12-05', N'Long Biên, Hà Nội', '0567890123', N'Nữ'),
( N'Ngô Hồng Phong', '1995-01-30', N'An Khê, Gia Lai', '0678901234', N'Nam'),
( N'Mai Thị Hạ', '1983-11-18', N'An Khê, Gia Lai', '0789012345', N'Nữ'),
( N'Lê Văn Khoa', '1998-04-22', N'An Khê, Gia Lai', '0890123456', N'Nam'),
( N'Trần Thị Liên', '1993-08-12', N'Ba Đình, Hà Nội', '0901234567', N'Nữ'),
(N'Vũ Văn Minh', '1987-06-08', N'Ba Đình, Hà Nội', '0912345678', N'Nam');
go

INSERT INTO tblBill
VALUES
( 1, 1, '2024-03-01', 1000000, 1, 0),
( 2, 2, '2024-03-02', 1500000, 1, 0),
( 3, 3, '2024-03-03', 800000, 0, 0),
( 4, 4, '2024-03-04', 2000000, 1, 0),
( 5, 5, '2024-03-05', 300000, 0, 0),
( 6, 6, '2024-03-06', 1200000, 1, 0),
( 7, 7, '2024-03-07', 700000, 0, 0),
( 8, 8, '2024-03-08', 900000, 1, 0),
( 9, 9, '2024-03-09', 1100000, 1, 0),
( 10, 10, '2024-03-10', 500000, 0, 0);

GO 
INSERT INTO tblDevice 
VALUES
( N'Samsung Galaxy S21 Ultra', 'Dynamic AMOLED', N'Android', '108 MP', 'Exynos 2100', '12 GB', '256 GB', 1, '5000 mAh', N'Đen Bóng', 1299.99),
( N'iPhone 13 Pro Max', 'Super Retina XDR OLED', N'iOS', '12 MP', 'A15 Bionic', '6 GB', '512 GB', 0, '4352 mAh', N'Bạc', 1399.99),
(N'Google Pixel 6 Pro', 'OLED', N'Android', '50 MP', 'Google Tensor', '12 GB', '128 GB', 1, '5000 mAh', N'Đen', 899.99),
(N'OnePlus 9 Pro', 'Fluid AMOLED', N'Android', '48 MP', 'Snapdragon 888', '8 GB', '128 GB', 1, '4500 mAh', N'Xám', 969.99),
( N'Xiaomi Mi 11 Ultra', 'AMOLED', N'Android', '50 MP', 'Snapdragon 888', '12 GB', '256 GB', 1, '5000 mAh', N'Trắng', 1199.99),
( N'Samsung Galaxy Z Fold 3', 'Dynamic AMOLED', N'Android', '12 MP', 'Snapdragon 888', '12 GB', '256 GB', 1, '4400 mAh', N'Xanh Bóng', 1799.99),
( N'iPhone 13 Mini', 'Super Retina XDR OLED', N'iOS', '12 MP', 'A15 Bionic', '4 GB', '128 GB', 0, '2438 mAh', N'Ánh Sáng', 699.99),
( N'Google Pixel 5a', 'OLED', N'Android', '12.2 MP', 'Snapdragon 765G', '6 GB', '128 GB', 1, '4680 mAh', N'Đỏ', 449.99),
( N'OnePlus Nord 2', 'AMOLED', N'Android', '50 MP', 'MediaTek Dimensity 1200', '12 GB', '256 GB', 1, '4500 mAh', N'Xám', 499.99),
( N'Xiaomi Redmi Note 11 Pro+', 'AMOLED', N'Android', '108 MP', 'Snapdragon 695', '8 GB', '256 GB', 1, '5000 mAh', N'Xanh Dương', 399.99),
( N'Samsung Galaxy A52s 5G', 'Super AMOLED', N'Android', '64 MP', 'Snapdragon 778G', '6 GB', '128 GB', 1, '4500 mAh', N'Đen', 499.99),
( N'iPhone SE (2022)', 'Retina IPS LCD', N'iOS', '12 MP', 'A15 Bionic', '4 GB', '256 GB', 0, '2015 mAh', N'Đỏ', 599.99),
( N'Google Pixel 4a', 'OLED', N'Android', '12.2 MP', 'Snapdragon 730G', '6 GB', '128 GB', 1, '3140 mAh', N'Đen', 349.99),
( N'OnePlus 8T', 'Fluid AMOLED', N'Android', '48 MP', 'Snapdragon 865', '8 GB', '128 GB', 1, '4500 mAh', N'Đen', 599.99),
( N'Xiaomi Poco X3 Pro', 'IPS LCD', N'Android', '48 MP', 'Snapdragon 860', '6 GB', '128 GB', 1, '5160 mAh', N'Đen', 249.99),
( N'Samsung Galaxy M52 5G', 'Super AMOLED Plus', N'Android', '64 MP', 'Snapdragon 778G', '8 GB', '128 GB', 1, '5000 mAh', N'Đen', 429.99),
( N'iPhone 12', 'Super Retina XDR OLED', N'iOS', '12 MP', 'A14 Bionic', '4 GB', '128 GB', 0, '2815 mAh', N'Xanh Lam', 799.99),
( N'Google Pixel 6', 'OLED', N'Android', '50 MP', 'Google Tensor', '8 GB', '128 GB', 1, '4614 mAh', N'Đen', 699.99),
( N'OnePlus 9R', 'Fluid AMOLED', N'Android', '48 MP', 'Snapdragon 870', '8 GB', '128 GB', 1, '4500 mAh', N'Đen', 699.99),
( N'Xiaomi Redmi Note 11', 'IPS LCD', N'Android', '50 MP', 'MediaTek Helio G88', '4 GB', '64 GB', 1, '5000 mAh', N'Đen', 199.99);
GO
INSERT INTO tblWarranty 
VALUES
(N'Bảo hành bình thường', '2024-12-31', N'Trung tâm bảo hành chính hăng', N'Không ghi chú',null,null),
(N'Bảo hành vàng', '2025-06-30', N'Trung tâm bảo hành chính hăng', N'Thời gian bảo hành mở rộng thêm 6 tháng',null,null),
(N'Bảo hành bình thường', '2024-09-30', N'Trung tâm bảo hành chính hăng', N'Không ghi chú',null,null),
(N'Bảo hành bình thường', '2025-03-31', N'Trung tâm bảo hành chính hăng', N'Không ghi chú',null,null),
(N'Bảo hành bình thường', '2024-11-30', N'Trung tâm bảo hành chính hăng', N'Không ghi chú',null,null),
(N'Bảo hành vàng', '2025-04-30', N'Trung tâm bảo hành chính hăng', N'Thời gian bảo hành mở rộng thêm 4 tháng',null,null),
(N'Bảo hành bình thường', '2024-08-31', N'Trung tâm bảo hành chính hăng', N'Không ghi chú',null,null),
(N'Bảo hành bình thường', '2025-05-31', N'Trung tâm bảo hành chính hăng', N'Không ghi chú',null,null),
(N'Bảo hành bình thường', '2024-10-31', N'Trung tâm bảo hành chính hăng', N'Không ghi chú',null,null),
(N'Bảo hành bình thường', '2025-01-31', N'Trung tâm bảo hành chính hăng', N'Không ghi chú',null,null);

GO
INSERT INTO tblBillDetails
VALUES
( 1, 1, 1, 0, 0.1, 0.15),
( 2, 2, 2, 0, 0, 0.12),
( 3, 4, 3, 0, 0.1, 0),
( 4, 7, 4, 0, 0.1, 0.1),
( 5, 11, 5, 0, 0.1, 0.12),
( 6, 15, 6, 0, 0.1, 0),
( 7, 19, 7, 0, 0.1, 0),
( 8, 1, 8, 0, 0.1, 0.15),
( 9, 5, 10, 0, 0.05, 0.1),
( 10, 9, 9, 0, 0.05, 0.15);

GO
UPDATE tblBillDetails
SET price = ROUND((1+VAT)*((1-sale)*(SELECT tblDevice.price FROM tblDevice WHERE tblBillDetails.idDevice= tblDevice.idDevice)),2)
GO 
UPDATE tblBill 
SET totalPrice = (SELECT SUM(price) FROM tblBillDetails WHERE tblBill.idBill= tblBillDetails.idBill)
SELect * from tblBillDetails
UPDATE tblBill 
SET amount = (SELECT COUNT(*) FROM tblBillDetails WHERE tblBillDetails.idBill = tblBill.idBill)

--UPDATE tblBillDetails
--SET price = ROUND((1 + VAT) * ((1 - sale) * d.price), 2)
--FROM tblBillDetails bd
--JOIN tblDevice d ON bd.idDevice = d.idDevice;
--UPDATE tblBill
--SET totalPrice = (SELECT SUM(price) FROM tblBillDetails WHERE tblBill.idBill = tblBillDetails.idBill),
--    amount = (SELECT COUNT(*) FROM tblBillDetails WHERE tblBillDetails.idBill = tblBill.idBill);
--CREATE TRIGGER tr_UpdatePriceOnVATSaleChange
--ON tblBillDetails
--AFTER UPDATE
--AS
--BEGIN
--    UPDATE b
--    SET price = ROUND((1 - i.sale) * (1 + i.VAT) * d.price, 2)
--    FROM tblBillDetails b
--    INNER JOIN inserted i ON b.idBillDetails = i.idBillDetails
--    INNER JOIN tblDevice d ON b.idDevice = d.idDevice
--    WHERE b.idBillDetails IN (SELECT idBillDetails FROM inserted)
--END;

--CREATE TRIGGER UpdateSaleValue
--ON tblBillDetails
--AFTER UPDATE
--AS
--BEGIN
--    DECLARE @SaleValue DECIMAL(10, 3)
--    SELECT @SaleValue = sale
--    FROM inserted
--    IF @SaleValue > 1
--    BEGIN
--        UPDATE tblBillDetails
--        SET sale = 1
--        WHERE idBillDetails IN (SELECT idBillDetails FROM inserted)
--    END
--END;

CREATE PROC procThemTK
@username VARCHAR(20),
@passwordd VARCHAR(20)
AS
INSERT INTO tblAccount(username,passwordd,rolee)
VALUES(@username,@passwordd,N'Nhân viên');

CREATE VIEW v_TK
AS
SELECT id AS [STT], username AS [Tài khoản],passwordd AS [Mật khấu] 
FROm tblAccount;

CREATE PROC XoaTK
@username VARCHAR(20)
AS
DELETE tblAccount
WHERE username = @username;

CREATE PROC DoiMK
@username VARCHAR(20),
@passwordd VARCHAR(20)
AS
UPDATE tblAccount
SET passwordd = @passwordd WHERE username=@username;

CREATE VIEW v_NV
AS
SELECT idEmployee AS [Mã nhân viên], username AS [Tài khoản] ,fullName [Họ tên], gender AS [Giới tính],convert(varchar(10),birthday,103) AS [Ngày sinh], adress AS [Địa chỉ], phoneNumber AS [SDT]
FROM tblEmployee ;
SELECT * FROM v_NV;

CREATE PROC procThemNhanVien
@username VARCHAR(20),
@fullName NVARchAR(50),
@gender NVARChAR(10),
@birthday DATE,
@adress NVARCHAR(50),
@phoneNumber VARCHAR(10)
AS
INSERT INTO tblEmployee(username,fullName,gender,birthday,adress,phoneNumber)
VALUES(@username,@fullName,@gender,@birthday,@adress,@phoneNumber);

CREATE PROC procXoaNV
@idEmployee INT
AS
DELETE FROM tblEmployee WHERE tblEmployee.idEmployee = @idEmployee;
select * from tblAccount, tblEmployee WHERE tblAccount.username = 'user1' and tblAccount.username = tblEmployee.username;

CREATE PROC procSuaNV
@idEmployee INT,
@fullName NVARchAR(50),
@gender NVARChAR(10),
@birthday DATE,
@adress NVARCHAR(50),
@phoneNumber VARCHAR(10)
AS
UPDATE tblEmployee
SET fullname = @fullName,
gender = @gender,
birthday = @birthday,
adress = @adress,
phoneNumber = @phoneNumber
WHERE idEmployee = @idEmployee;

CREATE VIEW v_KhachHang
AS
SELECT idCustomer AS [Mã khách hàng],fullName AS [Họ tên], gender AS [Giới tính],convert(varchar(10),birthday,103) AS [Ngày sinh], adress AS [Địa chỉ], phoneNumber AS [SDT]
FROM tblCustomer
go
SELECT * FROM v_KhachHang;

CREATE PROC procThemKhachHang
@fullName NVARCHAR(50),
@gender NVARCHAR(3),
@birthday DATE,
@adress NVARCHAR(50),
@phoneNumber VARCHAR(10)
AS
INSERT INTO tblCustomer(fullName,gender,birthday,adress,phoneNumber)
VALUES(@fullName,@gender,@birthday,@adress,@phoneNumber);

CREATE PROC procXoaKhachHang
@idCustomer INT
AS
DELETE FROM tblCustomer
WHERE idCustomer = @idCustomer;

CREATE PROC procSuaKh
@idCustomer INT,
@fullName NVARCHAR(50),
@gender NVARCHAR(3),
@birthday DATE,
@adress NVARCHAR(50),
@phoneNumber VARCHAR(10)
AS
UPDATE tblCustomer
SET fullName=@fullName,
gender=@gender,
birthday=@birthday,
adress=@adress,
phoneNumber=@phoneNumber
WHERE idCustomer=@idCustomer;

CREATE VIEW v_tblWarranty
AS
SELECT
	tblWarranty.idWarranty AS [Mã bảo hành],
	tblWarranty.typee AS [Loại bảo hành],
	tblWarranty.timeWarranty AS [Thời gian bảo hành],
	convert(varchar(10),tblWarranty.dateEnd,103) AS [Ngày kết thúc bảo hành],
	tblWarranty.support AS [Hệ thống hỗ trợ],
	tblWarranty.note AS [Ghi chú]
FROM tblWarranty;
SELECT * FROM v_tblWarranty;

CREATE PROC sp_AddWarranty
@typee NVARCHAR(30),
@support NVARCHAR(50),
@note NVARCHAR(50),
@timeWarranty INT
AS
INSERT INTO tblWarranty VALUES(@typee,null,@support,@note,@timeWarranty,null);
---
INSERT INTO tblWarranty VALUES('1',null,'1','1',1,null);

CREATE PROC sp_DeleteWarranty
@idWarranty INT
AS
DELETE FROM tblWarranty WHERE tblWarranty.idWarranty = @idWarranty;
go
CREATE PROC sp_UpdateWarranty
    @idWarranty INT,
    @typee NVARCHAR(30),
    @support NVARCHAR(50),
    @note NVARCHAR(50)
AS
UPDATE tblWarranty
SET 
    typee = @typee,
    support = @support,
    note = @note
WHERE
    idWarranty = @idWarranty;

CREATE TRIGGER trig_up_tblWarranty
ON tblBillDetails
FOR INSERT,UPDATE
AS
BEGIN
	DECLARE cur_warranty_bill CURSOR FOR SELECT * FROM v_Warranty
	OPEN cur_warranty_bill
	DECLARE @idWarranty INT,@datee DATE
	FETCH NEXT FROM cur_warranty_bill INTO @idWarranty,@datee
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		UPDATE [dbo].tblWarranty
		SET dateStart = @datee
		WHERE idWarranty = @idWarranty
		FETCH NEXT FROM cur_warranty_bill INTO @idWarranty,@datee
	END
	CLOSE cur_warranty_bill
	DEALLOCATE cur_warranty_bill
	UPDATE tblWarranty
	SET dateEnd = DATEADD(MONTH,tblWarranty.timeWarrantly,tblWarranty.dateStart);
END;

--CREATE TRIGGER trig_up_tblWarranty
--ON tblBillDetails
--AFTER INSERT, UPDATE
--AS
--BEGIN
--    -- Update dateStart for tblWarranty
--    UPDATE w
--    SET dateStart = v.datee
--    FROM tblWarranty w
--    INNER JOIN inserted i ON w.idWarranty = i.idWarranty
--    INNER JOIN v_Warranty v ON i.idWarranty = v.idWarranty;

--    -- Update dateEnd for tblWarranty
--    UPDATE w
--    SET dateEnd = DATEADD(MONTH, w.timeWarranty, w.dateStart)
--    FROM tblWarranty w
--    INNER JOIN inserted i ON w.idWarranty = i.idWarranty;
--END;


CREATE VIEW v_HoaDon
AS
SELECT idBill AS [Mã hóa đơn], tblBill.idCustomer AS[Mã khách hàng] ,tblCustomer.fullName AS[Tên khách hàng], tblBill.idEmployee AS[Mã nhân viên], 
tblEmployee.fullName AS[Tên nhân viên], convert(varchar(10),tblBill.datee,103) AS[Ngày lập],totalPrice AS[Tổng tiền], CASE WHEN statuss = 1 THEN N'Đã thanh toán' ELSE N'Chưa thanh toán' END AS[Trạng thái],amount AS[Số lượng]
FROM tblBill, tblCustomer, tblEmployee 
WHERE tblBill.idCustomer = tblCustomer.idCustomer  
AND tblBill.idEmployee = tblEmployee.idEmployee;

SELECT * FROM v_HoaDon;
SELECT * FROM v_HoaDon WHERE 1=1 AND v_HoaDon.[Tổng tiền] BETWEEN 1000 AND 4000;

CREATE VIEW v_CTHD
AS
SELECT idBillDetails AS [Mã chi tiết hóa đơn],
idBill AS [Mã hóa đơn], d.idDevice AS [Mã điện thoại]
,deviceName AS [Tên điện thoại],w.timeWarranty AS [Bảo hành (tháng)] ,
typee AS [Loại bảo hành],
bd.price AS [Thành tiền]
,VAT AS [Thuế VAT],
sale AS [Giảm giá] 
FROM tblBillDetails bd 
INNER JOIN tblDevice d ON d.idDevice= bd.idDevice 
INNER JOIN tblWarranty w ON w.idWarranty=bd.idWarranty;

CREATE PROC LayDataBaoCao
@mahoadon int
AS
SELECT datee, b.idBill,e.fullName AS [tennv],e.idEmployee,c.fullName AS [tenkhach],c.adress,c.phoneNumber,bd.idDevice,d.deviceName,wr.timeWarranty,VAT,sale,bd.price,totalPrice,CASE WHEN statuss = 1 THEN N'Ðã thanh toán' ELSE N'Chưa thanh toán' END AS [trangthai] FROM tblBill b
INNER JOIN tblEmployee e ON b.idEmployee=e.idEmployee
INNER JOIN tblCustomer c ON b.idCustomer=c.idCustomer
INNER JOIN tblBillDetails bd ON b.idBill=bd.idBill
INNER JOIN tblWarranty wr ON wr.idWarranty = bd.idWarranty
INNER JOIN tblDevice d ON bd.idDevice=d.idDevice
WHERE bd.idBill=@mahoadon;

SELECT * FROM tblBillDetails;



SELECT * FROM tblWarranty;

CREATE TRIGGER trgUpdateBillDetails
ON tblBillDetails
AFTER INSERT
AS
BEGIN
	DECLARE @VAT FLOAT
	DECLARE @sale FLOAT
	SELECT @VAT = VAT FROM inserted
	SELECT @sale = sale FROM inserted
	UPDATE tblBillDetails
	SET price = ROUND((1+@VAT)*((1-@sale)*(SELECT tblDevice.price FROM tblDevice WHERE tblBillDetails.idDevice= tblDevice.idDevice)),2)
END;

CREATE TRIGGER trgUpdateBill
ON tblBillDetails
AFTER INSERT
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill= idBill FROM inserted
	UPDATE tblBill 
	SET totalPrice = (SELECT SUM(price) FROM tblBillDetails WHERE tblBill.idBill= idBill) WHERE tblBill.idBill = @idBill
END;

CREATE TRIGGER UpDateDatestart
ON tblBillDetails
AFTER INSERT
AS
BEGIN
DECLARE @idWarranty INT
SELECT @idWarranty = idWarranty FROM inserted
	UPDATE tblWarranty
	SET dateStart = (SELECT datee FROM tblBillDetails,tblBill WHERE tblBillDetails.idBill = tblBill.idBill and tblBillDetails.idWarranty = tblWarranty.idWarranty) WHERE tblWarranty.idWarranty = @idWarranty
	UPDATE tblWarranty
SET dateEnd = DATEADD(Month,timeWarranty,dateStart)
END;

CREATE TRIGGER trgAmountBill
ON tblBillDetails
AFTER INSERT
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill=idBill FROM inserted
	UPDATE tblBill 
SET amount = (SELECT COUNT(*) FROM tblBillDetails WHERE tblBillDetails.idBill = tblBill.idBill) WHERE tblBill.idBill = @idBill
END;

INSERT INTO tblBillDetails (idBill, idDevice, idWarranty, price, VAT, sale)
VALUES (12, 20, 16, 0, 0, 0);


---- View v_HoaDon
--CREATE VIEW v_HoaDon
--AS
--SELECT 
--    b.idBill AS [Mã hóa đơn], 
--    b.idCustomer AS [Mã khách hàng], 
--    c.fullName AS [Tên khách hàng], 
--    b.idEmployee AS [Mã nhân viên], 
--    e.fullName AS [Tên nhân viên], 
--    CONVERT(varchar(10), b.datee, 103) AS [Ngày lập],
--    b.totalPrice AS [Tổng tiền],
--    CASE 
--        WHEN b.statuss = 1 THEN N'Đã thanh toán' 
--        ELSE N'Chưa thanh toán' 
--    END AS [Trạng thái],
--    b.amount AS [Số lượng]
--FROM tblBill b
--INNER JOIN tblCustomer c ON b.idCustomer = c.idCustomer
--INNER JOIN tblEmployee e ON b.idEmployee = e.idEmployee;

---- View v_CTHD
--CREATE VIEW v_CTHD
--AS
--SELECT 
--    bd.idBillDetails AS [Mã chi tiết hóa đơn],
--    bd.idBill AS [Mã hóa đơn], 
--    d.idDevice AS [Mã điện thoại],
--    d.deviceName AS [Tên điện thoại], 
--    w.timeWarranty AS [Bảo hành (tháng)],
--    w.typee AS [Loại bảo hành],
--    bd.price AS [Thành tiền],
--    bd.VAT AS [Thuế VAT],
--    bd.sale AS [Giảm giá] 
--FROM tblBillDetails bd 
--INNER JOIN tblDevice d ON bd.idDevice = d.idDevice 
--INNER JOIN tblWarranty w ON bd.idWarranty = w.idWarranty;

---- Trigger trgUpdateBillDetails
--CREATE TRIGGER trgUpdateBillDetails
--ON tblBillDetails
--AFTER INSERT
--AS
--BEGIN
--    UPDATE bd
--    SET price = ROUND((1 + bd.VAT) * ((1 - bd.sale) * d.price), 2)
--    FROM tblBillDetails bd
--    INNER JOIN tblDevice d ON bd.idDevice = d.idDevice
--    INNER JOIN inserted i ON bd.idBillDetails = i.idBillDetails;
--END;

---- Trigger trgUpdateBill
--CREATE TRIGGER trgUpdateBill
--ON tblBillDetails
--AFTER INSERT
--AS
--BEGIN
--    UPDATE b
--    SET totalPrice = (
--        SELECT SUM(price) 
--        FROM tblBillDetails 
--        WHERE idBill = b.idBill
--    )
--    FROM tblBill b
--    INNER JOIN inserted i ON b.idBill = i.idBill;
--END;

---- Trigger UpDateDatestart
--CREATE TRIGGER UpDateDatestart
--ON tblBillDetails
--AFTER INSERT
--AS
--BEGIN
--    UPDATE w
--    SET dateStart = i.datee
--    FROM tblWarranty w
--    INNER JOIN inserted i ON w.idWarranty = i.idWarranty;
    
--    UPDATE w
--    SET dateEnd = DATEADD(MONTH, w.timeWarranty, w.dateStart)
--    FROM tblWarranty w
--    INNER JOIN inserted i ON w.idWarranty = i.idWarranty;
--END;

---- Trigger trgAmountBill
--CREATE TRIGGER trgAmountBill
--ON tblBillDetails
--AFTER INSERT
--AS
--BEGIN
--    UPDATE b
--    SET amount = (
--        SELECT COUNT(*) 
--        FROM tblBillDetails 
--        WHERE idBill = b.idBill
--    )
--    FROM tblBill b
--    INNER JOIN inserted i ON b.idBill = i.idBill;
--END;


 alter table tblEmployee
 add CCCD varchar(12);
-- alter table tblEmployee alter column CCCD  unique;
