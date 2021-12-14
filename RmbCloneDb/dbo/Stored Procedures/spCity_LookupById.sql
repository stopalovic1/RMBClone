CREATE PROCEDURE [dbo].[spCity_LookupById]
	@id nvarchar(128)

AS
begin
	set nocount on;
	select * from [dbo].[Location]
	where Id = @Id;
end