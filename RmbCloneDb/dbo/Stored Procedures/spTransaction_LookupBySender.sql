CREATE PROCEDURE [dbo].[spTransaction_LookupBySender]
	@FromAccount nvarchar(128)
AS
begin
	set nocount on;
	select * from [dbo].[Transaction]
	where FromAccount=@FromAccount;
end
