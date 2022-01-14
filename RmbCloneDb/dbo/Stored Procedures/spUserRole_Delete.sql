CREATE PROCEDURE [dbo].[spUserRole_Delete]
	@UserId nvarchar(128),
	@RoleId nvarchar(128)
AS
begin
	set nocount on;
	delete ur from [dbo].[UserRole] as ur
	where ur.UserId=@UserId and ur.RoleId=@RoleId;
end