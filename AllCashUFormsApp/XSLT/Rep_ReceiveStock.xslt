<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_ReceiveStock_XSLT">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานรับสินค้า">
				<ss:Table>
					<xsl:apply-templates select="Rep_ReceiveStock_XSLT"/>
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
						<TD class="stdPageHdr" colspan="11">
							<xsl:value-of select="NewDataSet/Rep_ReceiveStock_XSLT/CompanyName"/>
						</TD>
					</TR>
					
					<TR>
						<TD class="stdPageHdr" colspan="11">รายงานรับสินค้า</TD>
					</TR>

					<TR>
						
					</TR><!--เว้นบรรทัด-->

					<TR>
						<TD class="gridHeader" align="center">Depot :</TD>
						<TD class="gridHeader" colspan="3" align="center">
							<xsl:value-of select="NewDataSet/Rep_ReceiveStock_XSLT/BranchName"/> (<xsl:value-of select="NewDataSet/Rep_ReceiveStock_XSLT/BranchID"/>)
						</TD>

						<TD class="gridHeader" align="center">Date :</TD>
						<TD class="gridHeader" colspan="3" align="center">
							<xsl:value-of select="NewDataSet/Rep_ReceiveStock_XSLT/DateFr"/> - <xsl:value-of select="NewDataSet/Rep_ReceiveStock_XSLT/DateTo"/>
						</TD>
					</TR>

					<TR>

					</TR><!--เว้นบรรทัด-->

					<TR>
						<TD CLASS="gridHeader">วันที่รับ</TD>
						<TD CLASS="gridHeader">เลขที่เอกสาร</TD>
						<TD CLASS="gridHeader" width="180px">เลขที่ใบกำกับภาษี</TD>
						<TD CLASS="gridHeader" width="125px">วันที่ใบกำกับภาษี</TD>
						<TD CLASS="gridHeader" width="80px">รหัสเจ้าหนี้</TD>
						<TD CLASS="gridHeader">ชื่อเจ้าหนี้</TD>
						<TD CLASS="gridHeader" width="95px">มูลค่ารวมภาษี</TD>
						<TD CLASS="gridHeader" width="100px">มูลค่ายกเว้นภาษี</TD>
						<TD CLASS="gridHeader" width="90px">รวมเป็นเงิน</TD>
						<TD CLASS="gridHeader" width="90px">มูลค่าภาษี</TD>
						<TD CLASS="gridHeader" width="110px">รวมเป็นเงินทั้งสิ้น</TD>
						<TD CLASS="gridHeader" width="100px">สถานะเอกสาร</TD>
					</TR><!--ชื่อคอลัมน์-->

					<xsl:for-each select="NewDataSet/Rep_ReceiveStock_XSLT">
						<xsl:sort select="DocNo" />
						<TR>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="DocDate"/>
							</TD>

							<TD class="SearchResultItem" width="180px">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="DocNo"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="CustInvNo"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="CustInvDate"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:value-of  select="SupplierID"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="SuppName"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:value-of  select="format-number(IncVat,'#,##0.00')"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:value-of  select="format-number(ExcVat,'#,##0.00')"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:value-of  select="format-number(Amount,'#,##0.00')"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:value-of  select="format-number(VatAmt,'#,##0.00')"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:value-of  select="format-number(TotalDue,'#,##0.00')"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="DocStatus"/>
							</TD>
							
							
						</TR>
					</xsl:for-each>
					
					<TR Class="GroupFooter">
						<TD ColSpan="6" align="right">รวมทั้งหมด</TD>
						
						<TD Class="subTotals" align="right">
							<xsl:value-of select="format-number(sum(/NewDataSet/Rep_ReceiveStock_XSLT/IncVat),'#,##0.00')"/>
						</TD>
						
						<TD Class="subTotals" align="right">
							<xsl:value-of select="format-number(sum(/NewDataSet/Rep_ReceiveStock_XSLT/ExcVat),'#,##0.00')"/>
						</TD>
						
						<TD Class="subTotals" align="right">
							<xsl:value-of select="format-number(sum(/NewDataSet/Rep_ReceiveStock_XSLT/Amount),'#,##0.00')"/>
						</TD>
						
						<TD Class="subTotals" align="right">
							<xsl:value-of select="format-number(sum(/NewDataSet/Rep_ReceiveStock_XSLT/VatAmt),'#,##0.00')"/>
						</TD>
						
						<TD Class="subTotals" align="right">
							<xsl:value-of select="format-number(sum(/NewDataSet/Rep_ReceiveStock_XSLT/TotalDue),'#,##0.00')"/>
						</TD>
						
						<TD Class="subTotals" align="right">
							
						</TD>
					</TR><!--รวมทั้งหมด-->
				
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
	
	
	
	
</xsl:stylesheet>
