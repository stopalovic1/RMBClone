CREATE PROCEDURE [dbo].[spAtmFilter_LookupById]
	@Id nvarchar(128)
	
AS
begin
	set nocount on;
	select * from [dbo].[ATMFilter]
	where Id=@Id;
end