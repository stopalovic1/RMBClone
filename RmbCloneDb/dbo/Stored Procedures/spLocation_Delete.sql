CREATE PROCEDURE [dbo].[spLocation_Delete]
	@Id nvarchar(128)
AS
begin
	set nocount on;
	delete from [dbo].[Location]
	where Id = @Id;
end
