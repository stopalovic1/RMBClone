CREATE PROCEDURE [dbo].[spFaq_Delete]
	@Id nvarchar(128)
AS
	set nocount on;
	delete from [dbo].[FrequentlyAskedQuestions]
	where Id = @Id;
