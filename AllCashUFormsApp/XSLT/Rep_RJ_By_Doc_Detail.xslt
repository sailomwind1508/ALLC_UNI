<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kCols1" match="ProductID" use="."/>
	<xsl:key name="kCols2" match="ProductName" use="."/>
	<xsl:key name="kRows" match="Rep_RJ_By_Doc_Detail_XSLT" use="DocNo"/>

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
					width: 120px;
					}
					.ResultItem {
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
						<TD class="stdPageHdr" colspan="17">
							<xsl:value-of select="Rep_RJ_By_Doc_Detail_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="17">
							รายงานรายละเอียดขายสินค้า (แยกตามเอกสาร)
						</TD>
					</TR>
					<TR>
						<TD Colspan="17"></TD>
					</TR>
					<TR>
						<TD class="ResultItem" style="font-weight: bold;text-align: center;">
							Depot :
						</TD>
						<TD colspan="3" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_RJ_By_Doc_Detail_XSLT/BranchName" />
						</TD>
						<TD class="ResultItem" style="font-weight: bold;text-align: center;" align="Center">
							Date :
						</TD>
						<TD colspan="3" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_RJ_By_Doc_Detail_XSLT/HDate" />
						</TD>
						
					</TR>
					<TR>
						<TD Colspan="4"></TD>
					</TR>
					<TR>
						<thead>
							<TH Colspan="4"></TH>
							<xsl:apply-templates select="$prdIds"/>

						</thead>
					</TR>
					<thead>
						<th class="SearchResultItem">วันที่</th>
						<th class="SearchResultItem">เลขที่</th>
						<th class="SearchResultItem">คลังต้นทาง</th>
						<th class="SearchResultItem">คลังปลายทาง</th>
						<xsl:apply-templates select="$prdNms"/>
						<th class="SearchResultItem">สถานะเอกสาร</th>
					</thead>
					<tbody>
						<xsl:apply-templates select=
                "//Rep_RJ_By_Doc_Detail_XSLT
                [generate-id() = 
                 generate-id(key('kRows', DocNo))]">
							<xsl:sort select="DocNo"/>

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

	<xsl:template match="Rep_RJ_By_Doc_Detail_XSLT">
		<tr>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="DocDate"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:choose>
					<xsl:when test='DocNo = "รวม"'>

					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select='DocNo'/>
					</xsl:otherwise>
				</xsl:choose>
				
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="WHIDFr"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="WHIDTo"/>
			</td>
			<xsl:apply-templates select="$prdIds" mode="row">
				<xsl:with-param name="nRows" select="key('kRows', DocNo)"/>
				
			</xsl:apply-templates>

			
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="Status"/>
			</td>
		</tr>
	</xsl:template>


	<xsl:template match="ProductID" mode="row">
		<xsl:param name="nRows"/>
		<td class="ResultItem" align="Right">
			<xsl:choose>
				<xsl:when test="not(number($nRows[ProductID=current()]/RejectedQty))">
					<xsl:value-of select='$nRows[ProductID=current()]/RejectedQty'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='number($nRows[ProductID=current()]/RejectedQty)'/>
				</xsl:otherwise>
			</xsl:choose>
		</td>
	</xsl:template>


</xsl:stylesheet>
