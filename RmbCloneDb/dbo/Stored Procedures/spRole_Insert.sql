CREATE PROCEDURE [dbo].[spRole_Insert]
	@Id nvarchar(128),
	@Name nvarchar(50)
AS
begin
	set nocount on;
	insert into [dbo].[Role](Id,[Name])
	values(@Id,@Name);
end