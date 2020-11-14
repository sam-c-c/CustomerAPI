CREATE PROCEDURE [dbo].[UpdateCustomerIsActiveFlag]
	@customerId INT,
	@isActive BIT
AS
	UPDATE dbo.Customer
	SET
		IsActive = @isActive
	WHERE
		Id = @customerId
GO