USE [master]
GO
/****** Object:  Database [StoreToDoorDB]    Script Date: 3/5/2023 7:07:53 PM ******/
CREATE DATABASE [StoreToDoorDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StoreToDoorDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\StoreToDoorDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StoreToDoorDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\StoreToDoorDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [StoreToDoorDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StoreToDoorDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StoreToDoorDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StoreToDoorDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StoreToDoorDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StoreToDoorDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StoreToDoorDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [StoreToDoorDB] SET  MULTI_USER 
GO
ALTER DATABASE [StoreToDoorDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StoreToDoorDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StoreToDoorDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StoreToDoorDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StoreToDoorDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [StoreToDoorDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [StoreToDoorDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [StoreToDoorDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [StoreToDoorDB]
GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 3/5/2023 7:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshToken](
	[RefreshTokenId] [bigint] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](max) NULL,
	[JwtId] [nvarchar](100) NULL,
	[UserId] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[ExpiryDate] [datetime] NULL,
	[Used] [bit] NULL,
 CONSTRAINT [PK_RefreshTokenId] PRIMARY KEY CLUSTERED 
(
	[RefreshTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 100, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleMaster]    Script Date: 3/5/2023 7:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMaster](
	[RoleId] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_RoleId] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 100, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 3/5/2023 7:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMaster](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](100) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[designation] [varchar](50) NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
 CONSTRAINT [PK_UserId] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 100, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 3/5/2023 7:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserRole] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[UpdateOn] [datetime] NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 100, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RoleMaster] ON 
GO
INSERT [dbo].[RoleMaster] ([RoleId], [RoleName], [CreatedOn], [UpdatedOn]) VALUES (1, N'Admin', CAST(N'2023-03-05T13:45:07.640' AS DateTime), CAST(N'2023-03-05T13:47:23.190' AS DateTime))
GO
INSERT [dbo].[RoleMaster] ([RoleId], [RoleName], [CreatedOn], [UpdatedOn]) VALUES (2, N'User', CAST(N'2023-03-05T13:45:07.640' AS DateTime), CAST(N'2023-03-05T13:47:23.190' AS DateTime))
GO
INSERT [dbo].[RoleMaster] ([RoleId], [RoleName], [CreatedOn], [UpdatedOn]) VALUES (12, N'Manager', CAST(N'2023-03-05T18:58:49.453' AS DateTime), CAST(N'2023-03-05T18:58:49.453' AS DateTime))
GO
INSERT [dbo].[RoleMaster] ([RoleId], [RoleName], [CreatedOn], [UpdatedOn]) VALUES (13, N'Guest', CAST(N'2023-03-05T18:58:56.320' AS DateTime), CAST(N'2023-03-05T18:58:56.320' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[RoleMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[UserMaster] ON 
GO
INSERT [dbo].[UserMaster] ([UserId], [Name], [Address], [Phone], [designation], [EmailAddress], [UserName], [Password], [IsActive], [CreatedOn], [UpdateOn]) VALUES (2, N'Admin', N'India', N'+1(306) 321 1234', N'Developer', N'dev@gmail.com', N'dev1', N'dev123', 1, NULL, CAST(N'2023-03-05T18:52:57.317' AS DateTime))
GO
INSERT [dbo].[UserMaster] ([UserId], [Name], [Address], [Phone], [designation], [EmailAddress], [UserName], [Password], [IsActive], [CreatedOn], [UpdateOn]) VALUES (3, N'Joy', N'Canada', N'+1(306) 321 1234', N'Developer', N'dev@gmail.com', N'dev2', N'dev123', 1, NULL, CAST(N'2023-03-05T18:53:05.637' AS DateTime))
GO
INSERT [dbo].[UserMaster] ([UserId], [Name], [Address], [Phone], [designation], [EmailAddress], [UserName], [Password], [IsActive], [CreatedOn], [UpdateOn]) VALUES (4, N'Hank', N'USA', N'+1(306) 321 1234', N'Developer', N'dev@gmail.com', N'dev3', N'dev123', 1, NULL, CAST(N'2023-03-05T18:53:13.537' AS DateTime))
GO
INSERT [dbo].[UserMaster] ([UserId], [Name], [Address], [Phone], [designation], [EmailAddress], [UserName], [Password], [IsActive], [CreatedOn], [UpdateOn]) VALUES (5, N'Roy', N'UK', N'+1(306) 321 123', N'Developer', N'dev@gmail.com', N'dev4', N'dev123', 1, NULL, CAST(N'2023-03-05T18:53:26.510' AS DateTime))
GO
INSERT [dbo].[UserMaster] ([UserId], [Name], [Address], [Phone], [designation], [EmailAddress], [UserName], [Password], [IsActive], [CreatedOn], [UpdateOn]) VALUES (6, N'Geemon', N'Europe', N'+1(306) 321 1234', N'Developer', N'dev@gmail.com', N'dev5', N'dev123', 1, NULL, CAST(N'2023-03-05T18:54:50.500' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[UserMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 
GO
INSERT [dbo].[UserRole] ([UserRole], [UserId], [RoleId], [UpdateOn]) VALUES (1, 2, 1, NULL)
GO
INSERT [dbo].[UserRole] ([UserRole], [UserId], [RoleId], [UpdateOn]) VALUES (2, 3, 2, NULL)
GO
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UC_RoleName]    Script Date: 3/5/2023 7:07:53 PM ******/
ALTER TABLE [dbo].[RoleMaster] ADD  CONSTRAINT [UC_RoleName] UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UC_UserName]    Script Date: 3/5/2023 7:07:53 PM ******/
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [UC_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UC_UserId]    Script Date: 3/5/2023 7:07:53 PM ******/
ALTER TABLE [dbo].[UserRole] ADD  CONSTRAINT [UC_UserId] UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_RoleId_UserRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[RoleMaster] ([RoleId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_RoleId_UserRole]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserId_UserRole] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserMaster] ([UserId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserId_UserRole]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLoginUser]    Script Date: 3/5/2023 7:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Milan>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetLoginUser]
	@UserName NVARCHAR(100),
	@Password NVARCHAR(100)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Select u.UserId,u.Name,u.Address,u.Phone,u.designation,u.EmailAddress,u.UserName,u.IsActive,u.CreatedOn,u.UpdateOn,r.RoleName as UserRole,ur.RoleId
	from UserMaster u 
	left join UserRole ur on u.UserId=ur.UserId 
	left join RoleMaster r on ur.RoleId=r.RoleId
	where UserName = @UserName and Password=@Password
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUsers]    Script Date: 3/5/2023 7:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Milan>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetUsers]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Select u.UserId,u.Name,u.Address,u.Phone,u.designation,u.EmailAddress,u.UserName,u.IsActive,u.CreatedOn,u.UpdateOn,r.RoleName as UserRole,r.RoleId from UserMaster u 
	left join UserRole ur on u.UserId=ur.UserId 
	left join RoleMaster r on ur.RoleId=r.RoleId	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_InserRefreshToken]    Script Date: 3/5/2023 7:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Author,,Milan>
-- Create date: <Create Date,,04-March-2023>
-- Schema Name: DBO
-- Description: <Description,, Insert Records in UserMaster>
-- History :
-- 
-- =============================================
CREATE PROCEDURE [dbo].[SP_InserRefreshToken]
@Token NVARCHAR(max),
@JwtId VARCHAR(100),
@UserId Bigint,
@CreatedOn datetime,
@ExpiryDate datetime,
@Used bit
AS
BEGIN
SET NOCOUNT ON

Insert into RefreshToken (Token,JwtId,UserId,CreatedOn,ExpiryDate,Used)
Values
(@Token,@JwtId,@UserId,@CreatedOn,@ExpiryDate,@Used)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertRoleMaster]    Script Date: 3/5/2023 7:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Author,,Milan>
-- Create date: <Create Date,,04-March-2023>
-- Schema Name: DBO
-- Description: <Description,, Insert Records in UserMaster>
-- History :
-- 
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertRoleMaster] 
@RoleName VARCHAR(50),
@CreatedOn datetime,
@UpdatedOn datetime
AS
BEGIN
SET NOCOUNT ON

