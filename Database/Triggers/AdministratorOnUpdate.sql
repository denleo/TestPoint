CREATE TRIGGER [AdministratorOnUpdate]
ON [dbo].[Administrator]
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [dbo].[Administrator]
	SET updated_at = GETDATE()
	FROM [dbo].[Administrator] a
	INNER JOIN inserted i on a.AdministratorId=i.AdministratorId
END