CREATE TABLE [dbo].[WorkingHours]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [BranchId] NVARCHAR(128) NOT NULL, 
    [Day] NVARCHAR(20) NOT NULL, 
    [Hours] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [FK_WorkingHours_ToBranch] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([Id])
)
