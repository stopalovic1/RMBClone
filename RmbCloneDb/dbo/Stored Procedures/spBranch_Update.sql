CREATE PROCEDURE [dbo].[spBranch_Update]
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
	update  [dbo].[Branch]
	set [Name] = @Name, CityId = @CityId, Contact = @Contact, BranchTypeId = @BranchTypeId,
	BranchServiceTypeId = @BranchServiceTypeId, ATMType = @ATMType, ATMFilterId = @ATMFilterId
	where Id = @Id;
end