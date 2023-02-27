<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_Count_Customer_Sales_By_Product_XSLT">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานร้านซื้อแยกตามสินค้า">
				<ss:Table>
					<xsl:apply-templates select="Rep_Count_Customer_Sales_By_Product_XSLT"/>
				</ss:Table>
			</ss:Worksheet>
		</ss:Workbook>
	</xsl:template>

	<xsl:key name="groupWHID" match="NewDataSet/Rep_Count_Customer_Sales_By_Product_XSLT" use="WHID" />
		
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
						<TD class="stdPageHdr" colspan="3">
							<xsl:value-of select="NewDataSet/Rep_Count_Customer_Sales_By_Product_XSLT/CompanyName"/>
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" colspan="3">
							<xsl:value-of select="NewDataSet/Rep_Count_Customer_Sales_By_Product_XSLT/BranchName"/>
						</TD>
					</TR>
					
					<TR>
						<TD class="stdPageHdr" colspan="3">
							รายงานร้านซื้อแยกตามสินค้า
						</TD>
					</TR>
					
					<TR>
						<TD align="left" colspan="3">
							<xsl:value-of select="NewDataSet/Rep_Count_Customer_Sales_By_Product_XSLT/DateRange"/>
						</TD>
					</TR>

					<xsl:for-each select="NewDataSet/Rep_Count_Customer_Sales_By_Product_XSLT[count(. | key('groupWHID', WHID)[1]) = 1]">
						<TR>
							<TD>
								คลัง
							</TD>
							<TD>
								<xsl:value-of select="WHID"/>
							</TD>
						</TR>
						
					<TR>
						<TD CLASS="gridHeader">ลำดับ</TD>
						<TD CLASS="gridHeader">รหัสสินค้า</TD>
						<TD CLASS="gridHeader" width="360">ชื่อสินค้า</TD>
						<TD CLASS="gridHeader">จำนวนร้านค้า</TD>
					</TR>

						<xsl:for-each select="key('groupWHID', WHID)">
							<xsl:sort select="WHID" />
							<TR>
								<TD CLASS="SearchResultItem">
										<xsl:value-of select="position()"/>
									</TD>
								<TD class="SearchResultItem">
									<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
									<xsl:value-of select="ProductID"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of  select="ProductName"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="QtyCust"/>
								</TD>
							</TR >
						</xsl:for-each>
					
					<TR>

					</TR>
					
					</xsl:for-each>

				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>
