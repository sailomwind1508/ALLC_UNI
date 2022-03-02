<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kUsers" match="ProductAll" use="."/>
	<xsl:key name="kRowByName" match="Rep_Daily_Sales_XSLT" use="DocKey"/>

	<xsl:variable name="prdIds" select=
      "//ProductAll
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
						<TD class="stdPageHdr" colspan="7">
							<xsl:value-of select="Rep_Daily_Sales_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="7">
							รายงานการขายประจำวัน
						</TD>
					</TR>
					<TR>
						<TD Colspan="7"></TD>
					</TR>
					<TR>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;">
							Depot :
						</TD>
						<TD class="SearchResultItem" Colspan="2" style="text-align: center;">
							<xsl:value-of select="Rep_Daily_Sales_XSLT/BranchName" />
						</TD>
						<TD class="SearchResultItem" style="font-weight: bold;text-align: center;" align="Center">
							Date :
						</TD>
						<TD colspan="4" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_Daily_Sales_XSLT/VisitDate" />
						</TD>						
					</TR>
					<TR>
						<TD Colspan="7"></TD>
					</TR>
					<thead>
						<th class="SearchResultItem">แวน</th>
						<th class="SearchResultItem">ชื่อพนักงาน</th>
						<th class="SearchResultItem">รหัส</th>
						<th class="SearchResultItem">ชื่อร้านค้า</th>
						<th class="SearchResultItem">ที่อยู่</th>
						<th class="SearchResultItem">วันที่</th>
						<th class="SearchResultItem">เลขที่เอกสาร</th>
						<th class="SearchResultItem">รหัสลูกค้า</th>
						<th class="SearchResultItem">ตลาดที่</th>
						<th class="SearchResultItem">เวลาในการเยี่ยม</th>
						<th class="SearchResultItem">เวลาในการเปิดบิล</th>
						<th class="SearchResultItem">สถานะบิล</th>
						<xsl:apply-templates select="$prdIds"/>
						<th class="SearchResultItem">SKU</th>
						<th class="SearchResultItem">ยอดขาย</th>
						<th class="SearchResultItem">ส่วนลด</th>
						<th class="SearchResultItem">ยอดสุทธิ(บาท)</th>
					</thead>
					<tbody>
						<xsl:apply-templates select=
                "//Rep_Daily_Sales_XSLT
                [generate-id() = 
                 generate-id(key('kRowByName', DocKey))]">
							<xsl:sort select="concat(WHID,DocNo)"/>
						</xsl:apply-templates>

						<!--<tr>
							<td ColSpan="12" Class="SearchResultItem" align="Right">
								รวมยอดขายประจำวัน
							</td>
							<xsl:apply-templates select="$prdIds" mode="row">
								<xsl:with-param name="nRows" select="key('kRowByName', CustomerID)"/>
							</xsl:apply-templates>

							<td class="SearchResultItem">
								<xsl:value-of select='format-number(Rep_Daily_Sales_XSLT/TotalPrice,"###,###.00")'/>
							</td>
						</tr>-->
					</tbody>
				</Worksheet>
			</table>
		</html>
	</xsl:template>

	<xsl:template match="ProductAll">
		<th class="SearchResultItem">
			<xsl:value-of select="."/>
		</th>
	</xsl:template>
	
	<xsl:template match="Rep_Daily_Sales_XSLT">
		<tr>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="WHID"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="Name"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="EmpIDCard"/>
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
				<xsl:value-of select="VisitDate"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="DocNo"/>
			</td>	
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="CustomerID"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="Seq"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="VisitTime"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="CustPOTime"/>
			</td>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:value-of select="DocStatus"/>
			</td>
		
			<xsl:apply-templates select="$prdIds" mode="row">
				<xsl:with-param name="nRows" select="key('kRowByName', DocKey)"/>
			</xsl:apply-templates>
			
			<td class="SearchResultItem">
				<xsl:value-of select='format-number(SKU,"###,###.00")'/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select='format-number(IncVat,"###,###.00")'/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select='format-number(Discount,"###,###.00")'/>
			</td>
			<td class="SearchResultItem">
				<xsl:value-of select='format-number(TotalDue,"###,###.00")'/>
			</td>
		</tr>
	</xsl:template>

	<xsl:template match="ProductAll" mode="row">
		<xsl:param name="nRows"/>
		<td class="SearchResultItem" align="Right">
			<!--<xsl:value-of select='format-number($nRows[ProductAll=current()]/QTY,"###,###.00")'/>-->
			<xsl:if test="$nRows[ProductAll=current()]/QTY=0">
-
			</xsl:if>
			<xsl:if test="$nRows[ProductAll=current()]/QTY>0">
				<xsl:value-of select='format-number($nRows[ProductAll=current()]/QTY,"###,###.00")'/>
			</xsl:if>
		</td>
	</xsl:template>
</xsl:stylesheet>
