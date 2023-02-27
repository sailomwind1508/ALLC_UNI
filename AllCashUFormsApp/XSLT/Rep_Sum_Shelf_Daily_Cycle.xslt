<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kUsers" match="Day" use="."/>
	<xsl:key name="kRowByName" match="Rep_Sum_Shelf_Daily_XSLT_Cycle" use="WHID"/>

	<xsl:variable name="days" select=
      "//Day
      [generate-id()
      =
      generate-id(key('kUsers', .)[1])
      ]
      "/>

	<xsl:decimal-format name="foo"
grouping-separator="'" digit="#" zero-digit="0" decimal-separator="."/>
	
	<xsl:template match="NewDataSet">
		<html>
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
			<table>
				<Worksheet Name="รายวัน">
					<TR>
						<TD class="stdPageHdr" colspan="6">
							<xsl:value-of select="Rep_Sum_Shelf_Daily_XSLT_Cycle/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="6">
							รายงานสรุปShelfสินค้า(รายวัน)
						</TD>
					</TR>
					<TR>
						<TD Colspan="6"></TD>
					</TR>
					<TR>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;">
							Depot :
						</TD>
						<TD class="SearchResultItem" Colspan="3" style="text-align: center;">
							<xsl:value-of select="Rep_Sum_Shelf_Daily_XSLT_Cycle/BranchName" />
						</TD>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							ปี :
						</TD>
						<TD class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_Sum_Shelf_Daily_XSLT_Cycle/YearBE" />
						</TD>
					</TR>
					<TR>
						<TD Colspan="6"></TD>
					</TR>
					<thead>
						<th class="SearchResultItem">ลำดับ</th>
						<th class="SearchResultItem">แวน</th>
						<th class="SearchResultItem">ชื่อร้านค้า</th>
						<th class="SearchResultItem">ชื่อสกุล</th>
						<th class="SearchResultItem">รหัสพนักงาน</th>
						<th class="SearchResultItem">จำนวนร้านทั้งหมด</th>
						<xsl:apply-templates select="$days"/>
						<th class="SearchResultItem">Total</th>
					</thead>
					<tbody>
						<xsl:apply-templates select=
                "//Rep_Sum_Shelf_Daily_XSLT_Cycle
                [generate-id() = 
                 generate-id(key('kRowByName', WHID))]">
							<xsl:sort select="WHID"/>
						</xsl:apply-templates>

						<!--<tr>
							<td ColSpan="5" Class="SearchResultItem" align="Right">
								รวมทั้งหมด

							</td>
							<td Class="SearchResultItem" align="Right">
								<xsl:value-of select="format-number(Rep_Sum_Shelf_Daily_XSLT_Cycle/TotalCountCus,'#,##0')" />
							</td>
							<xsl:apply-templates select="$days" mode="row">
								<xsl:with-param name="nRows" select="key('kRowByName', WHID)"/>
							</xsl:apply-templates>

							<td class="SearchResultItem">
								<xsl:value-of select="format-number(sum(Rep_Sum_Shelf_Daily_XSLT_Cycle/CouShef),'#,##0')"/>
							</td>
						</tr>-->
					</tbody>
				</Worksheet>
			</table>
		</html>
	</xsl:template>

	<xsl:template match="Day">
		<th class="SearchResultItem">
			<xsl:value-of select="."/>
		</th>
	</xsl:template>
	<xsl:template match="Rep_Sum_Shelf_Daily_XSLT_Cycle">
		<tr>
			<td class="SearchResultItem" align="Left">
				<xsl:value-of select="position()"/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select="WHID"/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select="WHName"/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select="EmpName"/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select="EmpIDCard"/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select="format-number(CoutCus,'#,##0')"/>
			</td>
			<xsl:apply-templates select="$days" mode="row">
				<xsl:with-param name="nRows" select="key('kRowByName', WHID)"/>
			</xsl:apply-templates>
			<td class="SearchResultItem">
				<xsl:value-of select="format-number(TotalCouShef,'#,##0')"/>
			</td>
		</tr>
	</xsl:template>
	<xsl:template match="Day" mode="row">
		<xsl:param name="nRows"/>
		<td class="SearchResultItem" align="Right">
			<xsl:if test="$nRows[Day=current()]/CouShef=0">
				-
			</xsl:if>
			<xsl:if test="$nRows[Day=current()]/CouShef>0">
				<xsl:value-of select="format-number($nRows[Day=current()]/CouShef,'#,##0')"/>
			</xsl:if>
		</td>
	</xsl:template>


</xsl:stylesheet>
