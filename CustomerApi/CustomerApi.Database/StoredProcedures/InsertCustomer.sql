CREATE PROCEDURE [dbo].[InsertCustomer]
	@title VARCHAR(20),
	@forename VARCHAR(50),
	@surname VARCHAR(50),
	@emailAddress VARCHAR(75),
	@mobileNo VARCHAR(20)
AS
	INSERT INTO dbo.Customer(Title, Forename, Surname, EmailAddress, MobileNo, IsActive)
	VALUES (@title, @forename, @surname, @emailAddress, @mobileNo, 1)

	SELECT @@IDENTITY
GO
