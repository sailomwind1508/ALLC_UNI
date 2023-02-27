<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="proc_StockMovement_ByWH_XSLT">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="สรุปยอดเคลื่อนไหวสินค้า เรียงตามรหัสสินค้า">
				<ss:Table>
					<xsl:apply-templates select="proc_StockMovement_ByWH_XSLT"/>
				</ss:Table>
			</ss:Worksheet>
		</ss:Workbook>
	</xsl:template>

	<xsl:key name="groupWHID" match="NewDataSet/proc_StockMovement_ByWH_XSLT" use="WHID" />

	<xsl:template  match="/">
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
					font-size: 11pt;
					}
					.gridHeader {
					background-color: #DDEEFF;
					color: DarkBlue;
					font-size: 9pt;
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
						<TD class="stdPageHdr" colspan="7">
							<xsl:value-of select="NewDataSet/proc_StockMovement_ByWH_XSLT/CompanyName"/>
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" colspan="7">
							<xsl:value-of select="NewDataSet/proc_StockMovement_ByWH_XSLT/BranchName"/>
						</TD>
					</TR>
					<TR>
						<TD class="stdPageHdr" colspan="7">
							รายงานสรุปยอดเคลื่อนไหวสินค้า เรียงตามรหัสสินค้า
						</TD>
					</TR>

					<TR>
						<TD align="right">
							วันที่
						</TD>
						<TD align="left">
							<xsl:value-of select="NewDataSet/proc_StockMovement_ByWH_XSLT/DateFr"/>
						</TD>

						<TD align="right">
							ถึง
						</TD>
						<TD align="left">
							<xsl:value-of select="NewDataSet/proc_StockMovement_ByWH_XSLT/DateTo"/>
						</TD>
					</TR>


					<xsl:for-each select="NewDataSet/proc_StockMovement_ByWH_XSLT[count(. | key('groupWHID', WHID)[1]) = 1]">
						<TR>
							<TD>
								คลัง
							</TD>
							<TD>
								<xsl:value-of select="WHID"/>
							</TD>
						</TR>

						<TR>
							<TD CLASS="gridHeader">ลำดับ</TD>
							<TD CLASS="gridHeader">รหัสสินค้า</TD>
							<TD CLASS="gridHeader" >รหัสบัญชี</TD>
							<TD CLASS="gridHeader" colspan ="3">ชื่อสินค้า</TD>
							<TD CLASS="gridHeader" colspan ="2">หน่วยใหญ่</TD>
							<TD CLASS="gridHeader" colspan ="2">หน่วยย่อย</TD>
							<TD CLASS="gridHeader" colspan ="2">ยอดยกมา</TD>
							<TD CLASS="gridHeader" colspan ="2">ซื้อ</TD>
							<TD CLASS="gridHeader" colspan ="2">ส่งคืน</TD>
							<TD CLASS="gridHeader" colspan ="2">ขาย</TD>
							<TD CLASS="gridHeader" colspan ="2">โอนเข้า</TD>
							<TD CLASS="gridHeader" colspan ="2">โอนออก</TD>
							<TD CLASS="gridHeader" colspan ="2">ยอดยกไป</TD>
						</TR>

						<xsl:for-each select="key('groupWHID', WHID)">
							<xsl:sort select="WHID" />
							<TR>
								<TD CLASS="SearchResultItem">
									<xsl:value-of select="position()"/>
								</TD>

								<TD CLASS="SearchResultItem">
									<xsl:value-of select="ProductID"/>
								</TD>

								<TD CLASS="SearchResultItem">
									<xsl:value-of select="ProductRefCode"/>
								</TD>

								<TD CLASS="SearchResultItem" colspan="3">
									<xsl:value-of select="ProductName"/>
								</TD>

								<TD CLASS="SearchResultItem">
									<xsl:value-of select="หน่วยใหญ่"/>
								</TD>

								<TD CLASS="SearchResultItem">
									<xsl:value-of select="จำนวน-หน่วยใหญ่"/>
								</TD>

								<TD CLASS="SearchResultItem">
									<xsl:value-of select="หน่วยเล็ก"/>
								</TD>

								<TD CLASS="SearchResultItem">

								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="ยอดยกมา-ใหญ่"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="ยอดยกมา-เล็ก"/>
								</TD>

								<TD CLASS="SearchResultItem">
									<xsl:value-of select="ยอดซื้อ-ใหญ่"/>
								</TD>
								<TD CLASS="SearchResultItem">
									<xsl:value-of select="ยอดซื้อ-เล็ก"/>
								</TD>
								<TD class="SearchResultItem">

								</TD>
								<TD class="SearchResultItem">
				
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="ขาย-ใหญ่"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="ขาย-เล็ก"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="โอนเข้า"/>
								</TD>

								<TD class="SearchResultItem">

								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="โอนออก-ใหญ่"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="โอนออก-เล็ก"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="ยอดยกไป-ใหญ่"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="ยอดยกไป-เล็ก"/>
								</TD>
							</TR>
						</xsl:for-each>

						<TR>

						</TR>

						<TR CLASS="GroupFooter">
							<TD colspan="3">
								รวมคลัง
								(<xsl:value-of select="WHID"/>)
							</TD>

							<TD>
								<xsl:value-of select="count(key('groupWHID', WHID)/ProductID)"/>
								รายการ
							</TD>

							<TD colspan="19" align="right">
								<xsl:value-of select="sum(key('groupWHID', WHID)/ยอดยกไป-ใหญ่)"/>
							</TD>

							<TD align="right">
								<xsl:value-of select="sum(key('groupWHID', WHID)/ยอดยกไป-เล็ก)"/>
							</TD>

						</TR>
					</xsl:for-each>
					
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
	
</xsl:stylesheet>
