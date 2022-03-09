CREATE TABLE [dbo].[Transaction]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [CardId] NVARCHAR(128) NOT NULL, 
    [TransactionReason] NVARCHAR(75) NOT NULL, 
    [TransactionValue] MONEY NOT NULL, 
    [DateOfTransaction] DATETIME2 NOT NULL, 
    [FromAccount] NVARCHAR(128) NULL, 
    [ToAccount] NVARCHAR(128) NULL, 
    CONSTRAINT [FK_Transaction_ToCard] FOREIGN KEY ([CardId]) REFERENCES [Card]([Id]) 
)
