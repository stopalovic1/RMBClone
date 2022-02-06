CREATE PROCEDURE [dbo].[spBranchServiceType_Insert]
	@Id NVARCHAR(128), 
    @Name NVARCHAR(50), 
    @NameEn NVARCHAR(50), 
    @NameBj NVARCHAR(50), 
    @NameDe NVARCHAR(50)
AS
begin
    set nocount on;
    insert into [dbo].[BranchServiceType](Id,[Name],NameEn,NameBj,NameDe)
    values(@Id,@Name,@NameEn,@NameBj,@NameDe);
end