<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Form_IV">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานรายละเอียดร้านค้าจัดกลุ่มตามตำบล">
				<ss:Table>
					<xsl:apply-templates select="Rep_Customer_Detail_By_Branch_XSLT"/>
				</ss:Table>
			</ss:Worksheet>
		</ss:Workbook>
	</xsl:template>

	<xsl:template match="/">
		<html>
			<head>
				<style>
					.gridHeader {
					font-size: 11pt;
					font-weight: bold;
					vertical-align:middle;
					text-align:center;
					border: solid thin Black;
					background-color: #99CCFF;
					}
					.SearchValue
					{
					color: Black;
					font-size: 9pt;
					font-weight: bold;
					vertical-align:middle;
					text-align:left;
					border: solid thin Black;
					}
					.BorderG {
					border: solid thin Black;
					}
					.HReport {
					color:blue;
					font-size: 11pt;
					font-weight: bold;
					text-align:center;
					border: none;
					}
					.TReport {
					font-size: 11pt;
					font-weight: bold;
					text-align:center;
					border: solid thin Black;
					background-color:#99CCFF;
					}
					.TDetail {
					border: solid thin Black;
					}
					.FReport {
					border-left: solid thin Black;
					border-right: solid thin Black;
					}
					.HL {
					border-left: solid thin Black;
					}
					.HR {
					border-right: solid thin Black;
					}
				</style>
			</head>
			<body class="content">
				<table>
					<TR>
						<TD CLASS="HReport" Colspan="7">
							<xsl:value-of  select="NewDataSet/Rep_Customer_Detail_By_Branch_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD CLASS="HReport" Colspan="7">รายงานรายละเอียดร้านค้าจัดกลุ่มตามตำบล</TD>
					</TR>
					<TR>
						<TD CLASS="TReport" Colspan="2" style="background-color:#99CCFF;">BB. :</TD>
						<TD CLASS="TDetail" Colspan="2">
							<xsl:value-of  select="NewDataSet/Rep_Customer_Detail_By_Branch_XSLT/BranchName" />
						</TD>
					</TR>
					<TR>
						<TD CLASS="TReport" Colspan="2" style="background-color:#99CCFF;">วันที่พิมพ์ :</TD>
						<TD CLASS="TDetail" Colspan="2">
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of  select="NewDataSet/Rep_Customer_Detail_By_Branch_XSLT/Date"/>
						</TD>
					</TR>
					<TR></TR>
					<TR>
						<TD CLASS="TReport">Van</TD>
						<TD CLASS="TReport">Route</TD>
						<TD CLASS="TReport">ชื่อตลาด</TD>
						<TD CLASS="TReport">อำเภอ</TD>
						<TD CLASS="TReport">จังหวัด</TD>
						<TD CLASS="TReport">ชื่อร้านค้า</TD>
						<TD CLASS="TReport">ที่อยู่</TD>
					</TR>
					<xsl:for-each select="NewDataSet/Rep_Customer_Detail_By_Branch_XSLT">
						<xsl:sort select="WHID"/>
						<tr>
							<TD class="TDetail">
								<xsl:value-of  select="Van"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="Route"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="ชื่อตลาด"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="อำเภอ"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="จังหวัด"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="ชื่อร้านค้า"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="ที่อยู่"/>
							</TD>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>