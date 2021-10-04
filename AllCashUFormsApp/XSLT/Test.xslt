<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_V_Sales_XSLT">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานภาษีขายเต็มรูป">
				<ss:Table>
					<xsl:apply-templates select="Rep_V_Sales_XSLT"/>
				</ss:Table>
			</ss:Worksheet>
		</ss:Workbook>
	</xsl:template>
	
	<Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet" xmlns:html="http://www.w3.org/TR/REC-html40">
		<Worksheet ss:Name="Sheet1">
			<Table ss:ExpandedColumnCount="1" ss:ExpandedRowCount="1" x:FullColumns="1" x:FullRows="1">
				<Row>
					<Cell>
						<Data ss:Type="String">Sheet1Data</Data>
					</Cell>
				</Row>
			</Table>
		</Worksheet>
		<Worksheet ss:Name="Sheet2">
			<Table ss:ExpandedColumnCount="1" ss:ExpandedRowCount="1" x:FullColumns="1" x:FullRows="1">
				<Row>
					<Cell>
						<Data ss:Type="String">Sheet2Data</Data>
					</Cell>
				</Row>
			</Table>
		</Worksheet>
	</Workbook>
</xsl:stylesheet>
