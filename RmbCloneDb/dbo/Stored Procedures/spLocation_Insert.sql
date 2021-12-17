CREATE PROCEDURE [dbo].[spLocation_Insert]
	@Id nvarchar(128),
	@Address nvarchar(50),
	@Latitude decimal(10,7),
	@Longitude decimal(10,7)
AS
begin
	set nocount on;
	insert into [dbo].[Location](Id,[Address],Latitude,Longitude)
	values(@Id,@Address,@Latitude,@Longitude);
end
