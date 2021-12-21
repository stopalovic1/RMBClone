CREATE PROCEDURE [dbo].[spWorkingHours_LookupById]
	@BranchId nvarchar(128)
AS
begin
	set nocount on;
	select * from [dbo].[WorkingHours]
	where BranchId = @BranchId;
end
