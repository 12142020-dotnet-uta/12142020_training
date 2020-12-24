--SELECT * from Addresses;
--SELECT * from Customers;
--SELECT * from Orders;

UPDATE Customers
SET remarks = 'Hey, Tiger!'
WHERE lastName = 'Moore1';

INSERT INTO Customers (firstName, lastName, addressID, lastOrderDate, remarks)
VALUES ('Hugo', 'Sanchez', 2, '2025-5-5', 'This can''t be correct correct?');

INSERT INTO Customers (firstName, lastName, addressID, lastOrderDate, remarks)
VALUES ('Jeff', 'Goldblum', 2, '2025-5-5', 'Can you say that this query hasn''t had an escape.');

DELETE FROM Customers
WHERE firstName = 'Hugo';



--CHALLENGES
-- 1. Modify the table. Create a constraint to prevent LasterOrderDate to be after today.
-- 2. 