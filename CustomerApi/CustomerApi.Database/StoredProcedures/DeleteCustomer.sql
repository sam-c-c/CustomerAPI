CREATE PROCEDURE [dbo].[DeleteCustomer]
	@customerId int
AS
	DELETE FROM dbo.Address WHERE CustomerId = @customerId;

	DELETE FROM dbo.Customer WHERE Id = @customerId;
GO
