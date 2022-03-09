CREATE PROCEDURE [dbo].[spTransaction_LookupByReceiver]
	@ToAccount nvarchar(128)
AS
begin
	select * from [dbo].[Transaction]
	where ToAccount = @ToAccount;
end
