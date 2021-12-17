CREATE TABLE [dbo].[Location]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [Address] NVARCHAR(50) NOT NULL, 
    [Latitude] DECIMAL(10, 7) NOT NULL, 
    [Longitude] DECIMAL(10, 7) NOT NULL
)
