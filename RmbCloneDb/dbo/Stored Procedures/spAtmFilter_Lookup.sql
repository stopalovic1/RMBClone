CREATE PROCEDURE [dbo].[spAtmFilter_Lookup]
AS
begin
	set nocount on;
	select * from [dbo].[ATMFilter];
end
