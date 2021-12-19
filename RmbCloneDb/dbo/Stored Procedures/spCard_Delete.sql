CREATE PROCEDURE [dbo].[spCard_Delete]
	@Id nvarchar(128)
AS
begin
	set nocount on;
	delete from [dbo].[Card]
	where Id = @Id;
end
