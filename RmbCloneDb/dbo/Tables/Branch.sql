CREATE TABLE [dbo].[Branch]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NOT NULL, 
    [CityId] NVARCHAR(128) NOT NULL, 
    [Contact] NVARCHAR(20) NOT NULL, 
    [BranchTypeId] NVARCHAR(128) NULL, 
    [BranchServiceTypeId] NVARCHAR(128) NULL, 
    [ATMType] NVARCHAR(50) NOT NULL, 
    [ATMFilter] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Branch_ToBranchType] FOREIGN KEY ([BranchTypeId]) REFERENCES [BranchType]([Id]), 
    CONSTRAINT [FK_Branch_ToBranchServiceType] FOREIGN KEY ([BranchServiceTypeId]) REFERENCES [BranchServiceType]([Id]), 
    CONSTRAINT [FK_Branch_ToCity] FOREIGN KEY ([CityId]) REFERENCES [City]([Id]) 

    

)
