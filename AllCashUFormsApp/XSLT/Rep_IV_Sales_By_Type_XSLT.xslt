<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_IV_Sales_By_Type_XSLT">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานภาษีขายอย่างย่อ(แยกตามประเภทการชำระเงิน)">
				<ss:Table>
					<xsl:apply-templates select="Rep_IV_Sales_By_Type_XSLT"/>
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
					.credit_class{
					color: red;
					font-weight: normal;
					font-size: 9pt;
					}
					.cash_class{
					color: green;
					font-weight: normal;
					font-size: 9pt;
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
					.gridHeader_credit {
					background-color: #FE6161;
					color: DarkBlue;
					font-size: 9pt;
					font-weight: bold;
					vertical-align:middle;
					text-align:center;
					border: solid thin Black;
					}
					.gridHeader_cash {
					background-color: #89FE83;
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
						<TD class="stdPageHdr" colspan="14">
							<xsl:value-of select="/NewDataSet/Rep_IV_Sales_By_Type_XSLT/Company"/>
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" colspan="14">รายงานภาษีขายอย่างย่อ(แยกตามประเภทการชำระเงิน)</TD>
					</TR>
					<TR>
						<TD class="SearchKey">ระหว่างวันที่</TD>
						<TD class="SearchValue" Colspan="1">
							<xsl:value-of select="/NewDataSet/Rep_IV_Sales_By_Type_XSLT/StartDate"/>
						</TD>
						<TD class="SearchKey">ถึงวันที่</TD>
						<TD class="SearchValue" Colspan="1">
							<xsl:value-of select="/NewDataSet/Rep_IV_Sales_By_Type_XSLT/EndDate"/>
						</TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
						<TD ColSpan="2" class="SearchKey">เลขประจำตัวผู้เสียภาษีอากร</TD>
						<TD class="SearchValue">
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of select="/NewDataSet/Rep_IV_Sales_By_Type_XSLT/BranchTaxID"/>
						</TD>
					</TR>
					<TR>
						<TD class="SearchKey"></TD>
						<TD class="SearchValue" ColSpan="2">
							<xsl:value-of select="/NewDataSet/Rep_IV_Sales_By_Type_XSLT/BranchName"/>
						</TD>
					</TR>
					<TR>
						<TD class="SearchKey">วันที่พิมพ์รายงาน</TD>
						<TD class="SearchValue" Colspan="9">
							<xsl:value-of select="/NewDataSet/Rep_IV_Sales_By_Type_XSLT/Period"/>
						</TD>
					</TR>
					<TR>
						<TD CLASS="gridHeader">ลำดับ</TD>
						<TD CLASS="gridHeader">วันที่เอกสาร</TD>
						<TD CLASS="gridHeader">แวน</TD>
						<TD CLASS="gridHeader">เลขที่ใบกำกับภาษีอย่างย่อ</TD>
						<TD CLASS="gridHeader" WIDTH="180">ชื่อลูกค้า</TD>
						<TD CLASS="gridHeader" WIDTH="110">เลขประจำตัวผู้เสียภาษีอากร</TD>
						<TD CLASS="gridHeader" WIDTH="110">สถานประกอบการ สำนักงานใหญ่ /สาขา</TD>
						<TD CLASS="gridHeader">ประเภทเอกสาร</TD>
						<TD CLASS="gridHeader_cash">เงินสด</TD>
						<TD CLASS="gridHeader_credit">เครดิต</TD>
						<TD CLASS="gridHeader">จำนวนเงินรวม</TD>
						<TD CLASS="gridHeader">จำนวนเงินภาษี</TD>
						<TD CLASS="gridHeader" WIDTH="100">อ้างถึงเอกสาร</TD>
					</TR>
					<xsl:for-each select="NewDataSet/Rep_IV_Sales_By_Type_XSLT">

						<xsl:sort select="DocDate" />
						<TR>
							<TD class="SearchResultItem">
								<xsl:value-of select="No" />
							</TD>
							<TD class="SearchResultItem">

								<xsl:value-of select="DocDate"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="WHID"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="DocNo"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="CustName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="TaxId"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="BranchName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="DocTypeName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:choose>
									<xsl:when test="CashAmount>0">
										<p class="cash_class">
											<xsl:value-of select="format-number(CashAmount,'#,##0.00')" />
										</p>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="format-number(CashAmount,'#,##0.00')"/>
									</xsl:otherwise>
								</xsl:choose>
							</TD>
							<TD class="SearchResultItem">
								<xsl:choose>
									<xsl:when test="CreditAmount>0">
										<p class="credit_class">
											<xsl:value-of select="format-number(CreditAmount,'#,##0.00')" />
										</p>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="format-number(CreditAmount,'#,##0.00')"/>
									</xsl:otherwise>
								</xsl:choose>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(Amount,'#,##0.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(VatAmt,'#,##0.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="CustInvNO"/>
							</TD>
						</TR >

					</xsl:for-each>
					<TR Class="GroupFooter">
						<TD ColSpan="8">รวมทั้งสิ้น</TD>
						<TD Class="subTotals" align="right">
							<xsl:choose>
								<xsl:when test="sum(/NewDataSet/Rep_IV_Sales_By_Type_XSLT/CashAmount)>0">
									<p class="cash_class" style="font-weight: bold;">
										<xsl:value-of select='format-number(sum(/NewDataSet/Rep_IV_Sales_By_Type_XSLT/CashAmount),"#,##0.00")'/>
									</p>
								</xsl:when>
								<xsl:otherwise>
									<p style="font-weight: bold;">
										<xsl:value-of select='format-number(sum(/NewDataSet/Rep_IV_Sales_By_Type_XSLT/CashAmount),"#,##0.00")'/>
									</p>
								</xsl:otherwise>
							</xsl:choose>
							
							
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:choose>
								<xsl:when test="sum(/NewDataSet/Rep_IV_Sales_By_Type_XSLT/CreditAmount)>0">
									<p class="credit_class" style="font-weight: bold;">
										<xsl:value-of select='format-number(sum(/NewDataSet/Rep_IV_Sales_By_Type_XSLT/CreditAmount),"#,##0.00")'/>
									</p>
								</xsl:when>
								<xsl:otherwise>
									<p style="font-weight: bold;">
										<xsl:value-of select='format-number(sum(/NewDataSet/Rep_IV_Sales_By_Type_XSLT/CreditAmount),"#,##0.00")'/>
									</p>
								</xsl:otherwise>
							</xsl:choose>
							

							
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_IV_Sales_By_Type_XSLT/Amount),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_IV_Sales_By_Type_XSLT/VatAmt),"#,##0.00")'/>
						</TD>
					</TR >
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>

</xsl:stylesheet>
