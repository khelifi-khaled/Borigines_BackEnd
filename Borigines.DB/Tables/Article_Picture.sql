CREATE TABLE [dbo].[Article_Picture]
(
	[FK_Article] INT NOT NULL, 
    [FK_Picture] INT NOT NULL, 
    CONSTRAINT [FK_Article_Picture_Article] FOREIGN KEY ([FK_Article]) REFERENCES [Articles]([Id]), 
    CONSTRAINT [FK_Article_Picture_Pictures] FOREIGN KEY ([FK_Picture]) REFERENCES [Picture]([Id]) ,
)
