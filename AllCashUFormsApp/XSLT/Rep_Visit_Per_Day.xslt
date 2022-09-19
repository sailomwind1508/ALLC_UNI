<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kCols1" match="Date" use="."/>

	<xsl:key name="kRows" match="Rep_Visit_Per_Day_XSLT" use="WHID"/>

	<xsl:variable name="colIds1" select=
      "//Date
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
					width: 100px;
					}
					.ResultItem {
					background-color: #FFFFFF;
					color: Black;
					border: solid thin Black;
					font-size: 10pt;
					width: 70px;
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
						<TD class="stdPageHdr" colspan="11">
							<xsl:value-of select="Rep_Visit_Per_Day_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="11">
							รายงานร้านเยี่ยมเฉลี่ยต่อวัน
						</TD>
					</TR>
					<TR>
						<TD Colspan="11"></TD>
					</TR>
					<TR>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;">
							Depot :
						</TD>
						<TD class="SearchResultItem" Colspan="3" style="text-align: center;">
							<xsl:value-of select="Rep_Visit_Per_Day_XSLT/BranchName" />
						</TD>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							Date :
						</TD>
						<TD colspan="3" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_Visit_Per_Day_XSLT/HDate" />
						</TD>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							Today :
						</TD>
						<TD colspan="2" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_Visit_Per_Day_XSLT/CurentDate" />
						</TD>
					</TR>
					<TR>
						<TD Colspan="11"></TD>
					</TR>
					<thead>
						<th class="ResultItem"></th>
						<th class="SearchResultItem"></th>
						<th class="SearchResultItem"></th>
						<th class="ResultItem"></th>
						<xsl:apply-templates select="$colIds1"/>
						<th colspan="3" class="ResultItem">Total</th>
					</thead>
					<tbody colspan="3">
						<xsl:apply-templates select=
                "//Rep_Visit_Per_Day_XSLT
                [generate-id() = 
                 generate-id(key('kRows', WHID))]">
							<xsl:sort select="WHID" />
							
						</xsl:apply-templates>

					</tbody>
				</Worksheet>
			</table>
		</html>
	</xsl:template>

	<xsl:template match="Date">
		<th class="SearchResultItem" colspan="3">
			<xsl:value-of select="."/>
		</th>
	</xsl:template>


	<xsl:template match="Rep_Visit_Per_Day_XSLT">
		<tr>
			<td class="ResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:if test='WHID="0Van"'>
					Van
				</xsl:if>
				<xsl:if test='WHID!="0Van"'>
					<xsl:value-of select="WHID"/>
				</xsl:if>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="Name"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="EmpIDCard"/>
			</td>
			<td class="ResultItem">
				<xsl:choose>
					<xsl:when test="not(number(WorkDay))">
						<xsl:value-of select='WorkDay'/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select='number(WorkDay)'/>
					</xsl:otherwise>
				</xsl:choose>
			</td>

			<xsl:apply-templates select="$colIds1" mode="row">
				<xsl:with-param name="nRows" select="key('kRows', WHID)"/>
			</xsl:apply-templates>


			<td class="ResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select='TotalVisit'/>
			</td>

			<td class="ResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select='TotalPO'/>
			</td>

			<td class="ResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select='TotalPercen'/>%
			</td>
		</tr>
	</xsl:template>


	<xsl:template match="Date" mode="row">
		<xsl:param name="nRows"/>
		
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[Date=current()]/VISIT))">
					<xsl:value-of select='$nRows[Date=current()]/VISIT'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[Date=current()]/VISIT)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[Date=current()]/PO))">
					<xsl:value-of select='$nRows[Date=current()]/PO'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[Date=current()]/PO)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[Date=current()]/Percen))">
					<xsl:value-of select='$nRows[Date=current()]/Percen'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[Date=current()]/Percen)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
		
	</xsl:template>

</xsl:stylesheet>
