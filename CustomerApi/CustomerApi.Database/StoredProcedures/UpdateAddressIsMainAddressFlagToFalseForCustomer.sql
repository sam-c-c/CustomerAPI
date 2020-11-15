CREATE PROCEDURE [dbo].[UpdateAddressIsMainAddressFlagToFalseForCustomer]
	@customerId int
AS
	UPDATE dbo.[Address]
	SET IsMainAddress = 0
	WHERE CustomerId = @customerId
GO
