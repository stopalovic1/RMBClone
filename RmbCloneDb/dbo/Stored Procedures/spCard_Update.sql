CREATE PROCEDURE [dbo].[spCard_Update]
	@Id nvarchar(128),
	@UserId nvarchar(128),
	@Iban nvarchar(128),
	@CardNumber nvarchar(128),
	@ValidUntil datetime2(7),
	@TransactionNumber nvarchar(128)
AS
begin
	set nocount on;
	update [dbo].[Card]
	set UserId = @UserId,Iban = @Iban,CardNumber = @CardNumber,ValidUntil = @ValidUntil,TransactionNumber = @TransactionNumber
	where Id=@Id;
end
