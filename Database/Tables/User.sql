CREATE TABLE [dbo].[User]
(
	[UserId] INT NOT NULL, 
    [LoginId] INT NOT NULL, 
    [FIO] NVARCHAR(64) NOT NULL, 
    [Email] NVARCHAR(254) NOT NULL, 
    [Avatar] VARBINARY(MAX) NULL

    CONSTRAINT PK_User_UserId PRIMARY KEY (UserId),
    CONSTRAINT UQ_User_Email UNIQUE (Email),
    CONSTRAINT FK_User_Login FOREIGN KEY (LoginId) REFERENCES [Login](LoginId)
)
