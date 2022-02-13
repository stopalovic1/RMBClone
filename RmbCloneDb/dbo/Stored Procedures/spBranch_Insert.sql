CREATE PROCEDURE [dbo].[spBranch_Insert]
	@Id nvarchar(128),
	@Name nvarchar(128),
	@CityId nvarchar(128),
	@Contact nvarchar(20),
	@BranchTypeId nvarchar(128),
	@BranchServiceTypeId nvarchar(128),
	@ATMType nvarchar(50),
	@ATMFilterId nvarchar(128)
AS
begin
	set nocount on;
	insert into [dbo].[Branch](Id,[Name],CityId,Contact,BranchTypeId,BranchServiceTypeId,ATMType,ATMFilterId)
	values(@Id,@Name,@CityId,@Contact,@BranchTypeId,@BranchServiceTypeId,@ATMType,@ATMFilterId);
end
