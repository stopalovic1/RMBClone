CREATE PROCEDURE [dbo].[spCard_Insert]
	@Id nvarchar(128),
	@UserId nvarchar(128),
	@Iban nvarchar(128),
	@CardNumber nvarchar(128),
	@ValidUntil datetime2(7),
	@TransactionNumber nvarchar(128)

AS
begin
	set nocount on;
	insert into [dbo].[Card](Id,UserId,Iban,CardNumber,ValidUntil,TransactionNumber)
	values(@Id,@UserId,@Iban,@CardNumber,@ValidUntil,@TransactionNumber);
end
