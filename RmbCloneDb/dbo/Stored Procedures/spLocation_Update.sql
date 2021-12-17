CREATE PROCEDURE [dbo].[spLocation_Update]
	@Id nvarchar(128),
	@Address nvarchar(50),
	@Latitude decimal(10,7),
	@Longitude decimal(10,7)
AS
begin
	set nocount on;
	update [dbo].[Location]
	set [Address] = @Address , Latitude = @Latitude , Longitude = @Longitude
	where Id = @Id;
end