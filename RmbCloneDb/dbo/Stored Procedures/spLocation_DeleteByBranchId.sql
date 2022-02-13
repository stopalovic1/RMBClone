CREATE PROCEDURE [dbo].[spLocation_DeleteByBranchId]
	@BranchId nvarchar(128)
	
AS
begin
	set nocount on;
	delete from [dbo].[Location]
	where BranchId = @BranchId;
end