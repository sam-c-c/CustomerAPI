CREATE PROCEDURE [dbo].[GetAllCustomers]
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
GO
