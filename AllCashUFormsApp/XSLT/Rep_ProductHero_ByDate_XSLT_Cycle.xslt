<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kCDate" match="CDate" use="."/>
	<xsl:key name="kRowByName" match="Rep_ProductHero_ByDate_Cycle" use="WHID"/>

	<xsl:variable name="days" select=
      "//CDate
      [generate-id()
      =
      generate-id(key('kCDate', .)[1])
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
						<TD class="stdPageHdr" colspan="4">
							<xsl:value-of select="Rep_ProductHero_ByDate_Cycle/CompanyName" />
						</TD>
					</TR>
					
					<TR>
						<TD class="stdPageHdr" Colspan="4">
							รายงาน Product Hero
						</TD>
					</TR>

					<TR>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;">
							Depot :
						</TD>
						<TD class="SearchResultItem" Colspan="3" style="text-align: center;">
							<xsl:value-of select="Rep_ProductHero_ByDate_Cycle/Depot"/>
						</TD>
					</TR>
					
					<TR>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							Year :
						</TD>
						<TD class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_ProductHero_ByDate_Cycle/StartYear" />
						</TD>
						
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							Cycle :
						</TD>
						<TD class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_ProductHero_ByDate_Cycle/StartCycle" />
						</TD>

						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							Year :
						</TD>
						<TD class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_ProductHero_ByDate_Cycle/EndYear" />
						</TD>

						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							Cycle :
						</TD>
						<TD class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_ProductHero_ByDate_Cycle/EndCycle" />
						</TD>
					</TR>
					
					<TR>
						<TD> </TD>
					</TR><!--เว้นวรรทัด-->
					
					<thead>
						<th class="SearchResultItem">Van</th>
						<th class="SearchResultItem">SaleName</th>
						<th class="SearchResultItem">Driver</th>
						<xsl:apply-templates select="$days"/>
						<th class="SearchResultItem">รวม</th>
					</thead> <!--Column-->
					
					<tbody>
						<xsl:apply-templates select= "//Rep_ProductHero_ByDate_Cycle [generate-id() 
							= generate-id(key('kRowByName', WHID))]">
								<xsl:sort select="WHID"/>
						</xsl:apply-templates>
					</tbody>
					
				</Worksheet>
			</table>
		</html>
	</xsl:template>

	<xsl:template match="CDate">
		<th class="SearchResultItem">
			<xsl:value-of select="."/>
		</th>
	</xsl:template>
	
	<xsl:template match="Rep_ProductHero_ByDate_Cycle">
		<tr>
			<td class="SearchResultItem">
				<xsl:value-of select="WHID"/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select="SaleName"/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select="Driver"/>
			</td>
			<xsl:apply-templates select="$days" mode="row">
				<xsl:with-param name="nRows" select="key('kRowByName', WHID)"/>
			</xsl:apply-templates>
			<td class="SearchResultItem">
				<xsl:value-of select="format-number(Qty,'#,##0')"/>
			</td>
		</tr>
	</xsl:template>
	
	<xsl:template match="CDate" mode="row">
		<xsl:param name="nRows"/>
		
		<xsl:if test="$nRows[CDate=current()]/Qty=0">
			<td class="SearchResultItem" align="center">
				-
			</td>
		</xsl:if>
		
		<xsl:if test="$nRows[CDate=current()]/Qty>0">
			<td class="SearchResultItem" align="Right">
				<xsl:value-of select="format-number($nRows[CDate=current()]/Qty,'#,##0')"/>
			</td>
		</xsl:if>
		
	
	</xsl:template>


</xsl:stylesheet>
