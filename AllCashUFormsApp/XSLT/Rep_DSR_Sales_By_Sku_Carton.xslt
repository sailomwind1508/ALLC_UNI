<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_DSR_Sales_By_Sku_Carton_XSLT">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานยอดขาย DSR (Carton)">
				<ss:Table>
					<xsl:apply-templates select="Rep_DSR_Sales_By_Sku_Carton_XSLT"/>
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
					.ReportHead {
					font-size: 14pt;
					font-weight: bold;
					}
					.header{
					font-size: 11pt;
					font-weight: bold;
					vertical-align:middle;
					text-align:center;
					border: solid thin Black;
					}

					.gridHeader {
					font-size: 11pt;
					font-weight: bold;
					vertical-align:middle;
					text-align:center;
					border: solid thin Black;
					background-color: #99CCFF;
					}

					.ResultAltItem {
					color: Black;
					font-size: 9pt;
					border: solid thin Black;
					}
					.ResultNull {
					text-align:center;
					border: solid thin Black;
					}
					.subTotals {
					font-size: 9pt;
					font-weight: bold;
					border-bottom:1px solid black
					}
				</STYLE>
			</HEAD>
			<BODY class="content">
				<TABLE>
					<TR>
						<TD colspan="3" class="ReportHead">
							<xsl:value-of select="NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/CompanyName"/>
						</TD>
					</TR>
					<TR>
						<TD colspan="3" class="ReportHead">
							รายงานยอดขาย DSR (Carton)
						</TD>
					</TR>
					<TR>
						<TD class="gridHeader" colspan="1">Depot :</TD>
						<TD class="header" colspan="2">
							<xsl:value-of select="NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/BranchName"/>
						</TD>
					</TR>
					<TR>
						<TD class="gridHeader" colspan="1">Year :</TD>
						<TD class="header" colspan="2">
							<xsl:value-of select="NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Year"/>
						</TD>
						<TD class="gridHeader" colspan="1">Month :</TD>
						<TD class="header" colspan="2">
							<xsl:value-of select="NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Month"/>
						</TD>
					</TR>
					<TR></TR>
					<TR></TR>
					<TR>
						<TD class="gridHeader">ProductGroup</TD>						
						<TD class="gridHeader">ProductID</TD>
						<TD class="gridHeader">Product</TD>
						<TD class="gridHeader">1</TD>
						<TD class="gridHeader">2</TD>
						<TD class="gridHeader">3</TD>
						<TD class="gridHeader">4</TD>
						<TD class="gridHeader">5</TD>
						<TD class="gridHeader">6</TD>
						<TD class="gridHeader">7</TD>
						<TD class="gridHeader">8</TD>
						<TD class="gridHeader">9</TD>
						<TD class="gridHeader">10</TD>
						<TD class="gridHeader">11</TD>
						<TD class="gridHeader">12</TD>
						<TD class="gridHeader">13</TD>
						<TD class="gridHeader">14</TD>
						<TD class="gridHeader">15</TD>
						<TD class="gridHeader">16</TD>
						<TD class="gridHeader">17</TD>
						<TD class="gridHeader">18</TD>
						<TD class="gridHeader">19</TD>
						<TD class="gridHeader">20</TD>
						<TD class="gridHeader">21</TD>
						<TD class="gridHeader">22</TD>
						<TD class="gridHeader">23</TD>
						<TD class="gridHeader">24</TD>
						<TD class="gridHeader">25</TD>
						<TD class="gridHeader">26</TD>
						<TD class="gridHeader">27</TD>
						<TD class="gridHeader">28</TD>
						<TD class="gridHeader">29</TD>
						<TD class="gridHeader">30</TD>
						<TD class="gridHeader">31</TD>
						<TD class="gridHeader">MTD</TD>
						<TD class="gridHeader">Avg./Day</TD>
					</TR>
					<xsl:for-each select="NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT">
						<!--<xsl:sort select="concat(ProductGroupID,ProductSubGroupID,Seq)" />-->
						<TR>
							<TD class="ResultAltItem">
								<xsl:value-of select="ProductSubGroupName"/>
							</TD>							
							<TD class="ResultAltItem">
								<xsl:value-of select="ProductID"/>
							</TD>
							<TD class="ResultAltItem">
								<xsl:value-of select="ProductName"/>
							</TD>
							<xsl:choose>
								<xsl:when test="Day1 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day1),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day2 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day2),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day3 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day3),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day4 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day4),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day5 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day5),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day6 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day6),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day7 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day7),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day8 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day8),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day9 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day9),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day10 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day10),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day11 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day11),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day12 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day12),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day13 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day13),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day14 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day14),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day15 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day15),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day16 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day16),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day17 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day17),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day18 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day18),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day19 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day19),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day20 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day20),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day21 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day21),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day22 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day22),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day23 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day23),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day24 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day24),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day25 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day25),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day26 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day26),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day27 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day27),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day28 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day28),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day29 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day29),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day30 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day30),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Day31 > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Day31),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="AllTotalCarton > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(AllTotalCarton),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="Avg > 0">
									<TD class="ResultAltItem">
										<xsl:value-of select='format-number(sum(Avg),"#,##0.00")'/>
									</TD>
								</xsl:when>
								<xsl:otherwise>
									<td class="ResultNull" >
										-
									</td>
								</xsl:otherwise>
							</xsl:choose>
						</TR>
					</xsl:for-each>
					<TR Class="GroupFooter">
						<TD ColSpan="3">รวมทั้งสิ้น</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day1),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day2),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day3),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day4),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day5),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day6),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day7),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day8),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day9),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day10),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day11),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day12),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day13),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day14),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day15),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day16),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day17),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day18),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day19),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day20),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day21),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day22),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day23),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day24),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day25),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day26),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day27),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day28),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day29),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day30),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Day31),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/AllTotalCarton),"#,##0.00")'/>
						</TD>
						<TD Class="subTotals" align="right">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_DSR_Sales_By_Sku_Carton_XSLT/Avg),"#,##0.00")'/>
						</TD>
					</TR >
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>
