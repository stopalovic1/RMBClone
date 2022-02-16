CREATE PROCEDURE [dbo].[spBranch_Filter]
	@City varchar(50),
	@BranchType varchar(50),
	@BranchServiceType varchar(50)
AS
begin
	set nocount on;
	select  br.*
	from [dbo].[Branch] as br
	inner join [dbo].[BranchServiceType] as brst on (br.BranchServiceTypeId = brst.Id)
	inner join [dbo].[BranchType] as brt on (br.BranchTypeId = brt.Id)
	inner join [dbo].[City] as c on (br.CityId = c.Id)
	where c.[Name] = IIF(@City is null, c.[Name], @City) 
	and brt.Name = IIF(@BranchType is null, brt.[Name] ,@BranchType) 
	and brst.[name] = IIF(@BranchServiceType is null, brst.[Name], @BranchServiceType);
end
