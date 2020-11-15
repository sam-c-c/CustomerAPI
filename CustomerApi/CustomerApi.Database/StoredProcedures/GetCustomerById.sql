CREATE PROCEDURE [dbo].[GetCustomerById]
	@customerId INT
AS
	SELECT 
		Id
	,	Title
	,	Forename
	,	Surname
	,	EmailAddress
	,	MobileNo
	,	IsActive
	FROM
		dbo.Customer
	WHERE
		Id = @customerId
GO
