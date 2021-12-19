CREATE TABLE [dbo].[FrequentlyAskedQuestions]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY,
    [QuestionBj] NVARCHAR(450) NOT NULL, 
    [AnswerBj] NVARCHAR(450) NOT NULL, 
    [QuestionEn] NVARCHAR(450) NOT NULL, 
    [AnswerEn] NVARCHAR(450) NOT NULL 
)