Insert into RoleMaster (RoleName,CreatedOn,UpdatedOn)
Values
(@RoleName,@CreatedOn,@UpdatedOn)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertUserMaster]    Script Date: 3/5/2023 7:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Author,,Milan>
-- Create date: <Create Date,,04-March-2023>
-- Schema Name: DBO
-- Description: <Description,, Insert Records in UserMaster>
-- History :
-- 
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertUserMaster]
@Name VARCHAR(50),
@Address VARCHAR(100),
@Phone NVARCHAR(50),
@Designation VARCHAR(50),
@EmailAddress NVARCHAR(50),
@UserName NVARCHAR(50),
@Password NVARCHAR(50),
@IsActive BIT,
@RoleId Bigint
AS
BEGIN
SET NOCOUNT ON

Declare @UserId bigint;

Insert into UserMaster (Name,Address,Phone,designation,EmailAddress,UserName,Password,IsActive,CreatedOn,UpdateOn)
Values
(@Name,@Address,@Phone,@Designation,@EmailAddress,@UserName,@Password,@IsActive,GETDATE(),GETDATE())

If(@RoleId >0)
Begin
Set @UserId = (Select userid from UserMaster where UserName=@UserName)
insert into UserRole (UserId,RoleId,UpdateOn) values (@UserId,@RoleId,GETDATE())
End

