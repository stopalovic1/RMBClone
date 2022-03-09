CREATE PROCEDURE [dbo].[spBranch_Filter]
	@CityId varchar(128),
	@BranchTypeId varchar(128),
	@BranchServiceTypeId varchar(128),
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
		where c.[Id] = IIF(@CityId is null, c.[Id], @CityId) 
		and brt.[Id] = IIF(@BranchTypeId is null, brt.[Id] ,@BranchTypeId) 
		and brst.[Id] = IIF(@BranchServiceTypeId is null, brst.[Id], @BranchServiceTypeId)
		and br.ATMType = IIF(@ATMType is null, br.ATMType, @ATMType);
	end

	else
	begin
		select br.*
		from [dbo].[Branch] as br
		inner join [dbo].[BranchServiceType] as brst on (br.BranchServiceTypeId = brst.Id)
		inner join [dbo].[BranchType] as brt on (br.BranchTypeId = brt.Id)
		inner join [dbo].[City] as c on (br.CityId = c.Id)
		inner join [dbo].[Location] as l on (l.BranchId = br.Id)
		where c.[Id] = IIF(@CityId is null, c.[Id], @CityId) 
		and brt.[Id] = IIF(@BranchTypeId is null, brt.[Id] ,@BranchTypeId) 
		and brst.[Id] = IIF(@BranchServiceTypeId is null, brst.[Id], @BranchServiceTypeId)
		and br.ATMType = IIF(@ATMType is null, br.ATMType, @ATMType)
		and [dbo].[convertLatAndLongToKM](@Latitude,@Longitude,l.Latitude,l.Longitude)<@Radius+1;
	end
end
