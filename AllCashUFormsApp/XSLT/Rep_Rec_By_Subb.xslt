<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kCols1" match="ProductID" use="."/>
	<xsl:key name="kCols2" match="ProductName" use="."/>
	<xsl:key name="kRows" match="Rep_Rec_By_Subb_XSLT" use="SupplierID"/>

	<xsl:variable name="prdIds" select=
      "//ProductID
      [generate-id()
      =
      generate-id(key('kCols1', .)[1])
      ]
      "/>

	<xsl:variable name="prdNms" select=
      "//ProductName
      [generate-id()
      =
      generate-id(key('kCols2', .)[1])
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
					.ResultItem {
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
						<TD class="stdPageHdr" colspan="13">
							<xsl:value-of select="Rep_Rec_By_Subb_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="13">
							รายงานรายละเอียดรับสินค้า (แยกตามเจ้าหนี้)
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
							<xsl:value-of select="Rep_Rec_By_Subb_XSLT/BranchName" />
						</TD>
						<TD colspan="2" class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							วันที่ :
						</TD>
						<TD colspan="4" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_Rec_By_Subb_XSLT/Period" />
						</TD>
					</TR>
					<TR>
						<TD Colspan="13"></TD>
					</TR>
					<TR>
						<thead>
							<TH Colspan="2"></TH>
							<xsl:apply-templates select="$prdIds"/>

						</thead>
					</TR>
					<thead>					
						<th class="ResultItem">รหัสเจ้าหนี้</th>
						<th class="ResultItem">ชื่อเจ้าหนี้</th>
						<xsl:apply-templates select="$prdNms"/>
						<th class="ResultItem">สถานะเอกสาร</th>
					</thead>
					<tbody colspan="1">
						<xsl:apply-templates select=
                "//Rep_Rec_By_Subb_XSLT
                [generate-id() = 
                 generate-id(key('kRows', SupplierID))]">
							<!--<xsl:sort select="SupplierID"/>-->
						</xsl:apply-templates>

					</tbody>
				</Worksheet>
			</table>
		</html>
	</xsl:template>

	<xsl:template match="ProductID">
		<th class="ResultItem2">
			<xsl:value-of select="."/>
		</th>
	</xsl:template>

	<xsl:template match="ProductName">
		<th class="ResultItem2">
			<xsl:value-of select="."/>
		</th>
	</xsl:template>


	<xsl:template match="Rep_Rec_By_Subb_XSLT">
		<tr>			
			<td class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="SupplierID"/>
			</td>
			<td class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="SuppName"/>
			</td>

			<xsl:apply-templates select="$prdIds" mode="row">
				<xsl:with-param name="nRows" select="key('kRows', SupplierID)"/>
			</xsl:apply-templates>

			<td class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="DocStatus"/>
			</td>
		</tr>
	</xsl:template>


	<xsl:template match="ProductID" mode="row">
		<xsl:param name="nRows"/>

		<td class="ResultItem2" align="Right">

			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductID=current()]/ReceivedQty))">
					<xsl:value-of select='$nRows[ProductID=current()]/ReceivedQty'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[ProductID=current()]/ReceivedQty)'/>
				</xsl:otherwise>
			</xsl:choose>

		</td>
	</xsl:template>

</xsl:stylesheet>
