CREATE PROCEDURE [dbo].[spWorkingHours_Update]
	@Id nvarchar(128),
	@BranchId nvarchar(128),
	@Day nvarchar(20),
	@Hours nvarchar(20)
AS
begin
	set nocount on;
	update [dbo].[WorkingHours]
	set [Day] = @Day,[Hours] = @Hours
	where Id = @Id;
end
