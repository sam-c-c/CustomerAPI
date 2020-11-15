CREATE PROCEDURE [dbo].[DeleteAddress]
	@customerId INT,
	@addressId INT
AS
	DELETE FROM dbo.[Address]
	WHERE
		Id = @addressId
	AND
		CustomerId = @customerId
GO
