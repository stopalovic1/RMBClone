CREATE PROCEDURE [dbo].[spBranch_LookupById]
	@Id nvarchar(128)
AS
begin
	set nocount on;
	select * from [dbo].[Branch]
	where Id = @Id;
end
