CREATE TABLE [dbo].[Location]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [Adress] NVARCHAR(50) NOT NULL, 
    [latitude] REAL NOT NULL, 
    [longitude] REAL NOT NULL
)
