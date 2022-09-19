<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Form_IV">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="จำนวนลูกค้าใหม่">
				<ss:Table>
					<xsl:apply-templates select="Rep_New_Customer_XSLT"/>
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
						<td Colspan="4" style="font-weight: bold;font-size: 11pt;">
							<xsl:value-of select="NewDataSet/Rep_New_Customer_XSLT/CompanyName"/>
						</td>					
					</tr>
					<tr>
						<td Colspan="4" style="font-weight: bold;font-size: 11pt;">
						รายงานลูกค้าใหม่(จำนวน)
						</td>
					</tr>
					<tr>
						<td style="font-weight: bold;font-size: 11pt;">Depot : </td>
						<td Colspan="3" style="font-weight: bold;font-size: 11pt;">
							<xsl:value-of select="NewDataSet/Rep_New_Customer_XSLT/BranchName"/>
						</td>
						<td style="font-weight: bold;font-size: 11pt;text-align:right">ปี : </td>
						<td style="text-align: left;">
							<xsl:value-of select="NewDataSet/Rep_New_Customer_XSLT/Year"/>
						</td>
					</tr>
					<tr></tr>
					
					
					<TR>
						<TD CLASS="HReport">ลำดับ</TD>
						<TD CLASS="HReport">แวน</TD>
						<TD CLASS="HReport">ชื่อแวน</TD>
						<TD CLASS="HReport">ชื่อสกุล</TD>
						<TD CLASS="HReport">รหัสพนักงาน</TD>
						<TD CLASS="HReport">จำนวนร้านทั้งหมด</TD>
						<TD CLASS="HReport">ม.ค.</TD>
						<TD CLASS="HReport">ก.พ.</TD>
						<TD CLASS="HReport">มี.ค.</TD>
						<TD CLASS="HReport">เม.ย.</TD>
						<TD CLASS="HReport">พ.ค.</TD>
						<TD CLASS="HReport">มิ.ย.</TD>
						<TD CLASS="HReport">ก.ค.</TD>
						<TD CLASS="HReport">ส.ค.</TD>
						<TD CLASS="HReport">ก.ย.</TD>
						<TD CLASS="HReport">ต.ค.</TD>
						<TD CLASS="HReport">พ.ย.</TD>
						<TD CLASS="HReport">ธ.ค.</TD>
						<TD CLASS="HReport">รวมปี</TD>
					</TR>
					<xsl:for-each select="NewDataSet/Rep_New_Customer_XSLT">
						<xsl:sort select="WHID"/>
							<tr>
								<xsl:choose>
										<xsl:when test="WHID = 9999">
											<td class="TDetail"></td>
										</xsl:when>
										<xsl:otherwise>
											<TD class="TDetail">
												<xsl:value-of  select="position()" />
											</TD>
										</xsl:otherwise>
									</xsl:choose>
								<!--<TD class="TDetail">
									<xsl:value-of  select="position()"/>
								</TD>-->
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
								<!--<TD class="TDetail">
									<xsl:value-of  select="WHID"/>
								</TD>-->
								<TD class="TDetail">
									<xsl:value-of  select="WHName"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="SaleName"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="SaleCode"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="CAllCust"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="M1"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="M2"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="M3"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="M4"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="M5"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="M6"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="M7"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="M8"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="M9"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="M10"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="M11"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="M12"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="SWH"/>
								</TD>
							</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>