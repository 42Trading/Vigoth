USE [master]
GO
/****** Object:  Database [SavageBeast]    Script Date: 8/31/2016 6:51:15 PM ******/
CREATE DATABASE [SavageBeast]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SavageBeast', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SavageBeast.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SavageBeast_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SavageBeast_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SavageBeast] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SavageBeast].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SavageBeast] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SavageBeast] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SavageBeast] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SavageBeast] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SavageBeast] SET ARITHABORT OFF 
GO
ALTER DATABASE [SavageBeast] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SavageBeast] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SavageBeast] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SavageBeast] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SavageBeast] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SavageBeast] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SavageBeast] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SavageBeast] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SavageBeast] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SavageBeast] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SavageBeast] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SavageBeast] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SavageBeast] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SavageBeast] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SavageBeast] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SavageBeast] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SavageBeast] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SavageBeast] SET RECOVERY FULL 
GO
ALTER DATABASE [SavageBeast] SET  MULTI_USER 
GO
ALTER DATABASE [SavageBeast] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SavageBeast] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SavageBeast] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SavageBeast] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SavageBeast] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SavageBeast', N'ON'
GO
ALTER DATABASE [SavageBeast] SET QUERY_STORE = OFF
GO
USE [SavageBeast]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [SavageBeast]
GO
/****** Object:  Table [dbo].[Brokerage]    Script Date: 8/31/2016 6:51:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brokerage](
	[BrokerageID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Brokerage] PRIMARY KEY CLUSTERED 
(
	[BrokerageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BrokerageAccount]    Script Date: 8/31/2016 6:51:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrokerageAccount](
	[BrokerageAccountID] [uniqueidentifier] NOT NULL,
	[BrokerageID] [uniqueidentifier] NOT NULL,
	[BrokerageAccountTypeID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BrokerageAccount] PRIMARY KEY CLUSTERED 
(
	[BrokerageAccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BrokerageAccountType]    Script Date: 8/31/2016 6:51:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrokerageAccountType](
	[BrokerageAccountTypeID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BrokerageAccountType] PRIMARY KEY CLUSTERED 
(
	[BrokerageAccountTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FinancialInstrument]    Script Date: 8/31/2016 6:51:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinancialInstrument](
	[FinancialInstrumentID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[MarketName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_FinancialInstrument] PRIMARY KEY CLUSTERED 
(
	[FinancialInstrumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MarketData]    Script Date: 8/31/2016 6:51:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarketData](
	[MarketDataID] [int] NOT NULL,
	[MarketDataSourceID] [uniqueidentifier] NOT NULL,
	[TimeIntervalID] [uniqueidentifier] NOT NULL,
	[Open] [money] NOT NULL,
	[Close] [money] NOT NULL,
	[High] [money] NOT NULL,
	[Low] [money] NOT NULL,
	[Volume] [money] NOT NULL,
 CONSTRAINT [PK_MarketData] PRIMARY KEY CLUSTERED 
(
	[MarketDataID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MarketDataSource]    Script Date: 8/31/2016 6:51:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarketDataSource](
	[MarketDataSourceID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[TimeIntervalID] [nchar](10) NULL,
 CONSTRAINT [PK_MarketDataSource] PRIMARY KEY CLUSTERED 
(
	[MarketDataSourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Position]    Script Date: 8/31/2016 6:51:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[PosisionID] [uniqueidentifier] NOT NULL,
	[FinancialInstrumentID] [uniqueidentifier] NOT NULL,
	[PositionTypeID] [uniqueidentifier] NOT NULL,
	[StrategyID] [uniqueidentifier] NOT NULL,
	[BrokerageAccountID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[PosisionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PositionType]    Script Date: 8/31/2016 6:51:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionType](
	[PositionTypeID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PositionType] PRIMARY KEY CLUSTERED 
(
	[PositionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Strategy]    Script Date: 8/31/2016 6:51:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Strategy](
	[StrategyID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[StrategyTypeID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Strategy] PRIMARY KEY CLUSTERED 
(
	[StrategyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StrategyType]    Script Date: 8/31/2016 6:51:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StrategyType](
	[StrategyTypeID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_StrategyType] PRIMARY KEY CLUSTERED 
(
	[StrategyTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TimeInterval]    Script Date: 8/31/2016 6:51:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeInterval](
	[TimeIntervalID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[TimeSpan] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TimeInterval] PRIMARY KEY CLUSTERED 
(
	[TimeIntervalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BrokerageAccount]  WITH CHECK ADD  CONSTRAINT [FK_BrokerageAccount_Brokerage] FOREIGN KEY([BrokerageID])
REFERENCES [dbo].[Brokerage] ([BrokerageID])
GO
ALTER TABLE [dbo].[BrokerageAccount] CHECK CONSTRAINT [FK_BrokerageAccount_Brokerage]
GO
ALTER TABLE [dbo].[BrokerageAccount]  WITH CHECK ADD  CONSTRAINT [FK_BrokerageAccount_BrokerageAccountType] FOREIGN KEY([BrokerageAccountTypeID])
REFERENCES [dbo].[BrokerageAccountType] ([BrokerageAccountTypeID])
GO
ALTER TABLE [dbo].[BrokerageAccount] CHECK CONSTRAINT [FK_BrokerageAccount_BrokerageAccountType]
GO
ALTER TABLE [dbo].[MarketData]  WITH CHECK ADD  CONSTRAINT [FK_MarketData_MarketDataSource] FOREIGN KEY([MarketDataSourceID])
REFERENCES [dbo].[MarketDataSource] ([MarketDataSourceID])
GO
ALTER TABLE [dbo].[MarketData] CHECK CONSTRAINT [FK_MarketData_MarketDataSource]
GO
ALTER TABLE [dbo].[MarketData]  WITH CHECK ADD  CONSTRAINT [FK_MarketData_TimeInterval] FOREIGN KEY([TimeIntervalID])
REFERENCES [dbo].[TimeInterval] ([TimeIntervalID])
GO
ALTER TABLE [dbo].[MarketData] CHECK CONSTRAINT [FK_MarketData_TimeInterval]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_BrokerageAccount] FOREIGN KEY([BrokerageAccountID])
REFERENCES [dbo].[BrokerageAccount] ([BrokerageAccountID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_BrokerageAccount]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_FinancialInstrument] FOREIGN KEY([FinancialInstrumentID])
REFERENCES [dbo].[FinancialInstrument] ([FinancialInstrumentID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_FinancialInstrument]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_PositionType] FOREIGN KEY([PositionTypeID])
REFERENCES [dbo].[PositionType] ([PositionTypeID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_PositionType]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Strategy] FOREIGN KEY([StrategyID])
REFERENCES [dbo].[Strategy] ([StrategyID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_Strategy]
GO
ALTER TABLE [dbo].[Strategy]  WITH CHECK ADD  CONSTRAINT [FK_Strategy_StrategyType] FOREIGN KEY([StrategyTypeID])
REFERENCES [dbo].[StrategyType] ([StrategyTypeID])
GO
ALTER TABLE [dbo].[Strategy] CHECK CONSTRAINT [FK_Strategy_StrategyType]
GO
USE [master]
GO
ALTER DATABASE [SavageBeast] SET  READ_WRITE 
GO
