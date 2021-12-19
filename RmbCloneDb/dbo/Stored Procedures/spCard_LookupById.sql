CREATE PROCEDURE [dbo].[spCard_LookupById]
	@Id nvarchar(128)
AS
begin
	set nocount on;
	select * from [dbo].[Card]
	where Id = @Id;
end