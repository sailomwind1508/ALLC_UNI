<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kCols" match="ProductSubGroupName" use="."/>

	<xsl:key name="kRows" match="proc_DSR_Sales_Summary_XSLT" use="EmpName"/>

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
							<xsl:value-of select="proc_DSR_Sales_Summary_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="13">
							รายงานสรุปยอดขาย
						</TD>
					</TR>
					<TR>
						<TD Colspan="13"></TD>
					</TR>
					<TR>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;">
							Depot :
						</TD>
						<TD class="SearchResultItem" Colspan="4" style="text-align: center;">
							<xsl:value-of select="proc_DSR_Sales_Summary_XSLT/BranchName" />
						</TD>
						<TD colspan="2" class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							รอบที่ :
						</TD>
						<TD colspan="2" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="proc_DSR_Sales_Summary_XSLT/CurentDate" />
						</TD>
						<TD colspan="2" class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							วันที่ :
						</TD>
						<TD colspan="4" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="proc_DSR_Sales_Summary_XSLT/HDate" />
						</TD>
					</TR>
					<TR>
						<TD Colspan="13"></TD>
					</TR>
					<thead>
						<th class="ResultItem"></th>
						<xsl:apply-templates select="$colIds"/>
						<th colspan="4" class="ResultItem2">ยอดขายรวม (หีบ)</th>
						<th colspan="4" class="ResultItem2">ยอดขายรวม (บาท) - Vat</th>

					</thead>
					<tbody colspan="4">
						<xsl:apply-templates select=
                "//proc_DSR_Sales_Summary_XSLT
                [generate-id() = 
                 generate-id(key('kRows', EmpName))]">
							<xsl:sort select="EmpName"/>
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


	<xsl:template match="proc_DSR_Sales_Summary_XSLT">
		<tr>


			<xsl:choose>
				<xsl:when test='EmpName="0Name" or EmpName="ฮ0" or EmpName="ฮ1" or EmpName="ฮ2"'>
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



			<xsl:apply-templates select="$colIds" mode="row">
				<xsl:with-param name="nRows" select="key('kRows', EmpName)"/>
			</xsl:apply-templates>


			<td class="ResultItem2">
				<xsl:choose>
					<xsl:when test="not(number(TotalTargetQty))">
						<xsl:value-of select='TotalTargetQty'/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select='number(TotalTargetQty)'/>
					</xsl:otherwise>
				</xsl:choose>
			</td>

			<td class="ResultItem2">


				<xsl:choose>
					<xsl:when test="not(number(TotalSalesQty))">
						<xsl:value-of select='TotalSalesQty'/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select='number(TotalSalesQty)'/>
					</xsl:otherwise>
				</xsl:choose>

			</td>

			<td class="ResultItem2">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>

				<xsl:value-of select='TotalPercentage'/>


			</td>
			<td class="ResultItem2">

				<xsl:choose>
					<xsl:when test="not(number(TotalEstimate))">
						<xsl:value-of select='TotalEstimate'/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select='number(TotalEstimate)'/>
					</xsl:otherwise>
				</xsl:choose>

			</td>
			<td class="ResultItem2">

				<xsl:choose>
					<xsl:when test="not(number(TotalTargetPrice))">
						<xsl:value-of select='TotalTargetPrice'/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select='number(TotalTargetPrice)'/>
					</xsl:otherwise>
				</xsl:choose>

			</td>

			<td class="ResultItem2">

				<xsl:choose>
					<xsl:when test="not(number(TotalSalesPrice))">
						<xsl:value-of select='TotalSalesPrice'/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select='number(TotalSalesPrice)'/>
					</xsl:otherwise>
				</xsl:choose>

			</td>

			<td class="ResultItem2">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select='TotalPercentagePrice'/>


			</td>
			<td class="ResultItem2">

				<xsl:choose>
					<xsl:when test="not(number(TotalEstimatePrice))">
						<xsl:value-of select='TotalEstimatePrice'/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select='number(TotalEstimatePrice)'/>
					</xsl:otherwise>
				</xsl:choose>

			</td>
		</tr>
	</xsl:template>


	<xsl:template match="ProductSubGroupName" mode="row">
		<xsl:param name="nRows"/>

		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductSubGroupName=current()]/TargetQty))">
					<xsl:value-of select='$nRows[ProductSubGroupName=current()]/TargetQty'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[ProductSubGroupName=current()]/TargetQty)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductSubGroupName=current()]/SalesQty))">
					<xsl:value-of select='$nRows[ProductSubGroupName=current()]/SalesQty'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[ProductSubGroupName=current()]/SalesQty)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductSubGroupName=current()]/Percentage))">
					<xsl:value-of select='$nRows[ProductSubGroupName=current()]/Percentage'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[ProductSubGroupName=current()]/Percentage)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
		<td class="ResultItem" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductSubGroupName=current()]/Estimate))">
					<xsl:value-of select='$nRows[ProductSubGroupName=current()]/Estimate'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[ProductSubGroupName=current()]/Estimate)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
	</xsl:template>

</xsl:stylesheet>
