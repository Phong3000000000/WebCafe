-- Tạo cơ sở dữ liệu
CREATE DATABASE WEB_CAFE;

USE WEB_CAFE;

-- Tạo bảng CATEGORIES với ID là INT tự tăng
CREATE TABLE CATEGORIES (
    ID INT PRIMARY KEY IDENTITY(1,1),
    NAME NVARCHAR(100)
);

-- Tạo bảng PRODUCTS với ID là INT tự tăng và CATE_ID là INT
CREATE TABLE PRODUCTS (
    ID INT PRIMARY KEY IDENTITY(1,1),
    TENHANG NVARCHAR(100),
    DVT NVARCHAR(50),
    CATE_ID INT,
    FOREIGN KEY (CATE_ID) REFERENCES CATEGORIES(ID)
);

-- Tạo bảng USERS với ID là INT tự tăng và MANV_QUANLY là INT
CREATE TABLE USERS (
    ID INT PRIMARY KEY IDENTITY(1,1),
    HOTEN NVARCHAR(100),
    CHUCVU NVARCHAR(100),
    MANV_QUANLY INT,
    NGAYSINH DATE,
    GIOITINH NVARCHAR(10),
    CMND NVARCHAR(20),
    EMAIL NVARCHAR(100),
    ROLE NVARCHAR(50),
    DIACHI NVARCHAR(200),
    SODT NVARCHAR(20),
    USERNAME NVARCHAR(50),
    PASSWORD NVARCHAR(50),
    FOREIGN KEY (MANV_QUANLY) REFERENCES USERS(ID)
);

-- Tạo bảng ORDERS với MAKH là INT
CREATE TABLE ORDERS (
    MAHOADON INT PRIMARY KEY IDENTITY(1,1),
    MAKH INT,
    NGAYLAP DATETIME,
    TONGGIA DECIMAL(18,2),
    FOREIGN KEY (MAKH) REFERENCES USERS(ID)
);

-- Tạo bảng ORDER_DETAILS với MAHOADON và MASP là INT
CREATE TABLE ORDER_DETAILS (
    MAHOADON INT,
    MASP INT,
    SOLUONG INT,
    GIA DECIMAL(18,2),
    PRIMARY KEY (MAHOADON, MASP),
    FOREIGN KEY (MAHOADON) REFERENCES ORDERS(MAHOADON),
    FOREIGN KEY (MASP) REFERENCES PRODUCTS(ID)
);


-- Câu lệnh insert vào bảng CATEGORIES
INSERT INTO CATEGORIES (NAME)
VALUES
    (N'Cà phê'),
    (N'Trà sữa'),
    (N'Sinh tố'),
    (N'Nước ép'),
    (N'Trà Trái Cây'),
    (N'Đá Xay');

-- Câu lệnh insert vào bảng PRODUCTS
-- Lưu ý: Thay các giá trị CATE_ID theo ID tự tăng của bảng CATEGORIES
INSERT INTO PRODUCTS (TENHANG, DVT, CATE_ID)
VALUES
    (N'Cà phê đen', N'Cốc', 1),
    (N'Cà phê sữa', N'Cốc', 1),
    (N'Trà sữa truyền thống', N'Cốc', 2),
    (N'Sinh tố dưa hấu', N'Cốc', 3),
    (N'Nước ép cam', N'Cốc', 4),
    (N'Trà sữa matcha', N'Cốc', 2),
    (N'Trà Vải Lài', N'Cốc', 5),
    (N'Trà Vải Nhãn', N'Cốc', 5);

