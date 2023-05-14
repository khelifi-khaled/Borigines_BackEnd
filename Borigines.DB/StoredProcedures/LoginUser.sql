CREATE PROCEDURE [dbo].[LoginUser]
	@login NVARCHAR(250),
	@pwd NVARCHAR (100)
AS
BEGIN 
	
	DECLARE @salt NVARCHAR(100)
	SET @salt = (SELECT Salt FROM Users WHERE Login = @login)

	DECLARE @key NVARCHAR(200)
	SET @key = dbo.GetSecretKey()

	DECLARE @pwd_hash VARBINARY(64)

	SET @pwd_hash = HASHBYTES('SHA2_512', CONCAT(@salt,@key,@pwd,@salt))

	SELECT Id , First_name , Last_name , Is_Admin , Login  
	FROM Users 
	WHERE Login = @login AND Passwd  = @pwd_hash AND Is_Active = 1 ;
	
END 