<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />

	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	<xsl:key name="kCols1" match="SEQ" use="."/>
	
	<xsl:key name="kRows" match="Rep_Cust_Sale_By_Root_XSLT" use="WHID"/>

	<xsl:variable name="seqIds" select=
      "//SEQ
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
					width: 120px;
					}
					.ResultItem {
					background-color: #FFFFFF;
					color: Black;
					border: solid thin Black;
					font-size: 10pt;
					width: 80px;
					}
					.ResultDetail {
					background-color: #FFFFFF;
					color: Black;
					border: solid thin Black;
					font-size: 10pt;
					width: 60px;
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
							<xsl:value-of select="Rep_Cust_Sale_By_Root_XSLT/CompanyName" />
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" Colspan="17">
							รายงานร้านซื้อแยกตามตลาด
						</TD>
					</TR>
					<TR>
						<TD Colspan="17"></TD>
					</TR>
					<TR>
						<TD class="ResultDetail" style="font-weight: bold;text-align: center;">
							Depot :
						</TD>
						<TD colspan="3" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_Cust_Sale_By_Root_XSLT/BranchName" />
						</TD>
						<TD class="ResultItem" style="font-weight: bold;text-align: center;" align="Center">
							Date :
						</TD>
						<TD colspan="3" class="SearchResultItem" style="text-align: center;">
							<xsl:value-of select="Rep_Cust_Sale_By_Root_XSLT/HDate" />
						</TD>
					</TR>
					<TR>
						<TD Colspan="5"></TD>
					</TR>
					<thead>
						<th class="ResultDetail">WHID</th>
						<xsl:apply-templates select="$seqIds"/>									
					</thead>
					<tbody>
						<xsl:apply-templates select=
                "//Rep_Cust_Sale_By_Root_XSLT
                [generate-id() = 
                 generate-id(key('kRows', WHID))]">
							<xsl:sort select="WHID"/>

						</xsl:apply-templates>
					</tbody>
				</Worksheet>
			</table>
		</html>
	</xsl:template>

	<xsl:template match="SEQ">
		<th class="SearchResultItem" Colspan="8">
			<!--<xsl:value-of select='.'/>-->
			<!--<xsl:if test='. != "0"'>
				<xsl:value-of select='.'/>
			</xsl:if-->
			<xsl:choose>
				<xsl:when test='. = "-999"'>
					รวม
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='.'/>
				</xsl:otherwise>
			</xsl:choose>
		</th>
	</xsl:template>


	<xsl:template match="Rep_Cust_Sale_By_Root_XSLT">
		<tr>
			<td class="SearchResultItem">
				<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
				<xsl:choose>
					<xsl:when test='WHID = "0"'>

					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select='WHID'/>
					</xsl:otherwise>
				</xsl:choose>
				
			</td>
			<xsl:apply-templates select="$seqIds" mode="row">
				<xsl:with-param name="nRows" select="key('kRows', WHID)"/>
				
			</xsl:apply-templates>
		</tr>
	</xsl:template>


	<xsl:template match="SEQ" mode="row">
		<xsl:param name="nRows"/>
		<td class="ResultDetail" align="Right">
			<xsl:choose>
				<xsl:when test="not(number($nRows[SEQ=current()]/VISIT))">
					<xsl:value-of select='$nRows[SEQ=current()]/VISIT'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='format-number(($nRows[SEQ=current()]/VISIT),"#,##0")'/>
				</xsl:otherwise>
			</xsl:choose>
		</td>
		<td class="ResultDetail" align="Right">
			<xsl:choose>
				<xsl:when test="not(number($nRows[SEQ=current()]/PO))">
					<xsl:value-of select='$nRows[SEQ=current()]/PO'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='format-number(($nRows[SEQ=current()]/PO),"#,##0")'/>
				</xsl:otherwise>
			</xsl:choose>
		</td>
		<td class="ResultDetail" align="Right">
			<xsl:choose>
				<xsl:when test="not(number($nRows[SEQ=current()]/DUP))">
					<xsl:value-of select='$nRows[SEQ=current()]/DUP'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='format-number(($nRows[SEQ=current()]/DUP),"#,##0")'/>
				</xsl:otherwise>
			</xsl:choose>
		</td>
		<td class="ResultDetail" align="Right">
			<xsl:choose>
				<xsl:when test="not(number($nRows[SEQ=current()]/PER_EFF))">
					<xsl:value-of select='$nRows[SEQ=current()]/PER_EFF'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='format-number(($nRows[SEQ=current()]/PER_EFF) * 100, "#,##0.00")'/>%
				</xsl:otherwise>
			</xsl:choose>
		</td>
		<td class="ResultDetail" align="Right">
			<xsl:choose>
				<xsl:when test="not(number($nRows[SEQ=current()]/CARTON))">
					<xsl:value-of select='$nRows[SEQ=current()]/CARTON'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='format-number(($nRows[SEQ=current()]/CARTON),"#,##0.00")'/>
				</xsl:otherwise>
			</xsl:choose>
		</td>
		<td class="ResultDetail" align="Right">
			<xsl:choose>
				<xsl:when test="not(number($nRows[SEQ=current()]/BAHT))">
					<xsl:value-of select='$nRows[SEQ=current()]/BAHT'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='format-number(($nRows[SEQ=current()]/BAHT),"#,##0.00")'/>
				</xsl:otherwise>
			</xsl:choose>
		</td>
		<td class="ResultDetail" align="Right">
			<xsl:choose>
				<xsl:when test="not(number($nRows[SEQ=current()]/COM))">
					<xsl:value-of select='$nRows[SEQ=current()]/COM'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='format-number(($nRows[SEQ=current()]/COM),"#,##0.00")'/>
				</xsl:otherwise>
			</xsl:choose>
		</td>
		<td class="ResultDetail" align="Right">
			<xsl:choose>
				<xsl:when test="not(number($nRows[SEQ=current()]/PER_COM))">
					<xsl:value-of select='$nRows[SEQ=current()]/PER_COM'/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='format-number(($nRows[SEQ=current()]/PER_COM) * 100, "#,##0.00")'/>%
				</xsl:otherwise>
			</xsl:choose>
		</td>
		
	</xsl:template>


</xsl:stylesheet>
