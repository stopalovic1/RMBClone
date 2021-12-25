CREATE PROCEDURE [dbo].[spLocation_LookupByBranchId]
	@BranchId nvarchar(128)
AS
begin
	set nocount on;
	select * from [dbo].[Location]
	where BranchId = @BranchId;
end