CREATE PROCEDURE [dbo].[spFaq_Insert]
	@Id NVARCHAR (128),
    @QuestionBj NVARCHAR (900),
    @AnswerBj  NVARCHAR (900),
    @QuestionEn NVARCHAR(900),
    @AnswerEn NVARCHAR(900)
AS
begin
    set nocount on;
    insert into [dbo].[FrequentlyAskedQuestions](Id,QuestionBj,AnswerBj,QuestionEn,AnswerEn)
    values(@Id,@QuestionBj,@AnswerBj,@QuestionEn,@AnswerEn);
end
