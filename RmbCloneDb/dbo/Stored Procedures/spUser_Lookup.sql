CREATE PROCEDURE [dbo].[spUser_Lookup]

AS
begin
	set nocount on;
	select * from [dbo].[User];
end
