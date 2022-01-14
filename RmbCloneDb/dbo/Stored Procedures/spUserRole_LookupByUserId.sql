CREATE PROCEDURE [dbo].[spUserRole_LookupByUserId]
	@UserId nvarchar(128)
	
AS
begin
set nocount on;
	select r.[Name] from [dbo].[Role] as r,[dbo].[UserRole] as ur
	where  r.Id = ur.RoleId and ur.UserId=@UserId;
end
