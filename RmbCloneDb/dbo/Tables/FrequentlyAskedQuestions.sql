CREATE TABLE [dbo].[FrequentlyAskedQuestions]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY,
    [QuestionBj] NVARCHAR(900) NOT NULL, 
    [AnswerBj] NVARCHAR(900) NOT NULL, 
    [QuestionEn] NVARCHAR(900) NOT NULL, 
    [AnswerEn] NVARCHAR(900) NOT NULL 
)
