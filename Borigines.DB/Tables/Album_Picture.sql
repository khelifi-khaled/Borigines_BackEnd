CREATE TABLE [dbo].[Album_Picture]
(
	[FK_Picture] INT NOT NULL, 
    [FK_Album] INT NOT NULL, 
    CONSTRAINT [FK_Album_Picture_Picture] FOREIGN KEY ([FK_Picture]) REFERENCES [Picture]([Id]), 
    CONSTRAINT [FK_Album_Picture_Album] FOREIGN KEY ([FK_Album]) REFERENCES [Albums]([Id])
)
