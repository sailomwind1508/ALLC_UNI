<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_Customer_List_By_WH_XSLT">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานสรุปจำนวนร้านค้าทั้งหมด(ตามคลังรถ)">
				<ss:Table>
					<xsl:apply-templates select="Rep_Customer_List_By_WH_XSLT"/>
				</ss:Table>
			</ss:Worksheet>
		</ss:Workbook>
	</xsl:template>
	
  <xsl:key name="groupWHID" match="NewDataSet//Rep_Customer_List_By_WH_XSLT" use="WHID" />
  <xsl:key name="groupEmpID" match="NewDataSet/Rep_Customer_List_By_WH_XSLT" use="concat(WHID,' ',EmpID)" />
  <xsl:key name="groupSalAreaID" match="NewDataSet/Rep_Customer_List_By_WH_XSLT" use="SalAreaID" />
	  
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
			<BODY>
				<TABLE>

						<TR>
							<TD class="stdPageHdr" align="left" colspan="14">
								<xsl:value-of select="NewDataSet/Rep_Customer_List_By_WH_XSLT/CompanyName"/>
							</TD>
						</TR>

						<TR>
							<TD class="stdPageHdr" align="left" colspan="14">รายงานสรุปจำนวนร้านค้าทั้งหมด(ตามคลังรถ)</TD>
						</TR>

						<TR>
							<TD Colspan="2" align="right">Depot</TD>
							<TD align="left">
								<xsl:value-of select="NewDataSet/Rep_Customer_List_By_WH_XSLT/BranchName"/>
							</TD>
						</TR>
					
		<xsl:for-each select="NewDataSet/Rep_Customer_List_By_WH_XSLT[count(. | key('groupWHID', WHID)[1]) = 1]">
               <xsl:for-each select="key('groupWHID', WHID)">
                 <xsl:sort select="WHID" />
                 <xsl:if test="count(. | key('groupEmpID', concat(WHID,' ',EmpID))[1]) = 1">
					
						<TR>
							<TD Colspan="2" align="right">
								Saleman :
							</TD>

							<TD align="left">
								<xsl:value-of select="WHID"/>
							</TD>

							<TD align="left" colspan="3">
								<xsl:value-of select="NAME"/>
							</TD>
						</TR>

					 <xsl:for-each select="key('groupEmpID', concat(WHID,' ',EmpID))">
                     <xsl:sort select="concat(WHID,' ',EmpID)"/>
                     <xsl:if test="count(. | key('groupSalAreaID',SalAreaID)[1]) = 1">      
					 
						<TR>
							<TD align ="right" colspan="2">
								ตลาด :
							</TD>
							<TD align ="left" colspan="1">
								<xsl:value-of select="SalAreaID"/>
							</TD>
							<TD align="left" colspan="3">
								<xsl:value-of select="SalAreaName"/>
							</TD>
						</TR>

						<TR>
							<TD CLASS="gridHeader">No.</TD>
							<TD CLASS="gridHeader">ตลาด</TD>
							<TD CLASS="gridHeader">รหัสลูกค้า</TD>
							<TD CLASS="gridHeader">ประเภทร้านค้า</TD>
							<TD CLASS="gridHeader">ชื่อลูกค้า</TD>
							<TD CLASS="gridHeader">ที่อยู่</TD>
							<TD CLASS="gridHeader">ตำบล</TD>
							<TD CLASS="gridHeader">อำเภอ</TD>
							<TD CLASS="gridHeader">จังหวัด</TD>
							<TD CLASS="gridHeader">รหัสไปรษณีย์</TD>
							<TD CLASS="gridHeader">โทรศัพท์</TD>
							<TD CLASS="gridHeader">Shelf No.</TD>
							<TD CLASS="gridHeader">Lat</TD>
							<TD CLASS="gridHeader">Lng</TD>
						</TR>

					 <xsl:for-each select="key('groupSalAreaID', SalAreaID)">
                     <xsl:sort select="SalAreaID"/>
						<TR>

							<TD class="SearchResultItem">
									<xsl:value-of  select="position()"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="Seq"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
									<xsl:value-of  select="CustomerID"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="ShopTypeName"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="CustName"/>
								</TD>

								<TD class="SearchResultItem" width="150">
									<xsl:value-of select="BillTo"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="DistrictName"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="AreaName"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="ProvinceName"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="PostalCode"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
									<xsl:value-of select="Telephone"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
									<xsl:value-of select="ShelfID"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="Latitude"/>
								</TD>

								<TD class="SearchResultItem">
									<xsl:value-of select="Longitude"/>
								</TD>
						</TR >
					</xsl:for-each>

					<TR>
						<TD Class="GroupTotal" colspan="3" align="right">
							ตลาด 
							(<xsl:value-of select="SalAreaID"/>)
						</TD>

						<TD>
							ลูกค้ารวม
							<xsl:value-of select="count(key('groupSalAreaID', SalAreaID)/CustomerID)"/>
						</TD>
					</TR >
					
					</xsl:if>
                    </xsl:for-each >
					   
					 <TR>
						<TD align="left" colspan="3" Class="GroupFooter">
							ลูกค้ารวม (<xsl:value-of select="concat(WHID,' ',EmpID)"/>)
						</TD>

						<TD align="left">
							<xsl:value-of select="count(key('groupEmpID',concat(WHID,' ',EmpID))/CustomerID)"/>
							รายการ
						</TD>
					</TR >
		 		
				   <TR>
                     <TD></TD>
                   </TR>
				 
					</xsl:if >
				</xsl:for-each>
	 
			 </xsl:for-each >           
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>

</xsl:stylesheet>
