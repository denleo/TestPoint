CREATE TABLE [dbo].[Login]
(
	[LoginId] INT NOT NULL, 
    [Username] NVARCHAR(16) NOT NULL, 
    [Password] NVARCHAR(256) NOT NULL, 
    [RegistryDate] DATETIME2(7) NOT NULL

    CONSTRAINT PK_Login_LoginId PRIMARY KEY ([LoginId]),
    CONSTRAINT UQ_Login_Username UNIQUE ([LoginId]),
)
