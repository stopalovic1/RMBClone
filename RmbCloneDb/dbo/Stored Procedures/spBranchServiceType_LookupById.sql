CREATE PROCEDURE [dbo].[spBranchServiceType_LookupById]
	@Id nvarchar(128)
AS
begin
	set nocount on;
	select *from [dbo].[BranchServiceType]
	where Id = @Id;
end
