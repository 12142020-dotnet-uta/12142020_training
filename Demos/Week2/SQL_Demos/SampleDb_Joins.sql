
--SELECT * from Addresses;
--SELECT * from Customers;
--SELECT * from Orders;

--Get all customers with addresses (RIGHT JOIN)
SELECT FirstName, LastName, Addresses.City, Addresses.AddressLine1, Addresses.Addressid
FROM Customers
RIGHT JOIN Addresses 
ON Customers.AddressID = Addresses.Addressid
WHERE Addresses.City = 'Crowley'
ORDER BY lastName;

--Get all customers with addresses (LEFT JOIN)
SELECT FirstName, LastName, Addresses.AddressLine1, Addresses.City
FROM Customers
LEFT JOIN Addresses 
ON Customers.AddressID = Addresses.AddressID
WHERE Addresses.AddressLine1 = '444 Main''s St.'
ORDER BY LastName;

--Get all customers with addresses (INNER JOIN)
SELECT FirstName, LastName, Addresses.City
FROM Customers
INNER JOIN Addresses 
ON Customers.AddressID = Addresses.Addressid
WHERE Addresses.City = 'Fort Worth, TX'
ORDER BY LastName;

--CROSS JOIN
SELECT Addresses.Addressid, AddressLine1, FirstName
FROM Addresses CROSS JOIN Customers;