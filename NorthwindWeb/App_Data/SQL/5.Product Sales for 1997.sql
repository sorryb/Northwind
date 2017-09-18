
CREATE VIEW "Product Sales for 1997" 
AS
SELECT 
	Categories.CategoryName, 
	Products.ProductName, 
	Sum(CONVERT(money,("Order Details".UnitPrice*Quantity*(1-Discount)/100))*100) AS ProductSales
FROM
	 (Categories INNER JOIN Products ON Categories.CategoryID = Products.CategoryID) 
	INNER JOIN (Orders 
		INNER JOIN "Order Details" ON Orders.OrderID = "Order Details".OrderID) 
	ON Products.ProductID = "Order Details".ProductID
WHERE
	 (((Orders.ShippedDate) Between '19900101' And getDate()))
GROUP BY 
	Categories.CategoryName, 
	Products.ProductName



