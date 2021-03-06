USE [master]
GO
/****** Object:  Database [manageuser]    Script Date: 2020/11/11 10:59:00 ******/
CREATE DATABASE [manageuser]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'manageuser', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\manageuser.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'manageuser_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\manageuser_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [manageuser] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [manageuser].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [manageuser] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [manageuser] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [manageuser] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [manageuser] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [manageuser] SET ARITHABORT OFF 
GO
ALTER DATABASE [manageuser] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [manageuser] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [manageuser] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [manageuser] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [manageuser] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [manageuser] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [manageuser] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [manageuser] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [manageuser] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [manageuser] SET  DISABLE_BROKER 
GO
ALTER DATABASE [manageuser] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [manageuser] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [manageuser] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [manageuser] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [manageuser] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [manageuser] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [manageuser] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [manageuser] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [manageuser] SET  MULTI_USER 
GO
ALTER DATABASE [manageuser] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [manageuser] SET DB_CHAINING OFF 
GO
ALTER DATABASE [manageuser] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [manageuser] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [manageuser] SET DELAYED_DURABILITY = DISABLED 
GO
USE [manageuser]
GO
/****** Object:  Table [dbo].[tbl_chitiethoadon]    Script Date: 2020/11/11 10:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_chitiethoadon](
	[mahd] [int] IDENTITY(1,1) NOT NULL,
	[masp] [int] NOT NULL,
	[makh] [int] NOT NULL,
	[tensp] [nvarchar](255) NOT NULL,
	[size] [int] NOT NULL,
	[soluong] [int] NOT NULL,
	[dongia] [int] NOT NULL,
	[tinhtrang] [int] NOT NULL,
 CONSTRAINT [PK_ChiTietHoaDon] PRIMARY KEY CLUSTERED 
(
	[mahd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_hoadon]    Script Date: 2020/11/11 10:59:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_hoadon](
	[mahd] [int] IDENTITY(1,1) NOT NULL,
	[full_name] [nvarchar](255) NOT NULL,
	[ngay_lap_hd] [datetime] NOT NULL,
	[ngay_giaohang] [datetime] NOT NULL,
	[diachi_giaohang] [nvarchar](255) NOT NULL,
	[trangthai] [int] NOT NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[mahd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_khach_hang]    Script Date: 2020/11/11 10:59:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_khach_hang](
	[makh] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[full_name] [nvarchar](100) NOT NULL,
	[gioitinh] [int] NOT NULL,
	[address_user] [nvarchar](255) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[tel] [nvarchar](50) NOT NULL,
	[salt_user] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[makh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_loaihang]    Script Date: 2020/11/11 10:59:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_loaihang](
	[maloai] [int] IDENTITY(1,1) NOT NULL,
	[tenloai] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_LoaiHang] PRIMARY KEY CLUSTERED 
(
	[maloai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_sanpham]    Script Date: 2020/11/11 10:59:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_sanpham](
	[masp] [int] IDENTITY(1,1) NOT NULL,
	[maloai] [int] NOT NULL,
	[tensanpham] [nvarchar](100) NOT NULL,
	[gia_mua] [int] NOT NULL,
	[gia_ban] [int] NOT NULL,
	[size] [varchar(50)] NOT NULL,
	[soluong] [int] NOT NULL,
	[thongtin] [ntext] NOT NULL,
	[ngaynhaphang] [datetime] NOT NULL,
	[hinhanh] [text] NOT NULL,
 CONSTRAINT [PK_sanpham] PRIMARY KEY CLUSTERED 
(
	[masp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_user]    Script Date: 2020/11/11 10:59:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_user](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[password] [nchar](50) NOT NULL,
	[full_name] [nvarchar](255) NOT NULL,
	[tel] [nchar](15) NOT NULL,
	[birthday] [date] NOT NULL,
	[address_user] [nvarchar](150) NOT NULL,
	[role] [int] NOT NULL,
	[salt_random] [nchar](255) NOT NULL,
 CONSTRAINT [PK_tbl_user_1] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [manageuser] SET  READ_WRITE 
GO
