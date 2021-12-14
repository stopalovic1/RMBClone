CREATE PROCEDURE [dbo].[spLocation_LookupById]
	@Id nvarchar(128)
AS
begin
	set nocount on;
	select * from [dbo].[Location]
	where Id = @Id;
end
