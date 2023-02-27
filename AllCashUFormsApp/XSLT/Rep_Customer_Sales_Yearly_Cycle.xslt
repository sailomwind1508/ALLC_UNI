<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_Customer_Sales_Yearly_Cycle">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานยอดขายแยกตามร้านค้า(รายปี)">
				<ss:Table>
					<xsl:apply-templates select="Rep_Customer_Sales_Yearly_Cycle"/>
				</ss:Table>
			</ss:Worksheet>
		</ss:Workbook>
	</xsl:template>

	<xsl:key name="groupWHID" match="NewDataSet//Rep_Customer_Sales_Yearly_Cycle" use="WHID" />

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
					font-size: 9pt;
					font-weight: bold;
					vertical-align:middle;
					text-align:center;
					border: solid thin Black;
					width: 100%;
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
					width: 100%;
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
						<TD class="stdPageHdr" align="left" colspan="15">รายงานสรุปจำนวนร้านค้าทั้งหมด(ตามคลังรถ)</TD>
					</TR>
					<TR>
						<TD Colspan="2" align="right">Depot :</TD>
						<TD align="left">
							<xsl:value-of select="NewDataSet/Rep_Customer_Sales_Yearly_Cycle/BranchName"/>
						</TD>
					</TR>

					<xsl:for-each select="NewDataSet/Rep_Customer_Sales_Yearly_Cycle[count(. | key('groupWHID', WHID)[1]) = 1]">

						<xsl:sort select="WHID" />
						<TR>
							<TD align ="right" colspan="3"></TD>

						</TR>
						<TR class="SearchResultHeader">
							<TD align ="right" colspan="2">
								แวน :
							</TD>
							<TD align="left">
								<xsl:value-of select="WHID"/>
							</TD>
						</TR>

						<TR>
							<TD CLASS="gridHeader">ลำดับ</TD>
							<TD CLASS="gridHeader">รหัสร้าน</TD>
							<TD CLASS="gridHeader">ชื่อร้าน</TD>
							<TD CLASS="gridHeader">ที่อยู่</TD>
							<TD CLASS="gridHeader">ตำบล</TD>
							<TD CLASS="gridHeader">อำเภอ</TD>
							<TD CLASS="gridHeader">จังหวัด</TD>
							<TD CLASS="gridHeader">Route</TD>
							<TD CLASS="gridHeader">Cycle 1</TD>
							<TD CLASS="gridHeader">Cycle 2</TD>
							<TD CLASS="gridHeader">Cycle 3</TD>
							<TD CLASS="gridHeader">Cycle 4</TD>
							<TD CLASS="gridHeader">Cycle 5</TD>
							<TD CLASS="gridHeader">Cycle 6</TD>
							<TD CLASS="gridHeader">Cycle 7</TD>
							<TD CLASS="gridHeader">Cycle 8</TD>
							<TD CLASS="gridHeader">Cycle 9</TD>
							<TD CLASS="gridHeader">Cycle 10</TD>
							<TD CLASS="gridHeader">Cycle 11</TD>
							<TD CLASS="gridHeader">Cycle 12</TD>
							<TD CLASS="gridHeader">Cycle 13</TD>
						</TR>

						<xsl:for-each select="key('groupWHID', WHID)">
							<xsl:sort select="WHID"/>

							<TR>
								<TD class="SearchResultItem">
									<xsl:value-of  select="position()"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
									<xsl:value-of  select="CustomerID"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
									<xsl:value-of select="CustName"/>
								</TD>
								<TD class="SearchResultItem" width="150">
									<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
									<xsl:value-of select="AddressNo"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
									<xsl:value-of select="DistrictName"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
									<xsl:value-of select="AreaName"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
									<xsl:value-of select="ProvinceName"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
									<xsl:value-of select="SalAreaName"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="Cycle1"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="Cycle2"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="Cycle3"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="Cycle4"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="Cycle5"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="Cycle6"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="Cycle7"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="Cycle8"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="Cycle9"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="Cycle10"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="Cycle11"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="Cycle12"/>
								</TD>
								<TD class="SearchResultItem">
									<xsl:value-of select="Cycle13"/>
								</TD>
							</TR >

						</xsl:for-each>
					</xsl:for-each >

				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>
