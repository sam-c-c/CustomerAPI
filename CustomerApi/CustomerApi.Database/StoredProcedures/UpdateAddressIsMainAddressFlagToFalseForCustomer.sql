CREATE PROCEDURE [dbo].[UpdateAddressIsMainAddressFlagToFalseForCustomer]
	@customerId INT
AS
	UPDATE dbo.[Address]
	SET IsMainAddress = 0
	WHERE CustomerId = @customerId
GO
