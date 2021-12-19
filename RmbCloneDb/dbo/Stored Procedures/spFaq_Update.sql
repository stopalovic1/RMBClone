CREATE PROCEDURE [dbo].[spFaq_Update]
	@Id NVARCHAR (128),
    @QuestionBj NVARCHAR (450),
    @AnswerBj NVARCHAR (450),
    @QuestionEn NVARCHAR(450),
    @AnswerEn NVARCHAR(450)
AS
begin
    set nocount on;
    update [dbo].[FrequentlyAskedQuestions]
    set QuestionBj = @QuestionBj, AnswerBj = @AnswerBj,QuestionEn = @QuestionEn, AnswerEn = @AnswerEn
    where Id = @Id;
end
