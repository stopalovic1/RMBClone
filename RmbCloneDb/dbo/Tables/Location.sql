CREATE TABLE [dbo].[Location]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [Address] NVARCHAR(50) NOT NULL, 
    [Latitude] REAL NOT NULL, 
    [Longitude] REAL NOT NULL
)
