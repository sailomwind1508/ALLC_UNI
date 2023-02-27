<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_Actual_Sale_By_Sku_XSLT">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานยอดขายแยกSKU">
				<ss:Table>
					<xsl:apply-templates select="Rep_Actual_Sale_By_Sku_XSLT"/>
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
					<TR>
						<TD CLASS="HReport">Year-M</TD>
						<TD CLASS="HReport">Day</TD>
						<TD CLASS="HReport">Van_Type</TD>
						<TD CLASS="HReport">Van</TD>
						<TD CLASS="HReport">CustomerID</TD>
						<TD CLASS="HReport">CustomerName</TD>
						<TD CLASS="HReport">Seq</TD>
						<TD CLASS="HReport">ProductSubGroupName</TD>
						<TD CLASS="HReport">ProductID</TD>
						<TD CLASS="HReport">ProductName</TD>
						<TD CLASS="HReport">Qty</TD>
						<TD CLASS="HReport">Gamt</TD>
					</TR>
					<xsl:for-each select="NewDataSet/Rep_Actual_Sale_By_Sku_XSLT">
						<xsl:sort select="concat(Day,WHID,ProductSubGroupID,ProductID)"/>
						<tr>
							<TD class="TDetail">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="Year-M"/>
							</TD>
							<TD class="TDetail">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="Day"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of select="Van_Type"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of select="WHID"/>
							</TD>
							<TD class="TDetail">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="CustomerID"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of select="CustName"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of select="ProductSubGroupID"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of select="ProductSubGroupName"/>
							</TD>
							<TD class="TDetail">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="ProductID"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of select="ProductName"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of select="format-number(Qty,'#,##0.00')"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of select="format-number(Gamt,'#,##0.00')"/>
							</TD>
						</tr>
					</xsl:for-each>
					
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>