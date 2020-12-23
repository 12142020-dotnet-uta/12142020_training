--- Scalar Function
CREATE FUNCTION dbo.GetNetSale(
	@quantity INT,	
	@unitprice dec(10,2),
	@discount dec(10,2)
)
RETURNS dec(10,2)--10 is the total number of digits, 2 are after the decimal
AS 
BEGIN
	RETURN @quantity*@unitprice*(1-@discount);
END

-- call function (dbo means DataBase Owner)
-- SELECT dbo.GetNetSale(100000,100000.00,0.9) AS netSale;

