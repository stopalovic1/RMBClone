CREATE PROCEDURE [dbo].[spLocation_Lookup]
AS
begin
	set nocount on;
	select * from [dbo].[Location];
end
