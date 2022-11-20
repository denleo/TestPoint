CREATE TRIGGER [UserOnUpdate]
ON [dbo].[User]
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [dbo].[User]
	SET updated_at = GETDATE()
	FROM [dbo].[User] u
	INNER JOIN inserted i on u.UserId=i.UserId
END