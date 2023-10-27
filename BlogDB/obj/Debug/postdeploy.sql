if not exists ( select 1 from dbo.[User])
begin
insert into dbo.[User](FirstName, MiddleName, LastName, Email, Password)
values ('Michael John','Torio', 'Pediglorio','michaelpediglorio@gmail.com', 'password')
end
GO
