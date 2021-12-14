CREATE PROCEDURE [dbo].[spCity_Delete]
	@Id nvarchar(128)

AS
begin
	delete from [dbo].[City]
	where Id = @Id;
end
