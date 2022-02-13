CREATE PROCEDURE [dbo].[spBranch_Delete]
	@Id nvarchar(128)

AS
begin
	set nocount on;
	delete from [dbo].[Branch]
	where Id = @Id;
end
