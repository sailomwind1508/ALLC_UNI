<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kCols1" match="ProductID" use="."/>
	<xsl:key name="kCols2" match="ProductName" use="."/>
	<xsl:key name="kRows" match="Rep_Product_Transfer_XSLT" use="RefDocNo"/>
	
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
					width: 100px;
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
							<xsl:value-of select="Rep_Product_Transfer_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="7">
							รายงานรายละเอียดโอนสินค้า
						</TD>
					</TR>
					<TR>
						<TD Colspan="5"></TD>
					</TR>
					<TR>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;">
							Depot :
						</TD>
						<TD class="SearchResultItem" Colspan="3" style="text-align: center;">
							<xsl:value-of select="Rep_Product_Transfer_XSLT/BranchName" />
						</TD>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							Date :
						</TD>
						<TD colspan="2" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_Product_Transfer_XSLT/HDate" />
						</TD>
					</TR>
					<TR>
						<TD Colspan="5"></TD>
					</TR>
					<TR>
					<thead>
						<TH Colspan="5"></TH>
						<xsl:apply-templates select="$prdIds"/>
						
					</thead>
					</TR>
					<thead>
						<th class="SearchResultItem">วันที่</th>
						<th class="SearchResultItem">เลขที่</th>
						<th class="SearchResultItem">คลังต้นทาง</th>
						<th class="SearchResultItem">คลังปลายทาง</th>
						<th class="SearchResultItem">พนักงาน</th>
						<xsl:apply-templates select="$prdNms"/>
						<th class="SearchResultItem">มูลค่าก่อน Vat</th>
						<th class="SearchResultItem">สถานะเอกสาร</th>
					</thead>
					<tbody>
						<xsl:apply-templates select=
                "//Rep_Product_Transfer_XSLT
                [generate-id() = 
                 generate-id(key('kRows', RefDocNo))]">
							<xsl:sort select="RefDocNo"/>
						</xsl:apply-templates>

					</tbody>
				</Worksheet>
			</table>
		</html>
	</xsl:template>

	<xsl:template match="ProductID">
		<th class="SearchResultItem">
			<xsl:value-of select="."/>
		</th>
	</xsl:template>
	
	<xsl:template match="ProductName">
		<th class="SearchResultItem">
			<xsl:value-of select="."/>
		</th>
	</xsl:template>

	<xsl:template match="Rep_Product_Transfer_XSLT">
		<tr>
			<td class="SearchResultItem">
				<xsl:if test="Date=88888888 or Date=99999999">
					 
				</xsl:if>
				<xsl:if test="Date!=88888888 and Date!=99999999">
					<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
					<xsl:value-of select="Date"/>
				</xsl:if>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="RefDocNo"/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select="FromWHID"/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select="ToWHID"/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select="WHName"/>
			</td>
			<xsl:apply-templates select="$prdIds" mode="row">
				<xsl:with-param name="nRows" select="key('kRows', RefDocNo)"/>
			</xsl:apply-templates>

			<td class="SearchResultItem">
				<xsl:value-of select='format-number(AmtBFVAT,"###,###.00")'/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select="DocStatus"/>
			</td>
		</tr>
	</xsl:template>


	<xsl:template match="ProductID" mode="row">
		<xsl:param name="nRows"/>
		<td class="SearchResultItem" align="Right">
			<xsl:if test="$nRows[ProductID=current()]/TrnQtyOut=0">
				-
			</xsl:if>
			<xsl:if test="$nRows[ProductID=current()]/TrnQtyOut>0">
				<xsl:value-of select='format-number($nRows[ProductID=current()]/TrnQtyOut,"###,###.00")'/>
			</xsl:if>
		</td>
	</xsl:template>


</xsl:stylesheet>
