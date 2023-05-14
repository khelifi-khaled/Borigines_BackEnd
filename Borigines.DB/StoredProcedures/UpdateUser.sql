CREATE PROCEDURE [dbo].[UpdateUser]
	@first_name NVARCHAR(50),
	@last_name NVARCHAR(50),
	@login NVARCHAR(250) , 
	@passwd NVARCHAR(100),
	@id int 
AS
BEGIN 
	DECLARE @salt NVARCHAR(100)
	SET @salt = CONCAT(NEWID(),NEWID(),NEWID())

	DECLARE @key NVARCHAR(200)
	SET @key = dbo.GetSecretKey()

	DECLARE @pwd_hash VARBINARY(64)

	SET @pwd_hash = HASHBYTES('SHA2_512', CONCAT(@salt,@key,@passwd,@salt))

	UPDATE Users
		SET First_name = @first_name ,
		     Last_name  = @last_name ,
			 Login = @login,
			 Passwd = @pwd_hash,
			 Salt = @salt WHERE Id = @id ;

END 