﻿CREATE TABLE [dbo].[Content_nl]
(
	[Id] INT NOT NULL PRIMARY KEY  IDENTITY (1,1), 
    [Title] NVARCHAR(100) NOT NULL, 
    [Content] TEXT NOT NULL
)
