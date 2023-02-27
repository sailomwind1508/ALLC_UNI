<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="ProductMovement_Summary">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานสินค้าเคลื่อนไหว(สรุป)">
				<ss:Table>
					<xsl:apply-templates select="ProductMovement_Summary"/>
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
				<xsl:for-each select="NewDataSet/Summary">
					<Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet" xmlns:html="http://www.w3.org/TR/REC-html40">
						<Worksheet ss:Name="Sheet1">
						</Worksheet>
					</Workbook>
				</xsl:for-each>

				<TABLE>

					<TR>
						<TD class="stdPageHdr" colspan="10">
							<xsl:value-of select="NewDataSet/Summary/BranchName"/>
						</TD>
					</TR>

					<TR>
						<TD class="stdPageHdr" colspan="10">รายงานสินค้าเคลื่อนไหว(สรุป)</TD>
					</TR>

					<TR>
						<TD class="SearchKey" colspan="2">วันที่ :</TD>
						<TD class="SearchValue" Colspan="1">
							<xsl:value-of select="NewDataSet/Summary/DateFr"/>
						</TD>

						<TD class="SearchKey">ถึง :</TD>
						<TD class="SearchValue" Colspan="1">
							<xsl:value-of select="NewDataSet/Summary/DateTo"/>
						</TD>
					</TR>

					<TR>
						<TD class="SearchKey" colspan="2">คลัง :</TD>
						<TD class="SearchValue" Colspan="1">
							<xsl:value-of select="NewDataSet/Summary/FromWHID"/>
						</TD>

						<TD class="SearchKey">ชื่อ :</TD>
						<TD class="SearchValue" Colspan="1">
							<xsl:value-of select="NewDataSet/Summary/WHName"/>
						</TD>
					</TR>

					<TR>
						<TD CLASS="gridHeader" WIDTH="50">ลำดับ</TD>
						<TD CLASS="gridHeader" WIDTH="85">รหัสสินค้า</TD>
						<TD CLASS="gridHeader" WIDTH="310">ชื่อสินค้า</TD>
						
						<TD CLASS="gridHeader" WIDTH="70">หน่วยคูณ</TD>

						<TD CLASS="gridHeader" WIDTH="87">ยกมา(ใหญ่)</TD>
						<TD CLASS="gridHeader" WIDTH="87">ยกมา(เล็ก)</TD>
						
						<TD CLASS="gridHeader" WIDTH="87">เข้า(เล็ก)</TD>
						<TD CLASS="gridHeader" WIDTH="87">ออก(เล็ก)</TD>

						<TD CLASS="gridHeader" WIDTH="87">คงเหลือ(ใหญ่)</TD>
						<TD CLASS="gridHeader" WIDTH="87">คงเหลือ(เล็ก)</TD>
					</TR>

					<xsl:for-each select="NewDataSet/Summary">
						<TR>
							<TD class="SearchResultItem">
								<xsl:value-of  select="position()"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="ProductCode"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of  select="ProductName"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of select="BaseQty"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of select="ImpLargeQty" />
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of select="ImpSmallQty"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of select="InQty"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of select="OutQty"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of select="QtyOnHandLarge"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of select="QtyOnHandSmall"/>
							</TD>
						</TR >
					</xsl:for-each>

				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>