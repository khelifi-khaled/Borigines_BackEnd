CREATE TABLE [dbo].[Albums]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1), 
    [Titel] NVARCHAR(100) NOT NULL, 
    [Date_Album] DATETIME NOT NULL, 
    [Id_user] INT NOT NULL, 
    CONSTRAINT [FK_Album_User] FOREIGN KEY ([Id_user]) REFERENCES [Users]([Id])
)
