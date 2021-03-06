USE [master]
GO
/****** Object:  Database [Recommender_System]    Script Date: 8/23/2021 12:57:55 PM ******/
CREATE DATABASE [Recommender_System]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Recommender_System', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Recommender_System.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Recommender_System_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Recommender_System_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Recommender_System] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Recommender_System].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Recommender_System] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Recommender_System] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Recommender_System] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Recommender_System] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Recommender_System] SET ARITHABORT OFF 
GO
ALTER DATABASE [Recommender_System] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Recommender_System] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Recommender_System] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Recommender_System] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Recommender_System] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Recommender_System] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Recommender_System] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Recommender_System] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Recommender_System] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Recommender_System] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Recommender_System] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Recommender_System] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Recommender_System] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Recommender_System] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Recommender_System] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Recommender_System] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Recommender_System] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Recommender_System] SET RECOVERY FULL 
GO
ALTER DATABASE [Recommender_System] SET  MULTI_USER 
GO
ALTER DATABASE [Recommender_System] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Recommender_System] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Recommender_System] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Recommender_System] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Recommender_System] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Recommender_System] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Recommender_System', N'ON'
GO
ALTER DATABASE [Recommender_System] SET QUERY_STORE = OFF
GO
USE [Recommender_System]
GO
/****** Object:  Schema [Disputes]    Script Date: 8/23/2021 12:57:55 PM ******/
CREATE SCHEMA [Disputes]
GO
/****** Object:  Schema [General]    Script Date: 8/23/2021 12:57:55 PM ******/
CREATE SCHEMA [General]
GO
/****** Object:  Schema [Labor]    Script Date: 8/23/2021 12:57:55 PM ******/
CREATE SCHEMA [Labor]
GO
/****** Object:  Schema [Payments]    Script Date: 8/23/2021 12:57:55 PM ******/
CREATE SCHEMA [Payments]
GO
/****** Object:  Schema [Products]    Script Date: 8/23/2021 12:57:55 PM ******/
CREATE SCHEMA [Products]
GO
/****** Object:  Schema [Sales]    Script Date: 8/23/2021 12:57:55 PM ******/
CREATE SCHEMA [Sales]
GO
/****** Object:  Schema [Stocks]    Script Date: 8/23/2021 12:57:55 PM ******/
CREATE SCHEMA [Stocks]
GO
/****** Object:  Schema [Users]    Script Date: 8/23/2021 12:57:55 PM ******/
CREATE SCHEMA [Users]
GO
/****** Object:  Table [dbo].[MemorySize]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemorySize](
	[Value] [varchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductReview]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductReview](
	[CustomerName] [varchar](600) NULL,
	[Rating] [int] NULL,
	[Email] [varchar](2000) NULL,
	[Review] [varchar](max) NULL,
	[ProductId] [int] NOT NULL,
	[TimeStamp] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScreenSize]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScreenSize](
	[Value] [varchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [General].[NotificationMessage]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [General].[NotificationMessage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](max) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[MessageType] [varchar](50) NOT NULL,
	[NotificationType] [varchar](50) NOT NULL,
	[Icon] [varchar](max) NOT NULL,
	[IsAjaxMessage] [bit] NOT NULL,
	[IsViewMessage] [bit] NOT NULL,
	[IsRedirectMessage] [bit] NOT NULL,
	[UserID] [int] NULL,
	[UserType] [varchar](50) NULL,
	[TimeStamp] [datetime] NOT NULL,
	[URL] [varchar](max) NOT NULL,
	[Viewed] [bit] NOT NULL,
	[Code] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [General].[WebSettings]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [General].[WebSettings](
	[ProjectTitle] [varchar](max) NULL,
	[ProjectImagePath] [varchar](max) NULL,
	[Address] [varchar](max) NULL,
	[Record] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Payments].[Cheque]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Payments].[Cheque](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[Amount] [float] NOT NULL,
	[Cleared] [bit] NOT NULL,
	[AccountNumber] [varchar](max) NOT NULL,
	[ChequeNumber] [varchar](max) NOT NULL,
	[TimeStamp] [datetime] NULL,
	[ChequeDate] [datetime] NOT NULL,
	[ImagePath] [varchar](max) NULL,
	[Cleared_Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Payments].[Expense]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Payments].[Expense](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Amount] [float] NOT NULL,
	[DocumentNo] [varchar](50) NOT NULL,
	[ImagePath] [varchar](max) NULL,
	[Description] [varchar](max) NULL,
	[UserID] [int] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[ExpenseDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Payments].[ExpenseCategory]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Payments].[ExpenseCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Payments].[Payment]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Payments].[Payment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [float] NOT NULL,
	[PaymentMethod] [varchar](50) NULL,
	[TimeStamp] [datetime] NOT NULL,
	[CustomerID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Payments].[Payment_Cheque]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Payments].[Payment_Cheque](
	[PaymentID] [int] NOT NULL,
	[ChequeID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Products].[Category]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Products].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
	[Sellable] [bit] NOT NULL,
	[Packed] [bit] NOT NULL,
	[LaborMeterial] [bit] NOT NULL,
	[RawMeterial] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Products].[Color]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Products].[Color](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ColorCode] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Products].[Formula]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Products].[Formula](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Products].[Formula_Product]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Products].[Formula_Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FormulaID] [int] NOT NULL,
	[ProductPackagingID] [int] NOT NULL,
	[Used] [bit] NOT NULL,
	[UsedQuantity] [int] NOT NULL,
	[Created] [bit] NOT NULL,
	[CreatedQuantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Products].[Packaging]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Products].[Packaging](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Products].[Product]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Products].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](5000) NULL,
	[Description] [nvarchar](max) NULL,
	[CategoryID] [int] NULL,
	[UnitID] [int] NOT NULL,
	[SKU] [varchar](max) NULL,
	[MU] [varchar](max) NULL,
	[ImagePath] [varchar](max) NULL,
	[Size] [varchar](50) NULL,
	[Color] [varchar](50) NULL,
	[Type] [varchar](50) NULL,
	[Vendor] [varchar](50) NULL,
	[Featured] [bit] NOT NULL,
	[ScreenSize] [varchar](10) NULL,
	[Memory] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Products].[Product_Color]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Products].[Product_Color](
	[ProductID] [int] NOT NULL,
	[ColorID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[ColorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Products].[Product_Packaging]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Products].[Product_Packaging](
	[ProductID] [int] NOT NULL,
	[PackagingID] [int] NOT NULL,
	[PackageSize] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[PricePerPiece] [float] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RetailPrice] [float] NOT NULL,
	[NotifyQuantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Products].[Product_Size]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Products].[Product_Size](
	[ProductID] [int] NOT NULL,
	[SizeID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[SizeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Products].[Size]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Products].[Size](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Products].[Unit]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Products].[Unit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Products].[WishList]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Products].[WishList](
	[ProductId] [int] NOT NULL,
	[UserId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Sales].[Customer]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[DisplayName] [varchar](100) NULL,
	[CompanyName] [varchar](100) NULL,
	[Email] [varchar](250) NULL,
	[Phone] [varchar](50) NULL,
	[Mobile] [varchar](50) NULL,
	[Address] [varchar](max) NULL,
	[CreditLimit] [float] NOT NULL,
	[CreditAmount] [float] NOT NULL,
	[Discount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Sales].[Invoice]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[Invoice](
	[ID] [int] IDENTITY(52411,1) NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[OrderDetails] [varchar](max) NULL,
	[CustomerID] [int] NOT NULL,
	[PaidDate] [datetime] NULL,
	[AmountPayable] [float] NOT NULL,
	[Discount] [int] NOT NULL,
	[SubTotal] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Sales].[Invoice_Product_Packaging]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[Invoice_Product_Packaging](
	[InvoiceID] [int] NOT NULL,
	[Product_Packaging_ID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[AmountPayable] [float] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Stocks].[Stock]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Stocks].[Stock](
	[ProductID] [int] NOT NULL,
	[PackagingID] [int] NOT NULL,
	[PackageSize] [int] NOT NULL,
	[Quantity] [varchar](max) NULL,
	[TimeStamp] [datetime] NOT NULL,
	[QuantityLeft] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Reason] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Users].[AuthToken]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Users].[AuthToken](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Selector] [varchar](12) NULL,
	[Token] [varchar](max) NULL,
	[Email] [varchar](max) NULL,
	[Expires] [datetime] NULL,
	[IpAddress] [varchar](50) NULL,
	[Impersonate] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Users].[BlockedEmail]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Users].[BlockedEmail](
	[Email] [varchar](max) NOT NULL,
	[Counter] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Users].[Role]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Users].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CanAddProducts] [bit] NOT NULL,
	[CanAddInvoice] [bit] NOT NULL,
	[CanViewInvoice] [bit] NOT NULL,
	[CanUpdateInvoice] [bit] NOT NULL,
	[CanDeleteInvoice] [bit] NOT NULL,
	[CanViewProduct] [bit] NOT NULL,
	[CanUpdateProduct] [bit] NOT NULL,
	[CanDeleteProduct] [bit] NOT NULL,
	[CanViewStock] [bit] NOT NULL,
	[CanAddStock] [bit] NOT NULL,
	[CanUpdateStock] [bit] NOT NULL,
	[CanDeleteStock] [bit] NOT NULL,
	[CanViewDashboard] [bit] NOT NULL,
	[CanChangeProductSettings] [bit] NOT NULL,
	[CanChangeBranding] [bit] NOT NULL,
	[CanViewUsers] [bit] NOT NULL,
	[CanAddUsers] [bit] NOT NULL,
	[CanDeleteUsers] [bit] NOT NULL,
	[CanUpdateUsers] [bit] NOT NULL,
	[CanViewRoles] [bit] NOT NULL,
	[CanAddRoles] [bit] NOT NULL,
	[CanDeleteRoles] [bit] NOT NULL,
	[CanUpdateRoles] [bit] NOT NULL,
	[CanViewCustomer] [bit] NOT NULL,
	[CanAddCustomer] [bit] NOT NULL,
	[CanUpdateCustomer] [bit] NOT NULL,
	[CanDeleteCustomer] [bit] NOT NULL,
	[IsDeveloper] [bit] NOT NULL,
	[CanReturnInvoice] [bit] NOT NULL,
	[CanViewDispute] [bit] NOT NULL,
	[CanResolveDispute] [bit] NOT NULL,
	[CanViewCheque] [bit] NOT NULL,
	[CanViewExpense] [bit] NOT NULL,
	[CanAddExpense] [bit] NOT NULL,
	[CanUpdateExpense] [bit] NOT NULL,
	[CanDeleteExpense] [bit] NOT NULL,
	[CanHardDeleteFormulas] [bit] NOT NULL,
	[CanDeleteFormulas] [bit] NOT NULL,
	[CanUpdateFormulas] [bit] NOT NULL,
	[CanAddFormulas] [bit] NOT NULL,
	[CanViewFormulas] [bit] NOT NULL,
	[CanAddLabor] [bit] NOT NULL,
	[CanViewLabor] [bit] NOT NULL,
	[CanUpdateLabor] [bit] NOT NULL,
	[CanDeleteLabor] [bit] NOT NULL,
	[CanAddWorker] [bit] NOT NULL,
	[CanUpdateWorker] [bit] NOT NULL,
	[CanDeleteWorker] [bit] NOT NULL,
	[CanViewWorker] [bit] NOT NULL,
	[CanHardDeleteWorker] [bit] NOT NULL,
	[CanHardDeleteLabor] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Users].[User]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Users].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NULL,
	[Password] [varchar](max) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[MobileNo] [varchar](50) NOT NULL,
	[Birthday] [datetime] NULL,
	[Gender] [varchar](50) NOT NULL,
	[BlockedStatus] [bit] NOT NULL,
	[LastLogin] [datetime] NOT NULL,
	[LastLoginIpAddress] [varchar](50) NULL,
	[LastModified] [varchar](50) NOT NULL,
	[AccountCreation] [datetime] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[BlockedReason] [varchar](max) NULL,
	[FirebaseToken] [varchar](max) NULL,
	[RoleID] [int] NOT NULL,
	[Address] [varchar](max) NULL,
 CONSTRAINT [PK__User__3214EC27C82298B8] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductReview] ADD  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [General].[NotificationMessage] ADD  DEFAULT ((0)) FOR [IsAjaxMessage]
GO
ALTER TABLE [General].[NotificationMessage] ADD  DEFAULT ((0)) FOR [IsViewMessage]
GO
ALTER TABLE [General].[NotificationMessage] ADD  DEFAULT ((0)) FOR [IsRedirectMessage]
GO
ALTER TABLE [General].[NotificationMessage] ADD  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [General].[NotificationMessage] ADD  DEFAULT ('#/') FOR [URL]
GO
ALTER TABLE [General].[NotificationMessage] ADD  DEFAULT ((0)) FOR [Viewed]
GO
ALTER TABLE [General].[NotificationMessage] ADD  DEFAULT ((0)) FOR [Code]
GO
ALTER TABLE [General].[WebSettings] ADD  CONSTRAINT [DF__WebSettin__Recor__73852659]  DEFAULT (getdate()) FOR [Record]
GO
ALTER TABLE [Payments].[Cheque] ADD  DEFAULT ((0)) FOR [Cleared]
GO
ALTER TABLE [Payments].[Cheque] ADD  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [Payments].[Expense] ADD  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [Payments].[Expense] ADD  DEFAULT (getdate()) FOR [ExpenseDate]
GO
ALTER TABLE [Payments].[Payment] ADD  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [Products].[Category] ADD  DEFAULT ((0)) FOR [Sellable]
GO
ALTER TABLE [Products].[Category] ADD  DEFAULT ((0)) FOR [Packed]
GO
ALTER TABLE [Products].[Category] ADD  DEFAULT ((0)) FOR [LaborMeterial]
GO
ALTER TABLE [Products].[Category] ADD  DEFAULT ((0)) FOR [RawMeterial]
GO
ALTER TABLE [Products].[Formula_Product] ADD  DEFAULT ((0)) FOR [Used]
GO
ALTER TABLE [Products].[Formula_Product] ADD  DEFAULT ((0)) FOR [UsedQuantity]
GO
ALTER TABLE [Products].[Formula_Product] ADD  DEFAULT ((0)) FOR [Created]
GO
ALTER TABLE [Products].[Product] ADD  DEFAULT ((1)) FOR [UnitID]
GO
ALTER TABLE [Products].[Product] ADD  DEFAULT ((0)) FOR [Featured]
GO
ALTER TABLE [Products].[Product_Packaging] ADD  DEFAULT ((0)) FOR [PackageSize]
GO
ALTER TABLE [Products].[Product_Packaging] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [Products].[Product_Packaging] ADD  DEFAULT ((0)) FOR [PricePerPiece]
GO
ALTER TABLE [Products].[Product_Packaging] ADD  DEFAULT ((5)) FOR [NotifyQuantity]
GO
ALTER TABLE [Sales].[Customer] ADD  DEFAULT ((0)) FOR [CreditLimit]
GO
ALTER TABLE [Sales].[Customer] ADD  DEFAULT ((0)) FOR [CreditAmount]
GO
ALTER TABLE [Sales].[Customer] ADD  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [Sales].[Invoice] ADD  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [Sales].[Invoice] ADD  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [Sales].[Invoice] ADD  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [Sales].[Invoice] ADD  DEFAULT ((0)) FOR [SubTotal]
GO
ALTER TABLE [Stocks].[Stock] ADD  CONSTRAINT [DF__Stock__TimeStamp__5535A963]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [Stocks].[Stock] ADD  CONSTRAINT [DF__Stock__QuantityL__6E01572D]  DEFAULT ((0)) FOR [QuantityLeft]
GO
ALTER TABLE [Users].[AuthToken] ADD  DEFAULT ((0)) FOR [Impersonate]
GO
ALTER TABLE [Users].[BlockedEmail] ADD  DEFAULT ((0)) FOR [Counter]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanAddProducts]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanAddInvoice]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanViewInvoice]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanUpdateInvoice]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanDeleteInvoice]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanViewProduct]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanUpdateProduct]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanDeleteProduct]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanViewStock]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanAddStock]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanUpdateStock]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanDeleteStock]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanViewDashboard]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanChangeProductSettings]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanChangeBranding]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanViewUsers]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanAddUsers]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanDeleteUsers]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanUpdateUsers]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanViewRoles]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanAddRoles]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanDeleteRoles]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanUpdateRoles]
GO
ALTER TABLE [Users].[Role] ADD  CONSTRAINT [DF_Role_CanViewCustomer]  DEFAULT ((0)) FOR [CanViewCustomer]
GO
ALTER TABLE [Users].[Role] ADD  CONSTRAINT [DF_Role_CanAddCustomer]  DEFAULT ((0)) FOR [CanAddCustomer]
GO
ALTER TABLE [Users].[Role] ADD  CONSTRAINT [DF_Role_CanUpdateCustomer]  DEFAULT ((0)) FOR [CanUpdateCustomer]
GO
ALTER TABLE [Users].[Role] ADD  CONSTRAINT [DF_Role_CanDeleteCustomer]  DEFAULT ((0)) FOR [CanDeleteCustomer]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [IsDeveloper]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanReturnInvoice]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanViewDispute]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanResolveDispute]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanViewCheque]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanViewExpense]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanAddExpense]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanUpdateExpense]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanDeleteExpense]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanHardDeleteFormulas]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanDeleteFormulas]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanUpdateFormulas]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanAddFormulas]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanViewFormulas]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanAddLabor]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanViewLabor]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanUpdateLabor]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanDeleteLabor]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanAddWorker]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanUpdateWorker]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanDeleteWorker]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanViewWorker]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanHardDeleteWorker]
GO
ALTER TABLE [Users].[Role] ADD  DEFAULT ((0)) FOR [CanHardDeleteLabor]
GO
ALTER TABLE [Users].[User] ADD  CONSTRAINT [DF__User__BlockedSta__182C9B23]  DEFAULT ((0)) FOR [BlockedStatus]
GO
ALTER TABLE [Users].[User] ADD  CONSTRAINT [DF__User__LastLogin__1920BF5C]  DEFAULT (getdate()) FOR [LastLogin]
GO
ALTER TABLE [Users].[User] ADD  CONSTRAINT [DF__User__LastModifi__1A14E395]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [Users].[User] ADD  CONSTRAINT [DF__User__AccountCre__1B0907CE]  DEFAULT (getdate()) FOR [AccountCreation]
GO
ALTER TABLE [Payments].[Cheque]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [Sales].[Customer] ([ID])
GO
ALTER TABLE [Payments].[Expense]  WITH CHECK ADD  CONSTRAINT [FK__Expense__UserID__17F790F9] FOREIGN KEY([UserID])
REFERENCES [Users].[User] ([Id])
GO
ALTER TABLE [Payments].[Expense] CHECK CONSTRAINT [FK__Expense__UserID__17F790F9]
GO
ALTER TABLE [Payments].[Payment_Cheque]  WITH CHECK ADD FOREIGN KEY([PaymentID])
REFERENCES [Payments].[Payment] ([ID])
GO
ALTER TABLE [Products].[Formula_Product]  WITH CHECK ADD FOREIGN KEY([FormulaID])
REFERENCES [Products].[Formula] ([ID])
GO
ALTER TABLE [Products].[Formula_Product]  WITH CHECK ADD FOREIGN KEY([ProductPackagingID])
REFERENCES [Products].[Product_Packaging] ([ID])
GO
ALTER TABLE [Products].[Product]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [Products].[Category] ([ID])
GO
ALTER TABLE [Products].[Product]  WITH CHECK ADD FOREIGN KEY([UnitID])
REFERENCES [Products].[Unit] ([ID])
GO
ALTER TABLE [Products].[Product_Color]  WITH CHECK ADD FOREIGN KEY([ColorID])
REFERENCES [Products].[Color] ([ID])
GO
ALTER TABLE [Products].[Product_Color]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [Products].[Product] ([ID])
GO
ALTER TABLE [Products].[Product_Packaging]  WITH CHECK ADD FOREIGN KEY([PackagingID])
REFERENCES [Products].[Packaging] ([ID])
GO
ALTER TABLE [Products].[Product_Packaging]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [Products].[Product] ([ID])
GO
ALTER TABLE [Products].[Product_Size]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [Products].[Product] ([ID])
GO
ALTER TABLE [Products].[Product_Size]  WITH CHECK ADD FOREIGN KEY([SizeID])
REFERENCES [Products].[Size] ([ID])
GO
ALTER TABLE [Sales].[Invoice]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [Sales].[Customer] ([ID])
GO
ALTER TABLE [Sales].[Invoice_Product_Packaging]  WITH CHECK ADD FOREIGN KEY([InvoiceID])
REFERENCES [Sales].[Invoice] ([ID])
GO
ALTER TABLE [Sales].[Invoice_Product_Packaging]  WITH CHECK ADD FOREIGN KEY([Product_Packaging_ID])
REFERENCES [Products].[Product_Packaging] ([ID])
GO
ALTER TABLE [Sales].[Invoice_Product_Packaging]  WITH CHECK ADD FOREIGN KEY([Product_Packaging_ID])
REFERENCES [Products].[Product_Packaging] ([ID])
GO
ALTER TABLE [Stocks].[Stock]  WITH CHECK ADD  CONSTRAINT [FK__Stock__Packaging__6383C8BA] FOREIGN KEY([PackagingID])
REFERENCES [Products].[Packaging] ([ID])
GO
ALTER TABLE [Stocks].[Stock] CHECK CONSTRAINT [FK__Stock__Packaging__6383C8BA]
GO
ALTER TABLE [Stocks].[Stock]  WITH CHECK ADD  CONSTRAINT [FK__Stock__ProductID__6477ECF3] FOREIGN KEY([ProductID])
REFERENCES [Products].[Product] ([ID])
GO
ALTER TABLE [Stocks].[Stock] CHECK CONSTRAINT [FK__Stock__ProductID__6477ECF3]
GO
ALTER TABLE [Stocks].[Stock]  WITH CHECK ADD  CONSTRAINT [FK__Stock__UserID__73852659] FOREIGN KEY([UserID])
REFERENCES [Users].[User] ([Id])
GO
ALTER TABLE [Stocks].[Stock] CHECK CONSTRAINT [FK__Stock__UserID__73852659]
GO
ALTER TABLE [Users].[User]  WITH CHECK ADD  CONSTRAINT [FK__User__RoleID__1AD3FDA4] FOREIGN KEY([RoleID])
REFERENCES [Users].[Role] ([ID])
GO
ALTER TABLE [Users].[User] CHECK CONSTRAINT [FK__User__RoleID__1AD3FDA4]
GO
/****** Object:  StoredProcedure [General].[UpdateProjectSettings]    Script Date: 8/23/2021 12:57:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [General].[UpdateProjectSettings]
(
@ProjectTitle VARCHAR(MAX),
@ProjectImagePath VARCHAR(MAX)
)
AS
if((SELECT COUNT(*) FROM [General].[WebSettings]) = 0)
BEGIN
INSERT INTO [General].[WebSettings] (ProjectTitle, ProjectImagePath) VALUES(@ProjectTitle, @ProjectImagePath)
END
ELSE
BEGIN
UPDATE [General].[WebSettings] SET ProjectTitle = @ProjectTitle, ProjectImagePath = @ProjectImagePath 
END
GO
USE [master]
GO
ALTER DATABASE [Recommender_System] SET  READ_WRITE 
GO
