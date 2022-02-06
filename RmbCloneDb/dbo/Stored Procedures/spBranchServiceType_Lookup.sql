CREATE PROCEDURE [dbo].[spBranchServiceType_Lookup]
AS
begin
	set nocount on;
	select * from [dbo].[BranchServiceType];
end