CREATE PROCEDURE [dbo].[spBranchType_Insert]
	@Id nvarchar(128),
	@Name nvarchar(50)
AS
begin
	set nocount on;
	insert into [dbo].[BranchType](Id,[Name])
	values(@Id,@Name);
end
