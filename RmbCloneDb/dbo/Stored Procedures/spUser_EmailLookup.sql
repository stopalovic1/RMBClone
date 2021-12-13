CREATE PROCEDURE [dbo].[spUser_EmailLookup]
	@Email nvarchar(50)
AS
begin
	set nocount on;
	SELECT * from [dbo].[User] 
	where Email = @Email;
end
