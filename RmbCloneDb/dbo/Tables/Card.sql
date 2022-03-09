CREATE TABLE [dbo].[Card]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [Iban] NVARCHAR(128) NOT NULL, 
    [CardNumber] NVARCHAR(128) NOT NULL, 
    [ValidUntil] DATETIME2 NOT NULL, 
    [TransactionNumber] NVARCHAR(128) NOT NULL, 
    [CurrentAmount] MONEY NULL, 
    [HasLimit] BIT NULL, 
    [LimitAmount] MONEY NULL, 
    CONSTRAINT [FK_Card_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) 
)
