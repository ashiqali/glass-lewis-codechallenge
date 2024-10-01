USE [CompanyPortalDB]
GO
ALTER TABLE [dbo].[Companies] DROP CONSTRAINT [CHK_ISIN]
GO
/****** Object:  Index [AK_Companies_Isin]    Script Date: 01-10-2024 07:20:21 PM ******/
ALTER TABLE [dbo].[Companies] DROP CONSTRAINT [AK_Companies_Isin]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01-10-2024 07:20:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 01-10-2024 07:20:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Companies]') AND type in (N'U'))
DROP TABLE [dbo].[Companies]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 01-10-2024 07:20:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type in (N'U'))
DROP TABLE [dbo].[__EFMigrationsHistory]
GO
USE [master]
GO
/****** Object:  Database [CompanyPortalDB]    Script Date: 01-10-2024 07:20:21 PM ******/
DROP DATABASE [CompanyPortalDB]
GO
/****** Object:  Database [CompanyPortalDB]    Script Date: 01-10-2024 07:20:21 PM ******/
CREATE DATABASE [CompanyPortalDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CompanyPortalDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CompanyPortalDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CompanyPortalDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CompanyPortalDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CompanyPortalDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CompanyPortalDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CompanyPortalDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CompanyPortalDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CompanyPortalDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CompanyPortalDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CompanyPortalDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CompanyPortalDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET RECOVERY FULL 
GO
ALTER DATABASE [CompanyPortalDB] SET  MULTI_USER 
GO
ALTER DATABASE [CompanyPortalDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CompanyPortalDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CompanyPortalDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CompanyPortalDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CompanyPortalDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CompanyPortalDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CompanyPortalDB', N'ON'
GO
ALTER DATABASE [CompanyPortalDB] SET QUERY_STORE = OFF
GO
USE [CompanyPortalDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 01-10-2024 07:20:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 01-10-2024 07:20:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Ticker] [nvarchar](10) NOT NULL,
	[Exchange] [nvarchar](100) NOT NULL,
	[Isin] [nvarchar](12) NOT NULL,
	[Website] [nvarchar](255) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01-10-2024 07:20:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](20) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240930222916_InitialMigration', N'8.0.8')
GO
SET IDENTITY_INSERT [dbo].[Companies] ON 
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (1, N'Apple Inc.', N'AAPL', N'NASDAQ', N'US0378331005', N'http://www.apple.com', CAST(N'2024-10-01T16:28:05.5871919' AS DateTime2), CAST(N'2024-10-01T16:28:05.5871925' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (2, N'British Airways Plc', N'BAIRY', N'Pink Sheets', N'US1104193065', NULL, CAST(N'2024-10-01T16:28:05.5871971' AS DateTime2), CAST(N'2024-10-01T16:28:05.5871972' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (3, N'Heineken NV', N'HEIA', N'Euronext Amsterdam', N'NL0000009165', NULL, CAST(N'2024-10-01T16:28:05.5871975' AS DateTime2), CAST(N'2024-10-01T16:28:05.5871976' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (4, N'Panasonic Corp', N'6752', N'Tokyo Stock Exchange', N'JP3866800000', N'http://www.panasonic.co.jp', CAST(N'2024-10-01T16:28:05.5871978' AS DateTime2), CAST(N'2024-10-01T16:28:05.5871978' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (5, N'Porsche Automobil', N'PAH3', N'Deutsche Börse', N'DE000PAH0038', N'https://www.porsche.com/', CAST(N'2024-10-01T16:28:05.5871980' AS DateTime2), CAST(N'2024-10-01T16:28:05.5871980' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (6, N'Microsoft Corporation', N'MSFT', N'NASDAQ', N'US5949181045', N'http://www.microsoft.com', CAST(N'2024-10-01T16:28:05.5871984' AS DateTime2), CAST(N'2024-10-01T16:28:05.5871984' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (7, N'Toyota Motor Corporation', N'7203', N'Tokyo Stock Exchange', N'JP3633400001', N'http://www.toyota-global.com', CAST(N'2024-10-01T16:28:05.5871987' AS DateTime2), CAST(N'2024-10-01T16:28:05.5871988' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (8, N'Samsung Electronics Co., Ltd.', N'005930', N'Korea Exchange', N'KR7005930003', N'http://www.samsung.com', CAST(N'2024-10-01T16:28:05.5871992' AS DateTime2), CAST(N'2024-10-01T16:28:05.5871993' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (9, N'Nestle S.A.', N'NESN', N'SIX Swiss Exchange', N'CH0038863350', N'http://www.nestle.com', CAST(N'2024-10-01T16:28:05.5871994' AS DateTime2), CAST(N'2024-10-01T16:28:05.5871995' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (10, N'Alphabet Inc.', N'GOOGL', N'NASDAQ', N'US02079K3059', N'http://www.abc.xyz', CAST(N'2024-10-01T16:28:05.5871999' AS DateTime2), CAST(N'2024-10-01T16:28:05.5872000' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (11, N'Amazon.com, Inc.', N'AMZN', N'NASDAQ', N'US0231351067', N'http://www.amazon.com', CAST(N'2024-10-01T16:28:05.5872002' AS DateTime2), CAST(N'2024-10-01T16:28:05.5872003' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (12, N'Tesla, Inc.', N'TSLA', N'NASDAQ', N'US88160R1014', N'http://www.tesla.com', CAST(N'2024-10-01T16:28:05.5872005' AS DateTime2), CAST(N'2024-10-01T16:28:05.5872005' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (13, N'Sony Corporation', N'6758', N'Tokyo Stock Exchange', N'JP3435000009', N'http://www.sony.net', CAST(N'2024-10-01T16:28:05.5872007' AS DateTime2), CAST(N'2024-10-01T16:28:05.5872008' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (14, N'Unilever PLC', N'ULVR', N'London Stock Exchange', N'GB00B10RZP78', N'http://www.unilever.com', CAST(N'2024-10-01T16:28:05.5872010' AS DateTime2), CAST(N'2024-10-01T16:28:05.5872011' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (15, N'Volkswagen AG', N'VOW3', N'Frankfurt Stock Exchange', N'DE0007664039', N'http://www.volkswagenag.com', CAST(N'2024-10-01T16:28:05.5872013' AS DateTime2), CAST(N'2024-10-01T16:28:05.5872013' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (17, N'Intel Corporation', N'INTC', N'NASDAQ', N'US4581401001', N'http://www.intel.com', CAST(N'2024-10-01T16:28:05.5872015' AS DateTime2), CAST(N'2024-10-01T16:28:05.5872016' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (18, N'IBM Corporation 1', N'IBM', N'NYSE', N'US4592001014', N'http://www.ibm.com', CAST(N'2024-10-01T16:30:06.4798288' AS DateTime2), CAST(N'2024-10-01T16:30:06.4798317' AS DateTime2))
GO
INSERT [dbo].[Companies] ([Id], [Name], [Ticker], [Exchange], [Isin], [Website], [CreatedDate], [ModifiedDate]) VALUES (19, N'Existing Company', N'EXC', N'NYSE', N'US1234567890', NULL, CAST(N'2024-10-01T16:40:23.2666351' AS DateTime2), CAST(N'2024-10-01T16:40:23.2666351' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Companies] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Username], [Password], [Name], [Surname], [CreatedDate], [ModifiedDate]) VALUES (1, N'admin', N'AQAAAAIAAYagAAAAEJwj1xV9cSZFncv7TsB4IXKrYhYO7UMPu3mAgBB28pkIhZX+GawBZwYyF6dfF4QGaQ==', N'Admin', N'Admin', CAST(N'2024-09-30T22:29:15.5520873' AS DateTime2), CAST(N'2024-09-30T22:29:15.5520878' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Companies_Isin]    Script Date: 01-10-2024 07:20:21 PM ******/
ALTER TABLE [dbo].[Companies] ADD  CONSTRAINT [AK_Companies_Isin] UNIQUE NONCLUSTERED 
(
	[Isin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Companies]  WITH CHECK ADD  CONSTRAINT [CHK_ISIN] CHECK  ((len([Isin])=(12) AND [Isin] like '[A-Z][A-Z]%'))
GO
ALTER TABLE [dbo].[Companies] CHECK CONSTRAINT [CHK_ISIN]
GO
USE [master]
GO
ALTER DATABASE [CompanyPortalDB] SET  READ_WRITE 
GO
