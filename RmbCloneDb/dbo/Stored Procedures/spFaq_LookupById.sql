CREATE PROCEDURE [dbo].[spFaq_LookupById]
	@Id NVARCHAR (128)
AS
begin
	set nocount on;
	select * from [dbo].[FrequentlyAskedQuestions]
	where Id=@Id;
end
