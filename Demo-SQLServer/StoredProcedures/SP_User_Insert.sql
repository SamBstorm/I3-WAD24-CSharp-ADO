CREATE PROCEDURE [dbo].[SP_User_Insert]
	@email NVARCHAR(320),
	@password NVARCHAR(32)
AS
BEGIN
	DECLARE @salt UNIQUEIDENTIFIER
	SET @salt = NEWID()
	INSERT INTO [User] ([Email],[Password],[Salt])
	VALUES (@email, [dbo].[SF_SaltAndHash](@password, @salt), @salt)
END
