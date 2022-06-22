<xsl:stylesheet version="1.0"
     xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
     xmlns="urn:schemas-microsoft-com:office:spreadsheet"
     xmlns:o="urn:schemas-microsoft-com:office:office"
     xmlns:x="urn:schemas-microsoft-com:office:excel"
     xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
     xmlns:html="http://www.w3.org/TR/REC-html40">
	<xsl:output method='html' version='1.0' encoding='UTF-8'/>
	<xsl:template name="FormatDate">
		<xsl:param name="DateTime" />
		<!-- old date format YYYYMMDD / want DD/MM/YYYY-->
		<xsl:variable name="year">
			<xsl:value-of select="substring($DateTime,1,4)" />
		</xsl:variable>
		<xsl:variable name="mon">
			<xsl:value-of select="substring($DateTime,5,2)" />
		</xsl:variable>
		<xsl:variable name="day">
			<xsl:value-of select="substring($DateTime,7,2)" />
		</xsl:variable>
		<xsl:value-of select="$day" />/<xsl:value-of select="$mon" />/<xsl:value-of select="$year" />
	</xsl:template>

	<xsl:decimal-format name="foo"
  grouping-separator="'" digit="#" zero-digit="0" decimal-separator="."/>

	<xsl:key name="contacts-by-provinceid" match="Rep_ActualSale_By_Province_XSLT" use="ProvinceID" />
	<xsl:key name="contacts-by-areaid" match="Rep_ActualSale_By_Province_XSLT" use="concat(ProvinceID,' ',AreaID)" />
	<!--<xsl:key name="contacts-by-empid" match="Rep_ActualSale_By_Province_XSLT" use="concat(WHID,' ',EmpID)" />-->
	<!--<xsl:key name="contacts-by-salareaid" match="Rep_ActualSale_By_Province_XSLT" use="concat(EmpID,' ',SalAreaID)" />-->

	<!--<xsl:template match="/">
    <Workbook
      xmlns="urn:schemas-microsoft-com:office:spreadsheet"
      xmlns:o="urn:schemas-microsoft-com:office:office"
      xmlns:x="urn:schemas-microsoft-com:office:excel"
      xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
      xmlns:html="http://www.w3.org/TR/REC-html40">
      <xsl:apply-templates select="Top"/>
    </Workbook>
  </xsl:template>-->

	<xsl:template match="/">
		<?mso-application progid="Excel.Sheet"?>
		<Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"
				  xmlns:o="urn:schemas-microsoft-com:office:office"
				  xmlns:x="urn:schemas-microsoft-com:office:excel"
				  xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
				  xmlns:html="http://www.w3.org/TR/REC-html40">
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
						font-size: 12pt;
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
						font-size: 11pt;
						font-weight: bold;
						}
						.SearchKey {
						color: DarkBlue;
						font-size: 11pt;
						vertical-align:middle;
						text-align:right;
						}
						.SearchValue
						{
						color: Black;
						font-size: 11pt;
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
						}
						.SearchResultAltItem {
						background-color: #99CCFF;
						color: Black;
						font-size: 11pt;
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
						font-size: 11pt;
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
						font-size: 11pt;
						}
						.GroupTotal{text-align:right;}
						.subTotals {border-bottom:1px solid black}
					</STYLE>
				</HEAD>
				<BODY>
					<TABLE>
						<TR>
							<TD class="stdPageHdr" ColSpan="7">
								<xsl:value-of select="NewDataSet/Rep_ActualSale_By_Province_XSLT/CompanyName"/>
							</TD>
						</TR>
						<TR>
							<TD class="stdPageHdr" colspan="7">รายงานยอดขายแยกตามจังหวัด (บาท)</TD>
						</TR>
						<TR>
							<TD class="SearchKey" Colspan="1" align="left">Depot :</TD>
							<TD class="SearchValue" Colspan="5">
								<xsl:value-of select="NewDataSet/Rep_ActualSale_By_Province_XSLT/BranchName" />
							</TD>
						</TR>
						<TR>
							<TD class="SearchKey">Year :</TD>
							<TD class="SearchValue" Colspan="6">
								<xsl:value-of select="NewDataSet/Rep_ActualSale_By_Province_XSLT/Year"/>
							</TD>
						</TR>
						<TR>
							<TD class="SearchKey">Van :</TD>
							<TD class="SearchValue" Colspan="6">
								<xsl:value-of select="NewDataSet/Rep_ActualSale_By_Province_XSLT/WHID"/>
							</TD>
						</TR>

						<TR>
							<TD CLASS="gridHeader">จังหวัด</TD>
							<TD CLASS="gridHeader">อำเภอ</TD>
							<TD CLASS="gridHeader">จำนวนร้านค้า</TD>
							<TD CLASS="gridHeader">ม.ค.</TD>
							<TD CLASS="gridHeader">ก.พ.</TD>
							<TD CLASS="gridHeader">มี.ค.</TD>
							<TD CLASS="gridHeader">เม.ย.</TD>
							<TD CLASS="gridHeader">พ.ค.</TD>
							<TD CLASS="gridHeader">มิ.ย.</TD>
							<TD CLASS="gridHeader">ก.ค.</TD>
							<TD CLASS="gridHeader">ส.ค.</TD>
							<TD CLASS="gridHeader">ก.ย.</TD>
							<TD CLASS="gridHeader">ต.ค.</TD>
							<TD CLASS="gridHeader">พ.ย.</TD>
							<TD CLASS="gridHeader">ธ.ค.</TD>
							<TD CLASS="gridHeader">รวมทั้งปี</TD>
						</TR>

						<xsl:for-each select="NewDataSet/Rep_ActualSale_By_Province_XSLT[count(. | key('contacts-by-provinceid', ProvinceID)[1]) = 1]">
							<xsl:for-each select="key('contacts-by-provinceid', ProvinceID)">
								<xsl:sort select="ProvinceID" />

								<TR>
									<TD class="SearchResultItem" width="150">
										<xsl:value-of select="ProvinceName"/>
									</TD>
									<TD class="SearchResultItem" width="150">
										<xsl:value-of select="position()" />.
										<xsl:value-of  select="AreaName"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="C_Area"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="format-number(M1,'#,##0.00')"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="format-number(M2,'#,##0.00')"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="format-number(M3,'#,##0.00')"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="format-number(M4,'#,##0.00')"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="format-number(M5,'#,##0.00')"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="format-number(M6,'#,##0.00')"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="format-number(M7,'#,##0.00')"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="format-number(M8,'#,##0.00')"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="format-number(M9,'#,##0.00')"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="format-number(M10,'#,##0.00')"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="format-number(M11,'#,##0.00')"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="format-number(M12,'#,##0.00')"/>
									</TD>
									<TD class="SearchResultItem">
										<xsl:value-of select="format-number(Sum_Amt,'#,##0.00')"/>
									</TD>
								</TR>

							</xsl:for-each>
							<TR>
								<TD ColSpan="2" Class="GroupFooter">
									รวมตามจังหวัด (<xsl:value-of select="ProvinceName"/>)
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/C_Area),'#,##0')"/>
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/M1),'#,##0.00')"/>
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/M2),'#,##0.00')"/>
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/M3),'#,##0.00')"/>
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/M4),'#,##0.00')"/>
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/M5),'#,##0.00')"/>
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/M6),'#,##0.00')"/>
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/M7),'#,##0.00')"/>
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/M8),'#,##0.00')"/>
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/M9),'#,##0.00')"/>
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/M10),'#,##0.00')"/>
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/M11),'#,##0.00')"/>
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/M12),'#,##0.00')"/>
								</TD>
								<TD>
									<xsl:value-of select="format-number(sum(key('contacts-by-provinceid',ProvinceID)/Sum_Amt),'#,##0.00')"/>
								</TD>
							</TR>
						</xsl:for-each>

						<TR Class="GroupFooter">
							<TD ColSpan="2">รวม</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/C_Area),"#,##0")'/>
							</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/M1),"#,##0.00")'/>
							</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/M2),"#,##0.00")'/>
							</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/M3),"#,##0.00")'/>
							</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/M4),"#,##0.00")'/>
							</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/M5),"#,##0.00")'/>
							</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/M6),"#,##0.00")'/>
							</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/M7),"#,##0.00")'/>
							</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/M8),"#,##0.00")'/>
							</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/M9),"#,##0.00")'/>
							</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/M10),"#,##0.00")'/>
							</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/M11),"#,##0.00")'/>
							</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/M12),"#,##0.00")'/>
							</TD>
							<TD Class="subTotals" align="right">
								<xsl:value-of select='format-number(sum(/NewDataSet/Rep_ActualSale_By_Province_XSLT/Sum_Amt),"#,##0.00")'/>
							</TD>
						</TR >


					</TABLE>
				</BODY>
			</HTML>
		</Workbook >
	</xsl:template>
</xsl:stylesheet>