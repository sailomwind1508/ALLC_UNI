<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_Distribution_Cycle">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงาน Distribution">
				<ss:Table>
					<xsl:apply-templates select="Rep_Distribution_Cycle"/>
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
					font-size: 12pt;
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
						<TD class="stdPageHdr" colspan="4">
							<xsl:value-of select="NewDataSet/Rep_Distribution_Cycle/CompanyName"/>
						</TD>
					</TR>

					<TR>
						<TD class="stdPageHdr" colspan="4">
							รายงาน Distribution
						</TD>
					</TR>

					<TR>
						<TD class="gridHeader" width="50">Depot :</TD>
						<TD class="gridHeader" width="200">
							<xsl:value-of select="(NewDataSet/Rep_Distribution_Cycle/BranchName)"/> (<xsl:value-of select="(NewDataSet/Rep_Distribution_Cycle/BranchID)"/>)
						</TD>

						<TD class="gridHeader" width="200">Van :</TD>
						<TD class="gridHeader" width="390">
							<xsl:value-of select="(NewDataSet/Rep_Distribution_Cycle/WHName)"/> (<xsl:value-of select="(NewDataSet/Rep_Distribution_Cycle/WHID)"/>)
						</TD>
					</TR>

					<TR>
						<TD class="gridHeader" width="50">Year :</TD>
						<TD class="gridHeader" width="200">
							<xsl:value-of select='(/NewDataSet/Rep_Distribution_Cycle/Years) + 543'/>
						</TD>
					</TR>

					<TR> </TR>
					<!--เว้นบรรทัด-->

					<TR>
						<TD CLASS="gridHeader" width="50">ลำดับ</TD>
						<TD CLASS="gridHeader" width="200">ตลาด</TD>
						<TD CLASS="gridHeader" width="200">ลูกค้า</TD>
						<TD CLASS="gridHeader" width="390">ที่อยู่</TD>
						<TD CLASS="gridHeader" width="48">Cycle 1</TD>
						<TD CLASS="gridHeader" width="48">Cycle 2</TD>
						<TD CLASS="gridHeader" width="48">Cycle 3</TD>
						<TD CLASS="gridHeader" width="48">Cycle 4</TD>
						<TD CLASS="gridHeader" width="48">Cycle 5</TD>
						<TD CLASS="gridHeader" width="48">Cycle 6</TD>
						<TD CLASS="gridHeader" width="48">Cycle 7</TD>
						<TD CLASS="gridHeader" width="48">Cycle 8</TD>
						<TD CLASS="gridHeader" width="48">Cycle 9</TD>
						<TD CLASS="gridHeader" width="60">Cycle 10</TD>
						<TD CLASS="gridHeader" width="60">Cycle 11</TD>
						<TD CLASS="gridHeader" width="60">Cycle 12</TD>
						<TD CLASS="gridHeader" width="60">Cycle 13</TD>
						<TD CLASS="gridHeader" width="60">วัดผล C1-C3</TD>
						<TD CLASS="gridHeader" width="60">วัดผล C4-C6</TD>
						<TD CLASS="gridHeader" width="60">วัดผล C7-C9</TD>
						<TD CLASS="gridHeader" width="60">วัดผล C10-C13</TD>
					</TR>

					<xsl:for-each select="NewDataSet/Rep_Distribution_Cycle">
						<TR>
							<TD class="SearchResultItem" align="left">
								<xsl:value-of  select="position()"/>
							</TD>

							<TD class="SearchResultItem">
								<xsl:value-of select="SaleAreaName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of  select="CustName"/>
							</TD>
							<TD class="SearchResultItem">
								<xsl:value-of select="Adds"/>
							</TD>

							<xsl:if test="Cycle1=1">
								<TD class="SearchResultItem" align="right">
									<xsl:value-of select="Cycle1"/>
								</TD>
							
							</xsl:if>
							<xsl:if test="Cycle1=0">
								<TD class="SearchResultItem" align="center">
									-
								</TD>
							</xsl:if>

							<xsl:if test="Cycle2=1">
								<TD class="SearchResultItem" align="right">
									<xsl:value-of select="Cycle2"/>
								</TD>

							</xsl:if>
							<xsl:if test="Cycle2=0">
								<TD class="SearchResultItem" align="center">
									-
								</TD>
							</xsl:if>

							<xsl:if test="Cycle3=1">
								<TD class="SearchResultItem" align="right">
									<xsl:value-of select="Cycle3"/>
								</TD>

							</xsl:if>
							<xsl:if test="Cycle3=0">
								<TD class="SearchResultItem" align="center">
									-
								</TD>
							</xsl:if>

							<xsl:if test="Cycle4=1">
								<TD class="SearchResultItem" align="right">
									<xsl:value-of select="Cycle4"/>
								</TD>

							</xsl:if>
							<xsl:if test="Cycle4=0">
								<TD class="SearchResultItem" align="center">
									-
								</TD>
							</xsl:if>

							<xsl:if test="Cycle5=1">
								<TD class="SearchResultItem" align="right">
									<xsl:value-of select="Cycle5"/>
								</TD>

							</xsl:if>
							<xsl:if test="Cycle5=0">
								<TD class="SearchResultItem" align="center">
									-
								</TD>
							</xsl:if>

							<xsl:if test="Cycle6=1">
								<TD class="SearchResultItem" align="right">
									<xsl:value-of select="Cycle6"/>
								</TD>

							</xsl:if>
							<xsl:if test="Cycle6=0">
								<TD class="SearchResultItem" align="center">
									-
								</TD>
							</xsl:if>

							<xsl:if test="Cycle7=1">
								<TD class="SearchResultItem" align="right">
									<xsl:value-of select="Cycle7"/>
								</TD>

							</xsl:if>
							<xsl:if test="Cycle7=0">
								<TD class="SearchResultItem" align="center">
									-
								</TD>
							</xsl:if>

							<xsl:if test="Cycle8=1">
								<TD class="SearchResultItem" align="right">
									<xsl:value-of select="Cycle8"/>
								</TD>

							</xsl:if>
							<xsl:if test="Cycle8=0">
								<TD class="SearchResultItem" align="center">
									-
								</TD>
							</xsl:if>

							<xsl:if test="Cycle9=1">
								<TD class="SearchResultItem" align="right">
									<xsl:value-of select="Cycle9"/>
								</TD>

							</xsl:if>
							<xsl:if test="Cycle9=0">
								<TD class="SearchResultItem" align="center">
									-
								</TD>
							</xsl:if>

							<xsl:if test="Cycle10=1">
								<TD class="SearchResultItem" align="right">
									<xsl:value-of select="Cycle10"/>
								</TD>

							</xsl:if>
							<xsl:if test="Cycle10=0">
								<TD class="SearchResultItem" align="center">
									-
								</TD>
							</xsl:if>

							<xsl:if test="Cycle11=1">
								<TD class="SearchResultItem" align="right">
									<xsl:value-of select="Cycle11"/>
								</TD>

							</xsl:if>
							<xsl:if test="Cycle11=0">
								<TD class="SearchResultItem" align="center">
									-
								</TD>
							</xsl:if>

							<xsl:if test="Cycle12=1">
								<TD class="SearchResultItem" align="right">
									<xsl:value-of select="Cycle12"/>
								</TD>

							</xsl:if>
							<xsl:if test="Cycle12=0">
								<TD class="SearchResultItem" align="center">
									-
								</TD>
							</xsl:if>
							<xsl:if test="Cycle13=1">
								<TD class="SearchResultItem" align="right">
									<xsl:value-of select="Cycle13"/>
								</TD>

							</xsl:if>
							<xsl:if test="Cycle13=0">
								<TD class="SearchResultItem" align="center">
									-
								</TD>
							</xsl:if>
							
							<TD class="SearchResultItem" align="right">
								<xsl:value-of select="Q1"/>
							</TD>
							<TD class="SearchResultItem" align="right">
								<xsl:value-of select="Q2"/>
							</TD>
							<TD class="SearchResultItem" align="right">
								<xsl:value-of select="Q3"/>
							</TD>
							<TD class="SearchResultItem" align="right">
								<xsl:value-of select="Q4"/>
							</TD>

						</TR >
					</xsl:for-each>

					<TR Class="GroupFooter">
						<TD ColSpan="4"> </TD>

						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle1) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Cycle1) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle1) = 0">
								-
							</xsl:if>
						</TD>



						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle2) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Cycle2) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle2) = 0">
								-
							</xsl:if>
						</TD>

						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle3) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Cycle3) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle3) = 0">
								-
							</xsl:if>
						</TD>

						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle4) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Cycle4) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle4) = 0">
								-
							</xsl:if>
						</TD>

						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle5) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Cycle5) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle5) = 0">
								-
							</xsl:if>
						</TD>

						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle6) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Cycle6) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle6) = 0">
								-
							</xsl:if>
						</TD>

						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle7) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Cycle7) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle7) = 0">
								-
							</xsl:if>
						</TD>

						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle8) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Cycle8) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle8) = 0">
								-
							</xsl:if>
						</TD>

						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle9) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Cycle9) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle9) = 0">
								-
							</xsl:if>
						</TD>


						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle10) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Cycle1) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle1) = 0">
								-
							</xsl:if>
						</TD>


						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle11) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Cycle11) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle11) = 0">
								-
							</xsl:if>
						</TD>


						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle12) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Cycle12) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle12) = 0">
								-
							</xsl:if>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle13) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Cycle13) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Cycle13) = 0">
								-
							</xsl:if>
						</TD>

						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Q1) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Q1) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Q1) = 0">
								-
							</xsl:if>
						</TD>

						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Q2) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Q2) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Q2) = 0">
								-
							</xsl:if>
						</TD>

						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Q3) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Q3) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Q3) = 0">
								-
							</xsl:if>
						</TD>

						<TD Class="subTotals" align="right">
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Q4) > 0">
								<xsl:value-of select='format-number((sum(/NewDataSet/Rep_Distribution_Cycle/Q4) div 1112) * 100,"###,###.00")'/>
							</xsl:if>
							<xsl:if test="sum(/NewDataSet/Rep_Distribution_Cycle/Q4) = 0">
								-
							</xsl:if>
						</TD>
						
					</TR >
					
				</TABLE>

			</BODY>

		</HTML>
	</xsl:template>

</xsl:stylesheet>
