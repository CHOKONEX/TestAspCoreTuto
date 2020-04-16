CREATE TABLE [dbo].[Directors](  
    [Id] [int] IDENTITY(1,1) PRIMARY KEY,  
    [Name] [nvarchar](50) NOT NULL,  
);  
  
CREATE TABLE [dbo].[Movies](  
    [Id] [int] IDENTITY(1,1) PRIMARY KEY,  
    [Name] [nvarchar](50) NOT NULL,  
    [DirectorId] [int] NOT NULL,  
    [ReleaseYear] [smallint] NOT NULL,  
    FOREIGN KEY([DirectorId]) REFERENCES [dbo].[Directors] ([Id]));  
  
SET IDENTITY_INSERT [dbo].[Directors] ON  
  
GO  
INSERT [dbo].[Directors] ([Id], [Name]) VALUES (1, N'Chistopher Nolan')  
GO  
INSERT [dbo].[Directors] ([Id], [Name]) VALUES (2, N'James Camron')  
GO  
SET IDENTITY_INSERT [dbo].[Directors] OFF  
GO  
SET IDENTITY_INSERT [dbo].[Movies] ON  
  
GO  
INSERT [dbo].[Movies] ([Id], [Name], [DirectorId], [ReleaseYear]) VALUES (1, N'Dunkirk', 1, 2017)  
GO  
INSERT [dbo].[Movies] ([Id], [Name], [DirectorId], [ReleaseYear]) VALUES (2, N'Interstellar', 1, 2014)  
GO  
INSERT [dbo].[Movies] ([Id], [Name], [DirectorId], [ReleaseYear]) VALUES (3, N'Avatar', 2, 2009)  
GO  
INSERT [dbo].[Movies] ([Id], [Name], [DirectorId], [ReleaseYear]) VALUES (4, N'Titanic', 2, 1997)  
GO  
INSERT [dbo].[Movies] ([Id], [Name], [DirectorId], [ReleaseYear]) VALUES (5, N'The Terminator', 2, 1984)  
GO  
SET IDENTITY_INSERT [dbo].[Movies] OFF  