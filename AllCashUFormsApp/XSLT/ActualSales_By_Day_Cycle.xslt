<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kCols1" match="Day" use="."/>

	<xsl:key name="kRows" match="proc_Actualsales_by_day_XSLT_Cycle" use="WHID"/>

	<xsl:variable name="colIds1" select=
      "//Day
      [generate-id()
      =
      generate-id(key('kCols1', .)[1])
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
					width: 100%;
					}
					.ResultItem2 {
					background-color: #FFFFFF;
					color: Black;
					border: solid thin Black;
					font-size: 10pt;
					width: 80px;
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
						<TD class="stdPageHdr" colspan="19">
							<xsl:value-of select="proc_Actualsales_by_day_XSLT_Cycle/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="19">
							รายงานยอดขาย แยกตามแวนและวันที่
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
							<xsl:value-of select="proc_Actualsales_by_day_XSLT_Cycle/BranchName" />
						</TD>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							เดือน :
						</TD>
						<TD class="SearchResultItem" style="text-align: center;">
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of select="proc_Actualsales_by_day_XSLT_Cycle/YearBE" />
						</TD>
					</TR>
					<TR>
						<TD Colspan="11"></TD>
					</TR>
					<thead>
						<th class="SearchResultItem">แวน</th>
						<th class="SearchResultItem">พนักงาน</th>
						<xsl:apply-templates select="$colIds1"/>
						<th class="SearchResultItem">Total</th>
					</thead>
					<tbody>
						<xsl:apply-templates select=
                "//proc_Actualsales_by_day_XSLT_Cycle
                [generate-id() = 
                 generate-id(key('kRows', WHID))]">
							<xsl:sort select="WHID"/>
						</xsl:apply-templates>

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


	<xsl:template match="proc_Actualsales_by_day_XSLT_Cycle">
		<tr>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:if test='WHID="99999"'>
					
				</xsl:if>
				<xsl:if test='WHID!="99999"'>
					<xsl:value-of select="WHID"/>
				</xsl:if>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="salesName"/>
			</td>
			<xsl:apply-templates select="$colIds1" mode="row">
				<xsl:with-param name="nRows" select="key('kRows', WHID)"/>
			</xsl:apply-templates>


			<td class="SearchResultItem">
				<xsl:value-of select='format-number(LineTotal,"###,###.00")'/>
			</td>

			
		</tr>
	</xsl:template>


	<xsl:template match="Day" mode="row">
		<xsl:param name="nRows"/>

		<td class="ResultItem2" align="Right">

			<xsl:if test="$nRows[Day=current()]/TotalDue=0">
				-
			</xsl:if>
			<xsl:if test="$nRows[Day=current()]/TotalDue>0">
				<xsl:value-of select='format-number($nRows[Day=current()]/TotalDue,"###,###.00")'/>
			</xsl:if>
		</td>
		
		
	</xsl:template>

</xsl:stylesheet>
