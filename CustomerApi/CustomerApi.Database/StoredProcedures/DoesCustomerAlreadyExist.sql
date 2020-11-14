CREATE PROCEDURE [dbo].[DoesCustomerAlreadyExist]
	@title VARCHAR(20),
	@forename VARCHAR(50),
	@surname VARCHAR(50),
	@emailAddress VARCHAR(75)
AS
	SELECT Id
	FROM
		dbo.Customer
	WHERE
		Title = @title
	AND
		Forename = @forename
	AND
		Surname = @surname
	AND
		EmailAddress = @emailAddress
GO