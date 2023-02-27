<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Form_IV">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายการลูกค้าใหม่">
				<ss:Table>
					<xsl:apply-templates select="Rep_New_Customer_Detail_XSLT_Cycle"/>
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
					font-size: 11pt;
					font-weight: bold;
					text-align:center;
					border: solid thin Black;
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
					<tr>
						<td Colspan="5" style="font-weight: bold;font-size: 11pt;">
							<xsl:value-of select="NewDataSet/Rep_New_Customer_Detail_XSLT_Cycle/CompanyName"/>
						</td>
					</tr>
					<tr>
						<td Colspan="5" style="font-weight: bold;font-size: 11pt;">
							รายงานลูกค้าใหม่(รายลูกค้า)
						</td>
					</tr>
					<tr>
						<td style="font-weight: bold;font-size: 11pt;">Depot : </td>
						<td Colspan="3" style="font-weight: bold;font-size: 11pt;">
							<xsl:value-of select="NewDataSet/Rep_New_Customer_Detail_XSLT_Cycle/BranchName"/>
							(<xsl:value-of select="NewDataSet/Rep_New_Customer_Detail_XSLT_Cycle/BranchID"/>)
						</td>
						<td style="font-weight: bold;font-size: 11pt;text-align:right;">ปี : </td>
						<td>
							<xsl:value-of select="NewDataSet/Rep_New_Customer_Detail_XSLT_Cycle/Y"/>
						</td>
					</tr>
					<tr></tr>


					<TR>
						<TD CLASS="HReport">รหัสศูนย์</TD>
						<TD CLASS="HReport">ชื่อศูนย์</TD>
						<TD CLASS="HReport">ปี</TD>
						<TD CLASS="HReport">เดือน</TD>
						<TD CLASS="HReport">วันที่เริ่ม</TD>
						<TD CLASS="HReport">VAN</TD>
						<TD CLASS="HReport">ชื่อสกุล</TD>
						<TD CLASS="HReport">รหัสพนักงาน</TD>
						<TD CLASS="HReport">รหัสลูกค้า</TD>
						<TD CLASS="HReport">ชื่อลูกค้า</TD>
						<TD CLASS="HReport">ที่อยู่</TD>
						<TD CLASS="HReport">เบอร์โทร</TD>
						<TD CLASS="HReport">มูลค่าบิล</TD>
						<TD CLASS="HReport">รวมทั้งเดือน</TD>
					</TR>
					<xsl:for-each select="NewDataSet/Rep_New_Customer_Detail_XSLT_Cycle">
						<tr>
							<TD class="TDetail">
								<xsl:value-of  select="BranchID"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="BranchName"/>
							</TD>
							<xsl:choose>
								<xsl:when test="Y = 0">
									<td class="TDetail"></td>
								</xsl:when>
								<xsl:otherwise>
									<TD class="TDetail">
										<xsl:value-of  select="Y"/>
									</TD>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="M = 0">
									<td class="TDetail"></td>
								</xsl:when>
								<xsl:otherwise>
									<TD class="TDetail">
										<xsl:value-of  select="M"/>
									</TD>
								</xsl:otherwise>
							</xsl:choose>
							<TD class="TDetail">
								<xsl:value-of  select="D"/>
							</TD>
							<xsl:choose>
								<xsl:when test="WHID = 9999">
									<td class="TDetail"></td>
								</xsl:when>
								<xsl:otherwise>
									<TD class="TDetail">
										<xsl:value-of  select="WHID" />
									</TD>
								</xsl:otherwise>
							</xsl:choose>
							<TD class="TDetail">
								<xsl:value-of  select="Name"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="EmpIDCard"/>
							</TD>
							<TD class="TDetail">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="CustomerID"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="CustName"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="BillTo"/>
							</TD>
							<TD class="TDetail">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="Telephone"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="format-number(Total,'#,##0.00')"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="format-number(STotal,'#,##0.00')"/>
							</TD>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>