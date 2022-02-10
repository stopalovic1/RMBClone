CREATE PROCEDURE [dbo].[spAtmFilter_Insert]
	@Id nvarchar(128),
	@Name nvarchar(50)
AS
begin
	set nocount on;
	insert into [dbo].[ATMFilter](Id,[Name])
	values(@Id,@Name);
end