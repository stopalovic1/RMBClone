CREATE PROCEDURE [dbo].[spFaq_Insert]
	@Id NVARCHAR (128),
    @Question NVARCHAR (128),
    @Answer  NVARCHAR (128),
    @QuestionEn NVARCHAR(128),
    @AnswerEn NVARCHAR(128)
AS
begin
    set nocount on;
    insert into [dbo].[FrequentlyAskedQuestions](Id,Question,Answer,QuestionEn,AnswerEn)
    values(@Id,@Question,@Answer,@QuestionEn,@AnswerEn);
end
