<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kCols" match="ProductShortName" use="."/>

	<xsl:key name="kRows" match="Rep_DSR_EffectiveCall_SKU_TEST_XSLT" use="EmpName"/>

	<xsl:variable name="colIds" select=
      "//ProductShortName
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
					.ResultItem3 {
					background-color: #FFFFFF;
					color: Black;
					border: solid thin Black;
					font-size: 10pt;
					width: 200px;
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
							<xsl:value-of select="Rep_DSR_EffectiveCall_SKU_TEST_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="13">
							รายงาน Eff.Call (SKU)
						</TD>
					</TR>
					<TR>
						<TD Colspan="13"></TD>
					</TR>
					<TR>
						<TD class="GroupHeader" style="font-weight: bold;text-align: center;">
							Depot :
						</TD>
						<TD class="GroupHeader" Colspan="4" style="text-align: center;">
							<xsl:value-of select="Rep_DSR_EffectiveCall_SKU_TEST_XSLT/BranchName" />
						</TD>
						
						<TD colspan="2" class="GroupHeader" style="font-weight: bold;text-align: center;" align="Center">
							วันที่ :
						</TD>
						<TD colspan="4" class="GroupHeader" style="text-align: center;">
							<xsl:value-of select="Rep_DSR_EffectiveCall_SKU_TEST_XSLT/HDate" />
						</TD>
					</TR>
					<TR>
						<TD Colspan="13"></TD>
					</TR>
					<thead>
						<th class="gridHeader"></th>
						<th class="gridHeader"></th>
						<th class="gridHeader"></th>
						<th class="gridHeader"></th>
						<th class="gridHeader"></th>
						<xsl:apply-templates select="$colIds">

					
							<xsl:sort select="substring-after(@ProductGroupID,' ')" data-type ="number" order="ascending" />
							<xsl:sort select="substring-after(@ProductSubGroupID,' ')" data-type ="number" order="ascending" />
							<xsl:sort select="@Seq" data-type="number" order="ascending" />
						</xsl:apply-templates>
						
					

					</thead>
					<tbody colspan="6">
						<xsl:apply-templates select=
                "//Rep_DSR_EffectiveCall_SKU_TEST_XSLT
                [generate-id() = 
                 generate-id(key('kRows', EmpName))]">
							<xsl:sort select="WHID"/>
						</xsl:apply-templates>
					</tbody>
				</Worksheet>
			</table>
		</html>
	</xsl:template>

	<xsl:template match="ProductShortName">
		<th class="gridHeader" colspan="6">
			<xsl:value-of select="."/>
		</th>
	</xsl:template>


	<xsl:template match="Rep_DSR_EffectiveCall_SKU_TEST_XSLT">
		<tr>

			<xsl:choose>
				<xsl:when test='WHID="0Name"'>
					<td class="ResultItem" style="font-weight: bold;text-align: center;" align="Center">
						<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
						<p style="font-weight: bold;background-color: #DDEEFF;color: DarkBlue;">แวน</p>
					</td>
				</xsl:when>
				<xsl:otherwise>
					<td class="ResultItem">
						<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
						<xsl:value-of select="WHID"/>
					</td>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test='EmpName="0Name" or EmpName="ฮ0" or EmpName="ฮ1" or EmpName="ฮ2"'>
					<td class="ResultItem3" style="font-weight: bold;text-align: center;" align="Center">
						<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
						<p style="font-weight: bold;background-color: #DDEEFF;color: DarkBlue;">พนักงาน</p>
					</td>
				</xsl:when>
				<xsl:otherwise>
					<td class="ResultItem3">
						<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
						<xsl:value-of select="EmpName"/>
					</td>
				</xsl:otherwise>
			</xsl:choose>
			<td class="ResultItem" style="font-weight: bold;text-align: center;" align="Center">
				
				<xsl:choose>
					<xsl:when test="not(number(CustList))">
						<p style="font-weight: bold;background-color: #DDEEFF;color: DarkBlue;">
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of select='CustList'/>
						</p>
					</xsl:when>
					<xsl:otherwise>
						<p style="font-weight: normal;">
							<xsl:value-of select="format-number(number(CustList),'#,##0')"/>
						</p>
					</xsl:otherwise>
				</xsl:choose>
			</td>
			<td class="ResultItem" style="font-weight: bold;text-align: center;" align="Center">
				<xsl:choose>
					<xsl:when test="not(number(TotVisited))">
						<p style="font-weight: bold;background-color: #DDEEFF;color: DarkBlue;">
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of select='TotVisited'/>
						</p>
					</xsl:when>
					<xsl:otherwise>
						<p style="font-weight: normal;">
							<xsl:value-of select="format-number(number(TotVisited),'#,##0')"/>
						</p>
					</xsl:otherwise>
				</xsl:choose>

			</td>
			<td class="ResultItem" style="font-weight: bold;text-align: center;" align="Center">
				<xsl:choose>
					<xsl:when test="not(number(TotBought))">
						<p style="font-weight: bold;background-color: #DDEEFF;color: DarkBlue;">
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of select='TotBought'/>
						</p>
					</xsl:when>
					<xsl:otherwise>
						<p style="font-weight: normal;">
							<xsl:value-of select="format-number(number(TotBought),'#,##0')"/>
						</p>
					</xsl:otherwise>
				</xsl:choose>

			</td>


			<xsl:apply-templates select="$colIds" mode="row">
				<xsl:with-param name="nRows" select="key('kRows', EmpName)"/>
			</xsl:apply-templates>


		</tr>
	</xsl:template>


	<xsl:template match="ProductShortName" mode="row">
		<xsl:param name="nRows"/>

		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductShortName=current()]/Bought))">
					<p style="font-weight: bold;background-color: #DDEEFF;color: DarkBlue;">
						<xsl:value-of select='$nRows[ProductShortName=current()]/Bought'/>
					</p>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[ProductShortName=current()]/Bought)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductShortName=current()]/CarQty))">
					<p style="font-weight: bold;background-color: #DDEEFF;color: DarkBlue;">
						<xsl:value-of select='$nRows[ProductShortName=current()]/CarQty'/>
					</p>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[ProductShortName=current()]/CarQty)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductShortName=current()]/PckQty))">
					<p style="font-weight: bold;background-color: #DDEEFF;color: DarkBlue;">
						<xsl:value-of select='$nRows[ProductShortName=current()]/PckQty'/>
					</p>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[ProductShortName=current()]/PckQty)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductShortName=current()]/CarUnitPrice))">
					<p style="font-weight: bold;background-color: #DDEEFF;color: DarkBlue;">
						<xsl:value-of select='$nRows[ProductShortName=current()]/CarUnitPrice'/>
					</p>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="format-number(number($nRows[ProductShortName=current()]/CarUnitPrice),'#,##0.00')"/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductShortName=current()]/PckUnitPrice))">
					<p style="font-weight: bold;background-color: #DDEEFF;color: DarkBlue;">
						<xsl:value-of select='$nRows[ProductShortName=current()]/PckUnitPrice'/>
					</p>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="format-number(number($nRows[ProductShortName=current()]/PckUnitPrice),'#,##0.00')"/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductShortName=current()]/LineTotal))">
					<p style="font-weight: bold;background-color: #DDEEFF;color: DarkBlue;">
						<xsl:value-of select='$nRows[ProductShortName=current()]/LineTotal'/>
					</p>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="format-number(number($nRows[ProductShortName=current()]/LineTotal),'#,##0.00')"/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
	</xsl:template>

</xsl:stylesheet>
