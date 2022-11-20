CREATE TRIGGER [LoginOnUpdate]
ON [dbo].[Login]
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [dbo].[Login]
	SET updated_at = GETDATE()
	FROM [dbo].[Login] l
	INNER JOIN inserted i on l.LoginId=i.LoginId
END