<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

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

	<xsl:decimal-format name="foo" grouping-separator="'" digit="#" zero-digit="0" decimal-separator="."/>
	
	<xsl:key name="contacts-by-whid" match="proc_RPTStock_XSLT" use="WHID" />
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
					font-size: 12pt;
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
					background-color: #AFD118;
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
					.GroupFooter{
					color: Black;
					font-weight: bold;
					font-style:Regular;
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
							รายงานสินค้าคงเหลือรวม สาขา<xsl:value-of select="NewDataSet/proc_RPTStock_XSLT/BranchName" />
						</TD>
					</TR>
					<TR>
						<TD class="SearchKey">พิมพ์วันที่</TD>
						<TD class="SearchValue" Colspan="6">
							<xsl:value-of select="NewDataSet/proc_RPTStock_XSLT/PrintDate" />
						</TD>
					</TR>

					<!--<xsl:for-each select="NewDataSet/proc_RPTStock_XSLT[count(. | key('contacts-by-whid', WHID)[1]) = 1]">-->
						<TR>
							<TD class="GroupHeader">
								คลัง :
							</TD>
							<TD class="GroupHeader" colspan="7">
								<xsl:value-of select="NewDataSet/proc_RPTStock_XSLT/WHID" />
							</TD>
							<!--<TD class="GroupHeader">
                <xsl:value-of select="WHName" />
              </TD>-->
						</TR>
						<TR>
							<TD class="GroupHeader">
								พนักงาน :
							</TD>
							<TD class="GroupHeader" colspan="7">
								<xsl:value-of select="NewDataSet/proc_RPTStock_XSLT/Emp" />
							</TD>

						</TR>
						<TR>
							<TD CLASS="gridHeader">ลำดับ</TD>
							<TD CLASS="gridHeader" WIDTH="80">รหัสสินค้า</TD>
							<TD CLASS="gridHeader" WIDTH="300">ชื่อสินค้า</TD>
							<TD CLASS="gridHeader" WIDTH="80">หน่วยใหญ่</TD>
							<TD CLASS="gridHeader" WIDTH="80"></TD>
							<TD CLASS="gridHeader" WIDTH="80">หน่วยย่อย</TD>
							<TD CLASS="gridHeader" WIDTH="80"></TD>
							<TD CLASS="gridHeader" WIDTH="80">มูลค่าคงเหลือ</TD>
						</TR>
						<xsl:for-each select="NewDataSet/proc_RPTStock_XSLT[key('contacts-by-whid', WHID)]">
							<xsl:sort select="WHID" />
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
									<xsl:value-of select="CarQty"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="CarQtyStr"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="PckQty"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="PckQtyStr"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(TotalPrice,'#,##0.00')"/>
								</TD>
							</TR >
						</xsl:for-each>
						<TR>
							<TD ColSpan="3" Class="GroupFooter">
								รวมคลัง(<xsl:value-of select="NewDataSet/proc_RPTStock_XSLT/WHID"/>)
								<xsl:value-of  select="format-number(count(NewDataSet/proc_RPTStock_XSLT[key('contacts-by-whid',WHID)]/ProductID),'#,##0')" />  สินค้า
							</TD>
							<TD Class="GroupFooter" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/proc_RPTStock_XSLT[key('contacts-by-whid',WHID)]/CarQty),'#,##0')" />
							</TD>
							<TD Class="GroupFooter"></TD>
							<TD Class="GroupFooter" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/proc_RPTStock_XSLT[key('contacts-by-whid',WHID)]/PckQty),'#,##0.00')" />
							</TD>
							<TD Class="GroupFooter"></TD>
							<TD Class="GroupFooter" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/proc_RPTStock_XSLT[key('contacts-by-whid',WHID)]/TotalPrice),'#,##0.00')" />
							</TD>
						</TR>
						<TR>
							<TD ColSpan="7" Class="GroupFooter"></TD>
							<TD Class="GroupFooter" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/proc_RPTStock_XSLT[key('contacts-by-whid',WHID)]/TotalPrice)*NewDataSet/proc_RPTStock_XSLT/VatRate,'#,##0.00')" />
							</TD>
						</TR>
						<TR>
							<TD ColSpan="7" Class="GroupFooter"></TD>
							<TD Class="GroupFooter" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/proc_RPTStock_XSLT[key('contacts-by-whid',WHID)]/TotalPrice) + sum(NewDataSet/proc_RPTStock_XSLT[key('contacts-by-whid',WHID)]/TotalPrice)*NewDataSet/proc_RPTStock_XSLT/VatRate,'#,##0.00')" />
							</TD>
						</TR>
					<!--</xsl:for-each >-->


				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>
