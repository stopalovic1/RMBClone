CREATE PROCEDURE [dbo].[spFaq_Lookup]
AS
begin
	set nocount on;
	select * from [dbo].[FrequentlyAskedQuestions]
end
