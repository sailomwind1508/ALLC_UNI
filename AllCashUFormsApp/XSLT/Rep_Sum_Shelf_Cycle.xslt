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

	<xsl:key name="contacts-by-whid" match="Rep_Sum_Shelf_XSLT_Cycle" use="WHID" />
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
				<Worksheet Name="รายเดือน">
					<TABLE>
						<TR>
							<TD class="stdPageHdr" colspan="19">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/CompanyName" />
							</TD>
						</TR>
						<TR>
							<TD class="stdPageHdr" Colspan="19">
								รายงานสรุปShelfสินค้า(รายเดือน)
							</TD>
						</TR>
						<TR>
							<TD Colspan="19"></TD>
						</TR>
						<TR>
							<TD class="SearchResultItem" style="font-weight: bold;text-align: center;">
								Depot :
							</TD>
							<TD class="SearchResultItem" Colspan="3" style="text-align: center;">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/BranchName" />
							</TD>
							<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
								ปี :
							</TD>
							<TD class="SearchResultItem" style="text-align: center;">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearBE" />
							</TD>
						</TR>
						<TR>
							<TD Colspan="19"></TD>
						</TR>
						<TR>
							<TD CLASS="gridHeader" WIDTH="80">ลำดับ</TD>
							<TD CLASS="gridHeader" WIDTH="80">แวน</TD>
							<TD CLASS="gridHeader" WIDTH="150">ชื่อร้านค้า</TD>
							<TD CLASS="gridHeader" WIDTH="150">ชื่อสกุล</TD>
							<TD CLASS="gridHeader" WIDTH="100">รหัสพนักงาน</TD>
							<TD CLASS="gridHeader" WIDTH="100">จำนวนร้านค้าทั้งหมด</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearCE" />01
							</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearCE" />02
							</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearCE" />03
							</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearCE" />04
							</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearCE" />05
							</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearCE" />06
							</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearCE" />07
							</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearCE" />08
							</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearCE" />09
							</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearCE" />10
							</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearCE" />11
							</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearCE" />12
							</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								<xsl:value-of select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle/YearCE" />13
							</TD>
							<TD CLASS="gridHeader" WIDTH="80">
								Total
							</TD>
						</TR>
						<xsl:for-each select="NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid', WHID)]">
							<xsl:sort select="WHID" />
							<TR>
								<TD class="SearchResultItem">
									<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
									<xsl:value-of select="position()" />
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="WHID"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="WHName"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="EmpName"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="EmpIDCard"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(TotalCust,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(M1,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(M2,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(M3,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(M4,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(M5,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(M6,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(M7,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(M8,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(M9,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(M10,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(M11,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(M12,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(M13,'#,##0')"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="format-number(MTotal,'#,##0')"/>
								</TD>
							</TR >
						</xsl:for-each>
						<TR>
							<TD ColSpan="5" Class="SearchResultItem" align="Right">
								รวมทั้งหมด

							</TD>
							<TD Class="SearchResultItem" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/TotalCust),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/M1),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/M2),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/M3),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/M4),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/M5),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/M6),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/M7),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/M8),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/M9),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/M10),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/M11),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/M12),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/M13),'#,##0')" />
							</TD>
							<TD Class="SearchResultItem" align="Right">
								<xsl:value-of  select="format-number(sum(NewDataSet/Rep_Sum_Shelf_XSLT_Cycle[key('contacts-by-whid',WHID)]/MTotal),'#,##0')" />
							</TD>
						</TR>
					</TABLE>
				</Worksheet>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>