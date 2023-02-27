<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="proc_GetCustomer_DuplicateName">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานชื่อร้านค้าซ้ำ">
				<ss:Table>
					<xsl:apply-templates select="proc_GetCustomer_DuplicateName"/>
				</ss:Table>
			</ss:Worksheet>
		</ss:Workbook>
	</xsl:template>

	<xsl:template match="/">
		<HTML>
			<HEAD>
				<STYLE>
					.content{
					font-family:Arial;
					}
					.HDR { background-color:bisque;font-weight:bold }
					.stdPVTblLCell {
					background-color: #00a7e7;
					color: white;
					font-weight: bold;
					text-align: left;
					padding-left: 4px;
					padding-top: 4px;
					padding-bottom: 4px;
					width: 100%;
					font-size: 9pt;
					}
					.stdPageHdr {
					color: DarkBlue;
					font-weight: bold;
					font-style:Regular;
					text-align: left;
					padding-left: 4px;
					padding-top: 4px;
					padding-bottom: 4px;
					width: 100%;
					font-size: 14pt;
					}
					.gridHeader {
					background-color: #DDEEFF;
					color: DarkBlue;
					font-size: 9pt;
					font-weight: bold;
					vertical-align:middle;
					text-align:center;
					border: solid thin Black;
					}
					.SearchHeader {
					color: DarkBlue;
					font-size: 9pt;
					font-weight: bold;
					}
					.SearchKey {
					color: DarkBlue;
					font-size: 9pt;
					vertical-align:middle;
					text-align:right;
					}
					.SearchValue
					{
					color: Black;
					font-size: 9pt;
					font-weight: bold;
					vertical-align:middle;
					text-align:left;
					}
					.SearchResultHeader {
					background-color: #9BE2F9;
					color: DarkBlue;
					font-weight: bold;
					font-size: 9pt;
					}
					.SearchResultItem {
					background-color: #FFFFFF;
					color: Black;
					border: solid thin Black;
					font-size: 9pt;
					}
					.SearchResultAltItem {
					background-color: #99CCFF;
					color: Black;
					font-size: 9pt;
					border: solid thin Black;
					}
					.GroupHeader{
					background-color: #AFD118;
					color: Black;
					font-weight: bold;
					font-style:Regular;
					text-align: left;
					padding-left: 1px;
					padding-top: 1px;
					padding-bottom: 1px;
					width: 100%;
					font-size: 9pt;
					}
					.GroupFooter{
					color: Black;
					font-weight: bold;
					font-style:Regular;
					text-align: left;
					padding-left: 1px;
					padding-top: 1px;
					padding-bottom: 1px;
					width: 100%;
					font-size: 9pt;
					}
					.GroupTotal{text-align:right;}
					.subTotals {border-bottom:1px solid black}
				</STYLE>
			</HEAD>
			<BODY class="content">
				<TABLE>
					<TR>
						<TD class="stdPageHdr" colspan="4">รายงานชื่อร้านค้าซ้ำ</TD>
						<TR>
							<TD Colspan="2" align="right">ศูนย์ :</TD>
							<TD align="left">
								<xsl:value-of select="NewDataSet/proc_GetCustomer_DuplicateName/BranchName"/>
							</TD>
						</TR>
					</TR>

					<TR>
						<TD CLASS="gridHeader">No.</TD>
							<TD CLASS="gridHeader">ตลาด</TD>
							<TD CLASS="gridHeader">รหัสลูกค้า</TD>
							<TD CLASS="gridHeader">ประเภทร้านค้า</TD>
							<TD CLASS="gridHeader">ชื่อลูกค้า</TD>
							<TD CLASS="gridHeader">ที่อยู่</TD>
							<TD CLASS="gridHeader">ตำบล</TD>
							<TD CLASS="gridHeader">อำเภอ</TD>
							<TD CLASS="gridHeader">จังหวัด</TD>
							<TD CLASS="gridHeader">รหัสไปรษณีย์</TD>
							<TD CLASS="gridHeader">โทรศัพท์</TD>
							<TD CLASS="gridHeader">Shelf No.</TD>
							<TD CLASS="gridHeader">Latitude</TD>
							<TD CLASS="gridHeader">Longitude</TD>
					</TR>

					<xsl:for-each select="NewDataSet/proc_GetCustomer_DuplicateName">
						<TR>
							<TD class="SearchResultItem">
								<xsl:value-of  select="position()"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:value-of select="Seq"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="CustomerID"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="ShopTypeName"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="CustName"/>
							</TD>

							<TD class="SearchResultItem" width="150">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="BillTo"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="DistrictName"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="AreaName"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:value-of select="ProvinceName"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="PostalCode"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="Telephone"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="ShelfID"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="Latitude"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="Longitude"/>
							</TD>
						</TR >
					</xsl:for-each>
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>
