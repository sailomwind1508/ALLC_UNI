<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_BillDuplicate">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานบิลซ้ำ">
				<ss:Table>
					<xsl:apply-templates select="Rep_BillDuplicate"/>
				</ss:Table>
			</ss:Worksheet>
		</ss:Workbook>
	</xsl:template>

	<xsl:template match="/">
		<HTML>
			<HEAD>
				<STYLE>
					.content{
					font-family:Arial;
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
					font-size: 9pt;
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
					font-size: 14pt;
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
					font-size: 9pt;
					font-weight: bold;
					}
					.SearchKey {
					color: DarkBlue;
					font-size: 9pt;
					vertical-align:middle;
					text-align:right;
					}
					.SearchValue
					{
					color: Black;
					font-size: 9pt;
					font-weight: bold;
					vertical-align:middle;
					text-align:left;
					}
					.SearchResultHeader {
					background-color: #9BE2F9;
					color: DarkBlue;
					font-weight: bold;
					font-size: 9pt;
					}
					.SearchResultItem {
					background-color: #FFFFFF;
					color: Black;
					border: solid thin Black;
					font-size: 9pt;
					}
					.SearchResultAltItem {
					background-color: #99CCFF;
					color: Black;
					font-size: 9pt;
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
					font-size: 9pt;
					}
					.GroupFooter{
					color: Black;
					font-weight: bold;
					font-style:Regular;
					text-align: left;
					padding-left: 1px;
					padding-top: 1px;
					padding-bottom: 1px;
					width: 100%;
					font-size: 9pt;
					}
					.GroupTotal{text-align:right;}
					.subTotals {border-bottom:1px solid black}
				</STYLE>
			</HEAD>
			<BODY class="content">
				<TABLE>
					<TR>
						<TD class="stdPageHdr" colspan="5">
							<xsl:value-of select="NewDataSet/Rep_BillDuplicate/CompanyName"/>
						</TD>
					</TR>

					<TR>
						<TD class="stdPageHdr" colspan="5">รายงานบิลซ้ำ</TD>
					</TR>

					<TR>
						<TD class="gridHeader">Depot :</TD>
						<TD class="gridHeader" width="140" colspan="2">
							<xsl:value-of select="(NewDataSet/Rep_BillDuplicate/BranchName)"/> (<xsl:value-of select="(NewDataSet/Rep_BillDuplicate/BranchID)"/>)
						</TD>

						<TD class="gridHeader">Van :</TD>
						<TD class="gridHeader" width="200">
							<xsl:value-of select="(NewDataSet/Rep_BillDuplicate/WHID)"/>
						</TD>
					</TR>

					<TR>
						<TD class="gridHeader">Date :</TD>
						<TD class="gridHeader">
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of select="(NewDataSet/Rep_BillDuplicate/DocDate)"/>
						</TD>
					</TR>

					<TR> </TR>
					<!--เว้นบรรทัด-->

					<TR>
						<TD CLASS="gridHeader" width="68">ลำดับ</TD>
						<TD CLASS="gridHeader" width="96">วันที่</TD>
						<TD CLASS="gridHeader" width="122">เลขที่เอกสาร</TD>
						<TD CLASS="gridHeader" width="110">รหัสลูกค้า</TD>
						<TD CLASS="gridHeader" width="100">ชื่อ</TD>
					</TR>

					<xsl:for-each select="NewDataSet/Rep_BillDuplicate">

						<TR>
							<TD class="SearchResultItem">
								<xsl:value-of  select="position()"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="DocDate"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of  select="DocNo"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="CustomerID"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:value-of select="CustName"/>
							</TD>

						</TR >
					</xsl:for-each>

				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>



</xsl:stylesheet>
