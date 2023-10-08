CREATE PROC [dbo].[spUser_Create]
@FirstName nvarchar(50),
@MiddleName nvarchar(50),
@LastName nvarchar(50),
@Email nvarchar(50),
@Password nvarchar(50)
AS
BEGIN
insert into dbo.[User](FirstName, MiddleName, LastName, Email, Password)
values(@FirstName, @MiddleName,  @LastName, @Email, @Password)
END
RETURN 0