CREATE PROCEDURE [dbo].[spCard_Lookup]

AS
begin
	set nocount on;
	select * from [dbo].[Card];
end
