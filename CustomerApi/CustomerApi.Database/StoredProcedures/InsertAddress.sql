CREATE PROCEDURE [dbo].[InsertAddress]
	@customerId INT,
	@addressLine1 VARCHAR(80),
	@addressLine2 VARCHAR(80) NULL,
	@town VARCHAR(50),
	@county VARCHAR(50) NULL,
	@postcode VARCHAR(10),
	@Country VARCHAR(10),
	@isMainAddress BIT
AS
	INSERT INTO dbo.Address(CustomerId, AddressLine1, AddressLine2, Town, County, Postcode, Country, IsMainAddress)
	VALUES (@customerId, @addressLine1, @addressLine2, @town, @county, @postcode, ISNULL(@country, 'UK'), @isMainAddress)

	SELECT @@IDENTITY
GO
