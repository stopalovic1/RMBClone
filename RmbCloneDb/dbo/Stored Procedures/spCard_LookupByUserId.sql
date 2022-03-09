CREATE PROCEDURE [dbo].[spCard_LookupByUserId]
	@UserId nvarchar(128)
AS
begin
	set nocount on;
	select * from [dbo].[Card]
	where UserId = @UserId;
end
