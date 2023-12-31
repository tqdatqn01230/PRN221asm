USE [PostDbContext]
GO
/****** Object:  Table [dbo].[AppUsers]    Script Date: 8/21/2023 09:39:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUsers](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Fullname] [nvarchar](250) NULL,
	[Address] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[Password] [nvarchar](250) NULL,
 CONSTRAINT [PK_AppUsers] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostCategories]    Script Date: 8/21/2023 09:39:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostCategories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](250) NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_PostCategories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 8/21/2023 09:39:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[AuthorId] [int] NULL,
	[CreatedDate] [date] NULL,
	[UpdatedDate] [date] NULL,
	[Title] [nvarchar](250) NULL,
	[Content] [nvarchar](250) NULL,
	[PublishStatus] [bit] NULL,
	[CategoryId] [int] NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AppUsers] ON 

INSERT [dbo].[AppUsers] ([UserID], [Fullname], [Address], [Email], [Password]) VALUES (1, N'a', N'a', N'a', N'1')
INSERT [dbo].[AppUsers] ([UserID], [Fullname], [Address], [Email], [Password]) VALUES (2, N'bc', N'bc', N'bc', N'bc')
SET IDENTITY_INSERT [dbo].[AppUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[PostCategories] ON 

INSERT [dbo].[PostCategories] ([CategoryId], [CategoryName], [Description]) VALUES (1, N'abc', N'abc')
INSERT [dbo].[PostCategories] ([CategoryId], [CategoryName], [Description]) VALUES (2, N'bcd', N'bcd')
INSERT [dbo].[PostCategories] ([CategoryId], [CategoryName], [Description]) VALUES (3, N'xyz', N'xyz')
SET IDENTITY_INSERT [dbo].[PostCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([PostID], [AuthorId], [CreatedDate], [UpdatedDate], [Title], [Content], [PublishStatus], [CategoryId]) VALUES (2, 1, CAST(N'2001-01-01' AS Date), CAST(N'2001-01-01' AS Date), N'abcd', N'abc', 1, 1)
INSERT [dbo].[Posts] ([PostID], [AuthorId], [CreatedDate], [UpdatedDate], [Title], [Content], [PublishStatus], [CategoryId]) VALUES (3, 1, CAST(N'2023-08-11' AS Date), CAST(N'2023-08-25' AS Date), N'1', N'1', 1, 2)
INSERT [dbo].[Posts] ([PostID], [AuthorId], [CreatedDate], [UpdatedDate], [Title], [Content], [PublishStatus], [CategoryId]) VALUES (4, 1, CAST(N'2023-08-18' AS Date), CAST(N'2023-08-26' AS Date), N'1', N'1', 0, 1)
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_AppUsers] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[AppUsers] ([UserID])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_AppUsers]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_PostCategories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[PostCategories] ([CategoryId])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_PostCategories]
GO