-- Câu lệnh insert vào bảng USERS
-- Lưu ý: Thay các giá trị MANV_QUANLY theo ID tự tăng của bảng USERS khi cần
INSERT INTO USERS (HOTEN, CHUCVU, MANV_QUANLY, NGAYSINH, GIOITINH, CMND, EMAIL, ROLE, DIACHI, SODT, USERNAME, PASSWORD)
VALUES
    (N'Nguyễn Văn A', N'Quản lý', NULL, '1980-01-01', N'Nữ', '012345678901', 'nguyenvana@example.com', 'ADMIN', N'cà mau', NULL, 'nguyenvana', '123456'),
    (N'Trần Thị B', N'Nhân viên pha chế', 1, '1985-02-02', N'Nữ', '023456789012', 'tranthib@example.com', 'ADMIN', NULL, NULL, 'tranthib', '123456'),
    (N'Lê Văn C', N'Nhân viên phục vụ', 1, '1990-03-03', N'Nam', '034567890123', 'levanc@example.com', 'ADMIN', NULL, NULL, 'levanc', '123456'),
    (N'Lại Văn D', N'Nhân viên phục vụ', 1, '1990-03-03', N'Nam', '034567890123', 'levanc@example.com', 'ADMIN', NULL, NULL, 'laivand', '123456'),
    (N'Bùi Thị E', N'Nhân viên pha chế', 1, '1990-03-03', N'Nam', '034567890123', 'levanc@example.com', 'ADMIN', NULL, NULL, 'buithie', '123456'),
    (N'Huỳnh Thanh Sơn', NULL, NULL, NULL, N'Nam', NULL, NULL, 'USER', N'TP.HCM', '0912345678', 'beba', '123456'),
    (N'Huỳnh Châu Khang', NULL, NULL, NULL, N'Nam', NULL, NULL, 'USER', N'Hà Nội', '0987654321', 'huynhchaukhang', '123');

-- Câu lệnh insert vào bảng ORDERS
-- Lưu ý: Thay giá trị MAKH theo ID tự tăng của bảng USERS
INSERT INTO ORDERS (MAKH, NGAYLAP, TONGGIA)
VALUES
    (6, '2024-11-10', 120000),
    (7, '2024-11-10', 150000);

-- Câu lệnh insert vào bảng ORDER_DETAILS
-- Lưu ý: Thay giá trị MASP theo ID tự tăng của bảng PRODUCTS và MAHOADON theo ID tự tăng của bảng ORDERS
INSERT INTO ORDER_DETAILS (MAHOADON, MASP, SOLUONG, GIA)
VALUES
    (1, 1, 2, 50000),
    (1, 3, 1, 60000);


-- câu lệnh select
SELECT * FROM CATEGORIES
SELECT * FROM PRODUCTS
SELECT * FROM USERS
SELECT * FROM ORDERS
SELECT * FROM ORDER_DETAILS

-- CÂU LỆNH DROP
/* DROP TABLE CATEGORIES
DROP TABLE PRODUCTS
DROP TABLE USERS
DROP TABLE ORDERS
DROP TABLE ORDER_DETAILS */

-- ALTER BỔ SUNG CHO BẢNG
    -- BỔ SUNG HÌNH ẢNH CHO PRODUCT
    ALTER TABLE PRODUCTS
    ADD HINHANH NVARCHAR(300);

    ALTER TABLE PRODUCTS
    ADD GIA FLOAT;

    UPDATE PRODUCTS
    SET HINHANH = '/images/FREEZE-TRA-XANH-5.1.png'

    UPDATE PRODUCTS
    SET GIA = 25000



-- BỔ SUNG CHO CATEGORIES
INSERT INTO CATEGORIES (NAME)
VALUES
    (N'Starter')

    INSERT INTO CATEGORIES (NAME)
VALUES
    (N'Main Dish')

    INSERT INTO CATEGORIES (NAME)
VALUES
    (N'Desserts')

    INSERT INTO CATEGORIES (NAME)
VALUES
    (N'Drinks')

        INSERT INTO CATEGORIES (NAME)
VALUES
    (N'Coffee')

    select * from CATEGORIES
    select * from USERS
    UPDATE USERS
    set ROLE = 'USER'
    WHERE ROLE is NULL
