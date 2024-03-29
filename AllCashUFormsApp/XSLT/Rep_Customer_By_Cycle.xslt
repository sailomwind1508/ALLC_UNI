﻿<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kCols" match="Cycle" use="."/>
	<xsl:key name="kRows" match="Rep_Customer_By_Cycle_XSLT" use="WHID"/>

	<xsl:variable name="cIds" select=
      "//Cycle
      [generate-id()
      =
      generate-id(key('kCols', .)[1])
      ]
      "/>

	<xsl:decimal-format name="foo" grouping-separator="'" digit="#" zero-digit="0" decimal-separator="."/>

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
						<TD class="stdPageHdr" colspan="5">
							<xsl:value-of select="Rep_Customer_By_Cycle_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="7">
							รายงานจำนวนร้านค้า ตามรอบการขาย
						</TD>
					</TR>
					<TR>
						<TD Colspan="4"></TD>
					</TR>
					<TR>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;">
							Depot :
						</TD>
						<TD class="SearchResultItem" Colspan="3" style="text-align: center;">
							<xsl:value-of select="Rep_Customer_By_Cycle_XSLT/BranchName" />
						</TD>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							Year :
						</TD>
						<TD class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_Customer_By_Cycle_XSLT/Year" />
						</TD>
					</TR>
					<TR>
						<TD Colspan="4"></TD>
					</TR>
					<thead>
						<th class="SearchResultItem">Van</th>
						<xsl:apply-templates select="$cIds"/>
						<th class="SearchResultItem">Total</th>
						<th class="SearchResultItem">Avg./Cycle</th>
					</thead>
					<tbody>
						<xsl:apply-templates select=
                "//Rep_Customer_By_Cycle_XSLT
                [generate-id() = 
                 generate-id(key('kRows', WHID))]">
							<xsl:sort select="WHID"/>
						</xsl:apply-templates>

					</tbody>
				</Worksheet>
			</table>
		</html>
	</xsl:template>

	<xsl:template match="Cycle">
		<th class="SearchResultItem">
			<xsl:value-of select="."/>
		</th>
	</xsl:template>

	<xsl:template match="Rep_Customer_By_Cycle_XSLT">
		<tr>
			<td class="SearchResultItem">
				<xsl:if test="WHID=88888888">
					<xsl:value-of select="CountVAN"/>
				</xsl:if>
				<xsl:if test="WHID=99999999">
					Avg./Van
				</xsl:if>
				<xsl:if test="WHID!=88888888 and WHID!=99999999">
					<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
					<xsl:value-of select="WHID"/>
				</xsl:if>
			</td>
			
			<xsl:apply-templates select="$cIds" mode="row">
				<xsl:with-param name="nRows" select="key('kRows', WHID)"/>
			</xsl:apply-templates>

			<td class="SearchResultItem">
				<xsl:value-of select='format-number(TotalCust,"###,###.00")'/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select='format-number(AVG,"###,###.00")'/>
			</td>
		</tr>
	</xsl:template>


	<xsl:template match="Cycle" mode="row">
		<xsl:param name="nRows"/>
		<td class="SearchResultItem" align="Right">
			<xsl:if test='$nRows[Cycle=current()]/Cust=0'>
				-
			</xsl:if>
			<xsl:if test="$nRows[Cycle=current()]/Cust>0">
				<xsl:value-of select='format-number($nRows[Cycle=current()]/Cust,"###,###.00")'/>
			</xsl:if>
		</td>
	</xsl:template>


</xsl:stylesheet>
