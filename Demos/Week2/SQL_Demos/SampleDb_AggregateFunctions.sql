--Get average of oder totals
SELECT AVG(totalAmount) AS 'This is the average of order totals'
FROM Orders;

SELECT Count(totalAmount) AS 'This is the count of order totals'
FROM Orders;

--Get how many orders each customer has made and format the output as Currency
SELECT CustomerID, FORMAT(SUM(totalAmount), 'c') AS 'Dollars'
FROM Orders
GROUP BY CustomerID;