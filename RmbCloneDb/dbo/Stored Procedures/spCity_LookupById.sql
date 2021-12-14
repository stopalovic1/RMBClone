CREATE PROCEDURE [dbo].[spCity_LookupById]
	@Id nvarchar(128)
AS
begin
	set nocount on;
	select * from [dbo].[City]
	where Id = @Id;
end