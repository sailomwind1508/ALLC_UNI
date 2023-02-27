<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kUsers" match="Cycle" use="."/>
	<xsl:key name="kRowByName" match="Rep_ActualSale_By_Customer_Carton_XSLT" use="DocKey"/>

	<xsl:variable name="cyIds" select=
      "//Cycle
      [generate-id()
      =
      generate-id(key('kUsers', .)[1])
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
						<TD class="stdPageHdr" colspan="7">
							<xsl:value-of select="Rep_ActualSale_By_Customer_Carton_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="7">
							รายงานยอดขายแยกตามลูกค้า(หีบ)
						</TD>
					</TR>
					<TR>
						<TD Colspan="7"></TD>
					</TR>
					<TR>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							Year :
						</TD>
						<TD colspan="1" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_ActualSale_By_Customer_Carton_XSLT/Year" />
						</TD>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;">
							Depot :
						</TD>
						<TD class="SearchResultItem" Colspan="4" style="text-align: center;">
							<xsl:value-of select="Rep_ActualSale_By_Customer_Carton_XSLT/BranchName" />
						</TD>
						
					</TR>
					<TR>
						<TD Colspan="7"></TD>
					</TR>
					<thead>
						<th class="SearchResultItem">รหัสศูนย์</th>
						<th class="SearchResultItem">ศูนย์</th>
						<th class="SearchResultItem">แวน</th>
						<th class="SearchResultItem">ตลาดที่</th>
						<th class="SearchResultItem">ตลาด</th>
						<th class="SearchResultItem">รหัสลูกค้า</th>
						<th class="SearchResultItem">ลูกค้า</th>
						<th class="SearchResultItem">ที่อยู่</th>
						<th class="SearchResultItem">จังหวัด</th>
						<th class="SearchResultItem">ลักษณะลูกค้า</th>
						<th class="SearchResultItem">วันที่เริ่มต้น</th>
						<th class="SearchResultItem">สถานะ</th>
						<xsl:apply-templates select="$cyIds"/>
						<th class="SearchResultItem">รวม</th>						
					</thead>
					<tbody>
						<xsl:apply-templates select=
                "//Rep_ActualSale_By_Customer_Carton_XSLT
                [generate-id() = 
                 generate-id(key('kRowByName', DocKey))]">
							<xsl:sort select="concat(WHID,Seq)"/>
						</xsl:apply-templates>
					</tbody>
				</Worksheet>
			</table>
		</html>
	</xsl:template>

	<xsl:template match="Cycle">
		<th class="SearchResultItem">
			 Cycle
			<xsl:value-of select="."/>
		</th>
	</xsl:template>

	<xsl:template match="Rep_ActualSale_By_Customer_Carton_XSLT">
		<tr>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="BranchID"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="BranchName"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="WHID"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="Seq"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="SalAreaName"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="CustomerID"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="CustName"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="BillTo"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="ProvinceName"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="ShopTypeName"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="StartDate"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="Status"/>
			</td>

			<xsl:apply-templates select="$cyIds" mode="row">
				<xsl:with-param name="nRows" select="key('kRowByName', DocKey)"/>
			</xsl:apply-templates>

			<td class="SearchResultItem">
				<xsl:value-of select='format-number(Total,"###,###.00")'/>
			</td>
			
		</tr>
	</xsl:template>

	<xsl:template match="Cycle" mode="row">
		<xsl:param name="nRows"/>
		<td class="SearchResultItem" align="Right">
			<xsl:if test="$nRows[Cycle=current()]/QTY=0">
				-
			</xsl:if>
			<xsl:if test="$nRows[Cycle=current()]/QTY>0">
				<xsl:value-of select='format-number($nRows[Cycle=current()]/QTY,"###,###.00")'/>
			</xsl:if>
		</td>
	</xsl:template>
</xsl:stylesheet>
