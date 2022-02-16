CREATE PROCEDURE [dbo].[spBranch_Filter]
	@City varchar(50),
	@BranchType varchar(50),
	@BranchServiceType varchar(50),
	@ATMType varchar(50),
	@Radius real,
    @Latitude decimal(10,7),
    @Longitude decimal(10,7)
AS
begin
	set nocount on;
	if(@Radius is null)
	begin
		select br.*
		from [dbo].[Branch] as br
		inner join [dbo].[BranchServiceType] as brst on (br.BranchServiceTypeId = brst.Id)
		inner join [dbo].[BranchType] as brt on (br.BranchTypeId = brt.Id)
		inner join [dbo].[City] as c on (br.CityId = c.Id)
		inner join [dbo].[Location] as l on(l.BranchId = br.Id)
		where c.[Name] = IIF(@City is null, c.[Name], @City) 
		and brt.[Name] = IIF(@BranchType is null, brt.[Name] ,@BranchType) 
		and brst.[Name] = IIF(@BranchServiceType is null, brst.[Name], @BranchServiceType)
		and br.ATMType = IIF(@ATMType is null, br.ATMType, @ATMType);
	end

	else
	begin
		select br.*
		from [dbo].[Branch] as br
		inner join [dbo].[BranchServiceType] as brst on (br.BranchServiceTypeId = brst.Id)
		inner join [dbo].[BranchType] as brt on (br.BranchTypeId = brt.Id)
		inner join [dbo].[City] as c on (br.CityId = c.Id)
		inner join [dbo].[Location] as l on(l.BranchId = br.Id)
		where c.[Name] = IIF(@City is null, c.[Name], @City) 
		and brt.[Name] = IIF(@BranchType is null, brt.[Name] ,@BranchType) 
		and brst.[Name] = IIF(@BranchServiceType is null, brst.[Name], @BranchServiceType)
		and br.ATMType = IIF(@ATMType is null, br.ATMType, @ATMType)
		and [dbo].[convertLatAndLongToKM](@Latitude,@Longitude,l.Latitude,l.Longitude)<@Radius+1;
	end
end
