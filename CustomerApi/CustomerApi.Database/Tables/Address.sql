CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL IDENTITY(1000,1)
,	[CustomerId] INT NOT NULL
,	[AddressLine1] VARCHAR(80) NOT NULL
,	[AddressLine2] VARCHAR(80) NULL
,	[Town] VARCHAR(50) NOT NULL
,	[County] VARCHAR(50) NULL
,	[Postcode] VARCHAR(10) NOT NULL
,	[Country] VARCHAR(10) NULL DEFAULT('UK')
,	[IsMainAddress] BIT NOT NULL 

,	CONSTRAINT PK_Address PRIMARY KEY(Id)
,	CONSTRAINT FK_Address_Customer FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer](Id)
)
