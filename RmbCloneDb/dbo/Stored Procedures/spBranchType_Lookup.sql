CREATE PROCEDURE [dbo].[spBranchType_Lookup]

AS
begin
	set nocount on;
	select * from [dbo].[BranchType];
end