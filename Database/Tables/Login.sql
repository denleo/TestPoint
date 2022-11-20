CREATE TABLE [dbo].[Login]
(
	[LoginId] INT NOT NULL IDENTITY(1,1),
    [LoginType] TINYINT NOT NULL,
    [Username] NVARCHAR(16) NOT NULL,
    [PasswordHash] NVARCHAR(256) NOT NULL,
    [PasswordReseted] BIT NOT NULL,
    [RegistryDate] DATETIME2(7) NOT NULL,
    [updated_at] DATETIME2 NULL

    CONSTRAINT PK_Login_LoginId PRIMARY KEY ([LoginId]),
    CONSTRAINT FK_Login_LoginType FOREIGN KEY (LoginType) REFERENCES [LoginType](LoginTypeId),
    CONSTRAINT UQ_Login_LoginTypeUsername UNIQUE ([LoginType], [Username]),
)
