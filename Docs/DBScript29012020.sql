USE [DB_ALL_CASH_UNI]
GO
/****** Object:  Table [dbo].[tbl_AdmAuthorize]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_AdmControlList]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_AdmFormList]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_AdmMenuList]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_AdmRoleControl]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_AmtArCustomer]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_AmtArCustomerDetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ApSupplier]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ApSupplierType]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ArCustomer]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ArCustomerShelf]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ArCustomerType]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Banknote]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Branch]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_BranchGroup]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_BranchLog]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_BranchRental]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_BranchSupervisor]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_BranchTerritory]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_BranchWarehouse]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_BranchWarehouseData]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Cause]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_CfgConnection]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_CfgFixedHolidays]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_CfgFloatingHolidays]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_CfgKeyField]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_CfgPosMachine]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_CfgSetting]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Company]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_CostPrice$]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_data$]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_DelArCustomer]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Department]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_DisplayImage]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_DocRunning]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_DocSendUpdate]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_DocSignature]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_DocumentStatus]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_DocumentType]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Employee]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_error_logs]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ErrorLog]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Expense]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_InvMovement]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_InvTransaction]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_InvWarehouse]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_IVDetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_IVMaster]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LtyQuestion]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytAnswer]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytChoice]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytCustTypeReward]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytMember]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytPointBonus]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytPointByProduct]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytPointCycle]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytPointMovement]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytPointType]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytPointTypePerBill]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytRedeem]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytReportGroup]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytReportMenu]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_LytReward]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_MstArea]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_MstCycle]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_MstDistrict]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_MstGrade]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_MstPart]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_MstProvince]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PaidDetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PaidMaster]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PayDetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PayMaster]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PODetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_POMaster]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Position]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PRDetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PreSaleWarehouse]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PriceGroup]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PRMaster]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Product]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductBrand]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductChanged]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductCompany]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductCostHistory]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductCostSupplier]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductFlavour]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductGroup]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductPriceBranch]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductPriceCustomer]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductPriceGroup]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductPriceHistory]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductPromotionBom]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductRemarkReject]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductSubGroup]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductType]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductUom]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ProductUomSet]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Promotion]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PromotionBranch]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PromotionCustomer]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PromotionPack]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PromotionProduct]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PromotionReward]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PromotionVan]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_PromotionVanProduct]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ReportGroup]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ReportMenu]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Roles]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_RouteOnsite]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_SalArea]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_SalAreaDistrict]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_SalAreaVisit]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_SaleBranchDaily]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_SaleBranchSummary]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_SaleExpenseDetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_SaleExpenseMaster]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_SaleYearTarget]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_SendData]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ShopType]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_ShopTypeGroup]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_SimCustomer]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_SimOrder]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_SimOrderItem]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TargetDetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TargetMaster]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TL_ArCustomer]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TL_ArCustomer_Test]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TL_ArCustomerShelf]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TL_DocSignature]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TL_PODetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TL_POMaster]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TL_PRDetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TL_PRMaster]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TL_PromotionCustomer]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TL_Visit]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TL_VisitStock]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TLDetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TLMaster]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TmpPRDetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TRDetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_TRMaster]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Users]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_VanType]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Visit]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_VisitStock]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_VMIDetail]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_VMIMaster]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_VMISafety]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_VMISetting]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  Table [dbo].[tbl_Zone]    Script Date: 29/01/2020 11:12:21 AM ******/
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
/****** Object:  StoredProcedure [dbo].[proc_error_logs_I]    Script Date: 29/01/2020 11:12:21 AM ******/
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
