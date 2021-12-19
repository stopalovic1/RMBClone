CREATE PROCEDURE [dbo].[spFaq_Update]
	@Id NVARCHAR (128),
    @Question NVARCHAR (128),
    @Answer NVARCHAR (128),
    @QuestionEn NVARCHAR(128),
    @AnswerEn NVARCHAR(128)
AS
begin
    set nocount on;
    update [dbo].[FrequentlyAskedQuestions]
    set Question = @Question,Answer = @Answer,QuestionEn = @QuestionEn, AnswerEn = @AnswerEn
    where Id = @Id;
end
