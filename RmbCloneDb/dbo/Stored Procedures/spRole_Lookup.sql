CREATE PROCEDURE [dbo].[spRole_Lookup]
	@Name nvarchar(50)
AS
begin
	set nocount on;
	select * from [dbo].[Role] as r
	where r.[Name]=@Name;
end