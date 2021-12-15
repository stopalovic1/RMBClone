CREATE PROCEDURE [dbo].[spCity_Update]
	@Id nvarchar(128),
	@Name nvarchar(50)
AS
begin
	set nocount on;
	update [dbo].[City]
	set [Name] = @Name
	where Id = @Id;
end