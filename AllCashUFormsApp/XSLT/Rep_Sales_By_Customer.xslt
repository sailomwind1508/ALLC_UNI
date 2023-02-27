<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_Sales_By_Customer_XSLT">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานยอดขาย แยกตามลูกค้า">
				<ss:Table>
					<xsl:apply-templates select="Rep_Sales_By_Customer_XSLT"/>
				</ss:Table>
			</ss:Worksheet>
		</ss:Workbook>
	</xsl:template>

	<xsl:template name="FormatDate">
		<xsl:param name="DateTime" />
		<!-- old date format YYYYMMDD / want DD/MM/YYYY-->
		<xsl:variable name="year">
			<xsl:value-of select="substring($DateTime,1,4)" />
		</xsl:variable>
		<xsl:variable name="mon">
			<xsl:value-of select="substring($DateTime,5,2)" />
		</xsl:variable>
		<xsl:variable name="day">
			<xsl:value-of select="substring($DateTime,7,2)" />
		</xsl:variable>
		<xsl:value-of select="$day" />/<xsl:value-of select="$mon" />/<xsl:value-of select="$year" />
	</xsl:template>

	<xsl:decimal-format name="foo"
  grouping-separator="'" digit="#" zero-digit="0" decimal-separator="."/>

	<!--<xsl:key name="contacts-by-docstatus" match="Table" use="DocStatus" />-->
	<xsl:key name="contacts-by-whid" match="Rep_Sales_By_Customer_XSLT" use="WHID" />
	<xsl:key name="contacts-by-docno" match="Rep_Sales_By_Customer_XSLT" use="concat(DocNo,' ',CustomerID)"/>
	<!--<xsl:key name="contacts-by-productcode" match="Table" use="concat(DocNo,' ',ProductCode)" />-->
	
	<xsl:template match="/">
		<HTML>
			<HEAD>
				<STYLE>
					.content{
					font-family:Tahoma;
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
					font-size: 11pt;
					}
					.stdPageHdr {
					color: DarkBlue;
					font-weight: bold;
					font-style:Regular;
					text-align: left;
					padding-left: 4px;
					padding-top: 4px;
					padding-bottom: 4px;
					width: 100%;
					font-size: 11pt;
					text-align: center;
					}
					.gridHeader {
					background-color: #DDEEFF;
					color: DarkBlue;
					font-size: 10pt;
					font-weight: bold;
					vertical-align:middle;
					text-align:center;
					border: solid thin Black;
					}
					.SearchHeader {
					color: DarkBlue;
					font-size: 10pt;
					font-weight: bold;
					}
					.SearchKey {
					color: DarkBlue;
					font-size: 10pt;
					vertical-align:middle;
					text-align:right;
					}
					.SearchValue
					{
					color: Black;
					font-size: 10pt;
					font-weight: bold;
					vertical-align:middle;
					text-align:left;
					}
					.SearchResultHeader {
					background-color: #9BE2F9;
					color: DarkBlue;
					font-weight: bold;
					font-size: 10pt;
					}
					.SearchResultItem {
					background-color: #FFFFFF;
					color: Black;
					border: solid thin Black;
					font-size: 10pt;
					}
					.SearchResultAltItem {
					background-color: #99CCFF;
					color: Black;
					font-size: 10pt;
					border: solid thin Black;
					}
					.GroupHeader{
					background-color: #FFFFFF;
					color: Black;
					font-weight: bold;
					font-style:Regular;
					text-align: left;
					padding-left: 1px;
					padding-top: 1px;
					padding-bottom: 1px;
					width: 100%;
					font-size: 10pt;
					}
					.GroupHeaderRed{
					background-color: #FFFFFF;
					color: Red;
					font-weight: bold;
					font-style:Regular;
					text-align: left;
					padding-left: 1px;
					padding-top: 1px;
					padding-bottom: 1px;
					width: 100%;
					font-size: 10pt;
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
					font-size: 10pt;
					}
					.GroupTotal{text-align:right;}
					.subTotals {border-bottom:1px solid black}
				</STYLE>
			</HEAD>
			<BODY class="content">
				<TABLE>
					<TR>
						<TD class="stdPageHdr" colspan="7">
							<xsl:value-of select="NewDataSet/Rep_Sales_By_Customer_XSLT/CompanyName"/>
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" colspan="7">รายงานยอดขาย แยกตามลูกค้า</TD>
					</TR>
					<TR>
						<TD class="SearchKey" Colspan="2" align="left">พิมพ์วันที่</TD>
						<TD class="SearchValue" Colspan="5">
							<xsl:value-of select="NewDataSet/Rep_Sales_By_Customer_XSLT/Period"/>
						</TD>
					</TR>
					<!--Header-->
					<xsl:for-each select="NewDataSet/Rep_Sales_By_Customer_XSLT[count(. | key('contacts-by-whid', WHID)[1]) = 1]">
						<TR>
							<TD class="GroupHeader" ColSpan="2">
								VAN :
							</TD>
							<TD class="GroupHeader" ColSpan="1">
								<xsl:value-of select="WHID" />
							</TD>

						</TR>
						<TR>
							<TD class="GroupHeader" ColSpan="2">
								SALE :
							</TD>
							<TD class="GroupHeader" ColSpan="1">
								<xsl:value-of select="EmpIDCard"/>
							</TD>
							<TD class="GroupHeader" ColSpan="3">
								<xsl:value-of select="EmpName"/>
							</TD>
						</TR>

						<xsl:for-each select="key('contacts-by-whid', WHID)">
							<!--<xsl:sort select="WHID" />-->
							<xsl:if test="count(. | key('contacts-by-docno', concat(DocNo,' ',CustomerID))[1]) = 1">
								<TR>
									<TD class="GroupHeader" ColSpan="2">
										เลขที่เอกสาร :
									</TD>

									<TD class="GroupHeader" ColSpan="3">
										<xsl:value-of select="DocNo" /> ,<xsl:value-of select="CustName"/>
									</TD>
									<xsl:choose>
										<xsl:when test="DocStatus = 4">
											<TD class="GroupHeader">
												ปกติ
											</TD>
										</xsl:when>
										<xsl:otherwise>
											<TD class="GroupHeaderRed">
												ยกเลิก
											</TD>
										</xsl:otherwise>
									</xsl:choose>
								</TR>

								<TR>
									<TD CLASS="gridHeader">ลำดับ</TD>
									<TD CLASS="gridHeader">รหัสสินค้า</TD>
									<TD CLASS="gridHeader" WIDTH="250">ชื่อสินค้า</TD>
									<TD CLASS="gridHeader">จำนวน</TD>
									<TD CLASS="gridHeader">หน่วย</TD>
									<TD CLASS="gridHeader">ส่วนลด</TD>
									<TD CLASS="gridHeader">จำนวนเงิน</TD>
								</TR>
								<xsl:for-each select="key('contacts-by-docno', concat(DocNo,' ',CustomerID))">
									<xsl:sort select="concat(DocNo,' ',CustomerID)"/>
									<TR>
										<TD class="SearchResultItem">
											<xsl:value-of select="position()" />
										</TD>
										<TD class="SearchResultItem">
											<xsl:value-of select="ProductID"/>
										</TD>
										<TD class="SearchResultItem">
											<xsl:value-of select="ProductName"/>
										</TD>
										<TD class="SearchResultItem">
											<xsl:value-of select="format-number(ReceivedQty,'#,##0.00')"/>
										</TD>
										<TD class="SearchResultItem">
											<xsl:value-of select="Pcv"/>
										</TD>
										<TD class="SearchResultItem">
											<xsl:value-of select="format-number(LineDiscount,'#,##0.00')"/>
										</TD>
										<TD class="SearchResultItem">
											<xsl:value-of select="format-number(LineTotal,'#,##0.00')"/>
										</TD>
									</TR >
								</xsl:for-each>
								<TR>
									<TD ColSpan="5" Class="GroupFooter">
										ยอดขายรวม(<xsl:value-of select="DocNo"/>)
									</TD>
									<TD Class="GroupFooter">
										<xsl:value-of select="LineDiscount"/>
									</TD>
									<xsl:choose>
										<xsl:when test="DocStatus = 4">
											<TD class="GroupHeader">
												<xsl:value-of  select="format-number(sum(key('contacts-by-docno',concat(DocNo,' ',CustomerID))/LineTotal),'#,##0.00')" />
											</TD>
										</xsl:when>
										<xsl:otherwise>
											<TD class="GroupHeaderRed">
												<xsl:value-of  select="format-number(sum(key('contacts-by-docno',concat(DocNo,' ',CustomerID))/LineTotal),'#,##0.00')" />
											</TD>
										</xsl:otherwise>
									</xsl:choose>
								</TR>


							</xsl:if>
						</xsl:for-each >


						<TR Class="GroupFooter">
							<TD ColSpan="6">
								ยอดขายรวม Van : <xsl:value-of select="WHID" />
							</TD>
							<TD Class="subTotals">
								<xsl:value-of select="format-number(sum(key('contacts-by-whid',WHID)/LineTotal),'#,##0.00')"/>
							</TD>
						</TR >
						<TR></TR>
					</xsl:for-each>

					<TR Class="GroupFooter">
						<TD ColSpan="6">ยอดขายรวมทั้งสิ้น</TD>
						<TD Class="subTotals">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Sales_By_Customer_XSLT/LineTotal),"#,##0.00")'/>
						</TD>
					</TR >
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
	
</xsl:stylesheet>