CREATE TABLE [dbo].[Administrator]
(
	[AdministratorId] INT NOT NULL,
	[LoginId] INT NOT NULL,
	[IsPasswordReset] BIT NOT NULL

	CONSTRAINT PK_Administrator_AdministratorId PRIMARY KEY (AdministratorId),
	CONSTRAINT FK_Administrator_Login FOREIGN KEY (LoginId) REFERENCES [Login](LoginId),
)
