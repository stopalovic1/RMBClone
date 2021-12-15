CREATE PROCEDURE [dbo].[spLocation_Update]
	@Id nvarchar(128),
	@Address nvarchar(50),
	@Latitude real,
	@Longitude real
AS
begin
	set nocount on;
	update [dbo].[Location]
	set [Address] = @Address , Latitude = @Latitude , Longitude = @Longitude
	where Id = @Id;
end