END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateRefreshToken]    Script Date: 3/5/2023 7:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Author,,Milan>
-- Create date: <Create Date,,04-March-2023>
-- Schema Name: DBO
-- Description: <Description,, Insert Records in UserMaster>
-- History :
-- 
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateRefreshToken]
@RefreshTokenId Bigint,
@Token NVARCHAR(max),
@JwtId VARCHAR(100),
@UserId Bigint,
@CreatedOn datetime,
@ExpiryDate datetime,
@Used bit
AS
BEGIN
SET NOCOUNT ON

Update RefreshToken Set Token=@Token,JwtId=@JwtId,UserId=@UserId,CreatedOn=@CreatedOn,ExpiryDate=@ExpiryDate,Used =@Used Where RefreshTokenId=@RefreshTokenId
Select 1

END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateRoleMaster]    Script Date: 3/5/2023 7:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Author,,Milan>
-- Create date: <Create Date,,04-March-2023>
-- Schema Name: DBO
-- Description: <Description,, Insert Records in UserMaster>
-- History :
-- 
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateRoleMaster]
@RoleId Bigint,
@RoleName VARCHAR(50)
AS
BEGIN
SET NOCOUNT ON

update RoleMaster set RoleName=@RoleName,UpdatedOn=GETDATE() where RoleId=@RoleId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateUserMaster]    Script Date: 3/5/2023 7:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Author,,Milan>
-- Create date: <Create Date,,04-March-2023>
-- Schema Name: DBO
-- Description: <Description,, Insert Records in UserMaster>
-- History :
-- 
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateUserMaster]
@UserId BIGINT,
@Name VARCHAR(50),
@Address VARCHAR(100),
@Phone NVARCHAR(50),
@Designation VARCHAR(50),
@EmailAddress NVARCHAR(50),
@UserName NVARCHAR(50),
@Password NVARCHAR(50),
@IsActive BIT,
@RoleId Bigint
AS
BEGIN
SET NOCOUNT ON

Update UserMaster Set Name =@Name,Address=@Address,Phone=@Phone,designation=@Designation,EmailAddress=@EmailAddress,
UpdateOn=GETDATE() Where UserId=@UserId

If(@RoleId>0)
Begin
	if((select count(*) from UserRole where UserId=@UserId)>0)
	Begin
		update UserRole set RoleId=@RoleId where UserId=@UserId
	End
	Else
	Begin
		Insert into UserRole(RoleId,UserId) values (@RoleId,@UserId)
	End		
End
Select 1
END
GO
USE [master]
GO
ALTER DATABASE [StoreToDoorDB] SET  READ_WRITE 
GO
