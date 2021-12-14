CREATE PROCEDURE [dbo].[spCity_Lookup]

AS
begin
	set nocount on;
	select * from [dbo].[City];
end
