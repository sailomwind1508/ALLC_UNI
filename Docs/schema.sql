USE [DB_ALL_CASH_UNI]
GO

GO
/****** Object:  Table [dbo].[tbl_AdmAuthorize]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_AdmAuthorize](
	[AuthGrpID] [varchar](50) NOT NULL,
	[AuthDetail] [xml] NULL,
	[CrDate] [varchar](50) NULL,
	[UserNo] [varchar](50) NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_AuthGrp] PRIMARY KEY CLUSTERED 
(
	[AuthGrpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_AdmControlList]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_AdmControlList](
	[ControlID] [int] IDENTITY(1,1) NOT NULL,
	[ControlName] [varchar](50) NULL,
	[FormID] [int] NULL,
	[ControlText] [varchar](50) NULL,
	[Seq] [smallint] NULL,
 CONSTRAINT [PK_tbl_AdmControlList] PRIMARY KEY CLUSTERED 
(
	[ControlID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_AdmFormList]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_AdmFormList](
	[FormID] [int] IDENTITY(1,1) NOT NULL,
	[FormName] [varchar](50) NOT NULL,
	[FormText] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_AdmFormList] PRIMARY KEY CLUSTERED 
(
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_AdmMenuList]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[tbl_AdmMenuList](
	[GrpAutoID] [int] IDENTITY(1,1) NOT NULL,
	[MIndex] [int] NOT NULL,
	[FormMode] [varchar](1) NULL,
	[MenuName] [varchar](100) NULL,
	[MenuText] [varchar](100) NULL,
	[MenuText2] [varchar](100) NULL,
	[FormName] [varchar](100) NULL,
	[MenuParent] [varchar](50) NULL,
	[ImageIndex] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[FlagSend] [bit] NOT NULL CONSTRAINT [DF_AdmFormList_FlagSend]  DEFAULT ((0)),
 CONSTRAINT [PK_tbl_Menu_List] PRIMARY KEY CLUSTERED 
(
	[GrpAutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_AdmRoleControl]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_AdmRoleControl](
	[RoleID] [int] NOT NULL,
	[ControlID] [int] NOT NULL,
	[Visible] [bit] NULL,
	[Enable] [bit] NULL,
	[DefaultValue] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_AdmRoleControl] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC,
	[ControlID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_AmtArCustomer]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_AmtArCustomer](
	[CycleNo] [smallint] NOT NULL,
	[Year] [nvarchar](4) NOT NULL,
	[DepotNo] [nvarchar](10) NOT NULL,
	[WHID] [nvarchar](20) NOT NULL,
	[SalAreaID] [nvarchar](50) NOT NULL,
	[AmtQty] [int] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [nvarchar](50) NOT NULL,
	[EdDate] [datetime] NOT NULL,
	[EdUser] [nvarchar](50) NOT NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[Seq] [smallint] NOT NULL,
	[TitleName] [nvarchar](10) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[SalAreaName] [nvarchar](100) NOT NULL,
	[ZoneName] [nvarchar](50) NOT NULL,
	[SaleEmpID] [nvarchar](20) NOT NULL,
	[ZoneID] [int] NOT NULL,
	[WHName] [nvarchar](100) NOT NULL,
	[WHSeq] [int] NOT NULL,
	[AmtArCustID] [nvarchar](20) NOT NULL,
	[VanTypeID] [int] NOT NULL,
	[VanTypeName] [nvarchar](50) NOT NULL,
	[VanTypeSeq] [int] NOT NULL,
	[WHType] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_AmtArCustomerDetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_AmtArCustomerDetail](
	[CustomerID] [varchar](20) NOT NULL,
	[Seq] [smallint] NOT NULL,
	[CustTitle] [nvarchar](20) NOT NULL,
	[CustName] [nvarchar](100) NOT NULL,
	[CustShotName] [nvarchar](50) NOT NULL,
	[Contact] [varchar](50) NOT NULL,
	[BillTo] [varchar](200) NOT NULL,
	[AreaName] [nvarchar](50) NOT NULL,
	[DistrictName] [nvarchar](50) NOT NULL,
	[ProvinceName] [nvarchar](50) NOT NULL,
	[PostalCode] [varchar](10) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Telephone] [varchar](50) NOT NULL,
	[ShopTypeID] [int] NOT NULL,
	[ShopTypeName] [nvarchar](50) NOT NULL,
	[AmtArCustID] [varchar](20) NOT NULL,
	[Latitude] [varchar](50) NULL,
	[Longitude] [varchar](50) NULL,
	[GPSDate] [datetime] NULL,
	[CustomerImg] [image] NULL,
	[FlagShelf] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ApSupplier]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ApSupplier](
	[SupplierID] [varchar](20) NOT NULL,
	[SupplierCode] [varchar](20) NOT NULL,
	[SupplierRefCode] [varchar](50) NOT NULL,
	[SuppTitle] [varchar](50) NOT NULL,
	[SuppName] [varchar](50) NOT NULL,
	[SuppShortName] [varchar](50) NULL,
	[SupplierTypeID] [int] NULL,
	[BillTo] [varchar](200) NULL,
	[ShipTo] [varchar](200) NULL,
	[Telephone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Contact] [varchar](50) NULL,
	[AddressNo] [varchar](50) NULL,
	[lane] [varchar](50) NULL,
	[Street] [varchar](50) NULL,
	[ProvinceID] [int] NULL,
	[AreaID] [int] NULL,
	[DistrictID] [int] NULL,
	[PostalCode] [varchar](10) NULL,
	[Email] [varchar](50) NULL,
	[CreditDay] [tinyint] NOT NULL,
	[TaxId] [char](15) NULL,
	[VatType] [bit] NULL,
	[DiscountType] [char](1) NULL,
	[Discount] [decimal](18, 3) NULL,
	[SaleAp] [varchar](50) NULL,
	[SaleApTel] [varchar](50) NULL,
	[LeadTime] [int] NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_ApSupplier] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ApSupplierType]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ApSupplierType](
	[APSupplierTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ApSupplierTypeCode] [char](2) NOT NULL,
	[ApSupplierTypeName] [varchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_ApSupplierType] PRIMARY KEY CLUSTERED 
(
	[APSupplierTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ArCustomer]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ArCustomer](
	[CustomerID] [varchar](20) NOT NULL,
	[CustomerCode] [char](13) NOT NULL,
	[CustomerRefCode] [varchar](50) NOT NULL,
	[Seq] [smallint] NOT NULL,
	[CustTitle] [nvarchar](20) NOT NULL,
	[CustName] [nvarchar](200) NULL,
	[CustShortName] [nvarchar](200) NULL,
	[CustomerTypeID] [int] NULL,
	[BillTo] [varchar](200) NULL,
	[ShipTo] [varchar](200) NULL,
	[Telephone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Contact] [varchar](200) NULL,
	[AddressNo] [varchar](200) NULL,
	[lane] [varchar](50) NULL,
	[Street] [varchar](50) NULL,
	[AreaID] [int] NULL,
	[DistrictID] [int] NULL,
	[ProvinceID] [int] NULL,
	[PostalCode] [varchar](10) NULL,
	[Email] [varchar](50) NULL,
	[CreditDay] [tinyint] NOT NULL,
	[TaxId] [char](15) NULL,
	[VatType] [bit] NULL,
	[VatRate] [numeric](18, 3) NULL,
	[DiscountType] [char](1) NULL,
	[Discount] [decimal](18, 3) NULL,
	[EmpID] [varchar](20) NULL,
	[PriceGroupID] [int] NULL,
	[WHID] [varchar](20) NOT NULL,
	[SalAreaID] [varchar](20) NULL,
	[ShopTypeID] [int] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[FlagMember] [bit] NOT NULL,
	[NetPoint] [int] NOT NULL,
	[CustomerSAPCode] [varchar](50) NOT NULL,
	[Latitude] [varchar](50) NULL,
	[Longitude] [varchar](50) NULL,
	[GPSDate] [datetime] NULL,
	[CustomerImg] [image] NULL,
	[FlagShelf] [bit] NOT NULL,
	[PromotionVanID] [bit] NOT NULL,
 CONSTRAINT [PK_ArCustomer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ArCustomerShelf]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ArCustomerShelf](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[ShelfID] [varchar](20) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[FlagNew] [bit] NOT NULL,
	[FlagEdit] [bit] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_ArCustomerShelf] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ArCustomerType]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ArCustomerType](
	[ArCustomerTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ArCustomerTypeCode] [char](2) NOT NULL,
	[ArCustomerTypeName] [varchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_ArCustomerType] PRIMARY KEY CLUSTERED 
(
	[ArCustomerTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Banknote]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Banknote](
	[BanknoteID] [int] IDENTITY(1,1) NOT NULL,
	[BanknoteName] [varchar](50) NULL,
	[BanknoteValue] [int] NULL,
	[BanknoteType] [varchar](50) NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_Banknote] PRIMARY KEY CLUSTERED 
(
	[BanknoteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Branch]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Branch](
	[BranchID] [varchar](20) NOT NULL,
	[BranchCode] [varchar](6) NOT NULL,
	[BranchRefCode] [varchar](10) NOT NULL,
	[BranchName] [varchar](50) NOT NULL,
	[BranchGroupID] [int] NOT NULL,
	[Address] [varchar](255) NULL,
	[SalAreaID] [int] NOT NULL,
	[ProvinceID] [int] NOT NULL,
	[AreaID] [int] NOT NULL,
	[DistrictID] [int] NOT NULL,
	[ShopTypeID] [int] NOT NULL,
	[PriceGroupID] [int] NOT NULL,
	[Phone] [varchar](20) NULL,
	[Mobile] [varchar](20) NULL,
	[CloseTime] [time](7) NOT NULL,
	[HQID] [int] NULL,
	[Remark] [varchar](255) NULL,
	[CanMinus] [bit] NULL,
	[SystemType] [char](1) NOT NULL,
	[PartID] [int] NOT NULL,
	[PartSeq] [char](2) NOT NULL,
	[TaxId] [char](15) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[SAPPlantID] [varchar](20) NOT NULL,
	[AgentID] [varchar](4) NOT NULL,
	[IPAddress] [varchar](50) NULL,
	[ConnStr] [varchar](200) NULL,
	[BranchRJCode] [varchar](20) NULL,
	[FactoryCode] [varchar](20) NULL,
	[FactoryLocation] [varchar](20) NULL,
	[VanCode] [varchar](20) NULL,
	[VanFormat] [varchar](20) NULL,
	[BranchReject] [varchar](20) NULL,
	[PremiumCode] [varchar](20) NULL,
	[PremiumFormat] [varchar](20) NULL,
	[BU] [int] NOT NULL,
	[DC] [int] NOT NULL,
	[BranchTax] [varchar](20) NULL,
	[BranchTitle] [varchar](50) NULL,
	[Prefix1] [varchar](20) NOT NULL,
	[Prefix2] [varchar](20) NOT NULL,
	[RunInit] [int] NULL,
	[RunLength] [smallint] NULL,
	[State] [varchar](50) NOT NULL,
	[Country] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_BranchGroup]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_BranchGroup](
	[BranchGroupID] [int] IDENTITY(1,1) NOT NULL,
	[BranchGroupCode] [char](2) NULL,
	[BranchGroupName] [nvarchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_BranchType] PRIMARY KEY CLUSTERED 
(
	[BranchGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_BranchLog]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_BranchLog](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[MachineID] [int] NULL,
	[MachineName] [varchar](50) NULL,
	[BranchID] [int] NOT NULL,
	[IPAddress] [varchar](50) NULL,
	[LogTime] [datetime] NOT NULL,
	[EmpID] [int] NULL,
	[Username] [varchar](50) NULL,
	[Event] [varchar](50) NULL,
	[EventType] [varchar](50) NULL,
	[IBDocNo] [varchar](50) NOT NULL,
	[INDocNo] [varchar](50) NOT NULL,
	[ShiftNo] [int] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_BranchLog] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_BranchRental]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_BranchRental](
	[BranchID] [varchar](20) NOT NULL,
	[RentYear] [varchar](50) NOT NULL,
	[Rental] [decimal](18, 2) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_BranchRental] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC,
	[RentYear] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_BranchSupervisor]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_BranchSupervisor](
	[EmpID] [varchar](20) NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_BranchSupervisor] PRIMARY KEY CLUSTERED 
(
	[EmpID] ASC,
	[BranchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_BranchTerritory]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_BranchTerritory](
	[TerritotyID] [int] IDENTITY(1,1) NOT NULL,
	[BranchID] [varchar](50) NOT NULL,
	[Locations] [xml] NOT NULL,
 CONSTRAINT [PK_BranchTerritory] PRIMARY KEY CLUSTERED 
(
	[TerritotyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_BranchWarehouse]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_BranchWarehouse](
	[WHID] [varchar](20) NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[WHCode] [varchar](10) NOT NULL,
	[WHName] [varchar](50) NOT NULL,
	[WHSeq] [varchar](10) NOT NULL,
	[License] [varchar](50) NOT NULL,
	[WHType] [int] NOT NULL,
	[SaleEmpID] [varchar](20) NULL,
	[DriverEmpID] [varchar](20) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[VanType] [int] NOT NULL,
	[HelperEmpID] [varchar](20) NOT NULL,
	[POSNo] [varchar](20) NOT NULL,
 CONSTRAINT [PK_BranchWarehouse_1] PRIMARY KEY CLUSTERED 
(
	[WHID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_BranchWarehouseData]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_BranchWarehouseData](
	[WHID] [varchar](20) NOT NULL,
	[SendOnline] [bit] NOT NULL,
	[ReceiveOnline] [bit] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_BranchWarehouseData] PRIMARY KEY CLUSTERED 
(
	[WHID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Cause]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Cause](
	[CauseID] [int] NOT NULL,
	[CauseCode] [varchar](10) NULL,
	[CauseName] [varchar](50) NULL,
	[CauseGroup] [varchar](20) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_Option] PRIMARY KEY CLUSTERED 
(
	[CauseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_CfgConnection]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_CfgConnection](
	[BranchID] [varchar](20) NOT NULL,
	[BranchName] [varchar](50) NULL,
	[ConnStr] [varchar](200) NULL,
	[ServerIP] [varchar](50) NULL,
	[Seq] [int] NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_CfgConnection] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_CfgFixedHolidays]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_CfgFixedHolidays](
	[month] [int] NOT NULL,
	[day] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[FlagSend] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_CfgFloatingHolidays]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_CfgFloatingHolidays](
	[month] [int] NOT NULL,
	[day_of_week] [varchar](9) NOT NULL,
	[nth] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[FlagSend] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_CfgKeyField]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_CfgKeyField](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [varchar](50) NULL,
	[DataName] [varchar](50) NULL,
	[FieldName] [varchar](50) NULL,
	[RefFieldName] [varchar](50) NULL,
	[IsPKey] [bit] NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_cfgKeyField] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_CfgPosMachine]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_CfgPosMachine](
	[MachineID] [int] IDENTITY(1,1) NOT NULL,
	[MachineName] [varchar](50) NULL,
	[HardwareKey] [varchar](50) NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[IPAddress] [varchar](50) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_cfgMachine] PRIMARY KEY CLUSTERED 
(
	[MachineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_CfgSetting]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_CfgSetting](
	[cfgName] [varchar](50) NOT NULL,
	[cfgValue] [varchar](200) NOT NULL,
	[cfgDesc] [varchar](100) NULL,
	[ModifiedDate] [date] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_CfgSetting] PRIMARY KEY CLUSTERED 
(
	[cfgName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Company]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Company](
	[CompanyID] [int] NOT NULL,
	[CompanyCode] [varchar](10) NOT NULL,
	[CompanyName] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[ZipCode] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Logo] [image] NULL,
	[BranchID] [varchar](20) NULL,
	[WHID] [varchar](20) NULL,
	[WHOnOrder] [varchar](20) NULL,
	[WHOnBackOrder] [varchar](20) NULL,
	[WHInTransit] [varchar](20) NULL,
	[WHOutTransit] [varchar](20) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[IsShow] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_CostPrice$]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CostPrice$](
	[ProductID] [float] NULL,
	[Unit] [int] NULL,
	[StdCost] [numeric](18, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_data$]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_data$](
	[OldNew] [float] NULL,
	[ID] [float] NULL,
	[ItemCode] [nvarchar](255) NULL,
	[ItemName] [nvarchar](255) NULL,
	[ShortName] [nvarchar](255) NULL,
	[Xena2] [nvarchar](255) NULL,
	[GroupID] [nvarchar](255) NULL,
	[SubGroupName] [nvarchar](255) NULL,
	[SubGroupID] [nvarchar](255) NULL,
	[Unit] [nvarchar](255) NULL,
	[ExVatCartonPrice] [float] NULL,
	[CartonPrice] [float] NULL,
	[ExVatCartonCost] [float] NULL,
	[PackConvert] [float] NULL,
	[UOMCode] [float] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_DelArCustomer]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DelArCustomer](
	[DepotNo] [varchar](10) NOT NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NOT NULL,
	[EdUser] [varchar](50) NOT NULL,
	[FlagDel] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Department]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Department](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentCode] [varchar](50) NULL,
	[DepartmentName] [varchar](50) NULL,
	[DepartmentDesc] [varchar](50) NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_Department_1] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_DisplayImage]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_DisplayImage](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
	[Image] [image] NULL,
	[Seq] [int] NULL,
	[CrUser] [nvarchar](50) NULL,
	[CrDate] [datetime] NULL,
	[EdUser] [nvarchar](50) NULL,
	[EdDate] [datetime] NULL,
	[FlagDel] [bit] NULL,
 CONSTRAINT [PK_DisplayImage] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_DocRunning]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DocRunning](
	[DocNum] [int] IDENTITY(1,1) NOT NULL,
	[DocTypeCode] [char](2) NOT NULL,
	[YearDoc] [char](2) NULL,
	[MonthDoc] [char](2) NULL,
	[DayDoc] [char](2) NULL,
	[RunDoc] [int] NULL,
	[ModifiledDate] [date] NULL,
	[WHCode] [char](3) NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_DocRunning_1] PRIMARY KEY CLUSTERED 
(
	[DocNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_DocSendUpdate]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DocSendUpdate](
	[DocNo] [varchar](50) NOT NULL,
	[DepotNo] [varchar](10) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NOT NULL,
	[EdUser] [varchar](50) NOT NULL,
	[FlagDel] [bit] NOT NULL,
 CONSTRAINT [PK_DocSendUpdate] PRIMARY KEY CLUSTERED 
(
	[DocNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_DocSignature]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DocSignature](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[DocNo] [nvarchar](50) NULL,
	[DocTypeCode] [varchar](2) NULL,
	[DocDate] [date] NULL,
	[WHID] [varchar](20) NULL,
	[ImgSign] [image] NULL,
	[Remark] [nvarchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
 CONSTRAINT [PK_DocSignature_1] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_DocumentStatus]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DocumentStatus](
	[DocStatusCode] [varchar](50) NOT NULL,
	[DocStatusName] [varchar](50) NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_DocumentStatus] PRIMARY KEY CLUSTERED 
(
	[DocStatusCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_DocumentType]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DocumentType](
	[DocTypeCode] [varchar](2) NOT NULL,
	[DocTypeName] [varchar](50) NULL,
	[DocFormat] [varchar](50) NULL,
	[RunLength] [smallint] NULL,
	[DocHeader] [varchar](50) NULL,
	[DocRemark] [varchar](255) NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_DocumentType] PRIMARY KEY CLUSTERED 
(
	[DocTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Employee]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Employee](
	[EmpID] [varchar](20) NOT NULL,
	[EmpCode] [varchar](10) NULL,
	[TitleName] [varchar](10) NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[NickName] [varchar](20) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[PositionID] [int] NOT NULL,
	[MgrID] [varchar](20) NULL,
	[RoleID] [int] NOT NULL,
	[Mobile] [varchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[IDCard] [varchar](17) NOT NULL,
	[EmpIDCard] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_error_logs]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_error_logs](
	[pk] [int] IDENTITY(1,1) NOT NULL,
	[user_code] [nvarchar](10) NULL,
	[form_name] [nvarchar](150) NULL,
	[function_name] [nvarchar](150) NULL,
	[err_desc] [nvarchar](max) NULL,
	[time_log] [datetime] NULL,
 CONSTRAINT [PK__error_lo__321403CF4A08EB67] PRIMARY KEY CLUSTERED 
(
	[pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_ErrorLog]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ErrorLog](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[ErrorNumber] [int] NULL,
	[ErrorSeverity] [int] NULL,
	[ErrorState] [int] NULL,
	[ErrorProcedure] [varchar](100) NULL,
	[ErrorLine] [int] NULL,
	[ErrorMessage] [varchar](500) NULL,
	[CrDate] [datetime] NULL,
	[InputData] [varchar](max) NULL,
	[WHID] [varchar](50) NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Expense]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Expense](
	[ExpenseID] [int] IDENTITY(1,1) NOT NULL,
	[ExpenseCode] [varchar](50) NOT NULL,
	[ExpenseName] [varchar](50) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_SaleExpense] PRIMARY KEY CLUSTERED 
(
	[ExpenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_InvMovement]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_InvMovement](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[ProductName] [varchar](200) NULL,
	[RefDocNo] [varchar](20) NOT NULL,
	[TrnDate] [datetime] NOT NULL,
	[TrnType] [char](1) NOT NULL,
	[DocTypeCode] [char](2) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[FromWHID] [varchar](20) NOT NULL,
	[ToWHID] [varchar](20) NOT NULL,
	[TrnQtyIn] [decimal](18, 2) NOT NULL,
	[TrnQtyOut] [decimal](18, 2) NOT NULL,
	[TrnQty] [decimal](18, 2) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[EdDate] [datetime] NULL,
	[ProductGroupCode] [varchar](20) NOT NULL,
	[ProductGroupName] [varchar](50) NOT NULL,
	[ProductSubGroupCode] [varchar](20) NOT NULL,
	[ProductSubGroupName] [varchar](50) NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_InvMovement_New_TransactionID] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_InvTransaction]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_InvTransaction](
	[TransactionID] [int] IDENTITY(100000,1) NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[RefDocNo] [varchar](20) NOT NULL,
	[RefLineID] [int] NOT NULL,
	[TrnDate] [datetime] NOT NULL,
	[BranchFrom] [varchar](20) NULL,
	[WHFrom] [varchar](20) NULL,
	[BranchTo] [varchar](20) NULL,
	[WHTo] [varchar](20) NULL,
	[TrnType] [char](1) NOT NULL,
	[DocTypeCode] [char](2) NOT NULL,
	[TrnUomID] [int] NULL,
	[TrnUom] [varchar](50) NULL,
	[BringQty] [decimal](8, 2) NULL,
	[TrnQtyIn] [decimal](8, 2) NOT NULL,
	[TrnQtyOut] [decimal](8, 2) NOT NULL,
	[TrnQty] [decimal](8, 2) NOT NULL,
	[RemainQty] [decimal](8, 2) NULL,
	[UnitPrice] [decimal](15, 5) NULL,
	[UnitCost] [decimal](15, 5) NOT NULL,
	[LineDiscountType] [char](1) NULL,
	[LineDiscount] [decimal](15, 5) NULL,
	[TrnVat] [decimal](15, 5) NULL,
	[TrnValue] [decimal](15, 5) NOT NULL,
	[TrnTotal] [decimal](15, 5) NOT NULL,
	[CostValue] [decimal](15, 5) NOT NULL,
	[Supplier] [int] NULL,
	[Customer] [varchar](20) NULL,
	[RefSONo] [char](6) NULL,
	[CustPONo] [varchar](50) NULL,
	[CustInvoiceNo] [varchar](50) NULL,
	[Salesperson] [int] NULL,
	[SalAreaID] [varchar](20) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_TransactionHistory_TransactionID] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_InvWarehouse]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_InvWarehouse](
	[ProductID] [varchar](20) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[QtyOnHand] [decimal](10, 3) NOT NULL,
	[QtyOnOrder] [decimal](10, 3) NOT NULL,
	[QtyOnBackOrder] [decimal](10, 3) NOT NULL,
	[QtyInTransit] [decimal](10, 3) NOT NULL,
	[QtyOutTransit] [decimal](10, 3) NOT NULL,
	[QtyOnReject] [decimal](10, 3) NOT NULL,
	[MinimumQty] [decimal](10, 3) NOT NULL,
	[MaximumQty] [decimal](10, 3) NOT NULL,
	[ReOrderQty] [decimal](10, 3) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PrdWarehouse] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[WHID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_IVDetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_IVDetail](
	[DocNo] [varchar](50) NOT NULL,
	[AutoID] [bigint] IDENTITY(1,1) NOT NULL,
	[Line] [smallint] NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[ProductName] [varchar](100) NULL,
	[OrderUom] [int] NOT NULL,
	[OrderQty] [decimal](10, 3) NULL,
	[ReceivedQty] [decimal](8, 2) NULL,
	[RejectedQty] [decimal](8, 2) NULL,
	[StockedQty] [decimal](8, 2) NOT NULL,
	[UnitCost] [decimal](15, 5) NULL,
	[UnitPrice] [decimal](15, 5) NULL,
	[VatType] [tinyint] NULL,
	[LineDiscountType] [char](1) NOT NULL,
	[LineDiscount] [decimal](14, 2) NOT NULL,
	[LineTotal] [decimal](15, 5) NULL,
	[CustType] [varchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[UnitComPrice] [decimal](15, 5) NOT NULL,
	[LineComTotal] [decimal](15, 5) NOT NULL,
 CONSTRAINT [PK_IVDetail] PRIMARY KEY CLUSTERED 
(
	[DocNo] ASC,
	[AutoID] ASC,
	[ProductID] ASC,
	[OrderUom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_IVMaster]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_IVMaster](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[DocNo] [varchar](50) NOT NULL,
	[RevisionNo] [tinyint] NOT NULL,
	[DocTypeCode] [char](2) NOT NULL,
	[DocStatus] [char](1) NOT NULL,
	[DocDate] [datetime] NOT NULL,
	[DocRef] [varchar](50) NULL,
	[StatusInOut] [char](1) NULL,
	[SupplierID] [varchar](20) NOT NULL,
	[SuppName] [varchar](50) NULL,
	[WHID] [varchar](20) NOT NULL,
	[EmpID] [varchar](20) NOT NULL,
	[SaleEmpID] [varchar](20) NOT NULL,
	[SalAreaID] [varchar](20) NOT NULL,
	[Address] [varchar](200) NULL,
	[ContactName] [varchar](200) NULL,
	[ContactTel] [varchar](200) NULL,
	[Shipto] [varchar](255) NULL,
	[CreditDay] [smallint] NULL,
	[DueDate] [datetime] NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[CustType] [varchar](50) NULL,
	[CustName] [varchar](200) NULL,
	[CustInvNO] [varchar](50) NULL,
	[CustInvDate] [datetime] NULL,
	[CustPODate] [datetime] NULL,
	[CustPONo] [varchar](50) NOT NULL,
	[Remark] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[OldAmount] [decimal](15, 5) NULL,
	[Amount] [decimal](15, 5) NULL,
	[OldExcVat] [decimal](15, 5) NULL,
	[ExcVat] [decimal](15, 5) NULL,
	[OldIncVat] [decimal](15, 5) NULL,
	[IncVat] [decimal](15, 5) NULL,
	[VatRate] [decimal](15, 5) NULL,
	[VatAmt] [decimal](15, 5) NULL,
	[Freight] [decimal](15, 5) NOT NULL,
	[DiscountType] [char](1) NOT NULL,
	[OldDiscount] [decimal](14, 2) NULL,
	[Discount] [decimal](14, 2) NULL,
	[TotalDue] [decimal](15, 5) NOT NULL,
	[ApprovedBy] [varchar](20) NULL,
	[ApprovedDate] [datetime] NULL,
	[PayType] [tinyint] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[OldTotalCom] [decimal](15, 5) NOT NULL,
	[TotalCom] [decimal](15, 5) NOT NULL,
	[CNType] [smallint] NULL,
	[FromWHCode] [varchar](20) NULL,
	[FromLocCode] [varchar](20) NULL,
	[ToWHCode] [varchar](20) NULL,
	[ToLocCode] [varchar](20) NULL,
 CONSTRAINT [PK_IVMaster] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC,
	[DocNo] ASC,
	[DocTypeCode] ASC,
	[DocStatus] ASC,
	[DocDate] ASC,
	[WHID] ASC,
	[EmpID] ASC,
	[SalAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LtyQuestion]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LtyQuestion](
	[QuestionID] [int] NOT NULL,
	[QuestionCode] [varchar](20) NULL,
	[Question] [varchar](200) NULL,
	[QuestionOption] [smallint] NULL,
	[FlagSend] [bit] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[Remark] [varchar](200) NULL,
 CONSTRAINT [PK_LtyQuestion] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytAnswer]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytAnswer](
	[AnswerID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[ChoiceID] [int] NOT NULL,
	[ChoiceRemark] [varchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_LytAnswer] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytChoice]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytChoice](
	[ChoiceID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[ChoiceSeq] [smallint] NULL,
	[Choice] [varchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
 CONSTRAINT [PK_LytChoice_1] PRIMARY KEY CLUSTERED 
(
	[ChoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytCustTypeReward]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytCustTypeReward](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[CYear] [int] NULL,
	[CycleNo] [int] NULL,
	[CustTypeID] [varchar](20) NULL,
	[MinVal] [decimal](10, 2) NULL,
	[MaxVal] [decimal](10, 2) NULL,
	[GetProductID] [varchar](20) NULL,
	[GetQty] [smallint] NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_CustTypeReward] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytMember]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytMember](
	[MemberID] [int] IDENTITY(1,1) NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[MemberCode] [varchar](20) NOT NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[DateOfBirth] [smalldatetime] NOT NULL,
	[NetPoint] [int] NOT NULL,
	[ApplyDate] [datetime] NOT NULL,
	[Remark] [varchar](200) NULL,
	[IDCode] [char](13) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_LytMember] PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytPointBonus]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytPointBonus](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[TrnNo] [varchar](20) NOT NULL,
	[PointTypeID] [int] NULL,
	[ProductID] [varchar](20) NOT NULL,
	[BaseQty] [int] NOT NULL,
	[RefDocNo] [varchar](20) NOT NULL,
	[DocStatus] [int] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[PayPoint] [int] NULL,
 CONSTRAINT [PK_LytPointBonus_1] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytPointByProduct]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytPointByProduct](
	[AutoNo] [int] IDENTITY(1,1) NOT NULL,
	[KeyID] [int] NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[CYear] [int] NULL,
	[CycleNo] [int] NULL,
	[PointTypeID] [int] NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[Operator] [varchar](2) NOT NULL,
	[BaseQty] [int] NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_LytProductPoint] PRIMARY KEY CLUSTERED 
(
	[AutoNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytPointCycle]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytPointCycle](
	[TrnNo] [varchar](20) NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[CYear] [int] NULL,
	[CycleNo] [int] NULL,
	[CustomerID] [varchar](20) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_LytPointCycle] PRIMARY KEY CLUSTERED 
(
	[TrnNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytPointMovement]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytPointMovement](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[TrnNo] [varchar](20) NOT NULL,
	[TrnDate] [datetime] NOT NULL,
	[PointTypeID] [int] NULL,
	[TrnType] [char](1) NOT NULL,
	[TrnPoint] [int] NULL,
	[RefDocNo] [varchar](20) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_LytPointMovement] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytPointType]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytPointType](
	[PointTypeID] [int] IDENTITY(1,1) NOT NULL,
	[PointTypeCode] [varchar](20) NOT NULL,
	[PointTypeName] [varchar](50) NOT NULL,
	[Seq] [smallint] NOT NULL,
	[Value] [int] NOT NULL,
	[UomName] [varchar](50) NULL,
	[MoneyValue] [int] NOT NULL,
	[StartDate] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
	[PrdQtyMin] [tinyint] NOT NULL,
	[PrdQtyMax] [tinyint] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_LytPointType] PRIMARY KEY CLUSTERED 
(
	[PointTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytPointTypePerBill]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytPointTypePerBill](
	[AutoNo] [int] IDENTITY(1,1) NOT NULL,
	[KeyID] [int] NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[CYear] [int] NULL,
	[CycleNo] [int] NULL,
	[PointTypeID] [int] NOT NULL,
	[BuyValue] [int] NOT NULL,
	[ConditionID] [int] NOT NULL,
	[GetPoint] [int] NOT NULL,
	[IsCondition] [bit] NOT NULL,
	[TypeSum] [varchar](10) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_LytPointPerBill] PRIMARY KEY CLUSTERED 
(
	[AutoNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytRedeem]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytRedeem](
	[DocNo] [varchar](20) NOT NULL,
	[Line] [smallint] NOT NULL,
	[DocDate] [datetime] NULL,
	[BranchID] [varchar](20) NULL,
	[WHID] [varchar](20) NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[BaseQty] [int] NOT NULL,
	[PayPoint] [int] NULL,
	[DocStatus] [int] NOT NULL,
	[RefDO] [varchar](20) NULL,
	[DODate] [datetime] NULL,
	[RefCM] [varchar](20) NULL,
	[CMDate] [datetime] NULL,
	[RefCMVan] [varchar](20) NULL,
	[CMWHID] [varchar](20) NULL,
	[ReceiveQty] [int] NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_LytRedeem_1] PRIMARY KEY CLUSTERED 
(
	[DocNo] ASC,
	[Line] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytReportGroup]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytReportGroup](
	[GroupID] [int] NOT NULL,
	[GroupName] [varchar](255) NULL,
	[Status] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytReportMenu]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytReportMenu](
	[ReportID] [int] NOT NULL,
	[GroupID] [int] NOT NULL,
	[ReportNameEng] [varchar](255) NULL,
	[ReportNameTh] [varchar](255) NULL,
	[FileName] [varchar](50) NULL,
	[Status] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LytReward]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LytReward](
	[RewardID] [int] IDENTITY(1,1) NOT NULL,
	[RewardCode] [varchar](20) NULL,
	[RewardName] [varchar](200) NULL,
	[Seq] [smallint] NULL,
	[ProductID] [varchar](20) NOT NULL,
	[UsePoint] [int] NULL,
	[GetQty] [int] NULL,
	[StartDate] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
	[ProductImg] [image] NULL,
	[BuyValue] [int] NOT NULL,
	[Remark] [varchar](200) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_LytReward] PRIMARY KEY CLUSTERED 
(
	[RewardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_MstArea]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_MstArea](
	[AreaID] [int] IDENTITY(1,1) NOT NULL,
	[AreaCode] [char](6) NOT NULL,
	[AreaName] [varchar](50) NULL,
	[ProvinceID] [int] NULL,
	[PostalCode] [varchar](50) NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_MstArea] PRIMARY KEY CLUSTERED 
(
	[AreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_MstCycle]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_MstCycle](
	[CYear] [nchar](4) NOT NULL,
	[CNo] [smallint] NOT NULL,
	[CName] [nchar](10) NULL,
	[CStartDate] [date] NULL,
	[CEndDate] [date] NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_MstCycle] PRIMARY KEY CLUSTERED 
(
	[CYear] ASC,
	[CNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_MstDistrict]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_MstDistrict](
	[DistrictID] [int] IDENTITY(1,1) NOT NULL,
	[DistrictCode] [char](6) NOT NULL,
	[DistrictName] [varchar](50) NOT NULL,
	[AreaID] [int] NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[CountDis] [int] NOT NULL,
 CONSTRAINT [PK_MstDistrict_1] PRIMARY KEY CLUSTERED 
(
	[DistrictID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_MstGrade]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_MstGrade](
	[BranchID] [nvarchar](50) NOT NULL,
	[Year] [varchar](4) NOT NULL,
	[CycleNo] [varchar](2) NOT NULL,
	[Grade] [nvarchar](10) NOT NULL,
	[MaxValue] [decimal](18, 2) NOT NULL,
	[MinValue] [decimal](18, 2) NOT NULL,
	[ValueType] [nvarchar](10) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_MstMenu]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_MstMenu](
	[MenuID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[MenuName] [nvarchar](50) NULL,
	[MenuText] [nvarchar](200) NULL,
	[FormName] [nvarchar](100) NULL,
	[MenuParent] [int] NULL,
	[MenuImage] [image] NULL,
	[Seq] [int] NULL,
 CONSTRAINT [PK_tbl_MstMenu] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_MstPart]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_MstPart](
	[PartID] [int] NOT NULL,
	[PartCode] [varchar](10) NOT NULL,
	[PartName] [varchar](50) NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_MstPart] PRIMARY KEY CLUSTERED 
(
	[PartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_MstProvince]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_MstProvince](
	[ProvinceID] [int] IDENTITY(1,1) NOT NULL,
	[ProvinceCode] [char](6) NOT NULL,
	[ProvinceName] [varchar](50) NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_MstProvince_1] PRIMARY KEY CLUSTERED 
(
	[ProvinceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PaidDetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PaidDetail](
	[PaidID] [int] NOT NULL,
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[BranchID] [int] NOT NULL,
	[ShiftNo] [smallint] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[TotalSale] [decimal](15, 2) NOT NULL,
	[TotalExpense] [decimal](15, 2) NOT NULL,
	[NetTotal] [decimal](15, 2) NOT NULL,
	[IBDocNo] [varchar](50) NOT NULL,
	[INDocNo] [varchar](50) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PaidDetail] PRIMARY KEY CLUSTERED 
(
	[PaidID] ASC,
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PaidMaster]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PaidMaster](
	[PaidID] [int] IDENTITY(1,1) NOT NULL,
	[SendNo] [varchar](50) NOT NULL,
	[SendDate] [datetime] NOT NULL,
	[SendTotal] [decimal](15, 3) NOT NULL,
	[ShipID] [int] NOT NULL,
	[EmpID] [int] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PaidMaster] PRIMARY KEY CLUSTERED 
(
	[PaidID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PayDetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PayDetail](
	[DocNo] [varchar](50) NOT NULL,
	[AutoID] [int] NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[Send] [decimal](8, 2) NOT NULL,
	[Deposit] [decimal](8, 2) NOT NULL,
	[TotalSale] [decimal](8, 2) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[Transfer] [decimal](8, 2) NOT NULL,
	[Cheque] [decimal](8, 2) NOT NULL,
 CONSTRAINT [PK_PayDetail] PRIMARY KEY CLUSTERED 
(
	[DocNo] ASC,
	[AutoID] ASC,
	[WHID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PayMaster]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PayMaster](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[DocNo] [varchar](50) NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[Docdate] [date] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[TotalSend] [decimal](8, 2) NOT NULL,
 CONSTRAINT [PK_PayMaster_1] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC,
	[DocNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PODetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PODetail](
	[DocNo] [varchar](50) NOT NULL,
	[AutoID] [bigint] IDENTITY(1,1) NOT NULL,
	[Line] [smallint] NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[ProductName] [varchar](100) NULL,
	[OrderUom] [int] NOT NULL,
	[OrderQty] [decimal](10, 3) NULL,
	[ReceivedQty] [decimal](8, 2) NULL,
	[RejectedQty] [decimal](8, 2) NULL,
	[StockedQty] [decimal](8, 2) NOT NULL,
	[UnitCost] [decimal](15, 5) NULL,
	[UnitPrice] [decimal](15, 5) NULL,
	[VatType] [tinyint] NULL,
	[LineDiscountType] [char](1) NOT NULL,
	[LineDiscountRate] [decimal](18, 2) NOT NULL,
	[LineDiscount] [decimal](14, 2) NOT NULL,
	[LineTotal] [decimal](15, 5) NULL,
	[CustType] [varchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[UnitComPrice] [decimal](15, 5) NOT NULL,
	[LineComTotal] [decimal](15, 5) NOT NULL,
	[LineRemark] [varchar](100) NOT NULL,
	[FreeQty] [int] NOT NULL,
	[FreeUom] [tinyint] NOT NULL,
	[FreeUnit] [int] NOT NULL,
 CONSTRAINT [PK_tmpPODetail] PRIMARY KEY CLUSTERED 
(
	[DocNo] ASC,
	[AutoID] ASC,
	[ProductID] ASC,
	[OrderUom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_POMaster]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_POMaster](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[DocNo] [varchar](50) NOT NULL,
	[RevisionNo] [tinyint] NOT NULL,
	[DocTypeCode] [char](2) NOT NULL,
	[DocStatus] [char](1) NOT NULL,
	[DocDate] [datetime] NOT NULL,
	[DocRef] [varchar](50) NULL,
	[StatusInOut] [char](1) NULL,
	[SupplierID] [varchar](20) NOT NULL,
	[SuppName] [varchar](50) NULL,
	[WHID] [varchar](20) NOT NULL,
	[EmpID] [varchar](20) NOT NULL,
	[SaleEmpID] [varchar](20) NOT NULL,
	[SalAreaID] [varchar](20) NOT NULL,
	[Address] [varchar](200) NULL,
	[ContactName] [varchar](200) NULL,
	[ContactTel] [varchar](50) NULL,
	[Shipto] [varchar](255) NULL,
	[CreditDay] [smallint] NULL,
	[DueDate] [datetime] NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[CustType] [varchar](50) NULL,
	[CustName] [varchar](200) NULL,
	[CustInvNO] [varchar](50) NULL,
	[CustInvDate] [datetime] NULL,
	[CustPODate] [datetime] NULL,
	[CustPONo] [varchar](50) NOT NULL,
	[Remark] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[OldAmount] [decimal](15, 5) NULL,
	[Amount] [decimal](15, 5) NULL,
	[OldExcVat] [decimal](15, 5) NULL,
	[ExcVat] [decimal](15, 5) NULL,
	[OldIncVat] [decimal](15, 5) NULL,
	[IncVat] [decimal](15, 5) NULL,
	[VatRate] [decimal](15, 5) NULL,
	[VatAmt] [decimal](15, 5) NULL,
	[Freight] [decimal](15, 5) NOT NULL,
	[DiscountType] [char](1) NOT NULL,
	[OldDiscount] [decimal](14, 2) NULL,
	[Discount] [decimal](14, 2) NULL,
	[TotalDue] [decimal](15, 5) NOT NULL,
	[ApprovedBy] [varchar](20) NULL,
	[ApprovedDate] [datetime] NULL,
	[PayType] [tinyint] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[OldTotalCom] [decimal](15, 5) NOT NULL,
	[TotalCom] [decimal](15, 5) NOT NULL,
	[CNType] [smallint] NULL,
	[DiscountRate] [decimal](14, 2) NOT NULL,
 CONSTRAINT [PK_tmpPOMaster] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC,
	[DocNo] ASC,
	[DocTypeCode] ASC,
	[DocStatus] ASC,
	[DocDate] ASC,
	[WHID] ASC,
	[EmpID] ASC,
	[SalAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Position]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Position](
	[PositionID] [int] IDENTITY(1,1) NOT NULL,
	[PositionCode] [varchar](50) NULL,
	[PositionName] [varchar](50) NULL,
	[PositionDesc] [varchar](255) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_Position_1] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PRDetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PRDetail](
	[DocNo] [varchar](50) NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[Line] [smallint] NOT NULL,
	[ProductName] [varchar](200) NULL,
	[OrderUom] [int] NOT NULL,
	[OrderQty] [decimal](10, 3) NULL,
	[SendQty] [decimal](8, 2) NULL,
	[ReceivedQty] [decimal](8, 2) NULL,
	[RejectedQty] [decimal](8, 2) NULL,
	[StockedQty] [decimal](8, 2) NOT NULL,
	[UnitCost] [decimal](15, 5) NULL,
	[UnitPrice] [decimal](15, 5) NULL,
	[VatType] [tinyint] NULL,
	[LineTotal] [decimal](15, 5) NULL,
	[LineRemark] [varchar](100) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PRDetail] PRIMARY KEY CLUSTERED 
(
	[DocNo] ASC,
	[ProductID] ASC,
	[OrderUom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PreSaleWarehouse]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PreSaleWarehouse](
	[WHID] [varchar](20) NOT NULL,
	[EmpID] [varchar](20) NOT NULL,
	[Seq] [smallint] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PreSaleWarehouse] PRIMARY KEY CLUSTERED 
(
	[WHID] ASC,
	[EmpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PriceGroup]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PriceGroup](
	[PriceGroupID] [int] IDENTITY(1,1) NOT NULL,
	[PriceGroupCode] [char](2) NULL,
	[PriceGroupName] [varchar](50) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[BranchID] [varchar](20) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_PriceGroup] PRIMARY KEY CLUSTERED 
(
	[PriceGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PRMaster]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PRMaster](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[DocNo] [varchar](50) NOT NULL,
	[RevisionNo] [tinyint] NOT NULL,
	[DocTypeCode] [char](2) NOT NULL,
	[DocStatus] [char](1) NOT NULL,
	[DocDate] [datetime] NOT NULL,
	[DocRef] [varchar](50) NOT NULL,
	[FromBranchID] [varchar](20) NOT NULL,
	[FromWHID] [varchar](20) NOT NULL,
	[RequestBy] [varchar](20) NULL,
	[ToBranchID] [varchar](20) NULL,
	[ToWHID] [varchar](20) NULL,
	[StatusInOut] [char](1) NULL,
	[Address] [varchar](200) NULL,
	[ReceiveDate] [datetime] NULL,
	[ReceiveBy] [varchar](20) NULL,
	[ShipDate] [datetime] NULL,
	[ShipBy] [varchar](20) NULL,
	[ShipWHID] [varchar](20) NULL,
	[SalAreaID] [varchar](20) NOT NULL,
	[EmpID] [varchar](20) NOT NULL,
	[ContactName] [varchar](50) NULL,
	[ContactTel] [varchar](50) NULL,
	[Shipto] [varchar](255) NULL,
	[Remark] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[ApprovedBy] [varchar](20) NULL,
	[ApprovedDate] [datetime] NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PRMaster] PRIMARY KEY CLUSTERED 
(
	[DocNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Product]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Product](
	[ProductID] [varchar](20) NOT NULL,
	[Seq] [smallint] NOT NULL,
	[ProductCode] [varchar](20) NOT NULL,
	[ProductRefCode] [varchar](20) NOT NULL,
	[Barcode] [varchar](50) NULL,
	[ProductName] [varchar](100) NOT NULL,
	[ProductShortName] [varchar](50) NULL,
	[ProductGroupID] [int] NOT NULL,
	[ProductSubGroupID] [int] NOT NULL,
	[Flavour] [varchar](50) NOT NULL,
	[Size] [decimal](8, 2) NOT NULL,
	[SizeUOM] [varchar](50) NOT NULL,
	[Weight] [decimal](8, 2) NOT NULL,
	[WeightUOM] [varchar](50) NOT NULL,
	[ReorderPoint] [smallint] NOT NULL,
	[MinPoint] [smallint] NOT NULL,
	[PurchaseUomID] [int] NOT NULL,
	[SaleUomID] [int] NOT NULL,
	[VatType] [bit] NOT NULL,
	[Remark] [varchar](255) NULL,
	[StandardCost] [decimal](5, 2) NULL,
	[SellPrice] [decimal](5, 2) NULL,
	[IsFulfill] [bit] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[ProductImg] [image] NULL,
	[FlagSend] [bit] NOT NULL,
	[FlagNew] [bit] NOT NULL,
	[FlagEdit] [bit] NOT NULL,
	[ProductBrandID] [int] NOT NULL,
	[ProductLine] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductBrand]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductBrand](
	[ProductBrandID] [int] IDENTITY(1,1) NOT NULL,
	[ProductBrandCode] [varchar](50) NOT NULL,
	[ProductBrandName] [varchar](50) NOT NULL,
	[ProductBrandSeq] [int] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PrdBrand_1] PRIMARY KEY CLUSTERED 
(
	[ProductBrandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductChanged]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductChanged](
	[ProductID] [varchar](20) NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[Seq] [smallint] NOT NULL,
	[ProductCode] [varchar](20) NOT NULL,
	[ProductRefCode] [varchar](20) NOT NULL,
	[Barcode] [varchar](50) NULL,
	[ProductName] [varchar](100) NOT NULL,
	[ProductShortName] [varchar](50) NULL,
	[ProductGroupID] [int] NOT NULL,
	[ProductSubGroupID] [int] NOT NULL,
	[Flavour] [varchar](50) NOT NULL,
	[Size] [decimal](8, 2) NOT NULL,
	[SizeUOM] [varchar](50) NOT NULL,
	[Weight] [decimal](8, 2) NOT NULL,
	[WeightUOM] [varchar](50) NOT NULL,
	[ReorderPoint] [smallint] NOT NULL,
	[MinPoint] [smallint] NOT NULL,
	[PurchaseUomID] [int] NOT NULL,
	[SaleUomID] [int] NOT NULL,
	[VatType] [bit] NOT NULL,
	[Remark] [varchar](255) NULL,
	[StandardCost] [decimal](5, 2) NULL,
	[SellPrice] [decimal](5, 2) NULL,
	[IsFulfill] [bit] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[ProductImg] [image] NULL,
	[FlagSend] [bit] NOT NULL,
	[FlagNew] [bit] NOT NULL,
	[FlagEdit] [bit] NOT NULL,
	[StartDate] [datetime] NULL,
	[ProductGroup] [xml] NULL,
	[ProductSubGroup] [xml] NULL,
	[ProductUom] [xml] NULL,
	[ProductUomSet] [xml] NULL,
	[ProductPriceGroup] [xml] NULL,
 CONSTRAINT [PK_ProductChanged] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[BranchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductCompany]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_ProductCompany](
	[ProductCompanyID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCompanyName] [nvarchar](50) NULL,
	[ProductCompanySeq] [int] NULL,
	[FlagDel] [bit] NULL,
	[CrUser] [nvarchar](50) NULL,
	[CrDate] [datetime] NULL,
	[EdUser] [nvarchar](50) NULL,
	[EdDate] [datetime] NULL,
 CONSTRAINT [PK_ProductCompany] PRIMARY KEY CLUSTERED 
(
	[ProductCompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_ProductCostHistory]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductCostHistory](
	[ProductID] [varchar](20) NOT NULL,
	[DateChanged] [datetime] NOT NULL,
	[NewCost] [decimal](15, 5) NOT NULL,
	[OldCost] [decimal](15, 5) NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PrdCostHistory] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductCostSupplier]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductCostSupplier](
	[SupplierID] [varchar](20) NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[UnitPrice] [decimal](15, 5) NOT NULL,
	[OldUnitPrice] [decimal](15, 5) NOT NULL,
	[DateChanged] [datetime] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PrdSupplierPrice] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductFlavour]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductFlavour](
	[ProductFlavourID] [int] IDENTITY(1,1) NOT NULL,
	[ProductFlavourCode] [varchar](50) NOT NULL,
	[ProductFlavourName] [varchar](50) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PrdFlavour_1] PRIMARY KEY CLUSTERED 
(
	[ProductFlavourID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductGroup]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductGroup](
	[ProductGroupID] [int] IDENTITY(1,1) NOT NULL,
	[ProductGroupCode] [varchar](10) NOT NULL,
	[ProductGroupName] [varchar](50) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[ProductGroupImg] [image] NULL,
	[FlagSend] [bit] NOT NULL,
	[BranchID] [varchar](20) NULL,
	[ProductTypeID] [int] NULL,
 CONSTRAINT [PK_PrdGroup] PRIMARY KEY CLUSTERED 
(
	[ProductGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductPriceBranch]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductPriceBranch](
	[BranchID] [varchar](20) NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[ProductUomID] [int] NOT NULL,
	[SellPrice] [decimal](15, 2) NULL,
	[BuyPrice] [decimal](15, 2) NULL,
	[SellPriceVat] [decimal](15, 2) NULL,
	[BuyPriceVat] [decimal](15, 2) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PrdPriceBranch] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC,
	[ProductID] ASC,
	[ProductUomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductPriceCustomer]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductPriceCustomer](
	[CustomerID] [varchar](20) NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[UnitPrice] [decimal](15, 5) NOT NULL,
	[OldUnitPrice] [decimal](15, 5) NOT NULL,
	[DateChanged] [datetime] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_ProductPriceCustomer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductPriceGroup]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductPriceGroup](
	[PriceGroupID] [int] NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[ProductUomID] [int] NOT NULL,
	[SellPrice] [decimal](15, 2) NULL,
	[BuyPrice] [decimal](15, 2) NULL,
	[SellPriceVat] [decimal](15, 2) NULL,
	[BuyPriceVat] [decimal](15, 2) NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[FlagNew] [bit] NOT NULL,
	[FlagEdit] [bit] NOT NULL,
	[ComPrice] [decimal](15, 2) NULL,
 CONSTRAINT [PK_ProductPriceGroup] PRIMARY KEY CLUSTERED 
(
	[PriceGroupID] ASC,
	[ProductID] ASC,
	[ProductUomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductPriceHistory]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductPriceHistory](
	[ProductID] [varchar](20) NOT NULL,
	[DateChanged] [datetime] NOT NULL,
	[TimeChanged] [decimal](8, 0) NOT NULL,
	[UnitPrice] [decimal](15, 5) NOT NULL,
	[NewSellingPrice] [decimal](15, 5) NULL,
	[OldSellingPrice] [decimal](15, 5) NULL,
	[FlagSend] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductPromotionBom]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductPromotionBom](
	[ProductID] [int] NOT NULL,
	[CompID] [int] NOT NULL,
	[CompUom] [int] NOT NULL,
	[CompQty] [int] NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_ProductPromotionBom] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[CompID] ASC,
	[CompUom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductRemarkReject]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductRemarkReject](
	[RemarkRejectID] [int] IDENTITY(1,1) NOT NULL,
	[RemarkRejectName] [varchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_ProductRemarkReject] PRIMARY KEY CLUSTERED 
(
	[RemarkRejectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductSubGroup]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductSubGroup](
	[ProductSubGroupID] [int] IDENTITY(1,1) NOT NULL,
	[ProductGroupID] [int] NOT NULL,
	[ProductSubGroupCode] [varchar](10) NOT NULL,
	[ProductSubGroupName] [varchar](50) NOT NULL,
	[IsFulfill] [bit] NOT NULL,
	[ProductSubGroupImg] [image] NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PrdSubGroup_1] PRIMARY KEY CLUSTERED 
(
	[ProductSubGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductType]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductType](
	[ProductTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ProductTypeName] [nvarchar](50) NULL,
	[ProductTypeSeq] [int] NULL,
	[FlagDel] [bit] NULL,
	[CrUser] [nvarchar](50) NULL,
	[CrDate] [datetime] NULL,
	[EdUser] [nvarchar](50) NULL,
	[EdDate] [datetime] NULL,
	[ProductCompanyID] [int] NOT NULL,
	[RatioType] [char](1) NOT NULL,
 CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[ProductTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductUom]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductUom](
	[ProductUomID] [int] IDENTITY(1,1) NOT NULL,
	[ProductUomCode] [varchar](50) NULL,
	[ProductUomName] [varchar](50) NOT NULL,
	[ProductUomNameTH] [varchar](50) NOT NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PrdUom] PRIMARY KEY CLUSTERED 
(
	[ProductUomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductUomSet]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ProductUomSet](
	[ProductID] [varchar](20) NOT NULL,
	[UomSetID] [int] NOT NULL,
	[UomSetName] [varchar](50) NOT NULL,
	[BaseUomID] [int] NOT NULL,
	[BaseUomName] [varchar](50) NOT NULL,
	[BaseQty] [int] NOT NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[FlagNew] [bit] NOT NULL,
	[FlagEdit] [bit] NOT NULL,
	[Weight] [decimal](15, 4) NULL,
	[StandardCost] [decimal](15, 4) NULL,
	[UomCode] [varchar](20) NOT NULL,
 CONSTRAINT [PK_PrdUomSet] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[UomSetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Promotion]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Promotion](
	[PromotionID] [int] NOT NULL,
	[PromotionCode] [varchar](20) NOT NULL,
	[PromotionName] [varchar](50) NOT NULL,
	[Description] [varchar](255) NULL,
	[StartDate] [datetime] NULL,
	[ExpiryDate] [datetime] NULL,
	[PromotionType] [char](1) NULL,
	[MixType] [char](1) NULL,
	[MixValue] [decimal](15, 2) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[WHID] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[PromotionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PromotionBranch]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PromotionBranch](
	[PromotionID] [int] NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PromotionBranch] PRIMARY KEY CLUSTERED 
(
	[PromotionID] ASC,
	[BranchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PromotionCustomer]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PromotionCustomer](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[CYear] [int] NOT NULL,
	[CMonth] [int] NOT NULL,
	[CDate] [date] NOT NULL,
	[PromotionID] [int] NOT NULL,
	[ShelfID] [varchar](20) NOT NULL,
	[DocNo] [varchar](20) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[FlagNew] [bit] NOT NULL,
	[FlagEdit] [bit] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_ArCustomerPromotionHistory] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PromotionPack]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PromotionPack](
	[PromotionID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[UomSetID] [int] NOT NULL,
	[NormalPrice] [decimal](15, 5) NULL,
	[SpecialPrice] [decimal](15, 5) NULL,
	[TypeDiscount] [char](1) NULL,
	[Discount] [decimal](15, 5) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PromotionPack] PRIMARY KEY CLUSTERED 
(
	[PromotionID] ASC,
	[ProductID] ASC,
	[UomSetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PromotionProduct]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PromotionProduct](
	[AutoProPrd] [int] NOT NULL,
	[PromotionID] [int] NOT NULL,
	[ProductID] [varchar](255) NOT NULL,
	[MinQty] [decimal](10, 4) NOT NULL,
	[MaxQty] [decimal](10, 4) NULL,
	[TypeDiscount] [char](1) NULL,
	[Discount] [decimal](15, 5) NOT NULL,
	[SpecialPrice] [decimal](15, 5) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PromotionProduct_1] PRIMARY KEY CLUSTERED 
(
	[AutoProPrd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PromotionReward]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PromotionReward](
	[AutoProPrd] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[RewardType] [varchar](50) NULL,
	[GiftName] [varchar](50) NULL,
	[GetQty] [int] NULL,
	[ExchangePrice] [decimal](15, 5) NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PromotionReward_1] PRIMARY KEY CLUSTERED 
(
	[AutoProPrd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PromotionVan]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PromotionVan](
	[PromotionVanID] [int] NOT NULL,
	[PromotionVanCode] [varchar](20) NOT NULL,
	[PromotionVanName] [varchar](50) NOT NULL,
	[Description] [varchar](255) NULL,
	[StartDate] [datetime] NULL,
	[ExpiryDate] [datetime] NULL,
	[PromotionVanType] [char](1) NULL,
	[MixType] [char](1) NULL,
	[MixValue] [decimal](15, 2) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[WHID] [varchar](200) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PromotionVanProduct]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PromotionVanProduct](
	[AutoProPrd] [int] NOT NULL,
	[PromotionVanID] [int] NOT NULL,
	[ProductID] [varchar](255) NOT NULL,
	[MinQty] [decimal](10, 4) NOT NULL,
	[MaxQty] [decimal](10, 4) NULL,
	[TypeDiscount] [varchar](5) NULL,
	[Discount] [decimal](15, 5) NOT NULL,
	[SpecialPrice] [decimal](15, 5) NULL,
	[GetQty] [int] NOT NULL,
	[BaseQty] [int] NOT NULL,
	[BasePrice] [decimal](15, 5) NULL,
	[BaseUomID] [int] NOT NULL,
	[BaseUomName] [varchar](50) NOT NULL,
	[ProductSubGroupID] [int] NULL,
	[ProductSubGroupName] [varchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_PromotionVanProduct] PRIMARY KEY CLUSTERED 
(
	[AutoProPrd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ReportGroup]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ReportGroup](
	[GroupID] [int] NOT NULL,
	[GroupName] [varchar](255) NULL,
	[Status] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ReportMenu]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ReportMenu](
	[ReportID] [int] NOT NULL,
	[GroupID] [int] NOT NULL,
	[ReportNameEng] [varchar](255) NULL,
	[ReportNameTh] [varchar](255) NULL,
	[FileName] [varchar](50) NULL,
	[Status] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Roles]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[ChangeStamp] [timestamp] NOT NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_RouteOnsite]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_RouteOnsite](
	[OnsiteID] [int] IDENTITY(1,1) NOT NULL,
	[OnsiteDate] [datetime] NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[SalAreaID] [varchar](20) NOT NULL,
	[Remark] [varchar](100) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_RouteOnsite] PRIMARY KEY CLUSTERED 
(
	[OnsiteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SalArea]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SalArea](
	[SalAreaID] [varchar](20) NOT NULL,
	[SalAreaCode] [char](3) NOT NULL,
	[SalAreaName] [varchar](50) NULL,
	[BranchID] [varchar](20) NOT NULL,
	[Seq] [smallint] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[ZoneID] [int] NOT NULL,
	[SalAreaImage] [image] NULL,
 CONSTRAINT [PK_SalArea] PRIMARY KEY CLUSTERED 
(
	[SalAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SalAreaDistrict]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SalAreaDistrict](
	[SalAreaID] [varchar](20) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[DistrictID] [int] NULL,
	[DistrictCode] [char](6) NOT NULL,
	[DistrictName] [varchar](50) NOT NULL,
	[AreaName] [varchar](50) NOT NULL,
	[ProvinceName] [varchar](50) NOT NULL,
	[PostalCode] [varchar](50) NULL,
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_SalAreaDistrict] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SalAreaVisit]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SalAreaVisit](
	[WHID] [varchar](20) NOT NULL,
	[CNo] [smallint] NOT NULL,
	[DateNo] [smallint] NULL,
	[SalAreaID] [varchar](20) NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_SalAreaVisit] PRIMARY KEY CLUSTERED 
(
	[WHID] ASC,
	[CNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SaleBranchDaily]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SaleBranchDaily](
	[BranchID] [varchar](20) NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[CloseDate] [date] NOT NULL,
	[SellPrice] [decimal](15, 2) NOT NULL,
	[CloseTime] [time](7) NOT NULL,
	[OpenQty] [decimal](10, 3) NOT NULL,
	[QtyReceive] [decimal](10, 3) NOT NULL,
	[QtyInTransit] [decimal](10, 3) NOT NULL,
	[QtyOutTransit] [decimal](10, 3) NOT NULL,
	[QtyOnSale] [decimal](10, 3) NOT NULL,
	[QtyOnReject] [decimal](10, 3) NOT NULL,
	[QtyBalance] [decimal](10, 3) NOT NULL,
	[SaleAmount] [decimal](15, 2) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_SaleBranchDaily] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC,
	[ProductID] ASC,
	[CloseDate] ASC,
	[SellPrice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SaleBranchSummary]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SaleBranchSummary](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[EmpID] [varchar](20) NOT NULL,
	[SaleDate] [date] NOT NULL,
	[ShiftNo] [smallint] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[TotalSale] [decimal](15, 2) NOT NULL,
	[TotalExpense] [decimal](15, 2) NOT NULL,
	[NetTotal] [decimal](15, 2) NOT NULL,
	[IsSend] [bit] NULL,
	[TotalSend] [decimal](15, 2) NULL,
	[IBDocNo] [varchar](50) NOT NULL,
	[INDocNo] [varchar](50) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_SaleSummary] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SaleExpenseDetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SaleExpenseDetail](
	[DocNo] [varchar](50) NOT NULL,
	[ExpenseID] [int] NOT NULL,
	[Qty] [decimal](10, 2) NULL,
	[UnitPrice] [decimal](15, 2) NULL,
	[LineTotal] [decimal](15, 2) NULL,
	[LineRemark] [varchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_SaleExpenseDetail] PRIMARY KEY CLUSTERED 
(
	[DocNo] ASC,
	[ExpenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SaleExpenseMaster]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SaleExpenseMaster](
	[BranchID] [varchar](20) NOT NULL,
	[DocNo] [varchar](50) NOT NULL,
	[DocTypeCode] [char](2) NULL,
	[DocDate] [date] NOT NULL,
	[DocStatus] [char](1) NULL,
	[ExpenseDate] [date] NOT NULL,
	[ShiftNo] [smallint] NULL,
	[EmpID] [varchar](20) NULL,
	[TotalPrice] [decimal](15, 2) NULL,
	[Remark] [varchar](100) NULL,
	[Comment] [varchar](255) NULL,
	[ApprovedBy] [varchar](20) NULL,
	[ApprovedDate] [date] NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_SaleExpense_1] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC,
	[DocNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SaleYearTarget]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SaleYearTarget](
	[CYear] [int] NOT NULL,
	[ProductSubGroupID] [int] NOT NULL,
	[Target] [int] NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_SaleYearTarget] PRIMARY KEY CLUSTERED 
(
	[CYear] ASC,
	[ProductSubGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SendData]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SendData](
	[DateSend] [smalldatetime] NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[BranchID] [varchar](20) NULL,
	[GenNo] [smallint] NULL,
	[FlagSend] [bit] NULL,
	[FlagReceive] [bit] NULL,
	[CrDate] [datetime] NULL,
	[SendDate] [datetime] NULL,
	[ReceiveDate] [datetime] NULL,
	[UserSend] [nvarchar](200) NULL,
 CONSTRAINT [PK_SendData] PRIMARY KEY CLUSTERED 
(
	[DateSend] ASC,
	[WHID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ShopType]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ShopType](
	[ShopTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ShopTypeCode] [char](2) NULL,
	[ShopTypeName] [nvarchar](50) NULL,
	[ShopTypeGroupID] [int] NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_ShopType] PRIMARY KEY CLUSTERED 
(
	[ShopTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ShopTypeGroup]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ShopTypeGroup](
	[ShopTypeGroupID] [int] NOT NULL,
	[ShopTypeGroupCode] [varchar](2) NULL,
	[ShopTypeGroupName] [varchar](50) NULL,
	[ShopTypeGroupDesc] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_ShopTypeGroup] PRIMARY KEY CLUSTERED 
(
	[ShopTypeGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SimCustomer]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SimCustomer](
	[AutoNo] [int] IDENTITY(1,1) NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[CustomerID] [varchar](50) NOT NULL,
	[CustName] [varchar](50) NULL,
	[BillTo] [varchar](200) NULL,
	[ShipTo] [varchar](200) NULL,
	[Telephone] [varchar](50) NULL,
	[Contact] [varchar](50) NULL,
	[TaxId] [char](15) NULL,
	[WHID] [varchar](50) NULL,
	[WHName] [varchar](50) NULL,
	[WHSeq] [varchar](50) NULL,
	[SalAreaID] [varchar](50) NOT NULL,
	[ShopTypeID] [int] NOT NULL,
	[License] [varchar](50) NULL,
	[SalAreaName] [varchar](50) NULL,
	[Seq] [smallint] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[oldCode] [varchar](20) NOT NULL,
 CONSTRAINT [PK_SimCustomer] PRIMARY KEY CLUSTERED 
(
	[AutoNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SimOrder]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SimOrder](
	[BranchID] [varchar](20) NOT NULL,
	[DocNo] [varchar](50) NOT NULL,
	[WHID] [varchar](50) NOT NULL,
	[CustomerID] [varchar](50) NOT NULL,
	[ShopTypeID] [int] NOT NULL,
	[DocDate] [datetime] NOT NULL,
	[RevisionNo] [tinyint] NOT NULL,
	[CustName] [varchar](50) NOT NULL,
	[Amount] [float] NOT NULL,
	[ExcVat] [float] NULL,
	[IncVat] [float] NULL,
	[VatAmt] [float] NULL,
	[TotalDue] [float] NOT NULL,
	[Status] [int] NOT NULL,
	[EmpID] [varchar](50) NOT NULL,
	[PayType] [tinyint] NULL,
	[CrDate] [datetime] NULL,
	[FlagSend] [bit] NOT NULL,
	[RefLoadNo] [varchar](20) NOT NULL,
	[InvNo] [varchar](20) NOT NULL,
	[InvDate] [datetime] NULL,
 CONSTRAINT [PK_SimOrder] PRIMARY KEY CLUSTERED 
(
	[DocNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SimOrderItem]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SimOrderItem](
	[BranchID] [varchar](20) NOT NULL,
	[ItemID] [varchar](50) NOT NULL,
	[WHID] [varchar](50) NOT NULL,
	[CustomerID] [varchar](50) NOT NULL,
	[DocDate] [datetime] NOT NULL,
	[RevisionNo] [tinyint] NOT NULL,
	[DocNo] [varchar](50) NOT NULL,
	[ProductID] [varchar](50) NOT NULL,
	[ProductShortName] [varchar](100) NOT NULL,
	[ProductName] [varchar](100) NOT NULL,
	[OrderUom] [int] NOT NULL,
	[ProductUomName] [varchar](50) NOT NULL,
	[ProductUomNameTH] [varchar](50) NOT NULL,
	[ReceivedQty] [float] NOT NULL,
	[UnitPrice] [float] NOT NULL,
	[VatType] [tinyint] NOT NULL,
	[LineDiscountType] [char](10) NOT NULL,
	[LineDiscount] [float] NOT NULL,
	[LineTotal] [float] NOT NULL,
	[Status] [int] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_SimOrderItem] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TargetDetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TargetDetail](
	[AutoID] [int] NOT NULL,
	[ProductSubGroupID] [int] NOT NULL,
	[ProductSubGroupPrice] [decimal](18, 2) NOT NULL,
	[TargetQty] [int] NOT NULL,
	[TargetBaht] [decimal](18, 2) NOT NULL,
	[TargetWeight] [decimal](18, 2) NOT NULL,
	[TargetValue1] [decimal](18, 2) NOT NULL,
	[TargetValue2] [decimal](18, 2) NOT NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TargetMaster]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TargetMaster](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[CYear] [nchar](4) NOT NULL,
	[CNo] [smallint] NOT NULL,
	[VanQty] [int] NOT NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_TargetMaster] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC,
	[BranchID] ASC,
	[WHID] ASC,
	[CYear] ASC,
	[CNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TL_ArCustomer]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TL_ArCustomer](
	[CustomerID] [varchar](20) NOT NULL,
	[CustomerCode] [char](13) NOT NULL,
	[CustomerRefCode] [varchar](50) NOT NULL,
	[Seq] [smallint] NOT NULL,
	[CustTitle] [nvarchar](20) NOT NULL,
	[CustName] [nvarchar](200) NULL,
	[CustShortName] [nvarchar](200) NULL,
	[CustomerTypeID] [int] NULL,
	[BillTo] [varchar](200) NULL,
	[ShipTo] [varchar](200) NULL,
	[Telephone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Contact] [varchar](200) NULL,
	[AddressNo] [varchar](200) NULL,
	[lane] [varchar](50) NULL,
	[Street] [varchar](50) NULL,
	[AreaID] [int] NULL,
	[DistrictID] [int] NULL,
	[ProvinceID] [int] NULL,
	[PostalCode] [varchar](10) NULL,
	[Email] [varchar](50) NULL,
	[CreditDay] [tinyint] NOT NULL,
	[TaxId] [char](15) NULL,
	[VatType] [bit] NULL,
	[VatRate] [numeric](18, 3) NULL,
	[DiscountType] [char](1) NULL,
	[Discount] [decimal](18, 3) NULL,
	[EmpID] [varchar](20) NULL,
	[PriceGroupID] [int] NULL,
	[WHID] [varchar](20) NOT NULL,
	[SalAreaID] [varchar](20) NULL,
	[ShopTypeID] [int] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[FlagMember] [bit] NOT NULL,
	[NetPoint] [int] NOT NULL,
	[CustomerSAPCode] [varchar](50) NOT NULL,
	[Latitude] [varchar](50) NULL,
	[Longitude] [varchar](50) NULL,
	[GPSDate] [datetime] NULL,
	[IsNewMember] [bit] NOT NULL,
	[FlagNew] [bit] NOT NULL,
	[FlagEdit] [bit] NOT NULL,
	[CustomerImg] [image] NULL,
	[PromotionVanID] [bit] NOT NULL,
 CONSTRAINT [PK_TL_ArCustomer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TL_ArCustomer_Test]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TL_ArCustomer_Test](
	[CustomerID] [varchar](20) NOT NULL,
	[CustomerCode] [char](13) NOT NULL,
	[CustomerRefCode] [varchar](50) NOT NULL,
	[Seq] [smallint] NOT NULL,
	[CustTitle] [nvarchar](20) NOT NULL,
	[CustName] [nvarchar](200) NULL,
	[CustShortName] [nvarchar](200) NULL,
	[CustomerTypeID] [int] NULL,
	[BillTo] [varchar](200) NULL,
	[ShipTo] [varchar](200) NULL,
	[Telephone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Contact] [varchar](200) NULL,
	[AddressNo] [varchar](200) NULL,
	[lane] [varchar](50) NULL,
	[Street] [varchar](50) NULL,
	[AreaID] [int] NULL,
	[DistrictID] [int] NULL,
	[ProvinceID] [int] NULL,
	[PostalCode] [varchar](10) NULL,
	[Email] [varchar](50) NULL,
	[CreditDay] [tinyint] NOT NULL,
	[TaxId] [char](15) NULL,
	[VatType] [bit] NULL,
	[VatRate] [numeric](18, 3) NULL,
	[DiscountType] [char](1) NULL,
	[Discount] [decimal](18, 3) NULL,
	[EmpID] [varchar](20) NULL,
	[PriceGroupID] [int] NULL,
	[WHID] [varchar](20) NOT NULL,
	[SalAreaID] [varchar](20) NULL,
	[ShopTypeID] [int] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[FlagMember] [bit] NOT NULL,
	[NetPoint] [int] NOT NULL,
	[CustomerSAPCode] [varchar](50) NOT NULL,
	[Latitude] [varchar](50) NULL,
	[Longitude] [varchar](50) NULL,
	[GPSDate] [datetime] NULL,
	[IsNewMember] [bit] NOT NULL,
	[FlagNew] [bit] NOT NULL,
	[FlagEdit] [bit] NOT NULL,
	[CustomerImg] [image] NULL,
 CONSTRAINT [PK_TL_ArCustomer_Test] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TL_ArCustomerShelf]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TL_ArCustomerShelf](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[ShelfID] [varchar](20) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[FlagNew] [bit] NOT NULL,
	[FlagEdit] [bit] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_TL_ArCustomerShelf] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TL_DocSignature]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TL_DocSignature](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[DocNo] [nvarchar](50) NULL,
	[DocTypeCode] [varchar](2) NULL,
	[DocDate] [date] NULL,
	[WHID] [varchar](20) NULL,
	[ImgSign] [image] NULL,
	[Remark] [nvarchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TL_DocSignature_1] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TL_PODetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TL_PODetail](
	[AutoID] [bigint] IDENTITY(1,1) NOT NULL,
	[DocNo] [varchar](50) NOT NULL,
	[DocRef] [varchar](50) NULL,
	[DocTypeCode] [char](2) NOT NULL,
	[Line] [smallint] NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[ProductName] [varchar](100) NULL,
	[OrderUom] [int] NOT NULL,
	[OrderQty] [decimal](10, 3) NULL,
	[ReceivedQty] [decimal](8, 2) NULL,
	[RejectedQty] [decimal](8, 2) NULL,
	[StockedQty] [decimal](8, 2) NOT NULL,
	[UnitCost] [decimal](15, 5) NULL,
	[UnitPrice] [decimal](15, 5) NULL,
	[VatType] [tinyint] NULL,
	[LineDiscountType] [char](1) NOT NULL,
	[LineDiscountRate] [decimal](18, 2) NOT NULL,
	[LineDiscount] [decimal](14, 2) NOT NULL,
	[LineTotal] [decimal](15, 5) NULL,
	[CustType] [varchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[UnitComPrice] [decimal](15, 5) NOT NULL,
	[LineComTotal] [decimal](15, 5) NOT NULL,
	[LineRemark] [varchar](100) NOT NULL,
	[FreeQty] [int] NOT NULL,
	[FreeUom] [tinyint] NOT NULL,
	[FreeUnit] [int] NOT NULL,
 CONSTRAINT [PK_TL_PODetail] PRIMARY KEY CLUSTERED 
(
	[DocNo] ASC,
	[DocTypeCode] ASC,
	[AutoID] ASC,
	[ProductID] ASC,
	[OrderUom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TL_POMaster]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TL_POMaster](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[DocNo] [varchar](50) NOT NULL,
	[RevisionNo] [tinyint] NOT NULL,
	[DocTypeCode] [char](2) NOT NULL,
	[DocStatus] [char](1) NOT NULL,
	[DocDate] [datetime] NOT NULL,
	[DocRef] [varchar](50) NULL,
	[StatusInOut] [char](1) NULL,
	[SupplierID] [varchar](20) NULL,
	[SuppName] [varchar](50) NULL,
	[WHID] [varchar](20) NOT NULL,
	[EmpID] [varchar](20) NOT NULL,
	[SaleEmpID] [varchar](20) NOT NULL,
	[SalAreaID] [varchar](20) NOT NULL,
	[Address] [varchar](200) NULL,
	[ContactName] [varchar](200) NULL,
	[ContactTel] [varchar](50) NULL,
	[Shipto] [varchar](255) NULL,
	[CreditDay] [smallint] NULL,
	[DueDate] [datetime] NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[CustType] [varchar](50) NULL,
	[CustName] [varchar](200) NULL,
	[CustInvNO] [varchar](50) NULL,
	[CustInvDate] [datetime] NULL,
	[CustPODate] [datetime] NULL,
	[CustPONo] [varchar](50) NOT NULL,
	[Remark] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[OldAmount] [decimal](15, 5) NULL,
	[Amount] [decimal](15, 5) NULL,
	[OldExcVat] [decimal](15, 5) NULL,
	[ExcVat] [decimal](15, 5) NULL,
	[OldIncVat] [decimal](15, 5) NULL,
	[IncVat] [decimal](15, 5) NULL,
	[VatRate] [decimal](15, 5) NULL,
	[VatAmt] [decimal](15, 5) NULL,
	[Freight] [decimal](15, 5) NOT NULL,
	[DiscountType] [char](1) NOT NULL,
	[OldDiscount] [decimal](14, 2) NULL,
	[Discount] [decimal](14, 2) NULL,
	[TotalDue] [decimal](15, 5) NOT NULL,
	[ApprovedBy] [varchar](20) NULL,
	[ApprovedDate] [datetime] NULL,
	[PayType] [tinyint] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[OldTotalCom] [decimal](15, 5) NOT NULL,
	[TotalCom] [decimal](15, 5) NOT NULL,
	[CNType] [smallint] NULL,
	[DiscountRate] [decimal](14, 2) NOT NULL,
 CONSTRAINT [PK_TL_POMaster] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC,
	[DocNo] ASC,
	[DocTypeCode] ASC,
	[DocStatus] ASC,
	[DocDate] ASC,
	[WHID] ASC,
	[EmpID] ASC,
	[SalAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TL_PRDetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TL_PRDetail](
	[AutoID] [bigint] IDENTITY(1,1) NOT NULL,
	[DocNo] [varchar](50) NOT NULL,
	[DocTypeCode] [char](2) NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[Line] [smallint] NOT NULL,
	[ProductName] [varchar](200) NULL,
	[OrderUom] [int] NOT NULL,
	[OrderQty] [decimal](10, 3) NULL,
	[SendQty] [decimal](8, 2) NULL,
	[ReceivedQty] [decimal](8, 2) NULL,
	[RejectedQty] [decimal](8, 2) NULL,
	[StockedQty] [decimal](8, 2) NOT NULL,
	[UnitCost] [decimal](15, 5) NULL,
	[UnitPrice] [decimal](15, 5) NULL,
	[VatType] [tinyint] NULL,
	[LineTotal] [decimal](15, 5) NULL,
	[LineRemark] [varchar](100) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_TL_PRDetail] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC,
	[DocNo] ASC,
	[ProductID] ASC,
	[Line] ASC,
	[OrderUom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TL_PRMaster]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TL_PRMaster](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[DocNo] [varchar](50) NOT NULL,
	[RevisionNo] [tinyint] NOT NULL,
	[DocTypeCode] [char](2) NOT NULL,
	[DocStatus] [char](1) NOT NULL,
	[DocDate] [datetime] NOT NULL,
	[DocRef] [varchar](50) NOT NULL,
	[FromBranchID] [varchar](20) NOT NULL,
	[FromWHID] [varchar](20) NOT NULL,
	[RequestBy] [varchar](20) NULL,
	[ToBranchID] [varchar](20) NOT NULL,
	[ToWHID] [varchar](20) NOT NULL,
	[StatusInOut] [char](1) NULL,
	[Address] [varchar](200) NULL,
	[ReceiveDate] [datetime] NULL,
	[ReceiveBy] [varchar](20) NULL,
	[ShipDate] [datetime] NULL,
	[ShipBy] [varchar](20) NULL,
	[ShipWHID] [varchar](20) NULL,
	[SalAreaID] [varchar](20) NOT NULL,
	[EmpID] [varchar](20) NOT NULL,
	[ContactName] [varchar](50) NULL,
	[ContactTel] [varchar](50) NULL,
	[Shipto] [varchar](255) NULL,
	[Remark] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[ApprovedBy] [varchar](20) NULL,
	[ApprovedDate] [datetime] NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_TL_PRMaster] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC,
	[DocNo] ASC,
	[DocTypeCode] ASC,
	[DocStatus] ASC,
	[DocDate] ASC,
	[DocRef] ASC,
	[FromBranchID] ASC,
	[FromWHID] ASC,
	[ToBranchID] ASC,
	[ToWHID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TL_PromotionCustomer]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TL_PromotionCustomer](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[CYear] [int] NOT NULL,
	[CMonth] [int] NOT NULL,
	[CDate] [date] NOT NULL,
	[PromotionID] [int] NOT NULL,
	[ShelfID] [varchar](20) NOT NULL,
	[DocNo] [varchar](20) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[FlagNew] [bit] NOT NULL,
	[FlagEdit] [bit] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_TL_PromotionCustomer] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TL_Visit]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TL_Visit](
	[AutoID] [bigint] IDENTITY(1,1) NOT NULL,
	[VisitID] [varchar](20) NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[VisitDate] [datetime] NOT NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[VisitStatus] [bit] NOT NULL,
	[CauseID] [int] NULL,
	[CauseRemark] [varchar](100) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[Latitude] [varchar](50) NULL,
	[Longitude] [varchar](50) NULL,
 CONSTRAINT [PK_TL_Visit_1] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC,
	[VisitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TL_VisitStock]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TL_VisitStock](
	[VisitID] [varchar](20) NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[Qty] [decimal](18, 2) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_TL_VisitStock] PRIMARY KEY CLUSTERED 
(
	[VisitID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TLDetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TLDetail](
	[TLID] [int] NOT NULL,
	[ProductID] [varchar](50) NOT NULL,
	[Qty] [int] NOT NULL,
	[OrderUom] [int] NOT NULL,
 CONSTRAINT [PK_TLDetail] PRIMARY KEY CLUSTERED 
(
	[TLID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TLMaster]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_TLMaster](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[AgentID] [nvarchar](10) NOT NULL,
	[DocDate] [nvarchar](50) NOT NULL,
	[LadyID] [nvarchar](50) NOT NULL,
	[WHID] [nvarchar](10) NOT NULL,
	[DocType] [nvarchar](50) NOT NULL,
	[Revision] [tinyint] NOT NULL,
	[Users] [nvarchar](20) NOT NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagRecieve] [bit] NOT NULL,
 CONSTRAINT [PK_TLMaster] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_TmpPRDetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TmpPRDetail](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[OrderUom] [int] NULL,
	[OrderQty] [decimal](10, 3) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_TmpPRDetail] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TRDetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TRDetail](
	[DocNo] [varchar](50) NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[Line] [smallint] NOT NULL,
	[ProductName] [varchar](200) NULL,
	[OrderUom] [int] NOT NULL,
	[OrderQty] [decimal](10, 3) NULL,
	[SendQty] [decimal](8, 2) NULL,
	[ReceivedQty] [decimal](8, 2) NULL,
	[RejectedQty] [decimal](8, 2) NULL,
	[StockedQty] [decimal](8, 2) NOT NULL,
	[UnitCost] [decimal](15, 5) NULL,
	[UnitPrice] [decimal](15, 5) NULL,
	[VatType] [tinyint] NULL,
	[LineTotal] [decimal](15, 5) NULL,
	[LineRemark] [varchar](100) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_TRDetail] PRIMARY KEY CLUSTERED 
(
	[DocNo] ASC,
	[ProductID] ASC,
	[OrderUom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TRMaster]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TRMaster](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[DocNo] [varchar](50) NOT NULL,
	[RevisionNo] [tinyint] NOT NULL,
	[DocTypeCode] [char](2) NOT NULL,
	[DocStatus] [char](1) NOT NULL,
	[DocDate] [datetime] NOT NULL,
	[DocRef] [varchar](50) NOT NULL,
	[FromBranchID] [varchar](20) NOT NULL,
	[FromWHID] [varchar](20) NOT NULL,
	[RequestBy] [varchar](20) NULL,
	[ToBranchID] [varchar](20) NULL,
	[ToWHID] [varchar](20) NULL,
	[StatusInOut] [char](1) NULL,
	[Address] [varchar](200) NULL,
	[ReceiveDate] [datetime] NULL,
	[ReceiveBy] [varchar](20) NULL,
	[ShipDate] [datetime] NULL,
	[ShipBy] [varchar](20) NULL,
	[ShipWHID] [varchar](20) NULL,
	[SalAreaID] [varchar](20) NOT NULL,
	[EmpID] [varchar](20) NOT NULL,
	[ContactName] [varchar](50) NULL,
	[ContactTel] [varchar](50) NULL,
	[Shipto] [varchar](255) NULL,
	[Remark] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[ApprovedBy] [varchar](20) NULL,
	[ApprovedDate] [datetime] NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[FromWHCode] [varchar](20) NULL,
	[FromLocCode] [varchar](20) NULL,
	[ToWHCode] [varchar](20) NULL,
	[ToLocCode] [varchar](20) NULL,
 CONSTRAINT [PK_TRMaster] PRIMARY KEY CLUSTERED 
(
	[DocNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Users]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Users](
	[Username] [varchar](50) NULL,
	[Password] [varchar](15) NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[EmpID] [varchar](20) NULL,
	[RoleID] [int] NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [varchar](50) NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL CONSTRAINT [DF_Users_FlagDel]  DEFAULT ((0)),
	[FlagSend] [bit] NOT NULL CONSTRAINT [DF_Users_FlagSend]  DEFAULT ((0)),
	[UserID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_VanType]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_VanType](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Seq] [int] NOT NULL,
	[WHType] [smallint] NOT NULL,
	[SalAreaQty] [int] NULL,
	[CrUser] [nvarchar](50) NULL,
	[CrDate] [datetime] NOT NULL,
	[EdUser] [nvarchar](50) NULL,
	[EdDate] [datetime] NOT NULL,
	[FlagDel] [bit] NOT NULL,
 CONSTRAINT [PK_VanType] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Visit]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Visit](
	[VisitID] [varchar](20) NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[VisitDate] [datetime] NOT NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[VisitStatus] [bit] NOT NULL,
	[CauseID] [int] NULL,
	[CauseRemark] [varchar](100) NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[Latitude] [varchar](50) NULL,
	[Longitude] [varchar](50) NULL,
 CONSTRAINT [PK_VisitCustomer_1] PRIMARY KEY CLUSTERED 
(
	[VisitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_VisitStock]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_VisitStock](
	[VisitID] [varchar](20) NOT NULL,
	[ProductID] [varchar](20) NOT NULL,
	[Qty] [decimal](18, 2) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_VisitStock] PRIMARY KEY CLUSTERED 
(
	[VisitID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_VMIDetail]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_VMIDetail](
	[InternalID] [bigint] NOT NULL,
	[ProductID] [varchar](50) NOT NULL,
	[SumSale] [decimal](18, 2) NOT NULL,
	[AvgSale] [decimal](18, 2) NOT NULL,
	[SaleFactor] [decimal](18, 2) NOT NULL,
	[SafetyFactor] [decimal](18, 2) NOT NULL,
	[Stock] [decimal](18, 2) NOT NULL,
	[SaleForecast] [decimal](18, 2) NOT NULL,
	[Suggest] [decimal](18, 2) NOT NULL,
	[Withdraw] [decimal](18, 2) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_VMIMaster]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_VMIMaster](
	[InternalID] [bigint] IDENTITY(1,1) NOT NULL,
	[BranchID] [varchar](20) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[SaleDate] [varchar](10) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
	[FlagReceive] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_VMISafety]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_VMISafety](
	[BranchID] [varchar](20) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[SafetyFactor] [decimal](18, 2) NOT NULL,
	[MinSaleVal] [decimal](18, 2) NOT NULL,
	[MaxSaleVal] [decimal](18, 2) NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_VMISetting]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_VMISetting](
	[BranchID] [varchar](20) NOT NULL,
	[WHID] [varchar](20) NOT NULL,
	[ProductID] [varchar](50) NOT NULL,
	[SaleFactor] [decimal](18, 2) NOT NULL,
	[SafetyFactor] [decimal](18, 2) NOT NULL,
	[UseSafetyFactor] [bit] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Zone]    Script Date: 31/01/2020 11:17:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Zone](
	[ZoneID] [int] NOT NULL,
	[ZoneName] [varchar](50) NOT NULL,
	[Parent] [int] NOT NULL,
	[CrDate] [datetime] NOT NULL,
	[CrUser] [varchar](50) NOT NULL,
	[EdDate] [datetime] NULL,
	[EdUser] [varchar](50) NULL,
	[FlagDel] [bit] NOT NULL,
	[FlagSend] [bit] NOT NULL,
 CONSTRAINT [PK_Zone] PRIMARY KEY CLUSTERED 
(
	[ZoneID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_AdmControlList] ON 

INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (1, N'mnuItemOrder', 1, N'1.1 สั่งสินค้า (OD)', 1)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (2, N'mnuItemReceive', 1, N'1.2 ซื้อเงินเชื่อ (RE)', 2)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (3, N'mnuItmAP', 1, N'1.3 ข้อมูลผู้จำหน่าย', 3)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (4, N'mnuReceipt', 1, N'2.1 ใบกำกับภาษีอย่างย่อ (IV)', 4)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (5, N'mnuItmInvoice', 1, N'2.1.1 ขายเงินสดจาก Tablet (CVS)', 5)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (6, N'mnuItmInvoice_Manual', 1, N'2.1.2 ขายเงินสดคลัง Van', 6)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (7, N'mnuItmPreSaleIV', 1, N'2.1.3 ขายเงินสดจาก Tablet (PreOrder)', 7)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (8, N'mnuItmPreSaleIV_Manual', 1, N'2.1.4 ขายเงินสดคลัง1000 (PreOrder)', 8)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (9, N'mnuItmInvoiceFull', 1, N'2.2 ใบกำกับภาษีเต็มรูป (V) ', 9)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (10, N'mnuLoadRL', 1, N'2.3 ใบจัดสินค้า(PreOrder)', 10)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (11, N'mnuItmBankNote', 1, N'2.4 Bank Note', 11)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (12, N'mnuItmTransferRL', 1, N'3.1 โอนสินค้าเข้าคลังหน่วยรถ (RL)', 12)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (13, N'mnuItmTransferRB', 1, N'3.2 โอนสินค้าระหว่างคลัง (RB)', 13)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (14, N'mnuItmTransferRJ', 1, N'3.3 ทำลายสินค้า (RJ)', 14)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (15, N'mnuItmReturn', 1, N'3.4 คืนสินค้า (RT)', 15)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (16, N'mnuItmMat2Mat', 1, N'3.5 โอนสินค้า Mat to Mat (TR)', 16)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (17, N'mnuItmMovement', 1, N'3.6 ตรวจสอบสินค้าเคลื่อนไหว', 17)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (18, N'mnuItmQtyOnhandUpdate', 1, N'3.7 คำนวณสินค้าคงเหลือ', 18)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (19, N'mnuItmBranch', 1, N'4.1 ข้อมูลศูนย์กระจายสินค้า (เดโป้)', 19)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (20, N'mnuItmSalePersonSetting', 1, N'4.2 ข้อมูลหน่วยขาย', 20)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (21, N'mnuItmProduct', 1, N'4.3 ข้อมูลสินค้า', 21)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (22, N'mnuItmPriceList', 1, N'4.4 ตารางราคาสินค้า', 22)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (23, N'mnuItmAR', 1, N'4.5 ข้อมูลลูกค้า', 23)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (24, N'mnuItmTansferCust', 1, N'4.6 โอนย้ายลูกค้า', 24)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (25, N'mnuItmEmployee', 1, N'4.7 ข้อมูลพนักงาน', 25)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (26, N'mnuItmTransferVan', 1, N'4.8 โอนย้ายพนักงานขาย/ตลาด', 26)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (27, N'mnuItmDisplay', 1, N'4.9 ข้อมูลสื่อแนะนำสินค้า', 27)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (28, N'mnuMasterOther', 1, N'4.10 อื่นๆ', 28)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (29, N'mnuItmProvince', 1, N'4.10.1 ข้อมูลสถานที่ (จังหวัด,อำเภอ,ตำบล)', 29)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (30, N'mnuItmPosition', 1, N'4.10.2 ข้อมูลตำแหน่ง', 30)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (31, N'mnuItmDepartment', 1, N'4.10.3 ข้อมูลแผนก', 31)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (32, N'mnuItmRemarkReject', 1, N'4.10.4 ข้อมูลสาเหตุการชำรุด', 32)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (33, N'mnuItmFlavour', 1, N'4.10.5 ข้อมูลรสชาติ', 33)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (34, N'mnuItmReceiveDataFromTablet', 1, N'5.1 รับข้อมูลจาก Tablet (ดึงยอดขาย)', 34)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (35, N'mnuItmSendtoCenter', 1, N'5.2 ส่งข้อมูลเข้า Data Center', 35)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (36, N'mnuItmTransferDataCenter', 1, N'5.3 รับ/ส่งข้อมูล', 36)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (37, N'mnuItmCloseDay', 1, N'5.4 ปิดวัน', 37)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (38, N'mnuItmReportSale', 1, N'6.1 รายงาน', 38)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (39, N'mnuItmCompany', 1, N'7.1 ตั้งค่าบริษัท', 39)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (40, N'mnuItmBranchTerritory', 1, N'7.2 ตั้งค่าพื้นที่เขตการขาย', 40)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (41, N'mnuItmDBSetting', 1, N'7.3 ตั้งค่าฐานข้อมูล', 41)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (42, N'mnuItmDocument', 1, N'7.4 ตั้งค่าเอกสาร', 42)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (43, N'mnuItmCycleSetting', 1, N'7.5 ตั้งค่า Cycle', 43)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (44, N'mnuItmSetting', 1, N'7.6 ตั้งค่าขั้นสูง', 44)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (45, N'mnuItmTargerSetting', 1, N'7.7 ตั้งค่า Target', 45)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (46, N'mnuItmUser', 1, N'7.8 ผู้ใช้งานระบบ', 46)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (47, N'mnuItmAuthorize', 1, N'7.9 ตั้งค่าสิทธิการใช้งาน', 47)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (48, N'mnuItmBackupDB', 1, N'7.10 สำรองฐานข้อมูล', 48)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (49, N'mnuItmLogOff', 1, N'Log Off ระบบ', 49)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (50, N'mnuItmSendProduct', 1, N'8.1 ส่งข้อมูลสินค้า', 50)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (51, N'mnuItmCheckOreder', 1, N'8.2 ดึงใบสั่งสินค้า(OD)', 51)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (52, N'mnuItmSetPrice', 1, N'8.3 ปรับปรุงราคาสินค้า', 52)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (53, N'mnuItmSetLoyalty', 1, N'8.4 ตั้งค่า Loyalty', 53)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (54, N'mnuItmChangeDocState', 1, N'8.5 ปรับสถานะเอกสาร', 54)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (55, N'mnuItmSendSaleState', 1, N'8.6 ปรับสถานะการส่งข้อมูลยอดขาย', 55)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (56, N'mnuItmWindowCascade', 1, N'Cascade', 56)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (57, N'mnuItmWindowTileHoriz', 1, N'Tile Horizontal', 57)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (58, N'mnuItmWindowTileVertical', 1, N'Tile Vertical', 58)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (59, N'mnuItmFrmRole', 1, N'7.9 ตั้งค่าสิทธิการใช้งาน', 59)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (60, N'mnuItemAbout', 1, N'เกี่ยวกับ BackEnd', 60)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (61, N'mnuItmVMI', 1, N'9.1 VMI', 61)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (62, N'mnuItmVisit', 1, N'9.2 ร้านเยี่ยม/สต็อคร้านเยี่ยม', 62)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (63, N'mnuItmOD_H', 1, N'8.4 ดึงใบโอนสินค้า(OD=>H)', 62)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (64, N'mnuItmRL_C', 1, N'8.5 ดึงใบโอนสินค้า (RL=>C)', 63)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (65, N'mnuItmRB_C', 1, N'8.6 ดึงใบโอนสินค้า (RB=>C)', 64)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (66, N'mnuItmIV_V', 1, N'8.7 ดึงเอกสารขายเงินสด (IV=>V)', 65)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (67, N'mnuItmUOM', 1, N'4.10.6 ข้อมูลหน่วยสินค้า', 34)
INSERT [dbo].[tbl_AdmControlList] ([ControlID], [ControlName], [FormID], [ControlText], [Seq]) VALUES (68, N'mnuItmMapping', 1, N'9.1. แผนที่ตั้งร้านค้า', 1)
SET IDENTITY_INSERT [dbo].[tbl_AdmControlList] OFF
SET IDENTITY_INSERT [dbo].[tbl_AdmFormList] ON 

INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (1, N'FrmMain', N'เมนูหลัก')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (2, N'FrmReceivePO', N'ซื้อเงินเชื่อ (RE)')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (3, N'FrmApSupplier', N'ข้อมูลผู้จำหน่าย')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (4, N'FrmInvoice', N'ขายเงินสดจาก PDA (IV)')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (5, N'FrmInvoice', N'ขายเงินสด')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (6, N'FrmCreditNote', N'ใบลดหนี้ (CN)')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (7, N'FrmArCustomer', N'ข้อมูลลูกค้า')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (8, N'FrmTransferCustomer', N'โอนย้ายลูกค้า')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (9, N'FrmEmployee', N'ข้อมูลพนักงาน')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (10, N'FrmPay', N'Bank Note')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (11, N'FrmTransferRL', N'โอนสินค้าเข้าคลังหน่วยรถ (RL)')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (12, N'FrmTransferRB', N'โอนสินค้าระหว่างคลัง (RB)')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (13, N'FrmReject', N'ทำลายสินค้า (RJ)')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (14, N'FrmProduct', N'ข้อมูลสินค้า')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (15, N'FrmPricing', N'ตารางราคาสินค้า')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (16, N'FrmSetPromotion', N'กำหนดโปรโมชั่นสินค้า')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (17, N'FrmMovement', N'ตรวจสอบสินค้าเคลื่อนไหว')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (18, N'FrmAdjustStock', N'ตรวจนับสินค้า')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (19, N'FrmUpdateQtyOnhand', N'ปรับปรุงแก้จำนวนสินค้า')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (20, N'FrmPromotionBom', N'ชุดสินค้าโปรโมชั่น')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (21, N'FrmSetProductChanged', N'ตั้งสินค้าปรับปรุงใหม่')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (22, N'FrmBranch', N'ข้อมูลศูนย์กระจายสินค้า (เดโป้)')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (23, N'FrmLocation', N'ข้อมูลสถานที่ (จังหวัด,อำเภอ,ตำบล)')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (24, N'FrmMaster', N'ข้อมูลตำแหน่ง')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (25, N'FrmMaster', N'ข้อมูลแผนก')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (26, N'FrmRemarkReject', N'ข้อมูลสาเหตุการชำรุด')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (27, N'Loyalty', N'Loyalty')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (28, N'FrmVanRoute', N'สร้างข้อมูลให้ Pocket PC (เตรียมตลาด)')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (29, N'FrmReceiveVanData', N'รับข้อมูลจาก Pocket PC (ดึงยอดขาย)')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (30, N'FrmLoadData', N'ดึงข้อมูลยอดขายผ่าน SIM')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (31, N'FrmUploadDBCenter', N'ส่งข้อมูลเข้า Data Center')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (32, N'FrmTransferDataCenter', N'รับ/ส่งข้อมูล')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (33, N'FrmChangeDocState', N'ปรับสถานะเอกสาร')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (34, N'FrmChageSendSaleState', N'ปรับปรุงสถานะการส่งข้อมูลยอดขาย')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (35, N'FrmPreSalePDAOut', N'เตรียมตลาดเข้า PDA')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (36, N'FrmPreSaleRL', N'โอนสินค้าขึ้นรถ Van')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (37, N'FrmPreSaleRL_SIM', N'จัดสินค้าขึ้นรถ จาก SIM')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (38, N'FrmPreSaleInvoice', N'ขายสินค้าจาก PDA (PreSale)')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (39, N'FrmPreSaleInvoice', N'ขายเงินสด (PreSale)')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (40, N'FrmCompany', N'ตั้งค่าบริษัท')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (41, N'FrmBranchTerritory', N'ตั้งค่าพื้นที่เขตการขาย')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (42, N'FrmDBConfig', N'ตั้งค่าฐานข้อมูล')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (43, N'FrmDocument', N'ตั้งค่าเอกสาร')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (44, N'FrmCycle', N'ตั้งค่า Cycle')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (45, N'FrmSetting', N'ตั้งค่าขั้นสูง')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (46, N'FrmTarget', N'ตั้งค่า Target')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (47, N'FrmUsers', N'ผู้ใช้งานระบบ')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (48, N'FrmRole', N'ตั้งค่าสิทธิการใช้งาน')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (49, N'FrmCondition', N'รายงาน')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (50, N'FrmVMI', N'VMI')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (51, N'FrmVisit', N'ร้านเยี่ยม/สต็อคร้านเยี่ยม')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (52, N'FrmSalePersonSetting', N'ข้อมูลหน่วยขาย')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (53, N'FrmPromotion', N'4.8 ข้อมูลโปรโมชั่น')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (54, N'FrmTransferVan', N'2.7 โอนย้ายพนักงานขาย/ตลาด')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (55, N'FrmMemberList', N'สมาชิก Loyalty')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (56, N'FrmBackupDB', N'สำรองฐานข้อมูล')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (57, N'FrmProductFlavour', N'ข้อมูลรสชาติ')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (58, N'FrmSendSaleDay', N'ส่งยอดขายนม PP')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (59, N'FrmReturn', N'สินค้าคืนจากลูกค้า')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (60, N'FrmDisplayImage', N'4.11 ข้อมูลสื่อแนะนำสินค้า')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (61, N'FrmReceiveTLData', N'5.3 รับข้อมูลจาก Tablet (ดึงยอดขาย)')
INSERT [dbo].[tbl_AdmFormList] ([FormID], [FormName], [FormText]) VALUES (62, N'FrmCreditPay', N'2.5 ชำระขายเงินเชื่อ (PM)')
SET IDENTITY_INSERT [dbo].[tbl_AdmFormList] OFF
SET IDENTITY_INSERT [dbo].[tbl_AdmMenuList] ON 

INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (1, 1, N'1', N'mnuItemReceive', N'1.1 ซื้อเงินเชื่อ (RE)', NULL, N'FrmReceivePO', N'mnuProduct', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (2, 2, N'1', N'mnuItmAP', N'1.2 ข้อมูลผู้จำหน่าย', NULL, N'FrmApSupplier', N'mnuProduct', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (3, 3, N'1', N'mnuItmInvoice', N'2.1 ขายเงินสดจาก PDA (IV)', NULL, N'FrmInvoice', N'mnuItmSale', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (4, 4, N'1', N'mnuItmInvoice_Manual', N'2.2 ขายเงินสด', NULL, N'FrmInvoice', N'mnuItmSale', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (5, 5, N'1', N'mnuItmCreditNote', N'2.3 ใบลดหนี้ (CN)', NULL, N'FrmCreditNote', N'mnuItmSale', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (6, 6, N'1', N'mnuItmAR', N'2.4 ข้อมูลลูกค้า', NULL, N'FrmArCustomer', N'mnuItmSale', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (7, 7, N'1', N'mnuItmTansferCust', N'2.5 โอนย้ายลูกค้า', NULL, N'FrmTransferCustomer', N'mnuItmSale', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (8, 8, N'1', N'mnuItmEmployee', N'2.6 ข้อมูลพนักงาน', NULL, N'FrmEmployee', N'mnuItmSale', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (9, 9, N'1', N'mnuItmBankNote', N'2.7 Bank Note', NULL, N'FrmPay', N'mnuItmSale', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (10, 10, N'1', N'mnuItmTransferRL', N'3.1 โอนสินค้าเข้าคลังหน่วยรถ (RL)', NULL, N'FrmTransferRL', N'mnuTransfer', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (11, 11, N'1', N'mnuItmTransferRB', N'3.2 โอนสินค้าระหว่างคลัง (RB)', NULL, N'FrmTransferRB', N'mnuTransfer', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (12, 12, N'1', N'mnuItmTransferRJ', N'3.3 ทำลายสินค้า (RJ)', NULL, N'FrmReject', N'mnuTransfer', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (13, 13, N'1', N'mnuItmProduct', N'3.4 ข้อมูลสินค้า', NULL, N'FrmProduct', N'mnuTransfer', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (14, 14, N'1', N'mnuItmPriceList', N'3.5 ตารางราคาสินค้า', NULL, N'FrmPricing', N'mnuTransfer', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (15, 15, N'1', N'mnuItmSetPromotion', N'3.6 กำหนดโปรโมชั่นสินค้า', NULL, N'FrmSetPromotion', N'mnuTransfer', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (16, 16, N'1', N'mnuItmMovement', N'3.7 ตรวจสอบสินค้าเคลื่อนไหว', NULL, N'FrmMovement', N'mnuTransfer', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (17, 17, N'1', N'mnuItmCheckStock', N'3.8 ตรวจนับสินค้า', NULL, N'FrmAdjustStock', N'mnuTransfer', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (18, 18, N'1', N'mnuItmQtyOnhandUpdate', N'3.9 ปรับปรุงแก้จำนวนสินค้า', NULL, N'FrmUpdateQtyOnhand', N'mnuTransfer', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (19, 19, N'1', N'mnuItmSetProductChanged', N'4.0 ตั้งสินค้าปรับปรุงใหม่', NULL, N'FrmSetProductChanged', N'mnuTransfer', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (20, 20, N'1', N'mnuItmBranch', N'4.1 ข้อมูลศูนย์กระจายสินค้า (เดโป้)', NULL, N'FrmBranch', N'mnuMasterData', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (21, 21, N'1', N'mnuItmProvince', N'4.2 ข้อมูลสถานที่ (จังหวัด,อำเภอ,ตำบล)', NULL, N'FrmLocation', N'mnuMasterData', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (22, 22, N'1', N'mnuItmPosition', N'4.3 ข้อมูลตำแหน่ง', NULL, N'FrmMaster', N'mnuMasterData', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (23, 23, N'1', N'mnuItmDepartment', N'4.4 ข้อมูลแผนก', NULL, N'FrmMaster', N'mnuMasterData', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (24, 24, N'1', N'mnuItmRemarkReject', N'4.5 ข้อมูลสาเหตุการชำรุด', NULL, N'FrmRemarkReject', N'mnuMasterData', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (25, 25, N'1', N'mnuItmLoyalty', N'4.6 Loyalty', NULL, N'Loyalty', N'mnuMasterData', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (26, 26, N'1', N'mnuItmGenerateRouteData', N'5.1 สร้างข้อมูลให้ Pocket PC (เตรียมตลาด)', NULL, N'FrmVanRoute', N'mnuProcess', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (27, 27, N'1', N'mnuReceiveDataFromPDA', N'5.2 รับข้อมูลจาก Pocket PC (ดึงยอดขาย)', NULL, N'FrmReceiveVanData', N'mnuProcess', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (28, 28, N'1', N'mnuItmImportSimData', N'5.3 ดึงข้อมูลยอดขายผ่าน SIM', NULL, N'FrmLoadData', N'mnuProcess', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (29, 29, N'1', N'mnuItmSendtoCenter', N'5.3 ส่งข้อมูลเข้า Data Center', NULL, N'FrmUploadDBCenter', N'mnuProcess', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (30, 30, N'1', N'mnuItmTransferDataCenter', N'5.4 รับ/ส่งข้อมูล', NULL, N'FrmTransferDataCenter', N'mnuProcess', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (31, 31, N'1', N'mnuItmChangeDocState', N'5.5 ปรับสถานะเอกสาร', NULL, N'FrmChangeDocState', N'mnuProcess', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (32, 32, N'1', N'mnuItmSendSaleState', N'5.6 ปรับปรุงสถานะการส่งข้อมูลยอดขาย', NULL, N'FrmChageSendSaleState', N'mnuProcess', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (33, 33, N'1', N'mnuItmPDAOut', N'6.1 เตรียมตลาดเข้า PDA', NULL, N'FrmPreSalePDAOut', N'mnuPreSale', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (34, 34, N'1', N'mnuLoadRL', N'6.2 โอนสินค้าขึ้นรถ Van', NULL, N'FrmPreSaleRL', N'mnuPreSale', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (35, 35, N'1', N'mnuItmLoadRL_SIM', N'6.3 จัดสินค้าขึ้นรถ จาก SIM', NULL, N'FrmPreSaleRL_SIM', N'mnuPreSale', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (36, 36, N'1', N'mnuItmPreSaleIV', N'6.4 ขายสินค้าจาก PDA (PreSale)', NULL, N'FrmPreSaleInvoice', N'mnuPreSale', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (37, 37, N'1', N'mnuItmPreSaleIV_Manual', N'6.5 ขายเงินสด (PreSale)', NULL, N'FrmPreSaleInvoice', N'mnuPreSale', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (38, 38, N'1', N'mnuItmCompany', N'7.1 ตั้งค่าบริษัท', NULL, N'FrmCompany', N'mnuSetting', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (39, 39, N'1', N'mnuItmBranchTerritory', N'7.2 ตั้งค่าพื้นที่เขตการขาย', NULL, N'FrmBranchTerritory', N'mnuSetting', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (40, 40, N'1', N'mnuItmDBSetting', N'7.3 ตั้งค่าฐานข้อมูล', NULL, N'FrmDBConfig', N'mnuSetting', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (41, 41, N'1', N'mnuItmDocument', N'7.4 ตั้งค่าเอกสาร', NULL, N'FrmDocument', N'mnuSetting', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (42, 42, N'1', N'mnuItmCycleSetting', N'7.5 ตั้งค่า Cycle', NULL, N'FrmCycle', N'mnuSetting', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (43, 43, N'1', N'mnuItmSetting', N'7.6 ตั้งค่าขั้นสูง', NULL, N'FrmSetting', N'mnuSetting', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (44, 44, N'1', N'mnuItmTargerSetting', N'7.7 ตั้งค่า Target', NULL, N'FrmTarget', N'mnuSetting', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (45, 45, N'0', N'mnuItmUser', N'7.8 ผู้ใช้งานระบบ', NULL, N'FrmUsers', N'mnuSetting', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (46, 46, N'0', N'mnuItmAuthorize', N'7.9 ตั้งค่าสิทธิการใช้งาน', NULL, N'FrmRole', N'mnuSetting', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (47, 47, N'1', N'mnuItmLogOff', N'Log Off ระบบ', NULL, N'', N'mnuSetting', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (48, 48, N'1', N'mnuItmReportSale', N'8.1 รายงาน', NULL, N'FrmCondition', N'mnuReport', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (49, 49, N'1', N'mnuItmWindowCascade', N'Cascade', NULL, N'', N'mnuWindow', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (50, 50, N'1', N'mnuItmWindowTileHoriz', N'Tile Horizontal', NULL, N'', N'mnuWindow', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (51, 51, N'1', N'mnuItmWindowTileVertical', N'Tile Vertical', NULL, N'', N'mnuWindow', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (52, 52, N'1', N'mnuItemAbout', N'เกี่ยวกับ BackEnd', NULL, N'', N'mnuHelp', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (53, 53, N'1', N'mnuItmVMI', N'9.1 VMI', NULL, N'FrmVMI', N'mnuVMI', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (54, 54, N'1', N'mnuItmVisit', N'9.2 ร้านเยี่ยม/สต็อคร้านเยี่ยม', NULL, N'FrmVisit', N'mnuVMI', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (55, 55, N'1', N'mnuItmDisplay', N'4.11 ข้อมูลสื่อแนะนำสินค้า', NULL, N'FrmDisplayImage', N'mnuMasterData', 0, 1, 0)
INSERT [dbo].[tbl_AdmMenuList] ([GrpAutoID], [MIndex], [FormMode], [MenuName], [MenuText], [MenuText2], [FormName], [MenuParent], [ImageIndex], [Status], [FlagSend]) VALUES (56, 56, N'1', N'mnuItmReceiveDataFromTablet', N'5.2 รับข้อมูลจาก Pocket PC (ดึงยอดขาย)', NULL, N'FrmReceiveTLData', N'mnuProcess', 0, 1, 0)
SET IDENTITY_INSERT [dbo].[tbl_AdmMenuList] OFF
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 0, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 1, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 2, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 3, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 4, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 5, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 6, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 7, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 8, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 9, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 10, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 11, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 12, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 13, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 14, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 15, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 16, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 17, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 18, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 19, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 20, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 21, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 22, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 23, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 24, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 25, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 26, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 27, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 28, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 29, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 30, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 31, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 32, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 33, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 34, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 35, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 36, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 37, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 38, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 39, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 40, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 41, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 42, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 43, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 44, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 45, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 46, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 47, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 48, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 49, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 50, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 51, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 52, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 53, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 54, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 55, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 56, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 57, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 58, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 59, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 60, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 61, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 62, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 63, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 64, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 65, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 66, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 67, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (1, 68, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 0, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 1, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 2, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 3, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 4, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 5, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 6, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 7, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 8, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 9, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 10, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 11, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 12, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 13, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 14, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 15, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 16, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 17, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 18, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 19, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 20, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 21, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 22, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 23, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 24, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 25, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 26, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 27, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 28, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 29, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 30, 1, 1, N'')
GO
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 31, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 32, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 33, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 34, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 35, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 36, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 37, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 38, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 39, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 40, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 41, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 42, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 43, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 44, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 45, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 46, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 47, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 48, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 49, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 50, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 51, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 52, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 53, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 54, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 55, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 56, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 57, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 58, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 59, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 60, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 61, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 62, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 63, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 64, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 65, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 66, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 67, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (2, 68, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 0, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 53, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 54, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 55, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 56, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 57, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 58, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 59, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 60, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 61, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 62, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 63, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 64, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 65, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 66, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 67, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (3, 68, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 0, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 1, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 2, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 3, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 4, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 5, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 6, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 7, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 8, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 9, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 10, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 11, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 12, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 13, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 14, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 15, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 16, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 17, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 18, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 19, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 20, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 21, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 22, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 23, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 24, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 25, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 26, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 27, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 28, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 29, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 30, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 31, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 32, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 33, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 34, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 35, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 36, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 37, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 38, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 39, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 40, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 41, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 42, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 43, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 44, 1, 0, N'')
GO
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 45, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 46, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 47, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 48, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 49, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 50, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 51, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 52, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 53, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 54, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 55, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 56, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 57, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 58, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 59, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 60, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 61, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 62, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 63, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 64, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 65, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 66, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 67, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (4, 68, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 1, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 2, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 3, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 4, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 5, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 6, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 7, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 8, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 9, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 10, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 11, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 12, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 13, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 14, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 15, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 16, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 17, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 18, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 19, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 20, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 21, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 22, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 23, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 24, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 25, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 26, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 27, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 28, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 29, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 30, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 31, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 32, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 33, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 34, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 35, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 36, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 37, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 38, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 39, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 40, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 41, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 42, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 43, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 44, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 45, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 46, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 47, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 48, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 49, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 50, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 51, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 52, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 53, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 54, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 55, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 56, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 57, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 58, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 59, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 60, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 61, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 62, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 63, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 64, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 65, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 66, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (5, 68, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (6, 0, 1, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (6, 68, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (7, 68, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 1, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 2, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 3, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 4, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 5, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 6, 0, 0, N'')
GO
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 7, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 8, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 9, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 10, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 11, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 12, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 13, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 14, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 15, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 16, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 17, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 18, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 19, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 20, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 21, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 22, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 23, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 24, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 25, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 26, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 27, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 28, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 29, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 30, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 31, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 32, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 33, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 34, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 35, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 36, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 37, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 38, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 39, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 40, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 41, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 42, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 43, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 44, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 45, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 46, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 47, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 48, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 49, 1, 1, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 50, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 51, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 52, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 53, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 54, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 55, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 56, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 57, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 58, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 59, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 60, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 61, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 62, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 63, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 64, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 65, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 66, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 67, 0, 0, N'')
INSERT [dbo].[tbl_AdmRoleControl] ([RoleID], [ControlID], [Visible], [Enable], [DefaultValue]) VALUES (9, 68, 0, 0, N'')
SET IDENTITY_INSERT [dbo].[tbl_MstMenu] ON 

INSERT [dbo].[tbl_MstMenu] ([MenuID], [UserID], [MenuName], [MenuText], [FormName], [MenuParent], [MenuImage], [Seq]) VALUES (1, 1, N'PlaceAnOrder', N'1. ซื้อสินค้า', N'', NULL, 0x89504E470D0A1A0A0000000D4948445200000200000002000806000000F478D4FA000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000DD600000DD601906F799C0000001974455874536F667477617265007777772E696E6B73636170652E6F72679BEE3C1A00005E0D49444154785EEDDD79B455E59D37F82A627C63C50CBD92AAE4EDB6AB6367A5D359AB7B252B79D39DB52AAB5662577555BF498CD96A8CCABEF732CF5C44050754444011154710511010106450110595790619647242140790E132CA70CFB4FBF91DEEC57DF7FD9E73F6F0EC73F6F0FDE353151FEEDD67B8E7797EDFB3F7B39FE7EF2CCB8A8D8E75037EA90C561E57E6286B948F94B38A4544D5D1A9AEB1B997D978A8D1ECFFFE4D1DFBADBAA563BF17EEECD877C4F46B7BFCDF96615E84FA2F11450B6C8C0A35D05CA0FC3FCA63CA2732F01051B4DDDEB1AF35E76F3D321F5DD5E90B1506B62A8B9569CA08A5ABF26FCA8F950B51BF27A2EA808DB5A60691FFA20C540EB50E2A44143FB776EC676DFE6B174B157B24AF7CAAAC50A628772B9D94DF293F523AA0F18188F4808DB5A2068CBF57AE57E4B43E1C5088287E865FDFC7DA7D75271402CA39AD6C549E511A95CB94EFA3B18388BC838DB5A006895F2B9BED83061125CB23D7F5B68E5D598F8ABD17FB9485CA68C5547EAEF072029147B0B1DAD4C0709D72C63E50105132359AFDAD8FAFF27C36A092ACB25D794AA9537E8CC61A22FA0A6CAC163518C829FF91F6C1818892AF8BD9686D283D374097FDCA6C6580F26BE502340E11A5156CAC0635087C5379D13E2810517A98CABCBF7547853B2CA79425CA30E53F946FA3B189282D6063D854E7976FFE2CFE4464BD7A4D3754ACAB41EE4258AFC8DD07BF5178D701A50A6C0C9BEAF43CED4F4445722660EBD59D5181AEB626658652AFFC108D5D4449021BC3A43ABC4CF883030111A55337B3D1FAFCAA0654946BA5A06C56462AFFAA70FE00250E6C0C8BEAE872AB1F67FB13513B3776EC679DBAB20E15E32838AECC51E4ECC077D0F8461437B0310CAA83CB75FF4DF60E4F4464F7DCB53D51F18D9A66E565E57AE55B68BC238A03D81806D5B9AF7576762222BB06E5D05581170AAAA633CA5CE51AE59B68EC238A2AD8A89BEAD8172A7BEC1D9D8808197B5D2F5468E3406E339CA55CA97047448A3CD8A89BEAD4FD9D9D9C880891BB02425829B0DA4E2AD395FF54787B2145126CD4497568D9D2F780BD83131195F3F075BD51518DABBDCA9DCA25688C24AA15D8A893EACC97393B7735C8B78881FDFB596386F5B61E1FD9AB6AEEBBA38FD5AF777FF89C88C89DCE758DD6D9E8DE11E0574E99AF5CAE7C0D8D9744D5041B75529DF95167E70E93593FC09AF6480FEBD406D5E1DEEE58331F2E6AB046DCDE073E4722AA6C63F87B05D4D2E7CA70E552346E1255036CD44975E4BDCE8E1D96864E8DD6AE573AC1825C2BCF3CD0133E57222A6F5C7C27037A210B0EBDAE5CA57C1D8DA14461818DBAA84EFC4B67A70ED38267BAC1225C4BF9AD1DAD913C1340E4594FB3D1CAE1A299540714992BF07D349E12E9061B75519D78B0B3538765D0807E5641155B54846B6DC7FC4EF039135179BBAF8EFDDD007E9C56C6293F41E32A912EB05117D5811F7776E8B04C7CA0272CBE517153633FF8BC89A8B4F5C99E075089EC56384FF92D1A5F8982828DBAA80E3CC7D9A1C3B2F4B9AEB0F046C5C407391780C8AB45B5DB2A386AD629324F80770F9036B05117D581D7383B7458DE981CED00B0765617F8BC89A8B4997FEB818A619AED51FA295C769802838DBAA80EFC91B3438765FCA85EB0F046C5B13575F079135169E3AF4DC59D007E1C5164C2E0B7D1D84BE4066CD44575E0B3CE0E1D96C6BEFDADCC26D53140F18D0A99A8889E3B1161A3AEEF838A1F7DA54919ACF08C0079061B75411D3A4CCF3DD20316DEA8E03C00226F863300B825B710DEA07C038DC544086CD40575E830C9F2BF4BA646772E00E7011079C300E099AC30D85BB9108DC94476B05117D4A1ABE181A1BDADDD8B1A227749E0D85ACE0320F28201C037D980A8AB72011A9B89046CD40575E86AAA6F38B740D0ED37F50D6CF86D7DAC671FEA696D9EDB191677B7380F80C83D0680C0762BA6C22D89A91DD8A80BEAD04970F72D7DAC832BEA6181AF6412E70110B9C600A0CD56E577689CA6F4828DBAA00E9D1437F6EF573CA58F8A7C39EB380F80C8350600EDE628DC81908A60A32EA84327C9B87BBDAF3DC0790044EE310084E2AC3252B9188DDB941EB05117D4A193A4A1D300EBB88FB3009C0740E40E0340A8F62B0DCADFA3F19B920F36EA823A74D22CF3B10701E70110B9C30050151B957F416338251B6CD40575E8A419779FF7CB00EB5FE03C0022371800AA6A86F2CF682CA764828DBAA00E9D34B204312AF2E5C86503742C226A8B01A0EA4E293729DC75300560A32EA84327D1A195DEE7010CBE81F300882A6100A899B7945FA0719D920336EA823A7412AD9CD10516F972645121742C22FA0A03404D6595FB14EE2F9050B05117D4A19368C2FD3D61912F87F300882A63008884F7152E229440B05117D4A1936860FF7EB0C897C379004495310044464199A07C178DF5144FB05117D4A193EAC86ACE0320D2ED1E0680A8D9A71868BCA7F8818DBAA00E9D54AB9FE73C0022DD6E630088AAB9CA0FD1B84FF1011B75411D3AA9263EC0790044BADDCC0010658794CBD1D84FF1001B75411D3AA964795F54E4CB39B1AECE32C1B188E89C1BAE6300888127957F403580A20D36EA823A7492F9D91D90F300884AEBC7001017EF2ABF447580A20B36EA823A7492C9297D54E4CBE13C00A2D27A3200C4494619A47440F580A20736EA823A749249314745BE1CCE03202AAD2B03401C2D512E413581A20536EA823A7492DD3AB02F2CF2E59CD9685A5DBB36C2E311A55D670680B83AA25C8DEA0245076CD40575E82493097D27D7A90F3F28F4E54C1EC3CB0044080340EC4D522E46F5816A0F36EA823A74D2BD35A7332CF2E59CDA6016CF1EA0E311A519034022EC527E8A6A04D5166CD40575E8A47BEE911EB0C85772744D9D3572481F784CA2B46200488CE3CA15A84E50EDC0465D50874EBADB6FF23E0FC06EDB4B9D8A2162D49DBDADA183FB1279D2D8A73FFC5CC6150340A2C87E022315DE251011B05117D4A193CEAC1F503CA58F8A3B51351C5E5967AD9AD1C56AEC1BFF30C00090488B94EFA19A41D5051B75411D3A0D36CFF33E0F804837596972F86DF1BEACC40090581F2B5C38A8C660A32EA843A7C18CC7FCCD0320D22DB7A5A37553637C579B640048B4334A03AA1D541DB05117D4A1D3E0AE41C1E60110E9B46C5A57F8398D0306805418AB5C886A08850B36EA823A741AD4370CB0CEBCA53ED8603026AA36390BD0AB473CE7033000A4C61AE507A88E507860A32EA843A7C5B697380F80A223AE9701180052E523E567A896503860A32EA843A7C5CC27380F80A263603F06008A85A3CAEF513D21FD60A32EA843A7C5DDB7A8810B0CC444B5D0A7172F01506CC8AE8275A8A6905EB05117D4A1D342E60134731E004580AC0B803EA371C000906A43515D217D60A32EA843A7C98EF99DE0804C544DB22810FA7CC6010340EA4D517887404860A32EA843A7C99C71DDE1804C544D1346C777B749060052962ADF45358682818DBAA00E9D26B20A1B1A9089AAE9C6FE5C0888624F7614BC14D519F20F36EA823A749A74EADC686537AB0F2F1894BD92FD05B6CCEB6CCD7CBC87F5F8C85E25C92A84AB9FEF627DB95ECFE3B63AB6B6CEDA38BB73F1F8E871DD9AFEE8B9E7D7B4AA0E3E8E5F0756D45B2BA677B5A68C09F6FC668FED5E7C9F75BF7F4756D7159F9F6CF4841EB7959C35924B476735CD1F91C7459FCDB86000209B03CAAF50AD217F60A32EA843A7CD3B0B82CF0358FA5C57ABA1133E7E29B2F08BAC00878EE7951CA7B30A33E871FCEADCA5D17A637257AB001ECF0B59E466EE93DD8B932ED1E3F8D5B37B633108A0C7F422BFB5A3F5CAD3DDACAEEAF5A2C729A55BD7462D7B4AAC9919DFEBFF8201801C645BE1DFA27A43DEC1465D50874E9B79E383CD03983C26D8F5DB8593BAC1E3BA25DF58D1717599F270B0F512C60CEB0D8FAB83A9BC36B176EF9F3CBE8407745CB7263E18DFEBFF8201808053CABFA39A43DEC0465D50874E9B7B87F89F0720670FD031BD906FC6BB1736C0E357B2E7F586621142C7D5458EBFE3657F6749AA31BB5DCE7CF8BD5C21974CD031BDA8AB1F607DB1BC1E1EDF8D4103E27BFD5F3000500967953FA3BA43EEC1465D50874E1B39D52DA7A9D1E05CC9A83BF57CBB7DEAFE9EF0F895487841C7D3CDEF64C96A2D6F2BB3E8D1E35772EF1D7ADE3FBF7FBFE36BE37DFD5F3000501959E55A547BC81DD8A80BEAD069F4FE6BDEBFE166377784C7F2A37BB7C6E2B568F438E5C8EFA1E3E9D6B56BA3E7B90032490F1D2B0CB7DDE87D774799B4A96B5E82CC47408F51C9FA17E27DFD5F3000500579A52BAA3F54196CD40575E8347A7982F7EBB872D6001DCB2F99258F1EA7149D01C48D432BBD3D3F1D9747DCEAD7BB3F7C0EE548E843C7F2A3670F7F0120E8FC9128600020170ACA005483A83CD8A80BEAD06974FF9DBDE1005D8E7C2346C7F2CBEB7576F9068B8E13967D4BBD0500D96D111D270C7226043D8772563DAFEFDB77EF9EDE0388B875605F78BC386100200F86A03A44A5C1465D50874E2339C5EDE7147CFF3EFA36705932D5FB2D81722B1A3A5618A21C006EF57109406E4D44C7F2E3CE41DE1FFFE47A33F4099CD5C000401E8D42B58830D8A80BEAD069F5E122EF33F19F18D90B1ECB8FE71FF77EBBDDED3755EF1B64940380DC6A889E4339E347E9FBDBC9E249E831CAD934A77AEF4F981800C80786009760A32EA843A7D58267BCCF03908572D0B1FCF053441EBE27BC7BEC9DF62FF31A00AA370760DA23DEDFBBB1F7EA0B009BE77A5F1028ECF51BAA8501807CE2E5001760A32EA843A7D58343BD7F8BFC6471033C961FF37D4C449CF944758A88CC96CF6C529D163C87520EAEA887C70AC3621F974F741560B37E80AF658987DC1CFFEBFF82018002E0C4C00A60A32EA843A7954C242B789C0720130107F4D5330F409684458F518ECE99ECE5C86435F4F895C8EC78743CDD643F7DF4F8E5BC3AB11B3C9657C36E5505101CBF1C99C029C1011D2F6E18002800B93B80B70896011B75411D3ACD3E7EC3FB3C001D854416CCF1B31891CE00528E9FB31362EAC3E19FA1B86BB0BF70224558F66340C7F462ED2CEFC14DF63040C78A2306000A48D609E0624125C0465D50874E333FEBF2CBAE70320B1C1DCF2D3FD7905BC9FDF6BA37DAB11BAA0AAC9F3B2484BC370343DCEA5636F0F96C89B7B9097641E7708CBAB38FAFE0263B36A2E3C511030069202B0672D9600036EA823A749AC9A43A34605772625D9D75878F6BBA72D941D6A347C7F462C5F42EC55B19D1630471DF1D7D8ADBD5A2C7744BEE1E081A9010B9F7FEED9782BF778BA7742D6E0B8D1EA31C5946D8EF96C072D6021D338E18004813D93B801B0839C0465D50874E33B966ED77FB5B993F20FBC98F1CD2C7EAA38A133ABE90BD07646D7D99C077C8C7B5EB52A450CBCA721244826C0DDCD8A77F31082D9F167C2BE056720661D1B3DD8A132DCBBD379548C8B9FB16F5DE3DDEA3780A1F3D961F329953EE2490B31DE5B67596751F64FF8720A1ED8C0A0D619EB1A9360600D2487611E456C236B05117D4A1D3EED3C5FE4F29135552CDDB23AB810180343BAEFC0AD5AB34828DBAA00E9D76725D180DDC443ACC7A42DF0A8451C000402138A05C8A6A56DAC0465D50874EBB4787F7820337910E72DB20FADCC5150300856497F25D54B7D20436EA823A74DAC9356A34701305D5BCC92C3BC7208E180028444B950B51ED4A0BD8A80BEAD0E47DDD7B223776BE92ACEBFF820180423605D5AEB4808DBAA00E4DFE76E623AA64CEB8645DFF170C0054054351FD4A03D8A80BEAD034A0B8510C1AC0898218717BB2AEFF0B0600AA923A54C3920E36EA823A340DB0FAF5E63C00D22BBBD9F4B5E050D4310050956494DFA33A9664B05117D4A1E99C03CB390F80F4797741F2AEFF0B0600AAA2A3CACF502D4B2AD8A80BEAD074CEB2699C0740FABC383E79D7FF05030055D947CA0F503D4B22D8A80BEAD074CE93F7711E00E923FB2AA0CF59DC3100500DAC5152717B206CD40575683A47B6D94503399157B26360972EC9BBFE2F1800A846C6A29A9634B05117D4A1E92B87356ED6737C6D5D711319D95BBFD52B4F77B3D6CDEA12DABA030757D45BAB9FEFD2E6312B919FDFBFAC5EDB4640AD64B324D9BA57364C428F5BCA86D95DACA36BF4FD1D5AC9EB93F77DEDCCB6EF8FFC4DB6BED859EB66431F2C6C809FAF246000A01A6A40752D4960A32EA843D35756CDE80207742F64A7B9DB6EACBCFDABEC10F8B9A6202013CE06F60BB60FFFCD03FA59BB55E142C7F76ADB4B9D8BBB0CA2C771EBF69BFA06DAFBDF6EF7A2868ADB379BF5E7FE268757050F1F122CD06324010300D5D019E597A8B625056CD4057568FACA84D13DE180EE967CE3F772EAB7A153A3F5DEAB9DE0B1DC7A6D6237AB4E152F747CAFE438CB9E0B361972EE93FA26BFC936C7F28D1D3D8E5B7246C104C72EA577CFFE81FF26A3EFEA0D8F9D040C0054631F2BDF43F52D0960A32EA843D3576EECDF0F0EE86E1C5B5BE7EBBE6FD98BC0EF296FF986ACABF8B792A22B9704D0E355F2FE6B9D8ADFA4D171FDEADAB5D13AE6F3FD91332C5D7D5C8B97BF4966931A6CC0312BC96FED687553CF191D37091800280216291D508D8B3BD8A80BEAD0D4D691D5FE8ACDCCC77BC0E3B9F1D253DDE1312B09EB9BE643C37AC3C7ABE4AE41952F7DF8E1F7CCCCB30FF584C773E3F567FD9D09D9F37A72AFFF0B06008A8891A8C6C51D6CD40575686A6B8DCF53CE7D7AF9BFE62DD7A7D131CB914976BABFFDB7F2B343A2EC7C57DF808F17949F951AE5FD09F237913908E8B895BC3A31B9D7FF050300454441B902D5B938838DBAA00E4D6D4D7CC0DFB7CD9E3DFC9FF695D3E6D9CDF8B8A59C5857078FA58BD7D3EE3281101D4707992B811EB31C59D9111DCBADC137F8BB1CF4D0DDC9BDFE2F180028428E2B3F45B52EAE60A32EA843535B8306F81BF87BF50836EBDDEB7577B9BE8D8EA38BD75B1565E63F3A8E2E7286013D6E29DB5F0EB614EFAD03FD9D95E9D13DB9D7FF05030045CC2EE56254EFE20836EA823A34B527F7F0A301BE1C090EE8586EBDAD0A283A6E2972DF3A3A8E2E510A00DDBB793F03B0784A57782CB746DEAE0A1D386E397BDF4CF6F57FC10040113409D5BB38828DBAA00E4DEDAD7FC1FB3C80A747FB9F7026DE98EC7DD259D0B30EE5442900F8B91E3FE331FF933285ECE58F8E5BCEA249C9BEFE2F180028A2AE46352F6E60A32EA843537B93C7789F07B0627A17782CB7A63FDA031EB79CA183C399752FA214001EB9C7FB3E0DF23BE8586EED98EF7D2D80A08F19070C00145147944B50DD8B13D8A80BEAD0D49E9FEBBF075604BB263FE941EFA163DCBDE1159C280500F9368F1EB39C20B748CADD0CCD6FA941051CB714596A38CC333251C1004011B64489F5FA00B05117D4A1A93D5939EEE47AF581720CF295C8ED6AE8786ECC1EEBFD94B3CE55F7ECE4AE84331E0B60989312974CF57E7964DC7DFEC3D1D05BBC07405994091D2B69180028E206A1DA1717B05117D4A1097B6B8EB74979E2A9FBFDCF0378738AF72217D6A633B22F007ABC72E41B70182BE04918F3B3495390390012ACD031CB91BF1F3A56D2300050C46594D8EE17001B75411D9AB0E71EF17EDAF9E337FC15645943C0CF6E745274839C7528C5CF297711E45B772932CF013D5625B233A2AC1F808E598E9CFEF7B31AE4E323927FFD5F3000500CBCABFC03AA8151071B75411D9AB0213E56E713E347793F0BB0705237782C37E4F6419DEBEFCBED8C7ED7C197C58964331D745C3F645F020955E8B1DC78DEC7F2CCB2A4333A56257D03AC3A18270C0014134FA21A1875B05117D4A10993A2EAE75B796E4B476BCC307713D06429DF17C7772F2E20838EE596DCF3EEE7DBAE93AC7E17748B62D9496F40DFE0C55036F1F1733BA69DFC2DE44C0E3A3E22B772CA591574AC7264112774BC246200A018B91CD5C128838DBAA00E4DA56D99E77D1E40AB4D733B5BC36EED03B70796D3F68F0EEF55DCC71FFDAE1F52841EBEA777711D7FE7E39523A7BCE56C87DCF79EDDAC3A0D38B65767369AD63455786F19D8CFF37E058D7DFA5BE347F5F2BD2913227F470965E8EC84CC5BB8EF8E3ED63B01FE164B9F4BC7F57FC10040317248F921AA8551051B75411D9A4AF37B2DDC4EBEDD37ADAAB30EAD3C47B60D463FA7939C8A6F7DBC4A7415FD52CEBE65C2C745FC9C71F14A8245EBE3C9FFF6F38DDF29CCDB31A386018062662EAA8551051B75411D9A4A93ED6DD1804F6427672DD0E72789180028860C540FA30836EA823A349526A7C7BDDE0F4FE922771BA0CF4E523100500CED53BE8B6A62D4C0465D5087A6F264853B34F0138915D3D373FD5F3000504C4D4035316A60A32EA8435379339F083E0F8092CBCF6D9F71C600403155507E87EA6294C0465D5087A6F264263F1AF8291CD95D43ACD31F3C631DDFF38A7574CF9BD6913D4BADC37B5658873E5C55FCFF47F62C2EFEDBC90F67AA9F7BCA6A7EEF7EABB0CDFB2A8ABA0CEC176C1BE8B86100A0187B5FF906AA8D51011B75411D9ACA6BE834C06AF6B9300E5556D8D6C33AF5C1C462A13FB07BB5F5C507AB3C93DF936020C1A1B0DDFBA64A7EC9DD1DE83393640C001473F7A1DA1815B05117D4A1A9B29D3EB686A5F29ADFBDAF58B4FD167DE4E0876BACC31FADB34EEE7DDDCA7D305A3D4EB8B75CAE7E3ED816D071C400403197557E81EA6314C0465D5087A6CA64911C5400C83B3965DFB467192CE07E34EDDD609DD8B7C53A7378A7D57CE49D363207575BF90FEE85CF4387671E48D7F57FC1004009F096F23554236B0D36EA823A345536FC36CE03082AB7F306F58D7F092CE27E1CFD64A375E6D08E76451FC91C586EE5DFBB033EAF206E6A4CD7F57FC10040097113AA91B5061B75411D9A2AEBD4B931F415F392ECD4EE67B59DEA9753FC6E0BBF536EEFB396B52DD85E07AD8EAD49DFF57FC1004009714AF96754276B0936EA823A34B91364ADF8B42A6CEBA6F55BFFF1CF37C3C2EE45E68BA5566167237CBE5EAC9B95BEEBFF820180126406AA93B5041B75411D9ADC99375EEF3C00598B7EE3ECCED6EE850DC5F5F2D1CF0495DDDCD1FAE0B5066BAD2A56A5BCFF5AA750EE72C8A9227BF8C395B090FBF1E5176FC382EE4BD3DB56E1DDDBE0F376EBD987D277FD5F300050C2FC0BAA95B5021B75411D9ADCB9F78EE0F300F25B3B16171672AE1D2F5B0F8FBDB757F1B432FA3DAF0EACA82FEE70E7768B60D9B16FD49D7D5428D1737AFCF3B577A8A2AD6F76FF499DC5BF55D3F640F30264EB64F45E261D030025CC46E5EF51BDAC05D8A80BEAD0E44EE72E8DC5FDE5513170E3E43AD3BA77481F78EC56DDBB355A9FBCD9007FDF2D39AB205BDCA2E357D255FDDEE6005B208B0DF387A8A2ADB1F8EFDF8A0BB80E4D3BACFCFB43E1EB2847765B34C1FB97060C0094406AD0C535B3DA60A32EA843937B723A1D1504371E1AD61B1ED36940DF7EC5B0808E51C9D1357556171554D071DDEAD9BDB1781C74FC4AF6AD1A60ED8F4BF16FA54240E19D41F0F59422210BBD7769C0004009B45FB918D5CC6A838DBAA00E4DEEBD3CA11B2C08957CBEB4DED337C6979EF237DF60C2683DD7A5657F7B74FC4A3E7B67392CE47E9CA846F16F9139B4DEB2B6BBFFDB4E79B8077CDFD2800180126A24AA99D5061B75411D9ADCBBFFCEDEB0205432D563C1B8FDA6BEF03895F83DF5EFD4BF4F7F78FC724EEE9E0E0BB91F473ED9080B7598B2FB16A8D7A10602F0DA9CE4EF83DEB7346000A0843AAB5C8AEA6635C1465D508726F7E41AB94CE44345A19C09F77BFF667E7AA3FA508263952233FED171FC92EBDCE87190EC3BB76BBBCF5F8E7316ACEA570DB98FC6C2D767F7E57AB3386913BD6769C00040093607D5CD6A828DBAA00E4DDE7CB8C8FB3C003F4BC67EFC86B7C791EBF6E8387EED5BEAF28E806DF5D6E13DFA6EF7ABCA75FF529AB659851DE52F7F6C9E9BDEEBFF82018012AEA65B06C3465D5087266F163CE37D1EC08CC7BC5F33968966E858A5C84A853A67A6BB0D00A7764F8585DC8FA68FD7E3C25C45D94F67C1D7D96AFAA3E9BDFE2F180028E1B62A1D50FDAC06D8A80BEAD0E4CD83777B9F07E067D6F86B13BD070DB9768F8EE587BB005057DCA71F1573AFE4D43FDAD0A716CADD1570C7CDE9BDFE2F18002805D4FFC135346CB05117D4A1C91BB957BFE0711E805C4F47C72A67C9D4AEF058E5C8A645E8587EC84A85E831EC4E7FF0142CE67EE858E65797ECE7F3E1EB3DB3D12C2E9A84DEAFB46000A014D8AD5C806A68D860A32EA84393775EAFCF8B4103BCAD1CF7D61CEF0BF2E8BA3DAD1872C0F19D745DFB2F4EFC6BDA058B716DEC827301B6BE98EEEBFF82018052427D03C375344CB05117D4A1C9BB8593BC9F9E7F717C77782CA4BE41BE817B5F96F7DD059DE0F1BC1A71BB1AE4C1F1EDCEBE3F0616733F8E7DB60914E1DACA7D34AEDD6B9EF978BAAFFF0B06004A89BDCA85A88E860936EA823A3479F7F03DDEE7011C5B5B57DC56181DCFE9B9477AC063542297266EBD31D8356A9948E866E7C3637B5E85C5DCB3085DFBB7CB1C5CDBEE350FBD25DDD7FF050300A5881AE8712D0D0B6CD4057568F2AE578FFEAE4E913BC964C04AD790E552C1A90DEAC3077EDF0DD908A84777FF0B02C92C77745C275D93FF8ED660D11FB70AEFDC7CFEF536BF6516CFCCA0F72C4D180028453E57BE816A695860A32EA843933F9F2EF6B773DEA6B99DAD81FDF07C80F1A37A160B0DFA3D2F3E5B525F3C8D8F1EA31459E468D973EE261E66770D82C5DC8FD38776C0E21B05B93D8F9E7FCD3B5ED6737925EE180028656E40B5342CB05117D4A1C99F37267B9FA5DF4A56ED93B3012F8CED6E4D7CB0A7B57C5A576BFF327F81A21C39952FEB168C1FD5CB7A7C24266B14AC7FA14BF112053A06A2EBDEFFA6BD1B60E18D8AEC6773CFBF66F95BA1CF41DA300050CA1C50BE89EA691860A32EA843933F8F8DF0B7614E121CD9B3181674AFBE0C639F7F8D3287369E7FCDF768BCC532CE1800288506A37A1A06D8A80BEAD0E44F9F9EDE37CC490A1DB7FF45EFD63FACB0A3B795D9645A0D9DF46CB414770C0094424DCAB7514DD50D36EA823A34F9E77ABDFC8439F861F08D7FA27EFABF954C04944B29E8EF9F460C00945277A29AAA1B6CD4057568F2CFCF6A7D497060372EEA5ED474D31F0FF2EFDD69CD7D92D7FF5B3100504A1D51429F0B001B75411D9AFC1B7B6FFAE60114B6F78405DDAB389CFE17F90FEEB3460EE1F5FF560C009462FD505DD50936EA823A34F9D7AF77FAE6016477DD060BBA178723B0EB9F5B99DD8F589D5D2EE094060C0094627B94AFA1DAAA0B6CD40575680AE6C0F274CD03C8BC330C16752F8EEFDB028B6D147DB1753CFCBBA7150300A5DC55A8B6EA021B75411D9A82593C255DF3007404805307B7C3621B456F2F9B00FFEE69C5004029B70ED5565D60A31686F95BD4A12998DB6EEC0B0B6552E9080071B9FE2FE63E3F09FEDDD38A0180C8FC2DACB11AC0462D0C731EEAD014DCBA595D60B14CA2A001E0D09EB5B0D046D5F429CFC2BF795A31001099F3608DD500360666983F51F2A8435370B2CBDFCE572AEFA097044103409437FF411800DA62002032F3CA4F60AD0D0836066698E3E489A30E4D7AC82E7F13EEEF595C1CC8CF4E81711134009C88D10440C100D016030051D138586B03828D8118E6F795D3F2A4518726FDBA7469B46E19D8CFBAFDA6BE8933F5A9076161772B4E13000503405B0C0044455253BF0F6B6E00B0311059C2B0E549A30E4DE4C598D163606177EBCCE19DB0D0461503405B0C0044E7695F1E1836FA66985F57643BC3E213461D9AC88BA001204E7700080680B6180088CE93DAFA75587B7D828DBEC9A205B6278C3A349117410280EC00888A6C943100B4C50040D486D6858160A36F86F9BAFDC9A20E4DE445900070F0C335B0C8461903405B0C00446DBC0E6BAF4FB0D117C3BC5429D89E28ECD0445E040900873E5A078B6C943100B4C50040D486D4D84B610DF60136FA6298C36D4FB2087568222F820480C30C00B1C70040D4CE7058837D808D9EC98E4586F9B9ED0916A10E4DE4050340BA310010B523B556CB2E81B0D133C3BCDCF6E4CE431D9AC80B068074630020822E87B5D823D8E89961CE773CB922D4A189BC60004837060022683EACC51EC1464F0CF31225677B62E7A10E4DE4050340BA3100104152732F8135D903D8E8896DE53F27D4A189BC600048370600A29202AF0C081B5D33CC0ECA5EDB136A037568222F1800D28D0180A824A9BD1D606D760936BA6698FF697B32EDA00E4DE4050340BA31001095F59FB036BB041B5D33CCE98E27D306EAD0445E3000A41B03005159D3616D760936BA62981729276D4FA41DD4A1295A1A3A355A03FAF6B386DDDAC71A7A4B5FABB16F7FABBE01FF6C2D3000A41B0300515952832F8235DA05D8E88A615E697B1210EAD0545B0D9D0658A3EEEC6DBD31B9AB7568659D65BDDDB19D82B27F59BDF5DAC46ED6F0DBFA5875F5F858D5C000906E0C0044155D096BB40BB0D115C39CE57812EDA00E4DB561AA22FEF4E89ED6D135B8E897737045BDF5D8885EF0B861630048370600A28A66C11AED026CACC830BFA99CB23D01087568AABEBB06F5B53E59DC008BBB171F2C6CB006DFD00F3E46581800D28D0180A822A9C5DF84B5BA02D85891615E637BF0925087A6EA7AF2BE5E5676B3FA7B8082EEC7998DA63566586FF858616000483706002257AE81B5BA02D8589161CE753C38843A3455CF8BE3BBC3221E94CC1198FE680FF898BA3100A41B0300912B7361ADAE0036966598DF52CED81EB824D4A1A93A26DCDF13166F5D240454E34C000340BA310010B92235F95BB06697011BCB32CCEB6D0F5A16EAD014BEBB6FE9636537E3C2AD935C0E087B4E000340BA310010B9763DACD965C0C6B20CF365C78396843A34854BEEE1975BF850C10E834C0C44CF431706807463002072ED6558B3CB808D2519E6779466DB0396853A3485EBD987C23DF58F84798B200340BA310010B926B5F93BB07697001B4B32CC7ADB8355843A3485A773E746EBF85AEFF7F90725EB0484B558100340BA31001079520F6B7709B0B124C39CE378B0B25087A6F03C7C4F6F58A0AB41560C44CF2928068074630020F2640EACDD25C046C8302F508EDB1EA822D4A1293CAB667481C5B91A64D960F49C82620048370600224FA4465F006B38001B21C3FC57DB83B8823A3485434EC17FB95EBDEFA03857834C3C44CF2B28068074630020F2EC5F610D0760236498231D0F5211EAD0148E7EBDFBC3C25C2DB22E4018BB083200A41B0300916723610D07602364989B1D0F5211EAD0148E2137F78585B99A642B61F4DC8260004837060022CF36C31A0EC0C6760CF3874AC1F600AEA00E4DE1786068ED2600B61A7A4B5FF8DC8260004837060022CFA456FF10D67207D8D88EC7DBFF5AA10E4DE17830020160D8ADFAEF04600048370600225F5CDD0E081BDB31CC198E83BB823A3485E38E085C0218D057FFB2C00C00E9C60040E4CB0C58CB1D60631B86D94169B21DD835D4A1291CFDFBD47612A068E8D4089F5B100C00E9D6850180C80FA9D91D604DB7818D6D18E66F6C07F5E47A537F41204C66E09F794BBDEFA03057C3A19575F07905C500906E5DAFED05C71622AAE837B0A6DBC0C6360CF36EC7415DBBF67AFD93C2A8B4F52FD46E21A037267785CF2928068074EB714D0F38B610514577C39A6E031BDB30CCF58E83BA76CDB5E1EF174F5F7962A4FAB6048A73358CBA339CBF350340BAF5BEBA1B1C5B88A8A2F5B0A6DBC0C6F30CF3DB4ADE76404FAEBEA627ECD4148E6E5D1BAD531BD47B0F0A74988EAEA9B31A3AE1E714140340BAF5BDB20B1C5B88A822A9DDDF86B5BD056C3CCF30FFC37630CFAEBCBA3BECD4149EE71FEF018B74989E1E1D5ED0630048B746A3131C5B88C895FF80B5BD056C3CCF3087390EE6C9154667D8A9293C9D3A375A8757556F4BE04F16375866485B010B068074BBF1CF786C21225786C1DADE02369E67984B1C07F3E4BFFFB123ECD414AED177F5B60A5B71C1D629BBD9B4EE1A1CEE444F0680F4FADB757DAD417FBA1E8E2D44E4CA1258DB5BC0C6A273DBFF9EB21DC8B3FFEF0FD77322608DCC782CFC4B0113EE0F7F8E0703407A1957756300200A466A78C9ED8161639161FEDA76105F2400FCE5CA706E0FA3CA964CED0A0BB70EAF3CDD0D3EA66E0C00E9F5C7CBEB19008882FB35ACF10A6C2C324C15C1E1C15C9300F0873F7145C05A9233013A2F07643777B49E79A07A77773000A4D3B5D7F72B8E1F0C0044810D80355E818D458639DB7110CFA403F33240EDC99C001D1303F72FAB0F65C39F721800D2494EFF33001069311BD67805361619E67EC7413C6B0D007FFA733DECE4543D727780DC22E8679D80E36BEBAC671FEA595C6E181D3B4C0C00E9735DC7FEC509C40C00445AEC87355E818DEA177EEC38802FAD01407051A06890C58264C5405936B8DCDE015FAE37AD5533BA580FDFD3DBEAACC2033A56353000A4CF154697F3E306030091163F46B5BE5D439161D6397ED9177B00F8C39FEAB83950C4C8377AD94550B6127E70686FEB016588FADFFD7AF7B7EA42BCB7DF0B06807469BDF6DF8A0180488B3A54EBDB351419E6538E5FF6C5DE9105570624AF1800D2E5CF7FE9D466CC600020D2E22954EBDB351419E676C72FFB62EFC8ADFE765D75279151BC3100A4877C41708E170C00445A6C47B5BE5D83FAC10B95ACED177D73766621B7055ED7B11F1C00889C1800D2E1AF7FEB0DC70B0600222DA4A65FE8ACF76DFEA3C8307F6EFBA540508716725700E703901B0C00C927D7FD5B67FD3B31001069F37367BD6FF31F4586693A7EC937D4A15BFDF92FDC28882A63004836B9E5EF8F97D7C1314230001069A3FE4FDB7ADFE63F8A0C73B4E3977C431DDAEEF22B1AACEBCDFE706020120C00C9259BFDC825413436B4620020D266B4B3DEB7F98F22C35CE8F825DF50877692F47FEDF5E1EE2847F1C500904C7FFD5BAF92A7FDED180088B459E8ACF76DFEA3C830F7397EC9B7FF0E3A342203814C02420305A51B0340F2A0D9FEA50CFE534738B6109167FB9CF5BECD7FA81FF8BEE31702B9E68FB8539722F3026442101A34289D180092E39A6BFB142700A3BE5FCA080600229DBE6FAFF9CE007099E38703E9E53100B4922D84393780040340FCC9253EE7023F6E8DBB9C018048A3CBEC35DF19001A1D3F1CC8ED2ABDA34EED865C16901DC164A2101A54281D1800E24B7601B5AFEBEFC7CC3F33001069D468AFF9CE00F08CE387037950A577D4A9BD9299C2725680DB0AA70F03407CC8DA1E575FD3AB7829CFCD043F375E670020D2E9197BCD7706808D8E1F0E6492A600E0248140AE25CA69C5BF5CD9A538A98892E9BE91A3607177238E0160F23313E0FB103572764EBEDD5F7E45A7625FAC743B9F5F9BAE600020D268A3BDE6DB8B7F07E5B4ED07035BAE3A2FEAD4446E0DBBEB6E58DCDD88630078FAC927E0FB9046721751D35FF0D84244BE488DEF8002C08F6C3FA4C529E54F8E4E4DE40503407A0DE01A004461F8110A00BF73FC901643024C04246200482F4E00240AC5EF5000E8E4F8212D5E519D18756E22371800D2EB139EFE270A43271400EE76FC901687552776BB2220911303403A75FD234FFF1385E46E1400A6387E489BBB7819807C620048A7393CFD4F1496292800AC70FC90361FFFC5B4FE003A3951250C00E953AFBEFD67C03842445AAC4001E053C70F69F570486B0250B23100A4CF627EFB270AD3A76D0380615EA8E46D3FA09DCC05F833E8EC44E53000A44B1FF5EDBF00C60F22D2466AFD85F600F063DB3F86662ACF0290470C00E9219385B772E53FA26AF8B13D00FC9BE31F439157826C1044E9C300901ED379EA9FA85AFECD1E00BA3AFE31345F2A728B0F1A00889C1800D2E15EEEFB4F544D5DED016084E31F43F5D95F4CEB2A300810393100245F3FF585A0198C1344149A11F60030CDF18FA1DB7C45474E0AA48A180092AD41157F99208CC608220ACD347B0058ECF8C7AA785F75FC8EBC1C4065300024D74D7FBADE3AC6E24F540B8BED0160ABE31FAB46B6FB1CC0104025300024D398CB3B5A59301E1051556CB50780501701AA44AEFFDDCFBB0308600048165911741E67FB13D55A7131A0D60070DAF18F35F1D6151DADDE3C1B40360C00C971B70AF9DCE18F28124E9F0B00867991E31F6A4A56017B537D43A8631020850120FE06FEE97A6BC715B8BF1351CD5C2401E012476324C86581052A08DCA6BE35FC090C2A940E0C00F174A522F7F6AFE3CA7E4451758904809F3B1A2347160F5AAA0692916A40E1FA01E9C200101F72D6EEF1CB3B162FE571821F51E4FD5C02C0658EC6C83BAD7CFE17D3DA7E85692D57838D4C2A9A4989B47CD83058DCDD886300D8F2D458F83E44C92C65BEB25AF5BD77541F3CA0FA220B3E51EC5C2601E06A4723516464EEB907167737E218007213C6C2F7818848B3AB2500F4743412450603001151287A4A00B8DDD14814190C004444A1B85D02C0438E46A2C8600020220AC5431200263B1A8922830180882814932500CC76341245060300115128664B0098EF68248A0C060022A250CC9700B0C8D14814190C004444A15824016099A39128321800427455BD95AFEB61656F1C6465EEB8D3CA77EF8B7F8E889268990480B58E46A2C86000D023D77FA0756AC644EBD8FA85D6E19DCBAC03EFE3F7EC80725039FCCE0AEBE8D6C5D689652F5ACDF7DFA7C2023E2E11C5D65A09009B1C8D4491C100E05FF6A6C1D6C957675A87543147EF8F17073F586D1D5BF79A75F689872DEB9A06F87844142B9B2400EC7034124506038077671F1A6D35ED5A0EDF133F0EEC5E6D35EDDD601DFB6C937562DF16EBD4276F59B9C9132CEBDA2EF0F1892816764800F8C0D14814190C00EE9D1D39DC6ADABE14BE177E1CFD64A375EAC036F83C4566EF5B56FEB1872DEBEA7AF87C8828D23E9000B0D7D14814190C0095E5BBF7B38E6E5808DF033F8E7EFA9675E6F04EF8FC90CC3BABADFC8811F0B9115164ED9500F085A39128321800CA3BFBE843C5EBF3E8F57B7568CF5AEBF4C1EDF079B9915DF08265FD95F3038862E20B0900471D8D4491C10050DAC9F9D3E1EBF6434EF79F6DDA059F9317994D8BAD42E75EF0F91251A41C950070CAD14814190C00C0D50DD6B1B5AFC2D7EC879CF247CFC5AFCC9E8D56E1C6C1F8B91351549C920090733412450603405B85EBBA584D3B96C1D7EBC711F5CD1F3D8FC00EEEB0F2C387C3D7404491906300A0486300B0B9B2CE3AB2E90DF85AFD08ADF8B73AB0DD2A0C1C845F4B0D4988E29D0B44E702002F01506431007CE5D8E279F075FA7164EF06F8F8BA653EDC60153A557F4E802C6B7C66D293D689A52F5947B6BC595C0CE9E0FBE7563A6C7D0F64F2E4E1F7565A47B62DB58EAD5B689D7C738E75E6C9472DEBDACEF098440953BC04C0498014590C00E79C19FF187C8D7E1CFE783D7CECB0C8C440EB9A4EF075E924CB1D7FF9F274EB70C0459064E1A3A32A349C7EEEE99A8417A22A294E02E46D8014590C00AAB00D1C542C4AE8357A25C7397368077CEC30655F7DA1DDEBD2E5CCD847AD260DCB1D9FA7DE2309497289E4F8A79BACB3AB1658851B6E868F4D1463C5DB00B9101045160380691DD138E9EFF8E79BE1E356437ED8B076AF2D88B3634607FEB66F2705FFCB2FDEC6B74336A910B370B655E8DE0F3E17A2182A2E04C4A58029B2D21E004E4D1E0F5F9B1F876AFC7E6476ACB4ACABEADABC3E3F72370CB28E6C5D0C5FA357C5D3FD5E563E3CB8C3CACD9A625975DDE173238A91E252C0DC0C88222BD501E0AF0DC5896BE8B5F91164953F5DF2631E6CF3F7F5EAD48C49DA2E87C8E7C3F7E590BD9BACC2E0DBE073248A89E26640DC0E98222BCD01E0D4AC67E1EBF24376F2438F576DB24890F537EFB3EC0B7FADB78E6A5CFC48CBA51059EB60CC03F0F912C540713BE0B58E46A2C8486D005045F290A635FE0F7EB846CB32BFBAE49E19D7EEEF5C4EA1534FADBB1C9EDCBF153E2FBF7233276BB9B44154656B25002C73341245465A03C0E9A913E06BF2A39613FF90CC471B2DEB4AFCF776CAF51B681D7A6F257C5D7E9CD8B7053EA7A0B22B16F83AB3415443CB24002C72341245465A0340D3F625F0357925D7CBA3F4EDBF55E1A6CA7B05E4D537FF3814FF56D937E6B90E364411B04802C07C47235164A4310014CC6EF0F5F8A17BA31F5D72D326C2BF77ABC25F1BACC33BF49DF6AFD65990DCE409F0F51045D07C0900B31D8D449191C60070E6297DABFEB9BEBDADCA323B57C2BF77ABE34B5F82AFC78FAA4E806C7AC7CADF3B12BE26A288992D0160B2A3912832D218008EAD7B0DBE1EAFAAB5DEBF5F855E8DF06F7E76C213F0F5F87168CF5AF8D8A1FA623B570EA438982C01E02147235164A43100E89AFD7F2A02F7FD97937BF2F1767FEF7C8F7ED6414DF7F98B5ABD0799775673C7418ABA872400DCEE68248A8CB40580ECD4A7E16BF14A6EFD43C78F92ECDC69EDFEDE47372E82AFC78F63359EFF907BECE176AF8F28426E9700D0D3D1481419690B006717CC82AFC5AB5A173F37B28B5F6AF3B76E1E3A14BE163F0EEE595BF3BB1F327BDFB2ACEBBAB4798D4411D25302C0D58E46A2C8485B0038BD5ACF6A77513FFD2F326F2D6EF3B7D6B5BEBFF8F2C036F898D5C6BB0228C2AE96007099A3912832D21600BEDC167CE7BF389CFE1799DDEBCFFF9DB3C386C1D7E247A4263FEEDF565CC9D0FE99268A88CB2400FCDCD1481419690B002776AF85AFC58BA8DEFBDFCEA19DE717CE39BAF90DF85AFC88C2A64776B9293C0B4091F473090097381A8922236D0140AEDDA3D7E2C5A9889CFE7643B6D5CD37F480AFC38FA68FD7C3C7A9A5CCB6E5F0B34D5463974800B8C8D1481419A90B009F6D82AFC52D59FA171D37AAE4F4F8E989E3E06BF1232AD7FE9D0ADDFBC2CF37510D5DF4779665490838EDF807A2486000F0266EAF5902C0D10D0BE16BF1EA50845F7B6EECA3F0F34D5423A7A5F6B706804F1DFF4814090C00DEC4E6FA7F8B42A75ED6414D0B1FE9DEE657A7EC9AD7E0E79BA8463EB50780AD8E7F248A0406006F4E44B808426346C3D7E155E4EF7C38BCD3B2AEEF0A3FE34435B0D51E00163BFE9128121800BC89DA0CF84ABE7CE579F83ABC92F70D1D3F4A6AB23FC095A655A8EBCE6D8AC969B13D004C73FC23512430007810B30980E2F8CA57F06BF1280E0B1FE5870D839FF1C0AEAAB73243EEB4BE7C719A756CEDABC56D940FBEB7D23AE0788F0E2A87DF59515C70E9F8EA05D6C957675A996177E36352D24DB30780118E7F248A040600F76AB2F35D403AEEFF8FCBC247F947C7C0CFB82F57D559671FBCDF3AB6EA15EBC0FBF87D714B82C171759CE6310F14C3047C3C4A9A11F600D0D5F18F4491C000E0DEE108DE035F49D3AEE5F0B5781197898FB9294FC3CFB81785FAEED689D767178B367A2FFC900025AB27CAFB78F2A3F55676C624CBEAD80D3E3E2546577B00F837C73F124542EA024080858022B504AE4B8735DC0110D57BFF9DB2F367C2CFB81B85EBBB5827E7CF68775ADF2FD92CE9F8E79BADD38776C0E7DAFCE9662B37FE71CBFA6B037C3E147BFF660F003F76FC2351243000B817B75B00857CFB44AFC5AD382D7C945DB1007EC62B39F3F413D6C180A7F95BC965222FB74BCA7E0DF907EEB7AC2BEBE073A3D8FAB13D005CA8E46DFF4814090C00EEC9373A74CC280B1A009A6274D623B37611FC8C9792974592B6BC095FB71F413E1FD9350B8BCB36A3E749B123B5FEC2AF02C0B910C0C58028721800DC3BB16F0B3C6694050D00710A3D5E0240E696DBD4B7FE95F0357B25A7FB75DC1E9A796FAD55E83B103E5F8A95E22240CE00B0C2F143443597B60020A7F1D16B7123CA2BE1957220600088CBF57FE136009C1DF380D66BFD670EEF84CFC7977D6F5BF9BB43BA9D91AA65050A00531C3F4454730C00EEC5A918B60A1A00CE36ED82C78D223701E0F4F467E0EBF443CEAE682DFEADD47B9E7BF271F8FC2916A6A00070B7E387886A8E01C0BD382C86E3142400C8B75B74CCA8AA1400CE8C7B04BE4E3F8AC5BFD40C7F4DF2A3EE83AF8322EF6E14003A397E88A8E61800DC0B7BC00F43900070E4938DF09851552E0034DF3D54DF697FF59E96BCBD4FA703DBADC2C041F0F550A4754201E0778E1F22AA390600F7E2743ABC95DCC6875E8B1B71BBEBA1540028EE88A8E9363F793FAB52FC5B64F66C2C3E7FF4BA28B27E8702C08F1C3F4469F1B7CE56E1D6DB8BA7F464F18FDCCCC95676E11C2BB36E91D5BC6B95D524EB876F5B621D5BBFD03AB178AE756ACE54EBCC534F58F93E37E0E369C400E01E3A5ED405090071DBF91006802B4DEBE8D6C5F0F5F971BC06778264362DB6AC6BD4974AE76BA3A8FA110A001D94D3B61FA224ABEF61E51F7AC0CA2E79D96AFEA2FCB5E35307B6C1C1461C7E77A575E2CDB95666F83DA1EC38C600E04E9C16C4B10B1200BEFCE26D78CCA84201E0F4F393E06BF3A3969FF7DCD4E0CB1C5355488DEFD03E009C0B011B6D3F4849737DD7E2ECDDCCFAD78B337951472E45EE3147838EDD4135981F5BF79A95B97D087E7C1F1800DC91EBBEE87851172400C46DD2A33300E4BBF5D576DD5F54F3D47F3B5F6CB30A9D7929200636DA6BBE33003CE3F8614A82BF3658B9718F59CD9F04BB667AF4938D70E0418E6E5C6465354C10620070278E3B018A2001A0A605CF07670038B6FC25F8BAFC904DA4D0635653F6C5196D5E1F45D233F69AEF0C008D8E1FA638BB527DCBB87F9495795F5F2194A28A0620E4C007ABADE36B5EB5F27D06E0E7E70203803B71DC0950040900719BF4680F00F99E8DDABEFD17C35F14DE8BC3BBAC4215E60551208DF69AEF0C0097397E9862AA70F3AD56E6ED65B8A306200B8B785DBE5506F92FE74C85CFB312060077E2B426BE5D9000808E1765F6007062D52BF035F911A505A0644E91BDFF52E45C66AFF9CE00F07DC70F530CE51F7EC86A3E14C20A602DE4DA2B1A882A39B2E975ABD0B12B7CCEA53000B813B77BE25BF90D00F27BE87851D61A000A9D7A060A3E768722F8192FF4525F321DFD9822E3FBF69ADF360008C3DCE7F8058A8BABEAADDCECA9B053EAE6FB54F57B2BADECCDB7E0E70F3000B8239B08A1E3459DDF4218C7498FAD01E0CB1726C3D7E44714F77FC83DF544BB7E4C91B0CF59EFDBFC4791612E74FC12C581D9DDCAAE7E0D76C830C8F5D720DFDECE3E3E06BF0E07060077E2B815B0F0FB198AE237DF4A5A0340D3DB7AEEFB8F6A08CA6C7CA35D3FA64858E8ACF76DFEA3C830473B7E8922AED0ADAF9579AFFA83819B5B03CB39FD54E50D451800DC89DBA238ADFC0680384E7A940050B8B6B3B6C97FB558F4C715F5E5A0D0D013F667AAA9D1CE7ADFE63F8A0CD374FC1245991A50326F2FC71DB10A6406321A9CDC6ABE77047E5D2D1800DCD1B1DF7B2DF80D00719CF42801E0EC130FC3D7E355F12C5A84EF82C88F7910F667AA29F57FDAD6FB36FF5164983F77FC1245D595A6957DF345D801AB456620A301CAAD831FACB672370DC6AF4F6100A84C8A013A561CF80D00B226053A5E94490038AE69F67F14EEFB2F27BB94770344D0CF9DF5BECD7F1419E6854AD6F64B1451B9C91360E7AB36F936860629B70EAB1090EFD607BE460680CAE2F83A5BF90D00512F80880480C3BB96C3D7E355D4CFF864DE5D03FB33D58CD4F40B9DF5BECD7F9C6798DB6DBF4811941F39D26A6EC29DAFDA64453634487971F4ED25F07532005426BF838E15077E03401C273D4A00D0B1EB5F2CEE80D8BF0DF667AA99EDA8D6B76B2832CCA71CBF4C1152E8DEAFB8F636EC7835E2F7DAB5DDA91913DBBD560680CAE23A0150F80D003201151D2FCA2400E898001897C0675DD7A55D7FA69A790AD5FA760D458659E7F8658A90EC6BB36187AB255921100D565EC87C00E7ECE1D405000FFB2DB48AEB0440E1370044F1FEF78A36BD095F8B57B23B273C7EC4147AFB5F029CB4AB43B5BE5D439161FED8F1CB1411851B6E8ECCA97FA72301E70288132B5F69F37A1900CA93028A8E13177E03409496BF75EBEC07C1EE98110762B40052FE567DBB8252603F46B5BE5DC37986B9DF71008A80EC9AEA2DF6E395DF2582EDA420E486DC71FEF532009417C7D768E73700C46D2B60716AFF56F85ABC88D3DD0FF9FBEE6D337651CDEC87355E818D458639DB7110AAB1FC5D4361478B1259A10D0D5C5E1CDBF4D54A620C00E5C57902A0F01B00E2B615B09053F7E8B57871F28BB7E1B1A328FFE0E836E317D5CC6C58E315D8586498031C07A11ACB6CABDD823F6EC9B55934707995BDE1A673AF9901A0AC384F00147E0340DCB602163A0280CCB541C78E220680C818006BBC021B8B0CF3D78E83500D15AFFD834E16455EB70B464EBCAE42AB7ADD690B00B2AB1F7A2DA5C47902A0F01300E2B81190081A00E274FD5F300044C6AF618D57606391615EA09CB21D846A2837251A8BFEB821F768A301CC8B43EFAF2ABE6E0680D2A478A263C4899F0020934DD1B1A22E6800688AD9FE070C00912035FC0258E315D8789E612EB11D886A280EA7FF5B156F09F4796AD72E3B6C18034019717C7D4E7E02405C773E0C1A00E2B6FA210340242C81B5BD056C3CCF3087390E4635505CF80774B028F37A2A1B39B9EC25068032E23E0150F8090071BC0550040D005FC66802A06000888461B0B6B7808DE719E67F380E4635901BF718EC6051A663C2D3E1775630009421EF313A469CF80900719C002882F689384D00140C0091F01FB0B6B7808DE719E6B795BCED605403D9350B61078B3A1D9301CF8EBE0FB6BB91E40010B70961A5780D00719D002882EE9C19B7E0C300507352BBBF0D6B7B0BD8D88661AEB71D906A20B3379EA77AB5EC0F30ED69D8EE46920340124EFF0BAF0120AE130045900020EF133A66943100D4DC7A58D36D60631B8679B7E3A0544D57D74776E9DF4A74AC0C78F2CDB9B0DD8D240780249CFE175E278BC67502A0081200E278E68301A0E6EE8635DD0636B66198BF711C94AAA8D0B50FEC5C7111F432C0F12D8B61BB1B490D00713E0DEEE43500C47502A008120064854D74CC286300A8B9DFC09A6E031BDB30CC0E4A93EDA05445859B06C3CE151772EB121AD0DC3AF21E6E7723A9012029A7FF057A7DE5C47502A0081200E2B606806000A829A9D91D604DB7818DED18E60CDB81A98AF2C387C3CE1517B2521D1AD0DC6ADAEDFF0C425203401C37C22905BDBE52E27EE623480088E3DC0706809A9A016BB9036C6CC730EB1D07A72AC93DF630EC5C7172688FFF6D500F7DE8FF779318009274FA5FA0D7584A9C27008A2001208E677D18006AAA1ED67207D8D88E61FE5029D80E4E55929B341E76AE38097219E06080F090C400702C41A7FF057A8DA5C47902A0081200E2B60AA06000A819A9D53F84B5DC01364286B9D9F6005425B9679F829D2B4E64EB5634A8B9C100D056924EFF0BF41A4B91028A8E11174102401CC30F0340CD6C86351C808D90618E743C085541120280F07B198001E02B493BFD2FD0EB448AF7C1C77802A00812004EECDB028F19650C00353312D67000364286F9AF8E07A12A484A00F0BB281003C057E2781AB812F43A11795FD0EFC789ACE58F5E9B1B2763B60F806000A8997F85351C808DD0B9ED818FDB1E84AA202901C0EFE0C700F095B8EFFD8FA0D789C46D231C24480088E3E50F06809A901A5D72FB5F27D8589261CEB13D105541520280DCBF8D06B64A521700F66E80AF452EA1A09F8F3BF45A9DE2B80C2E122400C431FC3100D4C41C58BB4B808D25F176C0AA4B4A0010873F5E0F07B7721800CE39B97F2BFCF9B843AFD52909A7FF45900070E6D00E78CC286300A80957B7FFB5828D2519E6779466DB8351C89214006426331ADCCA610038F71EA09F4D02E76B45E23EFBBF55900010C715101900AA4E6AF37760ED2E01369665982FDB1E904296A400E06755400680E47EFB17CED7EA9494D3FF22480040C78B3A0680AA7B19D6EC326063598679BDE3412944490A00C2EBF6AF690F0049FEF62FECAF1549D2BE077E03405C43100340D55D0F6B7619B0B12CC3FC9672C6F6A014A2A4058052D7B84B497B003891E06FFFC2FE5A91A46C7B2CFC0680B8AEFFC00050555293BF056B7619B0B122C39C6B7B600A51D20280143434C89592E60090C4857F9CEC7F2FA7A4BD7EBF01208E5B010B0680AA9A0B6B7505B0B122C3BCC6F1E01492A40580338777C241AE94340780A47FFB17F6BF9753924EFF0BBF01208E5B010B0680AABA06D6EA0A60634586F94DE594EDC12924490B00C24B514F6B0048C3B77FE1FC9BD925E9F4BFF01B00E2BA02240340D5482DFE26ACD515C046570C7396ED095048921800CAAD76E794B600D0D41200E2B8F6BB1FCEBF59AB240620BF01208ECB000B0680AA99056BB40BB0D115C3BCD2F1242804490C0052DCD04087A43100A4E5DBBF407F3791B46D8F85DF002097CDD0F1A28E01A06AAE8435DA05D8E88A615EA49CB43D090A4112038097ED81D31800D2F2ED5FA0BF9B886BD12BC74F0088F33A080C00552135F82258A35D808DAE19E674DB13A11024310008B7EB01A42D00246DE25B25E8EF96D4F7C04F0088EB0440C1005015D3616D760936BA6698FFE97832A4595203800C6C68C0734A5B0048E237DF72D0DF2DA9EF815CCB47AFB79C386F01CD005015FF096BB34BB0D135C3ECA0ECB53D19D22CA90140063634E039A52D00A48DF36F7634211BFF207E02405C27000A0680D049EDED006BB34BB0D113C3BCD3F68448B3A40600D9E0050D784E0C00C9E6FC9BC571D73BB7FC0480389F0D610008DD9DB0267B001B3D31CC4B949CED499146490D00B2BB191AF09C180092CDFEF74ACAB6BFA5780D0071DF088901205452732F8135D903D8E89961CEB73D31D228A901401C7251DC190092CDFEF74AF2B77FE13500C47902A0600008D57C588B3D828D9E19E6E58E27479A243900C86C6F34F0D93100245BEBDF2AE9DFFE85D70010E709808201205497C35AEC116CF4CC30BFA67C6E7B72A449920380EC738F063E3B0680646BFD5B25FDDBBFF01A00E4E7D171E282012034526BBF066BB147B0D117C31C6E7B82A4499203809B05811800924DFE4E69F8F62FBC0680B8DF0EC900109AE1B006FB001B7D31CC4B9582ED499206490E00020D7C760C00C9267F270982E8DF92C64B0088FB0440C100100AA9B197C21AEC036CF4CD305FB73D51D220E901A0D244400680046BDA959A6FFFC2CD25AF5649785F180042F13AACBD3EC146DF0CF32AC793A580921E00ECFBDF230C00C996966FFFC24B00907532D031E28401201457C1DAEB136CF4CD30BFAE1CB03D590A28E901E0F8E79BE100D88A018092C26D0048C2E97FC100A09DD4D6AFC3DAEB136C0C842B036A95F40050E9BA2803002585DB009094CD901800B40BBCF29F136C0CC430BFAF9CB63D690A20E901A0D29D000C0094146E03C0A9049CFE170C005A494DFD3EACB901C0C6C00C739CED895300490F00020D82AD18002829DC0480831FAE81BF1B470C005A8D83B53620D8189861FE44C9DB9E3CF994860050EE4E0006004A0A37012029A7FF05038036524B7F026B6D40B0510BC39C677B01E4531A0280DCF2840643C1004049E126009C3AB81DFE6E1C310068330FD6580D60A31686F95BC78B201FD21000CADD09C000404951290024E9F4BF6000D0E6B7B0C66A001BB531CC758E17421EA521007C59E64E0006004A8A4A0120EE9BFF38310068B10ED6564D60A3365C1828B0340400D908060D8882018092A25200389DA0D3FF8201400BAD0BFF38C1466DCEED12B8C7F662C8A3340400218B9FA04191018092A25C0090CF39FA9D386300084C6AA7965DFF4A818D5A19663FDB0B228FD212000EA9625D6A6044ED6E30005094940B00493BFD2F180002EB076BAA46B0512BC3FCA672C4F6A2C883B4048052770230005052940B0049DC1381012010A999DF84355523D8A81D9707F62D2D01A0D49D000C009414A50240D3DE0DF0E7E38E012010EDCBFE22B0513BC3FCB6D2647B71E4525A0240A93B01180028294A058024DDFB6FC700E09BD4CA6FC35AAA196C0C85610EB6BD4072292D01A0D49D000C0094142800347DBC1EFE6C123000F83618D6D010C0C6509C9B0BC0AD823D4A4B0010E84E0006004A8A13200024F5DBBF6000F0456A64E8D7FE5BC1C6D018E60DB6174A2EA42900A03B01180028299C0120C9DFFE0503802F37C0DA1912D8181AC3FC86F2B9EDC55205690A00E84E0006004A0A670048F2B77FC100E099D4C66FC0DA1912D8182AC3EC6D7BC154419A0200BA1320500048F8372C8A177B0048C3679301C0B3DEB06686083686CA302F54F6DA5E349591A60080EE04081200927A7B15C5933D009C3AB00DFE4C923000782235F142583343041B4367985D6D2F9CCA48530090C550EC055C0409007249013D0E512D9CD8B7A5F8B94CCB992906004FBAC25A1932D8183AC3BC40D96D7BF154429A02C0D9A65DED8A78900070F4D3B7E0E310D5426B0048C3B77FC100E09AD4C20B60AD0C196CAC0AC3346D6F009590A600200E7CB8A64D110F1200644E017A0CA25A900090A679290C00AEA9FF036A6415C0C6AA30CC0ECA56DB9B4040DA0280F356C0200140065CF41844B5209FC7B47CFB170C00AE480DEC006B6415C0C6AA31CCDFD9DE0802D2160064E29EBD88070900B2F21A7A0CA25A48E2863FE53000B8F23B581BAB0436569561CE71BC216493B60020D7EDED453C480090BB0AD0631051F818002A9A036B6215C1C6AA32CC4B95B3B637856CD216008EB74C946A152400247DA115A2286300284B6ADEA5B02656116CAC3AC31C697B63C8266D01E0A4632D802001206DA75C89A28401A0AC91B01656196CAC3AC3BC58D96F7B73A845DA02807C6BB717F12001E0CCE19DF03188287C0C002549ADBB18D6C22A838D3561980DB637885AA42D0048D1B617F1200140D615408F4144E1630028A901D6C01A808D3561987FAF6CB4BD49A4A42D00882F6CDB02070900E8D844541D0C0090D4B8BF8735B0066063CD18E6BFD8DE2852D21800EC45DF6F0038F8E11A786C22AA0E0600E85F60EDAB11D858538639C3F186A55A1A0380AC9676BE90FB0C0087D4EFA1631351753000B43303D6BC1A828D356598FFAC9CB2BD69A996C600209BF8B41672BF01803B0112D51603401B52D3FE19D6BC1A828D35679837D9DEB8544B630038F6D9A6F385DC6F00E03E0044B5C500D0C64DB0D6D5186CAC39C3FC9AF296EDCD4BAD340600FBBEE97E0300570124AA2D0680F3A4967D0DD6BA1A838D916098BF50B22D6F606AA531007C7960DBF942EE3700700D00A2DA620028921AF60B58E3220036468661DE677B2353298D014056F06B2DE47E0200EF0020AA3D0680A2FB606D8B08D8181986F90DE57DDB9B993A690C00B280CFF962EE230070022051ED3100146BD737606D8B08D81829E7B60C2EB4BCA1A993C600200EB42C06E4270070022051EDA53C0048CDAAE956BF6EC0C6C831CC09B6373655D21A000E7DB4AE58CCFD04004E0024AABD94078009B096450C6C8C1CC3FCAEB2CFF6E6A6465A03809CC69762EE270070022051EDA5380048ADFA2EAC6511031B23C9300DDB1B9C1A690D00473F7DAB58CCBD06004E00248A8614070003D6B008828D916598731D6F74E2A53500C875FC6241F71800380190281A521A00E6C2DA1551B031B20CF387CA21DB9B9D78690D0027F66D291674AF0180130089A2218501406AD30F61ED8A28D818698679B9ED0D4FBCB40680935FBC5D2CE85E030027001245430A03C0E5B06645186C8C3CC37CD2F1C627565A03C0A996D500BD06004E00248A869405802761AD8A38D8187986F90FCABBB6373FB1D21A005A5703F41200E467D1B188A8FA521400A416FD03AC5511071B63C1307FA9645AFE008995D60070567D936F2DEACE425F8AEC22888E4544D59792002035E897B046C5006C8C0DC31C64FB4324525A038090A2EE25009C3EB81D1E8788AA2F25016010AC4D31011B63C3303B284B6C7F8CC449730090E580DD06009EFE278A96140400A93D1D606D8A09D8182B86798972A4E50F9238690E00875451771B0078FA9F285A121E00A4E65C026B528CC0C6D831CCAB6D7F984449730038FCF17AD70140260DA26310516D243C005C0D6B51CCC0C65832CC498E3F5022A43900C8AA7E6E02809C2940BF4F44B593E0003009D6A018828DB16498172BBB6C7FA44448730090FD00DC0400AEFE47143D090D0052632E8635288660636C19E64F95E32D7FA84448730090EBFA6E02004FFF13454F020380D4969FC2DA1353B031D60CF30AA5D0F2078BBD340700D90FA05200E0E97FA2684A5800909A7205AC3931061B63CF3047DAFE70B196E6007072FFD68A0180A7FF89A22961016024AC3531071B63EFDCFA008B6C7FBCD84A7300F8F2C0B68A01E00C4FFF134552820280D49258DFEF5F0A6C4C04C3FC9EF271CB1F30B6D21C006465BF7201E0D047EBE0EF1151ED252400480DF91EAC3109001B13E3DC7E01675AFE90B194E600203BFB950B003CFD4F145D090800523B62BBCEBF1BB031510CB3C1F6078D9D340780B34DBB4A0780DDABB9F52F518425200034C09A9220B031710C73ACE30F1B1B690E00A25400903502D0CF135134C43C008C85B524616063E218E685CA1ADB1F3736D21E00E436BF760180DFFE89222FC601406AC585B096240C6C4C24C3FC81F251CB1F3836521F003E5AD72E00F0DB3F51F4C53400488DF801AC2109041B13CB307FA61C6DF943C742DA03806C08E40C00FCF64F147DF907EE87635A84496DF819AC1D09051B13CD307FAF645AFEE09197F600D0E40800FCF64F140FF9FBEE85635A44494DF83DAC1909061B13CF30EB6C7FF8486300681B00F8ED9F281EF2F7DC03C7B488AA83B522E160632A18E650C7072092521F00F66EE0B77FA218CADF35148E6911A49E28A81129001B53C330A7383E089193F60070E4938DFCF64F1443F9DB86C0312D62A6C0DA9012B03135CEDD1EB8D4F661889CB40780A32D0180DFFE89E2A570F3AD704C8B1019FB5371BB5F29B031550CF3BBCAAE960F44E4A43E00A8C2CF6FFF44F153B8E16638A645848CF9DF853521456063EA18E6A5CA81960F46A4A43D001CFB6C13BFFD13C550A1EF4038A645808CF597C25A9032B031950CF357CAF1960F4864A43D001C570180DFFE89E2A7D0A33F1CD36A4CC6F85FC11A9042B031B50CF3B7CAA9960F4A24A43D009C3AB00DB61351B415BAF486635A0DC9D8FE5B38F6A7146C4C35C3FC77E56CCB07A6E67293C6C3CE9516A70FED80ED44146D565D0F38A6D5888CE9FF0EC7FC14838DA967987F56B22D1F9C9ACA3DF938EC5C69C1D3FF44F1645DDB058E69352063F99FE1589F72B09114C3BC56C9B77C806A26FFE818D8B9D2E26CD32ED84E4411767897655D5907C7B42A9331FC5A38C61303405986D95529B47C906A223F7A14EE6044441195F968231CCFAA4CC6EEAE706CA722D848368639C0F681AABAFCF0E1B0831111455566CB52389E55D90038A6D379B0911C0C7388E3835535F93BEE821D8C8828AAB2CB5F81E359150D816339B5011B0930CC518E0F58551406DF063B18115154655F7A1E8E6755320A8EE1D40E6CA4126A10020A036E821D8C8828AA72539E86E35915B0F87B001BA98C2A5F0E28F46A841D8C8828AAF28F3D0CC7B390F1B4BF47B0912A383731B02A7707C86A5AA883111145954C5E46E35948642CE6843F1F6023B970EE16C1F0D709B8BABE784F2DEA6444445154B8F9163C9EE92763306FF5F30936924BE7160B0A7DC5C0CCEEF5B09311114551A17B5F38966926632F17F90900369207E7960D0E75EF80CCDA45B09311114591754D2738966924632E97F70D08369247E736100A6D17C1ECFC99B093111145CE675BE038A6918CB5DCD84703D8483E9CDB4A58F69A461FD840D2BE232011C54766F312388E6922632CB7F4D50436924F86F92BE540CB07551BEE07404471915DF0021CC73490B1F55770EC255F6023056098972ABB5A3EB05A703540228A8BDC33E3E03816908CA997C231977C838D1490617E5759DAF2C10D8C6B0110515C84B006808CA5DF85632D05021B4903C3BC5099D2F2010EE64AD36A3EB803763622A22829F4B9018F63FEC8187A211C632930D8481A19E650DB87D9B7CCDBCB606723228A8CC33B8B8B97A131CC87A1704C256D6023696698754AC6F6C1F62C3B771AEE7044441191D9B50A8E5F1EC9585907C752D20A3652080CF3F7CAD1960FB867F907EE871D8E88282AB24B5E82E397073246FE1E8EA1A41D6CA49018E6CF948F5A3EE89E147A0F801D8E88282A72D326C2F1CB25191B7F06C74E0A056CA41019E60F94352D1F78F76422E0675B60A723228A02395309C7AFCA644CFC011C332934B0914276EE0E81B12D1F7CD7B22B16C04E47441405859B06C3B1AB02190B39D3BF0660235589613628675A3A4145B9679F829D8E88A8E69A7659D6755DE0D855828C7D0D706CA4AA808D544586F94BE5E3960E5156FE8EBB70C72322AAB1CCB6E570DC2A41C6BC5FC23191AA0636529519E6F794452D1DA3B48EDD54CAC69D8F88A896B2F3A6E371AB3D19EBBE07C742AA2AD8483560981D94914A41419DA628B36529EC7C4444B5E46202A08C6D32C675806320551D6CA41A32CC2B9492DB0A731E00114551A157231CB35AC89876051CF3A8666023D59861FE54813B0A166EB819763E22A29AF96473BBB1CA46C6B29FC2B18E6A0A36520418E6C5CAA4960ED446E6C30DB8131211D54076D9FC76E3540B19C32E86631CD51C6CA40831CCAB95232D9DA988FB02105194E49E19672FFA42C6ACABE1984691011B29620CF31265494BC7B2F2770D859D9088A816F2B70DB1177F19AB2E816319450A6CA4083A7797C0202563FDB5C16ADEBF0D764422A2AA3ABCCBB2AEED2C855F76F193318AB3FC63023652849D5B38E8DDEC9B2FE2CE4844544599ADCBA4F8BF5B1C9BD0984591051B29E20CF31F72CF8C7B13754622A26ACABE387DBD8C4970ACA248838D140FD979D347643EDA98479D9288284C99DDEB73B9E79FBD0B8D4D140FB091E223FFD0033FC9BEF9E21E2E114C4455D1B4CBCA2E9CF3417EC488FF158D49141FB091E227F7CC93B765DE599D851D96884883CCCE95D9DCF82706A33188E20736523CE5EFB8EB9FB2F3676E6A3EB803765E22225FD498927D71C6C6C22DB7FD231A7B289E6023C55B6EECA3F599CD4B4EC38E4C44E44166D3E2D3B9C71E36D15843F1061B29FE0A5DFA5C947B61CAA2E67D5B61A726222A4B8D1DB9595316C95882C6188A3FD848C9917FE881FF37BBFAB5A3B0831311013266C8D881C6144A0ED8480963985FCB4D797A72E6A38D05D4D9898844E6E3B70AB9E79E992263061C4B2851602325537EF4A8FF23BBF4E5DDBC6590889CB2CB5FD9937F70F4FF89C60E4A26D848C9969BF35C9FCCFBEBCEA0418088D225B37BFDD9EC4B33FAA3B182920D3652F26576ACBC30BB68EED4E64F36712541A234527D3FBB68CE54190BD01841C9071B293D725326FC400D022B9BF7BF8D0709224A16D5D7A5CF4BDF476302A5076CA4F4C93DFEF07FCBBE31EFC3E6433BF1A04144F1A6FAB6F471E9EB680CA0F4818D945EF9310F5C975D36FF28270A122584EACBD2A7A56FA33E4FE9051B29E50CB343FE91317764D6BFCED50489624CFAB0F465E9D3B0AF53AAC146A222C3BC2037F1C98732DB9637A3C18588A229B37D45736ED2F831D28761DF26526023915DA167FFFF929B356562E683753934D810513414F7E87F61CA24E9B3A82F13D9C14622243FE6816F65E7CF9CC35B078922E6D3CDF9EC2BB3E6E6C63EFA6DD4778910D848544E6EDAC47FCA2E7EF18DE6FDDBB8B430512DED7FBB905DF2D29BB99993794B1F79061B89DCC83DFFECFF9C5DF0C2EB993D1B784680A88A321FAECF67E7CF5C989BF2F4FF84FA26911BB091C88BFC903BBFA3BE814CCE6C5B7E160D5644A44766EBB2B3B919CF4E923E87FA229117B091C817D97570D2F83BB3AB5F3BD2DCB40B0E6044E491EA4BD955AF1EC93DF3E410E963B0EF11F9001B8982CA3F3AE66FD9D7E77ED8BC7F1B1ED488A83CD577B28BE67E987FE4A16B501F230A0A3612E9921F31E2BF65E74D5F97D9B3811306895C90BE929D3B6D6DFE9EE1BF427D8A4817D848A45B61E0A0FF9A9B36717666DBF22C1AF488D24EFA86F411E92BA80F11E9061B894263985FCF3DFBD49D99758B0E729E00A55E932AFCAA2F489F90BE01FB0C514860235135E45E98F2FBECCA051B9B3FDBC2DB08295D3ED99CCFAE58B021377BEA65A86F1055036C24AAA6EC1BF32ECACE9B3E22BB66E1BEE603DBF180491477EAB39D5DF5EAE7AAE80F53B8542FD51C6C24AA95FCE8513FCBCD9AF26266C31B67B82531C55ED32ED991EF4CEEF967E7E5478CF8DFD1679EA856602351CDC996C4F7DC63E65E98BA35B363252F1150AC64B62DCFAB20BB397FD7D0EBE5B30C3FE34435061B8922C5302FCA3F3AE6C6ECAB2FBC9BD9B391B7135224653EDC50C82E98F54E7ECC8337C867167E9689220436124555A1F7806FE5A64CB827BB6CFEDEE67D6FC38198A86A3EDF6A6597CEFF38F7EC53C30AF53D2E469F59A2A8828D4471907BECE17FCACE9DF65866C31B5F341FDA89076822DDD4672DB3FEF5FDD939CF3D9A1FF3C03FA2CF26511CC046A2B8C9CD9AF2A3DC9CE7C66697CDFF98970948B7CCEEF5F9EC92973E529FB3C772D326FE33FA0C12C50D6C248A35C3BC2037FEF1FAEC8BD3DF54DFD48EF3D642F2EC8B6D5666EDA2E3D9B9D35ECF3DFE4847F94CC1CF1A518CC146A224C9DF36E47FC94D1A7F7FF6B5D9EF6476ACC8C1019F524F3E1BD9575F78273771FCA8C2A05BF92D9F120F36122596DC5E3872C41F73D327CECB2E7DF970F3DE4DB018500AA8BFBD7C06E4B3901F3EFC0FF2D9809F19A284828D44A9D1B1DB777213C6F6C92E78617166F39223CD0777E06241F12793F7B62E3DAABEE52FC93D3DAEAFFCEDE1678228256023515A65D72CFC8E2A107DB3AFCF9DAFFEF7A799F7D7E6B822610CC9263BEA6F277FC3E2DF52FD4D331BDFF81FD0DF9C28AD6023117D253FF4EE9FE7268C1D919B35654DF6CD178F6676AC2C341FE64E869121CBEDEE5C55C82E7EE968EE85A96BD5B7FB91F97BEEF905FA5B12D1576023119561981716FADEF07FE59E7864A80A056FA850F06966DBF20C2F1F54819CC6DFB122935DF2F267B9D953DFCC3DF9D8B0C20D37FF46FE26F06F454425C14622F2AE3070D07F55DF3EBB67E74E9B9E5DF2D2AECCD665279BF773B542DF0E6CB732DB577C995D36FF9DEC8B3366E69E7DAA57FEF63BFE47F4DE139177B091883491BB0E1E1CFD8BDC334F76CD4D9BF8880A07AF665F9BBD23BBFC9583994D8B9B33BBD75BA9BC9C20A7EDF76CB4325B963667572C38985D386767F6C5E9AFE5663CFB586ED2F86EB9C71EFE2567E513850B36125195C8A9EBBA1EFF5B7EE4C8EB72631FBD2F3779C28BB9D953B7A882F85976CDC2E3991D2B33CD9F6D89DFCA869F6F2D6476AECC64D72E3C9E5D34F7B3DC9CE7B6E6A63CFD52EEC9C7EFCF8FBAEFFA42F77E3FE5697BA2DA828D44142DAA80FE63F6D5177E9F7D6556A7ECBCE983D57F8F564161A2324FFDEFA5D9B9D336675F9AF18EFA990FB3AFCFFD24BBE4E52FB22B173465D62E3A9679EBCD2F336F2F3B93D9B52A93F9605D2EF3F15BF9E67D6F179A0FEFB48AD4FF96B6E2BFC9CFC8CFCAEFA8DF9563148F25C79463CB63A8C792C72C3EB63C07F55CE439159FDB6BB32FCBCE9FF903F41A88284AACBFFBFF01974812BA485370D60000000049454E44AE426082, 1)
INSERT [dbo].[tbl_MstMenu] ([MenuID], [UserID], [MenuName], [MenuText], [FormName], [MenuParent], [MenuImage], [Seq]) VALUES (2, 1, N'frmOD', N'1.1 สั่งสินค้า (OD)', N'frmOD', 1, 0x89504E470D0A1A0A0000000D4948445200000200000002000806000000F478D4FA000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001974455874536F667477617265007777772E696E6B73636170652E6F72679BEE3C1A000058CB49444154785EED9D07785455C28663775D5DEBAEBA02825D2109BD885216BB2258504151049200CAD84B403120BD0A486FA143E855AA1104043290CCA410D26626A18726BD2439FF3D33677E524EB87726B7DFEF7D9EF77177FFFF810833F77B67E6CEBD2100007D1369B7DFD03177F77FA37252C22373935F88F024BF13E571460A7E17E9710E88F038C645B89D7384FFBC4C70BDE036E1FF6617FE9929E8123C5ECA4B82A494F47F2BFDFF972398C97EAD6D82F4D75EE6FDBDDCC963A33C8EFEC27FFF96FE2CC23FDB4479929FA73F23FD596348FCF5ECC70700000040693E3EB8FBDF5DDC49B584316D19E9767E1AE1710E1406755644AE335E18D554E17F3B22FCB3F4581B43DFCF9EC2FE5D66D27F37FAEF48FF5D235CA935230FD8EF617F0C00000080B96843E2AEEBEC765413C6EFB9A8DCE42E91B98EA1C2082E11063159F09C7728AD2DFD3370D23F93A85CC710E13F47D13FAB2EAEC4AA31845CCBFE1801000000FD42DFFAA6E315E1717C260CD904E19F5B847F9E11E40D1F14F7A260AA605CA4C71123FCB34D8427A93A8D2AF6470E000000A807FD4C9E0E5194C7F1A1304C23D9D09F15E48D18945F7ABE8210068E19BED84A7A26F280FD16F6D703000000541CFA6AB34BAEA346943BB95344AE63B2303C4EC1CB6C88A07EBC1CE9763A223DC993E8DF15FD3BC3470800000024D33D33F35F516EE72B91EEE47E511EE74661584E951A1A681CFF16DC20FC5DF68D703B5FEE989E7E1BFB6B0600006075E828F83EB7770E646FE5F3BE2207CD6181203DA76082601B7C0B0100002CC41779DBFE41BFAF1EE9491E1CE971240A435024C81B0B687E0B05770B0EA211D8C1E5BA993D4C0000009881CE79C961C241FE5BE120BF4EF827BE7A07CB933E36D646B893BF8ECC7386B2870F000000A3405FC9F9DFD68F743BF7963AC8432851875BF8E7047AE122BC3B0000003AE5937D7BEE8ECA757ECC2EB283EFDE43B93D233CBE1647E43A3B74CA4BBD8B3DEC00000068013D898B7E175F70857080C6C97B502D0BE809A3DE6B10B8D3EE670F470000004AD23927F9DE284F72F7288F73B37020A62771F10ED010AA25FD66C1A648B7F3137A5F07F6300500002007F4CC7DE120DB06AFF4A1CEF5BE3340EF8E886B0E00004090D0DBC94678925F130EA6F385032BCEDC8746F36CA43B79AEE0ABB87701000048A06B8EE371DF8D5FBC6760F30EAC101ACD03C2E37924FD3A2A7B98030000A0B0ABF1450807CA6DA50E9C109A4AEF4704EEE44EDD8EA4DECA1EFE0000603D225DCE27BCDFD5F7388F973E50426872E93D262644E5A484B3A7030000989B36A9A9370A07BE3682EB0571195E6879A33C4E3B3D71909EECCA9E260000601E3AE6EEFE2FBD2B5BA4DB7984771084103A0F47E43A7FC6B5050000A620D2EDA82D1CD8E8DDD7CE173BD04108CBF7A2605C94DBD9883D8D0000C018C410726D94C7F1B67010DB5AECA006210CD8E43FA3DCC96FD2E7147B7A010080FEA09FEFD34BF30A07AE3D650F6410C20A984D2F3D8C9B1201007485EF6B7C8ECF8483545EA9831684505E0FD1EB64747025DEC19E7E0000A03EF4AE68DE93963CCE93A50E521042653D11E571F6EEEA71DEC99E8E0000A03CF4D6BBBEABF5394F943A284108D5F534BD96066E510C00509462C38F57FC10EACBD3DECB0DE724DFCB9EAE00005071E8654BA33CCEEF84830C861F427DEB7D472032DB7E3B7BFA020040E0F8CEEA77460A079543A50E3210427D7B94463BBE35000008087A2B5E7AC312E120925BEAA002213492B94E4F54AEF363DC921800204A84C7F95CA4DBE9E01E4C208446754F549EE325F6340700802B4479529F120E122B4B1D3420842632CAE35811999BFC307BDA0300AC8CF7263DB9CE29C2C1A1B0F4C10242684A2FD01305E905BCD8610000602522EDF61BD8D5FBFE2E757080105AC2E47C7A0CC0F901005888288FA3857000482D7B4080105AD05D51B9CE26ECF000003023DDF627578EF43866700E0010428B4BCF0FE8EC765463870B008019A05FEB139EE0DF0B9E2BFE848710C2529E8D743BBEA1C70C76F800001895A89C9470E1499D50EA490E2184E59BEB4C8AC84DA9C70E23000023F145DEB67FB0EBF65F2AF3E4861042712FD3FB0BB43FE4F8273BAC0000F40E3DA14778F2A6977A324308613066D30B84B1C30B00408F747025DE213C59270816157BF242086145158E298E19F48EA0EC700300D00B511E474BE149BAAFD493164208E5F49070ACF9901D7600005AE2BD929FC7B9ACD49314420895D3ED5C12E94EBB9F1D8600006A1399EB6C4DAFE6C57D824208A1A226E747E53A5BB1C31100400DD819FE23F94F4A08215453C78C6E47526F65872700805244E526D7159E7438C31F42A827D33B7B9C75D8610A00202B845CC36EDE73B1D4130F4208F520BD6E400C6E2E04808CD06BF847E43AE3394F380821D4998EDF3BEF735462872F0040B044B81D6F094FAA63659F641042A85B4F46BA9DEDD8610C0010081DD3D36F139E44B1A59E541042681C739D53708220000110B12FE931E1C99352E6C9042184C6333DCA93FA143BBC0100CA2322D7F1BAF0843959EA0904218446F65494C7F1363BCC01008A43CF9CA567D00A4F145CC71F4268468B223CCE81F8960000C5A037D8109E18EB384F1808213495F41B4D5DB21CFF61873F00AC4B1777522DE149E12AFD24811042139BDB252FA93E3B0C02603DE85DB58427C2B9524F0C0821B4821722DD8ECEEC70088035E89E997913AEE50F218454C70C7A7F13767804C0BCD05B6846789277F09F081042683DA3729D7F7573A5DEC70E9300988F084F5275A176DDBC270084105ADC7D513929E1EC7009807988F2385A080FF013A51EF0104208AF782A2ACFF1123B6C02607C22729D1D8407F6A5520F7408218465BD1CE57146B2C327000685906BD8C57D780F72082184E5EA18498FA1EC680A807168939A7AA3F0209E59F6410D2184508A511EE7FC0E2ED7CDECB00A80FEE9EA71DE89FBF7430861C58DF038B6441EB0DFC30EAF00E897CE6E4735E141BBA7F48318420861D06646E5253ECA0EB300E88F88DC947AC203F570A9072E8410C28A7B28D2EDA8CD0EB700E887883C6753E101FA77A9072CE41891E320EDFFDA42DA2C5A4D5A4F58405E193C83BCDC2F9634E9F02379A6EDB7A4C9C73F92E7BF19425A8D994E3EDABE99FB6B40082DE9C92E9E94C6ECB00B80F644791C2D850726AEE97F1523B212C9DB71ABC80B3D2791466FF5230D5EEB5DC63ACD3A93DA8DDB95B1EEFF3A91673BFC40DE59B494FB6B43082DE5D9284FF2F3ECF00B807644B89DEF0B0F487CC7BF1C3BD8B791D746CC214FBF3B803BFAC52D2F008A5BEF8528F272DF5F49A7BD76EEEF0721B4841722DCC96FB0C33000EA13E1717C263C108B4A3D30A160A7F4DDE4D5A1B348C3567DB863CF534A00F8ADD3EC63D272E414EEEF0D21B48497223CC9EFB0C33100EA21D4E7D79C0724146CB3602569DC762077E4AF662001E0B761ABCFC8FBF1EBB93F0784D0F41608C7E28EECB00C80F244799CDF711E8896372227C97B321F6FDCA5184C00787DF643D27AE22CEECF042134BD45911EC7E7ECF00C807260FCF9764CB393165F8CE50EBB54830E00AFEF93E7BE1DC2FDD92084E637C2E3ECC50ED300C84F64AEB30FEF8167753BA62490673F1EC61DF540AC5800F8FC9FAD1FF76784109A5F210206B2C33500F211E94EEECB7BC059DDCE9989A4F9A7A3B9831EA87204007D27E0C55E23B93F2B84D0FC46799CBDD9611B808A431F50BC079AD5A517F479EEBBF1DC310F46790240F099F7C9EBA363B93F3384D0127ECF0EDF00040FCEF62FDF56131670873C58650B00EAB3EDC987DB36717F6E08A1F98DCA757EC90EE300040E7D00F11E58D0493EDAB995347CA32F77C88355D600106CF4C6E7DC9F1D4268098B223CCE08763807403AEC223FBC0715743965FBDCBFB8720700F5D52113F9FF0E10422B58808B05818088F2247F203C700A4B3D9020F39DC5ABB9035E519508007AC5C0C86C07F7DF034268092FD1FBB5B0C33B00E51391EB785D78C05C2EF50082C56CDE7D1477C02BAA1201407D65F078EEBF0784D0325E8CCA73BCC40EF3009425D29DD25C78A09C2FF5C081C56CB7E177EE78076BD38EC3BCF70C683D651179F9E771A459973EA451EBCF48ED673EE08E7930D67B2192FBEF0221B49467A3729D4DD8E11E802B44E4A6D4131E20A74A3D6060299F8F9EC01DF2406DF1F538F2C1E6F2CFD2F75E59F0AB41DEB3F979A31EA8ADA7CCE5FE3E10424B7932D2EDA8CD0EFB00848474763BAA090F8C43A51E28B0941FEDD84A1AB6947E773FAEAFF726AD272DE4FEFA3C3FDABE99D47FA90B77D403B161CBEEDC5F1F42683593F33BE5A53EC20EFFC0CA080F84BB8407457AD907092CEDF3DF4FE48FBA545BF6266D16ACE2FEDA5793BE1B40DFC6E70DBB74DFC7BB001042BF695D3DCE3BD90C002B1269B7DF203C1036947A60408E6FCF5BC11FF5007C7558F077ECFBE0CFF80A7F1C50B7D9C7A4A3733BF7D787105ACE4DDD33336F6273002C0521D7447A1C33380F0A58CA76EB3756F8A23F4FBFD39F74DAB38BFBEB4BB5C51703B9C31E88F55FE9463AEFB1737F7D08A1C57427CFA55BC056015885885CE7CFDC07042CE1BBCBD790466F55FC8A7F2D7F99C3FDF50391DE75905EE79F37EC81D8E0D56EA443C216EEEF0121B49A8E18360BC00A44E53A3FE63F10A0DF4E7B7791D746CCF67E6ECF1BF4407D3F3E9EFBFB046AC3D7BB73473D50EB36ED405A8D9FC9FD3D208496B24888808FD83C0033D3C595DC4CF80BBF58EA0100991D5313BCDFC96FDC762077C883B553FA6EEEEF17A84D2363B8831EACF4DB01ADC6C692CE3949DCDF0F4268092F45789CCFB1990066E4AD78CF73EF25669C6E9B98410CE3EEBDA4CDC6DDE48DA55B49EB859B65F7F539BF9357A7FC465E1C14479A7E3A96346CF53377C02B223D7F80F3840BCAA65F0CE30E7945ADD3E42352AFD597A461D450F2F437E349E3DEB348E37E7365F79931AB49D38576D27C63260CD217FEC820AF6C4B27EF39D3B88F110883F4EFC83C67289B0B60265AADCF0B7D66D6B1CBF5279F2086707816A9DF2D8E3478733077540D65CBDEA4B34CD7E3AFD57E1077C08DE5FBA456F348F248DBE1E4FAFE69246470360CD2EB86E490DB7E7193AA53DDE499D599A47D7A2AF77103A1445DDD5CA9F7B1D90066A04DFC915B9BC51D3EC51D5A9DD960E251D2A0FB2261382B78C11D9DD976EB4EDE932D606BB68AE60CAA816DF211B9B7EB7CEEB8C1C0BD764836B977BC8B345997493ABBF88F2108AF6654AEF3AF36A9A937B2F90046E7F9E507537863AB3BC71D26F53FACE0C57674EA8BBFAEE13ED902B576D38FF9436A68DF2755DBFFCA1D3418BCB78C709106CBB310023060233C8E716C3E80917971F5FE89DCB1D59B938E93FA9D62B9E369061B759CC07DA20562D3618B38E36916DF27F747CDE10E19AC98B78D749397B7ECE53EA6202CCF087772473623C088BCB2665FFB06538FF107576FF6D8C41D4E33F9FA9C8A7D15B0568B6E9CE13491CF7E486EFC39993B62B0E23E32C3453A6527731F5B10723C1F959B5C97CD0930122FAC3DF04463A39CF437219F3478730877344D659BA1E4437B7057E1ABD561087F344DE6E36FF7E38E1794C7DB47B9C97BC93851104A34D7E9893C60BF87CD0A3002CDE2C9CD4DE6E51FE78EAD0E6DD027813F9826B4D18763028E80BA9F8C15C6B1E257003482B59A7ECC1D2E289F370F779137ED7BB88F3508396E6843E2AEF3AD0BD03D2D161F4AE40DAD5E6DF0C942EE589AD63643C9EBB3FFE03DD14AF8DEE66D24BC754FEE509AD9DBBFDDC81D2E289F370CCB21AD76EEE13EEE202CA33BB99F6F5D80AE7961C5FE91BC91D5B30D3A4CE50FA5C9A52706BE386A0D69B7258174CEF45D89EF9D4DDBC83383E348CDB77A91DACF54ECEE7F46F5FEA8D9DCD182F27AD3301779D7898F03A0248B22DC8EB7D8CC003DF2FA86BC971AC61E2FE28DAC9E6DD076347720AD669D6611DC41B49A553F1CC71D2C28BFB78E70938E59DC033E84A53DD5D99DFC249B1BA0275AC61FB8A7E9BC23E77903AB7BDF1BC51D44AB59A75967EE205ACD6A1F8CE68E1554C62A535CBC833D843C53BA1D49BD95CD0ED00BCF2F3FB0873BAE461001E01501E01301A0BEF4CA819C833D84658CC8754C66B303F4C02B6B73FB7387D5282200BC22007C2200D4979E0FD02123857BC087B0B451B9C9EFB2F9015AF2CA5A578346D38F19EE73FF122200BC22007C2200B4F1C939F828004AF64464766A153643400BDAC4911B9BCD3F72923BAA461201E01501E01301A08DD70F75918FF6E24A8150B29B707D000D796EC5C1DFB9836A3411005E11003E1100DA193A3F8777A08790AFDBF10D9B23A0262FAF3E10517FCA71FEA01A4D04805704804F048076DE32C2CD3FD043C8F752446E4A3D364B400D5E5CE3BEBFF1CCA3C6B8CEBF1411005E11003E1100DAFACAB674DE811EC2F24C8B3C60BF85CD13509A168B0E657087D4A82200BC22007C2200B4F5F15938191006A83B792C9B27A0242FAFCDEDCB1D51238B00F08A00F08900D0D63B46210060E046E43A5E67330594A0D566CF534FCF3856C81D51238B00F08A00F08900D0D66B86E4E0F2C03070DDCE23DD5CA9F7B1B90272D362F1C13CEE801A5D04805704804F0480F6BEBE1DE701C0C08DF23856B0B90272F2DA7ACF60EE789A4104805704804F0480F6365E9DC53DC04328AADBD98ECD16908357577A1E6A34FDA8F9DEFAF78B00F08A00F08900D0DEB0385C0F0006EDD1CE39C9F7B2F90215A5F9C24339DCE1348B0800AF08009F0800ED7D620E020056C838365FA022BCB0E2600C7734CD2402C02B02C02702407B1F9E8E6F02C08A19E571BCCD660C04C3EBDB0FDFDB78C6B102EE689A4904805704804F0480F63E148B008015F660578FF34E366720509E5B7220913B98661301E01501E01301A0BD0800288BB8405070BCF25B6E5BD35CEB5F4C04805704804F0480F62200A04C1646B8939F66B306A4D02635F5C6A6F30E9FE18EA5194500784500F84400682F0200CA6872A4DD7E039B3720C6CBBFED5BC61D4AB38A00F08A00F08900D05E040094D96FD9BC81ABD166734EFD46D38F177187D2AC2200BC22007C2200B417010065F66C64766A153673A03C9E5F62D2CBFD5E4D04805704804F0480F62200A0DC46789C0BD8CC011E2DD7E77EC11D48B38B00F08A00F08900D05E040054C288DCA417D9DC81E2348B3F72EB33B3F32F7207D2EC2200BC22007C2200B41701001532152704727869E5BE55DC712C6503C197669F24ED169F221F2E3587CF0A077CDE205A4D0480CF669F8C276F2D3F6C0A9F9D7780DC35DAC51D593D8B00808A99EBFC82CD1EA0D0FBFC379A7EECAA27FE7DB5EE0C599F73899CB95444CCC63B5DC67007D16A22007C7ED6379638CE1153E93C5B4416B8CE93D0D87DDCC1D59B0800A8A07FE36641C5A8B92C379F37FAD4B68B4E11E7E1CB6C2ACD0902C02702C0A71903C06FB2E0FCECF3E486A1FCE1D58B0800A8941DDC0EF2789ADDCEE6CFDAD4D99CD1A55E3957FCFB7CED1972D684AFF84B8300F08900F069E600F09B70B280DC37D6C31D5F3D8800804A59236D17B9267107A9B937B1359B41EB12BA208F7BE2DF474B4F910B05E61F7F0A02C02702C0A71502809A70A290DC345C9FE7072000A012BE9DE320D70AE31FB27B07B92FD97E98CDA035A9F547C658DEF8379B7E92E49F2D64F3687E10003E11003EAD1200D43959E7B803ACB50800A884F7272778C7DF6F68DAEEEFD81C5A8BEA24F5C6B079870A79013079F779368DD60001E01301E0D34A01E014AC3E358F3BC25A8A008072DB3C23B1C4F853EF74EC3CDF46D842368BD6217C7D563C6FFC1B4D3D41FEBE608DB7FEFDBCDB1501404500F8B4F59BCE1D4BB3AAC777011000504E3F763BC82D493BCB0400F5A93DBBC6B359B406615BB3FE536BD611EED7FEBAAD3ECD66D13A7CF4F944EE205A4D0480CFEF7F59C01D4AB39A74BA883BC25A8A0080721A96B68B3BFED47F26EDBC5CC76EBF9DCDA3F9095FED4EE58D3F75D40EE1886031BAF598CE1D44AB8900F0D977EA6AEE509A55FA31C08DC372B843AC95080028976F6727916B39C35FDC47531216B1793437CFFCE979A84EEC51EEF853E7245F60B3681DFA8C58CA1D44AB8900F039E38F64EE509AD91AB1FA3A0F000100E53042F05E67C913FF78DE9CB4B3B05EEACEFBD84C9A97B055AE6CDEF0FB5D9A7E91CDA27598BE600B7710AD260280FA3ED971EC027724CDEC730B0F7287582B1100500E1BA5EFE60E3ECF4753ED6BD84C9A93E63B53AB5FEDD53FD58A01B0CBE9E20EA2D54400B4234FBFD0953B9066170100CD663B9783DC90C83FF18FE74D493B8B9A24EFACCCE6D27C84ADCE71F346BFB8560C804B970A48D3B7FA7347D14A2200DA9177BB0EE20EA4D9450040B3F9608AF85BFFA57D24C5FE3B9B4B73513721AD466D9157FF542B0600A5C7C005DC51B49208807664E26F09DC8134BB08006826FF9759F63BFF52BC31714751FDF41DD5D86C9A0729AFFEA9560D80AD0919DC51B492560F80862D3A73C7D10A2200A059FCD0E520FF08E0ADFFD2564B4EF883CDA639A8EB90F6EA9F6AD500282A22A4DD27E3B8C36815AD1E009FF79FC11D472B88008066F191543B77D8A57A43D2CEA2867B12ABB2F9343E61BFB9727863CFD3AA014059F347327718ADA29503A05EB38F49C2F14BDC71B482080068065F08F2ADFFD23E9ABA6B039B4F63D36487BB5A9DD87CEED8F3B47200D07701BA465BF7A240560E80DE13977387D12A2200A0D1FDC0E5A0DFE7E70E7AA0DE9CB8B3E8D9CCDDFF66336A5CC256B9D378435F9E560E004A4EEE11CB7E23C0AA01F0FCDBDF7247D14A2200A0D1AD925CB1B7FE4BFB78AA3D8ECDA831A9BF61DFDDB5A74BFBECDFAFD50380B27C5D227720CDAE1503A05EF38E24DE739C3B8A561201008DECB37BA55FF047AAB724EDBC5CD5157F339B53E311BE367B0B6FE4AF2602C0C7A829EBB8236966AD1600759AB4B7E4657F792200A0516D4B2FF8B35B9EB7FE4BFB58B27D149B53635187D86F089F73887BC7BFAB8900F041CF07E83F7A057728CDAA9502A0CEB3EDC99865DBB86368451100D088D26BFDDF9F1CF8057FA47A9B23E15C0821D7B259350E35E3B3E378032F2602E00A3402C6CDF89D346CC91F4CB3699500A8D7B403898D777287D0AA2200A011AD17C0B5FE83F591D49DDFB359350EA10B0E5CE60DBC980880B26CDE9E4E9E6F3B983B9A66D20A01D0E4D5EE64F59E03DC11B4B2080068345B673B446FF32B87F72425E4B3593506B5FECCFA8A37EE524400F0C93F769AC40C5B62EA7703CC1C00759EFD9044F61C4F769D2EE40EA0D545004023D9C1ED24B7C9F4953F293E99666FCDE655FFD458EA39C51B77292200AE4E4AFA3EF2559FB9A60C013306405D61F83FFA6A24D9947B823B7CD02702001AC9872B78B5BF407D20D99EC6E655DFD4DB9CD9B4EE94E3DC71972202401A070F9F2453E76D261DBF9A4C9E6EF53377508DA65902808EFE0B6DBE25BDC62DB5E4BDFD831101008DE2337BE5B9DA5F205E27582363D7936C66F54BD84AB7A49BFE9427022070CE9CBD4092523D64D9BADDDE28F835760319F0EB0AC3F9FEA74349CBF6BD0C65EB8E7DC87B9F0C229DBF1F4B7E1CB398CCDA9C4212CF1471470E962F02001AC1B7731CE4FA44FE482BEDC3A909FABE3CF0B39B33FF5D7BBAF4CBFEF2440058979C0BFC7180E6170100F56E47B793DCE150EE2B7F62DE92B4B320CCE1F8279B5BFD11B62E7B036FD4031101605D1000D6150100F5EEA3A9BBB8C3ACA64FA4DA47B2B9D51F61F30E14F0463D101100D60501605D110050CF3652E1FBFE52BCDB917092CDADBEA81D9F11C51BF44045005817048075450040BDDA2A3B4995EFFB4BB5667A422B36BBFA217485FB086FD0031501605D1000D6150100F528BDC5EF2D2A7EDF5F8A5553EDBBD8ECEA8367ED69F7D78E0DECAE7FE58900B02E0800EB8A00807AB3B3E07D0A5EE73F586F4ADA59D4282FF52E36BFDA13BE216B236FCC831101605D1000D6150100F5E65369EA5EEC2710ABA7ED1ECBE6577B82BDEE3F4F048075410058570400D4934D32F471D25F79FEDB997082CDAFB6846F4D6B5F6F72F057FE2B2D02C0BA2000AC2B0200EAC5D7B392BC57DEE30DAF9E0C4BDDDD82CDB07684AE70EFE70D79B02200AC0B02C0BA2200A01E7C2FC7416ED6D9497FE5592DC5BE85CDB036B4C9CBFB47AD0A5EF9AFB40800EB8200B0AE0800A8B51DDC0E7297C318E34FBD25716741F5D4B81BD91CAB4FAD8D993378235E111100D60501605D1100504B23DC4E522559BF27FD9567D81EFBD76C8ED52774F1BED3BC11AF880800EB8200B0AE0800A8A5D5D3B4BFCC6F30564EB667B2395697263BB32AD799768C3BE2151101605D1000D6150100B5B2F15E7D9FF17F356F4CDA59547FCF8EBBD92CAB47CD8DD96B79035E511100D60501605D1100500B5FCC4C24D768747B5FB97C322D61049B65F5A8B178DF05DE80575404807541005857040054DBD6D9499ADDDB5F4EEF73261C64B3AC0EF5B7A4D7AD3B45BEEFFE171701605D1000D6150100D5B44D4E12B929D13867FC5F4D7AA3A29A69898FB279569EF075993B78E32D870800EB8200B0AE0800A896EFBB1CE456037DDD4F8A8FA7DA67B179569ED005FB65BBF46F691100D60501605D1100500DE977FDEF76E8EF063F15F55E67C23136CFCA527B8BAB613D85DEFEA72200AC0B02C0BA2200A0D27612C6FF7E1DDEDD4F0EE9C700E1D93B1E6333AD1CE16B72147BFB9F8A00B02E0800EB8A00804A4A6FED5B25C57817FA09C427D3764D6133AD1C61F30F5CE20DB75C2200AC0B02C0BA2200A052D2ABFC3D9C6AEEF1A7DEE7B41F6633AD0CCF6E4E0F53F2ED7F2A0220788E9C2D24339DC28A1A14048075450040A57CC202E34FA577307C26D55E85CDB5FC84AFCBDEC81B6D39450004C7D17385E49D85A7BC7F8623B60B4754038200B0AE0800A884617B8C7989DF607D322DF15736D7F213BA38EF5CE9C1965B0440E0141F7FBF468C00048075450040B9AD69B1F1A7FE3739218FCDB5BC34DA967A57ED69474B8C8C122200028337FE7E8D16010800EB8A0080725A37DD7AE34FBD2171675183CCCC7FB1D9968F5AF19993792323B70800E95C6DFCFD1A29021000D6150100E5B27EBA716FEE238735D2767DC3665B3E4257B88EF006466E1100D29032FE7E8D12010800EB8A008072587B8FB5C79F5A2DD56E67B32D1FE1738E14F1C6456E1100E20432FE7E8D10010800EB8A008015B5B6053FF3E7F9AFA49D97D86CCB43C3AD191D78A3A2840880AB13CCF8FBD57B042000AC2B020056C49A78E55FC2BAC989CFB3F9AE38616B739CBC4151420440F95464FCFDEA39021000D615010083B5461A5EF997F6F154FB0236DF15276CC1FE8BBC31514204001F39C6DFEF2F3A8D000480754500C0408D107C12E3CFF55EB9AE0A58C7EEBEBFEE9463DC215142044059E41C7FBF7A8C000480754500C040A4D7F6B7C2E57D83F5BAC41DA46662E21D6CC683A7E6C6CC49BC01514A04404994187FBF7A8B00048075450040A976743B49A51473DED54F4E9F4AB57FCD663C784257BAF7F1C643291100575072FCFDEA29021000D6150100A5F891DB41EE7362FCA5F8508A7D0B9BF1E0099D77B090371C4A8900F0A1C6F8FBD54B042000AC2B02008AF981CB41EEC2F84BF66E47C26936E3C1D12431F1D17A5395BDFB5F691100EA8EBF5F3D440002C0BA2200E0D56C2B8CFFBF1C3BB94307F95E2BD8C0B1BD129BF3C0A9199F399337164A6AF500D062FCFD6A1D010800EB8A0080E5D93A3B89FC2311E31F8CA169BB7F64731E38A12BDD077843A1A4560E007A3FFFB7E2FEE6FEB9A8E59C646185350201605D110090E7F39989E4FA44FEB84171AB25EFFA8BCD79E084CE3BA0EAE7FF54AB068096AFFCFDD2DF9FFE1C5A8100B0AE080058DAA7F7EE26D770460D4AF76E6790E70134DA96FA88DA9FFF53AD1800187F1F0800EB8A00807E23DC4E521D17F891457A1E401DBBFD7E36EBD209DB9015CB1B0AA5B55A0060FCAF8000B0AE080048FDD8ED2095F11D7F59ADBEC7FE3D9B75E984ADF0E4F2C64269F51200170BD87F50103D7CE6DF76D12972FC7C11FB89B40501605D1100B09D2B897E758D3B6230781F4EB3C7B359974ED8FC83977883A1B47A0880B109E749C48AD3E4EC25E58611AFFCCB8200B0AE08006BFB7A16CEF457CA7B1D09F96CD6A5D18C90EBEB4C53EFFAFFC5D53A00E8F8FB7F16A52200E3CF0701605D1100D6F5D9BDBBBD9F55F3C60B56DC9B127716B521E43A36EFE2D48EDFFB096F34D450CB00283EFE7EE58E008C7FF92000AC2B02C07AD21BFA3C811BFAA862DDBDBB5F66F32E4ED8DAACCDBCE15043AD028037FE7EE58A008CFFD5410058570480B5FCD0E524F727E3F37EB57C32CD3E89CDBB3835967B8EF1C6430DB50880AB8DBFDF8A4600C65F1C048075450058C7D65949E49F49F8BC5F4D1F4CB5A7B0791747ED1B001557ED009032FE7E838D008CBF341000D61501600DF179BF36DEE54C38CBE6FDEA3CB163CFDD5A9D004855330002197FBF814600C65F3A0800EB8A0030B71DDC4E522D059FF76B258DAE9A898977B0992F9FF03F337EE08D885AAA1500C18CBF5FA91180F10F0C048075450098D73773E89DFCF079BFD686A6DADBB1992F9FB0B539DB7843A2966A044045C6DFAF580460FC030701605D1100E6B459066EE6A3179F48DD3595CD7CF9842ECF3BCA1B13B5543A00E4187FBFE54500C63F38820980DDA70AC95FF9E76100EE3C7E89FB67A9A5080073D9C1ED208FE02B7EBAB26ACAAE2436F3E5133EEF50016F50D452C9009073FCFD968E008C7FF08805C0AEBF0BC8ACAD19E49B5F5690B73F194F5EF8601869D8B23769F01A0CD4266F0F20ADA2C690CEBDE690D1CBED64F3FED3DC3F73B5440098477A963FDEF2D79FFF76DA4FB299E7533D35F5C6BA1ADC01B0B84A058012E3EFD71F0118FF8A515E00D057F9A397EF22CFBF3F943B66B0E2D290FAA4DF02B2C1759CFB77A0B40800E3DBD9ED24F5D37196BF5EBD51EC8A8075B6ED69C71B153555220046EF3CC7FDBDE49446006EEC53317801B032ED2079ADF328EE6841F96DFC465F3274FEB6327F0F4A8B0030B66D5D0E721F2EECA37BEBED497C9ACD7D59C23766C4F186454DE50E00255FF9EB4923BFF2F7533A0062E3D34893B7FB73870A2AEB1743977A3F7229FEF7A1A40800E3FA5C66A2F0EA923F38505F3E95BABB379BFBB2D4589D93C11B173595330030FEC6A27800CCFD2B8B346AF533779CA03A7E367849899156520480F1A497F37D08DFED37940FA7D8D7B0B92F4BF5659E53BC815153B90200E36F3CFC01B036EB2869FECE20EE2841751D3C6F6B99B156420480B1A4AFFA6F4EE28F0CD4AF0F24DBB3D8DC972574DE01CD2E01EC578E003876AE88BC3C5BDBCFE3D5D0E89FF997C61F001F7D3F833B46507D9F6EFD33599F7DACCC60CB2D02C018B6773B70453F037B9723E10C9BFB92C410726DEDD8A3DCA15153B9DE01F09C2C24AFCC316F0498E995BF1F1A0033B7ECE50E11D4CE6EFD1670475B4E1100FA17AFFA8D2FFD26400821D7B0D9BF4283ED69CFF386466DE53C07C0AC1160C6F1A7D000F8E09B69DC1182DA49BF22B82147D9AF072200F4EB072E07A98A57FDA6B1FEDEDDE16CF6AF502B3E73346F6CD456CE00A0982D02CC3AFE94A4236748C3D7FB7047086AEBB0B8BFB8C32D970800FD19E17692C67B77E30C7F93199EBABB3B9BFD2B84ADCDD9CE1B1CB5953B0028668900338F3F65DA6F89DCF181DADBFEBBE9DCE1964B0480BE7C2B2789FCC789EFF59BD1275276C6B2D9BF42E872CF41DEE8A8AD120140317A04987DFC29FD26AFE78E0FD45E7AD965DE70CB2502401F76145EF5D7DCB38B5C8357FDA6B56AAAFD2F36FB57085BB0FF026F78D456A900A0183502AC30FE94AF072FE18E0FD4DE462DFB782FC9CC1B6F394400686F8B8C44724BD24EEE6840F378AF23613F9BFD2B84CF3E54C41B1FB555320028468B00AB8C3FA5EB4F73B9E303F5E1D6C367B9E32D870800ED7C272789544AC6497E56F10EC7CE736CF619845CA387AF0052950E008A5122C04AE34FF976E852EEF040EDA557654C3A53C41D6F394400A8EF476E87F7ED7EDCBCC75A7ABF0A589CC63B93AAF306480BD508008ADE23C06AE34F19306D23777CA0F6BEF4D108EE70CB2502403D3B0B3EBB7737BED36F61EB25EFACCCE63F24A4F6E6F4CF7923A4856A050045AF1160C5F1A7CCDA90CC1D1FA8BD1D7BCEE20EB75C2200D4F1D5AC2472A7039FF35BDDF0BD496FB0F90F09A9F947E64CDE1069A19A0140D15B045875FC2929C72E90C6ADFB7207086AEBA86576EE70CB25024059E9D7FAF0393FF41B9A6AEFC5E63F24246C5DF60EDE1869A1DA0140D14B045879FC29F44A80F495266F80A076D2CFFF37ED3BC51D6EB9440028633B571279320D5FEB83257D3CD53E9BCD7F48488D559E5CDE2069A1160140D13A02AC3EFE141A004B1C79DE4BCFF286086AE337BFACE08EB69C2200E495DEAA979EE0771DE7E00FE14329095BD8FC0B01A083DB00FBD52A00285A4500C6DF87FF6E805131F3B84304D5F79937FB913FF2FE2E33D8728B009047FF99FDD7E3153FBC8A959213B2D9FC878484C61DB8C41B262DD43200286A4700C6FF0AFE00D8BCFFB4F7AC73DE2041751DB3727799B156420440C5ECE07692BAE9B86E3F94E63DCE84E36CFEE945800EEBE2224054AD0380A2560460FC4BE20F00EA8AD403E4D9B7FA734709AA63AF89EB4B8CB492220082F3439783D4D9B39BDC948833FBA1746F77269C67F31F12A2978B0051F5100014A52300E35F96E201405D9C984B9E7F7F28779CA072D273307E9ABC81249E55EEC23FA545000426FD8C9F0E3F5EF1C360BC396967A177FC1B2F4BBF8D37505AA99700A0281501187F3EA503801AEF3949DA7D35953B54507E5BB41D42A66E4C2DF3F7A0B4080069B6C767FC5006E9B742DA90D41B431A6DCE6CCA1B29ADD4530050E48E008C7FF9F002809A749690D8F834D2BACB58EE68C18ADBB4CD00D27BEA46B2FDE805EEDF81D22200AEEEDBD949E4B1543B2EDB0B65B3CE5EE7132175366774E70D9556EA2D0028724500C6FFEA941700C55D9D7E98F49FB99944F49A4BDA749F485E683F8C347F77100CC016EF0D16626A0CE9103D837C3B6A1599B53583D84F5EE6FE79AB250280EFEB5949A44A0A2EE003E5B7667A62AB905AF17B47F3C64A2BF51800948A4600C65F1C290100CD2902E08AF45AFDCD3212C95DB8642F54D09A69BB3E0F098FCF58CC1B2CADD46B0050828D008CBF341000D61501E0241FB81CA46EFA2EF20F9CD10F55F0C9B45D834342D7656EE58D9656EA390028814600C65F3A0800E5749E3C47D2F679C8DECC2492919EA03BBF9AB196341FB45C37B69BB0920CDD11179413772F2071C90B253B296901E96B9F4FBEDA315756A76CFB952CDB3A54737F58DC877C36FF27A8A01D160D228DD6CF2277EEF89D3BF63C1F4B4E981952634D4E1A6FB8B452EF0140911A0118FFC04000C8EB5ED71EB26FDD787262FC07E44CFF67A14A5EF8ED6B527872B9F6EE9F488A72866AEEA949CF93A3DF3F0055B112C989A941E68C798F84C52FE00EBFDF875377AD0CA9B1CAEDE18D97561A210028621180F10F1C04803CEEC9CD2247E67CC31D27A8BC08809222003432BA12F97DE80BE4D1CD4BB801502DC5FE6748D8724F3E6FC0B4D2280140292F0230FEC18100A8A84524EFF7A9E4CC80A6DC6182EA8800282902405B8FF47890D8E6FF542600AAA6EE4A0A095DB24F373702A21A290028A52300E31F3C0880E04D3E75811C5ED08B3B48505D1100254500E8C0E84A64C6F80F4A044095147B6648E8C2FDE78B0FB0D61A2D0028FE08C0F8570C0440B01691438B62B86304D51701505204804E8C7E808C9A1CF9FF01F040B27D5F48D8FC83BAB91320D5880140710B1170FC7C11FB6F20181000C1E97DDB9F3344501B1100254500E84821023A2FEAEF0D807B1D09F921E1F30E16F086582B8D1A00A0E2200002774F5E363EF3D7990880922200F4E5C11F1E25B724FC496F097C32246CEEA142DE106B2502C0BA2000021767FBEB4F0440491100FA73F6D8B6E44EC7CEB32161730E15F186582B1100D605011098F47BFEBC0182DA8A00282902407F1EEE518DDC91F8D7C590F0D94710004017200002935EE4873740505B1100254500E8D3CF16F62D0C099F79983BC45A8900B02E0880C0C415FEF42902A0A408007DBA79C8F324A4D68C7CEE106B2502C0BA2000A49B7CE20C777CA0F622004A8A00D0A7D97DC24948EDE947B943AC950800EB8200902EBDB10F6F7CA0F622004A8A00D0A7477A561502601A0200E80304807433327673C7076A2F02A0A408009D1A5D8984D499768C3BC45A8900A818C7CF19F762440800E966A6FDC51D1FA8BD0880922200F46B48DD290800B33071D779F2E2AC9324EB7801FB5F8C050240BA0800FD8A0028290240BF86D49FC21F62AD440004071D7FFF9FA151230001205D04807E4500941401A05F110026A0F8F8FB3562042000A48B00D0AF0880922200F46B48BD29C7CB8C87962200028337FE7E8D16010800E92200F42B02A0A408007D7A8C9E0458772A02C0A85C6DFCFD1A29021000D24500E85704404911007AB532BE056054A48CBF5FA344000240BA0800FD8A00282902409FE6477B0300D701301A818CBF5F2344000240BA0800FD8A0028290240A7F6789084D48EC5A5808D4430E3EF57EF118000902E0240BF22004A8A00D0A7F93400702F00E35091F1F7ABE708082600B61C3A4336B88E5B4EFBD6F5DCF181DA8B00282902409F1EEE598D84D49C998FDB011B0039C6DFAF5E23402C00FECA3F4F26AE71926E7D1790973F1A411AB7EE4B1ABCD6DB92DADADBB8E303B51701505204803E3DFCC3434200CC3A8200D039728EBF5F3D46407901B0F3F8253268CE16D2F49D81DC31B4A23604806E4500941401A04F0FFDF00809099F7D1801A06394187FBF7A8B005E002CB0BBC9F3EF0FE58EA095B52100742B02A0A408007D7AE0874785009877A880370E5A8900B88292E3EF574F11503A00C6ACDC6DE9B7F9AFA60D01A05B1100254500E8D3DC98EA4521610BF65FE40D835622007CA831FE7EF51201C50320363E8D346CC91F3F8800D0B30880922200F4694E9FF0C290B08579E778A3A095080075C7DFAF1E22C01F002BD30E9267DFEACF1D3EE8D38600D0AD0880922200F4697ABFFA0521A14BF24EF206412BAD1E005A8CBF5FAD23800640D25942DA7D39853B7AF08A3604806E4500941401A04F9D031B5F0C095D967B8437065A69E500D072FCA90DA79C206BB2B4FBF3A7013075632A77F060496D0800DD8A00282902409FEE1CD2FC5C48E80A4F2E6F0CB4D2AA01A087F15F99A1ED9F3D0D80773F9FCC1D3C58521B0240B722004A8A00D0A75B87BF74322474956B0F6F10B4D28A0180F1F7B1EBE0299CF827511B0240B722004A8A00D0A7F1A35E3F1412BE2667276F14B4D26A0180F1BFC2E495BBB86307CB6A4300E8560440491100FA74ED98B7B343C2D665AFE20D8356EA2100B28F179075D997D87F530E8C7F497E9EB88E3B76B0AC3604806E4500941401A04F574D68B733A4E686CC5F79E3A0955A0780EB44017965CEDFDE715C95A9DCCF82F12FCB97831673C70E96D58600D0AD0880922200F4E9FC69910B43EAFC91D98D37105AA96500F8C7DFFFB3281501187F3E5D7F9ACB1D3B58561B0240B722004A8A00D0A753E77CD12B24EC4F5703DE4868A55601507AFCFDCA1D0118FFF2F96EF832EED8C1B2DA1000BA1501505204803E1DB6AC77F3901042AEAB37E538772CB4508B00286FFCFDCA150118FFAB3330F677EED8C1B2DA1000BA1501505204801EAD44BA678EBA2984523BF6287730B450ED00101B7FBF158D008CBF3873E3711120A9DA1000BA150150520480FE3CD2B32AF18E3F257CF621DDDC1258CD00903AFE7E838D008CBF34F69CBC489E79B31F77F060496D0800DD8A00282902407FEEFFF1892B011036FFC025DE7068A15A0110E8F8FB0D340230FED2A157028C8A99C71D3C58521B0240B722004A8A00D09FDE3B01FA095DB4EF0C6F3CB4508D000876FCFD4A8D008C7F60D00058B9E71069D4B20F77F4E0156D0800DD8A00282902407FA60C6C7489CDBF10002BDC077803A2854A07001DFF9767073FFE7EC52200E31F383400E8ED806D031771470F5ED18600D0AD0880922200F4E7CE612DFE66F31F1212B6C665E78D88162A1900728DBFDFF22200E31F1CFE00D876E41C69D9793477F8A04F1B0240B722004A8A00D09FEB47BF95C3E63F24A4E6C6ECD9BC21D142A50240EEF1F75B3A0230FEC1E30F00EA6F194748D3770672C70F2200F42C02A0A40800FDB97872C70D6CFE43426AFD9165E38D89162A11004A8DBF5F7F0460FC2B46F100A0AE4E3F4C5EED38923B8056D78600D0AD0880922200F4E794B95FF761F31F12D2684DEA23BC41D142B90320EB78017971F649EEEF25A70D38FF9B9A1A7DFC29A50380BAF5D05912814B0497D18600D0AD08809222007466F403A4CF8A81E16CFE7DE8E56240720680D2AFFCF5A219C69FC20B00BF0BEC6EF2C137D348C396FC41B49A3604806E4500941401A02FF37B14BB08909FF039870A79E3A2B6720500C6DF785C2D00FC6EDA778A8C5A9A40BE18BA947CDC731679A3CB58F25AA75196F3C7EE3DB8E303B5170150520480BECCFBA9D84580FC842ED8779E37306A2B47001C3B57A4CADBFE5A6BA6F1A7480900E83333ED2FEEF840ED4500941401A02FD3FBD52B60B37F85D0659EC3BC91515BB9DE0198A0F109794A6BB6F1A72000A48B00D0AF0880922200F465C29016A7D9EC5F217C4DCE4EDED0A8AD9CE700983502CC38FE140480741100FA150150520480BEDCF8EB1BD96CF6AF507343E618DED8A8AD9C0140315B049875FC290800E92200F42B02A0A408007D397F6AD40236FB57A8B5796F0BDEE0A8ADDC0140314B049879FC290800E92200F4EB85E536FE20ABAD4E02E0E4C846DC2182DA386271CC9B6CF64B5267DA31EEF0A8A9120140317A04987DFC290800E92200F4EBB9B99DF883ACB63A0980633F3DC41D22A8BEF9D155480821D7B2C92F49F8DC4305BCF15153A5028062D408B0C2F8531000D24500E8D77313DEE20FB2DAEA21005CC3B94304B53137A67A119BFBB2842EC93BC51B203555320028468B00AB8C3F0501205D04809E6D420AF6CDE68FB29AEA20002E39FA7087086AA37360E38B6CEECB12B6CA95C11B2135553A00284689002B8D3F0501205D0480BEBDB8F927FE28ABA90E02E0CCC2F7B84304B5F1F751AD0EB1B92F4BF8BAECC5BC215253350280A2F708B0DAF8531000D24500E8DBB3235F21854717F187592D350E80C2AC61E4688FCADC2182DA58E22E80A5A9BD39A3036F8CD454AD00A0E83502AC38FE140480741100FAF7E2C61EFC61564B8D03E0F4AC56DC1182DA3963D6A75DD8DC97A51921D76BFD4D00350380A2B708B0EAF8531000D2450018C0814DC9E5D431FC7156430D03E0E2B66FB803043534BA0A898F8FB99ECD3D9FB0790735FD2680DA0140D14B045879FC290800E922008CE1D9E12F90CBD993F903ADB41A0580F7C43FBCF5AF3B3DBD6B94FF0D003F61CB3CF9BC71524B2D0280A27504587DFC290800E922000CE4E0FF914BBB87F2475A49D50E00D7307261F317DCF181DABB73C8FFCEB1992F9FB075399B7803A5965A050045AB08C0F8FB4000481701603CCFCDF8805CCE99CA1F6B255431000A328692539371C95F3DBB7C5CBB7436F3E553333EEB6BDE48A9A5960140513B0230FE5740004817016050073421E7A6B7231737C790CB7B279082FDB349E1F1A5FC01AFA88A05C03052E81A410A32E967FDDF9253E39B730707EACB09B3BE18CC66BE7CEAACCFBEBDEE14ED4E04D43A00286A4500C6BF240800E92200A098277E7C9C3B04D08A5626316B26DFC566FEEA84CD3E54C81B2C35D4430050948E008C7F591000D2450040311100D06F5ECC53E22700FA095D9C7B92375A6AA89700A0281501187F3E0800E92200A0980800E877F7A0A6E7D9BC8B13F65BCE36DE70A9A19E028022770460FCCB0701205D040014130100FDAE1FF3B6F809807E6AFD91D995375E6AA8B700A0C8150118FFAB8300902E02008A8900807EA7CEFAAC179B770910725DEDD8A3DC11535A3D0600A5A21180F1170701205D040014130100A9F93D1E2431A97137B2759746E882FD177943A6B47A0D004AB01180F197060240BA08002826020052B3FAD42A64B32E9DD055EE2CDE9829AD9E038012680460FCA58300902E02008A890080D43F46B53CCA665D3A35D7678EE50D9AD2EA3D0028522300E31F180800E92200A0980800489D31FD933836EBD2A9F9D79EAAF5A61CE70E9B921A210028621180F10F1C0480741100504C04003C165D890C5EF2F3C36CD603438B0B0219250028E54500C63F38020D801DC72E92B599F96479CA01CBB975E32AEE411F42BF0800981B535DFA05804A13BA2CF7086FE094D4480140291D0118FFE0110B80F8BCBFC9F085DBC987DF4F27CFBED59F3478ADB765B5B5B7710FFA10FA4500C01D435B9C66731E38E1EBB2E7161F3735345A0050FC1180F1AF18E505C0D6C367C94F933790C66FF4E58EA115B52100A0880800B878D2879BD89C074E8375698FAA7D1E801103804223605526C6BF22F00260E69F7B49D377067247D0CADA100050440480D5AD447E59F8630336E7C1113EEF50016FA895D2A801002A4EE900183C6F2B69D4B20F7700ADAE0D010045440058DB0A7DFEEF2774A5CBC31B6AA544005897E201306E751277F8A04F1B02008A8800B0B65B87BF7C82CD78F0846FCC18C51B6AA5440058177F002C4ECC258D5BE3F3FEAB69430040111100D676D6F44FE7B0190F9EEADBF2EEAA3B55BDF3001000D6850640E2D922F2F6A713B8A307AF684300401111001636BA3219B778C87FD88C578C3015EF0B8000B02E3400F0D6BF346D0800282202C0BA66FE5C3BF0EBFF9747D86FEE14DE582B2102C0BAD000C0AB7F69DA10005044048075DDF0EB1B796CBE2B4EAD2D999D7863AD840800EBB233EF2469D8923F78B0A4360400141101605DA7CFB4FDC8E65B1E6ACE3A52C41B6CB94500589789CB12B86307CB6A430040111100D6F4F00F0F1136DBF211BADC7D8837D8728B00B02EBD27ACE58E1D2CAB0D010045440058D384C1CDCFB2D9968F9ABF678EE10DB6DC220082C77DB2908CDC718E1416B1FFC1607C31701177EC60596D0800282202C09ACE8D8D5ACC665B3E9AC5A7DE5A67DA31EE68CB290220383CC2F8BF32E76FEF9F61CFDFCF1A3202BAC6CCE58E1D2CAB0D010045440058CFFC1E55C8F0B898BBD86CCB4BD8A27D674A0FB6DC220002A7F8F8FB3562047C3F623977EC60596D0800282202C07AA6F46F7499CDB5FC84ADCD595F7C649410011018BCF1F76BB408183C3D9E3B76B0AC360400141101603D974CFA70379B6BF9A9BD3DAB8ED27707440048E76AE3EFD74811B0E0CF74EED8C1B2DA100050440480B53C165D890C9FDBB32E9B6B65089D7FE0126F68E41201200D29E3EFD72811907EEA3269F2767FEEE0C192DA100050440480B5CCE9132EDFD5FFCA23748D6B1B6F64E41201204E20E3EFD7081140AF04D8ADDF02EEE0C192DA100050440480B55C37E6AD4C36D3CAF1747C6A4D253F0640005C9D60C6DFAFDE238006C0DACC7CD2A8D5CFDCD18357B42100A08808000B19FD0019BBE8FBE7D84C2B4B58DC01C56E0E8400289F8A8CBF5F3D47000D007A3BE0AF86E3DB0062DA10005044048075CCFEB9A6F26FFFFB095F9BB399372E728800E023C7F8FBD56B04F80360C7B18BE4CD6EE3B8C3077DDA100050440480755C35AE6D3A9B67E5A9B3694FA8521F032000CA22E7F8FBD56304F80380BAC1759CB46837843B7E100100C5450058C54A64E28298A7D93CAB4368DC7E453E064000944489F1F7ABB708281E00FE0878A3CB58EE005A5D1B02008A8800B0867BFBD62B60B3AC1EA16B72E279A352511100575072FCFDEA29024A070075C7D10BE48BA14B49A3967DB84368556D0800282202C01A2E9DD05EB98BFF9447FD1DE9D594B8370002C0871AE3EF572F11C00B00BF2BF71C229131F348E3D67DB98368356D0800282202C0024657262317FEF0289B6575A9B178DF69DEA054440480BAE3EF570F1170B500F04BDF1198B22185F41CB7867C3A6021F9E09B69E41DDB44CBD9F79B9FB8077D08FD2200CCEF9E010D2EB139569FF00D591378635211AD1E005A8CBF5FAD23404A00409F99697F710FFA10FA450098DF45533B2D6373AC01845C5773667E116F4C82D5CA0140EFE7AFD5F8FB8D4D3ACF7E1AF5410048170100C5440098DBC33D1F22D3E23BDCCCD6581B4257BA3CBC210956AB068096AFFCFD765E719A9CBDA4DD5B000800E92200A098080073BB75F8CB27D80C6B47EDF8AC77796312AC560C008CBF0F0480741100504C0480B99D30EFDB4FD90C6B4BD8FC439779A3128C560B008CFF151000D2450040311100E635A77758119B5FED095F9BB385372CC1A88700A027C29DBEA8FC20EAE133FF089D8C3F0501205D0400141301605E978E7F3F99CDAFF6D4DB945CB976EC51EEC004AAD60140A770C096B3A4DDE253E4C479E58611AFFCCB8200902E02008A890030A7F93DAA90714B62AAB2F9D50761CBF38EF0462650B50C00FFF8FB7F16A52200E3CF0701205D0400141301604E13073539C766573FD4FA3DBD236F680255AB00283DFE7EE58E008C7FF92000A48B0080622200CCE99CD86E7DD9ECEA8BB0F9072FF1062710B50880F2C6DFAF5C1180F1BF3A0800E92200A0980800F3E9EA1DAA9F93FF4A13BE367B396F740251ED00101B7FBF158D008CBF380800E92200A0980800F3B978D2473BD9DCEA8F66F1A9B7D69A9ECF1D1FA9AA190052C7DF6FB01180F197060240BA0800282602C05C1EEE598D7CBF72EC9D6C6EF549D84AB78B374052552B00021D7FBF814600C65F3A0800E92200A098080073B9E997D78EB199D52FB5E3D35FAA37E5387788A4A84600043BFE7EA54600C63F301000D2450040311100E6F15874253276EE97AFB399D537A18B82BF4DB0D20150D1F1F72B160118FFC0410048170100C5440098C794018DB4BBED6FA0D4FE63EF17BC4192A2920120D7F8FB2D2F0230FEC18100902E02008A8900308FF3A7458D62F36A0CC2E61F08EA2B814A0580DCE3EFB7740460FC830701205D0400141301600EDDBD6BE8F7AB7FE511B6212B96374E622A11004A8DBF5F7F0460FC2B060240BA0800282602C01CC64DE9B481CDAA8120E49AF0D9878A78237535E50E007A639F3E9B951B7FBF6D179DD27CFCF574639F60400048170100C5440018DFFD3F3E4626D827DCC056D558D45C9BB38937545753CE0050FA95BF9E34F22B7F3F0800E92200A0980800E3BB7C42BB1436A7C62374A5E7CEDAD303BB4BA05C0180F1371E0800E92200A0980800637BB8E7432466C5D07BD89C1A93D055EE3DBCD12A4F390200E36F4C1000D2450040311100C676C3E837F3D88C1A97FA9BD2ABD589957E79603902E0F8F922F2669CB69FC7ABA1D13FF32F0D0240BA0800282602C0B8E647572193E67DFB249B516313BAD29DC91B309E727D0470F84CA1A923C04CAFFCFD2000A48B00806222008CEBB6112FE9FFB2BF5269B42DEB91DAB1D2CE0590F32440B3468019C79F8200902E02008A890030A6F9D195C994395F3762F3690EC256BAB37963565A39038062B60830EBF8531000D2450040311100C6F4CF5F5E39CE66D33C348A773D5167DA31EEA81557EE00A0982502CC3CFE944002C07EF232D9E83E411627E69279DBB32DE7EFAB96700FFA10FA4500184FFAD9FFA02531F5D96C9A8BF0956E376FD88AAB4400508C1E01661F7F8A5800ACDE7B98F4898D276F751B4F1ABCD6DBD2DADADBB8077D08FD22008CE7A6912D8FB2B9341FF57FDF1B5E67EAD5DF05502A0028468D002B8C3FA5BC00F83DF724F9E69715A451CB3EDC31B4A2360400141101602CE9ABFF9171DFD76173694EC4CE0550320028468B00AB8C3F851700E37F7390C66FF4E58EA095B52100A088080063B969E4AB47D84C9A97A737A63D78B56F04281D0014A3448095C69F523A007E9ABC813B7E100100C5450018C7FC1E0F926971DF85B2993437616B5C76DEE051D508008ADE23C06AE34F291E0043E76FE30E1FF469430040111100C6317E64AB7D6C1ECD4F83ED99FFAA35EB08F74E816A050045AF1160C5F1A7F80360EEF66CD2F0757CDE7F356D0800282202C018D26BFE8F5B31FC01368FD6207C6DCE6FBCF1533300287A8B00AB8E3F850640E2E942D2BACB18EEE8C12BDA100050440480315C36E143079B450B41C87561B30F15961E40B50380A29708B0F2F85368008C5C9AC01D3C58521B02008A8800D0BF793F3D41BA678EBA89ADA2B5A8B9216B52E911D42200285A4780D5C79F420300AFFEA569430040111100FA77DED488A56C0EAD49D8FC03978A0FA1560140D12A0230FE3EFE721FE38E1D2CAB0D0100454400E8DBCC9F6B17B219B42EB5FEC8EC5A6FCAF1FF1F432D0380A2760460FCAF306EF10EEED8C1B2DA1000504404808E8DAE44664C8DEAC566D0DAD4589E97EF1F44AD0380A2560460FC4BD27BFC5AEED8C1B2DA1000504404807E750C7CE61C9B3FD068536AAD3AD37C1707D2430050948E008C7F593E1FB0903B76B0AC360400141101A04FE9257FA7CFFEB2399B3F40095BE3DA4587512F0140512A0230FE7CBAF59EC71D3B58561B02008A8800D0A71B46B572B1D9037E1A6DDBF68FF0D9878BF4140014B92300E35F3E3D46AEE08E1D2CAB0D0100454400E8CF033F3C42A64CE9781B9B3D509C9AEB33C7EA2D0028724500C6FFEA0C99F10777EC60596D08002822024067463F40E2622316B0B9033CE6669EBECCF6405754340230FEE22CF92B933B76B0AC360400141101A02F53FB37B8CC660E94C7AA23676A9EBD5CC826415F041B01187F69649C2E204DDF19C81D3C58521B02008A8800D091D195C9C479DFBECF660E5C8D63172FFFC4364177041A01187FE9D02B017E3E640977F060496D0800282202403F6E1AF9DA21366F400C610BAE2F24C4E19B05FD21350230FE81410360A3FB0469FC465FEEE8C12BDA1000504404803E3CF0E3A364CE9CC87BD8BC0129087B504FB0C0BB0C3A442C0230FE81430380DE0EB8C7D835DCD18357B42100A08808007D1837B9D334366B2010844D18EB9B067D525E0460FC83C31F00F69397C97B5F4CE60E1FF469430040111100DABB7B70B3336CCE40A0089BF02FC1FDDE75D029A52300E31F3CFE00A06EDE7F8ABCD2E117EEF8410400141701A0AD877B5623A366757F8ACD1908066117DAF8E641BFF82300E35F318A07802F024EE39D8072B42100A08808006D5D3AFEFD783663A02208DBB0C03711FAE5C8D9428C7F05291D0054FA7140CFF16B49E3D63831B0B8360400141101A09D69FD1B5C62F3052A8AB00DF7081EF6AE04302DBC00F04BBF1DF0C5B065A4F93B83B88368356D0800282202401BE9CD7EC6CFF8E415365F400E847D78D73713C0AC5C2D00FCEE3E5548E66DCF26FD676E26D1BFAE265131F348871E332DE7905EFDB9077D08FD2200B471D5B87777B3D90272226C84EE3F0A00C1232500A0CFCCB4BFB8077D08FD2200D437B36FED023657406E848DC04701260601205D0400141301A0AEF93D1E2423E6F57899CD1550026127DEF3CD05301B0800E92200A0980800758D9BF8F19F6CA68092085B31C73719C04C2000A48B0080622200D4336960E38B6C9E80D2085B71BBA0DBBB1AC0342000A48B0080622200D4F170CF87C888B86F6AB279026A20ECC53382BABD5700081C0480741100504C04803ACE9BF4F13C364B404D84CDE8E79B0E60061000D2450040311100CA6B1FFABF136C8E80DA089B71BDE076EF7A00C38300902E02008A890050D6FDBD9E285A32EDF33BD81C012D1076E361C153DE0501860601205D0400141301A09CF9D195C9B459B62E6C86809608DBF1B16F428091410048170100C5440028E7A2C91F25B0F9017A40D88F69BE1901460501205D0400141301A08CBB0735BBC06607E805613F6E164CF42E0930240800E92200A0980800F93DF0E3A364E4C21F1E65B303F484B0218F0AFEED5D1360381000D2450040311100321B5D99C4CEB2F5647303F488B023B86BA041410048170100C54400C8EBAAB1B8CB9F2110B6649C6F528091400048170100C54400C8A77D70F3336C5E80DE11B6849E0F60F7AE0A300C0800E92200A098080079CCEDF554D1AF8BA3EF66F3028C80B027550471EB60038100902E02008A8900A8B8F416BF63677FD686CD0A3012C2A63416BCE85D17A07B1000D24500403111001534FA0132675AC42C3627C08808BBD2CD372F40EF2000A48B00806222002AE6BA5FDFCA6633028C8CB02D137D1303F40C0240BA08002826022078530634C6C57ECC82B02D37086EF6AE0CD02D0800E92200A0980880E0CC8DA95E346151BFFBD97C003320ECCBFD82FBBC4B0374090240BA080028260220700FF77C888C9FF3754B361BC04C081B534BF0B4776D80EE400048170100C5440004687415123BE3F3416C2E80191176E615C102EFE2005D8100902E02008A890008C44A64DEE44E6BD94C0033236C4D946F72809E400048170100C544004877DDA8D62E360FC00A087B33D2373B402F2000A48B0080622200A4B96B50B3D36C16805510F6E65AC1A5DEE501BA0001205D0400141301206ED6CFB50BE2E2BEF8079B05602584CDF9A7E02EEFFA00CD410048170100C544005CDD7DBD9E289A38FDCBC7D81C002B22ECCEBF05D3BD0B04340501205D040014130150BE077F78848C9CDBA3059B01606584EDA92678C0BB4240331000D24500403111007C8FF4A84A26CDFB2A921DFE01F046400DC1E3DE25029A8000902E02008A8900286B7E74153271F6D703D9611F802B081BD450F08C778D80EA2000A48B00806222004A195D89CC991639971DEE01288BB0432D052F7B1709A80A0240BA0800282602A0B895C8D2091F6C60877900CA47D8A28F058BBCAB04540301205D0400141301C0145EF9AF1AFBDE6E767807401C618F3A092202540401205D04001413012018FD00593BE6AD4C765807403AC2267DE69B26A0060800E92200A098960F0061FCD78F7ED3CD0EE700048EB04B5FFAE609280D0240BA080028A6D503E08F11AF1E628771008247D8A69F7C1305940401205D040014D3CA01B0F997578FB2C337001547D8A7BEBE99024A8100902E02008A69D500D83AFCC513ECB00D807C081B35D0375540091000D245004031AD18000943FF778A1DAE01901F61A7BEF3CD15901B0480741100504CAB05C0D6112F1F678769009443D8AA6F7C9305E40401205D040014D3320110FD00F97D54AB03ECF00C80F2087BD55510D70990110480741100504C4B04001DFF91AD3CECB00C807A089B152158E85D2F50611000D245004031AD1000EB7E7D730F3B1C03A03EC26E75102CF02E18A8100800E92200A098E60E804AE4B771EF26B1C33000DA216CD7EB82C2611954040480741100504CB306C0B1683AFEEF6D66875F00B447D82F7A2BE1A3DE2503418100902E02008A69C600A0F7F39F372D02B7F405FA43D8B0EA8279DE3503018300902E02008A69B600C8EFF1209939D3368C1D6E01D01FC28E5515DCEB5D3410100800E92200A098660A80833F3C42C6CDFEA62B3BCC02A05F842DBB5BF02FEFAA01C92000A48B0080629A2500F6F57AA268D29CAF9E67875700F48FB067B70A2EF32E1B90040240BA080028A6190220FBE7DA0553A7767D881D5601300EC2A65D2788FB07480401205D040014D3E801B07B5093B371715FFC831D4E013026C2B6450A5EF6AE1C28170480741100504CE3064025B271546B173B7C02607C847D7B41F06FEFD2012E0800E92200A098860C80E8CA64F1848F36B0C32600E641D8B850418F77ED40191000D2450040318D1600877B3E44A6CCECDE8F1D2E01301FC2CEFD57709B77F140091000D2450040318D140079BD9E2C1A3BF7CBD7D9611200F3226CDD4D8213BDAB07FE1F0480741100504CA3048073C0D31727CCEC713F3B3C02600D84CDA327075EF4AE1F4000042002008AA9F700A0D7F4FFE39756B9EC700880F51076AFB1E041EF025A1C0480741100504C3D07C0911E55C9CCD8AE33D9611000EB226CDF0382DBBD2B68611000D245004031F51A00B931D58B46CDFBEA4D76F8030008FB77B3E038EF125A140480741100504C3D0640F2C0C6E727C7C5DCC50E7B0080E2083BF8A6E009EF225A0C0480741100504C7D054065B266ECBB89EC300700280F610B1F134CF2AEA285400048170100C5D44B00D03BF9CD98D1FD67767803008821EC21FD4860A477192D020240BA080028A61E0220AD7F834BBFCEFBF231765803000482B08B1F089EF62EA4C9410048170100C5D43400A2AB906513DA39D8610C00102CC2365613DCE25D49138300902E02008AA95500ECEFF578D194E99F7667872F00404511F6F17AC118C1023A96660401205D040014538B00D831F4B9D3B3677F7F273B6C0100E444D8C9068299DEC534190800E92200A0986A0600BD91CFFCC91DE6B2C31400402984ADFC97E004EF6A9A080480741100504CB50220A55FC3CB83177D539B1D9E00006A206CE6BB82F9DEF534010800E92200A0984A07407E8F07C992891F6E6587230080DA08BBF91FC138EF821A1C0480741100504C250320A36FDD82B1F37ABCCC0E4300002D11F6B3A5E07EEF921A140480741100504C250280BEEAFF6DEC7BBBD9610700A017840DBD53D0B0E7062000A48B008062CA1D007B8557FD13677FF5223BDC0000F488B0A56F081EF0AEAA81400048170100C5942B00E8AD7B978D6FBB991D5E00007A47D8D3DB05E9A5840D73DD000480741100504C390280DEBD6FF4AC2F42D96105006024845DAD25B8D3BBB03A0701205D040014B3220170E08747C8FCA91153D86104006054846DBD5630F26C4151A17769750A0240BA080028665001105D99EC18F67CFE8A096DEF61870F0080191036B6D2A6C3E7CF9DBA5044F468E6F922922C8C1B14371B0100450C340072FAD42A983DC3D69E1D2E00006624FCF7ACF1E1B30F15D59F7C82E8C9DB47E49290C1D950822F0E5AC23DE843E8576A00ECFFF1313227366A013B3C0000CC4ED578D7CD616BB337D79E9ECF1D632D440048170100C5140B80FCE8CA64E3C856FB472C89B9831D16000056A2C1F6D4A76AAC72E7D59D728C3BCA6A8A00902E02008A597E005422C9039F393F77529766EC300000B032E15BD3DA575F9C7B9E37CC6A8900902E02008AC90B0077EFD0C285933BF5624F7B0000B842F8EF9993C3E71C2AE40DB4D22200A48B008062160F807DBD1E2771D33A2F624F73000028073BB9A1E6DA9C75B5661E51F544410480741100504C1A00477A54231B46BF91312D3EE666F6EC060000711A6CCFFC57F8BAACAD35671CE10EB6DC2200A48B008057D333F425B2704A44CEA8D5A32AB1A7330000044EADDDE9FF0D5B9B935C4BE16F0C2000A48B00803CF70D7D914C9ADBD33D60D99847D8D31700002A4ED816C7E3A1BFE5EC55EAAB830800E9220060710F0D7A8E4C9FFD6DEEA0F5939E644F570000909F069BD21EADB1CA955E679ABC5F1D440048170100A907063F4FC6CFEB99DBE3B74961ECE9090000CA53FFCFECC76AAE72A7C9F58E000240BA08006BEB19FA329930B787EBC755A371A73E0080768427ED7D207443D6AEF05987B9C32E550480741100D674CF88D7C998B898C4BE1BC73DC09E7E0000A03D8F6F49BF2D7C5DF6CAB0390783BA8E000240BA08006B9936B275D184B85EF6E16B26DFC59E6E0000A04308B9263C3E7D7CE8C2FD1779435F9E0800E92200ACE186F11F5F1EB874D0DC98F898EBD9B30B00008C41D8868CCFC216EF3B516FEA71EEE8171701205D0480793D36B0395910DBFDCC0F6BC747B3A71100001897F0ED59CD6BAC7667D79C59FE45851000D24500984FD7B097C994B9D1D9D11BC637624F1B0000300FD553C98D3537664DAAB168DFF97A534ABE2B8000902E02C01C9EEEDF846C9CF051C1A0E58337F4DFF0EBDDEC69020000E626FC8FCCF63556780ED48E3D8A0008500480B1DD3FE405326FFAE7C77E5CF5AB8D3D1D0000C07AD4D99C767FE8BAAC3FEE1A975BC81B3B5856048031DD3CEE83CBC3960C5C17B333EE3EF6F0070000407974614697FF4C731DBD69988B3B7CD02702C038660C7F8D4C9DFDDDFE9875A33F600F73000000E542C8B50F2FCD9876CFE49C0BD70FCBE18EA0954500E8DBFC81FF232BA6743DF3CBE2FE53E352E36E648F6A0000008110E6C8AC547951E65FB78F77155C3B843F8856F385810800BD797C6073B26652E70BC396F45F1383DBF0020080BC3CBE2CABF17FA7E764DC3ADA5D78CD60EBBE33D060D03AEE084175FDBB7F13B265ECFB9727C4FDB87DC0EA51B5D8C314000080923CF8DB9E46FF9D97E5A0EF0C5C37C45A315075D076EE2041E53D31A019593BA9F3A5E14BFA6DFFEA8F69F5D8C31100008016D48C4FAC59292E3BE18EF1AE4BD70F357F0CDC3C389D9CE8DF8C3B50507EF70D79812C9EDAEDECE02503D77E873BF00100803E095BEBF867954599D3FF3DD975EAE6E1E68D81D50322B86305E571CFC85645F4BBFA43970E9AF5FDCAB177B28717000000A3507569E6F7F7CE74EDFBD71877E175267A77C036700C77B860701E1EDC82AC9ADCF9E2C8053189BD7F1BF9117BF800000030038FACCEFCD7238B33A6DC373D3BFFF631AE22239F3B70FF603BC9EFDF823B6650DCBFFB37255BC7BC5F38716E74DE0FAB46FDDA695B1C6EB50B000056A1E6A6B447ABC465AEFBF714F7E95B7E7111A37DB360C0C07EDC718365A5E74CC40FFC98FCF24BFF8B9F4E9CE1FC6CD13C9CC0070000C0C7A3EB339EAC1C97B1E83F5373F26FFBD5557883CE3F32B867701271F57F953B7856F750FFE7C9AAC15D8B068C1A76AE536CDC963716AF09677FCD0000008008845CFBE8DA8CEE95E23213FF3D35E7DCBF7E75EBEE63832683569193FD9B7247D02AD257F7BB06BC47660CFDBE3066F4E8A39DA7CE8F8D5C61BF85FD2D0200000015E7890DFBEEAEB6307BD8FD33B2F7DC39C175F69FA35C45D70FD5F6DE055D064EF0DE5A96378E6673FF801749FCC04E64E2F01F0BA2C78E3BDA7EF69245EFAD5D5B99FDF500000000EAF2E8FA3DEDAA2CCA5877DFCCEC03774DCCB978EB2877919A1F21B41D34C354D70638DCFF39B275E0872476687461EFD1BF9CEE3A65F6EE0FE6ACFA3284906BD81F39000000A05F1E5991D3B2EAA29CB8FFCECCCEBA6772CEE9DBC7B90A6E19E922370CA5271DF2C73C581B0DFE8D24F47F8F3BA87A947E4EFFD7800FC8FC215F160D1D31E0E277E3261EEA1CBB20BEED82DFA230F40000004C4B757B769547566446575D98B5FA8199AEACFF4C75FD7DE778D7A5DB46BB0B6F1AEE22F4AA86817E33E186C119E4D3816349CA8037B9A3AB86A7FA3725EE01AF10FB80B664D5906E455387F5281C3872C885EFC64DC8EF366596B3DDECC553DBCE5CF612FB6300000000008FA77E7734A8B63CEBEBAACB3262ABC465FD59696ED6DEFB66E41C1482E1E4DD935C67EF989073F1B631EE827F8E7615DE3ADA55F48F5F72C82D2372C8FF86AD214306F7232B0676257B07B42AF73C8103FD5F20FBFABFF4FFD2FFDF94016F09BEED7D1B7EC3C0CE64C5A04FBCAFD4A70EED59386E78CCE511BF0CB8F0F3E811A7BF1B3FF150F7C933D33F9E3EFF8F76B3978D7B73EEAAF691C47E03FBD10100BA2424E4FF00EEC4669EFBD18A810000000049454E44AE426082, 2)
INSERT [dbo].[tbl_MstMenu] ([MenuID], [UserID], [MenuName], [MenuText], [FormName], [MenuParent], [MenuImage], [Seq]) VALUES (3, 1, N'frmRE', N'1.2 รับสินค้า (RE)', N'frmRE', 1, 0x89504E470D0A1A0A0000000D4948445200000200000002000806000000F478D4FA000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001974455874536F667477617265007777772E696E6B73636170652E6F72679BEE3C1A0000742D49444154785EEDDD077814D5FA3F70D67ABD62EF1D15761742D3A89480375292DD0D20A851112B286205A9D2143B626FF4DE0925C4801DB1F75EAEB778BDDEF6FBDF2ABD93BCFFF3EE9E68B2FB263BB333B333BBFB7D9FE7F36021B3D99DD9F33D3373CE994628140A854A2C9A96BF3F95773C932A3B77A5E70B06A83FEFA5E73B3DA72C53FFBE4EFDF9A5F227E53FCA4FCA0685A22A3BEDD6FF8DFD43FDFBF7EACF0FE9F9CE6BD59FF3D59F4FA8FF76275574BE5C6DAB3D55B43B4EBF2C0A8542A150A87414AD2FDC8F56776E41950597A9907F500574A50AE53FAB3FF72AB1404F8FED54D1E973F5DA0BD4EF3182D6742EA68A8213F5AF8942A1502814CA4A5159616375F6DD4905EE2845857DAD33772FAAECF47FEA4FFE3D47457FEFB2BC03F45B41A15028140A555F5159E9BED1CBECD14BF89D3F52415AF573B866A62DEAFDACA6CA8241B4B6E369FA6DA25028140A85A2F585BF5267CB7D54582E52A1FFDFB800CD36BF55EFF5615AD3295FBF7D140A8542A172A7F8D2385514F4A28ACE0B55286E8E0BC9DC101D6C58F080FA0C5AEB8F058542A150A8EC2C5ADBC9AFC26FA2F2AF3A6108DF2AA3E8A50E47EA8F0A8542A150A8CCAEE814BDCA4E57A8B3DD3755C855D70A3D48B455994D1505E7E88F0F8542A150A8CC2A1562875045A7C12AD0FE522BE05C57BDBA3DED5A16A41D0B4FA3ED0B4EA69D8BCEA03DCB5BA9FF5790F0775DF68EFAFC4A7960A4FE485128140A85F26ED1CACE27A8307D5C0598A7EEED57AD3E8FB6CD3D9E7E7ACEA7344AB071EA01B473F199A43A2EE2CFBBE83BF5790EE02B29FA2346A1502814CA3B45958547ABB0E2FBFBDB6A859727EC59D15A05FCFE62F0C7DB32F3F0E85502693B2EFB5119C88B21E98F1C8542A1506E17753BE3300A059B50A4595BF5672185FDBDD59FD750243098C2815114F23FA8FE7992FAEFD328E29FA3FEB92C26B846793539FFCA9F7F261C9CAEFE7C4E6D77A2FA7F13F4F66F567FF6A3E26625EACF022A6E91473DFC275151EB83F5AFE8585179E1E17A55BE2DB5C2CA33F6AE3C9B364CDE4F0CFBFA6C9E7928514547717B1EF05D7425446AE4D3BB008542A15076164D68B40F15373F818AFDE7A930BF4885EE6085437CAE0AD9B5CAA7CADF955D0A79D836F57BFFA0FE7C4FA9887642C2817B959B5407A2078502ADA9A4D511FA6D1BAEE8E753D9E96A1548FF8C0B28EFA828A04DD37F2D867C32DB179C226FD3333A7F44ABCFEFA877070A8542A1CC14F56E72389504F355405EAAC270B40AC559CA1BCA8FCA6E450AD46CB595C2C16F5587E045D5E17956FDFB906807A1C81FA470D303F547162D15AC17A810FA223194BC65D7926662B81BB161CABE545DDE4EDCAE8754AB4E589972AADE3528140A85AA5DD133F990BFAB0AB85B55B04D51D62BFF56A420844455CA9FA9B4C53A9ADEEEB742109956B5EA1CDAB534101D85BF6BA93F7A9FDEEE91F85B661D2E86BB513B173715B79BAAEAD51D6877598BE86043B67B590BAAAEB065BCC166D529BB95AFCAE8431E8542A172ABA8B0703F2A69DE9222FEABD459EC632AB4D6293FE910032B6E5501BDCCFA7DF15D4B83B469C6C162E06E98BA7F747A1E07A5F4B3666D98BC8FF83A466D9D7BACB85DB3AA56E5D3D639C7A86D4A33107CEAFF1D4D7B57E68B3F6B4EE7F754272A4F7F1D5028142A3B8B1A35F251281088867DD8FF0C45021FAAA0DA91105C604D690BA2E7CE13C2C69CEA8A8EB475F6D1420026E2FBF656039147F24BDB3663F3ACC3C56D9BB16B49C06047C4473B973413B761D24EAA28188FD90228142A6B8A0AF31A53B1BF1B8583F7A8607A49C199BDD36E69455466C765F902D397E3374E3980AACA53EF7854577410B76BC696594788DB368A6F7148DB6D888DB71D3EA48A0E4DF5D7078542A132A7A838EF487586DF4B9DDD3FAACFEEF724041438A34790E87E3B2E49C7EC58709A1876C96C9E7998B83DA3364C3137FD2FDEB679C78BDB3582C738A4740B42FDCCDE55678BDB4C012FC634507FA5502814CA9B45A58DF6A550B043EC0C3FF8810AA2BD09C104CEBB2A8F68817D73E0F952FC86C9FBCA6167000F9A93B66B44ECBEBBBC5D23762F6B2E6ED7081E3F206DD3882DB38F12B769C1227AB9C8F13521502814CA704517AB0907FAAB33FD65EACFFFD5092248BFDB5B13ADB477243E5FD29642CE281E20276DD7883D652DC56D1AB171DA81D1710BD27693E19FB33600D1E7C46A84DFD19AF39BEBAF1E0A8542A5B7A2A3F443FE4E2A6C262A9F28D53F870FB8A744B9C7B6CBCE75583D0BE7CBF8D2768DDA32FB4871BBC9F0D444697B46EC5E9E7AC7A386952B1F0DE0E98217E9AF230A8542395BD42B700885827D291C5CA5C2666B42F880BBFA0489A63AB7E00D2FAB2B059C19A99E89333E93DE34FD2071BBF5D936EF04715B4671E741DAAE19D1071309DBB641353DDF798CFE7AA2502894BD45A5271FA402BF278502F355C820F4BDEA32759639CFD907DFF0403E29E0CCB0D20160D5ABDB19EE886C5F70AAB80D333CDE01A8311B4F1844A150B654749ADE2F67FADB13C206BCE59A9644CB9C7FF4EDD6B9166F014CDD5FDC6E2A762DF1D3A6198D135F63F23ED181777B579C25FE9C597BECB805B0CC915B007555767A95CABA1DA6BFC228140A65BCE2CEF4B724840C78D3A05644E5E979EEBD95F5F8198F2190B66B05AF2FB06779EB68C8EE59D1D6F2158678B60C02B467896023BEA0551D8FD55F69140A85AABFA22BF015FB3BABE09FA7C2645B42B880B7DD94BEF067D1698016E6E3EF2ECB13B7EB755BE71E27BE1F2378C544699B0EFA1DBD5078B2FE8AA3502854DDA248DEF1147BBEFD5709A1029981A7F9AD1603C0513B163511832E992D33AD2FC5EB96AAF273A24F1494DE5743F8CA013F3B40DAA6C37EC4CA812814EAE78A2ECEC38F840D07CA955C7B3C6E76B9A38DD4E8A78DD9E9783C0FBFCAFB8FE36D103FF8487A6F0DB132FDD0067F47270085CAF1A270D39355688C52F8B9F872A040E6E027F955A4EFB2BFA8A2A3E1CBE29BA61F1C3D8316B79361B81360E44A009FF9F37809691B69F6572A2F6CA29B02140A950BC5CF12A7B0BFB70A8CD7142CD0932D6E72E7B27F7D78E09D34129F6D9C7A00ED58747AB4B320FD6CA6AA2A3F37DAF9910606F27FE3818EFCEC00E967DDD1F98FB4B2F309BA6940A150D95AD1E97BE1E0408A04BE13030432D78DA90FF8E3A7E949FFDD2E55ABCE8DAEB5BF73495375961CB06D0A9E97F1EC005E25902FF3F3D9FEEEB29654BDDAC9CF59EDFBD43B535FD3AAF38ED2CD040A85CAA68A0DEA0B4E50418135F8B3D1552D4DADEBCF81CF67DFBC60CE2F67AABEE8B3F9B7CF3F397A162BFD1C780BAF41C06B2FF0D5949FAF324CD93FFA4C8514D616789BD617FE4A371928142AD38BC24DCFD2F3F631A82F5B5DD29C68B1F1334B0E868D53F7FF393024DC29D8B1F034F1E733C3F9446BBA11BD50AC4412ADEDAEFE7F17A2CACEC2CF7A1F4FB7DC32EB0871DFD5C61D3CBEFA226D4354D9A98C6F0FEAE6038542655AC5EEEF077BAA33FE57C5C080ECD12B4834DBF822327C095E0A8AFAF0636EA5ED78C25A15F02FF7265A7715D11B0389DE1E4CF4EE28A20FEE22FAF01EE3DE1FAB7E6E04D15BB712ADBF5E6D4BE16DAE551D04E9755DC60B1B6D9CF62B717F49F8EAC05E33530D2B3B3FA89B12140A9529157DFA1E3F6E37ECFF63425040767AEE3CB91117EC296B453F4DF68921D1109ED72F6D2FBDD499FA8B3D63C1FCD66D2AE4C7C9616ED647F7117DF230D1E74F107DFD1CD1B733897E3BFB175F4D56FFFF41A27786A8CE461FE1F74AB702DA5CCFA0CA866C9A7690C9711E9DAFD5CD0A0A85F27245CFF843815215087F480808C85E13CC3DD277D38C83C57048866F07B83226802FD1BF7A990AFC5BD459FA7839C053F1D1032AF01F55E1AE02BF76D81BF1F534A24FD5CFAFBF4EFD8EE727FECE0EE3870649FBC8881DE61E7CB4832A0ACED14D0C0A85F25AD50AFEDF27840364375EE2B7426CB845FCBC7929148CDABEE06471BBB6ABFC0DD12B97EAB3FCBBE5004FC547F7EBB3FCA972B0A7E29BE9B1CEC0EB57CAEFC501662EFDC7E3259AA3330584EDD6E32F545978B46E6E50289417AA56F0FF2E211820FB5DDE826885B9E95EFCCC7B29148CE2C57AA4EDDAE68550ECDEBB9D67FAEC9389EA4CFF5939C0EDF4F564F5BB8F245ADB557E7F36D8BBEA6C71DF98B167B9EA380ADB6EC06B5456BAAF6E7A5028945BF573F0630E7FEEEA616ED05F8DCD330F1703C1389FB85DCB5E0813BD39489DED4F90033C25F7AAB3FDC7D519FA3439AC9DC463083EBE9FE8E55EF2FBB580D75190F78D713C0854DA76126375138442A1D25DD127F245CFF883DF8AA100B9E3BED41E1C53DF6A7C66D8FA18DD177BC42EF38B019E2A0EFE471307F2B96216D1A70FA9F75922BFFF86ACB94075202E4CB89AC00B0A49FBC58C140774EEA1E70BDAEBE6088542A5AB28D2AC9D0AFF77C53080DC32A0A5D4381B6264CE7843364CDE57DCAE691C886FDF2184B7459F3E12BB272F86B19BF88AC07D2ACC43F2E751DBAB9710FD692DD1E69F88B66D23DABA99E86F6FE90187D6C771B0941F3E54D9E97BAA283844374B2814CAC9A21EFE9328EC9FA61AFEAA842080DCD32748B424F533F0ED0B4F1103C1A8CDB30E13B76B58A53AAB5D3F4085B59D97FA954F1F76E752BF69AA23F0FE2875765FCFCC8137D467B3F1FFC5823FDE968DEABD8EA5AAD5E789FBC68CBD2BCDCD1CA9A3B2D32CDD3CA15028278A7AE6FF5A35F8FC64BE2D75020072DB23D61E1EB377455B31108CDAB9F80C71BBC9758E8DEAE7C576A4004F993AABFEF26921683D8E0724BED6B7EE67B4FE5AA24DFF94C3BFC6D62DEA3DDF459B671E22EE1F23364E3BA8EEEBA6A6876EAA5028945D556B64FF5F121A7FC86DD79B9BF2579F2DB3CC3DA3BFC6C62907A476FF9F97E175E472FF2475D63F430ED84CF1F1BDFAAA8881F0AFA13A017BDF1926EE2323762D49F1F27F5D3F52596163DD6CA15028AB45A1602185839F898D3FE4B69ECA427B9E20C78BF9F08362A4706808DF7B96B6D7A0574BCD2FC99BCC472A34BF4C61F11EAFFAC33215FEFF96C3BE3EAA13B0EBF581E27E6AC896D947C9FB2925058FEBA60B8542A55A2AF89BA846BE3CA1D107A8719F857BB6823D2BDA98E804F868E762B3D3C6CED7F7FA8500B7E2E3873C3AC82F45DF97136DD920877C32AA13B063DD00617FC936CF3CCCEEC710EFA5CACEE7E9660C854299A9D89AFDC161AA81DF9AD0E003D4B8228F68B5D8005BC24F87DB32BBE1DB01BCF0CF9EE5ADC59FAF175FF2E787EA48016EC5678FA8D09C9518A299CA4AF8D7509D80DD6FDE1A5DDD4FDA7F6CC3947D693B2FFF5B616AF53FA33EE1DB96BA4943A150468A22CDDAAAC6FDA384C61E20DEB3C61FF4938ABD2BCE5201714AB433C00F98D932EB70DA36EF44DABD4C753C84BFDF207E2A9FDD97FCD997CFC8219AA9EC08FF1AAA1350FDC158DAB9A4196D9D7B4CF4D1BF6CEB9CA3A3576EAA57B793F7955D2A3B5DAD9B35140AD550E9D1FD1395BD751A790009AFF52F35BA5E14BDDF6FE3BAFD51F7A6F6B01E2FB333FC6BA84E00CF0E10F78BF3FE492FB43B54377128144A2A0AF923AA51FF31A19107909428F36DBD67EB9CD7AEB079195F851FD1FBF51439440DAA569D873D1F8CA3DD6FDF4EBBDEBC29C1EEB787D25E5EA0275DAB06FE71456C3EBF14E256F18241EF0E91F78FF3EED7CD1C0A85AA5D14C93B9E4281F962230F509FE16DA486D6633A13BD7E9D1CE056F03AFAFCC85D2944939A45BBDFBC85B62CC9A39F26EF2BDE178FB7617A63DAB6AAB3EA0C3C206CCF463FFD550E6FBB6CF83BD1DA6EC27E72DC56AA68779C6EF25028546CEDFEE08DAA31DF90D0B80334841FF6B3D8EB67FF2AFCDFB8410E702BF891BD29CEEFAFFA74126D5E98FAB3F27F9ABC0F6D7BBE3BD1B70EAC2FF0C7957268DBEDC3B1C2BE4A87CE4FE8A60F85CAED8A9EF587036B131A76002346B5151A588F79FDDAC4F0B68A2FFBA7B8A4EF9E0FC646CFE4C5603769F3E22055F3A37D85D749D95F5E9303DB6E3C4D52DA5FCEDB412F149EAC9B40142A374B35E09728FFADD3A00318C567FF16D6FB4F0B5EC6560A704BEE4DF99E7FD5678FA8F03F540CF3546D59D2CADEB1013FBE2C07B6DD78C684B4CFD2E339DD0CA250B955146E7AA87E708FDCB0031831D2E367FFAF5C62FF80BFE868FFD4CFB8372F6A2686B8553B5FBD427CBD94FC6E516CB4BE14DA767AEB4679BFA5C74E5AD9F904DD24A250B9515412ECA81AEF3F2534E6006644D4D9BF4D4BFE3AE2A50B1D98EAA75898E7CF0BE148E16D870D330E89CE24905E3725FFFC520E6DBBFCE37D79BFA555C103BA5944A1B2BB283F7F7FD5684F508D37E6F58375B7985C75AFB6541ED463068F2EB7FD697ECA678FC96169D096A52DC5F0B6CBAE7503C4D74DC977F3887EFA8B1CDE56FDF74F442FAB0E9AB4EF5290D2839F627EC2838250595F54DC220F0FEF015B4D6F2F35A8A2BD2BF3A32BF66D9A7E900A2A5F34AC7849585ED12FF6C4371B977EAD3C9FE8EDA172805BF1C94372501AC467E73F4DAE7F195C3B6C5976B6F8DA29FBDD7CD509F8510EF154FDEF07A2572F91F79D41D5E5ED68C7C25369D38CC6B461F23ED1F7BE61F2BED15505772C3A5D7508CC5C99EA7C9B6E2651A8ECAAE8F4BE887FA86AB0772634E000A9BAC6E8D2BB05B46DFE89AA818E857E7D364DFF35ED5D61D37882F5FDE500B7C2C274BF1A7B3FBC5B7CEF76DA38FB38F1B52DB1B3136043F8EF5C744634ECA5F75F831F1EB57B5973F1E71375FE2351239F6E3251A8ECA8D840BFC08A84C61BC0AA87F38586344E4547754676B8D8404BF84C6E77594B795B46BDD45B05B6DD83FE141BA6D9ED7E7B88F8BEEDB461EA81E26B5B664727801F23BCFE5A79BF19B46DDE09E2FBAECFCEC56788DB49747E17DD6CA250995FB107F8F8FF2836DE0056F40A12AD4C7EC97EDBDCE3C446B9217C5BA06AD539E2F6925AD395E8837172805BF1F9A372289AC4CBF94AEFD95E3EF1B56D61A513B0E93F447F28539FE563F2BE3360E7E2D4164D32F8A0A8C5BAE944A132BB28E2BF4A35D4DB121A6E003B0C4BBEECEF9E156DC5C6D8882DB38F12B799D49BB7C8016E052FF663D3FCFA8CEF00B0543A0135E15FB38DB76E96F75F03F82981FCA860F93D376CE3B483D43692765877D10B9D8FD14D280A957945E1A6075224F094D86803D86546F247B6F2E35DA5C6D828D3570178CA9F14E0567DF56CDD00B4202B3A00CC4C27203EFC198FA5585B2CEFC77AEC58789AF05E8DDBB534286EB7AE8221BA2945A132ABA867DEA9140E7E2036D80076B9AC85D070C62B48F96CAD060FF492B72DE051FFEF8D9203DC8A4F1FA91B5C16654D078019E90448E15FE39307E57D598FCD330E11DEAB715BE71C2B6EB7AECE1FE9E61485CA9CD28FEEFD5F42630D60B77167090D675D7CF62E35C266F0602F69DBA2D7AF9103DC8A8FEEB5FD213B59D50160DFA94EC07FBF97C37FC33F54F82F957F2E6A56ECB1CCD2FE14F0D810F9FD1AC31D0869BB09CA3B9EA99B5514CADB159DE2170E8C52AAEA34D2004E99937CEEFFDE1567898DB0195BE71C236E3BC1DA2215D80EACF6F7F91342685993751D801A3FBE42F4EFDF12FDEF47A2FFFC9EE8AFEB55E760AEFC776BFBF229B50F3B27EE5301BF2FF9FD1AB3293A0E40DE769C51BA7945A1BC5B5498D75835C815090D348053FA1AB9FCDF89AACACF131B613378D12069DB09DE1C2407B825F60DFCAB2D6B3B0056BC3150DEAF71785EBFFC7E8DD93CF33071BB890A3ED34D2C0AE5CDA2A2334FA148E00BB1910670CA18E30BF56C9C7A80D8101B155B1D50DEF6CFD6765761EDC09CFF2F9E94C3CA227400045F3F171BC321EDDF5AB6CC3E4278AFC6F16254D27665E79FAE9B5A14CA5B45C5FE36AA31FE6B42E30CE0B469C947FFD730BB604B1D93F7892EF52A6DB78EF5EAEC510A702B3EBE5F05D3ACC4A0B2013A00F578FB1679FFD6B2737153E1BD1AB767792B71BBA28ACE37E9E61685F24E51C8DF4735C498DF0FE9776173A2D54263598FAA55E746835C6A8C93D93EFF24719B754417FD71E0DEBF4367FF0C1D807AF0544B691FD751401BA7FD4A78BFC9F1F301E46DD6A3B273856E7251286F1445028355438CC17EE08E5BCD3FF98F1FCA2235C80DE1C15AD5AB0D3C6468FD0039C0AD72E0DE7F0D74001AB0EE6A793FD7B27B794BD5A934371870C3E4FDA20FA292B6D780ADF442F840DDF4A250EE151516EEA71AE0C9090D32403A4D4A6D795E3E9B971A66099FE155AD32D058575EA0CEFEEF9203DC8ACFEC59F2B73EE8003480975B96F6759C5D4BFDD1F728BFF7BA38FCF7A84E83B49DA42A3B77D54D300AE54E5149AB2354E3BB2EA1310648B785661EAF5AD7AEA501DA38F540B191AEB175EEB1C6CEFC19CF1F9702DCAA6FA6CAE1641374009278A987BCBFE3EC59D19A36CD385878EFBFD83CEB70DA6BA433599FCACEF7EA6618854A7F5189FF0CD5F0FE2EA1210648B78B8C2CA3DAB0EA8A8ED1E558794536BE27CBCF71DF32EB48DAB1E034F39768DF1D2107B815169FF56F043A00497C385EDEDFF5E087FCF060D3CDB30ED3C7D3E1B47DC1C9D10E82F4F7CD2958A79B62142ABD4525CD9AAB86F7EF090D31801B6EB1A341B5C98B91C4F0B6C3974FCBA16423740092E02B300617064A83AD342D7F7FDD24A350E9298A346BA71A5D2CEB0BDE71DFD95203E90EA706FFF1036AA450B2113A0006BCD657DEEF6EA828384737CB2894F345A160A16A70372534C0006E9A6EF0DEBCE3D4D9E17BA3E500B7E293897218D90C1D00033EBE5BD8EF2EA9EC74BB6E9A5128678B22C11EAAB1DD91D0F802B86D65D267A9A7C78B2572805BF5C5537218D90C1D0003BE9E22EF7B3754769AA59B6714CAB952E17F856A68772734BC006EBBDCD8FAFF69E1C453FFD8B7D3E530B2193A0006BD7A89BCFFD3EF63DD44A350CE1485FC37AB86160BFC8037796900E03BC3E400B7829F4B2F859003D00130E8FD3BE5FD9F7EDBA9AC745FDD54A350F6966A60F951BE72C30BE005E3CF921AC6F45B5348F481030FFEF9EC3139841C800E80415FA87D221D036EA82808E8E61A85B2A7A2CFF10F051F131B5C002F79E25CB9614CB7977ACB016E15AF432F859003D00130886764547A643A6065A73EBAD946A1EC29D5B04E4C686801BC6896476600BC7EAD1CE056A561FA5F0D74004C78E562F93848B7CA4E4375B38D42592F8A041F121B5A002F2AEB28378CE9F6D66039C0ADF8E83E397C1C820E8009EF0C918F83B42B785A37DD2894B5A270F01EB19105F0A25ED69700B6CDFB63E510B7E2D347E4F071083A0026F0E04CE93848BF4ADD7CA350A9970AFF6162230BE055FDF2A40631FDD6769503DCAACF1F97C3C721E80098C04B334BC742FA7DAD9B70142AB5528DE99084C615C0EB06B6921AC4F47BB9971CE0567DF98C1C3E0E4107C0846FA6CBC742BA5576DAA89B7114CA7C512870BBD8B80278DDE03672A368C0DE9567D3EEB216B47B5973DAB3426DA7C2C25882572F9703DCAAAFA6C8E1E31074004CF85659D3453E1E0CA82A6F477B96B7A25DCB82EACF96EADFCF13FF9E01D5549677806ECE5128E3A5C27F806A48AB131A56804C30AAADD420D6AB7A7507DABEE014DA38F5808460DA3079DFE8A380AB569D23FE6C839C9A0190A615006BA00360D22BE65704E44E273F6E5AFA6C36CF3824FA486AE9E71A54D1EE38DDA4A350C68A42C16B55238A15FE2073DD65FC29807B57B415833FC1E47D68E79266E236EAF5E62039C0ADE2B34C29781C820E80496F0E948F0741754547D5C13C5AF83C126D997D64B4B32A6D475690A79B75142A7951D87F996A4011FE90D91E3276B6CEE1BF4105BBD4D8D667E7E233C56D899C9802F8F10372E838081D0093DE1B261F0F090A68F3ACC385CFA27E7C35803B0DF2F6E2549C7FBE6EDA51A8868B22CDCE578DE7CE84C61420D33C9A7C15403E93DA38F540B1916D982FDA7190B699E09DE172885B91C66700D44007C0A40F46CBC7439CED0B4E163E87E4B6CD3B41DC9EA0876EDE51A8FA8B8A5BE4A986734342430A90890C2C03BC63C16962E36AC4E699878BDB4CF09E0A0229C4ADF864A21C3A0E4207C0A48FEE968F875AAACACF357DF5E9679355277465BEB8DDBA3A5FAC9B78144A2EEA1E3851359A3F2634A20099EAE9E423A7374EFB95DCB81A646850A0138B007DF2B01C3A0E4207C0A48FEF938F875A762C6C227C06C66D9F7FB2B8DD387D75338F422516859B1EAA1ACCCF131A50804CF65CC31D000E6FA9513563E7E2A6E2B6EBF860BC1CE2567C3A490E1D07A103609281D500B7CC3477EF3FDEA6198DC5EDD6B1BAD375BAA947A1EA16E5E7EF4F61FF2B62030A90C9A634DC01E079D652A36AC6F685A788DBAEE383BBE510B722CDCB003374004CFA4C75D2A4E3A1968DD30E123E03E3364CDD5FDC6E1DF7E76339605462451FEB1B0ECE131B4F804C37B59DDC206ABB97B7141B5533780097B4ED3AD0013021D73A00D66E416D98B29FB8DD3A86B7219ED6AD9B7D142A56AA917C38A1D104C816CF367C0560EFAAB3C546D50C43D3013FB84B0E712B700BC0FB3E4D7E0BA0BE457F8CDA34FD6071BB75DCA13A00E1C06E8A34EFAE9B7E54AE97EA11DE98D0600264132383008D2CFED3005E3258DA6E1D1F8C9343DC8A4F3108D0F33EBE5F3E1E6AE15B48F2E760CCB679278ADBADE3F6D635DF894D140AB4D61180CAD5A2B03FAC0E86BD3F379400D9C8C034C06DF34F141B562336CD3070F6C5DE1F2387B8159806E87D1F4F908F875AB803297F0EC6EC596EE07917B7FEDC01603F5224EF781D05A85C2BEAD1BC993A0830D71FB2DF634616026A471BA6EC2B36AEC9EC2E33F8B8E1771D5808E8632C04E4791FAA8E9F743CC4E1674CC89F45C3B6CC3E4ADC5E829B5AC57F373EA5A2D607EB4840E54A51AFC021140E7E1B77300064A789C69602E620E7E0911AD9FA6C9F7F92B82DD1DB431203DCAA8FB014B0E771C74F3A1EE2545774A04DD37F2D7C16F5DB38ED40AA2E6F7890EBCF06B494BE1FE53C085C47032ADB2B36E23FB03CEE2000C85E138C3F0C881FBB6A7445B6D8C8FF82846DD44B05A718E296DC2B878E83D00130E92DB5DFA5E341C08FFBE5F5FDE5CFA42E1EF867EAA994FDF2E4EF47383846C7032ADB8B22C109F2410090A5EE3C4B6E10EB51B52A5F3F914DBE1AB0794663DA53A6CEA6849F6DD0FAFE4280DBE0DB9972F038041D00935EBD4C3E1EEA53D191762C3A3D3AB75FFA6C78CA1F2F5D6DF82140352E6E2E7F3FA20F7CF3877544A0B2B52812B830B6B3C58300203B0D692D378849F0B8007EEEFA8E454D68C7C25369D79266E6CEB8E2BDDA570E70ABBE9E22078F43D00130696D77F97848AA20BA48154F31DDAE8EBF9D8BCED01D4F13579D6AEB1194BF1F31FFA312FF193A2A50D95654E40FAA9DBC316EA70364BF41ADE40631DD5EEE2D07B8555F3E2B078F43D00130E19BE9F2B1906E2B55A741FA6ED416097C413DF37FAD2303952DA5D7F8FF6DC20E07C805571B1CA5EFB435EA4C500A70ABBE78420E1F87A00360C257CFC8C742BACDEF207F37E245020B746CA0B2A16842A37D28125C23EE6C805CD02728378A69D799E87D071E08F4597A97034607C084CF260AC7810B78354CE9BB21890406E9F840657A5128709FB8930172C90A9303A69CF2CE1D72885BC12BCD49E1E31074004C78CFD81440C73D748EFCBD90EDA270F3F63A4250995A140EFE46ED4CACF40730BBBDDC30A69B533301BE9921079003D00130E1B5CBE5E320DDC69C257F2FEAF77F54DCFC041D25A84C2BEAE93F5AEDC47FC4ED5480DCF454F2D500D3E2958BE400B7EAABF40D044407C0A06F55A76CCD6FE4E320DDEA2E036CD47A2A2CDC4F470A2A534A2FF65311B7330172D73DF972C3986E6BBAC8016ED5E78FCB21E40074000CFAE249F9187043BD8B002511F2DFAD63059529A576DC90841D0990CBF8496852C3E8867747C8216EC5270FC921E40074000CFA70ACBCFFD36DB552227C278CD943A160071D2D28AF174582ADD44EDB11B71301725BBF1672E3E886D71D1807F09192A61501D10130E8B5BEF2FE4FB739EDE5EF845191C00F3C955C470CCAABC54F76523BEB3B712702E4323E035A95E20A6A767BA9971CE2567DF98C1C44364307C0806FA611559E2FEFFF747BD4D40C807AF8E7EA984179B57827C93B0F0068B6C1A7A6398ED703182B87B8159F3C2C8791CDD00130E093FB85FDEE12F3330064A1605F1D3528AF953AF3BF54DC690010F390470602B23706CA216E551A6E03A00360C0FA6BE4FDEE06F931C0A9D84091E6A7E9C84179A528DCF44CB57336C5ED2C00A86D701BB98174C3CB4EDD06707E3A203A004944D7FFF7C8E57F1E00D8F043804C0ABE49A58DF6D5D18372BB624BFD06DE92771600FCEC327B0602EE5D7956F40981DBE61E4F5BE71E43DBE79F147D6A60754507F1EF8B2AF936C09D72885BF1C92439946C840E40121FDF27EFF37A54AD3A37FAC4BF6DF34EA0AD738EA16DF34F8C3E79B2BADC865B56D32D0E001405C7E8F841B95D6A8760CA1F80514B525F1278CF8AB6B479E6614258C5449FD3AE3A06861FD7BAEE6A39C4ADE2335029986C820E4012AF5E2CEFEF381CFC5BE71C2DBCF71A3ED5293891AA575B58C5F29EB3E5EF81357B28D2AC9D8E20945BA52FFD6F8BDB3900509FC7535B1170A73A23E306596EA8EBE24E82A1467B6D3715D8131203DCAACF1F9383C926E80034C0E0E23F7BCA5A453B8CF27BAF6BE3B45FD1DE95298E5FB9A995FC3DB0EE0F7874B08B15BDF41F0EBC11B75300A02177981F07B07B5973B1616EC8E69987125518B8DAF0E62039C4ADF8E85E470703A203D080376F94F7732D7B579E4D1B261B0BFF1A1BA71EA83A95266F09F0FDFF5E76DEFF4FF0888E2354BA8BC2C15B851D02000DB9B8B90AE6B886B2017C266FB6B1AEB16321DF0E90B7FBB3B5C5441F387015E0CBA7E580B2013A00F5F866AADAA7C907FF6D9ADE5878BFC96D9D7DB4B8BD7A4D36F108E0D4ECA548E01C1D49A8741585824DD487BF396E6700801126D603D8BEE014B13136822FF11A1A18F8E62D72885BF1F10344DF0A2165037400EA61E0D1BFA95C4DAA6DEF8AB3C4ED8A46B7958F7F7B7D49F9F9FBEB6842395DFA413F2FC7ED0400306AC2D9728329D838ED20B121368A1B7C69BB75BC1856A1EDC45500675606440740105DF9AF50DEBFB5343CE82F39EE904ADB15F1F2D7D2F16FB790FF4E1D4F28A74B9DFDDF28EE040030E69A3CB9C18CC3F75CA546D80C9E22286D3BC15BB7CB216EC5C7F7AB709A95185616A103207867B0BC5FE36C9C7A80F05E8DDB3CF37071BB091676908F7D67ECA49266CD7544A19C2AEAE13F497DD81BE23E7C00306B41F2017A3CDF5F6A84CDE07502A46D2778B1C499B1005F3C25079605E800C4F96AB23AFB37F6DC7F7E5FF2FB3566D3F45F8BDB4D70B723D3FF1AF2065F9DD6518572A2D487FC52DC870E00A930701BA06AD53962236C062F16246D5BF4C60D72885BF1D17DB6CF08400720CE1BFDE5FD29D830795FE1BD1AB769466371BB09FAE5C9C7BDA3FC37E8A842D95D14F25F2E7FE80060DAD5C96F03545774A49F26EF2336C446199A0950A3F202671E12F4F9E37270A5081D805AF8B395F6653D36CD385878AFC6199A09303FAD97FF6BDBC857A97564A1EC2A5E70417DB83FC67DD8006005379452035ACB965947880DB1517C1B41DA6EBD5EBD4C0E71ABBE9E2207580AD001D0F8CACA4B3DE5FD588FED0B4E16DEAB71BC44B0B4DD3AEE4AFBE5FFDA56E8D842D95514093E247CD00060C5E8E4E1BC7B590BB1213622BA1890B0CD8675267A7B881CE2567CFCA00A2C7B0604A203A0BD9F7CDA5FBCAA72BEAD94DA38800D53F7A7EAD506A6955EDA5C3EDED325E4EFA3A30B65B5A838AFA9FA5077267CC800604D1FD55096275FB77FCBACC3C506B9613EDAB322C5A70FBE1022FAE06E39C8ADE0656AA52033091D0085A758A6F8C4BFEDF353BB0AC0CB514BDBABE359C717FF31E2AF5826D8A6521FE6DAB80F1700ECF264F26703F06A809BA69B5B0F60E7E2A6E2B60C7BB5AF1CE296DC4BF4F55439D04CD8FDD66DE27BB6D594FDC4D7F68659442FF791F79B111505B465B6B95B4BFCA440715BF16E716CED7FB3C6EB0843A55A14095C287CB0006097812DE586340E77028C5C09E06583F9D1C0D2364C73E2390136DC0AD8F3E178F1BDDB69E3ACA3C5D7F684776F97F79729052AD48F17DF7B5D3EFD8449691B719615109508C7B83BB653CFBC537594A1CC161536F995FA10FF14F7A102809D22CA3CE38F5BDD5DD6427704EADEC7E527B6F1A55D4B8F6E8DC72BCBBD3B520E722B3E9D24079B41D55F3E4D3F4DB6369F3D992D4BF2C4D776DD27AA0325EDAB14ED5DD136BA3AE0862975A707F2FD7EEE20F05454E9E744EE0EFE4B14092CD07186325B14F2DF2D7EA80060AFA16DE506B5013C45901FD1CA6BB357959F27FE1D5BF078800F9D180F606D81A0CD0B9BD6092CBBED7CF50AF1755DF5D573446B922FF79B9A82E80041EE10C48EA7E46353EAE0B12C17B93CF82F51B5CAB14E3AD250468B2F9DA80F6F6BDC8709004EE811245A6AE0D1BD6E71626AE047CA5793E5A03360E76BD788C16D870D530EA0AA2F9E105FD735DFCE207AA997BC7FBCE0A17CF9D876DF27FCE87A1D6D282345E1E02AE1830400A78C37FE802057BC7EB51CE456F02A81DF4C97032F996F67D2A6392788016ED5F6353DE4D774CD4CF5F95F29EF17AFB8224D0FFE4945C47F958E3654B2A250B050FC1001C0397D82442B4C5E764DB7F503E420B7821F1894622760EF877745CFD6A5104FD5A679A752B58D8B1659372BB644B3B43FBCE28973E563DA3BFE4E45AD0FD611876AA8D487F55EDC870700E9C00F50911A58AFA8EC4CF4E64D72905BF1F103B14BDC6200366CD71B37D14F93F713C3DCAC8DB38FA5AACF1F155FC735EFDC26EF0B2F7165DD7F934281FB74C4A1EA2B5E4149FCF000C0791736272AF3F85580CAF389DE1E2C07B9153CBA3DC58706ED79FF4EDA38E33031D48DDAB2B425557FF58CB87DD7BC3F42DE075EF2E839F2B1EC3D3B28146CA2A30E155F54DA685F0AFBBF113E380048977126D7EE77037702DEBA450E722B3E7E28E54E005FB6DFF1421FDA38D3CC02373EDABCE04CDAFDF6EDE2365DF3ADF26E06847F85EAACF6F5F0BDFF448B74DCA1E28B2281EB840F0C00D2A95730B6A08AD4E07A4A67A2F503E520B7E223BE1D90E2C0C0A859D1858276BD3E8076BC78116DAF8CD4B526423B5FEE4BBBDE1844559F3F26FCBCCB7891A4B76F153E6F0F9AE8D991FFF5A9A250A0B58E3C544D5169DE01AA03F083F0810140BA0D4D710DFFB4539D80D7AF9183DC8AE8EC00EB4B06671C0E7F13CFF677D54AD549BDD873F3FE8DC0D302E38B42FE3B840F0A00DC32CBC615FD04BC62202F19CC8F87DD36F738DA36EFC4E8F303AA56257F364182D7AE20FA60821CE6A9E24E80A746E33B8C6742BC7AB9FCF936801FF8B463C169D155FBB6CD3D9E762C3C95F62C6F15BB3C2FFC7DDB8C6E2B1FB7DE574DE1A667E9E84351615E63F5A1FC2BEE430200375DDB5235E242C36B51754587E892C11B26EF23DC138FE1A5614D2D01CBF8B9F4EF8F95C3DC8ACF3DB6208F13BE7C9AE8C51EF2E75A0F5E127AD3F45F8BFB8FF1D2D0B63D1322DEC20EB1C5ABA4E336333CAFE30F4591E004E1030200B73D6E328493E0B3FB4DD30F160323DE8629FBA93349630F2AFAD99AAE443CA04E0A722BF8D901290E0EF4B659441FABF7C7832AA5CFB31EDB179C22EE33095F19B0FD6AC0A0D6F2F19A4922CDDAE908CCDDA29EFEA3D587B129E1C30100F7F5E16981F62C11CC67FE0D9D314AF82A01AF0D2F6DAF7E7A5C80EDB7041EC8AE7101DCA17963A0F0F9356CC7C2D3C47DD510BEBD236D2B258F7B7ED11FA35ED23198BBA5CEFE1F173E1800F08A3BEC1910C8F7F9A57048863B0DA61F0CC35EBA90E8BDD172985B910DB704F83903262FF933EE8C49FBC888DD6526AFE64856A8CEE8251939F04F96CB0F0AA2AEC1A3D4878007FE0078DD73EDE406D920BE9F1FFFF86033762DF18BDB4DAAF237FA6A80CD4F13E49503BFCAC00182BCDAE1DBA9AFECB765D691E2FE3162F38C43C46D9AC29D51E9F8CC5491C05B3A0E73AF786944F14301006FB9AC456CDA95D4281BB063611331148CE2E091B66BD8DA2267C6067CF6586CEA9C14B65EF389EAB4ACED2E7F3E065457B457FB22F54E1C333DB0B336EE8446846333D395042ED091983BC50F46506FFEBF091F06007893855B0156CE1C190F0894B66B4E67A2574A89DE1B238779AA78BAE0174F7AB723C023FC5F333FBD2F1E5FC297F68D19BB96A8E348D876523C0E259B2EFDD7160ABCAB6331778AC2C1E1E2870100DE95E2AC00BEFC2B058219D515F60C468C8E78E7F9EE764F19FCE8FE5847400A613770F0AFBF4EFE0C52C0B761A4FD62C68E45A78BDB4EEAD62C18F5DF9090BFAB8EC6EC2F0A373D50BDE97F247C0800E06DFCB0A045E68378F34C6B0FCAF969B24F6DC7E6A9643C3E801710B27BA0203F5E98C3D7AD2B02DC09596FFF6A7EBB9605E57D63C2CEC5678ADB6ED04319F3B01F0BFCAFE878CCFE526FF606F9430000CFEBDF9268B5D05037606B8A33006AF0A232D276EDC1B7062ED14F18B471EAE047F712F1A37DBF9E2607B5AD66127DFA60EC7D88EFD13A2B33006AEC2ECB13B75DAF39ED897A0AC760362A09E6EB88CCDE8A3DF12FF0FB84370F00996394B9B9F9BB9606C440306ADBBC13C4EDDA8E070BBE7EADFD57053E9D48F4D5B3F65F15E02B0DEFDE41B4A68BFC7E6CB671EAFEE2FE3182D77430751B87A7FC5D9EA5F7FD25A1C0321D93D95B14095C2ABE7900C82C938C8F07E0867FE3D403C5603062EF8A743FA2B833D18B25B17BE8EF8E94433D151F299FA8CE0007F73733E4506F082FDEC397F8F939FD2FA8DF4FFCDD9DB37DC1A9E2FE31C254278E97A0BE25CBEFFB27DA4B3D9A37D351999DA5DEE4A7716F1A0032115F9A9D6DFC81413C025C0A8664A24BC90ADB4BAB35DD895EBD8CE8CD4144EFDD29877B2A3E7E88E8F3C7635707A40E01073E7716B8E3F0E68D693BD3AF4FF5EA0EB4719AF98EDC86A9FB53D5EAF3C46D8AEE521D3EE998CB7691C0541D95D957140A168B6F1A0032134FCD5ADC416EC405DBE79F2406447D36CF3C549D0DDA34FADF4E1CC4BCD2E06BFD62C1FCF61DEAAC9CA717A6388680172A7A6F14D1BBC3D47646C6FE7CADAF7A9D42F9F55DC45763364CD957DC5F12BEF4BFA7AC95B82DD11359B3D46F2AB6F3F2F83A32B3ABD49B5B17F7660120D35D9D676A91A0D8A240C91794D93AF718FBA6FEA50B4F315CDB8DE8C570AC83F0CAC5313CED90BD72A9FE6F17A9FFDF83E88550AC3351D959DE9E47ED5D75366D9A7E90B8DF6AE3AB05FCB860691BA219ED7267D05FBD82637464664F5149B3E6EACD5527BE5900C87837AA333C1333037845387E36C08629750795F1D9E296D947C69E272FFC1C784941745ADFA6198DEBEC43C6CF6FD8B1A889B90E1C3FE2F7A21C1AF457BF7F5069DE013A3AB3A328EC7F4678A300902D86AB333D1EBC2535EEF52AA0AAF273D459625BDABB32DFC1CBFD05D1ED73C762EFCAB3543019BF6D91C9AACACF8B9E81EF59D13AFACFD2DFB143F5EAF6D1CF75EFCAB6545D9EC2732396E6D888FF6422C12B7474667E51615E63F5A63626BC4900C82E23CD3EBAD7597B57E5D3D6B9C7469712AE7B96EAA32DB30EA7DDCB4CCE4BCF007CD6CD2BEE6D9A96F8E8E58DD30E8A3ECED7531D209EEE77554BF978CA5D9FE8F8CCFCA27070A0F00601201B8D4FF7943D59F4B9F593938F35D832FBA8ACB922C08BF518996EB971EA01B467B93D8F79B6644501D135AA13261D4739AF797B1DA1995DAA03F099FC0601202BDD73B6DCE0A7C9B679278AC1571FBE8F9D71030EE370A0F3180AE9FD897874BE9B632D78E0687FF5FAD2F1034411FF1C1DA1995B14F27712DF1C0064B731EE5C094875D5C1AD738E15B79709F81EFCC6290788EFAB213C08B32A957BF656F165FFEB70D93F89ED549C77A48ED2CC2CF52616C5BD2900C815A3D3DB09B0BEE2A0B7C63018B57DFEC9E2FB31226DCB2CD758CE97FD11FE8684FC77E828CDBCE2050DD49BD891F0A60020778C4865768049EBFA117D378FAA7E7899767DBB88B6ADBF85364C4B9C9E968C53AB0E56AF6E179DEDE0C86D868A026190A371BCA84FDA6E7FF068FF7E2DE4E30404FE3F52A3463E1DA99955AAF772A7FCA60020A7F0BAEEE5363FCEB7062FA3BB750BD1B66D7554FDE77BDA5C76AE187AF5E1AB07E26BA480A736F26D85F870E60574B62F3C257AD95EFA39B3F62C6F5D67FBA9D85DA6CEC8856DDB6A9E7ABF9722FC53D045476AE6144D68B40F45023F086F060072D1B57944CB6C3ED3FC764642F0D756BDF1DFB479656731F4EAC36B0688AF65943A9BE62B09D2B66BDB3079BFE87805711B26A4FA8C85DA762E6E2A6EDB3653CE23BA10F3FC53B448C76AE61415372B11DE0800E4B2CBD419E07C9BA6DC2509FF1A663B01D6A60416D0E6998789DBADCFCEC56708DB316EE79266E276CDD8B9C8DAEFD0A047CE212A118E05306A3BF56E72B88ED6CC288AF8570A6F040072DD8541A2A7CF95C3C22883E15FC3682760C3E47DE5D733C8C8997F221FEDB170097E77590B619BE6EC5AAAF689B06D4B2A0A62334122C23100E644028374B47ABF78EA82FAA57726BC090000C6A190EA344193E15FC3482760CBEC23E4D734809F92276DD3085E3B3F1A98C27693E1B104461EAED410DB97092EEB487403E6F8DB2612F850C7ABF78BC2C15BC537010050DB4D2A24784EB81422127E3EBE10EE4625EB04EC5AEA975FD780D4CEFE7FB17B79EA5701B6CC3A52DCA6117CCB42DA66CA66B4272AC5603FDB8502AD75C47ABBD42FFB51C22F0F0020296D4E34D5C0623416C3BF467D9D005E3BDFCA00405E5E377E9B666C9F7F92B85D2378FD02699B46F02C02699BA6F154CFFBF371BFDF31FE4775C47AB728DCB485FCCB030034801F2454DF23856D0AFF1AF19D005E42D7CA22403C8FBE76A8A682CFE2A56D1B157DF681B0DD86F07444695BA62DEE40743D16F771D83FA8B4D1BE3A6ABD5914093E24FCE20000C9F10A71F1B3046C0EFF1AD51BFF459B97B78B2E87BBA7CCDA9AF8BCD08F14B066D871297EFB8253C46D4BAC5C71A8E3B173897A07E5FD0976F3EE9A00BC6291FA05FF1CF70B030018C797907980205F0DF8E04E31BCED52FDBFBF5055E50572B099647520DED639C788DB358B17F5D934E360F13518DFEAE09903D2CF9AB2A423D12D18E89756A1C00C1DB7DE2B7E7CA1F84B030098C54BC6FEFD4B31B86DF5E5D372C099B479C62162E01A65F75CFCBD2BDBD28E45A7D3B6F92746ED58D4C49E671DF0BDFE89E710F5C259BF0B3650B8E9813A72BD55EA977B22EE97050048CDC5F97260DBEDAFEBE5A03389C3560A764326FBA86A95C5B511D261463B3CBFDF75FEDE3A72BD53D1A57FC381BF27FEB2000029E8A702470A6CBBFDFD7D39EC4C8A3E89705A6A4F2274EA2144B6E1CBFD43DA60511F2F080596E9D8F54E51285828FEB20000A9286941F4AFFF2787B69DFE5026875E0AF8FE3B9FCD4B215F1F7E38905D0F06B21DAFD130FE6CA29EC2FE01B76CA1D2930FD2D1EB8DA2B0FF19E1170500485D596AABFE99F2E6F572F8A5881FCEC3D30AA5B08FB769DA4154B5EA1C713BAE5A5940748F0AFE3EB8CFEF4991C0853A7ADD2F3DFAFFAF09BF240080153C0EE00FDFC8C16D87EFE6CB0168110FB6DB3CF35031F4637CB46DDE09DE3BF35FA183FF223CB9CFD34281F93A7EDD2F8A346B27FE92000056F5EDE84C27E08FAB882ACF9783D026DC11D8BEF054DA3AE7D8E8423FDBE61E177DF4AEED6BEF5BB5942FF59F85F9FC99630395E61DA023D8DD52BFCCC4B85F0E00C03E767702D210FE1981D7ED1FDC06CBF766A24820A423D8DD52BFCCEF137E3900003BD9D509C8F5F0E7817D3C8FFF2A4CE7CB6891C0541DC1EE1515B7C8137F390000BB59ED04E46AF8AF2A207AF25CA2DB5A13F5C465FE2CF1371E7FA7A3D89DA290FF4EE11703007046AA9D80CAC54497B7241A7B16D1EC76B115EDA4B0CC16E52AF49F3E8F68B00A7DACDA97A59A9EA5A3D89DA270F04DF91703007088D94E00877F246E643B8F74BF5585E3A3E71095A5FE18604F59D82176799FDF17423F178CD7519CFEA26E671CA67E81DD71BF100080F38C7602A4F08F175161C94BDC8E6A4BF4C4B9B151F152C07ACDDCF6440FE5C7CEF22FC5D4BDDC13FC40C771FA8B22814BE55F0A00200DFA75569D806FE5E067E5F393877F7D2E6B113B93BEEBACD8FD733EBB9642381DF872FE2C15F693D4D9FD9DEAF7B9A115D185087C08545124EF781DC9E92D8AF8E708BF100040FA5CAC4271E51CA2FFFEE797E0FFD3EF54588E90FFBE15BC242E3FA570900AE0916D89EE3B3B76C5606A3B7536AE3A087CE5801F632C85787D78D5BD45EAE738E09F3D2F764B823B1D77B421BABEA53AB357AFC75728A4DF0720ECBF5A4772FA4AAFFEF7FF127F19000017F4CC23BAAE5BEC2142D2FF4F27BEFFCE8BEA5CACCED2F94A02BB42FD7E35FFCC4BECF2FFC7887CB02A1258A063397D45A1406BF1970100008034F1FF33EDD30129E21F2AFF32000000903691602B1DCDE929F5A26B137E0900000048B3E0301DCDCE171516EEA75E7473E22F0100000069F6928E67E78B42FE4EC22F00000000E9B78DF2F3F7D711ED6CA90EC0DDC22F000000006E08053BE88876B6D48BAD4B787100000070CB281DD1CE95BEFFBF25EE85010000C03D6B754C3B5754EC3F4F786100000070CF262A6DB4AF8E6A678AA71B082F0C000000E9D2D34F7495728BFAE731CA0341A2C5796FD0AAD66F5079EB5769D55915B4FADC67A9B2534FAA2C3C5A47B8B5521D8055E22F03000000A9BB58B95E19AADCA5027D5273A267F38866B6225AD08668D9D944ABCE235A9DC283A92A3A7C46CF175C4AEB0BF7D3716EBED42FF9EF845F1A000000EAEAA15CA1CED26F527FDEA9DCAF02FDF116445354A8CF55A1BEB82DD1F27388CADBA9804ED723A80BFE4A959D3AE948375E146E7AA6F82601000072411FE55AE536658C3A4B7F5005FA532AD0A7B5249AAFCED297AAB3F495E7AAB3F4F642F87A4541B5EA043C4464E2190214095E217E200000009928A25CA2CED26F6846344CFD79B7F2880AF66755B0CF52C1BE50857A195F7A5767EA15295C7AF7B28A8EB30C7702D487F544C287070000E025BD94AB955B559047CFD29B133DA1027D6A2BA2397C967E16D10A7DE9FDF982C460CC2515059374C4375CEA837D2FE18306000070DAC5EACCFC7A6598FAE7BB9489CAD32AD867B4245AA0827D990AF555E7AA40E35017820E64959DAAE9F9CEBFD1312F17AF33AC76C2F6849D0200006056897285C2D3D846A920FF79809C0AF439AD8996A8408F0E90E37BE9397E96EEB88E7FA7F585BFD2719F5854DCFC6C7127020000B0DECA35CD8806AB33F57141A28794A755A8F359FA7C15EA3567E9AB7196EE39951DAFD3719F58140A0C107738000064AF4B9541FAD2FB0415E88FAA33751E20379D2FBDD70C90E34BEFE99AC6068EA868F77B1DF7894561FFD3E2C101000099232BA6B18123D6763C4D477EDDA248E02DF160020000F734348D6D66964F63037BAD6E7BB38EFC5F8A1A35F2A9036D43C281070000F6EBA1429C57901BAC827CACF250737596CE97DD5B11CDD3D3D856AA408FDE4BC700B9ACC49DB5156ADF2F3B8568C9B131FCCFFCDF9CEAC8AD6AB346C7FE2F45A16013F1200500006338D44B9B125D73BA3A5B3F8DE866D5980F3F8968DCF144F7A9C6FD91A389669FAACED655C8AF3E4F6EA02107A80EDDF266448B0E255AD8B81EEAFFF1DFB1BBF3B7AACDB73AF67F298A042E140F6800805CD65B35C2FDCE201AA0037DE889EA8CFD8458A04F5281FECC1144330E239A7F88D088D7B24CFD7C05421F3A122D55C78F748C4896AAE3CDCEC1972B5BFE55C7FE2F45E1E018F1E00700C82525EA2C7EA00AEB078F219AD9D0199A418B8FE4B32EB93186F4E0CBE9E56DD57E68C90148B4FA1CF9EFA5C3B293E5E3A4214BD5CF48DB4AC5CAE6FFD1B1FF4B5124B040FC320000E402BE7C3F5C9D6DF1D9BCD408A762C951FA1EBED01083F3CACF8E9D412F14AECE2C3E826885DAE77C462EFDAC13B8F311FF7B18C53F2B6DD3ACB2D337E9D8FFA5D417E093842F0400402EB8EA74A2E70E971BDE54F1993F96AC754FD9196A3F24B92DC3961C9DBE2B024B8E917F07237880A0B44DB3161F5B45658DF6D5D1AFC23F3603604BC297020020DB0D3E996881D0E05AA28287CF3EA506189C57A63A74E27EA9C722D5F92B3F57DE965D78D0A7F4DA66581D43C2B742F8D82C6B7C8C8E7FD501E89977AAF8C50000C866B73B11FECAF2A672039C8D7880DACA16444B4F8A5DF5587498A2FEE47F5FA1FE7BBA570F5CD54ADE27C944CFB01D9C6EB98AD76B105ED70C1EC3206DDB28BEE5C1DB99DFB8A58EFFE800C022F1CB010090AD6E3AD599F0E7004CE77D6537AD6A1D0B7BE973A891EE4190C97E9F86708745DAA61D5636975FD38CE8980561DB86A8CE0D8F7BE0EDCC6FDC45C73F0F00F4DF227E410000B2D145EA0C7D960D23FC257CEF596C80B3CC0A9381C67F5FDA8E9DB8A321BDB6514B8E93B76B07B39F97C4CAB1C557A56AB6B3A8F1E53AFE550720147C4CFC920000181109CAFFDDAB78FE7EED86D54E4EDD4BE6FBB77CC6CD6781512A50CAF3E5BFEBB45483967F7F697B76597EA6FCBA861DE2DC2D8B546F4DD4B6EC5479DBC9F07894DA03221735BE5DC73FDF020894277C4100001A52A242FF0AD5A80D6C4B74F359443729D7AA06FE42154CD2DFF78A7E26078899C183C9A406D88A8AF6EACCEF34B5FD7AAE58F02876BBA68819C10199EA65F6E8CC08076F8F70404AAF6B46B9433302A2212CBC9E19BC8090B4ED86F0E043FEDC6B6F6751E30774FC473B005F267C490000EAD34385FF0D3AF8E37147A0B485FC735E70FFB1751B433BF19C73A9114E152F5EC39D0AE9B5E2F11AF2E97866000FF8935EDF28FE7969BB76E0CF407A4D33CA1D1AAFC023F8A5D73383A7114ADBAE0F5F8D8A0F7FB6A8F10C1DFFD10E00A6000280313DD519FE0DAA9194C2FF672AB84AF3E49F77D385CD922FD96BC5B22672439C0A3E636C70AD78018FBC97B665275E954E7A6DA378253C69BB76885E29115ED38C95EAB895B66D5947F9F5CC587484B0DD7A44C71CD4B3A8D582839F8F857FD199C78A5F1400807886C2BF86073B0137DA7089B82165367500F832B974E66684D383ED52FDBD6AF0ED0369BB76A83DD02D559646DA2761B6439740755EA5EDD6C6573096247BD6C0C11FC43A00C5FEF3C42F0A00406DA6C2BF86C73A01A38F171A431BF1023452A36C163F0E56DABE11D171080EDE0AE0B350E9750D331062A9B2F2B9D558EEE02C0E2B53146BC43F2698EFF1F300431E00B9F828F967121CFC43AC03100A5E247E5100006AA414FE353CD409E007FC880DA24DEC1A03B0D8E23885952A10A4EDDAC1EA1500E6D8B3EE2DACB55F23D591F646F06243D26B9AC1531517ABE3383AA73FE5DB59B1E70150243058FCA20000304BE15FC3239D80478D9E21A5C896CBDB7CAFD8E2380527D722B023C4563B345592074D4AAF6786DD033959F46984E7C4C25B7ACDF4DB5DD30198247E5100006C09FF1A1EE80438DD0160561F0064C768712742AC46F4E97AC26B9AC1412D6DDB2AEE5848AF6786A187EE14A8D752FB79F5D9B1B50D7805411E3BC01DAFB253639F118FD88FDE2E7170D069EA36EB0E807FB1F8450180DC666BF8D770B91330D1C105806A2C6F26048609690BB11471C049AF698653EB16441F7623BC9E191CDA7C0B850753F2A0421ED8C9331796F2A577D581E4A59EA59FCB240B1A7FAF3B0081B7C42F0A00E4AEE83C7FBBC3BF86EA045CE2522760820D97AF93E17BE45606E1F1C23FD276CD88FE0EC2B6EDC083E4A4D7348307EB49DBB68337CFB8BD6541E325B10E40D8FF47F18B0200B9EBEAD64270DB6890EA049408AFEBB43B934D8FB289D580B31A623CDD4CDAAE1DF852B7F49A66A4FAB4449E1EC9A3DE798D84E8A5773E4B6FA6CED24F5767E8A7102DE1591EE8002477F010DD01086C4EF8920040EEE260E6B37429B8ED74694BF9F59D74C7494263E8000E602BCF6EB73CD54E716AC95D5E28477A3D3378C19E9AEDFD1CEAEA98E050E7A7E6452FBD73A89F1C1B38B7F8E8ECB8F4EE158B0F6EADC2BFE981E297040072579F167260DBEDCA56F2EB3B891F012C35884E88DE874F3184790099B44D333854A56D5BC50BCD48AF678AEA20599BC606A95AD0F88DD8D97FD199A7885F1200C85DBDB3B803706D13B951740A5F924EE54C9C1FFA226DCF0CBE4C2E6D3B1535D3D878F4BE1DB700C04587F48975008A9B9F2D7E49002077F1A37D0709816DB74B5447437A7D27F5B561009B597C25C0ECBC773B9E6AD7E0A377337E1A1BA4E61D9AD0689F580720142C16BF240090DBFAB59443DB2E03DBA88E86F0BA4EEBDD4C6A149DC76302A26BCC1BBC1A60C7487BEE4464EB343648C1C15BD571D82C1AFE5CEA0BD12FE10B0200C0FA3B340D9067005CD85C7ECD7470F26980C970F8F2597679BE0AFA06A60AE2323BD8AB9A161C7CB58EFE58513878ABF8050100E033F4FE364F07E4F0E73106D2EBA5CB4CAF9C011FAACECA8F8F8D74AFC16306A223DED5FF137F06C0B46AE5661DFBBF94FA328C4DF8720000D4B0B313E085F067CFD830C50E2023F065FF83FBE9C8AF5B780E000024654727C02BE1CF1E4DC372C0006E5BD0F8ED3AF7FCE38BC2FE69E2170400A0362B9D002F853FBB2F0DCB0103B8E6904FD49F971135F2E9A8978BC2C125E2170400205E2A9D00AF853F1BCBCBC54A0D274086997B08D1E4C3891E3A9AE8816357A9FFD642C77BF2525F861712BE1C0000F531D309F062F8B361363CCE16C0090B949987123D7B04D1634711DD7F2CD138D5611DA18ED9DB4E21BAE134A2AB4E272A6D4AD4A359DCB11D1CAEA3DD58A91F78B3EE0600009230D209F06AF8B381AA11951A5F0027CC5367E9D30E237A5285FAC3EA4CFD5E15EAA34F883D97E26615EAFD9BC416A8EAA3429DBF5BD2316B88FF2E1DEDC64ABDD887F28600001AC00F0CBAB69E4EC08D1CFE2ECEF34FE622D5D04A0D3580511CEA5355A83F71642CD4795CC96875A63E242ED42FB61AEA2644820FE9683756EA87BE4AD808008051A579B18EC00D6D54A3A7F46DE9CE637ECDC25440A88D17879AA102FD69755C4CD2813E469DA50F3D3116E8034E23BA42077A895F3EA6DC16093CA5A3DD58A91FFA43C2460000B2DDE093E52080ECC167E9D1503770962E1D231927385D47BBB1523FF4B7C48D000064B99EEA2C8E47504BC101DE35FB50A2E70E8F5D7A7FE098D800B991EA4C9D437DD0A944D79C1E0BF55E1E3D4B77D6421DEDC6EAAFC56DFFFB785188AE2CBA9C8A8BAFA10EC5D7830D8A8AAEA14B8BFAD1C3C511FA21D456DA5100E0361E592D850CA40F77C26AEEA54F5467E9F7A8B3F4517A80DC4D2AD0F9F1CDD10172CDD2772F3D73ADD0D16EACF6291947BE1E7781C30E0D8D887608FE136A29ED34007003DFCBE5CBC3523041EAE207C8DD7D9C7C967E61FC3436B0241458ADA3DD58F9D00148ABFD2263A2575B76843D3C421A2097F0C02EDC0A6898388DED7807A6B18135FE4A1DEDC6CA57325E0C2A70D621E191F459F85C61070240DAF1C22AB9D609F0E23436B0EA051DEDC60A1D00F7F0D580D9C5DDA49D0800E9C6F79A6765F02378A5696C63336C1A1B58E47F4547BBB1928209D2671FD5012B2FFE8DB0230120EDF812360F449302D60D39378D0D2C5AA7A3DD5849A104E975406434FD217C96B4330120DDF872F74D2A5C9D181CC8EBBC73A0F32244FC48E2E859BA0AF4E12AD06F55AF79BD0AF42B4F27BA44057A0F9CA583696FE8683756522041FA9D1C1A2CED4C0070138F56BF4B05340F7EE34BEC52A837388D4D853AA6B141BA8402AFEB6837561803E01DCF1517CB3B1500DCC767E4979E49D44F750A98F8343600174582AFEA683756BE9271D5521841FA1D1A1E2EEF540000806442811775B41B2B5F642C3A001EB2A0A88BBC630100001A1209AED1D16EAC5407A04A0A227047FBE21BE41D0B0000D010D32B0146C6EC958208DCD1383452DEB10000000D89F857EA683756BEF0983D5210815BC6D32E2C130C0000A60597E8683756AA03B04B0E2270CBE7212C110C0000A6CDD2D16EAC7CE1D1DBA51002F7BC18EA24ED5800008006F89FD1D16EAC7CC5A3B7482104EEC1D2C00000605A24304947BBB1F285476F904208DC830E0000009816094ED0D16EAC7CA13BFF2D8510B8071D00000048C1481DEDC6CA577CE75FA41002F7A003000000E6056FD5D16EAC7C4523BE934208DC535EDA8BE8AAF39D57D242388000002023850257EA683756BEEEC33F924208DC53BEEE13A26DDB9CB7752BD13FFE48F4E850F960020080CC51DCAC4447BBC1EA36EC252984C03D69EB00FC4C7504564D910F280000C814053AD90D56F7A10BA41002F7A4BF03A06CDD4234AAAF744001004026286E91A793DD60751DF6A81442E01E573A00EC8B37E4830A002053F5694174595ECC8559BECC7AF7C0893AD90D56D7A143A51002F7B8D601F8D75FE4830A0020D35C18241AD086E8E6B3EABAAE35514FF5FFA49FC97445AD0FD6C96EB0BA0C2D954208DCE35A07E0A77FCA0715004026E9ADCEF407B54D0CFF1A37AAFFD72BEB3A01DB75AA9BA82EB79D278510B8071D0000801445C35F08FD7803DBA84E4056DD12F8B34E7513953FF0682984C03DE8000000A4C068F8D7C8AA4E40F0039DEAE6CA57325E0C2270073A00000026990DFF1AD9D309785E47BAB9F285C7EE918208DC810E00008009A9867F8D6CE80484023374A49B2B5F68F4262988C01DE80038A48FFA92DFD293E8BE414453EF229AF730D1D2A788564F03488F8AE9446F97137DF506D1EFDE977DF79EF22ED1B76FABBFF726D1E7EB883E7E5979A9AEF5CBD5F666123D3992E8BA2EF2319F0BAC867F8DCCEF04DCAF23DD5CF98A47FE280511B8031D001BF1F30E869612CDBC9F68DDD258A309E086375711FDFE03A23F7FEE8CCA594403BACADF836C6557F8D7C8E84E40F0361DE9E6CA5734E2432988C01DE800D8A087FA128FBA429D75CD901B638074FBAD3AAB9782DB4E7FF888E8EEEBE5EF44B689867F0353FD5295A9530423810B75A49BAC6EC3564A4104EE4007C0A2ABCF8F5DDA971A610037BC572907B6531EBC45FE6E640B5EC887835A0A703B5CDF86A844785D2F2B6E7EB64E7493D575D8C35210813BD001B060DC3544AF2D911B6100B77CFE9A1CD44EF95EB521B7F692BF23D9E0AA567270DBA96F9EFCDA5ED5D37FB44E7493D565C8555210813BD0014841449D114CBC4D6E7C01DCF6E57A39A89DF4D62AF5BDC8E8416DF573F2ECBF46FFD6F26B7BD3766AD4C8A713DD6475BFB1991444E00E740052F0D428B9E105F002373A006C543FF9FB92E9A4C0B6DB0D6DE4D7F6A6DFEB344FA97CBEC8B86A298C20FDD0013089A7F5498D2E8057B8D50158F6B4FC9DC974B80210EF359DE5A9952F3C7AA71446907EE80098C0F7395F2F931B5D00AF70AB03F0857A5DE97B93E9AE4CC31880CB5BCAAFED49FE693ACA532B5FF1A87F496104E9870E8041BDD55900CF7D961A5C002F71AB03F0C3A7443D5AC8DF9F4CD683670108A16D97EB55DB12115ED7AB4281113ACA532B5FD1C8AFA43082F473AD03B0F1DF444F8ECA1C0B1F951B5B00AF71AB03C02E3B4F0E8D4C77A1CD8B00D518A8C29FA7194AAFE955217F1F1DE52956F76115521841FAB9D601D8F41FB9F102006BDCEC00F46D2F874636E04E809DE301327511A050A0B54EF214ABCBF0BBA53082F443070020CBA003E01CBB3A01991AFEE1403515B53E5827798AD565F0F9521841FAA103009065D0017096D54E40E6863FFB3F9DE216AAB0703F5FC978319020BDD00100C832E800382FD54E4066873F5BA753DC5AF9C263B6498104E9850E0040964107203DCC7602F8EFF6543F236D2B63F89FD6116EAD7CC5237F900209D20B1D00802C830E40FA18ED046445F82B91C0201DE116ABFBF097A54082F442070020CBA003905EDC0968688A60264EF5AB4FA4D9F93AC12D56D7A10F4A8104E9850E00409671B503D0410E8E6CC79D007EAC6F7CF85FD73AB69090F4339928E5A700C657F7DBBB4A8104E9850E00409671B30370F5F97270E48ADE2D882ECD8BC9ECC17E02FF3F757ADB50B19900782890CBD00100C832E8008023FCAFE8F4B6A77CE1315BA45082F441070020CBB8D501F8E133A28BCF168203B2C4C33ABAED295FD1A8EFA45082F441070020CBB8D501F8FE63A248B65DF6869F85827D7574DB54DD472C964209D2071D00802CE35607E0D357E5E080EC100A047472DB54DD865E2D8512A40F3A000059C6AD0EC06B53896EF713F552A400814CB6952634DA4727B74DD5AEDFA15812D85DE8000064992F5E9703DA49BFFB80E8E5A144CF77520A88569E4BB4B035D1D3CD89EE5201723D3A0519EE3D9DDAF6962F341A4B02BB081D00802CF3D96B72483B8507FFBDF73CD1DA0B7507A01E151D89969F4D34A715D123AA63304205CBE50941039EE47F4647B6BDE5EB3EF22B2998203DD00100C832EFAA309682DA091CFE1FBF44B46EBA1CFA46AC6E47B4B42DD1B43CA20783B88DE04591E0153AB26DAEEE774C918209D2031D00802CF4ED3B7260DBE9874F893E7C21F67A2FDD26877BCA0A8856F16D8436B1DB0877AB10C26D04F794F8CFD0896D735D801501DD840E0040167A7325D177EFC9C16D159FF57FFD16D1DBAB63AFF5FA0215D85DE202DC21B56F233CAE3A06A354A7E072740C1CF62F9DD68E94CF5732AE4A0A27701E3A0000D96A05D1472F127DFB36D1EFDE8F0DD44B157726BE51A1FFE92B446F95D77D1DDBCFFE53501E771B61306E23D8A84267B533E50B8DFA97144EE03C7400002065EB2613559E2F87B2EB6ADD4678A6C52FB311220901070D0A8ED151ED50751FBE4E0A27701E3A0000909AC5C947FE7B11DF4628CB279AAD67230C57217719AE16D42B142CD449ED50751D36520A27701E3A0000605E19D10BFDE580CD54B567233CC4B71154F8F56A268762EED8453DF37FAD93DAA12A1A7EAC144EE03C740000C0B49706CB219A75F46D84053C1B41750CC6A98EC1752A184B1282325BBDAD53DAD9F285466F96020A9C850E000018A7CEFC5FBA4508CA1C53731B615A2BA2912DB2F0D9FF3FBB5F47B4C35534E22D29A0C059E8000080310B885EB8520EC486F0A5F5D5E7C54253FAFF59A180688AEA105CA13A03729066A650B05827B4C3D575E85029A0C059E800004052AF3D4A54191282AF1E2B5B122D3D9168E1A14A63ED10A225C712AD50C192B59D01D511784475047A64C515813DD42B70884E6887ABDD6D7830900BD00100807AAD9BA6CEFAAF1682AE1ED18174C7D70AFD7A2C3E92A8FC6C791BD9609EFA1C2ECDF4AB01FEF7753AA7A77CA1513F492105CE41070000EA5A4AF4EAC32AF8AF5261D63931DCEAC397F939D8A5C0171D4AB4AA8DBCAD6CB0AC035169467702EED5D19C9EF2751FFEB21452E01C74000072D962A2D76713BDF638D1CB6355E85F4354D9550EB40615C42EEF8B41DF8045871155A88E83B8CD2C30B77DE60E102CF677D6D19CA6EA36F46A29A4C039E800806DD6CE259A3C9E688C0A914111A22BD419501F758657A2CE82AE3A5D0E806CB2F404A215AAB12F6FAB424DBD77291052954AB8C65BA9F683B46D3BF0B6A5D73462D9A9F236B3C5C47C3960BD6D33E5E7EFAF93394D5558B81F9E0B905EE8008025EB96C542FFA612D5682439D39974B41C00D96AF111B181706567C4027235DFF35667CA5248245367305D8ACA551049DBB6C3E263E4D734E410FB3B4C5E52A1F679BF8CBB15F0BC4EE5F496AF68D45FA4A00267A003002979BD8CE8E9D14497B7931A0FD9E56712CD578DBD1802B942BDFFC5AA23B4EC64A2E5CD8856B58EDD3B9782A3065F2217B7650687AC4323EF2BD43120BEA6092BF3E46D3B4685F2EA73629F3F5FB149B60FAC7A4ABD96F49DF0ACE06D3A92D35CDD87CD91820A9C810E0098B66A2AD1F54542A361C0C813E400C8758BD4193E5FE65F765AEC36C2AA5AB71156B6927FC68C25AAD3111F4A765965C3EFC79D2169DB76AB684F54D6447DDE8727FE0E3C80917F0F273A4AAB55872393C60244827E9DC869AEA261F952508133D0010053A6DC4D74A13A6B921A0D23F8496CF7D9703F3B57F06D84456646D6D763D9297230D98143537A4D33F83689B46D3B71478A3B5AD2EBD7C69FB7135314075BF8DEA4D7EF741ABB53BEF0E8ED525881FDD00100C31E1BAE1A071BCE627AF8891EC9B1F1006E73F20C9B6F6748AF69C60A754C48DBB60BDF62905EB73EDC51B0BB1330496D4FFA3E788EFF511DC5EE94AF68C41B525881FDD00100431E1F26341416F09580E127C98D2FD88FEF754BA16407BEBD20BDA6194EFE7EABCF3576E61F6FD111F6DE0E9891211D8092C0053A8A5DAAAEB75F2E8515D80F1D00488A2FFBDB71E62FB9E134A2A987C90D30D867E97144CBCF5067DA7A36825DC1C6DBE10186D26B9AE1E4203C2B5728969F296F33158B544744FA0E78CBC6F44FFF4B2C9F2F3C668F1458602F7400A041ABA659BBE76F04DF12B85D35D2CFA8332EA9110607F06C842363D314973755FBB9A50AE173E4E06A085F2617B76F029F9D4BDBB6030FFA935ED328BE0A206D37158B326226C0529DC1EE96AFFBC8CFA4C0027BA10300F5E2A97E038BA546C23997AA332EEE0CDC732CD1532AA0E6E6FAB4C1745361FCF36C04B53F922D6AC45714C4ED98C0AF276DDB0EFC5022E935CD586DD3FA09F3D567291DF3DED24F47B0CBD5F58EC1526081BDD001807AF13C7FB99148AF4BD519EA40154823D5D9EA83C7104D3E9C6881D0508373F84C98573AAC59D488CFFCF9F23FFFBBF4F7CDE0CE86149876B0E3F7B36B7CC2F456F2F1ED1D3BA9DB1987E90476B9C2B71D8855019D870E00887885BFBEEDA546C21BF8B641BFD3896E3D8568AC0AA6478F269A89B104E9C5576752185C178FAF3448816987E8238985D73483A70F4ADB36EB7E75CC4AC7B25744826B74FA7AA37C4523FF288516D8071D0010F1F2BE5223E175BD54237B8DEA18F06D8471C7133D7124D11CDC46F034BECD2005A61D4C3D9DB01EE567C9DB36A580E87A755C4AC7AC574402D7E9E8F548751D768F145A601F740040C40FF3911A894C7571D3D88C039E7AC80B113DAD82611E3A069EE0D4730078BBD2EB99C50309A5ED9BC1EB10F469261F9BDEB087BA068FD2C9EB912ABCB9B12F32AE5A0A2EB0073A0090809FEAE7D4B43F2F29F113F53D4375764E8D2D51FCF0D19892E886C547112D3D39B658115F6EB7EBF1C07C65417A3D336C9905A0CEFEE7A84EA7740C7A4524F8AA4E5D6F952F34EA7B29B8C01EE8004082E732F4F2BF5D701BC17D35CF4628D3B31156B5317F26CECF5490B66D060F7C94B66D06FF1EBCF2A574AC7945C47FBD8E5C8F55F76163A4E0027BA0030009F879FE522391EB701BC17D8B0E531D83E354C74075D056368FDD9FAF6F51239E5D206DC30CABCF2888AE93A03A333C50553AA6BC611715E71DA913D763553AE1005FC958CC0670083A0090E0C6B0D44880A4BEDB0898A69846AA13C6B711A28F58569D34BE8DC02B0BF21504F1EF9BC0531EA56037826F65F0C39C783BB7A8DF4D3A7EBCA15CA7AD37CB5734E27329BCC03A7400208197A7FF650AE936021635CA3CBC6CB214EEC9F0997FED470EF7539D44E938F18250A05447AD47ABDBE0ABA5F002EBD00180047D3262C5B2CCC4B711FA37211A82DB08DEA7F68BE967261450F4C986B51F3EC4578378DD0AE97870DF662A3DF9209DB49E2D9F2F3C7AA71460600D3A0090A0674BA9A100A7E0368237F11A0262C80BB8A3C0CB222F523F13BF9DE70E97F7BB27F8E7EA8CF578751BF6921460600D3A0090E0A2B3848602D20EB711DC57331B6199EA9CF1993DCFE7E7A58157A9EF08FF334F5D5C7652DD33FE780F1C23EF5F2F70FDD1BF46ABCB90B3A500036BD00180047D3BC88D0578036E2364961127CAFBD17D7FA6098DF6D109EBFDF215DFF95F29C42075E80040829BB26C15C05C80DB08DEC55347A57DE63AFF5D3A5A33A48A864E96420C52870E00241877ADD0584046BA30EE36C2234711CD6AE07235D8AFB4A9BC6FDC554591E6A7E964CD902A1C78B4AF044B03DB091D0048307582D46040B688281C4AD1458D4E8CDDA3E6816AF3711BC1763C6643DA076EF3EAD2BFC9CA5734EA1B29C82035E800408217E6119534971B0EC85EB88D603F7E4CB5F459BB2D14ECAB2335C3AA70486F29C82035E80080E8965E72C301B9E7C26644D736211A7C32D1DDC7E1D908660CF3E400C0FF50B8E9813A5133AF7CA13B37496106E6A10300A269F7480D07408C741BE1D923701BA136FE2C2EF1E4FDFF893A4A33B4BA0E7B560A33300F1D0010BD5E46745567A9F100A81FDF46B8E20CA29B4F211A733CD1A4A389A61F260764B6BBDF93F3FFABA947F3663A4933B40AFA1F820704D9031D00A8170603825D783642BFD3631D836C9D8DB0487576960589CADA28ED8946F620BA21A43AD29D88FAA8FF267D2EE9160ABCA85334B3CB5734FC3D29D0C01C7400A04137AB464C6A4800EC50FB36029F3167C46D04F5FB2DCB232A57DF8D35B711BDF208D1BA79F2F787AFA4AD9A42B4F449A2C54F103D7D27D1956E5E59F3F7D6119AE175FEED1D7D25E3C55003E3D00180063D3F134B03437AC5CF46A899A6E8E66C043EBB5F7901D1DAC12AEC67C9DF9578AF2D255AF12CD11215FC75A8CEC08C7B882E3D577EFFCEF9331516EEA71334F3CB17BAF3EF52A88171E8004052731E22EA816981E0329E8DC08B1AD59E8D30DBC1AB058BD5EB94F7247AF12E7526BF58FE6ED4E7B5254465CF08E15FCBD2A75407A79FFC5E9D100ADCAEA3334BAAEBF0DBA55003E3D00100439E1DC38B87C80D0B809B6C7B36025FDA6F49547139D12B93D4715F96F83D30E295456A3B2ADCA5D0973C7C9BFCBEECF51315E635D6C9993DE50B8FDE2E051B18830E0018F6DC385C0980CC507B36C2E8E3638B1A4D3F3C31F497A8B3FCE8A5FD5BD559FB0CF9B837E3C5796A9B4FD60D7823EEBB497E1FB6F13FA02333CBAAFBB0D952B08131E80080297327AA33AEB3850606C0EB8244D775267AE032751C8F207A618A7C8CA76AED1C39DC8DE04182CE0DB8DD49C5CD4FD089996515BEED505FC9B8BD52B84172E800806995B3896EEB2D353400DE517A4E2C54EF1B4434FB41A297E6CBC7B365654415D3E560372BE2C815B6D93A2DB3B4BA0D7F490A37480E1D0048D9ACFB89AEBD406A7000D2A7674BA26B0A8986AB33FB870713CD99183B1B978E59BBD54CF393C23C15F7DD28BFC7D4555349F3963A29B3B40A6F3BD917198BA704A6001D00B08C67090C2D8D35C4722304903A7E3055E9B944FDBB120DB988E89E1B889E1A45346F526C9AAA744CA643BDD3FCAC7852FE0C52F7824EC9EC2E5FB71158182805E800806D5E5D1CEB0CDC7F936AA82F8935D8577420BA243FB6121AE4A0B64497AB63A0065F31AA716338762B69F8E544E3AE8D5DAA9F3424B658CECCFB63D3E4D6A833793ECB968E37371999E697AA1B8AA4204F4DC8DF5547649657E1AD412C0C641E3A00000026989DE667D69323E53037EF4B52E7C63A21B3BF7CDD477C25851CD40F1D00000083529DE667C6BC895298A7A29F8EC61CA9AE433AE02A8039E80000001860659A9F198B1E93C2DCAC3F517EFEFE3A1973A77C4523BF97820E64E800000034C4C6697E46F09A0072A81B170A5EA32331C7EA82C1C552D0810C1D0000807AD83DCDCF10CB1D803F64D5437FCC962F74E78F52D8412274000000048E4CF33360A1C55B00A1605F1D85395A5D877597C20E12A103000010C7C9697EC9CCB8570E7643FCDFD08446FBE824CCDDF2158DFC83147850173A000000B5383DCD2F195EE8480C770322C18B7504E67815DED61E33029243070000404BC734BF86F000405E384B0AF7E4BEC2D97FADF2158DC4BA0049A0030000A0F043AEA4504EA7390F4AC16E50B0A78E3E54B40A87B4F5958C13830F62D0010080DC96E6697EF55267FFFDBB08C16EC8C739B5EA9FD1F2150D7F470A3E884107000072962BD3FCEAF1DC5829D80D0A16E9C843D5A91E779CE42B195725851FA003000039CAAD697E92458F13F5CA1382DD907774DAA1C4EA3EAC420A3F4007000072909BD3FC123C4974797B29D88DA8A690BF934E3A9458E1DB0EF545C6EE910230D7A103000039E5E58544CB5C1CE91F2FF5F0678B74CAA11AAC6EC3A74B0198EBD00100809CF1E2FCD8543B2988D36DC124A23E6DA550376A3BF5CC3B55271CAAC12A9CB09F2F3C7AAB1482B90C1D0000C809E97A9A9F11D1D5FE9AC707BA59E375BAA10C55973BAEF395C84198ABD0010080AC5731430E62373C3D5A0A73B3FE463DF37FAD930D65B47C45A3FE240561AE42070000B29697A6F9F160BFFB0649619E02FF653AD250A68A17078A8CAB96C23017A103000059699D87A6F9F1B883E1970B419E8250E05D2CFA63A5BA0F7F410AC35C54BEF679A27F7D917E7FF980A87C6AE6E115C3F872E2F33389D6CC267A615EECE121EBD49986D4080140FA79699ADFD2A7880616CB616E5E1585FCE7EA2443A554853737F685C7EC920231D794AF56E1F5D775E9F7BBB5F29725932D7B9A68E564D539501D849717C42E3F4A8D130038C76BD3FCAE2890823C55B3748AA12C55D7A1E3A540CC35E80038882FFBAD9C12BB52C0AB8E498D1500D8C7EDA7F9D5367F1251EF365288A76A3345F28ED70986B25ABEE251FF90423197A0039046DC19E079C8FCF011A9F10280D465DF34BFBA4281113ABA50B6D405C3DBF94AC6E7F4804074005CC0B70AF8AA006E1100D8407D8F3CF1343FEDA93BE500B72212F882F2F3F7D7C985B2AD8A46E6F4804074005CB4EC29740400ACC8DA697E75ECA19260BE4E2C94AD5538E157BEF0986D5238E60274003CA0ECE9D8BD4BA981030099979EE6C723FD875D2A85B71D1ED6698572A4BA0EE92785632E4007C043788C00060B0224E7A9697EEACCFFC69014DC76F83315B53E582715CAA9F2158DFC440AC86C870E80C77063F2C25CB9D10380D8BA1B7CFB4CFAFEB8C1DE697EB55553A479771D512847ABC3802373716D0074003C6AD554A2D797C90D2040AECAEE697EF130E73FADD5ED8E5BA590CC66E8007818DFDFC42D018098CAD9F2F7C40D4E4CF3ABC3FF4F2ACE3B5227132A5DE52B1AF98D1494D90A1D008FE34B9DBCAAA0D42002E48432A2D5D3E4EF871BEC799A5FC322814B7524A1D25A85238EF745C6EE91C2321BA10390099E247A891710921A47802C167DA0CF64E13BE1021E9F73DF8D7260DB6BAD4E23942BD5F58E3152586623740032856A7CA2AB080A8D2440368A0EF67B5AF82EB8C0D9697EB56DA470D3937512A1DC2A5FF1A8DF4B81996DD001C8247C2500B7032007F04C18AF0CF673769A5F5D91E0153A8250AE56D1F0637D91B1593F2B001D800CC38D113FED4C6A3401321DCF7CE1C76E4BC7BE5B9C9BE6575728305FA70FCA1375C1D0FE52686613740032113A0190855EE2C7F87AE4923F5B3089A84F5B39ACED1609FC40E1A687EAE44179A57CDD47BC210567B640072043F13D49BE472A35A4009984D7F3AF98211FE76E717C9A5F1D55140EFE46470ECA53553AE1005FE8CE4D5278660374003218DF0E40270032190F6CE5676148C7B75B9E76E0697E0D0ADEA3D306E5C92A1C52E82B1997958F0D460720C3F195000C0C844CC30B5C79E6297E5AFAA6F9D5F6111EF39B09D565E8642940331D3A0059821F2B2C35B4005EC297FB2B67C5C2563A8EDDC2BFCFF0CBA58076D216EAD1BC994E1894D7CB573CF27B294433193A005984574BE355D3A48617C055EAB8E44EAA971EE25383AFA20D2C9602DA61FEAB75B4A032A2786A6038BBA606A2039065563C47F4EA12A1010670019FF1AF9DE3BDFBFCB5F5ED2884B3E356E85441655475BDE3725FC978314C33113A005968A9F2FC4CD5F80A0D32403AF07C7E0E7E2F4DEB8B97CE697EB5F194BFDE4D0ED78982CAB8EA3EBC4C0AD34C840E40168B5E0D582C37D0004E78451D6F15D3D5F1E7B17BFCF1D23BCDAFB61D5412CCD74982CAD0F2F98A47FE28056AA641072007F0D8003C56189CC297F9F939FDDCE1948E3FAF79729414CCE911095CA7330495D15538EC685F78F476295433C90B6B548F5D0A68A7A103905E7C5B001D01B0133FA69A8F291E44271D739EC3D3FC06C9C19C0E91C0733A3D5059515D8674C9F4F501DE794DF5DEA580761A3A002E518D20CFBF8E3E59103306C004BEAFCFC70D87BE1747F337C49D697EB5F8DFA7D2BC037472A0B2A6BA0EBB4B0AD68C50328EBE7E77A51CD04E4307C07D65CFC4E66463D6004838F07991291E509A2997F725AE4DF3ABE1FF27F5F09FA41303956DE52B1AFE9618B05E573C121D0088E1695A3C788BCFF0B8E1970201B2D73AB5CFF901533C729FCFF0973F2B1F2799C89D697E35F6609DFF6CAFC2C2FD7CA191FF1143D6CBBA0C460700647C7560E594D8D9DFDAB9B170E011DE3C86609DC203BFA42001EF58A7F611E37DC6B341781F72078F439EF76BB90AFA159333EF72BE51F32711F56E2385723A0DD12981CAEA2A1CD2C417C9B045820A06BAD701F8403546F217C6DB2EC927BAFA7CA21B4344832F221A772DD1132388663F40B4F8F10C1A100590C5DC9BE6574B70894E07544E54B72125BE92F1993128B0DB30F27518800E8013AE2C883D5464EEC4D8E023A981020067A4FD697EA2AFA9A8F5C13A19503953DD868E1503D74B7825C34E03D1014887923CA23B2E219A791F3A03004EE2EFD73D37C8DFC3B4F2FF9342C1263A1150395745C35788C1EB155D8744C31F1D8034BBB075ECCAC0C2C7E4060C0052E3FA34BF9F6DA770F3F63A0950B95ABEA2115F8BE1EBB6A211E4EB180B7F74005C74730FA2790FCB8D190018C7E1EFEA34BF9F5551C8DF4747002AA72B7CDB81BEE251FF1243D82DA1D12AFCAFFF39FCD101F080A197102DC2150180945D51207FB7D22D1218AC5B7F144A55D1A06355E86E13C338DD8A4726843F3A001E32FE3A7526831904008679639A5F4C243055B7FA2854AD2A1CD2D6171EBB470CE5B4184F8DBADE51E7B23F3A001EC5530C673F28377600F00B4F4CF3D322C13554DA685FDDE2A35071D5E58E9EAE3C3380EFF717DC28067F0D74003C68CC55B81A00501F5E7743FADEB8E3134CF74325AFEE770C1343DA4E3CBD2F7427F9BA0CF9799A5F32E80078D4E5ED89163D2E37800039E949779FE697E8478AE41DAF5B78142A4915DE3ADCD779509542B62B50815FCF65FE86A003E0613D5BE2960000E391FE434BE5EF893B3650718B3CDDB2A35006ABE3809BA520760B3A0019802F794A8D22404E50E17F6DA1FCDD70C7368A343B5FB7E82894B9DAA7C3807BA43076033A0019E2A15B85861120CB2D984474F1D9F277C21DBB28EC0FEBA61C854AADF6E970FD135220A71B3A0019E4E1DBE44612201B4D1EA78E7B8F8CF48FD94BA140A96EC251284BE5DBA7E380395228A7133A0019E6E1C1726309902DF8F1C43C13463AFEDD534591E015BAED46A16CA8FC81FBAB105E1D1FCAE9E45A07E0DDB9D2970C8C7872A4DC700264BA058F105DD1513EEEDD534DE1E040DD6AA35036565EE9012A889F8F0FE67471AD0350AEBEE8F2970D8C402700B2098FF27F64887CACBB0D4BFCA21CAD5827604D7C38A7836B1D80D197CA5F3630EEB9B172630A9049F8ACFFDA0BE463DC7D63752B8D4239581D4A0FF275E8BF4E0A6927B9D20178FE715E3E53FAB2815993C7CB8D2A80D72DE3857D6E948F6B2F88041FD7AD330A9586CA1FF86B15CAEBE343DA4969ED00FCF955A2797711F5CC93BF70909A4977A8065535A652230BE039EA589D3A81A8F41CF978F68248E029DD2AA35069ACD6571DAC82F9B5F8A076CAD7F7DC42F4E46DCE7A42BDC6DD5712F53D4FFEB2817523FAE2F901E07D3CAFBF7F37F918F68A486012356AE4D32D320A95E68A5D0978393EAC9DF075D159F2970032CF00D5B0F2602AA9E10570D3C24789EEB8583E6EBD65A26E855128172B3630B03C3EB0ED860E4096E9DD9A68EADDAAD14547003C80837F98A7D6F0AF4F3545FC4375EB8B4279A0B813D07EC00A29B8ED820E4096BAB5976A80D1090037A8E36EEE44A29B7BC8C7A6F7F03CFFDB74AB8B4279A80A27ECB74F87FE8BA5F0B6033A0059ECC2D67AAA203A0290067CFBE999D1445716C8C7A337EDA550F01ADDDAA2505EAC09FBECD371C06429C0ADFA161D80ECD7B73DD1B409181F000E50C7D4BC8789C65DAB8E354FADDB6FC42E8A042FD68D2C0AE5F1EA38609414E256FCBDB895F4C5806C74D5F94433EF4347002C52C70F2FDEF3C04D4417B5958F35EFDBA6CEFC8B75CB8A426548751870AB0AEEAAF8204FC5BE1DFBD3F650C6F5DAC12A6EB4F9C9828B1F171A770001771A673F4034FE3AA2DE6DE4E32A736CC2F3FC51995B1DFAF7D9AF43FFDD52A89B11E87485F4E5805C32284CF4DC38D5C8736700570640E3C09FF320D143AAA3E8DD657A53F17715FE6D754B8A42656875EC5FB47F87FEDBA56037EAAAF37B4A5F10C855A5E711DD7303D1CCFB6321C04BB5C607036417DEC70B1F239A751FD1A34389465E9EC997F593F99A7AE69DAA5B50142AC3ABE380968D3B5EF71F29DC8D28EFE6B9C76E82D75CD59968F455449306133D3D9A68CA5D4433EE5567870FC5067F8137CD55FB67B63A8367BCEC2E8FCE7FF40EA2FB06110D5721DFBF0B51AF5C5A92DBFF0A753BE330DD72A25059521DAE3EE9A88E57FF28057C438EEB7815EDC8BC51BB000066CDA2FCFCFD758B8942655915F43FE4B4822B3F9782BE3E0F5ED055FAA20000648B6A8A0427E8561285CAE22A2DDDF79C4E97BF22857DBC260557D296700BE90B0300900D7650D87F996E1D51A8DCA8C24E974CDDAF437F31F819FFBFD7BAB793BE300000D9E07F98E687CAD9EAD3B9CFA0C33A5C2BAE15F068972ED2170600201BFC817A346FA69B42142A37EBEADFF4E8706641BF1DB5C3FFCEC262E90B0300900DD65249AB2374138842E576F5EFDEEBC4F69D2EFDF7AF3A5C4793BB144A5F1800804C57AD4CA4098DF6D14D1F0A85E29A509A77C05BDDCF9B1BF7850100C8063F51D81FD6CD1D0A85928A22810BD5976543DC970700203345025F5089FF0CDDC4A150A8868A07C7A82FCE97095F240080CCB2907AE6FF5A376D2814CA485161935F51283043F842010078DD1E65946ECE5028542A4561FFD5EA8BB4ADD6170B00C0CBFE4125C18EBA0943A150568AC24DCF525FAAEFE3BE6400005EF31245F28ED74D170A85B2A3A87793C329E25F297CE10000DCB64319428D1AF974938542A1EC2E7D4B6073AD2F1E00809B7E4B91666D75138542A19C2C0A059B5024F096F045040048976A7542320DA3FC51A834171516EEA7BE80A3945DB5BE900000E9F02F8A047BE8E6088542B95114F29FABBE8CBF8FFB72020038E5652A6E7E826E825028949B45A5271F4491C053EA8BC96B6D4B5F580000AB76A876663006FAA1501E2C0A057AA92FE9FFC57D690100AC7A8FC24D5BE8A606854279B1A8DB1987E9AB0155715F600000B37811B25154DA685FDDC4A05028AF1715FB3BAB2FEEEF6A7D910100CC584B91E6A7E9260585426552C5C6060427A82FF2EEB82F3600407DFE47E1E040DD8CA050A84C2E2AF6B7515FEA8FE3BEE4000075450265D4D37FB46E3A502854365474DD001EC11B0E6C4DF8D203406E8B047E5067FD45BAB940A150D958146E7AA6FAC25724340000908BF65028F81856F343A172A828E4EF4A61FF3742830000B9E1358A045BE926018542E552517EFEFEFAB6C0C6B8860100B2D71F281428D5CD000A85CAE5E2E777471FEA81B50300B2D996D8ACA0A607EAAF3E0A8542C58A2281735423F15E5CA3010099AD4A9DF1CFA75E2D8FD35F75140A854A2C9AD0681FD511B84E351A7F896B440020F3ACE769C0FAEB8D42A150C98B0A9BFC8A22FEA1AA01F94F5C830200DEF73D458217EBAF330A8542992F2ACC6BAC1A9351CAA65A8D0B0078D35F63037B719F1F8542D954BC3A986A5C262A3B6A353600E00DFF5246F1953BFD9545A150287B8B7AE69DAA670CECADD5F800803BF816DD287EEE87FE8AA2502894B345C52DF254C3B34841470020FDFE4B21FF9D54D4FA60FD9544A150A8F41615054EA748E029D520E1D60080F3362B13A97793C3F557108542A1DC2D2A3AF314DD11D856ABB102007BF03DFEF154D2EA08FD9543A150286F5574B06074B5B1C0865A8D1700A4C4FFC7E8A87E3CAC078542654AF1254A0A05C6A906EC9F72C30600F50BBEA9F4E485B9F4570A8542A132ABA834EF007EF088F2AEDCD0018056A53ACC95140A76D05F1F140A85CA8EA292607E744DF27060775CC30790CBB644A7D646827EFD5541A150A8EC2CEA1E38517504EE530D1F0F6C921A44801CE0FF238583C331B00F8542E55CF172A5140A5EA31AC38F121B4780ACB48722FE951469DE9D1A35F2E9AF020A8542E56E5124D84A358E4F28FFAED55802648B1FD519FF5D7CF54B1FF228140A85AA5D54DA685F2AF677A348A04C359A182B00996C67EC380EF6E4E35A1FE228140A854A5654DCFC04D5888E548DE877710D2B8087F9DFA750F046EA76C661FA5046A1502854AA4525C18EAA719DA260E020784FB493EABF8B8AF39AEA43168542A1507656F4164138D045416700DCF63715FA8F5271F3B3F5E18942A150A87414AF9246217F2775F6C5CF20F8BFB8C619C0093F468F373EEEB04A1F0A8542B95FD12B0325810B54033D594167006C14FC569DE93F80337D140A85CA80A2E21679AAF11E4591E0ABEA4FCC260033F628EF28A3A8C81FD487140A8542A132AD78A5358A042EA5887F8E3A93C3C38940F26775A63F9D42C18BA830AFB13E745028140A952DC5ABAFE967128C539D82B754C3BF332E0820376C52CA29E4BF997A346FA60F0F140A8542E54A5161935F51A4D9F92A0CC653D8FF8AFA738B0E08C82E1BD5FEAD541DBF11146EDE9E0A0BF7D387000A8542A150DC2128DC8F8AFDE75138384C854685F2BF5A210299E36FCA728A040653B8E95958890F8542A150A62A76CBA0794B0A05AF5567904FAB33C87755B06CAD1534E0BE6DCADB6AFF3C4A91E0C5D4C37F92DE7D28140A8542D95BD1471AF31AEF91E084E8A5E570E0BFB502099CB3517927B6F683FFEAE8788ED2BC03F46E41A15028142ABD155D942812F4AB70BA44750CC628F354487DA8FE7D830E2E3087EFD9BF4FA1C00C8AF8875228584C45679EA23F6E140A8542A1BC5F2AB88E8D0D34F4DFA082ED11E579E5F7CA0E450ABF5CB14975943E531DA532D5797A4885FD0015F485B8848F42A150A8AC2FEAE93F9A8AFD6D5400F6883E412E1CBC27B65E41E065F5CFDFAA3F37D70ACC4CF26FFDFBAF5701BF8042FE07A353EEA2EF33D09AD766D01F010A8542A15028A978611AEA99776AB4A3100EFE86C2FEDED1418921FF1DD10E436C70E27CF5278F4558AF7CA27DAEFC49FB51F949E3FBE8B5C3BA4AA9F97F35F8EF7FAFA8EDF8DF57C1CD2B2A56A8D75BA2FE9C1C0D747E5473383850BD76A9FADDBA45839D1FE18C297628548656A346FF1F180F25F1E11984D80000000049454E44AE426082, 3)
INSERT [dbo].[tbl_MstMenu] ([MenuID], [UserID], [MenuName], [MenuText], [FormName], [MenuParent], [MenuImage], [Seq]) VALUES (4, 1, N'frmSupplierInfo', N'1.3 ข้อมูลผู้จำหน่าย', N'frmSupplierInfo', 1, 0x89504E470D0A1A0A0000000D4948445200000200000002000806000000F478D4FA000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC300000EC301C76FA8640000001974455874536F667477617265007777772E696E6B73636170652E6F72679BEE3C1A0000867149444154785EEDBD077C14579AAF6D4FDA999D9DCD7736DEFDF6CE867BEF7AAF77EE65E86E0136D8C6796C8F83C6E30056570B01221B831336D8188C6D1C3039E79C73464421A9AB24241049484239804926A7F3D5694E89EAD6ABAEEAEA6A75FA3FBFDFB35E8F557DAA4E7A4F559D3AE71E0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000008019EE4B2DFE89335DB9DFE9F1FEC1E1513E7148F23CA7A46C70BAE5FD4E493EA6FE7BA3FACF9BAAE754CFA8D6A996721D92B2CFF7F76E79B843F27A1C6EE5E1B6EEBCFF2E7E1A00000000B142DBB4BCBF75BA95D7D5003E5BF588EA0D5566B3D5EAC060B1D323F77348F9BFE9342CEB47227900000000B40EECDE14775E479747F94A0DCC450181BAB5FCDEE9F12E72A52B2FA4A466FF4C9C1800000000ECC69556F0CF0E4919AA06DFB280601C6D2FF0D7062EC9FB6C9B0CF9C7E27401000000601D76AFC323FFCEE996B3D4407B3B20F0C69C2EB75CE192E437DB4B7B7F212E0000000000A619C67EE0F228CFA84155090CB271E205D53198400800000098410DFC0EC9DB550D9EC774C1349EBDA63ABA4D86FC67E20A01000000A0C721E5FF17FF0C2F2080268AA7F91704A9A94B7E282E17000000486EF8DDB11A20C7A8F26FF3A9E019B28FF5CE613D3ED9C13E9BB28E2D5FB384EDDD3D9715C933D9C94353D9776513D98DBAB1EC72F57876A162023BA3FE7BF5D129AC54FD6F7B76CD654B562F61DFCC5CC3DEFE72337B71D05EF2F7C35056073A29E2D201000080E4C499E67D4A0D8A7C211E2A589AF6A97EFBD9D0B11BD9FA4D0B5963C964C61ABFB54D3E60C8CA9AEF1B14B887EE642E0F7D0E2178932F3484B504000000241DFC733935087EA10643CB33FB7F3B601F1B3F67352B299ACE6E37D0C13B12D61D9FCCE62F5BE61B0C50E7655E6FB6233DF77F882C01000000121B1EF4D40098DB3C201ADBBE9B970D19B389EDDF3B97DDAAA703746BCA5F1DF04148E75E39E4F99AF01C5FB258640D000000909838DCCAE3BEA04707C3167D20C3CB3E9FB2CE77F74D05E268CBE7132C5AB994FD6EA0E53903A3F99A07229B00000080C4417CDE773D20F005B543461EFB66E65A76EAC42432F0C69AFCA9C4DA0D8B7C7312A8EB09AEB2F0C93E257F24B20B000000887FF827706A900BE97D7FCF4F76B0B283D3C8401BEBF227025317ACF43DB9A0AEAD45DD72D6AFD30AFE5C641B00000010AFB07B9D92771C19EC5A904FEEDBBA75011958E3CDAA235358EF91DBC9EB0C62517B29FFEF450602000000F1871ACC460704B7A00E1ABD859D3F39810CA6F12AFF4281CF0FE0AF33A86B6EC163EDBA14FE526423000000103F383DF247446023EDD02DCF17245BF373BED6F648C10C963AD8FC244197A4E46153210000007105DF118F0A6A947CE6FCB103D3C9A09968F2B90183476F21F38156D98A8981000000E202A75B795D0D5EA626FCBDFEFE2E76AA343E66F8DB25FF52E08BA96BC9FCA05596F14D9244F602000000B187335DB95F0D5A979A07B1E6668ED8CEBEAF1C4F06C96490BFF248F198FC4AC02D7F28B218000000882DF8E76B0E8F7C820C6001F28D76AED78E23036332B96EE322B383809B6D3DF98F88AC06000000620576AF4B92571081AB99FCB3B8EB7508FE9ACBD62C21F389B0019F0702000088291C9232880858CD4CFF28CB37118E0A8491F452ED0C5657B9929D28DDC68A4BF6B2C2E3B92CFFA8CC0E1CF3AAFFBE8F1D2FCD6255E56BD9F99A39EC5643EB0F4EF8A241547E05AAE6F34EEC2208000020267074CBFD7735385D090C5681BEFADE6EDFFEFB5400B4D3DB8D63D9E9AAC5EC901AD8B71755B245DEEB6C5E2E33ED82BC5B6CC381067580A0B09A8AD5EC6643EBAC4B307A9AB98981EA20E03D91F500000040B4E08FFEBDDBA840A5F7A19EB9ECE4A1A964E0B3CB0BEADDFB8163796CA572810CEC565DECBDC6F61F2E660D95CBC974EDF256C358D6F7D36D64FE0578B9ADE4FD952800000000A0F571494A372240F9E9F2C82C2B6B3E19F4ECF04CF542B6AFF8089B9F7B9B0CE076BA2EFF342B2DDBC26E47E835C199B289BEA590A97CD4EB90BC1B451100000000AD4BDBB4BCBF5583D1D9C0E014E88439ABC86017AEFC8E7F47D149325047DA35F9677CAF07A8F30AD782DC59AC5DBA892F03DCDEE7445100000000AD87CBAD8C250393CEB40F77FA16BEA1029D556F368C6785C7F2D882DC9B64706E4D771D3CE19B60489D6738CE5ABC82CCCF004FB6C990FF5814070000001079D4C0F34F0E49BE4A04A526F95DECD1027B97F8E5B3F4D7173492C1385AF2390215E51BC8F3B52A1F347519B28BCCD700078A2201000000228FC3234F2582919FE3E7D8FB88FC64F9465FB0A582702C281F2D60B71BC692E76EC5626586994582EA5252B37F268A05000000881C291EE55FD5C0733D2010F9F9C2A0BDEC6A8D7D13E58E9ED8D52A93FCC235EB6099AD9F0D8E9CB49ECC5FBD2EB7DC53140D00000010395C923C8D0A447A376F594006342B161C95C9601BAB6E29AC66D7EBEDD9E0E8DCC909ECB1DE39641EEB2C6F9321FF58140F000000603F6AA0F93335E05C0C08407EBEFAEE6EDF37ED54400BD5A2633964908D75B71655FB262B52D714AA3316194F087478E437441101000000F6E3F0787B530148AF5DDFFC1F3FB1830CAEF1E2CE4365B6CC09E03B2676EE15FC29804352F6892202000000EC470D364581C1472F9FB97EBB810E64A1D850B98CCDCF8BFD77FE46F2CF15A9EB0B55BE960295DF3A6FF3B919A29800000000FB4871E7B523028F9FABD72F220358285EAD9FCA5628DF930135DEE413176B2B5692D7198A67CB27B28EDDF3C83CD77448CA505154000000807DA841667C60D0D1FB4866AE2D3BFD651D2C278369BCBA4CBEA40E6AA690D71A8A1F8DDB48E6BBCE12BE3783282E000000C01E5C6EB982083A4D7E3E651D19B842912FA84305D17837E7F021F27A4331377B3699EF7A1D527E8A282E000000207C9CE9CAFD54C0D17BBC30BC55FF6E344CB07D27BF5891BF0A3855B584BC6EB3F2D5019FEE9F4DE67D936E79A42832000000207C5C6EEFFB64C011F2857FA8A0158A078FEF278367A2B8B9B086BCEE501C336B0D99FF77F5668B2203000000C2877F6646079C3B7E357D2D19B0CC7AA361225BEABD4C06CE44B2A1723979FD66E5CB0353F9AFF37AA7CCE23F11C5060000005887EF38A706969B0181C64FFE7E9A0A58663D5CB2870C9889E6F6A24AF2FACDCA5F03F0C996541968A678942744D101000000D631FAFCAF538F5C76BD2EBC75FFF9FEFA54C04C44F98E86541E9875E0175BC972D07448F228517400000080759C92D28B0A349A7D466E27039559F9E4382A5026AA85C7D5FF43E48359172C5F469643931E79B7283A000000C03A469BFF8C9B1DDEB6BF79878BC84099A8AE52CE91F960D6A305D3C972D0D9208A0E000000B08E4392F38920D3E4B66DE1EDFCB752394F06CA44F67CCD5C322FCC78BD761C4B49A7CB42F3D769057F2E8A0F000000B002BB571D005CA5828C66E5E1A964A032E3F7B5B3C90099E81E2BCD22F3C3ACFCB34BAA2C34533C5E87284000000020741C5D73FF8A0A309A7C7DFA7036FF3951BA8D0C9089EEEE4325647E98B5FF67DBC8F268D2ADBC2E8A10000000089DB6DDF2EE23038C30DC0580F28E14920132D10D771E005F77812A8F263DF247A20801000080D069EBC97F840C30C26E1F85F7289B7F174F05C844976F757CB3D1FAC6494B562D25CBE3AEDE71A20801000080D0E18F92E90073C7F7BEDE4C0628B326EADAFF663C573D8FCC1333AEDFB4902C0F4D975B9E258A10000000081D9724BF490518CD2FA686B704F082BC5B64704C06EB2A57927962C6ACACF9647934E9F62E174508000000848ED1264013E7AE220394196F368C270363B25879723D992F6634B135F066518400000040E838246528115C9A9CBAC0FA5DECD5FAA964604C164BCBB690F962C683CA4CB23CEE8A5D010100008481D32D0FA703CC1DA72F5C410628335EAE9D4E06C664B1A4D4FA12CAC70E045F0D902FDE248A10000000081DFE3919156034C3790270BD7E12191893C593659BC87C31E321A327006E79BF284200000020741C9232880C30C26F66AE210394196F378C250363B25873D27ADEE5E7CC22CBA349B79C258A10000000081D8724F720038C70E424EB13D9B88BBDD7C8E0980C36562D6D961F66DDB36B1E591E77553688220400000042C72529AFD101E68E6F7F19DE3A00EB0B1AC9E0980C5EA9B3BE87C2EAF58BC8F2B8AB32471421000000103A0EB7F2301D60EEE81EBA930C5066DD537C8C0C8E89EE12EF55323FCC3A73F10AB23C748E164508000000848E33ADE0DF88E0D2E453FDF69301CAAC45C772C80099E86E3A5047E68759474C5A4F9687269FBB218A10000000089D94D4EC9FA901E5766080D174796476A9CAFA9AF67C353C2A4026BACA5185CC0FB3F6F86407591E4DBABD2F892204000000ACA106949A6601466791772619A4CCC857035C9877930C92896C385F0070F99317AA2C341D52FE7F89E203000000ACA106944D810146EFF2B58BC92065D6AD455564904C5417A8031EBE06029517663C756212590E3A6FDFDFA5F0E7A2F8000000006B3825EFE744906972C4C40D64A032EBF1D22C325026AA3B0F9692F960D65D3B8D3E01948F8BA203000000AC63B425F08B83F69281CAACD7EA26B305B9C9F31AA0A23CBCB513C6CD5E4D9683CE05A2E800000000EBB495BCBF22828C9F8D2593C96065D6DD874AC86099682EF55E61371BAD4F9AE476FD601759069A7C0B6751740000004078A881A53230D0E85DB7711119ACCC7ABA6A311930134DFED92375FD663D533691A578BC641968B64DCB6B2B8A0D000000080F975B9E4F051BCDB7466F250356286E2BAC248366A2C8973DE6AF3BA86B37EBEA758BC9FCD779263575C90F45B101000000E1E1F0C86F10C1A6C90732BCEC7275788FB61B2B97918133512C3C96475E7728F61BB58DCC7F9D4B459101000000E1E3E89AFB576A70B911106CFCDCB0792119B44271DFA1A364F08C77572A17D88D8689E4359BF56CF944D6BE5BF0C7FF7CA0268A0C000000B00735C0EC080C387AF9EA7454E00AC52B75D37CEBE45341349EAD2A5F4B5E6F28CE5BBE9CCC779DD73BF42CFA0B515C000000803D383CDEDE44D069922F0B5C71780A19BC42B1AC7C331944E355FE5483BACE50BCDDF02D4B1DBC97CCF7BB620B600000001180BF067048F2553AF8DCF1F329EBC80016AAFB0F1793C134DE5C9D7F36AC55FF34F7EF9D4BE6B75E87477E551415000000602FEA006031157C34F964C0D3A5E1BDEBE6DE6898C0D617349241355EE4B3FECF56CF27AF2F540D37FF91E4D39DD2B27E2A8A09000000B01735D03C1610789A3971EE2A328885EAD5FAA96C8D7A074D05D7587741DE2D565B614F3E1C546692F9ACD7E551BE124504000000440276AFD32D1FA2829066E75E39EC6265789F046A5EA899CD96CB97C8201BABCECFBBCD4E966D22AFC78A83466F21F359E7CD148FF2AFA28000000080C8E09294342208F9397BC972329859F1FBDA59BE77E954B08D35F99E0615E5E16D8EA4B7F4D054C395FFF86B19513400000040E4689321FFD8E5962BA860A4F958EF9CB01706D27BB96E5ACCCF0958225F650D95CBC8F3B7EA7BDF6C22F357AF5A164E513400000040645103CFC0C04014E882E54BC9A066D59B0DE399F7C80132F846DB0D071AD8F7B5B3C9F3B6EA491377FFAA3B44910000000091E7FE2E853F7748722311909A7CAADF7E76B5661C19DCC291AF13102B8B05F1F7FDF2D102764B1D9C50E71A8E1F8DDB48E6AB5E875B79581409000000D03AB83CF2102A28E95DBE660919DCC2F55ADD149673E42019945BCB0D071AD9A9AAC85C5FF5D129AC5DBAC1DDBF5BDE2F8A02000000683D9CAFE5FCA91A88CE360B4C3A9F7B731FBB5E6BFF5300CDC6AAA56C7B510519A023E59AFC33ACB46C0BBBDD38963C273B1C39693D999F7A5D92F749511400000040EBA2DE850EA78293DE351B169141CE4E4F572D667B0E1D670BF36E9241DB0E3717D6B093E51B231AF8B90D259358878C3C322F351D929CCF3FC914C500000000B42E6297C00B81014AEF0B83F6B29BF5910D9A9AD7EA27B392D2EDBEA7028BF26E9081DCACFCFD3E9FDC57742CC7F6097EC11C3D6D2D998F7A1D92F2BC2802000000203A3825EFE75490D2BB69CB0232D845D29B8DE35943E57276E87836CB2E3ECC361DA863CBE4CB6C01F194807FC2B7AEE034DB7DA8841D38E6659527D7B36B7593C9DF8DA4DF954D640F760F7EF7AF5A7CCF30F60391FD000000407470A6E7FC8D1A942E0704293F5F7E6737BBD5D03A4F01CCC867EDF3001FEE1EFD763B76D66A32FFF43ADCDE5744D603000000D1C521C9DF52C14A6F56963D1BE324AAE74E4E600FF5CC25F34E6749A761593F12D90E0000004417677ACE3FAAC1E95A40B0F2B3CB905DBE7DEDA9E007BF6593E6AD22F34D2F5F86596439000000101BA8016A4A60C00A74EFEEB964F04B76F9E6497C13252ACF34F9F2CBF7A516FF446437000000101BB495BCBF5203D58DC0C0A5571A9A4506C06477C6A215647EE97548720F91D5000000406CA106AAD981812BD0BCFD73C82098ACF24D931EEFB39FCC2B9DB59DD2B27E2AB219000000882D52D2E4FFA506AB5B01C1CBCF9E9FEC200361B23A6FF972329FFCF4C8FD441603000000B109DF9F9E0C623A0B726791C130D9BC563B963DDD3F9BCC239DA7F8E64B227B01000080D8A46DB7BCFBD4A015F42940BF515BC980986C2E59B594CC1FBD2E8FFCB6C85A00000020B6717AE4555430D35BACCC208362B278A36EAC6FB3242A6F747ED75EDAFB0B91AD000000406CE372CBFF4F0D5EB70382999F83476F210363B2B86ADD62325FF43A2465A8C852000000203E5003D8A6C080A6D7E5915949D174323826BAB7EABF652F0DDE43E68BCEF31D7A16FD85C84E000000203E7048F9294450F3F3FD319BC80099E8AEDFB490CC0FBD2EC93B426425000000105F3824652715DC34533C5E567A682A19241355BE29D22BEF18DEFD5F6AD7A5F097221B01000080F842BD8BED4C04373F3F1EB7810C9489EAB66D0BC87CD0EB90E42F451602000000F1891AD0F6060638BD29E932AB3A32850C968926DF0CA9EB07BBC87CD05483FFD594AE07FE41641F000000109F38DCCA6FA940A777D4E4F564C04C3477EF9C4B5E7F80E345D601000000F18D53F27A8940D764FB6E5E56777C32193413C9F48FB2C8EBD779DD9556F0CF22DB00000080F8461D00BC48043B3FBF9CBE960C9A89E2FEBD73C8EBD6EB92E46922CB000000804480DDAB06B78354D0D37C20C3CB4E954E22836722D87DF80EF2BA75DE7474CBFD7791610000004062E09294D788A0E7E7B8D96BC8E019EFE6E7CC22AF57AF4392E789AC020000001287D4D4253F5403DDB1C0C0A7B7538F5C76F6E4443288C6B37D466E27AF57E7EDDF78BCFF29B20A000000482C9C1EAF44043F3F27CF5F4506D178F5903293BC4E7F9565228B00000080C4A34D86FC6335E095370F80777D2433975DA8984006D37874E0175BC9EBF4335D6923B208000000484C9C1E39930C823A672E5A4106D378F378E174DFA647D4356ABA2479ADC81A000000207179B24FC91FA981AF3A3010EA7DAC770EBB5C3D9E0CAAF1E4BB5F6F26AF4FAFCBE36D2FB206000000486CD4BBDE37A960A877FEB26564508D17CB0F4DF36D76445DDB5D95AD224B00000080C4A74D86FCC76A006C681E10EFFA54BFFDEC6ACD3832B8C6831F8EDD485E97DE14775E4791250000004072E074CBEF524151EF92D54BC8E01AEB561F99C2DAA51BDDFD7BB3455600000000C983F3B59C3F5503E199E681F1AECFBE99CDAED7C6DF538011133790D7A3D7E1561E17590100000024172EC93B8C0A8E7A57AF5B4C06D958B5FEF824D621238FBC169D0A5F1E596403000000905CA478B2FF520D86170282A39F2FBCB597DDAC1F4B06DB58F48BA96BC9EBF0D3ED7D4E6401000000909C382479141924756EDCBC900CB6B1E6E9D289ECC1EE0677FF6EF9D03DC3D80FC4E503000000C9499B0CF9AFD5C0F87DB340A9F3F7EFEC61B71A62FF29C03733D790E7AFD72529BF17970E00000024374EC9FB35152CF56EDB3E9F0CBAB1E2B993137C9B1951E7AEF308EEFE01000000419B0CF9EFD4E078252058FAF9FAFBBBD8ED063AF8C68213E7AE22CF5BAF43F27615970C000000008E4B9227524153EF9E5DF3C8E01B6DBFAF1CCF3AF7CA21CF596769A761593F12970B000000004E9B0CF99FD420792D2068FAE91EBA930CC0D176DA8295E4F9EA75494A3771A900000000D0E394BC33A8E0A93767DF1C3208474BBE69D1E3BDF793E7AAB3EABED4E29F88CB04000000809E766FC8FFA206CB1B01C1D3CFF48FB2C8401C2DE72C5D4E9EA75E9724F7119708000000000A35602E080CA081E6E7CE2283716B7BAD762C7BBA5F36798E3AEB5352B37F262E0F00000000144E77FE7FA841F3564010F5B3EFA7DBC880DCDA2E5AB9943C3FBD0E4919242E0D00000000C17049F20A2A98EA2DF2CE2483726BC937297AEECD7DE4B9E93CDD5EDAFB0B7159000000000846DBB4BC5FABC1F3764030F573E0175BC8C0DC5A2E5FBB983C2FBD2E8F3C445C1200000000CCE0F4C8EBA9A0AAF748C10C323847DA1B7563D9F3030DEFFECFFF3AADE0CFC5E500000000C00CCE74C54504553FDFFB7A3319A023EDBA8D8BC8F3F1D32D0F17970200000080505003E9F6668155678AC7CB4E1C9C4606E94879ABFE5BF6FBB7F790E7A3F3E203EEFCFF262E0300000000A1A006D28702026B33878EDD4806EA48B965EB02F23CFC74CB5F884B000000008015D480BAA75980D599922EB3CAC353C9606DB77C33A257DFDB4D9E87CE2BEDA5FCBF17A70F000000002BB824EF934490F573C4A4F564C0B6DBACACF964FA7A1D92FCAD38750000000084834B52F2A860ABD9BE9B97D51D9F4C066D3BE59B1151E9EBBCDECE93FFFF89D3060000004038383CF2EF8860EBE7E8A9EBC8A06D97D97BE690E90638459C320000806485CF0277A57B3B39DD8ADBE1F60E767AE48F1C923C0A5A317F94C32D371201B7C90732BCECD4894964F0B6C38C8F7790E96AAAE7795B2DEBC9CDCF1D9AD1E99647BA3CF2DB6A3BC974A6799FE21B4389A6040000B18F335D69E394BC5FAB01A1383040C0C83B66D61A327887AB377B36991E8CB8752EB73CDF25799F6D9321FF58343300008815D8BDAE74E505B5B392033A2FD8CA76EA91CBCE964F24837838F61AB99D4C0FB6AA754E8FFC56A7CCE23F110D0F0000A28743CAFF2F87A4EC233A2B182527CE5B450671AB1E546692E9C0A859ED9294DF8B2608921B766F9B0CF9EF523CDEFFEB92BC9DE3C114775E3BF59CFFE9BED4E29F888B007107BB57BD1BE9A77646D7023A2718651FCECC65172A2690C1DC8A033EDF4AA603A3ACDBBB1C7B2D2429EA9D578AC3234F502B4275B38A113F5E7049F2123E9A4D4D5DF243716920C6E1EF229D923287284F18234E5FB8820CE6A17AECC074BEA31F99068C01DDF221677ACE3F8AA609129DDF48DEFFA976BEABC9CA10C7AA038183291EE50971992046E1C1DFE1F6AEA1CA10C68E8FF6CE6197AAC693413D14DFFE7233F9FB307674B9E50AACBD9004F04F43D4023F1F580112CC31F70C633F10970C620A762FEEFCE3C7394B979141DDAC6507A7F9361BA27E1BC69C471C5D73FF4A345490683824AF472DE45B01859E903A24791E0F36E2D2418CA006FFFE547905F3F1EEB96CC0DBBBD8880FB7B3499F6C66333FDD947C4EF1B299D3145B9D3645610F77A7F35CF3C9BEFBD9959A71647037E307DF6E227F57EFC8AFE8F30BDB2FB6D17999C04E1BB1897D3E6C1B7BFBDD9D2CB5CF7E32BF83AB6CC0CD5302E248531E500B38A9265BB924F98CFACF52131E51DDA3FEFD0AA7E41DE79294D7F8A4489175C0269CE9CAFD6A3E9BAA832992977DF0FE0E963B6115BBB26021BBB630C9F57AD9B58202DBE58192CA7FBD8B572E2583BB91D547A6B076E9C1EFFEBB7FA090E7658B2B57D2799944964C5BCE268ED8C21ECA08E5298CD25F34599008DCDFA5F0E76AC1D6362F686860B14352066196AC1DF047FFF2DE80FC254DEBBF971D9AB282ECD092D6080D00CEE515B0C733E972D07C664036BB5E17FA53808FC76F207F4FEFBE35F9E479D92206004D36CC5EC23E1E127C15469D1752BA1EF807D17041BCA306B1A1442143F35E70BAE56FDA64C87F2DB214848858E487CA5B3FDF7D2F8B5D98B788ECC492DA080D00B89327E79365A177C5DAC564906F49BEA950878C3CF2B734DDEF45F0EE9F8B014033977EB181B5373327C3AD4C124D17C433FC3B79B540CF362B60C24E83F3D9A3C30BE3C2873F38C05CE9F47544D006A7DBFB92C85A10024EC9EB25F2D3CF21EF67B1AB78DC4F1BC101C077B905AC734FBA4C349F7B731FBB5137960CF6949F4D5E4FFE8EDEAC1511BCFBE7620040BAE6AB756479E87548F2553C0548001C6EE571AA80F5F2C0EFDE779CF52A2D8D2B330A4BD8139F1791D71449F9BADA9DD2B27E2AB2181870676D7F3A2F35F963FFEFE7E3CEBF45233800E08E9B603C1760DDA64564B00FF454E924DFA642D46F68BE36586657F3E973B14D0C005AF4EB8F8C176672B9BDEF8B260CE215FEE89A2A5CCD0E7DF359F7A21364808D07334F94B207FA1B3FC2B45B87A4EC74BE96F3A7229B4110C4C63E643E72F9843FBCF33730C20380C6FD05AC93C117012F0EDACB6ED5D3415FEFD733D690C7EBDDBC34C277FF5C0C005AF4A23AD87EAE97E1570287451306F18AD323AF220AB6C99716149381359E7C79E561F2DA22AD4B52F23041D01835AF82EEEAC71FFD539D14D419E10100F7ABB1C64F01B66E5D40067DCDB32727FA3613A28ED5FCFD40995D89F4DD3F170380A0AEFDCAF8350D5F6E5D3463108FF0204515AC66DA9E6364508D37A96B6B0D5D92BC16EB0DB40CDFCF5FCDA7DB81F9A6977FEA47755050672B0C00EAF615B00733E832D2FCC3BBBBD9AD8696E7028C9BBD9A3C4EEFBA45AD70F7CFC50020A8FC29C0230613359D1E6F17D194413CA216E2E16685AAD3931B7FEFFE295D9EE0772F4B779E66B9C72E9166175F641BF3CEB3D99B1AD9E0F165ECD1BE07C8DF08E23B22BB410029EEBC8E447E35F958F75C761913FF8C6D85010077D437C64F0176EE9C4706FFF32727B0877A06BFFB7F7E80CC2EB7C6DD3F17030043DF7F2F8B2C279D9F8AA60CE211B500310050DD56709E9D3C75D39427EA6FB0191B1AD8D36F9A9E6078C321E5FF466439D021569FA4F2CC67FFC1BBC88E0906D84A0380CA3D05AC4337BAAC34DFF86027BBDDD07C003065C14AF2EFF52E9FDF4A77FF5C0C000C5DF4C546B29CEEAA2C134D19C4236A216200A01ACA0040F348D535F6EEC472F2F7027549DE6D22CB810EA7477E8BCA2F4DBEBC2FD531C1005B6900C01DFEA5F15380998BFC770A2C9267AA0387E08F939FED2BB38B329D6644C400C0D06DDFAE25CB4A13FD5A9CA316220600AA5606009A6396D690BF19A8DA583A8B6C0702A75BFE90CA2BCDF1C3B7901D130CB0150700E53B0B583B13DBF7667CBC838D9BBD860D19B389B5EF66BCB8CCA2D9AD78F7CFC500C0D0FC49864F6D72455306F1885A801800A8863300E08E9A5B45FEAE5EFE69A0C87620500745C3A8BCD29C3402030053B6E20080FBC1E7C64F0142F1C9DE32BBE0A5D38A981800185A38D96800E0F58AA60CE211B5103100500D770050D67083B93F394AFEB6CE5B583DCB1F0C006CB2950700253BF2597B1B57DA5C383BC2CBFE52620060280600098E5A881800A8863B00E0EE3E7891A5183C1A75B9E5BE22EB810A060036D9CA0300EED726D60530E3AB835A71E6BF5E0C000CC50020C1510B110300553B0600DCFE634E90BF7F57EF2E91F5400503009B8CC200804FD8E31BF650E566D6877BC8BEA709D4EF475C0C000CC50020C1717AE402BA60EFE8CE4E8C01807A9DE4F5693ED5BF903DFBD641D2978714FB66FBE71EBD44067DBDCB779F217F5FE7B53619F21F8BEC4F7A3000B0C9280C00B87C89E0AE6FD3656724DF60287F13FDBBAD22060086620090E03824EF46BA60EFF8875547C8801A4FBEB1EB18796DA1FA60F77CB645BE40067E4DBE4640C71EC1F71E70B8958745F6273D1800D864940600DCF3DE02F6C9970A7399F83240933F39E05F1350BFD76A620060280600098EC3234FA50BF68E0FBD57C0324BE8C01A2F3E3C24E495FB5A943F11E013FEA8E0AFD96D54F001874352868AEC4F7A3000B0C9280E00340B3717B077462AEC8116160AE20384F4F715B661497EEBACF56F24060086620090E0B824E535BA60EFFAE4E822D6E3481CEE0878A294BDB4C8FE8D80D6649F2503BFE6578BABC9E3746E17D99FF460006093313000D03C9757C072D6E6B3C573F2D9CC690A5B302B9F6D5B9ECF6AF7D17F1F353100301403800487EF56A716E4F5E605EB2FDF16F8997107D98BF38AE3C2E7A61E629DDE2920AF255C3F9D5B45067E4DBE6F00759CCE4BF7A516FF441441528301804DC6D000206EC400C0500C009200B52067362F58D8926F7C7C840CFC9A2575D759FB6EC167473BA4FC1491FD490D060036890140E8620060A8F100403EAB3ADE95AEBCD05EDAFB0BD1AC413CC117A7510BF192AE50611079703F5E7B9D0CFE9A7C90401DABE9707B078BEC4F6A3000B0490C0042170300434D0C00F49E75BAE5E11808C4217C811AA240610B6ECC3D47067E4D134B03AF13599FD4600060931800842E060086AEFB7A1DD92E0D3CEE4C57DA88260EE205A32F02124987241F6DE729FCCFB692F757944E8FBC9B3A4EF3CB45D564E0D75CBDEF2C799CCEB3F70C633F10599FB4600060931800842E060041DDF0F55AF640BAF1264E946AFF7AD5E956DCA2998378203575C90FD5821B45156882B9A75D97C25F8ACB26310A4CDD3E3D46067ECDA3D5D758BB74C37900FF25924B5A3000B0490C0042170300D22B0B16B2311F6F25DB63E87A27771A96F523D1DC413C203E0DAC6D5E9871EF15D54FDB64C83F1697DA227CB19E8063FDE48BFD94D6075F0FE0950F8AC963355D92DC472497B4600060931800842E0600CD3C3377311B307827D916C37073879E457F219A3C8807EEEF52F873A75B7E572DBCE280C28C47EB1D1E79821AF8FF495C9E2129A9D93F538FBB16F03B7E6ECF0FBE2AE0C7332BC9E334D501C012915CD28201804D620010BA1800F87964EA72F662EF6CB21DDAE0F1DF78BCFF299A3D88279C6905FFA606D057D580F5267F45100FBADCDEF7D5737EC3F7B99DC577ED4EC99B4D54E426C72DAF2503BFE692ACEFC8E374D68BA492160C006C120380D0C500C0E7D5050BD9D22F36B08EDDACBDEF0FC12B2E8FFC367FD52C9A3F00B10B1F481095B8C95E5F9690815FB3A8E2AAE1FAE88E6EB9FF2E924B4A3000B0490C0042170300563E6319EB3E700FD9F65AB27DA6C25E9857CCBA6C3BCA5E9C7F98B553FF9DFABB16F5C8BBF9446BD10500109B383CF2D36405163ED2FB002B6BA483BFE68BEF1E228FD574B9E574915C528201804D620010BA493E0058FDE53AF650466877FD1D0715304F9EFF0EB1DD0A4A7C7BC6507F1FC44BEA40A0DF3DF7B07B455700406CD12643FE33B5A2DE0CA8B87EEE3E78910CFC9AEF4F3E491EA773B6482E29C100C026310008DD241D001C9AB282F5782BB4BB7EEE13A30A59CF16F684E17BC53CF14511795C50DD7256DBB4BC5F8BEE0080D8421DA51690155738654D3D19F835E76D39451EA7B34C249594600060931800846E920D001AE62C619F0FDBC65242D8B6D9A7FAF77C6F95CC13CD03BF9FEA7FE7FBB084B22DB4F0965352E6A80381BF15DD0200B1814392BF252A6C9303C69492815F533971993C4E6F285F27241A1800D8240600A19B2403808BF317B1F99F6D640F67E4916D2C9829DD65F6EAFA2374C06FC1D7371F65ED7A86382FE08E17797FD0292DEBA7A27B0020BA383C722A51519B7CA25F2119F8F5FE7660F04763FC0B0B915CD28101804D620010BA093E00383B77119B336A137B3A33876C5B463E3CE400F3E4FABFEF372B9F27C08FA77ED784D5AA03B1A700883A7CC540B532DED655CE66E61EBD44067ECDC1E3CBC8E3345D923C51249774600060931800846E820E001A662F61933ED9CC1EED1EFA1D3F973FC2E78FFC7B1EA783BB59F92B03FE4A20A51B9D8E09CFAB8E692FE5FFBDE82E00687DD44A785457299B396B632319F83567AC6F208FD3592C924A3A3000B0490C004237C10600455356B24F3FDCCE1E0CE37BFE4E83F3997B9FB5BBFE9674671F679DDE0EF92B01BD57F84D1236180251C161B049D2DB13CAC8C0AFB9FFC845F2389DB78DF62648543000B0490C004237010600D5B396B2192337B197FB86B9825FBAC29E9D7288651EA367F9872BFFDDE7D4DFE7E990E99B97AF4CFB8E333DE71F45170240647148DEAE0195D0CF67071D2403BF5E3E57803A56D32129CF8BE4920A0C006C120380D08DD30140C58C65BE95FB32DFDACD52A4F057EF7BF4E303BEBB742A70DBADB4FFB82F3DEA3C42F496DA776C734A4A862BADE09F45770280FDF00A4654403F0B4AAF90815FB3FFD727C8E3345D1EE52B915C528101804D620010BA713200383F6F11CB1EB7CAB7435FD77E7BC97662C50707E6B357D68536C3DF2EF92A829D0687F55A20D052BEF3209FB4FDEBB4823F17DD0B00F6E072CB1544A56B72C1B65364E0D79CB4BA8E3C4EA72C924A2A3000B0490C004237060700F5B397B07DE357B3D9A336B10FDFCF62AFF5DBC7DA7BEC5DA39F2FE5FBD282E2B027F9852B4F9F9F073F1FEA3CC3F086EA01BEB680D323BFE54CF73EEA4CCFF91BD1E500103A0E499E1750C9FCFC60EA4932F06BEE2A329C077093AF3C28924B1A3000B0490C0042B78501C0F7F317B1E3D396B39CF1ABD8A66FD6DAEEBAAFD7F9BECBFF76F85636ECFD1DACDFE05DEC7535D03FDEC3DAE77A666DDF4B61CFCF3CC4BA1F8ACC7B7EABF2F3E1E7C5CF8F3A6FBB54FBF046DFC26E1E79BD7A43374BFDDF3E550709FDF967D8FCC981EDBA95875DE9CAFFC6BA060980C3E3ED1E58A1F4A6BE77880CFC9A7CCF00BE770075AC668A477942249734600060931800846EC000206BEC5A36E89D9DADB12B5EABFAC0807C96BAE8B06FB95E2A00C78AFCFCF879F2F3A5AE238EBDA4BAD2E9F63E27BA3D106FF0915C40A1FAC9BF9D3D78F22A19FC3533BF384E1EABA906C31122B9A40103009BC4002074C500A064DA329611E28E78F160A7770AD81F561D61992574C08D55F9FA01AFAC3EC21E7AD7D6390231A14352F6F15822BA3F103FB07BD5026C082C50BDCB767E47067ECD314B6BC8E374EE1189250D1800D8240600A1AB0E00F8E4BACE19B964DD8B47F963F467261C62697B8E91C135DEE4D7F1CC8483AC7DEFC8BE1E6865CFABFDDE93A20B04F182D3ED5D4E146693C3675592815F738B7C813C4ED321C9575352B37F26924B0A3000B0490C0042F6E0ACF509F1B8DFD54D668F7F5AE89BD11FED897D11B3E484EFCB81A7BE3EC852BA27C460E07A5B4FFE83A21B04F1009F3042146493AF0E3D4C067ECDD2FA1BAC638FE0EFB752DC791D45724901060036890140489ECE29604FF78ADF3BFF07FBE7B3DF7E7B90BDB2F628EB5E1C9D77FB03CBA2F394815FEFAB6B8FF8AE9FE703953F71629DA36BEE5F89AE10C43A7C294AA2109B6C97AEB0A3D5D7C8E0AFE91979943C56E70722B9A40003009BC4002024C78E8FAFBB48FEA91CBFCB4F5D7CD8F2063D7638B4FC20CBAE59C92E374C60ACF15BDF3FF9BFF3FF9DFAFBD6906F3C94BAB8983DA6E64F043E298CA8C9BAFE4B5C929ABAE4876AA19D0B2C44BD6BB2CF92815F73F4826AF2B826DDF216915C528001804D620060DAB3B9F9AC63065DDFA2AE7A13D17150017BF28B22F6C2EC43ECF54D475937A5840C7CAD69DFB2136C69D57676ADF14EE00FF47AE338B6BE6A23EB571AFD73ED965FE2CBB717E614B3274617B18E7CC1A1F097208E88BED7BE9EECBF14DD2188759C92B2812A48CDCFE75793815F73DDFE73E4713ABF6F9321FF582497F06000609318009876D352E3C7C6FCAB1EBE9D2D0FC491F0B7E30FB2E76715B3DF2F3BCC5EDB7884BDB1FBB82F70456A2DFE70FCF2A4CC6AEA6792813FD0C6FAE9ECDB93B9E4EF44539EAFDD0A4AD47C3EE61B1CBCACE63BCF7F3E61F2C92FE9320AD7873F38C05CE974FDF2D3ADBC2EBA4310EBA877E8EF9285284CFBE42819F8358FD75E67ED33828F46533C5E87482EE1C100C026310030ED88AF82B73F3EE3BCB5D6C68F65DF2A3BCAB2AAD7B0DB8D63C960DF92B755736B57B0B7CBA2B3CC702CC9EB51873E8603CE99A23B04B18E5A581D020ACFCFF6DD145652779D0CFE9A5D3E3A4C1EAB73A0482EE1C100C0263100306D9F61745DD3FCFD92C364679E2CF2C7F8CBAAB6B1EF1B269101DEACFC78FE3BB1F05A209AFE7ED911B29E69AA7DE036D11D8258E7BED4E29FA885C65776220B93BBC97B9E0CFC9A23E75491C735E9915789E4121E0C006C120300D3767927F81300BBF7C38F177B979E60D32AF6B253F5D3C8806ED533F553D882AA2CD647FD7D2ADD44973F05A0EA99A64B920F8AEE10C4030E49D94915A4E6D74B6AC8C0AFB962CF19F2389DDFDD338CFD402497D060006093180098F6D541745DD34CC6C7FF132BF6B3EAFA596400B74B3E8F80A743A59FC84AB9180024146AA17D1C58887A33461D2303BFE6E1AA6B2CC56056EA6F3CDEFF14C925341800D8240600A6C513803BF23BFE4915D9ACAC7E2E19B0236555FD6C36A772B7EFCB02EABC124DA32700AA07447708E201DFF6927441FAECD8B3809536DC2083BFE6CB438AC9639BF4C89922B9840603009BC400C0B4D2BB745DD3E433C5A98E3C517CB3ECB8EF93BEEF1AA69201BAB53CD730C5F7E9209F6C489D67A2C89733A6EA99CE1CD11D8278E0FE2E853F570BED7A4021FAB9E3C0F764E0D71C36BD823CEEAECA42915C428301804D620060DAEE1F047F02F0C68EC40C48C3CB0FB06D35EB9B16F18915F9F9F0F3E2E7479D77BCDB35CB6800E0DD25BA43102FA80597DBBC20EF3A7E452D19F83517ED384D1EA7B3562495D060006093180098B6F7B0E003802E5B136700F056D931DFE3F6A3750B7C9FE751013896ACA89BED7B3A91489F10F23D0CA87AD664922DFE9610A885F6055998C23E5F9590815FB3A8FCAA6FB111EA58CD766FC8FF22924B583000B049BB0600F9F9EC9A2CD3FF2DDA2ACA9DF3A3FE5B08BE393CF800802F144375E4F1E280D2E3BE77FB4AED32DF0A7D54A08D75F979F3F3E7D7C1AF87BACE7891D727AA9E693ADCDE35A23B04F1821AB89EA50A53F3D1DE075879231DFC359F7FFB10796C936EC52D924B583000B049AB03809C1C766DF3E63B7BE32F5EECFF9B4B97B26B6BD6B06BDBB7B7FE13061EE877ED62D7D6AD63D7962D63D7162DBA7B5EFCFF5FB1825DDBB0815DDBB327E441C13B23830F005E5D177F779FEF941DF6DDE9E7D72E65571BC69341355EE58301FE04833F1918527E88BCFE5896D727AA9EE95C2ABA43102FF0F59BD582BB1550907EEE3D74910CFC9AEF4E2A278FBBAB7786482E61C100C026430DD07979ECDADAB5F46F51F2A0BB69D39DBB70EAF7EC342BEBCEE0833A0FCAE5CBD9B5BD7BE9DF22FCE0F3E003803FAC8AFD8580DE5503FE948A7DBE77E791FE7C2FD6E4D7CBAF9B5F3FCF072A7F62C93FAC0A3E007048F23CD11D8278422DBCA2C0C2D43B6D5D3D19F835676F6A248FD37478E41322A9840503009B0C6500C0EFE8F577D4A1C8037308C13624F9A064D52A3A5D33AE5F6F6A80F2E9D7C107007C6739AA238F9683CA8EB22FCA65B6B8723BCBAB59CE4E37D8BB504FBCCBF383E70BCF1F9E4F3CBFA87C8C96BC3E51F54C53EDE72788EE10C413EA1DFA38AA4035078E2D2503BFA6F7F865F238BDEDA5FCBF17C925241800D8A49901000F8E3C4852C787EAB66D741A56CDCE0EEDAEBF25F9D380DC5C3A0DA1D156C07C1739AA238F947CC2DBC7E585ECAB935E3655BDAB5D51B985EDAB59C54AEAE6B10B612EC79BACF27CE3F9C7F391E727CF579EBF3C9F5B7B82E10BB30D3EF976CB23457708E209975B79992C50E153038AC8C0AFF7E9378BC8639BF478FF20924B483000B049A30100BFBBE6EFCDA963AD6AF28EDB50FEC8DFEA13094A3E9761F76E3A2DD599D3820F009E9D1CFE7B66BEE21DBF2B2DAD9BEB9BD91E686DFD0C76BE6132BB15E2463BD01E79BE5F50F39F9703553E6575737CE567C7CA85BC3E51F54CD3E1F60E16DD218827DA64C87F4715A85E7E974F057E4DFE94803A4EE778915C428201804D061B00F0C9744B96D0C7852B9F3CB87F3F9DAE91FC6B03BB9E4804CA07145BD4BA434C105C3A37F8EE6C4F8F394876E466EC5F5AC20A6A97904107C6A7BC3C79B952E56D465E9FA87A76572543748720DE500BB0A47981DE95BFE7A702BF269F27401DA7B34824959060006093D400803F0AE7B3F8A9BFB75BFE2581D9CF077950E677FD911A94E8E55F10F02F0574E96F5A1A7C00F0C4678564476EC63D35ABC82002E3DB3DD5ABC8F23623AF4F543DD37478E454D11D827883CFD4A70A5593CFF4A702BFE6BEE28BE4713A6F3BBAE6FE95482EE1C000C02679B0E78FF9F7EDBB33C96FF56AFAEF2229BFEBE677F43CB8F3A7027C40C05F11F07FF2F3E38FE5F9970476BCEB0F553E37803F11500703FB56E491754DB3F347D656A4FBB0EC201EE927A8BC5C79F952E56E24AF4F543DD3F998E80E41BCC1BFD5270AB4C9DF0D3E44067E4DBE56C0A37D0D2A88DBFB9C482EE1C00000B6B645535692754DB3D3E07CB22337726EE52E3278C0C490972F55EE46761A5C40D6334D8794FF1BD11D827883AFD64715AADE82B22B64F0D7E4AB0652C7E91C2D924B38300080AD6DCDCCA5645DD36C97A9901DB991CBAAB69181032686CBABB692E56E647BB53E51F54C33A5EB817F10DD218847D442AC0A2C54BD0B779C2603BFE6849575E4713A73455209070600B0B5BDB260214B315886BBE7D1D0B7AA9D55B99B0C1C30FADE6E1CCB0ED72D642BD420CE67F67F7B3297CDACDCE35B50E84CBDB95D11675784FE0480D723AA7EE9BCD56958D68F447708E211BE731F51B04D0E9D5641067ECDAC03DF93C7E9BCD15EDAFB0B915C428101008C864FF6CC25EB9B66BA12FAACEFD127153270C0E8CA97110EB6D360EFD2136C5EE54EF6BDC19A0B5F9E94C9E383992E1B3EDD4D8A4DDF121ABE773F51B04DFEFEFDE0F300CA1A6EB0873283BF2772A67B1F15C925141800C068D8A5FF3EB2BE69BEB1F318D9A107932F4B4B050E183DB755AFF30578AABC02E593FC1AEAA793BFC37DAF2CF405A28CB7029665D1158278E5371EEF7F1205DB247FDC78A8E22A19FC35BB7F769C3C56E7C722B984020300180D07BCBD8BAC6F9AAFAEB5B65A5CA26DC413CFE6D4AC50833F5D4E2DC9371BBA443C09B8DE383EE4DFE2BEA2D623AA7E696227C08480DDAB16E6E9C0C2D5BB7CF71932F06B7EB3B8863C4ED321293B456209050600301A7EF2C176B2BE69BE34DFDA72C035F5339B050FD8FAF20D83AC2EDE33ABA2F95C0EBE6220F5B746BEA8D623AA7EDDD53B597485209E717AE4557401DF71E49C2A32F06B6EF29E278FD37448F2D54E69593F15C9250C1800C0683875E466B2BE69FE76BCB56FBE8B6A17370B1EB075BDD63821E83B7F23FB949E60A7EAFD375CE2E54AFDAD91CF8C0BBE0AA0CBED7D5F7485209E510B736060E1EAEDF2D16132F06B9EA8BBC11EEC1E7C8532D50E22B984010300180DD77FBD8EAC6F9A8F7E626D35C01DD56BFD02076C7DAD7EAFAF776BF53ABFDFCCAA5E43FE9D918F0E0FBE0A20DF4F467485209E49F1781D54016BB6EBA6B0A335D7C9E0AF99F6C951F2584D87A4BC27924B18300080D1F0C0A415647DD37C70A0B5C58096566DF70B1CB075F5D62C23CB2554A754ECF3FB5D5EAED4DF19F9E09B58042829E0DF72AA057A21B080F5AEDD7F8E0CFC9A9FCFAF268FD37448DE8D22B984010300180D4FCD594CD6374D974766992574A71E4CFE8DB93E70C0D6933FB61F5816FAD71B94DF9CCCF3FB6D2BBB02669E28F5D523AA7E6976E859F417A22B04F18E5AA09B030B58EFE805D564E0D75C937D963C4EE785445B34020300182D3B6718AC0520873E898CEF37AF0F1CB075BCD9308E7D76D2DA531BCA712773FC7E9F972BF577C134B106C059D10D8244401DED0D210AB949CFC8A364E0D7E4AF08F8AB02EAD826D3953622B984000300182DD306045F0BE0F5CD47C98E3D98FC9B737C0AD8FAF26598A9F2B0EAE2CABBAF72F8A4423E3190FABB60BEBE25F82B5DA7E4F58A6E10240229EEBC8E7441DFF181EEF9BEC97E54F0D77C75E861F2584D87DB3B40249710600000A3E5FBEF6591754EF38579D63E052CAD9BEB179C60642DAE5D68E91BFD60F2DFD47E9F9727F53746BE383F785FCE579015DD2048049EEC53F2476AC15E695ED077DD225F2003BFE627B32AC9E3345D92BC42249710600000A3A5D1A7804F7D5D4476EC46EEAC5EE317A060E43C5B3F85BD5D666DD1A6961C567E90DD6CB8BBAD332F4FEAEF8C7CEA6B7C029874A805BB27B0A0F58E595A43067ECD65BBBE238FD3798A2F3C24928B7B300080D172FB9835649DD37CE8DD02B2633712DB02B78E7C8F7E3E598F2A83703C58BBC82F1DAB9F153EF45EF26EF39EB4A8016D0459D8C29E9F1F2703BF6671E535C39DCA9CEEFCFF10C9C53D1800C068593E7D1959E7345D190AEB5512FABBDF91E5057E010446C6F5D59BC8FC0FC7055559CDD2E1E549FD6D307D5F00A8F587AA579A6D25EFAF4437081285148FF20455D89A7CD31FBEF90F15FC3553DF3B441EABE9F078BB8BE4E21E0C0060B4BCB27011EBD42D8FAC779A9EBCE364071FCCBE6527D8F5C671CD0209B4CF92BA799626E60573F8C94276ADD17F02E70DB51C7979527F1F4C4FAEE1DE2E17EF19C67E20BA41902874CA2CFE13B5706F0414B69F3B0BBF2703BFE687D34E92C7693A24799E482EEEC1000046D3B4FE7BC97AA7F9CA3A6BEF972BEA66FB0512689F7CB31EBE690F95EF56E5FB06F0FD0302D3E2E548FDBD91AFAC35FA0240CE115D204834D4C295030ADBCF89ABEAC8C0AFB970DB69F2389D5522A9B8070300184D3F1EB283AC779ACF4DB1B627C0DEEA55CD82090CDFDBAA932AB2C93C0FC7BD357479F172A4FEDEC867A7183DC595A78A2E10241A2E8FF21555E89AFDBE3E41067ECD82B22BE4717A5D6905FF2C928B6B300080D174D1E71BC97AA7D97998B54D651656367F970CC3777BF53A32BFC3715AC55E322DEEA2CA1DE43146F27A43D5A7263D72A6E80241A2E19094E7C9421776EE73809537D2C15FF3B941C13F217148DEAE22B9B8060300184D8DF60448E9AEF82674519D7C3047941F20030AB42E7F1CDFB7CCDA16BF2D39B4FC20BBDC30814C8FCBCB913A2EA8252758BB1EC9B5A01BD0D12643FE6BB5906F372B749DD9872F92815FF3ED0965E4719A89F208090300184D2F2D58C41EECE625EB9EA6B43FF489807C619A0B0D93C9A0024397AFAEF851B9B575195A924FEE2BAB9B43A6C7BDD030C9D20243EE6CC3098057EE4B2DFE89E8024122A216727140A1FB397D7D0319F835676D6C248FD3794C2415D7600000A3AD3420F844C097971D263B7A23959AA5646081A13BAB723799C7E1B8AD663D9996262F3FEA3823797DA1EAD15DBDD9A2FB03898A4B9227D2857FC741E3CAC8C0AF997BF412799CDE3619F2DF89E4E2160C0060B4FD7CD836B2EE693E3DD6DA4440EA9B7218BAD9352BC9FC0DC7F11539BE0985547A9ABCFCA8638DE4F585AA473AC788EE0F242A0E8FFC2A51F04DFE76601119F8F53E35A0883C56534D23552417B7600000A3EDBAAFD691754FB3E35BD65604E44BCA5281059AB7A17E3A7BB32CF45730C17CA7EC303B6FE2F50C2F3FEA78233BBE954FD6234D1E1B44F7071215677ACE3F5285AF572EB94C067ECD01634AC9E3341D92FCAD482E6EC10000465BA31501B91907AC4D3EFBAE612A195CA0B17C31252BABF00593EFD678B46E01999EDE33F553C8E38DE4F584AA3F7ADBBD21FF8BE8FE4022A316765960E1EB9DB7E51419F835A7ACA9278FD379402415B760000063C1673373C8FAA7F9CA1A6B0B02EDAF5941061868EC428B8FE083B9AE7A339956A0BCDCA8E38DFC835A4FA8FAA33361D6700106A8853D3BA0F0FD7C7FF24932F06BEE3E78913C4EE7AD144FF65F8AE4E2120C00602CF8C1FBC11704FAADC57900732A7793010606B7A07609999FE1F8D5492FBBA5DBE52F98B32BAC6D00F4DBB1C15FDB3A25658EE8FA40A2E372CBE97425B8E30BEF1C2203BF265F2B80AF19401DABE9702BBF15C9C5251800C05870F9E80D64FDD3EC38C8DAA3E8F7CA8AC900035B963F7E1F64F316BF83CA8E86F43AE6FDF262F2778CE4F345A8FAA3E990BC1ED1F58144C7D12DF7DFA94AA0E9F2C8ACA8FC2A19FC357B7D19FC9D92CBA37C26928B4B300080B1605904E701D4D4CF24830C6C2EBF43FFF2A44CE6A355F9B7FC85B58BC9F428797951BF6324DEFF8366A8855E1B5809F42EC9FA8E0CFC9AE396D792C7DD35BEBF29C50000C68A46F3005E5E616D3D80F5551BC940039BBBB26A2B9987E1B8B46A3B99564B5ADD66F8F7CB8DBEFFC7FBFFA4C325C94B888AD0E447332AC8C0AFB92DFF02799CCEEBF77729FCB9482EEEC00000C68A46F3009EF8DCDA4A745816D89C87EB16FA66E9537968559EF7A16ECD6C69F95FD527BEC0FB7F10803A00E84357863BFEE1836232F06B9636DC601D7B067FAFD4D693FF88482EEEC00000C68A6BBF5A4FD641CD763D1596594277FE46D6D7CF20830DBC235F76F7DD326B4F585A7240E9B190F3BDB17E3AF95B46F2FD22DA67065FFF1FDFFF27210E29FFBFA8CAA09992AEB0C355D7C8E0AFD96DD431F2584D1E4445727107060030566C98B384A578E87AA8D935EB1819008CDC5CBD810C38906FF13B968D3D994BE65B38E6D686FE09262F27EAB78CE4F582AA2F3A6FB5EB52F84BD1ED81A46118FB815AF8DF0554063F57ED3D43067ECDAF165793C7E9DC2E528B3B300080B1A4D1BE00BF9B7A880C00467E76329F0C38F05BB6A97A239967E138B77217999691BC9CA8DF3392D70BAABE34E996F78B2E0F241B2E495E4B560AE1A773ABC8C0AFB931F71C799CCECBF1BABB14060030969C326233590F353B0DB61620B8A7EAA7914127993D5937C7B72B1F955F56E54BF85E09B2C56F4B9E6E986669F73F6EA7B783BFA65507001F8A2E0F241B0EB777305929845D3F3A42067ECD13F53758870C83F5A5A5FC14915C5C8101008C250B27AF24EBA15E4FAEB5B5E9B755AF23034FB27AB96122FBB0CCDA024B2DD9AFB48455D5CD22D33372BB5A3ED46F1AE9F11A6EFFCBFBE7DF882E0F241BCE74C545550ACDF6190A3B567B9D0CFE9A6F7C1C7C894997477E5B24175760000063C9AB0B16B2277BE6927551F38539D61689197D5221034FB23AA5621F994FE1B8AB660D999619ADAE3FF0C26C83C7FF925CCF5F058B2E0F241B6D32E41FAB95E06240A5F07343EE3932F06B8E9A5B451EA7739D482EAEC00000C69A233EDC4ED645CD87DEB5B62A20FFC48D3F66A6824FB2B9530DD4541E85E3C48AFD865BFCB6245F25D0EA2788BC3E50F5E4AEDEC9A2BB03C98A1AE8B6D195E38E5F2EAA2603BFE6EA7D67C9E3749E4B4D5DF243915CDC8001008C35F78E5B4DD645BD9E3C6BAF01CC6E4663D66B8D13D8650BEFBBA36955FD6CD6BFD4DAAA8A2D39A4FC10BBD430914CCF8CBC5CA8DF35325D315EFDCF99EE7D54747720597148CA50B27208BB7D7A8C0CFC9A476BAEB376E9C1BF336D9B96F76B915CDC8001008C352F2F58C81EEF1EFC35C08B73ADBD06E06BCCDF6A34B7214D4BF2ED6C6756EE6183CB8E36FDEE80D2E36CDCC91C9653B3DCF48637D1F06AC378F67179A15F9E846B9FB213ACB46E2E999E197979F00104F5DB46F2D74154FDD079BAD3B0AC1F89EE0E242B0EB7F23051399AECD8239F95D6DF2083BFE62B1F04AF6C2EB7DC5724173760000063D18F87045F15D0EA6B00EEC1DA45642032F252C324DF636EEA37F50E2F3F10B3FB0FCCA9B4B6CB5E303785B9C6022F0FEA77CD68F8F8DFAD4C175D1D48665252B37FA656886BCD2A88CE6DCA0532F06B7E3CB3923C4EE752915CDC8001008C45778D5D43D647BDD27E6BAF0126556493812898E71AA68474E7CC57C13B6471A01129F9C23CD4B986E3B727737D0B0951E999959707F5DB464AB9C6B3FF533CCA13A2AB03C90EDFB887AA249A6397D792815F936F1C441DA7E990E4C67BEE61F78AE4E2020C00602C7A69C122D63923F86B80E7A658FB848D3FB2E6019D0A4694FC913EDFCB9EFAAD60F62D2B614AED32F2375BDB86FAE9ECCD326B03A6967CA7EC083BDF30994CCFACBC1C787950BF6FE473530C67FF9F8DD7F55940045003F428A29234D96B740919F8358B2AAEFAB610A68ED5FC8DE4FD9F22B9B800030018AB1ABD06E8D027DFB7063C151C8C0CE5B17576CD4AF237CCD8A7F404DB57B38AFCDDD6F246E33836EAA4F55726947CC11EABAF52F46EACB2B6F46FAF9213AC43DFE06BB3A84E11DD1C00EA00C0233F4D5492261FEE55C0CA1AE9E0AFF9E2BBC1479D2E49E926928B0B300080B16AEE8455649DD4DB65ABB5BD01F80238661F5DF377FAD46F989507CB1DD56BC9DF6E0DF976BCD4798523DF36984A2B14F9278343CBAD3DC5797DCB51B23E04D841747300F8D603F833B552DC0CA8247EEE3E78910CFC9A43A69C248FBB6B7C6D398901008C55F9A240CFF7DA4FD64BCD27BFB4B6453097CFE6A702935EFE7D3A75AC1557556D21D388A4FC2EDDEAF2BA2DF9F94985DD6C086D8B5F4ABEFD30F5FB667C72B4D1D6BF7259BCBD8E05AD80D323171095A5C9C9ABEBC9C0AF396FEB29F2389DE522A9B800030018CB4EFA24F8DE00AE0C85753F64ED1DF2641393018B6A1793C75A7571E576CB8BE584EAD9FA297E9F2ADAE15BEAEFD9B59892992F2A287979F372A7EA8326FFEC5B747100DC45AD1C63022B8BDEFE634E90815F533971993C4E6F9B0CF99F4472310F060030963D3963A9E1BC9B17E75B5B1380DF19D7197CAEE7AD59461E1B8EF32BB3C25E8BC048AB13178DCCAF5D4AA617AA7C52A2D595FF5E9C7F98AC073A6FB795BCBF125D1C007771BABD2F1115A6C9C7FB1592815FEF33030F92C76ABA24E535915CCC8301008C75BBBDB987AC9B9A0F0EB43E1970A11A8CA900A5C95F1350C785EBF4CABDEC6604170C5A6B7165BD601AE555282EA8CA22D330542DE78E6F194DFEF3EE12DD1B00FE3CE0CEFF6F6A25B9DDBCD2DC35E7E82532F06B0E1E5F461ED7A45B9924928B79300080B1EEAA2FD79375532F9F1446060C03FB971E67DF374C228314977FE666F54ED548FE08FC7AE37832DD703C563BDFF6731E517EC0B673E5F9CDF39D4AC7483393FF1C92B7ABE8DE00688E5A498E06561ABD33373690815F73C6FA06F2389D874552310F060030D6BD306F117BCC6069E0C747595FDE767DD54632506946E251BAE637155E4B7BE7B7240FAEEF95597B25D2927C99E3BABA19647A56B4BAEE3FF7F1518693FF4E774ACBFAA9E8DE00688E5A49A604541A3FF91D3E15F835F71FB9481EA7F376BB2E85BF14C9C5341800C078F0AB8FB692F5B3C97485A51758DBE0E6ADB263BEBDF1A960C5E577D4D47176F9D9C9FCA04F21CCCA27175A5D552F987C1D042A3D2BF2C1CE208B131379F9F27226CBBF49EFE7A26B0380C6E9F176A12BCF1D9F7DEB2019F8F53ED1AF903C56D395AEBC20928B69300080F160F9F4652C45F2927554F3D929D63694E16E365818687685FD6BE8EBE58FD843599D90725BCD7AF2B7C3916F7A44A565D54DD51BC974CCC8CB972A779DB79D6905FF26BA3600685C6905FF4C541E3F9592CB64E0D7ECFFF509F2B8BB7ABF16C9C5341800C078B1F7A0DD641DD54CE9A1B0EEC5D6DE7DF3CFE5823D8AE7ABE9F175EFA963ED7258F941CB9FD89DAC9BC3FA5A5C52B725F9223D76BE9EE05B27BF5D76844CCBC81E874FB0763D8DEEFE950DA25B0320382EB75C4157A23BCEDF7A8A0CFC9A9356D791C7E9544452310D0600305EDCF6ED5AB28EEA7D619EF5F7DF5BABD791814B930F02F8DA01D4B176C9DFDF87BA93200FD21F955B5F1089920F26CAEAAD6FF14BC9F3974ACB8CBC5CA9F2D6EBF228CF886E0D80E03824791E558934874C3D49067ECD5D4586F3006EF295074572310B0600305EBCC25706EC1D7C65C00EFD14D6F3381D448CE4EFA62F05990BC0E5DFD7CFA9DC4D1E6F977C939DAAFAD964FA94FC313DF53BE1B8DDE6A58B2FAB8314AB8B12659694B207FA1BAEFB5F9A9ABAE487A25B0320380E8FB73B51899A4C7DEF1019F835F99E018FF43E401EABA906D7274572310B0600309E5CF8F906B29EEA7D7995B5C7CC5CFEFD3C15C0F4F2457C16545AFC8EDDA47CC5BDD23AE33BF0BDD5ABC8E3C3717C458EEDAB15AEA9DA44A665465E9E5439EB75B9E59EA24B03C01857BAF2BFA98AA4C9571FE3BBFF51C15F33F30B83FDA8DDF248915CCC8201008C27CF9BF824B0D360EB0B03F17DFCCD6C71CB03E48ACA2DE46FD825FFFC8EAF974FA5CFADAD9FE1FB1BEA58ABBE5F5E6CCB17097AF9EF0D2CB3B669132FC74E6F1790E5ACF3549B0CF98F4597068019D8BD6AC56908A8487E2EDBF91D19F835C72CAD218FD3B9572416B3600000E3CD099F6C21EBAADE57D65A5F037F49D5763290516EA9B67FE6BDDEBE6525ACA07649B374AF378E6323CBEDDEE2F784EF93C7C0B4C27549A5F5DD085F5D6BE2EE5FEDC344770680799C6EEF72AA42690E9F5549067ECDADCA05F2389DD7627D648A01008C371B662F650F760BFE4960C7C105969F02F4293BE1BBBBA68219E5AE9A35115B2D90DB47FDEDFD352BFCD28CC42B88F5D59BFCD2B0C37A351FAD7E9DE0BBFB7FC7F0EEFF8A333DE76F44770680799C92D29FA8504DBE36F43019F8354BEB6FB08E3D824F4E71A57B3B89E462120C00603CFAE987DBC9FAAAF79535D6E7024CA8D84F06B496CCAB5DEE1B3850BF65877CE3A21D62621EDF9087FA9B70E4AB1D466283223E9F804ACF8CAF98B8FB8FA765D7418CE172CBFF8FAC54C276E90A3B5A7D8D0CFE9AE9230DD6A676CB1F8AE462120C00603C5A397319EB906EF014E0AD02D6ABC47A502EAE6DF9FD3BA552BBCCF7C89EFA2DBB5C51B5D5B77221F5DFACCABFCDE75B0753D7148E616DA4A4961B7F8A4395ABCEEB8EF4DCFF21BA320042837F36A256A2730195CACF35FBCE92815FF38B85D5E4717755B68AE462120C0060BCFAC907269E02ACB6FE1460F8C9C29077ECE38306AB1BDD4443FE64A1B07631792DE1C8F38DE71F95A61979B951E519E014D18D01600DBE7A1451B19AFC6C5E3519F835D7ED3F471EA7F3629B0CF9C722B99803030018AF56CE5C6AF814E08137F359A6C57501B8468B03519EA89B6779D67B6BBBBC6A1B790DE11ACEA23F7C1D07BEC533559E3A71F70FC2C7E996DF252A579369C38F90815FF378ED75D63E23F812952EB7EC14C9C51C1800C078768489B9002F2DB0BE3A20FFCCCECAF2BC1575B32D2F7BDB5AF24D88F8EA86D4F987E399FAA9ECCD32EB4F415E9A6F62D53F499E28BA3000ACE3F278DB53154CB37D378595D45D2783BF66978F0E93C736E991DF12C9C51C1800C078D6CC538076990AEB7EC8FA5C003E918D0A7446D6D5CFF47D574FFD66B4E5019ACFD0A7CE3B5CC3D991B047F109D6BE97D19AFFF235BE9F8BE8C200B0CE7DA9C53F512BD4A5800AE6E7C6BCF364E0D71C39A78A3CEEAECA6A915CCC8101008C77477DB88DACBB7AC3D929907B80F816DF8CFCE901DF5487FACD68CABF5AA0CE375C793E51E999D5C48E7FAADE71A2FB02207C9C6E398BAE6877FC7A71F079002BF69C218FD379E69E61EC0722B99802030018EFF275011ECEC823EBAF664A3799A5CBD667E8BF5B76985DB2B8421EDFE6976FF74BFD6E349C57B9933CCF70BDDC3031AC271EDD9412E652CB892A3F9D17F0DD3FB015B5527D1C50C9FCCC18758C0CFC9A87ABAEB194F4E08FAD52D2E4FF23928B2930008089E0D4919BC9FAABF7F1CFACCF4AE7CEAAD84D063E33F2C1C3E72715F2775BD361E545B66EF1AB7776C52E324DB3F2F2A1CA4DAF4352DE135D1700F6E04CF73E4A55364DBED84F69C30D32F86BBE3CC468E28AD24B24175360000013C10BF316B1673273C83AACF7F5CDD69708E65A7D15C0E53BE27D7952267FB735EC5F5A12D22E83A178B0761199A659BB6C35584FE58ED558F31FD8CEFD5D0A7FAE56AEEB0195CDCFED05DF93815F73D8F40AF2B8263DDE4522B9980203009828AE1CBD9EACC37AF9E76599C7AC4F08E4FBF55F34D8323898D71AC7B37127ADAF8E178E7B6A5691E714AEFCE946388FFEF9677F1D07192EFAC31C1EF90DD16D01602F6A05CB0DAC707AC7AFA82503BFE6A21DA7C9E374D689A4620A0C0060A27865E122D6A5FF3EB21EEB7D61767833F32757649381D0AC371BC6B169157BC9DF8E94532BF691E76287332AF790699A959707554E0116C6EA3C2A900038DDF21744A56BB2F7972564E0D73C78F2AA6F0B61EA58CD148FF2AF22B9980103009848E64E5865DC0EBB2B2C5D096FC9DEEC9A956430342B5F779F4FC6A37EDB6E87A877E75627301A9953B3824CD3ACE9CA715F7950E5A4F3B6EA43A2CB02C07ED440F86C40A5F3F391DE075859231DFC359F7FDBE013168F5712C9C50C1800C04473C8FB59645DD6FBD888F02604DAF11DFD6D7510B0388CAD72CDC877E22BAB9B43A61FAE0DF5D3C35EF1909703553E7A5D6E79BEE8AE00880C1D7A16FD855AD96E05563EBD7B0E5D2403BFE67B93CAC9E374CE14C9C50C1800C044B37EF612D6392397ACCF7A5F5E11DE4A7D9F9417FADEE953C13114B754AF277FDF0E23F5DEFF7AE338F669793E99A65979FE53E512E0F9F652FEDF8BEE0A80C8A156B6A280CAE7E7D4B5F564E0D79CB3B9913C4E67A9482A66C0000026A2F33FDB48D667BDED7A2AAC5B4178AF026656EE210364A8F24100DF98874AC3AA1BAA369069D9E1DCCAF03EF9CB282CF1ADD048958B5EF5EEBFAFE8AA00882C7C8529AA126A0EFCB6940CFC9ADEE397C9E3F4B675E7FD77915C4C8001004C44AF2C58C85EEF673C21F0B191E1BD0AE0863B1F40B3A076892D9B08F1C7FEBB6A569369D8E1BE9A5564BAA1F884896FFE558B3A0DCBFA91E8AA00882C2EB7F23251099B7C72402119F8F53EFD661179ACA6C3ED7D45241713600000135565D22A962205DF2780FB875587C9206556BEFD2FDFFC870A96A1CAE7157C516E7DAD00BEE2E0C908BDF3E7F2EB0C77BBE357D6987AF47FCB21E5A7886E0A80C8D3362DEF6F898AE867DEF14B64E0D71C38B6943C4ED3E1912788E462020C0060223B7AD856B25EEBE58FA2BB1D08EF55005F1FE04CFD1432685A912FAC13CACA817C3EC2FE9A15EC56C358F2F7EC902F691CEEE646196A3E9BD8EC8739DDF237A28B02A0F5502B5F49B3CAA873F6A64632F06B4E5B574F1EA7E992E48322A99800030098C89E9FB788BDD83B9BACDB7A3B0F3BC07A95585F2088FBE9C90276ADD1DE6576F966423B6BD6F8D60DE0AB087E5876D037D8187EB2D0B71EC1E6EA0DACB26E1679AC9D5E6F1C1FFE52C627CCCDFA572DEB9459FC27A28B02A0F570BA95E944856CF2DD89E564E0D7DC577C913C4EE7ED3619F25F8BE4A20E060030D15526AE34F52AE0C5F9E16FDDCB17DCE19FF75141345EBDAD1AEE623FDC9716985AF0E7B6DA277516DD1300AD8B4B52D2884AD9E473830F92815FB3BCF1267BB4EF01F2584D8747FE9D482EEA60000093C151438DB70CE63B06A6ED097F12DECAAAAD64208D57F9F550D7198AEE7DC7CDECF41773AF484192D1EE0DF95FA88AA9B7A0F40A19FC35FB7C55421EA7E990E42F457251070300980C9E9BB798FDAE97F166410F0CC867DD0F85F72A80BBB57A1D194CE3CD9DD56BC8EB0BC51EC527D8836F1AAFF5AF7AB2BDB4F717A26B02203AA815B132A062FAB970FB6932F06B4E5859471EA7E992943C9154D4C10000268B327F1560B04C30F7D1E18561CF07E0DFF4DBF57960B4CCAB5D1EFEDA04274AD913A34CBDF7E78BB061B95F107D9C92B230A072FA39745A0519F835B30E7C4F1EA7F346AC8C74310080C9E4B84F8CBF0AE0BE3027FCF9007D4A4FF8BEEDA7826BAC5B54BB98F5290BFF4908CF472A7F9BE996478A2E0980E8E2F4C899642515A6BE77880CFC9A650D37D84399868FBC1E13C945150C0060327969C12296D67F2F59D7FD4C575897ED762CCA53E20BA654908D550FD72D64FD4BC3FB2C92DB75C7315F3E92F9EBAF7C5F6AF14F4497044074F98DC7FB9F44256D923F463C5471950CFE9ADD3F3B4E1EDBA45B1E2E928B2A1800C064F3E48CA5A6F60A68DF4761DDF2C30F84F13408B02BF8A71794B00E7DF2C97C0DF02C9F7725BA2300620176AF5A314F0754543F97EF3E43067ECD6F16D790C7DDD5BB4B241655300080C9E8CEB16B0CB70DE6761A9CCFBA1787FF283C1E06017605FF9E474EB04EEF989AF477DB95AEBC20BA22006207A7475E4554D82647CCAE2403BFE666F93C799CA64392AF764ACBFAA9482E6A60000093D56F879B9B0FC017AFC93C4107BB50ECA706D7581D04F09507F9F951E71D922527D8639F9A9AF4C7FBC051A21B0220B6502BE8C0C00AABF7F56187C9C0AF79A2EE067BB07BF047608E34E501915CD4C0000026AB7CC3A0CC417BC87A1FE8D3630FD2012F44F9C440BE990E1584A3259FEDCF3710A2CE37549F997088CCBF401D92B2131BFD8098A56D5A5E5BAAE26AB64B57D8D19AEB64F0D7747F72943C56D3E5F6BE2F928B1A1800C064B67EF612F6544FE3F501B82F2D0C6FD3204DFE69DDFAAA8D64306E6DB3AAD7A8E7634FF07F69D16132DF08EBB1C73F8869F8E854ADA817022AAE9F6BF79F2303BFE6E7F3ABC9E3746E12C9458DD61C005C59B2945DD892C5CEEECA616777E7C6BDE777EC6597D6AC27AF15C68F791356B10EE9C64B05F319EDAFAE3F42063F2B2EADDA1EB56583F9F2BE76ACF0A7F9EA06F566C7DC8CFF6BB1F0E4130043D4CABA39A0F2FA397A413519F835D7649F258FD3F97DB41F83B5C600E0CAB2E5EC54FE5156D118FC8949BC5A53769A7DBF793B79ED303E5CF5D57AB2FE07CA970B7E7DF35132085A916FF273AD713C19A423254F6F7A45F86BFB6B76D97694B9324C057FBE085A37D1F50010DBB83CF210AA126B4A238E92014193BF2268D72D78C37048F9BF11C94585480F002EAF5CCDAA2ACE92F9935036DE6067F629641EC0F8D0EC224129DD15F6C60EFB060123CB0BD899FAA964B0B65BBEA56FD8BBFAE97C63F771353FE87C6A668C7CFA0C8029DA7AF21F242BB2F081EEF9EC44FD0D3A20085F1B16FCBD984B92DF14C94585480E00AE2E5EECBB3BA6F22551BDB07527991730F6BDBA60211BF25E16D90E026DD75361697B8F9341D18A7C8BDFF2FAB964D0B6CBAABA596C4898FBF9EB75671F67ED32CDDDF9AB2EBD6718FB81E87600887D9EEC53F2476AC5BD125091FDDC229F270381E627B32AC9E374AE14C94585480E00CEECC923F32491ADAA3CC7AE2D5A44E6078C7DBF9FBF88790698FB32A07D2F8549B9F60D02FA971E673935CBC9E01DAEB9B52B7CBF4FA56B458FF7B8D9857E54BDDE3619F21F8B2E0780F8C1E99177D395FA8E6396D490814073D9AEEFC8E3747E17CD9171240700B525F5649E24BADF6F087FDE048C9E0DB397B2E77BED27DB43A03C084AFBED0BACDC3995BB6D9B1770BD719C6FB221958E55F9F59A0FFE72A9333DE76F447703407CA106C81144A56EB2C7E7C7C920A0595C79CD7007B2B6DDF2EE13C9B53A911C0054345C23F324D13DBBD74BE6078C1F2B672E65CF669AFB3C903F067F6377F8FB06E81D5E7E80D5D6CF2083BA591BEAA7FBE61750BF6F55BEAF3F7FF241E503614D5BC9FB2BD1D500107FA4789427888ADD64A7CC02DFE63F5420D0E49B0751C76A3A24B98748AED589D400E0EAE225645E2483DFE516917902E3CB13D397B1277A18EF19C0E513E1ECD83C48EFC0B263CC5BB38C0CEE46F2E3F8F1D4EF5AF58D9DC77C731FA8EB273CE574E7FF87E86600884F3A6516FF895A996F04546E3F77167E4F0602CD0FA79D248FD374B9E5F922B956070300FB3D9D5B48E6096C1DAF2E59CACE64E7B3DAE375ACBAE2AC6F5E8655F7E435B28733CD3DEEE65F0774D96ADFD7019AFC53C18B0D13C9401FE8E586096C76C52EF277C2B16BD63196D2C374F03F1FEDAF9B00B00DB542CB0115DCCF09ABEAC840A0B970DB69F2389DD522A956070300FBC500207AF28599AAAA824FCC0D55BEAF47C71EE60601AE6EB26F511C2A8886E33B65870DF7113852B780BD6FE32C7FCD57379AFFCE5FF5A26A07D1BD0010FFB83CCA570195DCCFBE5F9D203B0ECD036557C8E3F446EB5D190600F68B014074E477FE76077FCD35FBCEB20E192607011E99BDB4C0FE40CC97105E5095E5FB965F1FF8F9BF2FACCCF2FD77EAB870E4CB1F9B5CE18FCBBF987A4C742D0024060E49793EA0A2FBD9B9CF0156DE48771C9ACF0D3A481EABE9F0C86F88E45A150C00EC170380E8C81FFB53E561971B73CFB18E3D4D6D73EB936F2094594207D670E4BBF67D53E165B32A76B3311579F6ECE21768C909F6CC84E07D5680179DE9DE4745B70240E2D02643FE6BB582DF0EA8F07E66175F243B0DCD77269493C769BA24799A48AE55C100C07E3100888E75C783BF8AB3C36DF917D823BD0F906D85F2D14F0A598FC3F66CB6D35AF63C5A6A7A4B5FE1B914775E3BD1A5009078A895BC38A0D2FB396D5D03D96168CEDED4481EA7F3B848AA55C100C07E3100888E7CC21F551E769B75E07BF66808838087DE2D60DD0E44E02E3D02F2F37CE83DF3D7A67ADA99AEB411DD090089897A873E91A8FC4DBE35AE94EC2C34738F5E228FD31B8D2D323100B05F0C00A2635D2B2E3CC59FF83D39C0FC5D32FF769E6F9A4305DD5891EF6FD0A1AFE9057EB8F56AF0BF5F742500242E0E8FFC2AD1009A7CEACD22B2A3D0CBFF863A56D32529BF17C9B51A1800D82F0600D1F1CCFE02B23C22658E3AA87FD6606E8F9FE90A7B716E31EB75820EC051533D1F7E5E214CF6E3963AD30AFE4D7423002436295D0FFC03D108FC948F5F263B0ACD37BF2D258FD374B995B122B956030300FBC500203A5EE15F015446E62B80963C547195A50D3F42B69D967CFCD342D6A33836E605F43C72823DF179F01B13C29C765D0A7F29BA10009203B5E2970534043FE76E3E4576129A53D7D493C7E92C1449B51A1800D82F0600D13312EB00185952779DF5FFE604D97E5AF2C181F6EF2110AA3CFD8E6F85F4C89F4F565E92929AFD33D17D00903CB8DCF22CAA5168BE3FB99CEC2034771FBC481EA7F3568A27FB2F4572AD020600F68B014074D55602E47302A8D5FD226165C539F6E59C32B20DB5245F5CE7C579C52CB3B55F09A8E9FD7ED911DFCA85D47905710CB6F405498B43F27A8846D1E4F3EF1C220382265F2BC068F6B0CBA33C23926B153000B05F0C0092D7E5A337B0F61E2FD9965AF2D18F0FB06E05ADF395004F87A7479D4710AFAB373FE9A2CB0020397174CBFD77A27134C957002B2ABF4A0605CD5E5F9690C7DED5FBB948AE55C000C07E3100486EBD1357B1A77A98DB4950936FB2C3EFCAA9A06D97AFAC3BC2DAF70AED91BF43921BDB7AF21F11DD0500C98DDA286A021B89DEC559A7C9A0A0396E792D795C936E79BF48AA55C000C07E310080B5B397B2EE03F7906D2A984F7E59C4BA1FB2778220FF3DFEBB547A41F5C8BBDB64C87F27BA0A00009F04433616E147D32BC8A0A0C95712A38ED37983EF4028928B381800D82F0600D1F7E2FA4DECBB9C4276AAE0186B3C70BC553DA51C66E7B2F6B14B8B97B2311F6FF53D19A4DA564BB6CB144F036C981BE0BBEBEF13F2BB7E55EF6435F8FF58741300008E3A00E84337983BBE3CA4980C0A9AA50D370CD71357837267915CC4C100C07E3100889EFC53C0FAE29364B9B4B695D5DFB3EF376F673BBE5DC33A67E492ED2B989D871D609E5C6B5F0AA42B25BE6588A9DF35F07B975B7959740F00003D0E29FFBF8846D3648A3ADA2FAEBC4676089ADD461D238F6DD2237F24928B381800D82F06005172D122567BAC962C936859D1789D7DBF61332B99B68CBDD17F2FD9C682C9BF1478614E31EB799C0EF481F2CD87F8DF5B98E1CF9553D2E4FF25BA0600403386B11FA80DE5BB8086E3E7CA3D67C8CE40F3ABC5D5E4713A7788D4220E0600F68B014074E48FDDA9F288B635A5A77CE77769C12236EE93ADBE9B04AAAD05B3E3E002F6CA9A232D0E04F8FFFECADAA3BEBFA38E37F0A6DA0F8CC0237F004CE092E4B544236A72E4DC2AB223D0DC98779E3C4EE7E527FB94FC91482EA2600060BF180044C7BA23C1DB5D34BDB47A5DD3792A1357B2E77BEF27DB9B917C16FF139F15B2DF4D3BE45B4380FF93FF7BA8B3FB7596BA3CDEF6A23B000018E1707B07130DA9C92E1F1D213B01CD13F53758878CE00DB6B5B6D7C400C07E3100888ED527833F798BA617B6EEF43BD7F3F316B1CF876D23DB5CEBA9CC692FEDFD85E80A00006670A62B2EBA41DDB17D86C28ED55E273B024D13EB87BF23928B28911A00702B1A82CF854854CFEE95C9FC8091953F6AA7CA2316BCB0691B79CE5963D7B26732435B33C006AB5A7BC131001206FEAE4C6D4417031A959FEB73CE911D81E6A8B955E4714D7AE4F522B98812C901402C77C89194CFFCA6F2034656FEF91D551ED1960F84F9F2C4D43973F9D300FEB9A095B901217A8B7FDEE77C2DE74F45F3070058410D9CDB8806D6E4E885D56467A0B97ADF59F2389DE7535397FC50241731223900E0EBB153D79EC8F24FBFAE2E5E4CE6078CAC9757AE89C9A74EA7BC87C8F30DF4D094152CCDC29702262D74B965A768F60080707048CA50A2913599FEE931B233D03C5A739DB533D87F3BC5E3FDBF22B98811C901009F07D0DA3BB345DBB33BF79379015BC7F3DBF7A88380E0AFDF5AD3DAA335ECEA22F303C22B0B17B1A55F6C600F6584B69F40102FF3367E5F6AF14F44930700848BDAB01E0A68687E3ED83D9F9DA8BB41760A9AAF7E584C1EDBA447EE27928B18911C00702FAEDDC02A6B2F91D79F6836E61F21F300B6AE7C15C0BAA3D5511D08F0812F7F02C6D726A0CED1C89A994BD9A8A1DB42DE5848E735D5F1CEF49C7F144D1D0060177C4F6CD1C8A8C6E7739B7281EC1C343F9E59491E77576599482E62447A00C0E58F6679874CE54122585973919DDD9D4B5E3B8C9EFCCEFBF2CAD5ECF2AAB5ADEA95E52BC8F3B1E2EEB16BC876D982B75515F5C6E1AD94AE07FE4134710040247048CABE8006E8E7B7CB82AF4AB624EB3BF2384DBE13D73DF7B07B457211A13506009AFC5BE8337BBDECB4F7A0EFBD68BCFB5DCE01DFE75DFC550775BD10866BE1E49564BBD4744972B51AF0331D6EE5E15FA715FCB968D6008048A306E85154A3D4CCFCE23819F8358B2AAE1A6E1412E9A5395B730000210C4DA3018053F27A45530600B4260E8FFC34DD28EFF870AF0256D648077FCD17DF3D441E7B5725432417113000803076C500008018A54D86FC676A23BCD9BC51DE75F7C18B64E0D71C32E524795C931EEF5C915C44C00000C2D81503000062188724E7D30DF38E9356D791815F73DED653E4713A2B4552110103000863570C00008861D44638A679A3BC6BFF6F4E90815F33FFC415F238BDED3CF9FF9F48CE76D4DFFF20303DBDDF0EDF4A764C10C2C8EB9DB88A6C974DBAE5FDA22903005A1BA7DBFB12D930858FF72B2403BFDE67061E248F6DD2ADBC2E92B31D87DB3B804C53386CC80EB263821046DE8D5FAF23DBA5CECDA22903005A9B07DCF9FF4D6D84FCFB5BAA71FADC7F24F8423883C79791C7DDD53B5924673B7C7041A779C79E6FED263B260861E49D3D6A13D92E355D6E79BE68CA008068A036C4A3810D53EF8C0D0D64E0D7E4FF9D3A4EE7119194EDA478BC0E22BD261F48F7B2B373ADAD6606210CCFDE837693EDB249B7FCA168CA008068A036C429CD1AA64E7E874F057ECDFD472E92C7E9BCED4CCFF91B919CAD74CA2CFE13F5F7AF07A4E7E7D6316BC9CE09421839BF9BBB98755007E0549B6CD2ED7D4E3465004034707ABC5DC8C6297CF6AD8364E0D7FB44FF42F2D8BB7A5F14C9D98EFADBD9749A77740FD8CBAE2EA03B290861649CFCC966B23DEABCD9A167D15F88660C0088067C963ED138FD544A2E93815F937F2D401DD7A45BFE4624673B0E49798F4C53E7B66FF11400C2D6B261F652133B027A7789260C0088262EB75C4137D23BF2EFFDA9C0AFC9D70BA08ED3E4EB0D88A46CA74D86FC4F6A1AB702D3D4FB64CF5C56357329D9594108ED936F09DC7FF02EB21DEA55FB9C74D1840100D184AFD84735524DBEE21F15F835771519CE03B815C9CD3E9C1E791591A69F5DFBEDC584400823EC17C3B691ED2FC0D37CFE8E68BE008068C2D7EC271A69932FBD7B880CFC9A7CCF80477A1F208F6D32CDFB9448CE76DAA6E5B555D308FA3923F795BED9AC7CC632B2E382105AF7FBF98BD890F7B3C87617A8CBED7D5F345D0040B471A52BFF9B6AA89A7CD7BFA2F2AB64F0D7E4BB0752C7EAFC54241711D4DF9F1D901EE963DD73D9A2CF37B24B0BF03400423BCC1EB78ABDDA771FD9DE08CB5352B37F269A2D0020FAB07BD586D910D050FD5CBAF33B32F06B8E595A431EA7E990947D22B188D0AE4BE12FD574825E83DE177B67B3E92337B3635397939D1A84B065F944BFE5A3D7B3CCB70CBEF5F7F7B64BF23E299A2C00205670BABDCB8906DBE4C7332BC9C0AFB955B9401EA7F35A9B0CF98F45723E9CE939FFE85BCDCF2D8F54FFFB02A7A4AC56DD6AD53B9B1B2986AF02026DEF9159A76E5ED6B97B1E8430880F65E4197FDFDFA2CA497D7BB5E00697242F517F6B8CCB2DF774A62BF7F39B17D19D0000ACA236AEFECD1BEC5D5FFDB0980CFC9AA5F53758C71EF9E4B13A1FE28FFF9C1E3953FDFFE580FF062184A15AC56F2052BA1EF807D195010042451D51FF3FA271359992AEB0A3D5D7C8E0AF993EF22879ACA6C3A3AC51FF69FA313D84109AD121C9575D1EE52BE76B397F2ABA34008059525397FC506D4867031B96DED5FBCE92815FF38B85D5E4711042D84A56BA3CDEF6A25B03009885BF63231A5493A3E65593815F737DCE39F23808216C456F3A24AF47746B000033A80DE79D8086E4E71B1F1F2103BFE6F1DAEBAC7D86421E0B2184ADE86D87477E43746D000023F8A333A22135D9BE9BC24AEAAE93C15FB3CB4747C8632184B095BDE14AF77612DD1B002018F7A516FF446D3497021A919F1BF3CE93815F73E4DC2AF23808218C82B58EAEB97F25BA380040309C6E398B68444D7EB538F83C80957BCE90C7B564878C7CF6FA27A56CF0F406F6D1A2B3ECD3E5E75BD5114BCFB1776737B2C1D3EAD99B93EB2084411C38A58EBD33B3917D1C85B63A7CC939F6DEAC532CFDCB0AF6489F22B23FA1F54E16DD1B0020184E8FFC11DD88EED86DD43132F06B1EACB8461E17A8CBA3B01E63AAD8B4DD37D8BC5C062184A69D9373DB3718E89469B007C91D6FA678947F155D1C00A0255C92B733D1809AE48BFD9436DC20833FF78B05C6AF00F8DE021F2F3E43366C082134EB846D57D993030F91FD4C80E3451707006889FBBB14FE5C6D2CD7031A8F9FDBF22F90C19FFBE48042F218BDBDC6D6908D1942084375E2F66BAC532FC32701DF3DD9A7E48F443707006809B5B1E404341E3FC72DAF2583FFF19AEBCCA9DEDD53C7683EDEFF20D9882184D0AA1FCEFF8EEC6FF4E28B00004CE094BC9F530D48B3D79725E40060EEE653E4DFEB1DB5F202D9802184D0AA737398F1AB00B7FCA1E8E200002DE1F228CF900D48D8B9CF0156DED87C00F0DEA472F2EF35F98C7FAAF1420861B8F61E1F7C4B72D505A28B0300B444879E457FA136965B018DC7CFDD072F361B00741F758CFC5BCDA7DE3A44365C08210CD7E14BCE92FD8EA64352768A2E0E00100CB5C114063620BD53D7D4371B0074FD38F86E802F0C3946365C08210CD72FD75E24FB1D9D8AE8DE0000C1704ADE7144036AB2FFD7279A0D003C23820F009E7DE730D9702184305CF9624154BF73576FB6E8DE0000C17049CAEFE9467447FE3EBFA8FCAADF00A0FF3727C8BFD57CA87721D9702184305CDF99D548F63B3AD789EE0D00108CB669797FAB3698A0F300DC9F1C65476BEE6E0E3466692DF9774D7A64361D2BFF410823E01F3E2AA1FB1DA1CBA37C25BA370080116AA3D91BD88802E50BFF7CB1B09A2DD8768A8D5F5E47FE8DDE2E23CAC8C60B2184561DBBF90A4B490FBE15B9C323BF2ABA360080110E49EE4135A47074A97EBDE112D98821843054F91A00BF1F7A9CEC6F74DEE24F3545D7060030426C0F5C16D090C2F6C19E07D88C3D37C9C60C218466E5C1BFFB3726F61F91BCDB44B70600308BC3ADFC566D40B7031B54B83E36A0888DD978996CD410426824DF4994BF52A4FA97405DE9CA0BA24B030084823A7A1E4135AA70E55B02A78D2A679FAE38CF6667DF221B3984106ACECDB9CDBE5E7FC9B7EADF833D0AC87EA5991EB9E09E61EC07A23B03008406BB570DD69F918DCB46F98E5E8FF53F18928FF429F27D92D82E5DF14D020A597510C2072277BC33478194FF37F177FC18EAB7F879F0F3A1CE13C248E96B03DD13BF0D3CDCA7D0972ED57704F196CBE36D2F3A320080551C92B7ABDAA0CE04343008218C4DDDF248D17D0100C2A54D86FC7762B7C0B3CD1A1B8410C68EEB3A0DCBFA91E8BA00007691929AFD33875B79986FB1E9742BD31D6EEF1AA7A46C0DD4E1514E100D13420823A75BDE727F97C29F8BEE0A00102D9C1EAFA436CACBCD1A298410DAEF14FE09B3E87E0000D126254DFE3FEAA87C3FD1582184D00E6B1C1E395574390080986218FB01DF64C821C9F944E38510422BD6A9BED35EDAFB0BD1D300006299B66979BF7649DE610E49D9A936DE53BAC60C2184C1BCC46F2254BF4DF1284F60A21F00714EA7CCE23FE16B75B795BCBF32D49DD7D1E9910B898EA199E9A32BD8A895176CF7B9778E90E985A243F29E76B8E537C86B8430888E34E501B34FD23C116A03BF7BF728995E889E71BAF3DCD43536D39DF7DF7F9D56F0E7A2CB0000241B4EB7F739B5D330FD09E2EFDE3B4AAE2816AE2F0F0BBEF56808DE72BAE5E158A90C98452CCF6D7A3D8E67D5C12A5587C3F5958F4F90E959902F35FE29DA0000A045D4BBFE4CB5A3B8A5EB380CE5AB974DD8768DECC0C2D1C601804F975B9E8F99CBC008A7A464A8F5E56660FD3172DC96AB643D0E471B07003E1D92BCF8C93E257F242E150000EEA006F2B7A94EC38C0326D7911D58381A0D0052D2BDE4FF6EE076E76B397F2A2E19003F441BB0B43157FF89B5643D0E47A30180C536B0036D0000D084DA298C09E82442F2B7830F931D58381A0D001EEA93C31EEA95C35CA177823998CD0C0251EBC5E8807A12924FBC7988ACC7E1683400F0B581DE39EA4080FEEF4194DB64C87F262E1D0090AC38246528D14184ECD8CD57C84ECCAA6606008F0DC8668FF6CF660FF4CC23FFA625F9DEE578140A34D436F01E554F42D5EEEDB7CD0C00781B786CC07EF660CF5CF26F5A927F1DD4292DEBA7220B0000C986C3E3ED4E750E56EC33C1DE47A06607009A8FF4DD1FEA23D195A9A94B7E28B202242962032E4B8FFD03ED3DAE86ACCB56353F00B8E3C3BE3640FF2DADB21A9FF80190848899CEA6263BF1C0DABE7BF0BBEC2706DAFB0834D40100973F0D689F11D22060B4C80E9084F06FDCD53A60AE0D74F3B20E066DE0B10107D9DC1CBA3E5B31D401C0DD3610D213B131223B0000C900FFDE576DF8A7033A02D2F66AC7F768FFFDEC61B5B3A1FEBBDE6F36D8F708D4CA0040F3C11EA63BC0DB0E49795E640B48229CE939FFA896BFA905B2DAF136D0CF5C1BF872ED45B23E5BD1CA0040339457020E8FFCAAC816004022C31FF9A98D7E6F602740C9EF24F81D85D6A9F04FFEA8BFD3CC1C5B4D7664560C6700C0ED9869BA033CEB48CFFD1F227B4012E06B031E793751179AA96F038FAA1A4D3AED31A68AACCF560C6700C00DA10D5C7074CBFD77913D008044C5E5513E233A806606067FEE033D8277289DFB16D9F60834DC010037840E3007F30192078747F984A803CDE46D804FB0D3D729A336F070EF42DBDA40B803006E086D807F19F0639145008044C3B763A024DF0868F8CDE48FFD033B3EAE9947A0A36D7A046AC700806BF651A8CB2DF715D9041298B6DDF2EE53CBFB7A60F907DA4EBDD3E7AFBE02EBD3237D8DDBC067ABBE27EB74A8DA3100E086F03A60A0C826004062C1EE7548CA3EA2D1FBC927FC05DEF9EB359A699FF15525D99985AA5D0300FED8D668F296F0BC7A07F47722B34042C2EE754ADE5D44D9FBE96B03FD9A077F4D3E21903A4E33FDCB0AB24E87AA5D0300AEC936F03D9F1B21320B009028A877B8E944836F26BFC3A13A104DA3BB89877C8F406F931D5A28DA3500E0F2018D99CFA3F872C122BB4002E2F0C86F50E51EE8237D5A0EFE5CA336D029F3009B63431BB07300C09FE8190D5CB82E495E22B20B009008A4A466FF4C6DDC7C4F6FB2D16B76CACC253A0E7FCD3C02E53B99511D5A28DA3900E0F2F3369AC4C86D9B96D756641B4820F8C24F6AF956079677A0FC9D39557FF49A690323969D27EB7528DA3B00B8B35686D3441B7048F92922DB0000F18EC3E3ED4D3574BD77263CD11D47A046AF013C5F9C243BB450B47B00C0353521CA23AF12D9061208338B5EB5EF66BE0DF04F03A9DFD04CFBAC9CACD7A168F700806BB20DAC17D906008867F8CC5EB5519F6CD6C875F23BE3CE41DE79066AF408F4C11E056C7676788F40233100E0AF02F8E42EEAF774DEE6932545F68104807FE1A1966B49403937D3E8F5975EA336F080AF0DDC22EBB659233100E09A7915E04C57DA88EC0300C42B4EB7E2261BB84EDE99511D454B76E68F1289DFD1FBC9D27364A766D6480C00B80FF7313E77D50522FB4002A0B681D78932F6F3C11EF6B7818F179F25EBB659233500E04B0653BF17E052917D008078C5E996F7138DBB497EF74F7DEE64A4D15DC41B9F86F708D46800F0787F6B9D1F97BFEEA07E53E7B53619F25F8B2C04718ED1A23F56DB80D16B80D747969275DBAC460380C7FB873668D16BA20DDC70A6E7FC8DC8420040BCE14C2BF837B52107DDE824D4BB7F4DA377891DBA17B059FBAC3F02351A00CC9A21B3A7DF0ABDD3E69A59CFC0E5967B8A6C04718C2BADE09FD5F28C4A1B689F91CF6686D1068C060033A62BECB703E97333F261131319B1360600718C7AF73F9C6AD89A77EE7CE80EC2C8A15FE519CEAAFF68D119B26333A3D10060E7CA7CB67939DF80853E3F23DB6518BC0775CBFB45368238462DC70FC9F2D50CA30D7CF8A5711BF870FE7764FD36A3D10060DBF27CB67D6504DB8024CB221B0100F186DA80CB021AB49F0F74373FEB596FB78F72D8056F014B7B57217F57F3B54FAC3F02353300B85650C0BE9E68ED1A3AF536BC03BADDEE0DF95F44568238452DC7E301E5EAA7D536E01EBA9F9DCBCB67E9EF076F037FF8E80459BFCD686600C0DBC098C9D6AEE1A15EC64F0152D2E4FF25B21200102FFC46F2FE4FAA41EBE58FC2A98E21984FBE99CD8EEDB8D3F1CC9D11BCF36BD72D9F4DDF7393ECDC8C343B00B82017B0AE1F867E1DFCAECF2905BF03C26B80F8A6ADE4FD1555AE7AADB48127D43670789BE2AB7F0B671BB501854DDB7D83ACE3469A1D005C94F3599A3A20A1CE35A86A1B70790CDB005E0300106FF0E04535684DBEAB19D92918387E6A9EAFD3E1D6EE2B6029068F4087CC3B4D766E469A1D0070F7ADB5F61894DFFD51BFAD1333A1E318B50D045DFDD26A1BE077DC5ADD6BC82E60ED0CDAC07B734E9175DC48B303006ECE7A8B6DC068EB6CAC8B0140FCC197F4241BB490EF6A467508C17C7650366BCCB9D3E16876FF20F81D100FE454E76664EAD0E3E4EF69EE59ED7F1E0346857E3D0F19BF06387DCF30F60391A520CE500700F389326DD24A1B7886B781FD77032F377368F036F0D207C7C93A6EE41F3E0A3E08CE5AE17F1E033F0BFD7A4C4C883D879D320188331C92DC4834E62679F0A33A84604ED0DDFD6B2E9E9B4FFEBE664ABAC2A6ED0AFD11E8536F1593BFA77970B37FE7B77FBD429E7330F96B0075A044FE7E93E9CAFD224B419CA1965F4DB3F2D469A50D7C3B39D7AFDE7197CF0FDE065C1E854DD9799DACE7C1FCEDE0C3E4EF69166CF46F03791BADB501EAB7FDC4A24000C40FFCFB5DB2210B79D0B332F3B974977F87C36DD85FC0DA1B6CB4F3D6D47AB2836BC989DBAF19CEAEAEDBE77F1E57F30B58970FEC5FCFC0E1915F15D90AE288144FF65F52E5A9B7733FBA4E04F3D88E3BEFFEF59ECE29601DBAD169680E985447D6F5969CBCE3BA6FE040FD9666F59EE6ED914F4EA4CE3B9846EB19F04D9444B60200629DB69EFC07A986ACC9831ED51104B3F7889C669D8D669F61743A9A7C77B4509E02F00554A8DFD17CBC97CCAEA8013FF03CA6CD087D36347F0C4CA5D1A447FE48642B8823F8863664790AADB4819EC35B6E030386070FD67C79ECA93BCDB701BE9016F53B9A9D7BD26D60E6ACD0DBC0833D0DE7C27C2AB2150010EB3825258368C44D760861E31F4DDEB10476369AAB16067F04CAE58F338D1E83CECD61ACE7982AF278BD1F8D6E7E17C6CDDF14FA44A84E469F4279BC8B44B68238C268096C2B6D800F30A97AC75DB7D8B80DF0D75A93B38CDB40AF7135E4F17A3FF89C6E03855B437F0D60D4065C92BC42642B0020D6511BEDE8C046ACD7CACA67CA46BAC3E19EF716B0473383DF0171F9EA8099DF56FBF60918BBF90A1BBFF5AACFD16B2FB2B76736B2A70DDEFB6B064E7ED2BCA414F8266951E7DF92867B0378E40291AD208E50CBEED36665A9F301F5AE97AA0FC1CCDDD0721BF85E2EF03D99A2D2D2DB21239FF5FCA68A0D5FA2B6814DFE6DE09D598D86EFFD35B72EA3DBC0E5FC02F6DCE0D05E033C62BC3F46B1C8560040ACA336D8D9010DD84F3EE2A73A82609ECEA13B1CCD31E38DEF80ECB0CB3B0AF9E853B3E727A15D1B7F0F4CA5A3B35A642B8823D4BBD669445936D9D1421B089CFD1FE8F889ADD3065E1D443FFED7EC3332B46B7BB45FF001009F502CB2150010EBF047765443D60C7517B1D477F7931D8DDEEF720BD8537DE8F4EC347B2D9DBEE6886F437BBA616216F47991AD208E5083D662A22C9BE4ABE051F5A1255F78DBB80D9CCDCD67CFF4337E1216AE819FC006FAD9B8109FF019B7812B225B0100B18ED32D6F211A7193FCB137D911B4205FFA97EA6802E58FE68D16060AC7CFC7B4FC085673DC94D01FED5269E9BC75CF3DEC5E91B5204E707AE4F5445936F970889F00BA87996B03BB5745B60D7CFAB5711B9834CDF636C0DA64C83F16590B0088658CB6007EA46F689D5FDF205F00043A6746641E83F61AAAF8DE6F5269EA9D3133F4CECFE893C3FBBB14FE5C642D8813D40140D02D80435D023873B8F11300CD05B322D3067A7CA8F8E6B95069EA9D3D3BF42F1C8C9604EED0B3E82F44D602006219B5C1E6043660BD8FF40DED0940DF91E607005CBE47809D7741EF7CAAF8261A5269053A6B9695CE8F4E57B3BDB4F717226B419CA096DB9EC072D4CB27BE5175A1257B7D125A1BE07B04182D111C8A834728EC5C1E9D56A073E7D8DF061C5D73FF4A642D0020967149DE6D5423D60CF5EE27E3E3D03A3FEEBE35F9ECF901E1BD0FEDD45D6633A729BE457EA83428F96A85D435B4E4A3AA06AB01DEC272C0F18743F26E24CAB2C950DB8034D4FC1300CD9CB5F9EC850174FA66ED9821B36953F3436A0393A7DB3F00B82FB5F827226B0100B18CDA6057063660BDA14E027CF9BDD03B3F2EDF329807F067FBD1E7D1923CF08FFC2ADFB7D910F5BBC11C35D6F649801744B68238421DD405DD0B23D4658053DFC926EB9B91FCF3C0D933F2D9EFFA873618E6817FF8970AABD943FF6E30478FB77712A04392AF8A6C0500C43A4E4999433564CD4EBD425F07E0ACC9C78F94FC93A5DC75F96CF2E47CF6D6278A6F0FF5D4818AEF0901577A57F6FDEF5F8C517C1309F9C081FA1D3386FA0954E7FE86DF40D7886C057184D3AD4C27CAB2492B6DE054C04658A1C8DB409EDA06A64CC9678346D06D60A0DA06F844D7EDCBC36B03033E0DB10D187F0A7B4A642B0020D6714ADEAF8946DCE48399A14F943BB0C578F671B4E59DECEF06D3E7DF922676432B12D90AE208B50D7C4E9465937CF95BAA3E0453DE14FB6D80BF2AE09F2C52E7DF927C4E1095473A8F8A6C0500C43A4E8F9C4934E2263B740FBDF39B37D74B7638B1E4412BCBA0661A0D009465225B411CE192946E7479DED14A1BE0B3EBA97A174B1EDE6EFF52C00EB7778DC8560040ACE3702B0F530D59D3CA46280347853E11B0B5B5F2F9135F1699CAA326DDF24891AD208E70A4290F90E529B4D206FA85F8354C349CAF0ED4A9730FA6E166406EF90B91AD00805827A5EB817F201BB2CE50B7037EFCCD6C56456C3F1A4BF299DAD4B907B35DB7E09D9F43F27615D90AE288765D0A7F4995A726FFF2C34A1BA888F136C0BFD8A1CE3D98ED8DDB8047642B0020F661F7AA0DF76C6043D61BEA2C686EB01D01A36DFEE6D01F7D9AF80240EDFCF27F233215C4197C0D7BAA4C35ADB481603B0246DBA22D16DA80AAE11A00527E8AC85200403CE09494D55463D6B4B21BDA8BEFEC67E7F262F30EE8DDD1A177E63C005079A3F342A761593F12590AE20C3E7F8328D3261FE8117A1B787E7036FB2E3736DBC0075F86DE064C4C82BD8435000088339C1EB91FD1989B4C490F7DEF7C2E5F698FEA7CA2A995BB7F2E0F0054DE68BA2479ADC84E1087184D86B5DA0662F12940E156C5F78A823ADF601AB501D54D223B0100F182335DB99F68CC7E86BA2700F7E9B7F6B3B25DB17307C4F707E86EE1BD277FFC6FF4E853BD83EC2FB213C421AE74E57FD3E57AD7505704E43E35309B95C6501BE09FFEF51E11FA75708D1FFF2B8344760200E207DF3C80DAC006ADD7CA6B002EDF1CC8CCA624ADE1F499A1CF7AE69A78FCCFDA76CBBB4F64268853D472AC0C2C57BD1D2CBC06E0F2801B2B6DC0CAFE175C138FFF59DBB4BC5F8BAC0400C4132E8FF215D5A835F90E60A1CE84D69C3C3DFA8F41F7AF932D3DF6E4B6CF307CF45928B211C4316A1BF88C28DB26F91DB0D536307E6AF4DB40DE46853D61B10DF0B510A83CD1795864230020DE48F178FF2FD1A8FDEC9869EDD12177E982E8CD0738B6233FE415CF344DAC7CC69C1EF92D918D208E71BAF3FF832C5F9D1D7B5A6F034BE647AF0D1CDFA184D1068CEFFE9D6EF95D918D008078446DC885CD1AB64E7E07C43703A13A0923F9DDF79A25ADDF01166FCF67A9EF5AEBF8B826EE7C6EF1B51444168238472D4F25A07CFDF4B58101D6EA136F032B17CB643D8DA447B62BECF7EFD2E76446336DA0AD3BEFBF8B2C0400C4232EB7DC9768DC7E76CC0C7D63144DDE01F27790A16C571A8EFBD6C9ECB9C1D683BF99BB7FCCFE4F2CD436D0932A67BDE1B4012E9F8BD25A6D80BFFA0A75CF0BBDBC0D186C81CDC5EC7F00E29D3619F21FAB8DB921A071FBC93B83CEFDAC0755EE3BA373594376E46646F309577C9F73ABEFFCB97CD19376DDBC641EE87579BCED45F68104A0535AD64FD572AD092C673F3DBC0DD0F5C6AC833ECF61F5116E03FC13C4F0DB80E1DD3F6BEBC97F50641F00209E7148CA7B5423D76B65739440F9FBC8150BBDBE4FF3A80ECCAA791B144BCBFC066AB4E909D72579B7896C030904FF9C8D2A6FBDED33ACCDA6D7CB170A5AA6B601BBBF109037CACC33AC75DA809A57FB44B60100E21DE76B397FAA36EC33810D3DD04E169646A5ECFA618E6F6E40387B9AF3C7A9FC5127DF84884A2354F9130EE3EFFE7D3E24B20D2410F77729FCB95AB6A702CABA9976B5812E1FEC67AB16CBEC7C986D20571DFC0EFC2CBCD7139A9DF9A37F736DE031916D00804480CF6A271A7A80DEB05F05E8E5EFEA3F1B97CBB6AF9459E37EE347A317E43B1DDEA46979ECD521F69D079FE4C8777FA3AFD9CFCD22BB400262B43A26970748FE8E9CAC4716E46DE0D3B1B96C5B086D807FDAC73FB37DCDCE363060BFA9D75F4E49D92AB20B009028B4C9907FEC92E48374A3BF6BBB74EB6B0318C983FAA0CF72D40E318F7D3D31978D999CC73E570708EF7F99CBDC43F7FB5658A38E0B57BED80B75AD015E69F786FC2F22BB4002C2F7755007010544D9FB19C936F08A680323D541C1D713F3D8B793737D6D60C8576A1B189613B136C017FDA2AE55AF4392AFFE46F2FE4F915D008044426DE41D546FEB1B3D255F24C7EA6751B1269FDD4D5D63A00E49192AB20924306DD3F2DAAAE57D2BB0FC034DC636E074CBC345360100121197244F231B7F807C52209F314C7528F1A299094FC2237CA6B8C82290E0A86D602251079A99106DC0C492D7C2E3FC8B219145008044444C862A0E68FCA456B64B8D153BF53279D723C99753D2E4FF23B207240129A9D93F53CBBD28A01E903E60C3D731D13284367085AF1A2AB2070090C8F04D6ED4467F29A01320E59F4645EA7D68A434FDC853D521793D225B4012E14C2BF837B5FC2F04D6074AFE3A20DEDA4008C19FBFFBEF21B20500900C383CF21B546740D9BE9BBD5F07444AFEB8D6E4843FA13247640748425C92F29A5A0F0CE7C470F90CFA786803FC8B9710DBC042911D008064C2E9913FA23B85E6F2CFA3F816BA64A713033EA276CE7CF63675EEA46E39EBC93E257F24B20224296ABD1E42D60F425F1BE815BB6D80AF64686695BFBB7A7761EE0B00498CD3AD4CA23B075A3E2F20D61E87F247FE261738F1E990E47CBE3892C80290E4B8DCCA58AA9EB424BFC37EB47F6C3D0DB8F3DA2B8401B02417FE3AADE0CF451600009291D4D4253F543B83A5019D4350B53BA168CF907EB86F8ED9057EF41E6F9B96F7B7E2F201B8E79E61EC076ABD5810504F82CADB00FFCA24DA6D802F5A14DA5DBFCFD2F652FEDF8BAB07002433770601DEC9444711D49474AF6FB211D53145D247FAEC37B39D2965113A3E40C1DB80D9CF03F5DE6D03ADFB4480077E2B6D802F0686ADAE010001B07B5D927704D5691899927E673BD5CE117D35B0DFF74D73BB8C90EFF87DF24D7EDA4B7B7F212E16009250E6C5E8E503818E3DD53610E68E82C1E4AFDE781B681FFA1DFF1DDD72569B0CF9CFC4A50200803FFC9320BE2428D98118A8DE5DF83E99E2774476CC98E69D29EFF0F89D4E28EFF89BEB9D715F6AF14FC425021014975B4E57EBCD95E6F5C858AD0D74EC9563CB9E023CE8F3C9B77C3D8210DFF107A8CCC1A457008021CE74A58DC3239FA03B12F3A678BCBEE0FDA07A67C43B31DE21F281816F0295785AC0FF7F1EE8F92CFE87FAE4F89E24F04956E6362E31F422FFDC515C1600A6E10BE3A8F5E778407D0A59FE74CCD70632F3D843EAC0F891BE392DB601BE5B1F6F27BC0D3CD02337B4AF5A5AF692D3E395C46501008031FC51A17A37B384E850E242FEAED3E9CEFF0F713900848CD8463BA4C9813166F16F3CDEFF1497030000A1E1F228CFA81D497940C712CB5E7249DE6178DC09ECC2E1567EABD6ABB2807A16CB5E461B0000D802DF24C4E99647AA1DCBB5808E26A654EFFA57A8E7FA4FE2B401B00DDFFE016E79B8D5F931ADA7B2DA9556F0CFE2B40100C01E9CE9397FA37680A3D48EC6D43E02ADA7B2D599AEB8C46902103162B70DC87B1D6EE561719A000010197827E8F2289FA99DCEA9804EA8D5F4DD8979BC8BDAA6E5FD5A9C1600AD46BB2E85BF54EBE1A76A3D6C0CAC9BADE8353E4FC7E596FF9F382D0000681DEE2C9EE2EDCC3F31523BA3D6BA23929D1EB9DF03EEFCFF264E0380A811D0062E06D4D54889360000881DF8BAE20E8FFC3BF58EE85BA75B3E44745A56AD557F731EFF9409EFF7412CC3BF9C71BABDCFA975768CEF2B14933B0D9AB04EBDCB9FCFB7AEC6FB7D0040CCE37B4DC0EF8C3C72A66FB315B7BC450DE4F96A6756AACA5F1DF085566EA89E512D171DE61EF59FD31C6EEF60F5D867533CCABF8A9F0320EEE0AF09DA7AF21FE16DC0373096E4CD016DE0B2EA4D55BF36E0742BD3791BF00D26D20AFE4DFC1C0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000040AB71CF3DFF3F702E596A7CDC25800000000049454E44AE426082, 4)
INSERT [dbo].[tbl_MstMenu] ([MenuID], [UserID], [MenuName], [MenuText], [FormName], [MenuParent], [MenuImage], [Seq]) VALUES (6, 1, N'frmAddMenu', N'11. เพิ่มเมนู', N'frmAddMenu', NULL, 0x89504E470D0A1A0A0000000D4948445200000200000002000806000000F478D4FA000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000DD600000DD601906F799C0000001974455874536F667477617265007777772E696E6B73636170652E6F72679BEE3C1A0000339E49444154785EEDDD7DAC657579F6F1DB619CF032E2B412A191AA1382C6A45183C1984A28188C264DA85189150B62C21F94D488A9C1A4354614475B7C48D326885AA233C0CC30CC0CC3BC710E55B4B6C517D4F23CFEB414B5422CA051A054071098D9CF7D9F7D38ACBDF675CED92F6BEFB5D66F7FFFF890999B99F3B2F7BA7FD775E69CBDB7753A1D004D97EC1877B27B8D7B933BCF5DE2FEDA5DED36BB9D6E9F9B775F73DF70DF75C9FDC8DDEF7EEE1E7587DC338BE2D7318BFF177F26FE6CFC9DF8BBF136E26DC5DB8CB71DEF23DE57BCCF78DFF131C4C7121F537C6CF1311E233F07008D228700A624D93A778A3BC75DEC3EE96E745F7177BB9FB9C75DA765E2638E8F3D3E87F85CE2738ACF2D3EC7F85CE3735E276F1300532187002A926C8D7BB93BCBBDCF5DE1B6B8AFBB08C8C34E05E82C88CF3D6E83B82DE23689DB266EA3B8ADE2365B236F530095904300234876828B7F0AFF80BBCEDDE5DAF8D57B53C46D17B761DC96719BC66D7B82BCED010C4D0E01ACA0FBCFF6F1FDEE0BDC556ECE3DE85488A17A715BC76D1EB77DDC07715FF0ED0460487208A0A0FBFDEA0BDDE7DDF7DDD34E0513EA13F749DC37711FC57D758ABC2F012C91436066255BEB4E7797B9F889F7879C0A1C345FDC77711FC67D19F7E95A799F03334A0E819991EC78F716F77177878B87C4A93041FBC57D1BF771DCD7719F1F2FAF096046C82190ADEE4FE5BFC1C54F9C7FCBCDF24FE1CFBAB8EFE31A886B21AE091E75809922874056929DE4DEEBB6B9879D0A0320AE8DB846E25A39495E4B4046E41068B5EEF7F1CF749BDCF7DC11A70E7C603971CDC4B513D7505C4BFCFC00B2238740EB247BA18BAFDC76B9C79C3AD48151C53515D7565C632F94D720D0327208B442B217B8F7B8BDEEB74E1DDC40D5E25A8B6B2EAEBD17C86B13680139041A2BD971EE5D6EB77BC2A9031A9896B806E35A8C6BF23879CD020D258740A3745F09EF1D6E87E3617A68AAB836E31A8D6B95574444E3C92150BBEEC3F5DEEAB6BA5F3B75E0024D15D76C5CBB710DF3F04234921C02B5E9BE9EFC475DBC2EBD3A5881B6896B39AEE993E5350FD4440E81A94A76943BD7ED73CF387588026D17D7765CE371AD1F25770198223904A622D94677A57BC0A90313C8555CF371ED6F94BB014C811C021393ECF9EE9DEE76C713F460D6C50EC42EC44E3C5FEE0C30217208542ED9092EBE0FFA0BA70E4260D6C56EC48E9C207708A8981C02954976AAFBAC7BDCA9430F40AFD895D89953E54E01159143606CC9CE70B7385E6D0F184DEC4EECD01972C78031C9213092EE4FF3C7F732BFE9D481066034B153B15B3C7A009591436028DDA7E77DBFFB89538717806AFC978B5DE36987313639040692EC78173FB4F48853871580C9889D8BDD3B5EEE263000390456D4FD8AFFC3EE61A70E2700D3113B18BBC8BF086068720848C98E761F743C940F6896D8C9D8CDA3E5EE02821C023D92AD73973A9EB10F68B6D8D1D8D57572978102390416245BEB2E76BC300FD02EB1B3B1BB6BE56E034E0E31E3BA2FC57B81FBB153870B8076881D8E5DE62589D1470E31C3929DE5EE76EA3001D04EB1D367C99DC7CC9243CCA0EE2BF3ED5A3C2C00E429769C5720C40239C40C49B6DE6D724F3A756000C84BEC7AECFC7A79266066C8216640B2E7B98BDC434E1D1200F216BB1F67C0F3E41981ECC9213297EC8DEE2EA70E0500B325CE8237CAB302599343642AD94BDDB6C5A50780A2381B5E2ACF0E64490E9199EEABF47DC81D726AF10120C419116705AF3A3803E4101949F65AF71DA7961D009438335E2BCF1464430E9181EEF3F67FDA3DEDD48203C04AE2EC883384D717C8941CA2E5BA4FE673AF534B0D00C388B3842711CA901CA2A5926D705F70479C5A640018459C2971B66C90670F5A490ED142C9DEEE1E746A7901A00A71C6BC5D9E41681D39448B243BC9ED5E5C4E00988638734E9267125A430ED112C9CE75BF5C5C480098A6387BCE9567135A410ED170C98E75D72E2E2100D429CEA263E559854693433458B2D3DC3D8B8B07004D1067D269F2CC4263C9211A28D91A77B97BCAA90504803AC5D91467D41A7986A171E4100D93EC647787534B07004D1267D5C9F22C43A3C8211A24D979EE91C5C50280368833EB3C79A6A131E4100D906CBDFBE2E23201401BC519B65E9E71A89D1CA266C95EE97EB8B84000D0667196BD529E75A8951CA246C9DEE61E5B5C1C00C8419C696F93671E6A2387A841F7A7FC37399EC71F408EE26C8B338E470934841C62CA92BDC8CD3BB53400909338EB5E24CF424C951C628ABA4FEC73DFE26200C02C88338F270EAA991C624A925DE49E585C0800982571F65D24CF464C851C62C292AD73D72C2E0100CCB2380BD7C9B31213258798A06427BA3B172F7C0040F74C3C519E99981839C484247B95FBE9E2050F00784E9C8DAF92672726420E3101C9CE768F2E5EE800807E71469E2DCF50544E0E51B164173A5EC50F00561767E585F22C45A5E410154AF6B1C2850D0018CCC7E4998ACAC8212AD0FD49FF2D858B1900309C38437984C084C821C6946C83FBEAE2050C00185D9CA51BE4598BB1C821C6906CA3E395FC00A03A71A66E94672E4626871851B2D7B95F2C5EB00080EAC4D9FA3A79F66224728811243BC3F132BE00303971C69E21CF600C4D0E31A4646F7687162F5000C0E4C459FB667916632872882124FB13F7E4E285090098BC3873FF449EC918981C6240C9DEED9E5EBC200100D31367EFBBE5D98C81C8210690EC627778F14204004C5F9CC117CB331AAB9243AC22D965EEC8E2050800A84F9CC597C9B31A2B9243AC20D9470A171E00A0193E22CF6C2C4B0EB18C647F53BAE00000CDF137F2EC86248710087F0068034AC080E41025FCB33F00B409DF0E18801CA2A0FB037FEA02030034173F18B80A39C4A2EE43FDF8697F00689F38BB7988E00AE410AEFB243F3CCE1F00DA2BCE709E2C68197238F3BA4FEFCB33FC0140FBC559CED3060B7238D3BA2FECC373FB03403EE24CE705844AE47066755FD29757F50380FCC4D9CE4B0917C8E14C4AF63AC7EBF90340BEE28C7F9DCC801924873327D946F78BC50B040090AF38EB37CA2C9831723853926D703F5CBC300000F98B337F83CC841922873323D93AF7D5C50B0200303BE2EC5F27B36146C8E1CC48B6A57031000066CB16990D33420E6742B28F952E0400C0ECF998CC88192087D94B7661E9020000CCAE0B6556644E0EB396EC6CF754E18E0700CCB6C884B36566644C0EB395EC55EED1C53B1C0080674536BC4A6647A6E4304BC94E743F5DBCA30100288B8C3851664886E4303BDD87FBDDB978070300B09CC88A997878A01C6627D935853B170080955C23B32433729895641795EE580000567391CC948CC86136929DE69E28DCA100000C22B2E334992D9990C32C247B91BB6FF18E0400605891212F92199301396CBD646BDCFCE21D0800C0A8224BD6C8AC6939396CBD649B0A771E0000E3D824B3A6E5E4B0D592BDCD1D29DC7100008C2332E56D32735A4C0E5B2BD92BDD638B7718000055896C79A5CC9E9692C3564AB6DEFD70F18E0200A06A9131EB6506B5901CB652B22F16EE24000026E18B32835A480E5B27D979A53B08008049394F6651CBC861AB243BD93D52B863000098A4C89C936526B5881CB646F7F1FE772CDE2100004C4B644FAB9F1F400E5B23D9E5853B03008069BA5C66534BC8612B749FE7FFA9C21D0100C0344506B5F6F502E4B0F1921DEBEE59BC030000A84B64D1B132AB1A4E0E1B2FD9B5851B1F00803A5D2BB3AAE1E4B0D1929D5BBAE10100A8DBB932B31A4C0E1B2BD949EE97851B1C008026886C3A49665743C9616325DB5DB8B101006892DD32BB1A4A0E1B29D9DB4B373400004DF37699610D24878D936C837BB070030300D04491551B6496358C1C364EB22F146E5C00009AEC0B32CB1A460E1B25D959EE48E1860500A0C922B3CE9299D62072D818C98E76F72EDEA00000B44564D7D132DB1A420E1B23D9A70B372600006DF269996D0D21878D90ECB5EEE9C20D0900409B4486BD56665C03C861ED921DE5BEB378030200D056916547C9ACAB991CD62ED9870A371E00006DF62199753593C35A257BA93B54B8E1000068B3C8B497CACCAB911CD62AD9B6C28D0600400EB6C9CCAB911CD626D91B4B37180000B978A3CCBE9AC8612D923DCFDD55B8A10000C84964DCF36406D6400E6B91ECA2C28D0400408E2E92195803399CBA64EBDD43851B0800801C45D6AD97593865723875C936156E1C000072B64966E194C9E15425DBE89E2CDC300000E42C326FA3CCC42992C3A94AB6AB70A30000300B76C94C9C22399C9AEE4BFDAA1B060080DCD5FA92C1723815C9D6B8BB0B37040000B32432708DCCC82990C3A9487641E146000060165D2033720AE470E292AD753F2EDC000000CCA2C8C2B5322B274C0E272ED9C5854F1E8BAEFBCCEBF3F4B7AFEF3C7CFBB1F2731ED63DB7BDB873DD55FE36013497EFFC759F769BDC95EE13EE0AF7B1D7773A77E9DD9E7117CBAC9C30399CA864EBDCFD854F1C8BCE3FFFFC6CDDF70FBFD3E9FC9BFEBC87F1E52DA7CAB70FA01D3AD7FB2E5302CA2213D7C9CC9C20399CA86497163E6914A865C9C57D7FEF0560B37F9EDFE8FFBC87410100DAADF345DF654A8072A9CCCC0992C3894976B47BA0F009A3402D4B2E160A402C7E94806FF67EDEC3A00000EDB6700E500294C8C6A365764E881C4E4CB20F163E5994A865C9C55201085BDCB79EFBBC87410100DA6DE91CA004281F94D93921723811C98E73BF287CA22851CB928B9E02F0ECE27FBBFB790F830200B45BCF39F0EC594009785664E471324327400E2722D9870B9F2404B52CB9E82B00232E3E050068B7BE7360C4B320631F96193A017258B964C7BB870B9F2004B52CB9900520DCE0BEE3C4EDA15000807693E740A0043C2BB2F27899A51593C3CA25FB68E193C332D4B2E462D902106E74DF75E23629A30000ED26CF806751029EF55199A51593C34A75BFF7FF48E113C332D4B2E462C50210A2047CCF89DBA5880200B49BDCFF224A4088CC9CF8CF02C861A592BDBFF04961056A5972B16A01085BDDBF3B71DB3C8B0200B49BDCFD324A4078BFCCD40AC96165921DE5FEABF00961056A597231500108DBDC0A25800200B49BDC7B851210D97994CCD68AC8616592BDB3F0C960156A597231700108DBDDDD4EDC461400A0DDE4CE2F8712F04E99AD1591C3CA24FB66E993C10AD4B2E462A802106E72FFD7956E230A00D06E72DF5732DB25E09B325B2B228795487646E913C12AD4B2E462E8021076B85209A00000ED26777D35B35D02CE90195B0139AC44B25B4A9F0456A196251723158070B3FB7F6EF136A20000ED26F77C10B35B026E91195B01391C5BB253DDE1C2278001A865C9C5C80520EC728B25800200B49BDCF141CD6609882C3D5566ED98E4706CC93E5BF8E03120B52CB918AB0084DDEEFB1400A0EDE47E0F63364BC06765D68E490EC792EC04F778E103C780D4B2E462EC02106EA100006D27777B58B3570222534F90993B06391C0B4FFB3B32B52CB9A8A400B82F5F450100DA4CEDF54866AF0454FEF4C07238B264CF77BCE4EF88D4B2E4A2B202F0090A00D0666AAF47365B2520B2F5F9327B47248723E3897FC6A296251714000041EDF55866AB0454FAC4407238B264B7973E580C412D4B2E28000082DAEBB1CD4E09B85D66EF88E47024C936BA23850F144352CB920B0A0080A0F6BA12B35102226337CA0C1E811C8E24D995850F122350CB920B0A0080A0F6BA32B35102AE94193C02391C5AF755FF1E287C8018815A965C50000004B5D795CABF0444D656F22A817238B464E7163E388C482D4B2E28000082DAEBCAE55F02CE95593C24391C5AB27DA50F0E2350CB920B0A0080A0F67A22F22E01FB64160F490E8792EC64F74CE103C388CE39E79C6CFDCB952FD14B3AA4ABFEFC34F9F601B483DAEB89C9B70444E69E2C3379087238149EF9AF326A597241010010D45E4F54BE2560EC670694C381255BE3EE2F7C4018835A965C50000004B5D71397670988EC5D23B379407238B0646F2D7C3018935A965C50000004B5D75391670978ABCCE601C9E1C0926D2D7D3018835A965C50000004B5D753935F09D82AB3794072389064C7B85F173E108C492D4B2E28000082DAEBA9CAAB0444061F23337A0072389064EF287C10A8805A965C50000004B5D7539757097887CCE801C8E14092ED287D1018935A965C50000004B5D7B5C8A704EC90193D00395C55B2E3DCA1C207800AA865C905050040507B5D9B3C4A4064F17132AB572187AB4AF6AEC23B4745D4B2E482020020A8BDAE551E25E05D32AB572187AB4AB6BBF4CE5101B52CB9A00000086AAF6BD7FE12B05B66F52AE47045C95EE09E28BC6354442D4B2E28000082DAEB46687709884C7E81CCEC15C8E18A92BDA7F04E5121B52CB9A00000086AAF1BA3DD25E03D32B35720872B4AB6B7F44E5111B52CB9A00000086AAF1BA5BD2560AFCCEC15C8E1B292BDD0FDB6F00E5121B52CB9A00000086AAF1BA79D2520B2F98532BB972187CB4AF6DEC23B43C5D4B2E482020020A8BD6EA4769680F7CAEC5E861C2E2BD9AED23B4385D4B2E482020020A8BD6EACF695805D32BB97218752B2B5EEB1C23B42C5D4B2E482020020A8BD6EB4769580C8E8B532C3053994929D5978279800B52CB9A00000086AAF1BAF5D25E04C99E1821C4AC93695DE092AA696251714000041ED752BB4A7046C92192EC8A194EC7BA577828AA965C905050040507BDD1AED2801DF93192EC8619F6427B9238577800950CB920B0A0080A0F6BA559A5F0222AB4F92595E22877D78F8DF54A865C905050040507BDD3ACD2F01033D1C500EFB24DB567AE39800B52CB9A00000086AAF5BA9D925609BCCF21239EC916C8D7BB8F08631216A597241010010D45EB756734B4064F61A99E90572D823D91B0A6F1413A496251714000041ED75AB35B704BC41667A811CF6487645E98D6242D4B2E482020020A8BD6EBD6696802B64A617C8618F64DF2ABD514CC825EFF9A3CE25EFCED3BD576FD08B33A4031FD9D8B9E44FCF04D0526AAFB3D0BC12F02D99E90572B824D9F1EE70E10D6292E2E2898B485D5C0080666B560988EC3E5E66FB22395C92EC2D85378669888BE706A72E2E0040B335AB04BC4566FB22395C92ECE3A5378669A00400407B35A7047C5C66FB22395C92EC8ED21BC3B4500200A0BD9A5102EE90D9BE480E17745FFEF750E10D61DA280100D05EF59780C8F0655F1E580E17243BBDF0465097EF384A0000B453FD25E07499F14E0E1724BBACF44650174A0000B457BD25E03299F14E0E1724DB597A23A813250000DAABBE12B05366BC93C305C91E2ABD11D48D120000ED554F09784866BC9343FF0BA794DE009A82120000ED554F093845657DDF6041B20B4B7F194D1225E046A72E2E0040B34DBF045CA8B2BE6FB020D9E74B7F194DF35D47090080769A6E09F8BCCAFABEC18264DF2FFD653411250000DA6B7A25E0FB2AEBFB06FE07D7B9A70B7F114D46090080F69A4E09884C5F57CEFB9EDF2C48F69AC25F421B500200A0BDA653025E53CEFB9EDF2C487641E92FA10D280100D05E932F011794F3BEE7370B925D55FA4B680B4A0000B4D7644BC055E5BCEFF9CD826473A5BF8436A10400407B4DAE04CC95F3BEE7370B923D58FA4B689BEFB9AD4E5D5C0080669B4C0978B09CF73DBFF13F7042E92FA0AD280100D05E9329012714337FE9170B92BDA9F487D16694000068AFEA4BC09B8A99BFF48B05C93E50FAC3683B4A0000B457B525E003C5CC5FFAC58264D795FE307240090080F6AAAE045C57CCFCA55F2C487657E90F231794000068AF6A4AC05DC5CC5FFA85FF8F35EEF1C21F446E280100D05EE39780C8F835AA00BCBCF08790AB2801DB9CBAB80000CD367E0978B92A006795FE1072F5EF8E120000ED345E09384B1580F795FE107246090080F61ABD04BC4F15802B4A7F08B9A30400407B8D5602AE5005604BE90F6116500200A0BD862F015B5401F87AE90F6156500200A0BD862B015F5705E067A53F8459122560BB53171700A0D9062F013FEB2D00C9D6B9C3853F805944090080F61AAC0444D6AF2B1680530AFF13B38C120000ED35580938A55800CE29FD4FCC324A0000B4D7EA25E09C6201B8B8F43F31EBEE7694000068A7954BC0C5C502F0C9D2FF440DAEBBEAF5CDF2297779351EFEECB1FA221DD23D57BF58BE7D00EDA0F61A13B27C09F864B100DC58FA9FA8C1F9E79F9FADFBFEFE77F4053AA42F7FE254F9F601B483DA6B4C902E0137160BC0574AFF133550CB920B0A0080A0F61A13D65F02BE522C007717FE076AA296251714000041ED35A6A0B704DC5D2C003C095003A865C905050040507B8D2979AE042C3C1990FF6AA1003C5E0C22D4432D4B2E28000082DA6B4C5194806FDB93DD0290EC987210A11E6A597241010010D45E63CAA204ECB313A3009C5C0E22D4432D4B2E28000082DA6BD4E080FD611480D7948308F550CB920B0A0080A0F61A3598B377460178533988500FB52CB9A00000086AAF518339FB8B2800E7958308F550CB920B0A0080A0F61A3598B78F4701B8A41C44A8875A965C50000004B5D7A8C1BC5D1305E0AFCB41847AA865C905050040507B8D1ACCD94D5100AE2E0711EAA196251714000041ED356A3067B74701D85C0E22D4432D4B2E28000082DA6BD4E036FB7614809DE520423DD4B2E482020020A8BD460D0EDAF7A300EC2B0711EAA196251714000041ED356AB0DFEE8D02305F0E22D4432D4B2E28000082DA6BD460AFDD1705E06BE520423DD4B2E482020020A8BD460D6EB507A3007CA31C44A8875A965C50000004B5D7A8C12DF6CB2800DF2D0711EAA196251714000041ED356AB0CB1E8D0290CA41847AA865C905050040507B8D1AECB45F4701F8513988500FB52CB9A00000086AAF51831DF6441480FBCB41847AA865C905050040507B8D1A6CB7DF4601F8793988500FB52CB9A00000086AAF5183ADF64C148047CB41847AA865C905050040507B8D1ADC6087A3001C2A0711EAA196251714000041ED356AB0C58E440178A61C44A8875A965C50000004B5D7A8C197AC4301689073CE39275BFF72E54BF44538A4ABFEFC34F9F601B483DA6BD460B100F02D808650CB920B0A0080A0F61A35D8DC2D00FC106043A865C905050040507B8D1A2CFE0C000F036C08B52CB9A00000086AAF5183EBBB8F02E089801A422D4B2E28000082DA6BD4E0467B3A0A004F05DC106A597241010010D45EA3065BEDA92800BC185043A865C905050040507B8D1A6CB327A300F072C00DA196251714000041ED356AB0DD0E4501F8463988500FB52CB9A00000086AAF51839BBA2F07FCB57210A11E6A597241010010D45EA3063BECD12800F3E520423DD4B2E482020020A8BD460D76D8AFA200EC2B0711EAA196251714000041ED356A70B3FD3C0AC0CE7210A11E6A597241010010D45EA3063BEDBFA3006C2E0711EAA196251714000041ED356AB0CB7E1C05E0EA7210A11E6A597241010010D45EA306B7D877A300FC753988500FB52CB9A00000086AAF51835BED9FA2005C520E22D4432D4B2E28000082DA6BD4609F6D8D02705E3988500FB52CB9A00000086AAF5183FD7675148037958308F550CB920B0A0080A0F61A3538687F1905E035E520423DD4B2E482020020A8BD460D6EB3F3A2009C5C0E22D4432D4B2E28000082DA6BD460CE4E8F02704C3988500FB52CB9A00000086AAF518379FB5DFF6F274AC0E3E530C2F4A965C905050040507B8D29DBEC3CFBE3BF51007E560E234C9F5A965C50000004B5D798B21BEC996201B8BB1C46983EB52CB9A00000086AAF3165DBECF16201F84A398C307D6A597241010010D45E63CA76D823C5027063398C307D6A597241010010D45E63CA76D94F8B05E093E530C2F45D72FE1F752E79779EEEFD3F1BF48538A4031FD9D8B9E44FCF04D0526AAF31657BECDF8A05E0E27218A10677B9EB9DBAC30000A8C23EDB5C2C00E7F48511EA1125E006A7EE340000C675C02E2F168053FA8208F5A10400002665CECE2E168075EE704F08A15E940000C024CCDBFAE70A40B704F064404D430900005469F14980CA05E0EB7D0184FA7DC75102000055B8C9FE4715802D7DE18366A0040000AAB0CB7EA20AC0157DC183E6A0040000C675ABFDB32A00EFEB0B1D340B250000308E7D769D2A0067F5050E9A8712000018D541BB4C158097F7850D9A294AC08D4EDDB900002C67CECE5005608D7BBC2768D05CDF75940000C0A036BB795BDB5F00BA25E0AEBEA041735102000083DA6EBF2966FED22F1624BBAE2F64D06C940000C02076DB7F16337FE9170B927DA02F60D07C940000C06AF6DAEE62E62FFD6241B237F5850BDA8112000058C941FBCB62E62FFD6241B213FA8205ED410900002C67CE5E51CCFCA55F2C49F6605FB0A03D28010080B21BEC7039EF7B7EB320D95C5FA8A05DBEE7B63A7511000066CFCDF6CB72DEF7FC6641B2ABFA0205ED430900003C6B8F7DBB9CF73DBF5990EC82BE30413B5102000061BF5D5BCEFB9EDF2C48F69ABE20417B51020000B7D979E5BCEFF9CD8264EBDCD33D218276A30400C0ECFA929BB7F5E5BCEFF9CD9264DFEF0B11B41B25000066D3367B42657DDF6041B2CFF70508DA8F120000B367B7FD87CAFABEC1826417F68507F21025609B53170900203FFBED732AEBFB060B929DD2171CC8C7BF3B4A0000CC86393B5B657DDF6049B287FA8203F9A0040040FEC433003E4B0E1724DBD9171AC80B250000F2B6D3FE5B66BC93C305C92EEB0B0CE487120000F9DA6BB7C88C7772B820D9E97D61813C510200204F07EDBD32E39D1C2E48B6D61DEA090AE42B4AC076A72E200040FB6C76F376B4CC7827874B92DDD11714C817250000F2B1C31E91D9BE480E9724FB785F48206F940000C8C31EFBBACCF64572B824D95BFA0202F9A3040040FB1DB0BF92D9BE480E97243BDE1DEE0907CC86BB1D250000DA6BDE5E22B37D911CF648F6ADBE70C0445CF799D737CBA7DCE5D578F8DA63F5053AA47BAE7EB17CFB00DA41ED352660BBFD46667A811CF648764539A83019E79F7F7EB6EEFBFBDFD117E990BEFC8953E5DB07D00E6AAF3101B7DA3FCB4C2F90C31EC9DE500E2A4C865A965C50000004B5D798808376B1CCF40239EC916C8D7BB81854980CB52CB9A00000086AAF51B12D76A4336F6B65A617C8619F64DBCA6185EAA965C905050040507B8D8AEDB2FB659697C8619F64EF2D8715AAA796251714000041ED352AB6CFBE20B3BC440EFB243BC91D298615AAA796251714000041ED352A3667AF96595E228752B2EF95030BD552CB920B0A0080A0F61A15DA6E8FCB0C17E4504AB6A91C58A8965A965C50000004B5D7A8D01EBB5366B8208752B233CB81856AA965C905050040507B8D0A1DB4F7CB0C17E450EABE3CF063C5C042B5D4B2E482020020A8BD4645BA0FFF5BF6E57FCBE47059C97695430BD551CB920B0A0080A0F61A15D9690FC8EC5E861C2E8B87034E945A965C50000004B5D7A8C8800FFF7B961C2E2BD90BDD6F8BA185EAA865C905050040507B8D0A7CC9CDDBEFCBEC5E861CAE28D9DE7270A11A6A597241010010D45EA30237DBCF6566AF400E5794EC3DE5E04235D4B2E482020020A8BD4605F6DB3532B35720872B4AF602F74431B8500DB52CB9A00000086AAF31A6CD6EDE7E4F66F60AE47055C97697C30BE353CB920B0A0080A0F61A631AF2A7FF9F2587AB4AF6AE7278617C6A597241010010D45E634CFBEDEF6456AF420E5795EC3877A8185E189F5A965C50000004B5D71843F79FFF5F2CB37A15723890643BCA0186F1A865C905050040507B8D31ECB49FC98C1E801C0E24D93BCA0186F1A865C905050040507B8D31ECB7AB64460F400E0792EC18F7EB6280613C6A597241010010D45E63449B179EFBFF7765460F400E07966C6B39C4303AB52CB9A00000086AAF31A25D769FCCE601C9E1C092BDB51C62189D5A965C50000004B5D718D101FB88CCE601C9E1C092AD71F717430CA353CB920B0A0080A0F61A23B8D19EEECCDB5A99CD0392C3A124FB6839C8301AB52CB9A00000086AAF31825BED0E99C94390C3A1243BD93D530C328C462D4B2E28000082DA6B0C295EF96FCE4E97993C04391C5AB27DE530C3F0CE39E79C6CFDCB952FD117F290AEFAF3D3E4DB07D00E6AAF31A4115EF94F91C3A1253BB71C66189E5A965C50000004B5D718D201BB5266F190E47068C98E720F14C30CC353CB920B0A0080A0F61A43B8C19EE9CCDB3A99C54392C391246F2422D43038B52CB9A00000086AAF31843DF6AF32834720872349B6D11D29061A86A396251714000041ED3586306767CA0C1E811C8E2CD9EDE550C3E0D4B2E482020020A8BDC68076D8AF64F68E480E4796EC9DE550C3E0D4B2E482020020A8BDC680F6DB6764F68E480E4796ECF9EE17C550C3E0D4B2E482020020A8BDC600AEB7C39D793B5666EF88E4702C3C33E0C8D4B2E482020020A8BDC6002A78E6BF32391C4BB213DCE3C560C360D4B2E482020020A8BDC62A36BB397B85CCDC31C8E1D8927DB61C6E589D5A965C50000004B5D758C56EFB81CCDA31C9E1D8929DEA0E17C30DAB53CB920B0A0080A0F61AABB8CDDE2CB3764C72588964B794030E2B53CB920B0A0080A0F61A2BD8690FCA8CAD801C5622D919E580C3CAD4B2E482020020A8BDC60A0EDAA532632B20879549F6CD72C861796A597241010010D45E6319DBEDD7325B2B228795E1898186A296251714000041ED359651F113FF94C96165BAAF12F85FC590C3F2D4B2E482020020A8BD8670A33D5DD5ABFE2D470E2B95ECFDE5A083A696251714000041ED3584BD76B3CCD40AC961A5921DE71E29061D34B52CB9A00000086AAF51B2C58EF857FF2F96995A2139AC1C4F0F3C10B52CB9A00000086AAF513281A7FD55E4B072C98E770F17C30EFDD4B2E482020020A8BD4641F7AB7F3F304596564C0E2722D987CB81875E6A597241010010D45EA3E0563B28337402E47022BA3F0BC04B05AF402D4B2E28000082DA6B2CEABEE4EFC4BFF7FF2C399C98641F2C871E9EA396251714000041ED3516EDB53D323B27440E2726D9D1EE8162E8E1396A597241010010D45EC3DD60CFF857FF1B64764E881C4E54B24BCBC1872EB52CB9A00000086AAFE1F6D976999913248713956C9DBBBF187CE852CB920B0A0080A0F67AE6759FF56FBDCCCC0992C3894B767139FC609D4BDEF3479D4BDE9DA77BAFDEA02FFC211DF8C8C6CE257F7A268096527B3DF3F6D966999513268713976CADFB7131FCE0EE72D73B75810000F273A33DE55FFD1F2DB372C2E4702A925DD01780A00400C02CD96FD7CA8C9C02399C8A646BDCDD3DE1872E4A0000E46F9B3DEE5FFDAF95193905723835C9CEEA0B3F74510200206F07ED32998D53228753956C575FF8A18B12000079DA690FC84C9C22399CAA641BDD933DC187E7500200202F5F727376A6CCC42992C3A94BB6A92FF8F01C4A0000E4638FDD29B370CAE470EA92AD770FF5841E7A510200A0FD6E5878C19F1365164E991CD622D9457DA1875E94000068B77DF68F32036B2087B548F63C77574FE0A11F250000DA69BBFDC6BFFA5F2333B00672589B646FEC0B3CF4A3040040FB1CB44B64F6D5440E6B956C5B5FE0A11F250000DA6397DD2F33AF467258AB642F75877AC20E1A2500009A6FB39BB337C8CCAB911CD62ED987FAC20E1A2500009A6DAFED9359573339AC5DB2A3DC777A820ECBA3040040336DB7439D795B27B3AE6672D808C95EEB9EEE093A2C8F120000CD12CFF8779BBD4B665C03C8616324FB745FD06179940000688E3DF60D996D0D21878D91EC68776F4FC86165940000A8DF567BAA336F1B64B635841C364AF725838FF4841C56460900807AD5FC52BF8390C3C649F685BE90C3CA280100508FDD768FCCB28691C3C649B6C13DD81370581D250000A6ABFB623F2F9359D63072D848C9DEDE1770581D250000A667BFFDADCCB00692C3C64AB6BB2FE0B03A4A00004CDE4E7B40665743C96163253BC9FDB227DC30184A00004CCEF576B83367AF96D9D55072D868C9CEED0B370C861200009371C0AE9499D56072D878C9AEED0B370C86120000D5DA6D3F9059D57072D878C98E75F7F4041B06470900806A6CB5DF76E6ED0499550D2787AD90EC34F7544FB06170940000184FF7B9FECF9719D50272D81AC92EEF0B360C8E120000A3DB6B076436B5841CB646B235EE8E9E50C37028010030BC1DF64867DED6CA6C6A09396C956427BB477A420DC3A10400C0E0B6D891CE9C9D2E33A945E4B075929DD7176A180E25000006B3DFAE9659D43272D84AC9BED8176A180E25000056B6CB7E2433A885E4B09592AD773FEC09340C8F120000DA567BB2336F27CA0C6A21396CAD64AF748FF5041A86470900805EF17DFFDBECAD327B5A4A0E5B2DD9DBDC919E40C3F0280100F09C03F62999392D2687AD976C535FA06178940000E874F6D89D326B5A4E0E5BAFFBFC00F33D6186D1500200CCB21DF6ABB63FDE7F39729885642F72F7F5841946430900308B6EB4A73B7376AACC980CC86136BAAF17F0444F986134940000B364B36BF1F3FC0F420EB392ECA2BE30C36828010066C53EFB4799291991C3EC24BBA62FCC301A4A0080DCEDB624B3243372989D64EBDC9D3D4186D1510200E4EA26FBDFCEBCAD9759921939CC52B213DD4F7B820CA3A30400C84DF787FEFE40664886E4305BC95EE51EED09328C8E12002017DD67FAFB63991D9992C3AC253BDB3DD51364181D250040DB7DC91DB40FCACCC8981C662FD9857D4186D1510200B4D97EFB9CCC8ACCC9E14C48F6B1BE20C3E8280100DAE856FB9ACC881920873323D996BE20C3E8280100DA6497FD4466C38C90C399D17D78E0577B420CE3A1040068831DF6E8AC3CDC6F39723853926D703FEC09318C871200A0C9B6DA931EFE2F93993043E470E624DBE87ED11362180F250040135D6F873B7376A6CC821923873329D9EBDC633D2186F15002003449F7B1FE7F26336006C9E1CC4A76863BD41362180F25004013C4ABFB1DB44BE5D93FA3E470A6257BB37BB227C4301E4A00803AC513FD1CB00FCB337F86C9E1CC4BF627EEE99E10C378280100EAD00DFF4DF2AC9F71720897ECDDEE704F88613C940000D3B6DFFE419EF1A000AC28D9C5EE484F88613C940000D3B2CF36CBB31D0BE41005C92EEB0B318C87120060D2F6DA2DF24CC712394449B28FF48518C64309003029B7DA97E5598E1E720821D9DFF48518C643090050B53DF64D7986A38F1C62199480EA5102005485F01F8A1C62057C3BA07A940000E3E29FFD8726875845F707037974409528010046C50FFC8D440E3180EE4304799E802A5102000C8B87FA8D4C0E31A0EE9305F18C8155A2040018443CC31F4FF2331639C410BA4F1BCC6B075489120060253CBD6F25E41043EABE8010AF2258254A0000255ED58F17F6A9841C6204DD97127EAC27C4301E4A0080A2783D7F5ED2B732728811257B9DFB454F88613C940000E17A3BDCB9CDFE4C9EBD18891C620CC936BA1FF68418C643090066DB567BB2336767CA33172393438C29D906F7D59E10C3782801C06CDA618F76E6ED65F2ACC558E4101548B6CE6DE909318C871200CC965DF6130FFFF5F28CC5D8E410154AF6B1BE20C3E82801C06CB8D5BE26CF5454460E51B16417BAA77A820CA3A30400F9EA3EC1CFE7E4598A4AC9212620D9D9EED19E20C3E82801407EBA0FF3FBA03C43513939C484247B95FB694F9061749400201F37DAD39DDBEC8FE5D9898990434C50B213DD9D3D4186D1510280F6BBC9FEB733677F20CF4C4C8C1C62C2BA8F10B8A627C8303A4A00D05EBB2DF193FEF590434C49B28BDC133D6186D15002807689E7F4DF67FF28CF464C851C628A929DE6EEEB09338C861200B443F7FBFDE7CB331153238798B2642F72F33D6186D150028066DB61BFEACCD9A9F22CC454C9216A906C8DDBE48E2C8519464309009A698FDDD999B7B5F20CC4D4C9216A94EC6D8E97151E172500688E787CFF01FB943CF3501B3944CD92BDD2F18A82E3A20400F58B57F2BBCDDE2ACF3AD44A0ED100C9D6BB2FF6041A86470900EAB3CB7ED499B713E51987DAC9211A24D979EE919E50C3702801C074C53FF9EFB7ABE59986C69043344CB293DD1D3DA186E1500280E9D8E15FB0CCD9E9F22C43A3C8211AA8FB2881CB1DAF2A382A4A003039F12A7E7BED003FE5DF1E728806EB3E71D03D3DC186C1510280EA6DB5DFF2C43EED238768B864C7BA6B7B820D83A30400D5D96D3FF0AFFA4F9067151A4D0ED112C9CE75BFEC09370C8612008CE77A3BDC396057CAB309AD208768916427B9DD3DE186C1500280D1ECB4073A73F66A7926A135E4102D94ECEDEEC19E80C3EA2801C0E06EF0AFFAF7DBDFCA3308AD238768A9641BDC171CAF27300C4A00B0BADD764F67DE5E26CF1EB4921CA2E5929DE5EEED0939AC8C1200685BEDA9CE41BB4C9E356835394406921DED3EED9E5E0A39AC8C12003C271ED7BFC7BEE15FF56F90670C5A4F0E919164AF75DFE9093A2C8F1200743ADBED50E7367B973C53900D394466921DE53EE40E2D051D964709C0ACDAECF6DA3EFFAA7F9D3C4B90153944A692BDD46DEB093B689400CC9A5D767F67CEDE20CF0E64490E91B9646F7477F5041EFA5102300BB6DB6F3A07ED127956206B72881990EC79EE22F7D052E0A11F2500B98AC7F4EFB37FECCCDB1A7946207B728819926CBDDBE49E5C0A3DF4A2042027DD9FEEBFD383FF4479266066C8216650B28D6E574FF0E1399400E4A0FB14BE67CA330033470E31C3BA4F2274774FF8A18B1280B6DA668FF3643E289343CCB8646BDC05EEC74BE1872E4A00DAE4467BAAB3DFAEEDCCDB5AB9EB986972082C487E6824BBD8DDBF1480A004A0F96EB4A73BFB6CB307FFD172B7012787408F64EBDCA5EE81A5109C75940034D10DF68C07FF760FFEF5729781023904A4EEEB0B7CD0FD622908671925004D71BD1DEEECB53D1EFC3C6F3F062687C08A921DE73EEC1E5E0AC3594509409DB6D891CEAD76D083FFC572578115C821309064C7BB8FBA47960271165102306DDDE0BFC383FF2572378101C8213094EEBF08BCDFFD642914670D2500D3103FDCB7D76EE62B7E54410E8191745F75F09DEE9B4BC1384B28019894EDF6EBCE7EFB8C073FAFD287CAC82130B66467B85BDCE1A5809C0594005469A73DD8396897CA1D03C624874065929DEA3EEB1E5F0AC9DC5102308E784DFEDDF683CE6DF666B9534045E410A85CB2135CFCC0E06C3C8490128061C543F9E207FBE6EC157287808AC9213031C99EEFE2E7046E774716C2325794000C6287FD6AF1FBFBC7CA9D0126440E81A9E8BE02E1952EDF6718A404408967ECDB63FFCA2BF3A14E72084C55F7D103E7BA7DEE9985E0CC092500215E87FF66FB79E780975E7E9A1F0D2087406D929DECE26705F27A01224AC0EC8AC7EE77BFB77FBABCE6819AC82150BBEE4B12BFD56D75BF5E08D1B6A304CC8ECD76A4B3CBEEF3AFF63FE25FEDF352BC682439041A25D931EE1D6E873BB410A66D4509C8573C7C6FA7FDACB3DFAEF2D0FF5D792D030D22874063759F76F85D6EB77B622154DB8612908F6EE83FE0A1FF771EFA3C3D2F5A450E815648F602F71EB7D7FD76215CDB8212D05ECFFE30DF7EBBC643FFF7E4B509B4801C02AD93EC85EEBD6E977B6C21649B8E12D01EF1EA7BF195FE3EFB8287FEEFCB6B10681939045A2DD95A77A6DBE4BEE79AFB84439480E6DA6E8F77F6D89D9D83F67E0FFDA3E5B506B4981C02594976928B7F1DD8E61E5E08DE26A10434437C95BFCBEE5FF82A7FCE5E2DAF252023720864ABFBF0C237B82BDCB75C335EAD9012508FEDF69BCEADF6CFFE55FEC5FE553E0FD7C34C91436066243BDEBDC57DDCDDE1EA7B98212560B2E227F677D8239D3DF6F5CE01FB2B0FFC97C86B0298117208CCACEECF0F9CEE2E733BDD434E07F6245002AA73831DEEECB4FFEEECB55BFC2BFCF7F27D7CA0971C022848768ABBD07DDE7DDF3DED748057811230BC7868DE367BA2B3DBFEA3B3DF3ED799B3B3E57D0960891C025841B275EE35EE0277959B730F3A1DE8A3A0042C2FBEB2BFD97ED9D963DFF6B0BFB6739B9DE75FDDAF97F7158065C9218011243BC1BDC97DC05DE7EE728F3B1DF2AB99F51210DFB38F1FD2DB6DFFD9D96BBB3B07ED2FFD2BFB57C8DB1EC0D0E4104045BA8F3A78B93BCBBDCFC5A30FB6B8AFBB9FB9951F85907B0988D7C5BFC9FEA7B3CB7EB2F0D3F8FBBC381DB4CB3CE8CFE0A7F281C992430053D2FD7642FC8CC139EE62F74977A3FB8ABBDBFDACF36D7BB2752520BE7A8F70DF668F2FFCE4FD2EFB69678FFD9B07FCE6CE01BB7CE17BF4FCB33D502B3904D030FBEC440FCE3FF4E07CA7FB0B0FCF8FBB6BFCD737B9DB3BB7D9B7FD2BE7EF77F6DBBD9DBD769F7F35FD60E716FBA507EFA39D9DF66B0FE1273ADBEDB79DAD1ECAF13DF478D29BF8C1B910611DBFBFDEE7F1DAF55BED290FEE27FDCF1FF2AFCEE3EF3EEA7EB5F0FCF7F153F5BBECC7FEB6BFEBEFE39FFCE3DAEAEFF3EA857F9E8FEFC5C76BDEF34A78400B74ECFF03032578A7544E1FD00000000049454E44AE426082, 99)
SET IDENTITY_INSERT [dbo].[tbl_MstMenu] OFF
SET IDENTITY_INSERT [dbo].[tbl_Users] ON 

INSERT [dbo].[tbl_Users] ([Username], [Password], [FirstName], [LastName], [Email], [EmpID], [RoleID], [CrDate], [CrUser], [EdDate], [EdUser], [FlagDel], [FlagSend], [UserID]) VALUES (N'Admin', N'uni17', N'Admin', N'', N'', N'103E000', 5, NULL, NULL, NULL, NULL, 0, 0, 1)
SET IDENTITY_INSERT [dbo].[tbl_Users] OFF
ALTER TABLE [dbo].[tbl_AdmAuthorize] ADD  CONSTRAINT [DF_AdmAuthorize_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_CycleNo]  DEFAULT ((0)) FOR [CycleNo]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_Year]  DEFAULT (datepart(year,getdate())) FOR [Year]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_BranchID]  DEFAULT ('') FOR [DepotNo]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_WHID]  DEFAULT ('') FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_SalAreaID]  DEFAULT ('') FOR [SalAreaID]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_AmtQty]  DEFAULT ((0)) FOR [AmtQty]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_EdDate]  DEFAULT (getdate()) FOR [EdDate]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_EdUser]  DEFAULT ('') FOR [EdUser]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_Seq__71BDA4CF]  DEFAULT ((0)) FOR [Seq]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_Title__72B1C908]  DEFAULT (N'คุณ') FOR [TitleName]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_First__73A5ED41]  DEFAULT ('') FOR [FirstName]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_LastN__749A117A]  DEFAULT ('') FOR [LastName]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_SalAreaName]  DEFAULT ('') FOR [SalAreaName]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_ZoneName]  DEFAULT ('') FOR [ZoneName]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_SaleEmpID]  DEFAULT ('') FOR [SaleEmpID]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_ZoneID]  DEFAULT ((0)) FOR [ZoneID]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_WHName]  DEFAULT ('') FOR [WHName]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_WHSeq]  DEFAULT ((0)) FOR [WHSeq]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_Temp_AmtArCustID]  DEFAULT ('') FOR [AmtArCustID]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_VanType]  DEFAULT ((0)) FOR [VanTypeID]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_VanTypeName]  DEFAULT ('') FOR [VanTypeName]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_VanTypeSeq]  DEFAULT ((0)) FOR [VanTypeSeq]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomer] ADD  CONSTRAINT [DF_AmtArCustomer_WHType]  DEFAULT ((0)) FOR [WHType]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_CustomerID]  DEFAULT ('') FOR [CustomerID]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_Seq]  DEFAULT ((0)) FOR [Seq]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_CustTitle]  DEFAULT (N'คุณ') FOR [CustTitle]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_CustName]  DEFAULT ('') FOR [CustName]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_CustShotName]  DEFAULT ('') FOR [CustShotName]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_Contact]  DEFAULT ('') FOR [Contact]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_BillTo]  DEFAULT ('') FOR [BillTo]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_AreaName]  DEFAULT ('') FOR [AreaName]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_DistrictName]  DEFAULT ('') FOR [DistrictName]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_ProvinceName]  DEFAULT ('') FOR [ProvinceName]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_PostalCode]  DEFAULT ('') FOR [PostalCode]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_Email]  DEFAULT ('') FOR [Email]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_Telephone]  DEFAULT ('') FOR [Telephone]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_ShopTypeID]  DEFAULT ((0)) FOR [ShopTypeID]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_ShopTypeName]  DEFAULT ('') FOR [ShopTypeName]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_AmtArCustID]  DEFAULT ('') FOR [AmtArCustID]
GO
ALTER TABLE [dbo].[tbl_AmtArCustomerDetail] ADD  CONSTRAINT [DF_AmtArCustomerDetail_FlagShelf]  DEFAULT ((0)) FOR [FlagShelf]
GO
ALTER TABLE [dbo].[tbl_ApSupplier] ADD  CONSTRAINT [DF_ApSupplier_SupplierRefCode]  DEFAULT ('') FOR [SupplierRefCode]
GO
ALTER TABLE [dbo].[tbl_ApSupplier] ADD  CONSTRAINT [DF_ApSupplier_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ApSupplier] ADD  CONSTRAINT [DF_ApSupplier_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_ApSupplier] ADD  CONSTRAINT [DF_ApSupplier_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ApSupplier] ADD  CONSTRAINT [DF_ApSupplier_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ApSupplierType] ADD  CONSTRAINT [DF_ApSupplierType_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ApSupplierType] ADD  CONSTRAINT [DF_ApSupplierType_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ApSupplierType] ADD  CONSTRAINT [DF_ApSupplierType_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_CustomerRefCode]  DEFAULT ('') FOR [CustomerRefCode]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_Seq]  DEFAULT ((0)) FOR [Seq]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_CustomerTypeID]  DEFAULT ((0)) FOR [CustomerTypeID]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_CreditDay]  DEFAULT ((0)) FOR [CreditDay]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_VatType]  DEFAULT ((1)) FOR [VatType]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_VatRate]  DEFAULT ((7)) FOR [VatRate]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_DiscountType]  DEFAULT ('N') FOR [DiscountType]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_Discount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_PriceGroupID]  DEFAULT ((1)) FOR [PriceGroupID]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_WHID]  DEFAULT ((0)) FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_ShopTypeID]  DEFAULT ((0)) FOR [ShopTypeID]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_FlagLoyalty]  DEFAULT ((0)) FOR [FlagMember]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_NetPoint]  DEFAULT ((0)) FOR [NetPoint]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_CustomerSAPCode]  DEFAULT ('') FOR [CustomerSAPCode]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_FlagMember1]  DEFAULT ((0)) FOR [FlagShelf]
GO
ALTER TABLE [dbo].[tbl_ArCustomer] ADD  CONSTRAINT [DF_ArCustomer_PromotionVanID_1]  DEFAULT ((0)) FOR [PromotionVanID]
GO
ALTER TABLE [dbo].[tbl_ArCustomerShelf] ADD  CONSTRAINT [DF_Table_1_ShelfCode]  DEFAULT ('') FOR [ShelfID]
GO
ALTER TABLE [dbo].[tbl_ArCustomerShelf] ADD  CONSTRAINT [DF_ArCustomerShelf_WHID]  DEFAULT ((0)) FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_ArCustomerShelf] ADD  CONSTRAINT [DF_ArCustomerShelf_FlagNew]  DEFAULT ((0)) FOR [FlagNew]
GO
ALTER TABLE [dbo].[tbl_ArCustomerShelf] ADD  CONSTRAINT [DF_ArCustomerShelf_FlagEdit]  DEFAULT ((0)) FOR [FlagEdit]
GO
ALTER TABLE [dbo].[tbl_ArCustomerShelf] ADD  CONSTRAINT [DF_ArCustomerShelf_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ArCustomerShelf] ADD  CONSTRAINT [DF_ArCustomerShelf_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ArCustomerShelf] ADD  CONSTRAINT [DF_ArCustomerShelf_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ArCustomerType] ADD  CONSTRAINT [DF_ArCustomerType_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ArCustomerType] ADD  CONSTRAINT [DF_ArCustomerType_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ArCustomerType] ADD  CONSTRAINT [DF_ArCustomerType_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_Banknote] ADD  CONSTRAINT [DF_Banknote_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_BranchRefCode]  DEFAULT ('') FOR [BranchRefCode]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_BranchGroupID]  DEFAULT ((0)) FOR [BranchGroupID]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_SalAreaID]  DEFAULT ((0)) FOR [SalAreaID]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_ProvinceID]  DEFAULT ((0)) FOR [ProvinceID]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_AreaID]  DEFAULT ((0)) FOR [AreaID]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_DistrictID]  DEFAULT ((0)) FOR [DistrictID]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_ShopTypeID]  DEFAULT ((0)) FOR [ShopTypeID]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_PriceGroupID]  DEFAULT ((0)) FOR [PriceGroupID]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_CloseTime]  DEFAULT ('00:00:00') FOR [CloseTime]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_CanMinus]  DEFAULT ((0)) FOR [CanMinus]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_SysType]  DEFAULT ('M') FOR [SystemType]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_PartNo]  DEFAULT ((0)) FOR [PartID]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_PartSeq]  DEFAULT ((0)) FOR [PartSeq]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_SAPPlantID_1]  DEFAULT ('') FOR [SAPPlantID]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_AgentID]  DEFAULT ('') FOR [AgentID]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  DEFAULT ((0)) FOR [BU]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  DEFAULT ((0)) FOR [DC]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  DEFAULT ('') FOR [BranchTax]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  DEFAULT ('') FOR [BranchTitle]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_Prefix1]  DEFAULT ('') FOR [Prefix1]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_Prefix2]  DEFAULT ('') FOR [Prefix2]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_RunInit]  DEFAULT ((0)) FOR [RunInit]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_RunLength]  DEFAULT ((3)) FOR [RunLength]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_State]  DEFAULT ('') FOR [State]
GO
ALTER TABLE [dbo].[tbl_Branch] ADD  CONSTRAINT [DF_Branch_Country]  DEFAULT ('') FOR [Country]
GO
ALTER TABLE [dbo].[tbl_BranchGroup] ADD  CONSTRAINT [DF_BranchType_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_BranchGroup] ADD  CONSTRAINT [DF_BranchType_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_BranchGroup] ADD  CONSTRAINT [DF_BranchGroup_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_BranchLog] ADD  CONSTRAINT [DF_BranchLog_LogTime]  DEFAULT (getdate()) FOR [LogTime]
GO
ALTER TABLE [dbo].[tbl_BranchLog] ADD  CONSTRAINT [DF_Table_2_Action]  DEFAULT ('') FOR [Event]
GO
ALTER TABLE [dbo].[tbl_BranchLog] ADD  CONSTRAINT [DF_BranchLog_IBDocNo]  DEFAULT ('') FOR [IBDocNo]
GO
ALTER TABLE [dbo].[tbl_BranchLog] ADD  CONSTRAINT [DF_BranchLog_INDocNo]  DEFAULT ('') FOR [INDocNo]
GO
ALTER TABLE [dbo].[tbl_BranchLog] ADD  CONSTRAINT [DF_BranchLog_ShiftNo]  DEFAULT ((0)) FOR [ShiftNo]
GO
ALTER TABLE [dbo].[tbl_BranchLog] ADD  CONSTRAINT [DF_BranchLog_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_BranchLog] ADD  CONSTRAINT [DF_BranchLog_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_BranchRental] ADD  CONSTRAINT [DF_BranchRental_Rental]  DEFAULT ((0.00)) FOR [Rental]
GO
ALTER TABLE [dbo].[tbl_BranchRental] ADD  CONSTRAINT [DF_BranchRental_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_BranchRental] ADD  CONSTRAINT [DF_BranchRental_CrUser]  DEFAULT ('Admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_BranchRental] ADD  CONSTRAINT [DF_BranchRental_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_BranchRental] ADD  CONSTRAINT [DF_BranchRental_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_BranchSupervisor] ADD  CONSTRAINT [DF_BranchSupervisor_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_BranchSupervisor] ADD  CONSTRAINT [DF_BranchSupervisor_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_BranchSupervisor] ADD  CONSTRAINT [DF_BranchSupervisor_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_BranchTerritory] ADD  CONSTRAINT [DF_BranchTerritory_BranchID]  DEFAULT ('') FOR [BranchID]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouse] ADD  CONSTRAINT [DF_BranchWarehouse_WHSeq]  DEFAULT ((0)) FOR [WHSeq]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouse] ADD  CONSTRAINT [DF_BranchWarehouse_License]  DEFAULT ('') FOR [License]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouse] ADD  CONSTRAINT [DF_BranchWarehouse_WHType]  DEFAULT ((0)) FOR [WHType]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouse] ADD  CONSTRAINT [DF_BranchWarehouse_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouse] ADD  CONSTRAINT [DF_BranchWarehouse_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouse] ADD  CONSTRAINT [DF_BranchWarehouse_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouse] ADD  CONSTRAINT [DF_BranchWarehouse_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouse] ADD  CONSTRAINT [DF_BranchWarehouse_VanType_1]  DEFAULT ((0)) FOR [VanType]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouse] ADD  DEFAULT ('0') FOR [HelperEmpID]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouse] ADD  CONSTRAINT [DF_BranchWarehouse_POSNo]  DEFAULT ('') FOR [POSNo]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouseData] ADD  CONSTRAINT [DF_BranchWarehouseData_SendOnline]  DEFAULT ((0)) FOR [SendOnline]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouseData] ADD  CONSTRAINT [DF_BranchWarehouseData_ReceiveOnline]  DEFAULT ((0)) FOR [ReceiveOnline]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouseData] ADD  CONSTRAINT [DF_BranchWarehouseData_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouseData] ADD  CONSTRAINT [DF_BranchWarehouseData_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouseData] ADD  CONSTRAINT [DF_BranchWarehouseData_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_BranchWarehouseData] ADD  CONSTRAINT [DF_BranchWarehouseData_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_Cause] ADD  CONSTRAINT [DF_Option_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_Cause] ADD  CONSTRAINT [DF_Option_CrUser]  DEFAULT ('Admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_Cause] ADD  CONSTRAINT [DF_Option_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_Cause] ADD  CONSTRAINT [DF_Option_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_CfgConnection] ADD  CONSTRAINT [DF_CfgConnection_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_CfgFixedHolidays] ADD  CONSTRAINT [DF_CfgFixedHolidays_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_CfgFloatingHolidays] ADD  CONSTRAINT [DF_CfgFloatingHolidays_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_CfgKeyField] ADD  CONSTRAINT [DF_CfgKeyField_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_CfgPosMachine] ADD  CONSTRAINT [DF_cfgMachine_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_CfgPosMachine] ADD  CONSTRAINT [DF_cfgMachine_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_CfgPosMachine] ADD  CONSTRAINT [DF_cfgMachine_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_CfgPosMachine] ADD  CONSTRAINT [DF_CfgPosMachine_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_CfgSetting] ADD  CONSTRAINT [DF_CfgSetting_cfgValue]  DEFAULT ('') FOR [cfgValue]
GO
ALTER TABLE [dbo].[tbl_CfgSetting] ADD  CONSTRAINT [DF_CfgSetting_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[tbl_CfgSetting] ADD  CONSTRAINT [DF_CfgSetting_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_Company] ADD  CONSTRAINT [DF_Company_IsShow]  DEFAULT ((0)) FOR [IsShow]
GO
ALTER TABLE [dbo].[tbl_Company] ADD  CONSTRAINT [DF_Company_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_DelArCustomer] ADD  CONSTRAINT [DF_DelArCustomer_DepotNo]  DEFAULT ('') FOR [DepotNo]
GO
ALTER TABLE [dbo].[tbl_DelArCustomer] ADD  CONSTRAINT [DF_DelArCustomer_CustomerID]  DEFAULT ('') FOR [CustomerID]
GO
ALTER TABLE [dbo].[tbl_DelArCustomer] ADD  CONSTRAINT [DF_DelArCustomer_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_DelArCustomer] ADD  CONSTRAINT [DF_DelArCustomer_CrUser]  DEFAULT ('') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_DelArCustomer] ADD  CONSTRAINT [DF_DelArCustomer_EdDate]  DEFAULT (getdate()) FOR [EdDate]
GO
ALTER TABLE [dbo].[tbl_DelArCustomer] ADD  CONSTRAINT [DF_DelArCustomer_EdUser]  DEFAULT ('') FOR [EdUser]
GO
ALTER TABLE [dbo].[tbl_DelArCustomer] ADD  CONSTRAINT [DF_DelArCustomer_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_Department] ADD  CONSTRAINT [DF_Department_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_Department] ADD  CONSTRAINT [DF_Department_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_Department] ADD  CONSTRAINT [DF_Department_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_DocRunning] ADD  CONSTRAINT [DF_DocRunning_ModifiledDate]  DEFAULT (getdate()) FOR [ModifiledDate]
GO
ALTER TABLE [dbo].[tbl_DocRunning] ADD  CONSTRAINT [DF_DocRunning_WHCode]  DEFAULT ('00') FOR [WHCode]
GO
ALTER TABLE [dbo].[tbl_DocRunning] ADD  CONSTRAINT [DF_DocRunning_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_DocSendUpdate] ADD  CONSTRAINT [DF_DocSendUpdate_DepotNo]  DEFAULT ('') FOR [DepotNo]
GO
ALTER TABLE [dbo].[tbl_DocSendUpdate] ADD  CONSTRAINT [DF_DocSendUpdate_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_DocSendUpdate] ADD  CONSTRAINT [DF_DocSendUpdate_CrUser]  DEFAULT ('') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_DocSendUpdate] ADD  CONSTRAINT [DF_DocSendUpdate_EdDate]  DEFAULT (getdate()) FOR [EdDate]
GO
ALTER TABLE [dbo].[tbl_DocSendUpdate] ADD  CONSTRAINT [DF_DocSendUpdate_EdUser]  DEFAULT ('') FOR [EdUser]
GO
ALTER TABLE [dbo].[tbl_DocSendUpdate] ADD  CONSTRAINT [DF_DocSendUpdate_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_DocSignature] ADD  CONSTRAINT [DF_DocSignature_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_DocumentStatus] ADD  CONSTRAINT [DF_DocumentStatus_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_DocumentType] ADD  CONSTRAINT [DF_DocumentType_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_DocumentType] ADD  CONSTRAINT [DF_DocumentType_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_DocumentType] ADD  CONSTRAINT [DF_DocumentType_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_Employee] ADD  CONSTRAINT [DF_Employee_TitleName]  DEFAULT ('คุณ') FOR [TitleName]
GO
ALTER TABLE [dbo].[tbl_Employee] ADD  CONSTRAINT [DF_Employee_NickName]  DEFAULT ('') FOR [NickName]
GO
ALTER TABLE [dbo].[tbl_Employee] ADD  CONSTRAINT [DF_Employee_DepartmentID]  DEFAULT ((0)) FOR [DepartmentID]
GO
ALTER TABLE [dbo].[tbl_Employee] ADD  CONSTRAINT [DF_Employee_PositionID]  DEFAULT ((0)) FOR [PositionID]
GO
ALTER TABLE [dbo].[tbl_Employee] ADD  CONSTRAINT [DF_Employee_RoleID]  DEFAULT ((0)) FOR [RoleID]
GO
ALTER TABLE [dbo].[tbl_Employee] ADD  CONSTRAINT [DF_Employee_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_Employee] ADD  CONSTRAINT [DF_Employee_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_Employee] ADD  CONSTRAINT [DF_Employee_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_Employee] ADD  CONSTRAINT [DF_Employee_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_Employee] ADD  CONSTRAINT [DF_Employee_IDCard]  DEFAULT ('') FOR [IDCard]
GO
ALTER TABLE [dbo].[tbl_Employee] ADD  CONSTRAINT [DF_Employee_EmpIDCard_1]  DEFAULT ('') FOR [EmpIDCard]
GO
ALTER TABLE [dbo].[tbl_ErrorLog] ADD  CONSTRAINT [DF_ErrorLog_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_Expense] ADD  CONSTRAINT [DF_SaleExpense_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_Expense] ADD  CONSTRAINT [DF_SaleExpense_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_Expense] ADD  CONSTRAINT [DF_SaleExpense_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_Expense] ADD  CONSTRAINT [DF_Expense_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_InvMovement] ADD  CONSTRAINT [DF_InvMovement_DocTypeCode]  DEFAULT ('') FOR [DocTypeCode]
GO
ALTER TABLE [dbo].[tbl_InvMovement] ADD  CONSTRAINT [DF_InvMovement_FromWHID]  DEFAULT ('') FOR [FromWHID]
GO
ALTER TABLE [dbo].[tbl_InvMovement] ADD  CONSTRAINT [DF_InvMovement_ToWHID]  DEFAULT ('') FOR [ToWHID]
GO
ALTER TABLE [dbo].[tbl_InvMovement] ADD  CONSTRAINT [DF_InvMovement_TrnQtyIn]  DEFAULT ((0)) FOR [TrnQtyIn]
GO
ALTER TABLE [dbo].[tbl_InvMovement] ADD  CONSTRAINT [DF_InvMovement_TrnQtyOut]  DEFAULT ((0)) FOR [TrnQtyOut]
GO
ALTER TABLE [dbo].[tbl_InvMovement] ADD  CONSTRAINT [DF_InvMovement_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_InvMovement] ADD  CONSTRAINT [DF_InvMovement_EdDate]  DEFAULT (getdate()) FOR [EdDate]
GO
ALTER TABLE [dbo].[tbl_InvMovement] ADD  CONSTRAINT [DF_InvMovement_ProductGroupID]  DEFAULT ((0)) FOR [ProductGroupCode]
GO
ALTER TABLE [dbo].[tbl_InvMovement] ADD  CONSTRAINT [DF_InvMovement_ProductGroupName]  DEFAULT ('') FOR [ProductGroupName]
GO
ALTER TABLE [dbo].[tbl_InvMovement] ADD  CONSTRAINT [DF_InvMovement_ProductSubGroupID]  DEFAULT ((0)) FOR [ProductSubGroupCode]
GO
ALTER TABLE [dbo].[tbl_InvMovement] ADD  CONSTRAINT [DF_InvMovement_ProductSubGroupName]  DEFAULT ('') FOR [ProductSubGroupName]
GO
ALTER TABLE [dbo].[tbl_InvMovement] ADD  CONSTRAINT [DF_InvMovement_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_InvTransaction] ADD  CONSTRAINT [DF_TransactionHistory_DocTypeCode]  DEFAULT ('') FOR [DocTypeCode]
GO
ALTER TABLE [dbo].[tbl_InvTransaction] ADD  CONSTRAINT [DF_InvTransaction_TrnQtyIn]  DEFAULT ((0)) FOR [TrnQtyIn]
GO
ALTER TABLE [dbo].[tbl_InvTransaction] ADD  CONSTRAINT [DF_InvTransaction_TrnQtyOut]  DEFAULT ((0)) FOR [TrnQtyOut]
GO
ALTER TABLE [dbo].[tbl_InvTransaction] ADD  CONSTRAINT [DF_TransactionHistory_UnitCost]  DEFAULT ((0)) FOR [UnitCost]
GO
ALTER TABLE [dbo].[tbl_InvTransaction] ADD  CONSTRAINT [DF_InvTransaction_LineDiscountType]  DEFAULT ('N') FOR [LineDiscountType]
GO
ALTER TABLE [dbo].[tbl_InvTransaction] ADD  CONSTRAINT [DF_InvTransaction_LineDiscount]  DEFAULT ((0)) FOR [LineDiscount]
GO
ALTER TABLE [dbo].[tbl_InvTransaction] ADD  CONSTRAINT [DF_InvTransaction_Supplier]  DEFAULT ((0)) FOR [Supplier]
GO
ALTER TABLE [dbo].[tbl_InvTransaction] ADD  CONSTRAINT [DF_InvTransaction_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[tbl_InvTransaction] ADD  CONSTRAINT [DF_InvTransaction_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_InvTransaction] ADD  CONSTRAINT [DF_InvTransaction_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_InvWarehouse] ADD  CONSTRAINT [DF_InvWarehouse_QtyOnHand]  DEFAULT ((0)) FOR [QtyOnHand]
GO
ALTER TABLE [dbo].[tbl_InvWarehouse] ADD  CONSTRAINT [DF_InvWarehouse_QtyOnOrder]  DEFAULT ((0)) FOR [QtyOnOrder]
GO
ALTER TABLE [dbo].[tbl_InvWarehouse] ADD  CONSTRAINT [DF_InvWarehouse_QtyOnBackOrder]  DEFAULT ((0)) FOR [QtyOnBackOrder]
GO
ALTER TABLE [dbo].[tbl_InvWarehouse] ADD  CONSTRAINT [DF_InvWarehouse_QtyInTransit]  DEFAULT ((0)) FOR [QtyInTransit]
GO
ALTER TABLE [dbo].[tbl_InvWarehouse] ADD  CONSTRAINT [DF_InvWarehouse_QtyOutTransit]  DEFAULT ((0)) FOR [QtyOutTransit]
GO
ALTER TABLE [dbo].[tbl_InvWarehouse] ADD  CONSTRAINT [DF_InvWarehouse_QtyOnReject]  DEFAULT ((0)) FOR [QtyOnReject]
GO
ALTER TABLE [dbo].[tbl_InvWarehouse] ADD  CONSTRAINT [DF_InvWarehouse_MinimumQty]  DEFAULT ((0)) FOR [MinimumQty]
GO
ALTER TABLE [dbo].[tbl_InvWarehouse] ADD  CONSTRAINT [DF_InvWarehouse_MaximumQty]  DEFAULT ((0)) FOR [MaximumQty]
GO
ALTER TABLE [dbo].[tbl_InvWarehouse] ADD  CONSTRAINT [DF_InvWarehouse_ReOrderQty]  DEFAULT ((0)) FOR [ReOrderQty]
GO
ALTER TABLE [dbo].[tbl_InvWarehouse] ADD  CONSTRAINT [DF_PrdWarehouse_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_InvWarehouse] ADD  CONSTRAINT [DF_PrdWarehouse_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_InvWarehouse] ADD  CONSTRAINT [DF_InvWarehouse_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_IVDetail] ADD  CONSTRAINT [DF_IVDetail_OrderQty]  DEFAULT ((0.00)) FOR [OrderQty]
GO
ALTER TABLE [dbo].[tbl_IVDetail] ADD  CONSTRAINT [DF_IVDetail_ReceivedQty]  DEFAULT ((0.00)) FOR [ReceivedQty]
GO
ALTER TABLE [dbo].[tbl_IVDetail] ADD  CONSTRAINT [DF_IVDetail_RejectedQty]  DEFAULT ((0)) FOR [RejectedQty]
GO
ALTER TABLE [dbo].[tbl_IVDetail] ADD  CONSTRAINT [DF_IVDetail_StockedQty]  DEFAULT ((0)) FOR [StockedQty]
GO
ALTER TABLE [dbo].[tbl_IVDetail] ADD  CONSTRAINT [DF_IVDetail_DiscountType]  DEFAULT ('N') FOR [LineDiscountType]
GO
ALTER TABLE [dbo].[tbl_IVDetail] ADD  CONSTRAINT [DF_IVDetail_LineDiscount]  DEFAULT ((0)) FOR [LineDiscount]
GO
ALTER TABLE [dbo].[tbl_IVDetail] ADD  CONSTRAINT [DF_IVDetail_CustType]  DEFAULT ('') FOR [CustType]
GO
ALTER TABLE [dbo].[tbl_IVDetail] ADD  CONSTRAINT [DF_IVDetail_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_IVDetail] ADD  CONSTRAINT [DF_IVDetail_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_IVDetail] ADD  CONSTRAINT [DF_IVDetail_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_IVDetail] ADD  CONSTRAINT [DF_IVDetail_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_IVDetail] ADD  CONSTRAINT [DF_IVDetail_UnitComPrice]  DEFAULT ((0)) FOR [UnitComPrice]
GO
ALTER TABLE [dbo].[tbl_IVDetail] ADD  CONSTRAINT [DF_IVDetail_LineComTotal]  DEFAULT ((0)) FOR [LineComTotal]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_RevisionNumber]  DEFAULT ((0)) FOR [RevisionNo]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_WHID]  DEFAULT ((0)) FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_SaleEmpID]  DEFAULT ((0)) FOR [SaleEmpID]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_SalAreaID]  DEFAULT ((0)) FOR [SalAreaID]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_CustomerID]  DEFAULT ((0)) FOR [CustomerID]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_CustPONo]  DEFAULT ('') FOR [CustPONo]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_Freight]  DEFAULT ((0.00)) FOR [Freight]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_DiscountType]  DEFAULT ('') FOR [DiscountType]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_TotalDue]  DEFAULT ((0.00)) FOR [TotalDue]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_PayType]  DEFAULT ((0)) FOR [PayType]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_OldTotalCom]  DEFAULT ((0)) FOR [OldTotalCom]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_TotalCom_1]  DEFAULT ((0)) FOR [TotalCom]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_CNType]  DEFAULT ((0)) FOR [CNType]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_FromWHCode]  DEFAULT ('') FOR [FromWHCode]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_FromLocID]  DEFAULT ('') FOR [FromLocCode]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_ToWHCode]  DEFAULT ('') FOR [ToWHCode]
GO
ALTER TABLE [dbo].[tbl_IVMaster] ADD  CONSTRAINT [DF_IVMaster_ToLocID]  DEFAULT ('') FOR [ToLocCode]
GO
ALTER TABLE [dbo].[tbl_LtyQuestion] ADD  CONSTRAINT [DF_LtyQuestion_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_LtyQuestion] ADD  CONSTRAINT [DF_LtyQuestion_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_LtyQuestion] ADD  CONSTRAINT [DF_LtyQuestion_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_LytAnswer] ADD  CONSTRAINT [DF_LytAnswer_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_LytAnswer] ADD  CONSTRAINT [DF_LytAnswer_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_LytAnswer] ADD  CONSTRAINT [DF_LytAnswer_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_LytChoice] ADD  CONSTRAINT [DF_LytChoice_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_LytChoice] ADD  CONSTRAINT [DF_LytChoice_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_LytCustTypeReward] ADD  CONSTRAINT [DF_CustTypeReward_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_LytCustTypeReward] ADD  CONSTRAINT [DF_CustTypeReward_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_LytCustTypeReward] ADD  CONSTRAINT [DF_CustTypeReward_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_LytCustTypeReward] ADD  CONSTRAINT [DF_CustTypeReward_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_LytMember] ADD  CONSTRAINT [DF_LytMember_BranchID]  DEFAULT ('') FOR [BranchID]
GO
ALTER TABLE [dbo].[tbl_LytMember] ADD  CONSTRAINT [DF_LytMember_NetPoint]  DEFAULT ((0)) FOR [NetPoint]
GO
ALTER TABLE [dbo].[tbl_LytMember] ADD  CONSTRAINT [DF_LytMember_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_LytMember] ADD  CONSTRAINT [DF_LytMember_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_LytMember] ADD  CONSTRAINT [DF_LytMember_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_LytPointBonus] ADD  CONSTRAINT [DF_LytPointBonus_BaseQty]  DEFAULT ((0)) FOR [BaseQty]
GO
ALTER TABLE [dbo].[tbl_LytPointBonus] ADD  CONSTRAINT [DF_LytPointBonus_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_LytPointBonus] ADD  CONSTRAINT [DF_LytPointBonus_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_LytPointBonus] ADD  CONSTRAINT [DF_LytPointBonus_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_LytPointByProduct] ADD  CONSTRAINT [DF_LytPointByProduct_KeyID]  DEFAULT ((0)) FOR [KeyID]
GO
ALTER TABLE [dbo].[tbl_LytPointByProduct] ADD  CONSTRAINT [DF_LytPointByProduct_BranchID]  DEFAULT ('') FOR [BranchID]
GO
ALTER TABLE [dbo].[tbl_LytPointByProduct] ADD  CONSTRAINT [DF_LytPointByProduct_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_LytPointByProduct] ADD  CONSTRAINT [DF_LytPointByProduct_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_LytPointCycle] ADD  CONSTRAINT [DF_LytPointCycle_BranchID]  DEFAULT ('') FOR [BranchID]
GO
ALTER TABLE [dbo].[tbl_LytPointCycle] ADD  CONSTRAINT [DF_LytPointCycle_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_LytPointCycle] ADD  CONSTRAINT [DF_LytPointCycle_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_LytPointCycle] ADD  CONSTRAINT [DF_LytPointCycle_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_LytPointMovement] ADD  CONSTRAINT [DF_LytPointMovement_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_LytPointMovement] ADD  CONSTRAINT [DF_LytPointMovement_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_LytPointMovement] ADD  CONSTRAINT [DF_LytPointMovement_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_LytPointType] ADD  CONSTRAINT [DF_LytPointType_Seq]  DEFAULT ((0)) FOR [Seq]
GO
ALTER TABLE [dbo].[tbl_LytPointType] ADD  CONSTRAINT [DF_LytPointType_Value]  DEFAULT ((0)) FOR [Value]
GO
ALTER TABLE [dbo].[tbl_LytPointType] ADD  CONSTRAINT [DF_LytPointType_Amount]  DEFAULT ((0)) FOR [MoneyValue]
GO
ALTER TABLE [dbo].[tbl_LytPointType] ADD  CONSTRAINT [DF_LytPointType_MinQty]  DEFAULT ((0)) FOR [PrdQtyMin]
GO
ALTER TABLE [dbo].[tbl_LytPointType] ADD  CONSTRAINT [DF_LytPointType_MaxQty]  DEFAULT ((0)) FOR [PrdQtyMax]
GO
ALTER TABLE [dbo].[tbl_LytPointType] ADD  CONSTRAINT [DF_LytPointType_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_LytPointType] ADD  CONSTRAINT [DF_LytPointType_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_LytPointType] ADD  CONSTRAINT [DF_LytPointType_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_LytPointTypePerBill] ADD  CONSTRAINT [DF_LytPointTypePerBill_KeyID]  DEFAULT ((0)) FOR [KeyID]
GO
ALTER TABLE [dbo].[tbl_LytPointTypePerBill] ADD  CONSTRAINT [DF_LytPointTypePerBill_BranchID]  DEFAULT ('') FOR [BranchID]
GO
ALTER TABLE [dbo].[tbl_LytPointTypePerBill] ADD  CONSTRAINT [DF_LytPointTypePerBill_CYear]  DEFAULT ((0)) FOR [CYear]
GO
ALTER TABLE [dbo].[tbl_LytPointTypePerBill] ADD  CONSTRAINT [DF_LytPointTypePerBill_CycleNo]  DEFAULT ((0)) FOR [CycleNo]
GO
ALTER TABLE [dbo].[tbl_LytPointTypePerBill] ADD  CONSTRAINT [DF_LytPointTypePerBill_PointTypeID]  DEFAULT ((0)) FOR [PointTypeID]
GO
ALTER TABLE [dbo].[tbl_LytPointTypePerBill] ADD  CONSTRAINT [DF_LytPointPerBill_BuyValue]  DEFAULT ((0)) FOR [BuyValue]
GO
ALTER TABLE [dbo].[tbl_LytPointTypePerBill] ADD  CONSTRAINT [DF_LytPointPerBill_GetPoint]  DEFAULT ((0)) FOR [GetPoint]
GO
ALTER TABLE [dbo].[tbl_LytPointTypePerBill] ADD  CONSTRAINT [DF_LytPointPerBill_IsCondition]  DEFAULT ((0)) FOR [IsCondition]
GO
ALTER TABLE [dbo].[tbl_LytPointTypePerBill] ADD  CONSTRAINT [DF_LytPointTypePerBill_LinkProduct]  DEFAULT ('') FOR [TypeSum]
GO
ALTER TABLE [dbo].[tbl_LytPointTypePerBill] ADD  CONSTRAINT [DF_LytPointPerBill_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_LytPointTypePerBill] ADD  CONSTRAINT [DF_LytPointPerBill_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_LytPointTypePerBill] ADD  CONSTRAINT [DF_LytPointPerBill_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_LytRedeem] ADD  CONSTRAINT [DF_LytRedeem_BaseQty]  DEFAULT ((0)) FOR [BaseQty]
GO
ALTER TABLE [dbo].[tbl_LytRedeem] ADD  CONSTRAINT [DF_LytRedeem_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_LytRedeem] ADD  CONSTRAINT [DF_LytRedeem_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_LytRedeem] ADD  CONSTRAINT [DF_LytRedeem_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_LytReward] ADD  CONSTRAINT [DF_LytReward_BuyValue]  DEFAULT ((0)) FOR [BuyValue]
GO
ALTER TABLE [dbo].[tbl_LytReward] ADD  CONSTRAINT [DF_LytReward_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_LytReward] ADD  CONSTRAINT [DF_LytReward_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_LytReward] ADD  CONSTRAINT [DF_LytReward_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_MstArea] ADD  CONSTRAINT [DF_MstArea_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_MstArea] ADD  CONSTRAINT [DF_MstArea_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_MstArea] ADD  CONSTRAINT [DF_MstArea_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_MstCycle] ADD  CONSTRAINT [DF_MstCycle_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_MstCycle] ADD  CONSTRAINT [DF_MstCycle_CrUser]  DEFAULT ('') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_MstCycle] ADD  CONSTRAINT [DF_MstCycle_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_MstCycle] ADD  CONSTRAINT [DF_MstCycle_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_MstDistrict] ADD  CONSTRAINT [DF_MstDistrict_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_MstDistrict] ADD  CONSTRAINT [DF_MstDistrict_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_MstDistrict] ADD  CONSTRAINT [DF_MstDistrict_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_MstDistrict] ADD  CONSTRAINT [DF_MstDistrict_CountDis]  DEFAULT ((0)) FOR [CountDis]
GO
ALTER TABLE [dbo].[tbl_MstGrade] ADD  CONSTRAINT [DF_MstGrade_BranchID]  DEFAULT ('') FOR [BranchID]
GO
ALTER TABLE [dbo].[tbl_MstGrade] ADD  CONSTRAINT [DF_MstGrade_cYear]  DEFAULT (datepart(year,getdate())) FOR [Year]
GO
ALTER TABLE [dbo].[tbl_MstGrade] ADD  CONSTRAINT [DF_MstGrade_CycleNo]  DEFAULT ((0)) FOR [CycleNo]
GO
ALTER TABLE [dbo].[tbl_MstGrade] ADD  CONSTRAINT [DF_MstGrade_Grade]  DEFAULT ('') FOR [Grade]
GO
ALTER TABLE [dbo].[tbl_MstGrade] ADD  CONSTRAINT [DF_MstGrade_MaxValue]  DEFAULT ((0)) FOR [MaxValue]
GO
ALTER TABLE [dbo].[tbl_MstGrade] ADD  CONSTRAINT [DF_MstGrade_MinValue]  DEFAULT ((0)) FOR [MinValue]
GO
ALTER TABLE [dbo].[tbl_MstGrade] ADD  CONSTRAINT [DF_MstGrade_UOM]  DEFAULT ('') FOR [ValueType]
GO
ALTER TABLE [dbo].[tbl_MstGrade] ADD  CONSTRAINT [DF_MstGrade_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_MstGrade] ADD  CONSTRAINT [DF_MstGrade_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_MstGrade] ADD  CONSTRAINT [DF_MstGrade_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_MstGrade] ADD  CONSTRAINT [DF_MstGrade_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_MstPart] ADD  CONSTRAINT [DF_MstPart_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_MstPart] ADD  CONSTRAINT [DF_MstPart_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_MstPart] ADD  CONSTRAINT [DF_MstPart_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_MstProvince] ADD  CONSTRAINT [DF_MstProvince_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_MstProvince] ADD  CONSTRAINT [DF_MstProvince_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_MstProvince] ADD  CONSTRAINT [DF_MstProvince_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PaidDetail] ADD  CONSTRAINT [DF_PaidDetail_BranchID]  DEFAULT ((0)) FOR [BranchID]
GO
ALTER TABLE [dbo].[tbl_PaidDetail] ADD  CONSTRAINT [DF_PaidDetail_IBDocNo]  DEFAULT ('') FOR [IBDocNo]
GO
ALTER TABLE [dbo].[tbl_PaidDetail] ADD  CONSTRAINT [DF_PaidDetail_INDocNo]  DEFAULT ('') FOR [INDocNo]
GO
ALTER TABLE [dbo].[tbl_PaidDetail] ADD  CONSTRAINT [DF_PaidDetail_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PaidDetail] ADD  CONSTRAINT [DF_PaidDetail_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_PaidDetail] ADD  CONSTRAINT [DF_PaidDetail_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PaidDetail] ADD  CONSTRAINT [DF_PaidDetail_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PaidMaster] ADD  CONSTRAINT [DF_PaidMaster_SendDate]  DEFAULT (getdate()) FOR [SendDate]
GO
ALTER TABLE [dbo].[tbl_PaidMaster] ADD  CONSTRAINT [DF_PaidMaster_SendTotal]  DEFAULT ((0)) FOR [SendTotal]
GO
ALTER TABLE [dbo].[tbl_PaidMaster] ADD  CONSTRAINT [DF_PaidMaster_ShipID]  DEFAULT ((0)) FOR [ShipID]
GO
ALTER TABLE [dbo].[tbl_PaidMaster] ADD  CONSTRAINT [DF_PaidMaster_EmpID]  DEFAULT ((0)) FOR [EmpID]
GO
ALTER TABLE [dbo].[tbl_PaidMaster] ADD  CONSTRAINT [DF_PaidMaster_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PaidMaster] ADD  CONSTRAINT [DF_PaidMaster_CrUser]  DEFAULT ('Admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_PaidMaster] ADD  CONSTRAINT [DF_PaidMaster_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PaidMaster] ADD  CONSTRAINT [DF_PaidMaster_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PayDetail] ADD  CONSTRAINT [DF_PayDetail_Send]  DEFAULT ((0)) FOR [Send]
GO
ALTER TABLE [dbo].[tbl_PayDetail] ADD  CONSTRAINT [DF_PayDetail_Deposit]  DEFAULT ((0)) FOR [Deposit]
GO
ALTER TABLE [dbo].[tbl_PayDetail] ADD  CONSTRAINT [DF_PayDetail_TotalSale]  DEFAULT ((0)) FOR [TotalSale]
GO
ALTER TABLE [dbo].[tbl_PayDetail] ADD  CONSTRAINT [DF_PayDetail_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PayDetail] ADD  CONSTRAINT [DF_PayDetail_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_PayDetail] ADD  CONSTRAINT [DF_PayDetail_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PayDetail] ADD  CONSTRAINT [DF_PayDetail_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PayDetail] ADD  CONSTRAINT [DF_PayDetail_Transfer_1]  DEFAULT ((0)) FOR [Transfer]
GO
ALTER TABLE [dbo].[tbl_PayDetail] ADD  CONSTRAINT [DF_PayDetail_Cheque_1]  DEFAULT ((0)) FOR [Cheque]
GO
ALTER TABLE [dbo].[tbl_PayMaster] ADD  CONSTRAINT [DF_PayMaster_BranchID]  DEFAULT ('') FOR [BranchID]
GO
ALTER TABLE [dbo].[tbl_PayMaster] ADD  CONSTRAINT [DF_PayMaster_Docdate]  DEFAULT (getdate()) FOR [Docdate]
GO
ALTER TABLE [dbo].[tbl_PayMaster] ADD  CONSTRAINT [DF_PayMaster_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PayMaster] ADD  CONSTRAINT [DF_PayMaster_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PayMaster] ADD  CONSTRAINT [DF_PayMaster_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PayMaster] ADD  DEFAULT ((0)) FOR [TotalSend]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_tmpPODetail_OrderQty]  DEFAULT ((0.00)) FOR [OrderQty]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_tmpPODetail_ReceivedQty]  DEFAULT ((0.00)) FOR [ReceivedQty]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_tmpPODetail_RejectedQty]  DEFAULT ((0)) FOR [RejectedQty]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_tmpPODetail_StockedQty]  DEFAULT ((0)) FOR [StockedQty]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_tmpPODetail_DiscountType]  DEFAULT ('N') FOR [LineDiscountType]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_PODetail_LineDiscount1]  DEFAULT ((0)) FOR [LineDiscountRate]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_tmpPODetail_LineDiscount]  DEFAULT ((0)) FOR [LineDiscount]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_tmpPODetail_CustType]  DEFAULT ('') FOR [CustType]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_tmpPODetail_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_tmpPODetail_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_tmpPODetail_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_tmpPODetail_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_tmpPODetail_UnitComPrice]  DEFAULT ((0)) FOR [UnitComPrice]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_tmpPODetail_LineComTotal]  DEFAULT ((0)) FOR [LineComTotal]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_PODetail_LineRemark]  DEFAULT ('') FOR [LineRemark]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_PODetail_FreeQty]  DEFAULT ((0)) FOR [FreeQty]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_PODetail_FreeUom]  DEFAULT ((0)) FOR [FreeUom]
GO
ALTER TABLE [dbo].[tbl_PODetail] ADD  CONSTRAINT [DF_PODetail_FreeUnit]  DEFAULT ((0)) FOR [FreeUnit]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_RevisionNumber]  DEFAULT ((0)) FOR [RevisionNo]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_WHID]  DEFAULT ((0)) FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_SaleEmpID]  DEFAULT ((0)) FOR [SaleEmpID]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_SalAreaID]  DEFAULT ((0)) FOR [SalAreaID]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_CustomerID]  DEFAULT ((0)) FOR [CustomerID]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_CustPONo]  DEFAULT ('') FOR [CustPONo]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_Freight]  DEFAULT ((0.00)) FOR [Freight]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_DiscountType]  DEFAULT ('') FOR [DiscountType]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_TotalDue]  DEFAULT ((0.00)) FOR [TotalDue]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_PayType]  DEFAULT ((0)) FOR [PayType]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_OldTotalCom]  DEFAULT ((0)) FOR [OldTotalCom]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_TotalCom_1]  DEFAULT ((0)) FOR [TotalCom]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_tmpPOMaster_CNType]  DEFAULT ((0)) FOR [CNType]
GO
ALTER TABLE [dbo].[tbl_POMaster] ADD  CONSTRAINT [DF_POMaster_DiscountRate]  DEFAULT ((0.0)) FOR [DiscountRate]
GO
ALTER TABLE [dbo].[tbl_Position] ADD  CONSTRAINT [DF_Position_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_Position] ADD  CONSTRAINT [DF_Position_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_Position] ADD  CONSTRAINT [DF_Position_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PRDetail] ADD  CONSTRAINT [DF_TransferDetail_RejectedQty]  DEFAULT ((0)) FOR [SendQty]
GO
ALTER TABLE [dbo].[tbl_PRDetail] ADD  CONSTRAINT [DF_TransferDetail_ReceivedQty]  DEFAULT ((0.00)) FOR [ReceivedQty]
GO
ALTER TABLE [dbo].[tbl_PRDetail] ADD  CONSTRAINT [DF_TransferDetail_RejectedQty_1]  DEFAULT ((0)) FOR [RejectedQty]
GO
ALTER TABLE [dbo].[tbl_PRDetail] ADD  CONSTRAINT [DF_TransferDetail_StockedQty]  DEFAULT ((0)) FOR [StockedQty]
GO
ALTER TABLE [dbo].[tbl_PRDetail] ADD  CONSTRAINT [DF_PRDetail_UnitCost]  DEFAULT ((0)) FOR [UnitCost]
GO
ALTER TABLE [dbo].[tbl_PRDetail] ADD  CONSTRAINT [DF_PRDetail_UnitPrice]  DEFAULT ((0)) FOR [UnitPrice]
GO
ALTER TABLE [dbo].[tbl_PRDetail] ADD  CONSTRAINT [DF_TransferDetail_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PRDetail] ADD  CONSTRAINT [DF_TransferDetail_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_PRDetail] ADD  CONSTRAINT [DF_TransferDetail_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PRDetail] ADD  CONSTRAINT [DF_PRDetail_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PreSaleWarehouse] ADD  CONSTRAINT [DF_PreSaleWarehouse_Seq]  DEFAULT ((0)) FOR [Seq]
GO
ALTER TABLE [dbo].[tbl_PreSaleWarehouse] ADD  CONSTRAINT [DF_PreSaleWarehouse_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PreSaleWarehouse] ADD  CONSTRAINT [DF_PreSaleWarehouse_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_PreSaleWarehouse] ADD  CONSTRAINT [DF_PreSaleWarehouse_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PreSaleWarehouse] ADD  CONSTRAINT [DF_PreSaleWarehouse_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PriceGroup] ADD  CONSTRAINT [DF_PriceGroup_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PriceGroup] ADD  CONSTRAINT [DF_PriceGroup_CrUser]  DEFAULT ('Admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_PriceGroup] ADD  CONSTRAINT [DF_PriceGroup_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PriceGroup] ADD  CONSTRAINT [DF_PriceGroup_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PRMaster] ADD  CONSTRAINT [DF_PRMaster_RevisionNo]  DEFAULT ((0)) FOR [RevisionNo]
GO
ALTER TABLE [dbo].[tbl_PRMaster] ADD  CONSTRAINT [DF_PRMaster_WHID]  DEFAULT ((0)) FOR [FromWHID]
GO
ALTER TABLE [dbo].[tbl_PRMaster] ADD  CONSTRAINT [DF_PRMaster_SalAreaID]  DEFAULT ((0)) FOR [SalAreaID]
GO
ALTER TABLE [dbo].[tbl_PRMaster] ADD  CONSTRAINT [DF_TransferMaster_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PRMaster] ADD  CONSTRAINT [DF_PRMaster_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_Seq]  DEFAULT ((0)) FOR [Seq]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_ProductRefCode]  DEFAULT ('') FOR [ProductRefCode]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_Barcode]  DEFAULT ('') FOR [Barcode]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_Flavour]  DEFAULT ('ไม่มีรส') FOR [Flavour]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_Size]  DEFAULT ((0)) FOR [Size]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_SizeUOM]  DEFAULT ((0)) FOR [SizeUOM]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_Weight]  DEFAULT ((0)) FOR [Weight]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_WeightUOM]  DEFAULT ((0)) FOR [WeightUOM]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_ReorderPoint]  DEFAULT ((0)) FOR [ReorderPoint]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_MinPoint]  DEFAULT ((0)) FOR [MinPoint]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_PurchaseUomID]  DEFAULT ((0)) FOR [PurchaseUomID]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_SaleUomID]  DEFAULT ((0)) FOR [SaleUomID]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_VatType]  DEFAULT ((1)) FOR [VatType]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_StandardCost]  DEFAULT ((0)) FOR [StandardCost]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_SellPrice]  DEFAULT ((0)) FOR [SellPrice]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_IsFulfill]  DEFAULT ((0)) FOR [IsFulfill]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_FlagNew]  DEFAULT ((0)) FOR [FlagNew]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  CONSTRAINT [DF_Product_FlagEdit]  DEFAULT ((0)) FOR [FlagEdit]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  DEFAULT ((0)) FOR [ProductBrandID]
GO
ALTER TABLE [dbo].[tbl_Product] ADD  DEFAULT ((0)) FOR [ProductLine]
GO
ALTER TABLE [dbo].[tbl_ProductBrand] ADD  DEFAULT ((0)) FOR [ProductBrandSeq]
GO
ALTER TABLE [dbo].[tbl_ProductBrand] ADD  CONSTRAINT [DF_PrdBrand_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ProductBrand] ADD  CONSTRAINT [DF_PrdBrand_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ProductBrand] ADD  CONSTRAINT [DF_ProductBrand_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductChanged] ADD  CONSTRAINT [DF_ProductUpdated_BranchID]  DEFAULT ('') FOR [BranchID]
GO
ALTER TABLE [dbo].[tbl_ProductCompany] ADD  CONSTRAINT [DF_ProductCompany_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ProductCompany] ADD  CONSTRAINT [DF_ProductCompany_EdDate]  DEFAULT (getdate()) FOR [EdDate]
GO
ALTER TABLE [dbo].[tbl_ProductCostHistory] ADD  CONSTRAINT [DF_PrdCostHistory_DateChanged]  DEFAULT (getdate()) FOR [DateChanged]
GO
ALTER TABLE [dbo].[tbl_ProductCostHistory] ADD  CONSTRAINT [DF_ProductCostHistory_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductCostSupplier] ADD  CONSTRAINT [DF_PrdSupplierPrice_UnitPrice]  DEFAULT ((0.0)) FOR [UnitPrice]
GO
ALTER TABLE [dbo].[tbl_ProductCostSupplier] ADD  CONSTRAINT [DF_PrdCostSupplier_OldUnitPrice]  DEFAULT ((0.0)) FOR [OldUnitPrice]
GO
ALTER TABLE [dbo].[tbl_ProductCostSupplier] ADD  CONSTRAINT [DF_PrdSupplierPrice_DateChanged]  DEFAULT (getdate()) FOR [DateChanged]
GO
ALTER TABLE [dbo].[tbl_ProductCostSupplier] ADD  CONSTRAINT [DF_ProductCostSupplier_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductFlavour] ADD  CONSTRAINT [DF_PrdFlavour_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ProductFlavour] ADD  CONSTRAINT [DF_PrdFlavour_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ProductFlavour] ADD  CONSTRAINT [DF_ProductFlavour_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductGroup] ADD  CONSTRAINT [DF_PrdGroup_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ProductGroup] ADD  CONSTRAINT [DF_ProductGroup_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_ProductGroup] ADD  CONSTRAINT [DF_PrdGroup_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ProductGroup] ADD  CONSTRAINT [DF_ProductGroup_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductGroup] ADD  CONSTRAINT [DF_ProductGroup_ProductTypeID]  DEFAULT ((0)) FOR [ProductTypeID]
GO
ALTER TABLE [dbo].[tbl_ProductPriceBranch] ADD  CONSTRAINT [DF_ProductPriceBranch_ProductUomID]  DEFAULT ((0)) FOR [ProductUomID]
GO
ALTER TABLE [dbo].[tbl_ProductPriceBranch] ADD  CONSTRAINT [DF_ProductPriceBranch_SellPrice]  DEFAULT ((0)) FOR [SellPrice]
GO
ALTER TABLE [dbo].[tbl_ProductPriceBranch] ADD  CONSTRAINT [DF_ProductPriceBranch_BuyPrice]  DEFAULT ((0)) FOR [BuyPrice]
GO
ALTER TABLE [dbo].[tbl_ProductPriceBranch] ADD  CONSTRAINT [DF_ProductPriceBranch_SellPriceVat]  DEFAULT ((0)) FOR [SellPriceVat]
GO
ALTER TABLE [dbo].[tbl_ProductPriceBranch] ADD  CONSTRAINT [DF_ProductPriceBranch_BuyPriceVat]  DEFAULT ((0)) FOR [BuyPriceVat]
GO
ALTER TABLE [dbo].[tbl_ProductPriceBranch] ADD  CONSTRAINT [DF_PrdPriceBranch_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ProductPriceBranch] ADD  CONSTRAINT [DF_ProductPriceBranch_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_ProductPriceBranch] ADD  CONSTRAINT [DF_PrdPriceBranch_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ProductPriceBranch] ADD  CONSTRAINT [DF_ProductPriceBranch_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductPriceCustomer] ADD  CONSTRAINT [DF_ProductPriceCustomer_UnitPrice]  DEFAULT ((0.0)) FOR [UnitPrice]
GO
ALTER TABLE [dbo].[tbl_ProductPriceCustomer] ADD  CONSTRAINT [DF_ProductPriceCustomer_OldUnitPrice]  DEFAULT ((0.0)) FOR [OldUnitPrice]
GO
ALTER TABLE [dbo].[tbl_ProductPriceCustomer] ADD  CONSTRAINT [DF_ProductPriceCustomer_DateChanged]  DEFAULT (getdate()) FOR [DateChanged]
GO
ALTER TABLE [dbo].[tbl_ProductPriceCustomer] ADD  CONSTRAINT [DF_ProductPriceCustomer_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductPriceGroup] ADD  CONSTRAINT [DF_ProductPriceGroup_ProductUomID]  DEFAULT ((0)) FOR [ProductUomID]
GO
ALTER TABLE [dbo].[tbl_ProductPriceGroup] ADD  CONSTRAINT [DF_ProductPriceGroup_BuyPrice]  DEFAULT ((0)) FOR [BuyPrice]
GO
ALTER TABLE [dbo].[tbl_ProductPriceGroup] ADD  CONSTRAINT [DF_ProductPriceGroup_SellPriceVat]  DEFAULT ((0)) FOR [SellPriceVat]
GO
ALTER TABLE [dbo].[tbl_ProductPriceGroup] ADD  CONSTRAINT [DF_ProductPriceGroup_BuyPriceVat]  DEFAULT ((0)) FOR [BuyPriceVat]
GO
ALTER TABLE [dbo].[tbl_ProductPriceGroup] ADD  CONSTRAINT [DF_ProductPriceGroup_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ProductPriceGroup] ADD  CONSTRAINT [DF_ProductPriceGroup_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductPriceGroup] ADD  CONSTRAINT [DF_ProductPriceGroup_FlagNew]  DEFAULT ((0)) FOR [FlagNew]
GO
ALTER TABLE [dbo].[tbl_ProductPriceGroup] ADD  CONSTRAINT [DF_ProductPriceGroup_FlagEdit]  DEFAULT ((0)) FOR [FlagEdit]
GO
ALTER TABLE [dbo].[tbl_ProductPriceGroup] ADD  CONSTRAINT [DF_ProductPriceGroup_ComPrice]  DEFAULT ((0)) FOR [ComPrice]
GO
ALTER TABLE [dbo].[tbl_ProductPriceHistory] ADD  CONSTRAINT [DF_PrdPriceHistory_UnitPrice]  DEFAULT ((0)) FOR [UnitPrice]
GO
ALTER TABLE [dbo].[tbl_ProductPriceHistory] ADD  CONSTRAINT [DF_ProductPriceHistory_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductPromotionBom] ADD  CONSTRAINT [DF_ProductPromotionBom_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ProductPromotionBom] ADD  CONSTRAINT [DF_ProductPromotionBom_CrUser]  DEFAULT ('Admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_ProductPromotionBom] ADD  CONSTRAINT [DF_ProductPromotionBom_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ProductPromotionBom] ADD  CONSTRAINT [DF_ProductPromotionBom_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductRemarkReject] ADD  CONSTRAINT [DF_ProductRemarkReject_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ProductRemarkReject] ADD  CONSTRAINT [DF_ProductRemarkReject_CrUser]  DEFAULT ('Admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_ProductRemarkReject] ADD  CONSTRAINT [DF_ProductRemarkReject_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ProductRemarkReject] ADD  CONSTRAINT [DF_ProductRemarkReject_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductSubGroup] ADD  CONSTRAINT [DF_ProductSubGroup_IsFulfill]  DEFAULT ((0)) FOR [IsFulfill]
GO
ALTER TABLE [dbo].[tbl_ProductSubGroup] ADD  CONSTRAINT [DF_PrdSubGroup_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ProductSubGroup] ADD  CONSTRAINT [DF_ProductSubGroup_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_ProductSubGroup] ADD  CONSTRAINT [DF_PrdSubGroup_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ProductSubGroup] ADD  CONSTRAINT [DF_ProductSubGroup_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductType] ADD  CONSTRAINT [DF_ProductType_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ProductType] ADD  CONSTRAINT [DF_ProductType_EdDate]  DEFAULT (getdate()) FOR [EdDate]
GO
ALTER TABLE [dbo].[tbl_ProductType] ADD  CONSTRAINT [DF_ProductType_ProductCompanyID]  DEFAULT ((0)) FOR [ProductCompanyID]
GO
ALTER TABLE [dbo].[tbl_ProductType] ADD  DEFAULT ('Q') FOR [RatioType]
GO
ALTER TABLE [dbo].[tbl_ProductUom] ADD  CONSTRAINT [DF_ProductUom_ProductUomNameTH]  DEFAULT ('') FOR [ProductUomNameTH]
GO
ALTER TABLE [dbo].[tbl_ProductUom] ADD  CONSTRAINT [DF_PrdUom_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ProductUom] ADD  CONSTRAINT [DF_PrdUom_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ProductUom] ADD  CONSTRAINT [DF_ProductUom_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductUomSet] ADD  CONSTRAINT [DF_PrdUomSet_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ProductUomSet] ADD  CONSTRAINT [DF_ProductUomSet_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_ProductUomSet] ADD  CONSTRAINT [DF_PrdUomSet_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ProductUomSet] ADD  CONSTRAINT [DF_ProductUomSet_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ProductUomSet] ADD  CONSTRAINT [DF_ProductUomSet_FlagNew]  DEFAULT ((0)) FOR [FlagNew]
GO
ALTER TABLE [dbo].[tbl_ProductUomSet] ADD  CONSTRAINT [DF_ProductUomSet_FlagEdit]  DEFAULT ((0)) FOR [FlagEdit]
GO
ALTER TABLE [dbo].[tbl_ProductUomSet] ADD  DEFAULT ('') FOR [UomCode]
GO
ALTER TABLE [dbo].[tbl_Promotion] ADD  CONSTRAINT [DF_Promotion_PromotionType]  DEFAULT ('0') FOR [PromotionType]
GO
ALTER TABLE [dbo].[tbl_Promotion] ADD  CONSTRAINT [DF_Promotion_MixType]  DEFAULT ('N') FOR [MixType]
GO
ALTER TABLE [dbo].[tbl_Promotion] ADD  CONSTRAINT [DF_Promotion_MixValue]  DEFAULT ((0)) FOR [MixValue]
GO
ALTER TABLE [dbo].[tbl_Promotion] ADD  CONSTRAINT [DF_Promotion_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_Promotion] ADD  CONSTRAINT [DF_Promotion_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_Promotion] ADD  CONSTRAINT [DF_Promotion_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_Promotion] ADD  CONSTRAINT [DF_Promotion_WHID]  DEFAULT ('') FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_PromotionBranch] ADD  CONSTRAINT [DF_PromotionBranch_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PromotionBranch] ADD  CONSTRAINT [DF_PromotionBranch_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PromotionBranch] ADD  CONSTRAINT [DF_PromotionBranch_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PromotionCustomer] ADD  CONSTRAINT [DF_ArCustomerPromotionHistory_RefCode]  DEFAULT ('') FOR [ShelfID]
GO
ALTER TABLE [dbo].[tbl_PromotionCustomer] ADD  CONSTRAINT [DF_PromotionCustomer_WHID]  DEFAULT ((0)) FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_PromotionCustomer] ADD  CONSTRAINT [DF_PromotionCustomer_FlagNew]  DEFAULT ((0)) FOR [FlagNew]
GO
ALTER TABLE [dbo].[tbl_PromotionCustomer] ADD  CONSTRAINT [DF_PromotionCustomer_FlagEdit]  DEFAULT ((0)) FOR [FlagEdit]
GO
ALTER TABLE [dbo].[tbl_PromotionCustomer] ADD  CONSTRAINT [DF_ArCustomerPromotionHistory_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PromotionCustomer] ADD  CONSTRAINT [DF_ArCustomerPromotionHistory_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PromotionCustomer] ADD  CONSTRAINT [DF_ArCustomerPromotionHistory_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PromotionPack] ADD  CONSTRAINT [DF_PromotionPack_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PromotionPack] ADD  CONSTRAINT [DF_PromotionPack_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PromotionPack] ADD  CONSTRAINT [DF_PromotionPack_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PromotionProduct] ADD  CONSTRAINT [DF_PromotionProduct_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PromotionProduct] ADD  CONSTRAINT [DF_PromotionProduct_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PromotionProduct] ADD  CONSTRAINT [DF_PromotionProduct_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PromotionReward] ADD  CONSTRAINT [DF_PromotionReward_ExchangePrice]  DEFAULT ((0.00)) FOR [ExchangePrice]
GO
ALTER TABLE [dbo].[tbl_PromotionReward] ADD  CONSTRAINT [DF_PromotionReward_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PromotionVan] ADD  CONSTRAINT [DF_Table_1_PromotionType]  DEFAULT ('0') FOR [PromotionVanType]
GO
ALTER TABLE [dbo].[tbl_PromotionVan] ADD  CONSTRAINT [DF_PromotionVan_MixType]  DEFAULT ('N') FOR [MixType]
GO
ALTER TABLE [dbo].[tbl_PromotionVan] ADD  CONSTRAINT [DF_PromotionVan_MixValue]  DEFAULT ((0)) FOR [MixValue]
GO
ALTER TABLE [dbo].[tbl_PromotionVan] ADD  CONSTRAINT [DF_PromotionVan_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PromotionVan] ADD  CONSTRAINT [DF_PromotionVan_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PromotionVan] ADD  CONSTRAINT [DF_PromotionVan_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_PromotionVan] ADD  CONSTRAINT [DF_PromotionVan_WHID]  DEFAULT ('') FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_PromotionVanProduct] ADD  CONSTRAINT [DF_PromotionVanProduct_GetQty]  DEFAULT ((0)) FOR [GetQty]
GO
ALTER TABLE [dbo].[tbl_PromotionVanProduct] ADD  CONSTRAINT [DF_PromotionVanProduct_BaseQty]  DEFAULT ((0)) FOR [BaseQty]
GO
ALTER TABLE [dbo].[tbl_PromotionVanProduct] ADD  CONSTRAINT [DF_PromotionVanProduct_BasePrice]  DEFAULT ((0.0)) FOR [BasePrice]
GO
ALTER TABLE [dbo].[tbl_PromotionVanProduct] ADD  CONSTRAINT [DF_PromotionVanProduct_BaseUomID]  DEFAULT ((0)) FOR [BaseUomID]
GO
ALTER TABLE [dbo].[tbl_PromotionVanProduct] ADD  CONSTRAINT [DF_PromotionVanProduct_BaseUomName]  DEFAULT ('') FOR [BaseUomName]
GO
ALTER TABLE [dbo].[tbl_PromotionVanProduct] ADD  CONSTRAINT [DF_PromotionVanProduct_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_PromotionVanProduct] ADD  CONSTRAINT [DF_PromotionVanProduct_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_PromotionVanProduct] ADD  CONSTRAINT [DF_PromotionVanProduct_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_Roles] ADD  CONSTRAINT [DF_Roles_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_Roles] ADD  CONSTRAINT [DF_Roles_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_RouteOnsite] ADD  CONSTRAINT [DF_RouteOnsite_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_RouteOnsite] ADD  CONSTRAINT [DF_RouteOnsite_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_RouteOnsite] ADD  CONSTRAINT [DF_RouteOnsite_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_SalArea] ADD  CONSTRAINT [DF_SalArea_WHID]  DEFAULT ((0)) FOR [BranchID]
GO
ALTER TABLE [dbo].[tbl_SalArea] ADD  CONSTRAINT [DF_SalArea_Seq]  DEFAULT ((0)) FOR [Seq]
GO
ALTER TABLE [dbo].[tbl_SalArea] ADD  CONSTRAINT [DF_SalArea_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_SalArea] ADD  CONSTRAINT [DF_SalArea_CrUser]  DEFAULT ('Admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_SalArea] ADD  CONSTRAINT [DF_SalArea_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_SalArea] ADD  CONSTRAINT [DF_SalArea_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_SalArea] ADD  CONSTRAINT [DF_SalArea_ZoneID]  DEFAULT ((0)) FOR [ZoneID]
GO
ALTER TABLE [dbo].[tbl_SalAreaDistrict] ADD  CONSTRAINT [DF_SalAreaDistrict_WHID]  DEFAULT ((0)) FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_SalAreaDistrict] ADD  CONSTRAINT [DF_SalAreaDistrict_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_SalAreaVisit] ADD  CONSTRAINT [DF_SalAreaVisit_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_SalAreaVisit] ADD  CONSTRAINT [DF_SalAreaVisit_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_SalAreaVisit] ADD  CONSTRAINT [DF_SalAreaVisit_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_SaleBranchDaily] ADD  CONSTRAINT [DF_Table_1_QtyInTransit]  DEFAULT ((0)) FOR [QtyInTransit]
GO
ALTER TABLE [dbo].[tbl_SaleBranchDaily] ADD  CONSTRAINT [DF_Table_1_QtyOutTransit]  DEFAULT ((0)) FOR [QtyOutTransit]
GO
ALTER TABLE [dbo].[tbl_SaleBranchDaily] ADD  CONSTRAINT [DF_Table_1_QtyOnOrder]  DEFAULT ((0)) FOR [QtyOnSale]
GO
ALTER TABLE [dbo].[tbl_SaleBranchDaily] ADD  CONSTRAINT [DF_ProductBranchDaily_QtyOnReject]  DEFAULT ((0)) FOR [QtyOnReject]
GO
ALTER TABLE [dbo].[tbl_SaleBranchDaily] ADD  CONSTRAINT [DF_Table_1_QtyOnBackOrder]  DEFAULT ((0)) FOR [QtyBalance]
GO
ALTER TABLE [dbo].[tbl_SaleBranchDaily] ADD  CONSTRAINT [DF_ProductBranchDaily_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_SaleBranchDaily] ADD  CONSTRAINT [DF_ProductBranchDaily_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_SaleBranchDaily] ADD  CONSTRAINT [DF_ProductBranchDaily_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_SaleBranchDaily] ADD  CONSTRAINT [DF_SaleBranchDaily_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_SaleBranchSummary] ADD  CONSTRAINT [DF_Table_1_IsSend]  DEFAULT ((0)) FOR [IsSend]
GO
ALTER TABLE [dbo].[tbl_SaleBranchSummary] ADD  CONSTRAINT [DF__SaleBranc__IBDoc__5402595F]  DEFAULT ('') FOR [IBDocNo]
GO
ALTER TABLE [dbo].[tbl_SaleBranchSummary] ADD  CONSTRAINT [DF__SaleBranc__INDoc__54F67D98]  DEFAULT ('') FOR [INDocNo]
GO
ALTER TABLE [dbo].[tbl_SaleBranchSummary] ADD  CONSTRAINT [DF_SaleSummary_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_SaleBranchSummary] ADD  CONSTRAINT [DF_SaleSummary_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_SaleBranchSummary] ADD  CONSTRAINT [DF_SaleSummary_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_SaleBranchSummary] ADD  CONSTRAINT [DF_SaleBranchSummary_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_SaleExpenseDetail] ADD  CONSTRAINT [DF_SaleExpenseDetail_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_SaleExpenseDetail] ADD  CONSTRAINT [DF_SaleExpenseDetail_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_SaleExpenseMaster] ADD  CONSTRAINT [DF_SaleBranchExpenseDaily_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_SaleExpenseMaster] ADD  CONSTRAINT [DF_SaleBranchExpenseDaily_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_SaleExpenseMaster] ADD  CONSTRAINT [DF_SaleBranchExpenseDaily_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_SaleExpenseMaster] ADD  CONSTRAINT [DF_SaleExpenseMaster_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_SaleYearTarget] ADD  CONSTRAINT [DF_SaleYearTarget_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_SaleYearTarget] ADD  CONSTRAINT [DF_SaleYearTarget_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_SaleYearTarget] ADD  CONSTRAINT [DF_SaleYearTarget_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_SaleYearTarget] ADD  CONSTRAINT [DF_SaleYearTarget_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ShopType] ADD  CONSTRAINT [DF_ShopType_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ShopType] ADD  CONSTRAINT [DF_ShopType_CrUser]  DEFAULT ('Admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_ShopType] ADD  CONSTRAINT [DF_ShopType_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ShopType] ADD  CONSTRAINT [DF_ShopType_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_ShopTypeGroup] ADD  CONSTRAINT [DF_ShopTypeGroup_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_ShopTypeGroup] ADD  CONSTRAINT [DF_ShopTypeGroup_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_ShopTypeGroup] ADD  CONSTRAINT [DF_ShopTypeGroup_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_SimCustomer] ADD  CONSTRAINT [DF__SimCustomer__Seq__3E730AF3]  DEFAULT ((0)) FOR [Seq]
GO
ALTER TABLE [dbo].[tbl_SimCustomer] ADD  CONSTRAINT [DF__SimCustom__FlagS__3F672F2C]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_SimCustomer] ADD  CONSTRAINT [DF_SimCustomer_oldCode]  DEFAULT ('') FOR [oldCode]
GO
ALTER TABLE [dbo].[tbl_SimOrder] ADD  CONSTRAINT [DF_SimOrder_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_SimOrder] ADD  CONSTRAINT [DF__SimOrder__FlagSe__3AA27A0F]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_SimOrder] ADD  CONSTRAINT [DF_SimOrder_RefLoadNo]  DEFAULT ('') FOR [RefLoadNo]
GO
ALTER TABLE [dbo].[tbl_SimOrder] ADD  CONSTRAINT [DF_SimOrder_InvNo]  DEFAULT ('') FOR [InvNo]
GO
ALTER TABLE [dbo].[tbl_SimOrderItem] ADD  CONSTRAINT [DF__SimOrderI__FlagS__3C8AC281]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TargetDetail] ADD  CONSTRAINT [DF_TargetDetail_AutoID]  DEFAULT ((0)) FOR [AutoID]
GO
ALTER TABLE [dbo].[tbl_TargetDetail] ADD  CONSTRAINT [DF_TargetDetail_ProductSubGroupID]  DEFAULT ((0)) FOR [ProductSubGroupID]
GO
ALTER TABLE [dbo].[tbl_TargetDetail] ADD  CONSTRAINT [DF_TargetDetail_PricePerCarton]  DEFAULT ((0)) FOR [ProductSubGroupPrice]
GO
ALTER TABLE [dbo].[tbl_TargetDetail] ADD  CONSTRAINT [DF_TargetDetail_TargetQty]  DEFAULT ((0)) FOR [TargetQty]
GO
ALTER TABLE [dbo].[tbl_TargetDetail] ADD  CONSTRAINT [DF_TargetDetail_TargetBaht]  DEFAULT ((0)) FOR [TargetBaht]
GO
ALTER TABLE [dbo].[tbl_TargetDetail] ADD  CONSTRAINT [DF_TargetDetail_TargetWeight]  DEFAULT ((0.0)) FOR [TargetWeight]
GO
ALTER TABLE [dbo].[tbl_TargetDetail] ADD  CONSTRAINT [DF_TargetDetail_TargetValue1]  DEFAULT ((0.0)) FOR [TargetValue1]
GO
ALTER TABLE [dbo].[tbl_TargetDetail] ADD  CONSTRAINT [DF_TargetDetail_TargetValue2]  DEFAULT ((0.0)) FOR [TargetValue2]
GO
ALTER TABLE [dbo].[tbl_TargetDetail] ADD  CONSTRAINT [DF_TargetDetail_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_TargetDetail] ADD  CONSTRAINT [DF_TargetDetail_CrUser]  DEFAULT ('') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_TargetDetail] ADD  CONSTRAINT [DF_TargetDetail_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TargetDetail] ADD  CONSTRAINT [DF_TargetDetail_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TargetMaster] ADD  CONSTRAINT [DF_TargetMaster_VanQty]  DEFAULT ((0)) FOR [VanQty]
GO
ALTER TABLE [dbo].[tbl_TargetMaster] ADD  CONSTRAINT [DF_TargetMaster_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_TargetMaster] ADD  CONSTRAINT [DF_TargetMaster_CrUser]  DEFAULT ('') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_TargetMaster] ADD  CONSTRAINT [DF_TargetMaster_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TargetMaster] ADD  CONSTRAINT [DF_TargetMaster_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_CustomerRefCode]  DEFAULT ('') FOR [CustomerRefCode]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_Seq]  DEFAULT ((0)) FOR [Seq]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_CustomerTypeID]  DEFAULT ((0)) FOR [CustomerTypeID]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_CreditDay]  DEFAULT ((0)) FOR [CreditDay]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_VatType]  DEFAULT ((1)) FOR [VatType]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_VatRate]  DEFAULT ((7)) FOR [VatRate]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_DiscountType]  DEFAULT ('N') FOR [DiscountType]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_Discount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_PriceGroupID]  DEFAULT ((1)) FOR [PriceGroupID]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_WHID]  DEFAULT ((0)) FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_ShopTypeID]  DEFAULT ((0)) FOR [ShopTypeID]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_FlagLoyalty]  DEFAULT ((0)) FOR [FlagMember]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_NetPoint]  DEFAULT ((0)) FOR [NetPoint]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_CustomerSAPCode]  DEFAULT ('') FOR [CustomerSAPCode]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_IsNewMember_1]  DEFAULT ((0)) FOR [IsNewMember]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_FlagNew_1]  DEFAULT ((0)) FOR [FlagNew]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_FlagEdit_1]  DEFAULT ((0)) FOR [FlagEdit]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer] ADD  CONSTRAINT [DF_TL_ArCustomer_PromotionVanID]  DEFAULT ((0)) FOR [PromotionVanID]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_CustomerRefCode]  DEFAULT ('') FOR [CustomerRefCode]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_Seq]  DEFAULT ((0)) FOR [Seq]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_CustomerTypeID]  DEFAULT ((0)) FOR [CustomerTypeID]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_CreditDay]  DEFAULT ((0)) FOR [CreditDay]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_VatType]  DEFAULT ((1)) FOR [VatType]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_VatRate]  DEFAULT ((7)) FOR [VatRate]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_DiscountType]  DEFAULT ('N') FOR [DiscountType]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_Discount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_PriceGroupID]  DEFAULT ((1)) FOR [PriceGroupID]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_WHID]  DEFAULT ((0)) FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_ShopTypeID]  DEFAULT ((0)) FOR [ShopTypeID]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_FlagLoyalty]  DEFAULT ((0)) FOR [FlagMember]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_NetPoint]  DEFAULT ((0)) FOR [NetPoint]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_CustomerSAPCode]  DEFAULT ('') FOR [CustomerSAPCode]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_IsNewMember_1]  DEFAULT ((0)) FOR [IsNewMember]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_FlagNew_1]  DEFAULT ((0)) FOR [FlagNew]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomer_Test] ADD  CONSTRAINT [DF_TL_ArCustomer_Test_FlagEdit_1]  DEFAULT ((0)) FOR [FlagEdit]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomerShelf] ADD  CONSTRAINT [DF_TL_ArCustomerShelf_ShelfCode]  DEFAULT ('') FOR [ShelfID]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomerShelf] ADD  CONSTRAINT [DF_TL_ArCustomerShelf_WHID]  DEFAULT ((0)) FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomerShelf] ADD  CONSTRAINT [DF_TL_ArCustomerShelf_FlagNew]  DEFAULT ((0)) FOR [FlagNew]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomerShelf] ADD  CONSTRAINT [DF_TL_ArCustomerShelf_FlagEdit]  DEFAULT ((0)) FOR [FlagEdit]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomerShelf] ADD  CONSTRAINT [DF_TL_ArCustomerShelf_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomerShelf] ADD  CONSTRAINT [DF_TL_ArCustomerShelf_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TL_ArCustomerShelf] ADD  CONSTRAINT [DF_TL_ArCustomerShelf_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_OrderQty]  DEFAULT ((0.00)) FOR [OrderQty]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_ReceivedQty]  DEFAULT ((0.00)) FOR [ReceivedQty]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_RejectedQty]  DEFAULT ((0)) FOR [RejectedQty]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_StockedQty]  DEFAULT ((0)) FOR [StockedQty]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_DiscountType]  DEFAULT ('N') FOR [LineDiscountType]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_LineDiscountRate]  DEFAULT ((0)) FOR [LineDiscountRate]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_LineDiscount]  DEFAULT ((0)) FOR [LineDiscount]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_CustType]  DEFAULT ('') FOR [CustType]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_UnitComPrice]  DEFAULT ((0)) FOR [UnitComPrice]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_LineComTotal]  DEFAULT ((0)) FOR [LineComTotal]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_LineRemark]  DEFAULT ('') FOR [LineRemark]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_FreeQty]  DEFAULT ((0)) FOR [FreeQty]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_FreeUom]  DEFAULT ((0)) FOR [FreeUom]
GO
ALTER TABLE [dbo].[tbl_TL_PODetail] ADD  CONSTRAINT [DF_TL_PODetail_FreeUnit]  DEFAULT ((0)) FOR [FreeUnit]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_RevisionNumber]  DEFAULT ((0)) FOR [RevisionNo]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_WHID]  DEFAULT ((0)) FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_SaleEmpID]  DEFAULT ((0)) FOR [SaleEmpID]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_SalAreaID]  DEFAULT ((0)) FOR [SalAreaID]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_CustomerID]  DEFAULT ((0)) FOR [CustomerID]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_CustPONo]  DEFAULT ('') FOR [CustPONo]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_Freight]  DEFAULT ((0.00)) FOR [Freight]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_DiscountType]  DEFAULT ('') FOR [DiscountType]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_TotalDue]  DEFAULT ((0.00)) FOR [TotalDue]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_PayType]  DEFAULT ((0)) FOR [PayType]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_CrUser]  DEFAULT ('@admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_OldTotalCom]  DEFAULT ((0)) FOR [OldTotalCom]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_TotalCom_1]  DEFAULT ((0)) FOR [TotalCom]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_CNType]  DEFAULT ((0)) FOR [CNType]
GO
ALTER TABLE [dbo].[tbl_TL_POMaster] ADD  CONSTRAINT [DF_TL_POMaster_DiscountRate_1]  DEFAULT ((0.0)) FOR [DiscountRate]
GO
ALTER TABLE [dbo].[tbl_TL_PRDetail] ADD  CONSTRAINT [TL_PRDetail_RejectedQty]  DEFAULT ((0)) FOR [SendQty]
GO
ALTER TABLE [dbo].[tbl_TL_PRDetail] ADD  CONSTRAINT [TL_PRDetail_ReceivedQty]  DEFAULT ((0.00)) FOR [ReceivedQty]
GO
ALTER TABLE [dbo].[tbl_TL_PRDetail] ADD  CONSTRAINT [TL_PRDetail_RejectedQty_1]  DEFAULT ((0)) FOR [RejectedQty]
GO
ALTER TABLE [dbo].[tbl_TL_PRDetail] ADD  CONSTRAINT [TL_PRDetail_StockedQty]  DEFAULT ((0)) FOR [StockedQty]
GO
ALTER TABLE [dbo].[tbl_TL_PRDetail] ADD  CONSTRAINT [DF_TL_PRDetail_UnitCost]  DEFAULT ((0)) FOR [UnitCost]
GO
ALTER TABLE [dbo].[tbl_TL_PRDetail] ADD  CONSTRAINT [DF_TL_PRDetail_UnitPrice]  DEFAULT ((0)) FOR [UnitPrice]
GO
ALTER TABLE [dbo].[tbl_TL_PRDetail] ADD  CONSTRAINT [TL_PRDetail_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_TL_PRDetail] ADD  CONSTRAINT [TL_PRDetail_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_TL_PRDetail] ADD  CONSTRAINT [TL_PRDetail_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TL_PRDetail] ADD  CONSTRAINT [DF_TL_PRDetail_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TL_PRMaster] ADD  CONSTRAINT [DF_TL_PRMaster_RevisionNo]  DEFAULT ((0)) FOR [RevisionNo]
GO
ALTER TABLE [dbo].[tbl_TL_PRMaster] ADD  CONSTRAINT [DF_TL_PRMaster_WHID]  DEFAULT ((0)) FOR [FromWHID]
GO
ALTER TABLE [dbo].[tbl_TL_PRMaster] ADD  CONSTRAINT [DF_TL_PRMaster_SalAreaID]  DEFAULT ((0)) FOR [SalAreaID]
GO
ALTER TABLE [dbo].[tbl_TL_PRMaster] ADD  CONSTRAINT [DF_TL_TransferMaster_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TL_PRMaster] ADD  CONSTRAINT [DF_TL_PRMaster_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TL_PromotionCustomer] ADD  CONSTRAINT [DF_TL_PromotionCustomer_RefCode]  DEFAULT ('') FOR [ShelfID]
GO
ALTER TABLE [dbo].[tbl_TL_PromotionCustomer] ADD  CONSTRAINT [DF_TL_PromotionCustomer_WHID]  DEFAULT ((0)) FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_TL_PromotionCustomer] ADD  CONSTRAINT [DF_TL_PromotionCustomer_FlagNew]  DEFAULT ((0)) FOR [FlagNew]
GO
ALTER TABLE [dbo].[tbl_TL_PromotionCustomer] ADD  CONSTRAINT [DF_TL_PromotionCustomer_FlagEdit]  DEFAULT ((0)) FOR [FlagEdit]
GO
ALTER TABLE [dbo].[tbl_TL_PromotionCustomer] ADD  CONSTRAINT [DF_TL_PromotionCustomer_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_TL_PromotionCustomer] ADD  CONSTRAINT [DF_TL_PromotionCustomer_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TL_PromotionCustomer] ADD  CONSTRAINT [DF_TL_PromotionCustomer_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TL_Visit] ADD  CONSTRAINT [DF_TL_Visit_VisitStatus]  DEFAULT ((0)) FOR [VisitStatus]
GO
ALTER TABLE [dbo].[tbl_TL_Visit] ADD  CONSTRAINT [DF_TL_Visit_CauseID]  DEFAULT ((0)) FOR [CauseID]
GO
ALTER TABLE [dbo].[tbl_TL_Visit] ADD  CONSTRAINT [DF_TL_Visit_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_TL_Visit] ADD  CONSTRAINT [DF_TL_Visit_CrUser]  DEFAULT ('Admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_TL_Visit] ADD  CONSTRAINT [DF_TL_Visit_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TL_Visit] ADD  CONSTRAINT [DF_TL_Visit_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TL_VisitStock] ADD  CONSTRAINT [DF_TL_VisitStock_Qty]  DEFAULT ((0.0)) FOR [Qty]
GO
ALTER TABLE [dbo].[tbl_TL_VisitStock] ADD  CONSTRAINT [DF_TL_VisitStock_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_TL_VisitStock] ADD  CONSTRAINT [DF_TL_VisitStock_CrUser]  DEFAULT ('Admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_TL_VisitStock] ADD  CONSTRAINT [DF_TL_VisitStock_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TL_VisitStock] ADD  CONSTRAINT [DF_TL_VisitStock_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TLMaster] ADD  CONSTRAINT [DF_TLMaster_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TLMaster] ADD  CONSTRAINT [DF_TLMaster_FlagRecieve]  DEFAULT ((0)) FOR [FlagRecieve]
GO
ALTER TABLE [dbo].[tbl_TmpPRDetail] ADD  CONSTRAINT [DF_TmpPRDetail_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_TmpPRDetail] ADD  CONSTRAINT [DF_TmpPRDetail_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_TmpPRDetail] ADD  CONSTRAINT [DF_TmpPRDetail_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TmpPRDetail] ADD  CONSTRAINT [DF_TmpPRDetail_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TRDetail] ADD  CONSTRAINT [DF_TRDetail_RejectedQty]  DEFAULT ((0)) FOR [SendQty]
GO
ALTER TABLE [dbo].[tbl_TRDetail] ADD  CONSTRAINT [DF_TRDetail_ReceivedQty]  DEFAULT ((0.00)) FOR [ReceivedQty]
GO
ALTER TABLE [dbo].[tbl_TRDetail] ADD  CONSTRAINT [DF_TRDetail_RejectedQty_1]  DEFAULT ((0)) FOR [RejectedQty]
GO
ALTER TABLE [dbo].[tbl_TRDetail] ADD  CONSTRAINT [DF_TRDetail_StockedQty]  DEFAULT ((0)) FOR [StockedQty]
GO
ALTER TABLE [dbo].[tbl_TRDetail] ADD  CONSTRAINT [DF_TRDetail_UnitCost]  DEFAULT ((0)) FOR [UnitCost]
GO
ALTER TABLE [dbo].[tbl_TRDetail] ADD  CONSTRAINT [DF_TRDetail_UnitPrice]  DEFAULT ((0)) FOR [UnitPrice]
GO
ALTER TABLE [dbo].[tbl_TRDetail] ADD  CONSTRAINT [DF_TRDetail_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_TRDetail] ADD  CONSTRAINT [DF_TRDetail_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_TRDetail] ADD  CONSTRAINT [DF_TRDetail_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TRDetail] ADD  CONSTRAINT [DF_TRDetail_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TRMaster] ADD  CONSTRAINT [DF_TRMaster_RevisionNo]  DEFAULT ((0)) FOR [RevisionNo]
GO
ALTER TABLE [dbo].[tbl_TRMaster] ADD  CONSTRAINT [DF_TRMaster_WHID]  DEFAULT ((0)) FOR [FromWHID]
GO
ALTER TABLE [dbo].[tbl_TRMaster] ADD  CONSTRAINT [DF_TRMaster_SalAreaID]  DEFAULT ((0)) FOR [SalAreaID]
GO
ALTER TABLE [dbo].[tbl_TRMaster] ADD  CONSTRAINT [DF_TRMaster_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_TRMaster] ADD  CONSTRAINT [DF_TRMaster_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_TRMaster] ADD  CONSTRAINT [DF_TRMaster_FromWHID1_1]  DEFAULT ('') FOR [FromWHCode]
GO
ALTER TABLE [dbo].[tbl_TRMaster] ADD  CONSTRAINT [DF_TRMaster_FromWHID1]  DEFAULT ('') FOR [FromLocCode]
GO
ALTER TABLE [dbo].[tbl_TRMaster] ADD  CONSTRAINT [DF_TRMaster_FromWHCode1]  DEFAULT ('') FOR [ToWHCode]
GO
ALTER TABLE [dbo].[tbl_TRMaster] ADD  CONSTRAINT [DF_TRMaster_ToLocID]  DEFAULT ('') FOR [ToLocCode]
GO
ALTER TABLE [dbo].[tbl_VanType] ADD  CONSTRAINT [DF_VanType_Seq]  DEFAULT ((0)) FOR [Seq]
GO
ALTER TABLE [dbo].[tbl_VanType] ADD  CONSTRAINT [DF_VanType_WHType]  DEFAULT ((0)) FOR [WHType]
GO
ALTER TABLE [dbo].[tbl_VanType] ADD  CONSTRAINT [DF_VanType_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_VanType] ADD  CONSTRAINT [DF_VanType_EdDate]  DEFAULT (getdate()) FOR [EdDate]
GO
ALTER TABLE [dbo].[tbl_VanType] ADD  CONSTRAINT [DF_VanType_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_Visit] ADD  CONSTRAINT [DF_VisitCustomer_VisitStatus]  DEFAULT ((0)) FOR [VisitStatus]
GO
ALTER TABLE [dbo].[tbl_Visit] ADD  CONSTRAINT [DF_VisitCustomer_CauseID]  DEFAULT ((0)) FOR [CauseID]
GO
ALTER TABLE [dbo].[tbl_Visit] ADD  CONSTRAINT [DF_VisitCustomer_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_Visit] ADD  CONSTRAINT [DF_VisitCustomer_CrUser]  DEFAULT ('Admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_Visit] ADD  CONSTRAINT [DF_VisitCustomer_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_Visit] ADD  CONSTRAINT [DF_VisitCustomer_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_VisitStock] ADD  CONSTRAINT [DF_VisitStock_Qty]  DEFAULT ((0.0)) FOR [Qty]
GO
ALTER TABLE [dbo].[tbl_VisitStock] ADD  CONSTRAINT [DF_VisitStock_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_VisitStock] ADD  CONSTRAINT [DF_VisitStock_CrUser]  DEFAULT ('Admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_VisitStock] ADD  CONSTRAINT [DF_VisitStock_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_VisitStock] ADD  CONSTRAINT [DF_VisitStock_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_VMIDetail] ADD  CONSTRAINT [DF_VMIDetail_InternalID]  DEFAULT ((0)) FOR [InternalID]
GO
ALTER TABLE [dbo].[tbl_VMIDetail] ADD  CONSTRAINT [DF_VMIDetail_ProductID]  DEFAULT ('') FOR [ProductID]
GO
ALTER TABLE [dbo].[tbl_VMIDetail] ADD  CONSTRAINT [DF_VMIDetail_AvgSale1]  DEFAULT ((0)) FOR [SumSale]
GO
ALTER TABLE [dbo].[tbl_VMIDetail] ADD  CONSTRAINT [DF_VMIDetail_AvgSale]  DEFAULT ((0)) FOR [AvgSale]
GO
ALTER TABLE [dbo].[tbl_VMIDetail] ADD  CONSTRAINT [DF_VMIDetail_SaleFactor]  DEFAULT ((0)) FOR [SaleFactor]
GO
ALTER TABLE [dbo].[tbl_VMIDetail] ADD  CONSTRAINT [DF_VMIDetail_SafetyFactor]  DEFAULT ((0)) FOR [SafetyFactor]
GO
ALTER TABLE [dbo].[tbl_VMIDetail] ADD  CONSTRAINT [DF_VMIDetail_Stock]  DEFAULT ((0)) FOR [Stock]
GO
ALTER TABLE [dbo].[tbl_VMIDetail] ADD  CONSTRAINT [DF_Table_1_CalSuggest]  DEFAULT ((0)) FOR [SaleForecast]
GO
ALTER TABLE [dbo].[tbl_VMIDetail] ADD  CONSTRAINT [DF_VMIDetail_RealSuggest]  DEFAULT ((0)) FOR [Suggest]
GO
ALTER TABLE [dbo].[tbl_VMIDetail] ADD  CONSTRAINT [DF_VMIDetail_Withdraw]  DEFAULT ((0)) FOR [Withdraw]
GO
ALTER TABLE [dbo].[tbl_VMIMaster] ADD  CONSTRAINT [DF_VMIMaster_BranchID]  DEFAULT ('') FOR [BranchID]
GO
ALTER TABLE [dbo].[tbl_VMIMaster] ADD  CONSTRAINT [DF_VMIMaster_WHID]  DEFAULT ('') FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_VMIMaster] ADD  CONSTRAINT [DF_VMIMaster_SaleDate]  DEFAULT (CONVERT([varchar](10),getdate(),(112))) FOR [SaleDate]
GO
ALTER TABLE [dbo].[tbl_VMIMaster] ADD  CONSTRAINT [DF_VMIMaster_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_VMIMaster] ADD  CONSTRAINT [DF_VMIMaster_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_VMIMaster] ADD  CONSTRAINT [DF_VMIMaster_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_VMIMaster] ADD  CONSTRAINT [DF_VMIMaster_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_VMIMaster] ADD  CONSTRAINT [DF_VMIMaster_FlagReceive]  DEFAULT ((0)) FOR [FlagReceive]
GO
ALTER TABLE [dbo].[tbl_VMISafety] ADD  CONSTRAINT [DF_VMISafety_BranchID]  DEFAULT ('') FOR [BranchID]
GO
ALTER TABLE [dbo].[tbl_VMISafety] ADD  CONSTRAINT [DF_VMISafety_WHID]  DEFAULT ('') FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_VMISafety] ADD  CONSTRAINT [DF_VMISafety_SafetyFactor]  DEFAULT ((0)) FOR [SafetyFactor]
GO
ALTER TABLE [dbo].[tbl_VMISafety] ADD  CONSTRAINT [DF_VMISafety_MinSaleVal]  DEFAULT ((0)) FOR [MinSaleVal]
GO
ALTER TABLE [dbo].[tbl_VMISafety] ADD  CONSTRAINT [DF_VMISafety_MaxSaleVal]  DEFAULT ((0)) FOR [MaxSaleVal]
GO
ALTER TABLE [dbo].[tbl_VMISafety] ADD  CONSTRAINT [DF_VMISafety_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_VMISafety] ADD  CONSTRAINT [DF_VMISafety_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_VMISafety] ADD  CONSTRAINT [DF_VMISafety_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_VMISafety] ADD  CONSTRAINT [DF_VMISafety_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_VMISetting] ADD  CONSTRAINT [DF_VMIsetting_BranchID]  DEFAULT ('') FOR [BranchID]
GO
ALTER TABLE [dbo].[tbl_VMISetting] ADD  CONSTRAINT [DF_VMIsetting_WHID]  DEFAULT ('') FOR [WHID]
GO
ALTER TABLE [dbo].[tbl_VMISetting] ADD  CONSTRAINT [DF_VMIsetting_ProductID]  DEFAULT ('') FOR [ProductID]
GO
ALTER TABLE [dbo].[tbl_VMISetting] ADD  CONSTRAINT [DF_VMIsetting_SaleFactor]  DEFAULT ((0)) FOR [SaleFactor]
GO
ALTER TABLE [dbo].[tbl_VMISetting] ADD  CONSTRAINT [DF_VMIsetting_SafetyFactor]  DEFAULT ((0)) FOR [SafetyFactor]
GO
ALTER TABLE [dbo].[tbl_VMISetting] ADD  CONSTRAINT [DF_VMIsetting_UseSafetyFactor]  DEFAULT ((0)) FOR [UseSafetyFactor]
GO
ALTER TABLE [dbo].[tbl_VMISetting] ADD  CONSTRAINT [DF_VMIsetting_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_VMISetting] ADD  CONSTRAINT [DF_VMIsetting_CrUser]  DEFAULT ('admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_VMISetting] ADD  CONSTRAINT [DF_VMIsetting_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_VMISetting] ADD  CONSTRAINT [DF_VMIsetting_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
ALTER TABLE [dbo].[tbl_Zone] ADD  CONSTRAINT [DF_Zone_ZoneID]  DEFAULT ((0)) FOR [ZoneID]
GO
ALTER TABLE [dbo].[tbl_Zone] ADD  CONSTRAINT [DF_Zone_ZoneName]  DEFAULT ('') FOR [ZoneName]
GO
ALTER TABLE [dbo].[tbl_Zone] ADD  CONSTRAINT [DF_Zone_Parent]  DEFAULT ((0)) FOR [Parent]
GO
ALTER TABLE [dbo].[tbl_Zone] ADD  CONSTRAINT [DF_Zone_CrDate]  DEFAULT (getdate()) FOR [CrDate]
GO
ALTER TABLE [dbo].[tbl_Zone] ADD  CONSTRAINT [DF_Zone_CrUser]  DEFAULT ('Admin') FOR [CrUser]
GO
ALTER TABLE [dbo].[tbl_Zone] ADD  CONSTRAINT [DF_Zone_FlagDel]  DEFAULT ((0)) FOR [FlagDel]
GO
ALTER TABLE [dbo].[tbl_Zone] ADD  CONSTRAINT [DF_Zone_FlagSend]  DEFAULT ((0)) FOR [FlagSend]
GO
/****** Object:  StoredProcedure [dbo].[proc_error_logs_I]    Script Date: 31/01/2020 11:17:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Author,,Name>
-- Create date: Create Date,
-- Description:	Description,
-- =============================================
CREATE PROCEDURE [dbo].[proc_error_logs_I]
	@user_code nvarchar(10),
	@form_name nvarchar(150),
	@function_name nvarchar(150),
	@err_desc nvarchar(MAX)

AS
BEGIN
	
	INSERT INTO dbo.error_logs
           (user_code
		   ,form_name
           ,function_name
           ,err_desc
           ,time_log)
     VALUES
           (@user_code
		   ,@form_name
           ,@function_name
           ,@err_desc
           ,GETDATE()
		   )
END


GO