-- BỔ SUNG CHO PRODUCT
INSERT INTO PRODUCTS (TENHANG, DVT, CATE_ID, HINHANH, GIA)
VALUES
    (N'Bò Bít Tết', N'Phần', 7, '/images/bobittet.jpg', 20000),
    (N'Sườn Nướng Tảng', N'Phần', 7, '/images/suonnuongtang.jpg', 29000),
    (N'Bò Rau Củ', N'Phần', 7, '/images/boraucu.jpg', 20000),
    (N'Gà Nướng Mật Ong', N'Phần', 7, '/images/ganuongmatong.jpg', 20000),
    (N'Tôm Luộc', N'Phần', 8, '/images/tomluoc.jpg', 49910),
    (N'Cá Nướng', N'Phần', 8, '/images/canuong.jpg', 20000),
    (N'Gỏi Tôm Chua Cay', N'Phần', 8, '/images/goitom.jpg', 20000),
    (N'Cá Phi Lê Sốt Chanh Dây', N'Phần', 8, '/images/caphilesotchanhday.jpg', 20000),
    (N'Bánh Hoa Hồng', N'Phần', 9, '/images/banhhoahong.jpg', 20000),
    (N'Bánh Caramel Dâu', N'Phần', 9, '/images/banhcarameldau.jpg', 29000),
    (N'Bánh Flan Trái Cây', N'Phần', 9, '/images/banhflantraicay.jpg', 20000),
    (N'Bánh Kem Dâu Tằm', N'Phần', 9, '/images/banhkemdautam.jpg', 20000),
    (N'Bánh Socola', N'Phần', 9, '/images/banhsocola.jpg', 20000),
    (N'Bánh Trái Cây Caramel', N'Phần', 9, '/images/banhtraicaycaramel.jpg', 20000),
    (N'Old Fashion', N'Phần', 10, '/images/old fashion.jpg', 49910),
    (N'Trà Dâu', N'Phần', 10, '/images/tra dau.jpg', 20000),
    (N'Volka Chanh', N'Phần', 10, '/images/volka chanh.jpg', 20000),
    (N'Kiss Of Death', N'Phần', 10, '/images/kiss of death.jpg', 20000);

INSERT INTO PRODUCTS (TENHANG, DVT, CATE_ID, HINHANH, GIA)
VALUES
    (N'Capuchino Đậm', N'Phần', 11, '/images/capuchinodam.jpg', 25000),
    (N'Capuchino Sữa', N'Phần', 11, '/images/capuchinosua.jpg', 25000),
    (N'Cà Phê Sữa Tươi', N'Phần', 11, '/images/cafesuatuoi.jpg', 25000),
    (N'Cà Phê Kem Cháy', N'Phần', 11, '/images/cafekemchay.jpg', 25000)


select * from PRODUCTS

-- TRIGGER TỰ ĐỘNG CẬP NHẬT GIÁ
CREATE TRIGGER TRG_UpdateTotalAmount
ON ORDER_DETAILS
FOR INSERT, UPDATE, DELETE
AS
BEGIN
    -- Cập nhật tổng tiền khi có thao tác thêm hoặc cập nhật
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        UPDATE o
        SET o.TONGGIA = (
            SELECT SUM(od.SOLUONG * od.GIA) 
            FROM ORDER_DETAILS od
            WHERE od.MAHOADON = o.MAHOADON
        )
        FROM ORDERS o
        WHERE o.MAHOADON IN (SELECT DISTINCT MAHOADON FROM inserted)
    END

    -- Cập nhật tổng tiền khi có thao tác xóa
    IF EXISTS (SELECT * FROM deleted)
    BEGIN
        UPDATE o
        SET o.TONGGIA = (
            SELECT SUM(od.SOLUONG * od.GIA) 
            FROM ORDER_DETAILS od
            WHERE od.MAHOADON = o.MAHOADON
        )
        FROM ORDERS o
        WHERE o.MAHOADON IN (SELECT DISTINCT MAHOADON FROM deleted)
    END
END
