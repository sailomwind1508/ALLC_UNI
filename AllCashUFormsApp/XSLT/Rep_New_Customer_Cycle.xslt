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
					<xsl:apply-templates select="Rep_New_Customer_XSLT_Cycle"/>
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
							<xsl:value-of select="NewDataSet/Rep_New_Customer_XSLT_Cycle/CompanyName"/>
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
							<xsl:value-of select="NewDataSet/Rep_New_Customer_XSLT_Cycle/BranchName"/>
						</td>
						<td style="font-weight: bold;font-size: 11pt;text-align:right">ปี : </td>
						<td style="text-align: left;">
							<xsl:value-of select="NewDataSet/Rep_New_Customer_XSLT_Cycle/Year"/>
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
						<TD CLASS="HReport">Cycle1</TD>
						<TD CLASS="HReport">Cycle2</TD>
						<TD CLASS="HReport">Cycle3</TD>
						<TD CLASS="HReport">Cycle4</TD>
						<TD CLASS="HReport">Cycle5</TD>
						<TD CLASS="HReport">Cycle6</TD>
						<TD CLASS="HReport">Cycle7</TD>
						<TD CLASS="HReport">Cycle8</TD>
						<TD CLASS="HReport">Cycle9</TD>
						<TD CLASS="HReport">Cycle10</TD>
						<TD CLASS="HReport">Cycle11</TD>
						<TD CLASS="HReport">Cycle12</TD>
						<TD CLASS="HReport">Cycle13</TD>
						<TD CLASS="HReport">รวมปี</TD>
					</TR>
					<xsl:for-each select="NewDataSet/Rep_New_Customer_XSLT_Cycle">
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
									<xsl:value-of  select="Cycle1"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="Cycle2"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="Cycle3"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="Cycle4"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="Cycle5"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="Cycle6"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="Cycle7"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="Cycle8"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="Cycle9"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="Cycle10"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="Cycle11"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="Cycle12"/>
								</TD>
								<TD class="TDetail">
									<xsl:value-of  select="Cycle13"/>
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