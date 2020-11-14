﻿CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL IDENTITY(1000,1)
,	[Title] VARCHAR(20) NOT NULL
,	[Forename] VARCHAR(50) NOT NULL
,	[Surname] VARCHAR(50) NOT NULL
,	[EmailAddress] VARCHAR(75) NOT NULL
,	[MobileNo] VARCHAR(20) NOT NULL
,	[IsActive] BIT NOT NULL 

,	CONSTRAINT PK_Customer PRIMARY KEY(Id)
)
