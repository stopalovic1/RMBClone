CREATE PROCEDURE [dbo].[spFaq_Update]
	@Id NVARCHAR (128),
    @QuestionBj NVARCHAR (900),
    @AnswerBj NVARCHAR (900),
    @QuestionEn NVARCHAR(900),
    @AnswerEn NVARCHAR(900)
AS
begin
    set nocount on;
    update [dbo].[FrequentlyAskedQuestions]
    set QuestionBj = @QuestionBj, AnswerBj = @AnswerBj,QuestionEn = @QuestionEn, AnswerEn = @AnswerEn
    where Id = @Id;
end
