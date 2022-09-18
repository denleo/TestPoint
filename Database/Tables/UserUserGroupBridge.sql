CREATE TABLE [dbo].[UserUserGroupBridge]
(
	[UserUserGroupBridgeId] INT NOT NULL IDENTITY(1,1), 
    [UserId] INT NOT NULL, 
    [UserGroupId] INT NOT NULL

    CONSTRAINT PK_UserUserGroupBridge_UserUserGroupBridgeId PRIMARY KEY (UserUserGroupBridgeId),
    CONSTRAINT FK_UserUserGroupBridge_UserId FOREIGN KEY (UserId) REFERENCES [User](UserId),
    CONSTRAINT FK_UserUserGroupBridge_UserGroupId FOREIGN KEY (UserGroupId) REFERENCES [UserGroup](UserGroupId)
)
