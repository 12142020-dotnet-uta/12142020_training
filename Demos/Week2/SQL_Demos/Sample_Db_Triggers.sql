--'AFTER' TRIGGER
--Create a table to log all the changes made
--DROP TABLE Customer_audits;
CREATE TABLE Customer_audits
(
ChangeId INT IDENTITY PRIMARY KEY,--record a unique id of the change
CustomerId INT NOT NULL,
FirstName VARCHAR(255),
LastName VARCHAR(255),
AddressID INT,
LastOrderDate DATE,
Remarks VARCHAR(255),
UpdatedAt DATETIME NOT NULL,--record when the operation happened
Operation CHAR(3) NOT NULL,--Record what type of operation it is.
CHECK(operation = 'INS' or operation='DEL')
);
--Select * FROM Customers;

--Create the 'AFTER' Trigger
-- DROP TRIGGER WhenCustomerAdded;
CREATE TRIGGER WhenCustomerAdded
ON Customers
AFTER INSERT, DELETE
AS 
BEGIN
-- to suppress the number of rows affected 
-- messages from being returned (@@ROWCOUNT)
	SET NOCOUNT ON;
	INSERT INTO Customer_audits
	(
		CustomerId,FirstName,LastName,AddressID,
		LastOrderDate,Remarks,UpdatedAt,Operation
	)
	SELECT
		CustomerId,FirstName,LastName,AddressID,
		LastOrderDate,Remarks,GETDATE(),'INS'
	FROM
		inserted --the internal table
	UNION ALL
	SELECT
		CustomerId,FirstName,LastName,AddressID,
		LastOrderDate,Remarks,GETDATE(),'DEL'
	FROM
		deleted --the internal table
END

--test the trigger to verify that it works.
INSERT INTO Customers (FirstName, LastName, AddressID, LastOrderDate, Remarks) 
VALUES ('Test', 'Testerson', 6, '0999-12-31', 'Testing for the first millennium');

SELECT * FROM Customer_audits;


--'INSTEAD OF' TRIGGER
--Create a table to log all the changes made
CREATE TABLE Customers_pending
(
	--record a unique id of the change
	PendingChangeId INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(255),
	LastName VARCHAR(255),
	AddressID INT,
	LastOrderDate DATE,
	Remarks VARCHAR(255),
);

--Create an 'INSTEAD OF' Trigger
-- DROP TRIGGER NewCustomerAdded;
CREATE TRIGGER NewCustomerAdded
ON Customers
INSTEAD OF INSERT
AS 
BEGIN
-- to suppress the number of rows affected 
-- messages from being returned (@@ROWCOUNT)
SET NOCOUNT ON;
INSERT INTO Customers_pending
(
	FirstName,LastName,AddressID,
	LastOrderDate,Remarks
)
SELECT
	FirstName,LastName,AddressID,
	LastOrderDate,Remarks
FROM
	inserted
END

--Insert a new Customer
INSERT INTO Customers 
(FirstName, LastName, AddressID, LastOrderDate, Remarks) 
VALUES 
('Testy2', 'McTesterson', 6, '0999-12-31', 'Testing the Test of a test');
INSERT INTO Customers 
(FirstName, LastName, AddressID, LastOrderDate, Remarks) 
VALUES 
('Jerky', 'McJerkyson', 6, '1950-10-01', 'ReTesting the Test of a tested test');

--Look at the Customers_pending table
SELECT * FROM Customers_pending;
DROP TABLE Customers_pending;


--disable the triggers on a table
DISABLE TRIGGER ALL ON Customers;
ENABLE TRIGGER ALL ON Customers;
