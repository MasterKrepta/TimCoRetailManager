CREATE PROCEDURE [dbo].[spProduct_GetAll]
	
AS
Begin
	set nocount on;

	select Id, ProductName, [Description], RetailPrice, QuantityInStock, IsTaxable
	from dbo.Product
	order by ProductName;
End
