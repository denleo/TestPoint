CREATE TABLE [dbo].[User]
(
	[UserId] INT NOT NULL IDENTITY(1,1), 
    [LoginId] INT NOT NULL,  
    [Email] NVARCHAR(254) NOT NULL,
    [FirstName] NVARCHAR(64) NOT NULL,
    [LastName] NVARCHAR(64) NOT NULL,
    [Avatar] VARBINARY(MAX) NULL

    CONSTRAINT PK_User_UserId PRIMARY KEY (UserId),
    CONSTRAINT UQ_User_Email UNIQUE (Email),
    CONSTRAINT FK_User_Login FOREIGN KEY (LoginId) REFERENCES [Login](LoginId)
)
