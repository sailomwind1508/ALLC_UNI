<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kCols" match="ProductSubGroupName" use="."/>

	<xsl:key name="kRows" match="Rep_DSR_EffectiveCall_KPI_XSLT" use="EmpName"/>

	<xsl:variable name="colIds" select=
      "//ProductSubGroupName
      [generate-id()
      =
      generate-id(key('kCols', .)[1])
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
					width: 130px;
					}
					.ResultItem {
					background-color: #FFFFFF;
					color: Black;
					border: solid thin Black;
					font-size: 10pt;
					width: 60px;
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
						<TD class="stdPageHdr" colspan="13">
							<xsl:value-of select="Rep_DSR_EffectiveCall_KPI_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="13">
							รายงาน Eff.Call (KPI)
						</TD>
					</TR>
					<TR>
						<TD Colspan="13"></TD>
					</TR>
					<TR>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;">
							BB :
						</TD>
						<TD class="SearchResultItem" Colspan="4" style="text-align: center;">
							<xsl:value-of select="Rep_DSR_EffectiveCall_KPI_XSLT/BranchName" />
						</TD>
						<TD colspan="2" class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							รอบที่ :
						</TD>
						<TD colspan="2" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_DSR_EffectiveCall_KPI_XSLT/Round" />
						</TD>
						<TD colspan="2" class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							วันที่ :
						</TD>
						<TD colspan="4" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_DSR_EffectiveCall_KPI_XSLT/HDate" />
						</TD>
					</TR>
					<TR>
						<TD Colspan="13"></TD>
					</TR>
					<thead>
						<th class="ResultItem"></th>
						<th class="ResultItem"></th>
						<th class="ResultItem"></th>
						<th class="ResultItem"></th>
						<xsl:apply-templates select="$colIds"/>


					</thead>
					<tbody colspan="4">
						<xsl:apply-templates select=
                "//Rep_DSR_EffectiveCall_KPI_XSLT
                [generate-id() = 
                 generate-id(key('kRows', EmpName))]">
							<xsl:sort select="WHID"/>
						</xsl:apply-templates>

					</tbody>
				</Worksheet>
			</table>
		</html>
	</xsl:template>

	<xsl:template match="ProductSubGroupName">
		<th class="SearchResultItem" colspan="4">
			<xsl:value-of select="."/>
		</th>
	</xsl:template>


	<xsl:template match="Rep_DSR_EffectiveCall_KPI_XSLT">
		<tr>


			<xsl:choose>
				<xsl:when test='EmpName="0Name" or EmpName="ฮ0" or EmpName="ฮ1"'>
					<td class="ResultItem" style="font-weight: bold;text-align: center;" align="Center">
						<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
						<xsl:value-of select="WHID"/>
					</td>
				</xsl:when>
				<xsl:otherwise>
					<td class="ResultItem">
						<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
						<xsl:value-of select="EmpName"/>
					</td>
				</xsl:otherwise>
			</xsl:choose>
			<td class="ResultItem2">
				<xsl:choose>
					<xsl:when test="not(number(TotVisited))">
						<xsl:value-of select='TotVisited'/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select='number(TotVisited)'/>
					</xsl:otherwise>
				</xsl:choose>
			</td>
			<td class="ResultItem2">
				<xsl:choose>
					<xsl:when test="not(number(TotBought))">
						<xsl:value-of select='TotBought'/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select='number(TotBought)'/>
					</xsl:otherwise>
				</xsl:choose>

				
			</td>
			<td class="ResultItem2">
				<xsl:choose>
					<xsl:when test="not(number(Eff))">
						<xsl:value-of select='Eff'/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select='number(Eff)'/>%
					</xsl:otherwise>
				</xsl:choose>
			</td>

			<xsl:apply-templates select="$colIds" mode="row">
				<xsl:with-param name="nRows" select="key('kRows', EmpName)"/>
			</xsl:apply-templates>

		</tr>
	</xsl:template>


	<xsl:template match="ProductSubGroupName" mode="row">
		<xsl:param name="nRows"/>

		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductSubGroupName=current()]/CustList))">
					<xsl:value-of select='$nRows[ProductSubGroupName=current()]/CustList'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[ProductSubGroupName=current()]/CustList)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductSubGroupName=current()]/Bought))">
					<xsl:value-of select='$nRows[ProductSubGroupName=current()]/Bought'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[ProductSubGroupName=current()]/Bought)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductSubGroupName=current()]/Perc))">
					<xsl:value-of select='$nRows[ProductSubGroupName=current()]/Perc'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[ProductSubGroupName=current()]/Perc)'/>%
				</xsl:otherwise>
			</xsl:choose>

		</td>
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductSubGroupName=current()]/PlusDel))">
					<xsl:value-of select='$nRows[ProductSubGroupName=current()]/PlusDel'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[ProductSubGroupName=current()]/PlusDel)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
	</xsl:template>

</xsl:stylesheet>
