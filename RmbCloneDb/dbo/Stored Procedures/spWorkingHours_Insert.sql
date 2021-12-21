CREATE PROCEDURE [dbo].[spWorkingHours_Insert]
	@Id nvarchar(128),
	@BranchId nvarchar(128),
	@Day nvarchar(20),
	@Hours nvarchar(20)
AS
begin
	set nocount on;
	insert into [dbo].[WorkingHours](Id,BranchId,[Day],[Hours])
	values(@Id,@BranchId,@Day,@Hours);
end
