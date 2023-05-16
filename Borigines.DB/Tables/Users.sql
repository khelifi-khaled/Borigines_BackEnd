CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1), 
    [First_name] NVARCHAR(50) NOT NULL, 
	[Last_name] NVARCHAR(50) NOT NULL, 
    [Login] NVARCHAR(250) NOT NULL , 
    [Passwd] VARBINARY(64) NOT NULL, 
    [Salt] NVARCHAR(100) NOT NULL,
    [Is_Admin] BIT DEFAULT 0 NOT NULL,
    [Is_Active] BIT DEFAULT 1 NOT NULL,
     CONSTRAINT Unique_Login_constraint UNIQUE ([Login])
)
