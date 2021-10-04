<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_V_Sales">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานสรุปยอดขาย แยกตามเอกสาร">
				<ss:Table>
					<xsl:apply-templates select="Rep_Sales_By_Doc_XSLT"/>
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
					text-align: center;
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
						<TD class="stdPageHdr" colspan="11">
							<xsl:value-of select="NewDataSet/Rep_Sales_By_Doc_XSLT/CompanyName"/>
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" colspan="11">รายงานสรุปยอดขาย แยกตามเอกสาร</TD>
					</TR>
					<TR>
						<TD class="SearchKey">พิมพ์วันที่</TD>
						<TD class="SearchValue" Colspan="1">
							<xsl:value-of select="(NewDataSet/Rep_Sales_By_Doc_XSLT/DateFr)"/>
						</TD>
						<TD class="SearchKey">ถึง</TD>
						<TD class="SearchValue" Colspan="1">
							<xsl:value-of select="(NewDataSet/Rep_Sales_By_Doc_XSLT/DateTo)"/>
						</TD>
					</TR>
					<TR>
						<TD class="SearchKey">สถานะ</TD>
						<TD class="SearchValue" Colspan="1">
							<xsl:value-of select="NewDataSet/Rep_Sales_By_Doc_XSLT/Status"/>
						</TD>
					</TR>
					<TR>
						<TD CLASS="gridHeader">No</TD>
						<TD CLASS="gridHeader">วันที่</TD>
						<TD CLASS="gridHeader">เลขที่เอกสาร</TD>
						<TD CLASS="gridHeader">รหัสลูกค้า</TD>
						<TD CLASS="gridHeader">ชื่อลูกค้า</TD>
						<TD CLASS="gridHeader">จำนวนชิ้น</TD>
						<TD CLASS="gridHeader">มูลค่าสินค้า</TD>
						<TD CLASS="gridHeader">ภาษีมูลค่าเพิ่ม</TD>
						<TD CLASS="gridHeader">มูลค่าสุทธิ</TD>
						<TD CLASS="gridHeader" WIDTH="200">จำนวนรายการ</TD>
						<TD CLASS="gridHeader">พนักงานขาย</TD>
						<TD CLASS="gridHeader">สถานะ</TD>
					</TR>
					<xsl:for-each select="NewDataSet/Rep_Sales_By_Doc_XSLT">
						<TR>
							<TD class="SearchResultItem">
								<xsl:value-of  select="No"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="DocDate"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="DocNo"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="CustomerID"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="CustName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="ReceivedQty"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(Total,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(VatAmt,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(NetAmt,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="Count"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="EmpName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="Status"/>
							</TD>
						</TR >
					</xsl:for-each>
					<TR Class="GroupFooter">
						<TD ColSpan="4" align="right">รวมทั้งหมด</TD>
						<TD>เอกสารใช้งาน
							<xsl:value-of select='format-number(count(/NewDataSet/Rep_Sales_By_Doc_XSLT/No),"#,##0")'/> รายการ
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Sales_By_Doc_XSLT/ReceivedQty),"###,###.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Sales_By_Doc_XSLT/Total),"###,###.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Sales_By_Doc_XSLT/VatAmt),"###,###.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Sales_By_Doc_XSLT/NetAmt),"###,###.00")'/>
						</TD>
					</TR>
					
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
	
</xsl:stylesheet>
