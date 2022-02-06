CREATE PROCEDURE [dbo].[spBranchType_LookupById]
	@Id nvarchar(128)
AS
begin
	set nocount on;
	select * from [dbo].[BranchType]
	where Id = @Id;
end