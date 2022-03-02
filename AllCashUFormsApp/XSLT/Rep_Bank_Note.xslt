<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_Bank_Note">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานส่งเงินประจำวัน">
				<ss:Table>
					<xsl:apply-templates select="Rep_Bank_Note"/>
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
					font-weight: bold;
					font-style:Regular;
					padding-left: 4px;
					padding-top: 4px;
					padding-bottom: 4px;
					width: 100%;
					font-size: 11pt;
					}
					.gridHeader {
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
					font-size: 9pt;
					vertical-align:middle;
					text-align:right;
					border: solid thin Black;
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
						<TD class="stdPageHdr" colspan="8">รายงานส่งเงินประจำวัน</TD>
					</TR>
					<TR>
						<TD class="SearchKey">Depot :</TD>
						<TD class="SearchValue" Colspan="2">
							<xsl:value-of select="NewDataSet/Rep_Bank_Note/BrnName"/>
						</TD>
						<TD class="SearchKey">Date :</TD>
						<TD class="SearchValue" Colspan="2">
							<xsl:value-of select="NewDataSet/Rep_Bank_Note/DocDate"/>
						</TD>
					</TR>
					<tr></tr>
					<TR>
						<TD CLASS="gridHeader">คลัง</TD>
						<TD CLASS="gridHeader">ยอดส่งเงินสด</TD>
						<TD CLASS="gridHeader">ชำระบิลเครดิต</TD>
						<TD CLASS="gridHeader">ยอดโอน</TD>
						<TD CLASS="gridHeader">ค่าธรรมเนียม</TD>
						<TD CLASS="gridHeader">รวมส่งเงิน</TD>
						<TD CLASS="gridHeader">รวมยอดขาย</TD>
						<TD CLASS="gridHeader">เกิน/ขาด</TD>
					</TR>
					<xsl:for-each select="NewDataSet/Rep_Bank_Note">
						<xsl:sort select="WHID" />
						<TR>
							<TD class="SearchResultItem">
								<xsl:value-of  select="WHID"/>
							</TD>
							<TD class="SearchResultItem" align="Right">
								<xsl:if test="Send=0">
									-
								</xsl:if>
								<xsl:if test="Send!=0">
									<xsl:value-of select="format-number(Send,'#,##0.00')"/>
								</xsl:if>

							</TD>
							<TD class="SearchResultItem" align="Right">
								<xsl:if test="Deposit=0">
									-
								</xsl:if>
								<xsl:if test="Deposit!=0">
									<xsl:value-of select='format-number(Deposit,"#,##0.00")'/>
								</xsl:if>
				
							</TD>
							<TD class="SearchResultItem" align="Right">
								<xsl:if test="Transfer=0">
									-
								</xsl:if>
								<xsl:if test="Transfer!=0">
									<xsl:value-of select='format-number(Transfer,"#,##0.00")'/>
								</xsl:if>
			
							</TD>
							<TD class="SearchResultItem" align="Right">
								<xsl:if test="Cheque=0">
									-
								</xsl:if>
								<xsl:if test="Cheque>0">
									<xsl:value-of select='format-number(Cheque,"#,##0.00")'/>
								</xsl:if>
									

							</TD>
							<TD class="SearchResultItem" align="Right">
								<xsl:if test="TotalSend=0">
									-
								</xsl:if>
								<xsl:if test="TotalSend!=0">
									<xsl:value-of select='format-number(TotalSend,"#,##0.00")'/>
								</xsl:if>
						
							</TD>
							<TD class="SearchResultItem" align="Right">
								<xsl:if test="Total=0">
									-
								</xsl:if>
								<xsl:if test="Total!=0">
									<xsl:value-of select='format-number(Total,"#,##0.00")'/>
								</xsl:if>
				
							</TD>
							<TD class="SearchResultItem" align="Right">
								<xsl:if test="Balance=0">
									-
								</xsl:if>
								<xsl:if test="Balance!=0">
									<xsl:value-of select='format-number(Balance,"#,##0.00")'/>
								</xsl:if>
								
							</TD>
						</TR >
					</xsl:for-each>
					<TR Class="GroupFooter">
						<TD ColSpan="1">รวม</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Bank_Note/Send),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Bank_Note/Deposit),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Bank_Note/Transfer),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Bank_Note/Cheque),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Bank_Note/TotalSend),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Bank_Note/Total),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Bank_Note/Balance),"#,##0.00")'/>
						</TD>
					</TR >
					<TR></TR>
					<tr>
						<td ColSpan="6"></td>
						<td>รวมส่งจริง</td>
						<td Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Bank_Note/Send),"#,##0.00")'/>
						</td>
					</tr>
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>