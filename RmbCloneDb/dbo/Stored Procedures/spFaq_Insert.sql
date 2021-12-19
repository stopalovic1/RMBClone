CREATE PROCEDURE [dbo].[spFaq_Insert]
	@Id NVARCHAR (128),
    @QuestionBj NVARCHAR (450),
    @AnswerBj  NVARCHAR (450),
    @QuestionEn NVARCHAR(450),
    @AnswerEn NVARCHAR(450)
AS
begin
    set nocount on;
    insert into [dbo].[FrequentlyAskedQuestions](Id,QuestionBj,AnswerBj,QuestionEn,AnswerEn)
    values(@Id,@QuestionBj,@AnswerBj,@QuestionEn,@AnswerEn);
end
