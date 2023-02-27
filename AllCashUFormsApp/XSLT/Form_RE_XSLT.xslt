<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Form_RE">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="Form_RE">
				<ss:Table>
					<xsl:apply-templates select="Form_RE"/>
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
						<TD CLASS="gridHeader">No.</TD>
						<TD CLASS="gridHeader">รหัสสินค้า</TD>
						<TD CLASS="gridHeader" width="360">รายการ</TD>
						
						<TD CLASS="gridHeader">จำนวน</TD>
						<TD CLASS="gridHeader">หน่วย</TD>
						
						<TD CLASS="gridHeader">Vat</TD>
						<TD CLASS="gridHeader">หน่วยละ</TD>
						<TD CLASS="gridHeader">ส่วนลด</TD>
						<TD CLASS="gridHeader">จำนวนเงิน</TD>
					</TR>

					<xsl:for-each select="NewDataSet/Form_RE">
						<TR>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="position()"/>
							</TD>
							
							<TD class="SearchResultItem">
								<!--<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>-->
								<xsl:value-of  select="ProductID"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of  select="ProductName"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(ReceivedQty,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="ProductUomName"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(LineVatAmt,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(UnitPrice,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(Discount,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(LineTotal,'###,###.00')"/>
							</TD>
							
						</TR >
					</xsl:for-each>
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>
