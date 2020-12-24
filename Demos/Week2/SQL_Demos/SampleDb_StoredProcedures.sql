

--Simple Stored Procedure
CREATE PROCEDURE GetAllCustomerNames
AS 
SELECT CustomerId, FirstName 
FROM Customers
WHERE addressID = 1;

EXEC GetAllCustomerNames;
DROP PROCEDURE GetAllCustomerNames;


--Stored Procedure with variables
CREATE PROCEDURE GetCustomerShe   
    @LastName nvarchar(50),   
    @FirstName nvarchar(50)   
AS     
    SELECT FirstName, LastName, Remarks  
    FROM Customers  
    WHERE FirstName = @FirstName 
	AND 
	LastName = @LastName  
    AND 
	Remarks LIKE 'S_e%';  

EXEC GetCustomerShe 
	@LastName = 'Moore', 
	@FirstName = 'Maya';
DROP PROCEDURE GetCustomerShe;