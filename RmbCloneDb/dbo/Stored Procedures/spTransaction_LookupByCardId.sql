CREATE PROCEDURE [dbo].[spTransaction_LookupByCardId]
	@CardId nvarchar(128)

AS
begin
	set nocount on;
	select * from [dbo].[Transaction]
	where CardId = @CardId;
end
