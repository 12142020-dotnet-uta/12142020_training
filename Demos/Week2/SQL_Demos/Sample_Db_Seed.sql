
--SELECT * from Addresses;
--SELECT * from Customers;
--SELECT * from Orders;

--drop in this order because of FK References
--DROP TABLE Orders;
--DROP TABLE Customers;
--DROP TABLE Addresses;

--TRUNCATE TABLE Orders;
--TRUNCATE TABLE Customers;
--TRUNCATE TABLE Addresses;

CREATE DATABASE Sample_DB;

CREATE TABLE Addresses(
Addressid INT PRIMARY KEY IDENTITY,
AddressLine1 varchar(50) NOT NULL,
AddressLine2 varchar(50) NULL,
City varchar(100) NULL,
CountryCode char(3) NULL);

CREATE TABLE Customers(
CustomerId INT PRIMARY KEY IDENTITY,
FirstName varchar(50) NOT NULL,
LastName varchar(50) NOT NULL,
AddressID INT NOT NULL FOREIGN KEY REFERENCES Addresses(Addressid) ON DELETE CASCADE,
LastOrderDate date NULL,
Remarks varchar(250) NULL);

CREATE TABLE Orders(
OrderId INT PRIMARY KEY IDENTITY NOT NULL,
CustomerID INT NOT NULL FOREIGN KEY REFERENCES Customers(CustomerId) ON DELETE CASCADE,
OrderDate date NOT NULL,
totalAmount MONEY NOT NULL);

--insert to tables in this order because of FK References
INSERT INTO Addresses (AddressLine1, AddressLine2, City, CountryCode) 
VALUES ('123 MAIN St.', NULL, 'Fort Worth, TX', 'USA');
INSERT INTO Addresses (AddressLine1, AddressLine2, City, CountryCode) 
VALUES ('321 MAIN St.', NULL, 'Dallas, TX', 'USA');
INSERT INTO Addresses (AddressLine1, AddressLine2, City, CountryCode) 
VALUES ('444 Main''s St.', NULL, 'Dallas, TX', 'USA');

INSERT INTO Customers (FirstName, LastName, AddressID, LastOrderDate, Remarks) 
VALUES ('Matt', 'Moore', 1, '2020-09-13', 'test1');
INSERT INTO Customers (FirstName, LastName, AddressID, LastOrderDate, Remarks) 
VALUES ('Libby', 'Moore', 2, '2020-09-15', 'test2');
INSERT INTO Customers (FirstName, LastName, AddressID, LastOrderDate, Remarks) 
VALUES ('Jeff', 'Moore', 3, null, 'test3');

INSERT INTO Orders (CustomerID, OrderDate, totalAmount) 
VALUES (2, '2020-09-13', 101.00);
INSERT INTO Orders (CustomerID, OrderDate, totalAmount) 
VALUES (1, '2020-09-4', 51);
INSERT INTO Orders (CustomerID, OrderDate, totalAmount) 
VALUES (3, '1979-09-13', 41.31);




