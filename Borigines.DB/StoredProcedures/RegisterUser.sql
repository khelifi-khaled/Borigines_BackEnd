CREATE PROCEDURE [dbo].[RegisterUser]
	@first_name NVARCHAR(50),
	@last_name NVARCHAR(50),
	@login NVARCHAR(250) , 
	@passwd NVARCHAR(100)
AS
BEGIN 
	DECLARE @salt NVARCHAR(100)
	SET @salt = CONCAT(NEWID(),NEWID(),NEWID())

	DECLARE @key NVARCHAR(200)
	SET @key = dbo.GetSecretKey()

	DECLARE @pwd_hash VARBINARY(64)

	SET @pwd_hash = HASHBYTES('SHA2_512', CONCAT(@salt,@key,@passwd,@salt))

	INSERT INTO Users (First_name,Last_name,Login,Passwd,Salt)
			   OUTPUT INSERTED.Id 
			   VALUES (@first_name ,@last_name,@login ,@pwd_hash ,@salt)

END 