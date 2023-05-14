CREATE TABLE [dbo].[Articles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1), 
    [Date_Article] DATETIME NOT NULL, 
    [FK_id_user] INT NOT NULL, 
    [FK_content_fr] INT NOT NULL, 
    [FK_content_en] INT NOT NULL, 
    [FK_content_nl] INT NOT NULL, 
    [Fk_category_id] INT NOT NULL, 
    CONSTRAINT [FK_Articles_User] FOREIGN KEY ([FK_id_user]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_Articles_Category] FOREIGN KEY ([Fk_category_id]) REFERENCES [Categorys]([Id]), 
    CONSTRAINT [FK_Articles_Content_fr] FOREIGN KEY ([FK_content_fr]) REFERENCES [Content_fr]([Id]), 
    CONSTRAINT [FK_Articles_Content_en] FOREIGN KEY ([FK_content_en]) REFERENCES [Content_en]([Id]), 
    CONSTRAINT [FK_Articles_Content_nl] FOREIGN KEY ([FK_content_nl]) REFERENCES [Content_nl]([Id])
)
