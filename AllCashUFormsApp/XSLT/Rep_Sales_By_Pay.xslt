<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_Sales_By_Pay_XSLT">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานสรุปยอดขาย(แยกตามประเภทการชำระเงิน)">
				<ss:Table>
					<xsl:apply-templates select="Rep_Sales_By_Pay_XSLT"/>
				</ss:Table>
			</ss:Worksheet>
		</ss:Workbook>
	</xsl:template>
	
	<xsl:key name="contacts-by-DocDate" match="Rep_Sales_By_Pay_XSLT" use="DocDate" />
	
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
					text-align: right;
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
						<TD class="stdPageHdr" colspan="5">รายงานสรุปยอดขาย(แยกตามประเภทการชำระเงิน) </TD>
					</TR>
					<TR>
						<TD class="stdPageHdr">Depot : </TD>
						<TD class="stdPageHdr" Colspan="2">
							<xsl:value-of select="NewDataSet/Rep_Sales_By_Pay_XSLT/BrnName"/>
						</TD>
					</TR>
					
					<!--Header-->
					
				<xsl:for-each select="NewDataSet/Rep_Sales_By_Pay_XSLT[count(. | key('contacts-by-DocDate', DocDate)[1]) = 1]">
					
					<xsl:for-each select="key('contacts-by-DocDate', DocDate)">
						<xsl:if test="count(. | key('contacts-by-DocDate', DocDate)[1]) = 1">
					<tr>
						<td></td>
					</tr>
							
					<TR>
						<TD class="GroupHeader" ColSpan="1">
							วันที่ :
						</TD>
						<TD class="GroupHeader" ColSpan="1">
							<xsl:value-of select="DocDate" />
						</TD>
					</TR>
					
					<TR>
						<TD CLASS="gridHeader">คลัง</TD>
						<TD CLASS="gridHeader">พนักงานขาย</TD>
						<TD CLASS="gridHeader">ยอดส่งเงินสด</TD>
						<TD CLASS="gridHeader">ชำระบิลเครดิต</TD>
						<TD CLASS="gridHeader">รวมยอดขาย</TD>
					</TR>
						
					<!--<xsl:for-each select="NewDataSet/Rep_Sales_By_Pay_XSLT">-->
					<xsl:for-each select="key('contacts-by-DocDate', DocDate)">
						<xsl:sort select="concat(DocDate,' ',WHID)"/>
						
						<TR>
							<TD class="SearchResultItem">
								<xsl:value-of  select="WHID"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of  select="SaleEmpName"/>
							</TD>
							
							<TD class="SearchResultItem" align="Right">
								<xsl:if test="CashAmount=0">
									0
								</xsl:if>
								<xsl:if test="CashAmount!=0">
									<xsl:value-of select="format-number(CashAmount,'#,##0.00')"/>
								</xsl:if>
							</TD>

							<TD class="SearchResultItem" align="Right">
								<xsl:if test="CreditAmount=0">
									0
								</xsl:if>
								<xsl:if test="CreditAmount>0">
									<xsl:value-of select='format-number(CreditAmount,"#,##0.00")'/>
								</xsl:if>
							</TD>

							<TD class="SearchResultItem" align="Right">
								<xsl:if test="Total=0">
									0
								</xsl:if>
								<xsl:if test="Total!=0">
									<xsl:value-of select='format-number(Total,"#,##0.00")'/>
								</xsl:if>
							</TD>
							
						</TR>
					</xsl:for-each>
					
					<TR>
						<TD></TD>
						<TD ColSpan="1" Class="GroupFooter">
							รวม : 
						</TD>
						<TD Class="GroupFooter" >
							<xsl:value-of  select="format-number(sum(key('contacts-by-DocDate', DocDate)/CashAmount),'#,##0.00')" />
						</TD>
						<TD Class="GroupFooter" >
							<xsl:value-of  select="format-number(sum(key('contacts-by-DocDate', DocDate)/CreditAmount),'#,##0.00')" />
							<!--<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Sales_By_Pay_XSLT/CreditAmount),"#,##0.00")'/>-->
						</TD>
						<TD Class="GroupFooter" >
							<xsl:value-of  select="format-number(sum(key('contacts-by-DocDate', DocDate)/Total),'#,##0.00')" />
							<!--<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Sales_By_Pay_XSLT/Total),"#,##0.00")'/>-->
						</TD>
					</TR>		
					
					</xsl:if>
					</xsl:for-each >
					
					<!--<TR Class="GroupFooter">
						<TD>
						</TD>
						<TD ColSpan="1" align="right">รวม</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Sales_By_Pay_XSLT/CashAmount),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Sales_By_Pay_XSLT/CreditAmount),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Sales_By_Pay_XSLT/Total),"#,##0.00")'/>
						</TD>
					</TR >-->
					</xsl:for-each >
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>