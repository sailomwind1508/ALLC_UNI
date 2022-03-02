<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="proc_GetProductDataToExcel">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานสินค้าทั้งหมด">
				<ss:Table>
					<xsl:apply-templates select="proc_GetProductDataToExcel"/>
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
					text-align: center;
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
						<TD class="stdPageHdr" colspan="3">รายงานสินค้าทั้งหมด</TD>
					</TR>
					
					<TR>
						<TD CLASS="gridHeader">รหัสสินค้า</TD>
						<TD CLASS="gridHeader" width="360">ชื่อสินค้า</TD>
						<TD CLASS="gridHeader">รหัส SAPCode</TD>
						<TD CLASS="gridHeader">ชื่อย่อ</TD>
						<TD CLASS="gridHeader">ลำดับ</TD>
						<TD CLASS="gridHeader">ประเภทสินค้า</TD>
						<TD CLASS="gridHeader">กลุ่มสินค้า</TD>
						<TD CLASS="gridHeader">กลุ่มย่อยสินค้า</TD>
						<TD CLASS="gridHeader">บาร์โค้ด</TD>
						<TD CLASS="gridHeader">หน่วยใหญ่</TD>
						<TD CLASS="gridHeader">หน่วยเล็ก</TD>
						<TD CLASS="gridHeader">จำนวน(หน่วยใหญ่)</TD>
						<TD CLASS="gridHeader">จำนวน(หน่วยเล็ก)</TD>
						<TD CLASS="gridHeader">ราคาซื้อก่อนภาษี(หน่วยใหญ่)</TD>
						<TD CLASS="gridHeader">ราคาก่อนภาษี(หน่วยใหญ่)</TD>
						<TD CLASS="gridHeader">ราคารวมภาษี(หน่วยใหญ่)</TD>
						<TD CLASS="gridHeader">ค่าคอมมิชชั่น(หน่วยใหญ่)</TD>
						<TD CLASS="gridHeader">น้ำหนัก(หน่วยใหญ่)</TD>
						<TD CLASS="gridHeader">ราคาซื้อก่อนภาษี(หน่วยเล็ก)</TD>
						<TD CLASS="gridHeader">ราคาก่อนภาษี(หน่วยเล็ก)</TD>
						<TD CLASS="gridHeader">ราคารวมภาษี(หน่วยเล็ก)</TD>
						<TD CLASS="gridHeader">ค่าคอมมิชชั่น(หน่วยเล็ก)</TD>
						<TD CLASS="gridHeader">น้ำหนัก(หน่วยเล็ก)</TD>
						<TD CLASS="gridHeader">รสชาติ</TD>
						<TD CLASS="gridHeader">ประเภทภาษี</TD>
						<TD CLASS="gridHeader">จุดสั่งซื้อ</TD>
						<TD CLASS="gridHeader">จุดต่ำสุด</TD>
						<TD CLASS="gridHeader">น้ำหนักบรรจุ</TD>
						<TD CLASS="gridHeader">ปริมาณบรรจุ(หน้าซอง)</TD>
						<TD CLASS="gridHeader">แสดงใน Tablet</TD>
					</TR>
					
					<xsl:for-each select="NewDataSet/proc_GetProductDataToExcel">
						<TR>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="ProductID"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of  select="ProductName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
								<xsl:value-of select="ProductRefCode"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="ProductShortName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="Seq"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="ProductTypeName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="ProductGroupName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="ProductSubGroupName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="Barcode"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="BigUnitName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="SmallUnitName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="BigUnit"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="SmallUnit"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(BigBuyPrice,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(BigSellPrice,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(BigSellPriceVat,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(BigComPrice,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(BigWeight,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(SmallBuyPrice,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(SmallSellPrice,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(SmallSellPriceVat,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(SmallComPrice,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="format-number(SmallWeight,'###,###.00')"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="Flavour"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="VatType"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="ReorderPoint"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="MinPoint"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="Weight"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="Size"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="IsFulfill"/>
							</TD>
						</TR >
					</xsl:for-each>
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>
