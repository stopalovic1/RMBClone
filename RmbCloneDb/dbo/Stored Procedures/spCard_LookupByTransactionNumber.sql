CREATE PROCEDURE [dbo].[spCard_LookupByTransactionNumber]
	@TransactionNumber nvarchar(128)

AS
begin
	set nocount on;
	select * from [dbo].[Card]
	where TransactionNumber = @TransactionNumber;
end
