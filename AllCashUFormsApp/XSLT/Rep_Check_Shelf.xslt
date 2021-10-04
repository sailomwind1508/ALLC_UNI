<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_Check_Shelf_XSLT">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานรายละเอียด Shelf">
				<ss:Table>
					<xsl:apply-templates select="Rep_Check_Shelf_XSLT"/>
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
					font-size: 11pt;
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
						<TD class="stdPageHdr" colspan="17">
							<xsl:value-of select="NewDataSet/Rep_Check_Shelf_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="17">
							รายงานรายละเอียด Shelf
						</TD>
					</TR>
					<TR>
						<TD Colspan="17"></TD>
					</TR>
					<TR>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;">
							Depot :
						</TD>
						<TD class="SearchResultItem" Colspan="3" style="text-align: center;">
							<xsl:value-of select="NewDataSet/Rep_Check_Shelf_XSLT/BranchName" />
						</TD>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							From Date/Month:
						</TD>
						<TD colspan="3" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="NewDataSet/Rep_Check_Shelf_XSLT/MonthFr" />
						</TD>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							To Date/Month:
						</TD>
						<TD colspan="2" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="NewDataSet/Rep_Check_Shelf_XSLT/MonthTo" />
						</TD>
					</TR>
					<TR>
						<TD Colspan="17"></TD>
					</TR>
					<TR>
						<TD CLASS="gridHeader">เดือน</TD>
						<TD CLASS="gridHeader">รหัสลูกค้าในPO</TD>
						<TD CLASS="gridHeader">ชื่อลูกค้าในPO</TD>
						<TD CLASS="gridHeader">วันที่เอกสาร</TD>
						<TD CLASS="gridHeader">แวน</TD>
						<TD CLASS="gridHeader">เลขที่เอกสาร</TD>
						<TD CLASS="gridHeader">ยอดขาย</TD>
						<TD CLASS="gridHeader">สถานะเอกสาร</TD>
						<TD CLASS="gridHeader">ชื่อสินค้า</TD>
						<TD CLASS="gridHeader">รหัสชั้นวางในPO</TD>
						<TD CLASS="gridHeader">รหัสชั้นวาง</TD>
						<TD CLASS="gridHeader">รหัสลูกค้า</TD>
						<TD CLASS="gridHeader">ชื่อลูกค้า</TD>
						<TD CLASS="gridHeader">วันที่เพิ่มชั้นวาง</TD>
						<TD CLASS="gridHeader">วันที่แก้ไขชั้นวาง</TD>
						<TD CLASS="gridHeader">User ที่แก้ไข</TD>
						<TD CLASS="gridHeader">สถานะชั้นวาง</TD>
					</TR>
					<xsl:for-each select="NewDataSet/Rep_Check_Shelf_XSLT">
						<!--<xsl:sort select="concat(DocDate,DocNo)" />-->
						<TR>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="Month"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="CustIDPO"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="CustNamePO"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="DocDate"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="WHID"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="DocNo"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select='format-number(TotalDue,"#,##0.00")'/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="DocStatus"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="ProductFullName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="ShelfIDInPO"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="ShelfIDAterEdit"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="CustIDShelf"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="CustNameShelf"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="CrDateAterEdit"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="EdDateAterEdit"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="EdUser"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="StatusShelf"/>
							</TD>
						</TR >
					</xsl:for-each>
					<TR Class="GroupFooter">
						<TD ColSpan="9">รวมทั้งสิ้น</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='count(/NewDataSet/Rep_Check_Shelf_XSLT/ShelfIDInPO)'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='count(/NewDataSet/Rep_Check_Shelf_XSLT/ShelfIDAterEdit)'/>
						</TD>
					</TR >
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>