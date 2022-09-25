CREATE TABLE [dbo].[LoginType]
(
	[LoginTypeId] TINYINT NOT NULL IDENTITY(0,1), 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(256) NULL

    CONSTRAINT PK_LoginType_LoginTypeId PRIMARY KEY (LoginTypeId),
)
