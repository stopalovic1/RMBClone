CREATE PROCEDURE [dbo].[spTransaction_Insert]
	@Id NVARCHAR(128),
    @CardId NVARCHAR(128),
    @TransactionReason NVARCHAR(75),
    @TransactionValue MONEY ,
    @DateOfTransaction DATETIME2 ,
    @FromAccount NVARCHAR(128) ,
    @ToAccount NVARCHAR(128)
AS
begin
    set nocount on;
	insert into [dbo].[Transaction](Id,CardId,TransactionReason,TransactionValue,DateOfTransaction,FromAccount,ToAccount)
    values(@Id,@CardId,@TransactionReason,@TransactionValue,@DateOfTransaction,@FromAccount,@ToAccount);
end
