CREATE PROCEDURE [dbo].[spCity_Insert]
	@Id nvarchar(128),
	@Name nvarchar(128)
AS
begin
	set nocount on;
	insert into [dbo].[City](Id,[Name])
	values(@Id,@Name);
end