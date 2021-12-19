CREATE TABLE [dbo].[FrequentlyAskedQuestions]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY,
    [Question] NVARCHAR(128) NOT NULL, 
    [Answer] NVARCHAR(128) NOT NULL, 
    [QuestionEn] NVARCHAR(128) NOT NULL, 
    [AnswerEn] NVARCHAR(128) NOT NULL 
)
