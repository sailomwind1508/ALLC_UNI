<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_BankNote_TotalSalePerMonth">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานยอดขาย">
				<ss:Table>
					<xsl:apply-templates select="Rep_BankNote_TotalSalePerMonth"/>
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
					font-size: 14pt;
					}
					.stdPageHdrLeft {
					color: DarkBlue;
					font-weight: bold;
					font-style:Regular;
					text-align: left;
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
						<TD class="stdPageHdr" colspan="9">
							<xsl:value-of select="NewDataSet/Rep_BankNote_TotalSalePerMonth/ReportHeader"/>
						</TD>
					</TR>
					
					<TR>
						<TD class="stdPageHdrLeft" colspan="3">All Cash Daily Report</TD>
					</TR>
					
					<TR>
						<TD></TD>
						<TD WIDTH="100"></TD>
						<TD WIDTH="100"></TD>
						<TD WIDTH="160"></TD>
						<TD WIDTH="100"></TD>
						<TD WIDTH="160">ยอดเงินที่ PayIn Last Day ทั้งหมด</TD>
						<TD WIDTH="120">วันนี้ Credit Sales (ยังไม่ Due,ใบกำกับภาษีที่ยังไม่ได้เก็บเงิน)</TD>
						<TD WIDTH="90"></TD>
						<TD WIDTH="110"></TD>
						<TD WIDTH="300"></TD>
						<TD WIDTH="130"></TD>
					</TR><!--Comment-->
					
					<TR>
						<TD CLASS="gridHeader"></TD>
						<TD CLASS="gridHeader" WIDTH="100">1</TD>
						<TD CLASS="gridHeader" WIDTH="100">2</TD>
						<TD CLASS="gridHeader" WIDTH="160">3</TD>
						<TD CLASS="gridHeader" WIDTH="100">4</TD>
						<TD CLASS="gridHeader" WIDTH="160">5</TD>
						<TD CLASS="gridHeader" WIDTH="120">6</TD>
						<TD CLASS="gridHeader" WIDTH="90">8</TD>
						<TD CLASS="gridHeader" WIDTH="110">9</TD>
						<TD CLASS="gridHeader" WIDTH="300"></TD>
						<TD CLASS="gridHeader" WIDTH="130"></TD>
					</TR><!--Columns Number-->
						
					<TR>
						<TD CLASS="gridHeader">Date</TD>
						<TD CLASS="gridHeader" WIDTH="100">Sales</TD>
						<TD CLASS="gridHeader" WIDTH="100">PayIn Today</TD>
						<TD CLASS="gridHeader" WIDTH="160">Credit Sales ChequeIn</TD>
						<TD CLASS="gridHeader" WIDTH="100">CashIn Today</TD>
						<TD CLASS="gridHeader" WIDTH="160">PayIn LastDay</TD>
						<TD CLASS="gridHeader" WIDTH="120">CashIn Last Day</TD>
						<TD CLASS="gridHeader" WIDTH="90">Balance</TD>
						<TD CLASS="gridHeader" WIDTH="110">Cash Balance</TD>
						<TD CLASS="gridHeader" WIDTH="300">หมายเหตุ</TD>
						<TD CLASS="gridHeader" WIDTH="130">มูลค่าสินค้าคงเหลือ</TD>
					</TR>
					
				
					<TR>
							<TD class="SearchResultItem">
							</TD><!--DocDate-->
							<TD class="SearchResultItem">
							</TD><!--Sales-->
							<TD class="SearchResultItem">
							</TD><!--PayInToday-->
							<TD class="SearchResultItem">
							</TD><!--Credit Sales ChequeIn-->
							<TD class="SearchResultItem">
							</TD><!--CashInToday-->
							<TD class="SearchResultItem">
							</TD><!--PayIn LastDay-->
							<TD class="SearchResultItem">
							</TD><!--CashIn Last Day-->
							<TD class="SearchResultItem">
							</TD><!--Balance-->
						
							<TD class="SearchResultItem">
								<xsl:value-of select='format-number(NewDataSet/Rep_BankNote_TotalSalePerMonth/CashBalance,"###,###.00")'/>
							</TD><!--CashBalance-->
						
							<TD class="SearchResultItem" align="center">
								ยอดยกมาของวันที่ <xsl:value-of select="(NewDataSet/Rep_BankNote_TotalSalePerMonth/SaleDateFr)"/>
							</TD><!--หมายเหตุ-->
						
							<TD class="SearchResultItem">
							</TD><!--มูลค่าสินค้าคงเหลือ-->
						</TR >
						
					<xsl:for-each select="NewDataSet/Rep_BankNote_TotalSalePerMonth">
						<TR>
							<TD class="SearchResultItem">
								<xsl:value-of select="DocDate2"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(Sales,'###,###.00')"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(PayInToday,'###,###.00')"/>
							</TD>
							
							<TD class="SearchResultItem">
								
							</TD><!--Credit Sales ChequeIn-->
								
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(CashInToday,'###,###.00')"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(PayInLastDay,'###,###.00')"/>
							</TD>
								
							<TD class="SearchResultItem">
								
							</TD><!--CashIn Last Day-->
							
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(Balance,'###,###.00')"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(CashInToday,'###,###.00')"/>
							</TD><!--CashBalance-->
							
							<TD class="SearchResultItem">
								<xsl:value-of select="Reason"/>
							</TD>
							
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(TotalPrice,'###,###.00')"/>
							</TD><!--มูลค่าสินค้าคงเหลือ-->
							
						</TR >
					</xsl:for-each>
					
	
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>

</xsl:stylesheet>
