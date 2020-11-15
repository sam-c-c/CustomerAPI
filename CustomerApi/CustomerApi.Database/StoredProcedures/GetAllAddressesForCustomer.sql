CREATE PROCEDURE [dbo].[GetAllAddressesForCustomer]
	@customerId int
AS
	SELECT 
		Id
	,	CustomerId
	,	AddressLine1
	,	AddressLine2
	,	Town
	,	County
	,	Postcode
	,	Country
	,	IsMainAddress
	FROM
		dbo.[Address]
	WHERE
		CustomerId = @customerId
	ORDER BY Id DESC
GO
