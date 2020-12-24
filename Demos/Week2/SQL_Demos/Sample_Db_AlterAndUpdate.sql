--Change the totalAmount column to MONEY
ALTER TABLE Orders
ALTER COLUMN totalAmount MONEY;

ALTER TABLE Orders
DROP COLUMN totalAmount;

ALTER TABLE Orders
ADD totalAmount MONEY;

UPDATE Orders
SET totalAmount = 99.99
WHERE OrderId = 1;

UPDATE Addresses
SET AddressLine1 = '444 Main''s St.'
WHERE Addressid = 6;
