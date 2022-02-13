CREATE PROCEDURE [dbo].[spWorkingHours_Delete]
	@BranchId nvarchar(128)
AS
begin
	set nocount on;
	delete from [dbo].[WorkingHours]
	where BranchId = @BranchId;
end
