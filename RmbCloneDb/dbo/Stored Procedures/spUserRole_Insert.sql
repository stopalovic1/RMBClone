CREATE PROCEDURE [dbo].[spUserRole_Insert]
	@UserId nvarchar(128),
	@RoleId nvarchar(128)
AS
begin
	set nocount on;
	insert into [dbo].[UserRole](UserId,RoleId)
	values(@UserId,@RoleId);
end
