<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_DSR_EffectiveCall_SKU_XSLT">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานภาษีขายเต็มรูป">
				<ss:Table>
					<xsl:apply-templates select="Rep_DSR_EffectiveCall_SKU_XSLT"/>
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
						<TD class="stdPageHdr" colspan="6" text-align="left">
							<xsl:value-of select="/NewDataSet/Rep_DSR_EffectiveCall_SKU_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" alight ="left" colspan="6">รายงาน Eff.Call (SKU) </TD>
					</TR>
					<TR>
						<TD Colspan="7"></TD>
					</TR>
					<TR>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;">
							BB :
						</TD>
						<TD class="SearchResultItem" Colspan="3" style="text-align: center;">
							<xsl:value-of select="/NewDataSet/Rep_DSR_EffectiveCall_SKU_XSLT/BranchName" />
						</TD>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;">
							วันที่ :
						</TD>
						<TD colspan="3" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="/NewDataSet/Rep_DSR_EffectiveCall_SKU_XSLT/HDate" />
						</TD>
					</TR>
					
					<TR>
						<TD Colspan="7"></TD>
					</TR>
						
					<TR>
						<TD CLASS="gridHeader">ลำดับ</TD>
						<TD CLASS="gridHeader">วันที่เอกสาร</TD>
						<TD CLASS="gridHeader">เดือน</TD>
						<TD CLASS="gridHeader" WIDTH="200">ชื่อพนักงาน</TD>
						<TD CLASS="gridHeader">TotVisited</TD>
						<TD CLASS="gridHeader">TotBought</TD>
						<TD CLASS="gridHeader">รหัสสินค้า</TD>
						<TD CLASS="gridHeader">ชื่อสินค้า</TD>
						<TD CLASS="gridHeader">CustList</TD>
						<TD CLASS="gridHeader">Bought</TD>
						<TD CLASS="gridHeader">CarQty</TD>
						<TD CLASS="gridHeader">CarQtyStr</TD>
						<TD CLASS="gridHeader">PckQty</TD>
						<TD CLASS="gridHeader">PckQtyStr</TD>
						<TD CLASS="gridHeader">ราคาต่อหีบ</TD>
						<TD CLASS="gridHeader">ราคาต่อแพ็ค</TD>
						<TD CLASS="gridHeader">ราคารวม</TD>
					</TR>
					
					<xsl:for-each select="NewDataSet/Rep_DSR_EffectiveCall_SKU_XSLT">
						<TR>
							<TD class="SearchResultItem">
								<xsl:value-of  select="position()"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of  select="DocDate"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of  select="Round"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="EmpName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="TotVisited"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="TotBought"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="ProductID"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="ProductName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="CustList"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="Bought"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="CarQty"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="CarQtyStr"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="PckQty"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="PckQtyStr"/>
							</TD>	
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(CarUnitPrice,'#,##0.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(PckUnitPrice,'#,##0.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(LineTotal,'#,##0.00')"/>
							</TD>
						</TR >
					</xsl:for-each>
					
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>