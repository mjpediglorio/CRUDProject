CREATE TABLE [dbo].[Posts]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [UserFirstName] NCHAR(10) NOT NULL, 
    [UserLastName] NCHAR(10) NOT NULL, 
    [PostContent] NCHAR(10) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT (getutcdate()) 
)
