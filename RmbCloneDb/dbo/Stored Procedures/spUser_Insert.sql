CREATE PROCEDURE [dbo].[spUser_Insert]
	@Id nvarchar(128),
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Email nvarchar(50),
	@PasswordHash nvarchar(max),
	@FcmToken nvarchar(450)
AS
begin
	set nocount on;
	insert into [dbo].[User](Id,FirstName,LastName,Email,PasswordHash,FcmToken)
	values(@Id,@FirstName,@LastName,@Email,@PasswordHash,@FcmToken);
end
	