CREATE TABLE [dbo].[UserGroup]
(
	[UserGroupId] INT NOT NULL, 
    [AdministratorId] INT NOT NULL, 
    [Name] NVARCHAR(64) NOT NULL

    CONSTRAINT PK_UserGroup_UserGroupId PRIMARY KEY (UserGroupId),
    CONSTRAINT FK_UserGroup_AdministratorId FOREIGN KEY (AdministratorId) REFERENCES [Administrator](AdministratorId)
)
