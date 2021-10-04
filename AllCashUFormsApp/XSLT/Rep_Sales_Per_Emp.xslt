<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Rep_Sales_Per_Emp_XSLT">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="รายงานสรุปยอดขายแยกตามพนักงาน (รายวัน/รายเดือน)">
				<ss:Table>
					<xsl:apply-templates select="Rep_Sales_Per_Emp_XSLT"/>
				</ss:Table>
			</ss:Worksheet>
		</ss:Workbook>
	</xsl:template>

	<xsl:key name="groupDocDate" match="NewDataSet/Rep_Sales_Per_Emp_XSLT" use="DocDate" />
	<xsl:key name="groupWHID" match="NewDataSet/Rep_Sales_Per_Emp_XSLT" use="WHID" />
	<xsl:key name="groupEmpID" match="NewDataSet/Rep_Sales_Per_Emp_XSLT" use="EmpIDCard" />
	<xsl:key name="groupWHEmp" match="NewDataSet/Rep_Sales_Per_Emp_XSLT" use="concat(WHID,' ',EmpIDCard)" />
	
	<xsl:template match="/">
		<HTML>
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
          font-size: 10pt;
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
          text-align: center;
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
          font-size: 9pt;
          }
          .SearchResultItemCenter {
          background-color: #FFFFFF;
          color: Black;
          border: solid thin Black;
          font-size: 9pt;
          text-align: center;
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
          .GroupFooterRight{
          color: Black;
          font-weight: bold;
          font-style:Regular;
          text-align: right;
          padding-left: 1px;
          padding-top: 1px;
          padding-bottom: 1px;
          width: 100%;
          font-size: 9pt;
          }
          .GroupFooterSum{
          color: Black;
          font-weight: bold;
          font-style:Regular;
          text-align: left;
          padding-left: 1px;
          padding-top: 1px;
          padding-bottom: 1px;
          width: 100%;
          font-size: 9pt;
          border-bottom:2px solid black;
          }
          .GroupFooterSumRight{
          color: Black;
          font-weight: bold;
          font-style:Regular;
          text-align: right;
          padding-left: 1px;
          padding-top: 1px;
          padding-bottom: 1px;
          width: 100%;
          font-size: 9pt;
          border-bottom:2px solid black;
          }
          .GroupTotal{text-align:right;}
          .subTotals {border-bottom:1px solid black}
				</STYLE>
			</HEAD>
			
			<BODY class="content">
				<TABLE>

					<TR>
						<TD class="stdPageHdr" colspan="11">
							<xsl:value-of select="NewDataSet/Rep_Sales_Per_Emp_XSLT/CompanyName"/>
						</TD>
					</TR> <!--Header-->

					<TR>
						<TD class="stdPageHdr" colspan="11">รายงานสรุปยอดขายแยกตามพนักงาน (รายวัน/รายเดือน)</TD>
					</TR> <!--Header-->

					<TR>
						<TD align="left">ศูนย์ : </TD>
						<TD align="left" colspan="10">
							<xsl:value-of select="NewDataSet/Rep_Sales_Per_Emp_XSLT/BranchName"/>
						</TD>
					</TR><!--ศูนย์ :-->
					
					<TR>
						<TD align="right">วันที่</TD>
						<TD align="left">
							<xsl:value-of select="NewDataSet/Rep_Sales_Per_Emp_XSLT/DateFr"/>
						</TD>
						
						<TD align="right">ถึง</TD>
						<TD align="left">
							<xsl:value-of select="NewDataSet/Rep_Sales_Per_Emp_XSLT/DateTo"/>
						</TD>
					</TR><!--วันที่ :-->

					<xsl:for-each select="NewDataSet/Rep_Sales_Per_Emp_XSLT[count(. | key('groupDocDate',DocDate)[1]) = 1]">
						
					<TR>
						<TD CLASS="gridHeader" align="center">ลำดับ</TD>
						<TD CLASS="gridHeader" align="center">วันที่</TD>
						<TD CLASS="gridHeader" align="center">รหัส Van</TD>
						<TD CLASS="gridHeader" align="center">รหัสพนักงาน</TD>
						<TD CLASS="gridHeader" align="center">ชื่อพนักงานขาย</TD>
						<TD CLASS="gridHeader" align="center">ส่วนลด</TD>
						<TD CLASS="gridHeader" align="center">มูลค่าสินค้า</TD>
						<TD CLASS="gridHeader" align="center">VAT.</TD>
						<TD CLASS="gridHeader" align="center">รวมทั้งสิ้น</TD>
						<TD CLASS="gridHeader" align="center">น้ำหนัก kg.</TD>
						<TD CLASS="gridHeader" align="center">เลขที่ใบกำกับ</TD>
					</TR><!--ชื่อคอลัมน์-->

					<xsl:for-each select="key('groupDocDate',DocDate)">
						<xsl:sort select="DocDate"/>
						
					<TR>
							<TD CLASS="SearchResultItem">
								<xsl:value-of select="position()"/>
							</TD>

							<TD CLASS="SearchResultItem">
								<xsl:value-of select="DocDate"/>
							</TD>

							<TD CLASS="SearchResultItem">
								<xsl:value-of select="WHID"/>
							</TD>

							<TD CLASS="SearchResultItem">
								<xsl:value-of select="EmpIDCard"/>
							</TD>

							<TD CLASS="SearchResultItem">
								<xsl:value-of select="EmpName"/>
							</TD>

							<TD CLASS="SearchResultItem">
								<xsl:value-of select="Discount"/>
							</TD>

							<TD CLASS="SearchResultItem">
								<xsl:value-of select="Amount"/>
							</TD>

							<TD CLASS="SearchResultItem">
								<xsl:value-of select="VatAmt"/>
							</TD>

							<TD CLASS="SearchResultItem">
								<xsl:value-of select="TotalDue"/>
							</TD>

							<TD CLASS="SearchResultItem">
								<xsl:value-of select="Weight"/>
							</TD>

							<TD CLASS="SearchResultItem">
								<xsl:value-of select="DocNo"/>
							</TD>
							
					</TR><!--ข้อมูลแถว-->
					
					</xsl:for-each>

					<TR>
							<TD ColSpan="5" Class="GroupFooter">
								รวม  <xsl:value-of select="format-number(count(key('groupDocDate',DocDate)/WHID),'#,##0')"/> Van
							</TD>
						
							<TD Class="GroupFooter">
								<xsl:value-of  select="format-number(sum(key('groupDocDate',DocDate)/Discount),'#,##0.00')" />
							</TD>
						
							<TD Class="GroupFooter">
								<xsl:value-of  select="format-number(sum(key('groupDocDate',DocDate)/Amount),'#,##0.00')" />
							</TD>
						
							<TD Class="GroupFooter">
								<xsl:value-of  select="format-number(sum(key('groupDocDate',DocDate)/VatAmt),'#,##0.00')" />
							</TD>
						
							<TD Class="GroupFooter">
								<xsl:value-of  select="format-number(sum(key('groupDocDate',DocDate)/TotalDue),'#,##0.00')" />
							</TD>
						
							<TD Class="GroupFooterRight">
								<xsl:value-of  select="format-number(sum(key('groupDocDate',DocDate)/Weight),'#,##0.00')" />
							</TD>
						
						</TR> <!--สรุปรวมในแต่ละวัน-->

					</xsl:for-each>
					
					<TR Class="GroupFooter">	
						
						<TD ColSpan="8">ยอดขายรวมทั้งสิ้น</TD>
						
						<TD Class="subTotals">
							<xsl:value-of select='format-number(sum(/NewDataSet/Rep_Sales_Per_Emp_XSLT/TotalDue),"#,##0.00")'/>
						</TD>
						
					</TR ><!--ยอดขายรวมทั้งสิ้น-->

					<TR>
	
					</TR><!--เว้นบรรทัด-->
					<TR>

					</TR><!--เว้นบรรทัด-->
					<TR>

					</TR><!--เว้นบรรทัด-->
					
					<TR>
						<TD class="stdPageHdr" colspan="10">รวมทั้งหมดตามพนักงาน</TD>
					</TR><!--คอลัมรวมทั้งหมดตามพนักงาน-->

					<TR>

					</TR><!--เว้นบรรทัด-->
					
					<TR>
						<TD CLASS="gridHeader">ลำดับ</TD>
						<TD CLASS="gridHeader">วันทำงาน</TD>
						<TD CLASS="gridHeader">รหัส Van</TD>
						<TD CLASS="gridHeader">
							รหัส<br />พนักงาน
						</TD>
						<TD CLASS="gridHeader" WIDTH="220">ชื่อพนักงานขาย</TD>
						<TD CLASS="gridHeader">ส่วนลด</TD>
						<TD CLASS="gridHeader">มูลค่าสินค้า</TD>
						<TD CLASS="gridHeader">VAT.</TD>
						<TD CLASS="gridHeader">รวมทั้งสิ้น</TD>
						<TD CLASS="gridHeader">น้ำหนัก kg.</TD>
					</TR><!--คอลัมน์รวมทั้งหมดตามพนักงาน-->

					<xsl:for-each select="NewDataSet/Rep_Sales_Per_Emp_XSLT[count(. | key('groupWHEmp', concat(WHID,' ',EmpIDCard))[1]) = 1]">
					<xsl:sort select="concat(WHID,' ',EmpIDCard)" />

					<TR>
						<TD class="SearchResultItem">
							<xsl:value-of select="position()" />
						</TD>
						
						<TD class="SearchResultItem">
							<xsl:value-of select="format-number(count(key('groupWHEmp',concat(WHID,' ',EmpIDCard))/DocDate),'#,##0')"/> วัน
						</TD>
						
						<TD class="SearchResultItem">
							<xsl:value-of select="WHID"/>
						</TD>
						
						<TD class="SearchResultItemCenter">
							<xsl:value-of select="EmpIDCard"/>
						</TD>
						
						<TD class="SearchResultItem">
							<xsl:value-of select="EmpName"/>
						</TD>
						
						<TD Class="SearchResultItem">
							<xsl:value-of  select="format-number(sum(key('groupWHEmp',concat(WHID,' ',EmpIDCard))/Discount),'#,##0.00')" />
						</TD>
						
						<TD Class="SearchResultItem">
							<xsl:value-of  select="format-number(sum(key('groupWHEmp',concat(WHID,' ',EmpIDCard))/Amount),'#,##0.00')" />
						</TD>
						
						<TD Class="SearchResultItem">
							<xsl:value-of  select="format-number(sum(key('groupWHEmp',concat(WHID,' ',EmpIDCard))/VatAmt),'#,##0.00')" />
						</TD>
						
						<TD Class="SearchResultItem">
							<xsl:value-of  select="format-number(sum(key('groupWHEmp',concat(WHID,' ',EmpIDCard))/TotalDue),'#,##0.00')" />
						</TD>
						
						<TD Class="SearchResultItem">
							<xsl:value-of  select="format-number(sum(key('groupWHEmp',concat(WHID,' ',EmpIDCard))/Weight),'0.000')" />
						</TD>
						
					</TR><!--รวมยอดขายรวมทั้งสิ้น-->
					
					</xsl:for-each>
			
				    <TR>
                        <TD ColSpan="5" Class="GroupFooter">
                            รวมทั้งหมด : 
                        </TD>
						
                        <TD Class="GroupFooterSumRight">
                            <xsl:value-of  select="format-number(sum(/NewDataSet/Rep_Sales_Per_Emp_XSLT/Discount),'#,##0.00')" />
                        </TD>
						
                        <TD Class="GroupFooterSumRight">
                            <xsl:value-of  select="format-number(sum(/NewDataSet/Rep_Sales_Per_Emp_XSLT/Amount),'#,##0.00')" />
                        </TD>
						
                        <TD Class="GroupFooterSumRight">
                            <xsl:value-of  select="format-number(sum(/NewDataSet/Rep_Sales_Per_Emp_XSLT/VatAmt),'#,##0.00')" />
                        </TD>
						
                        <TD Class="GroupFooterSumRight">
                            <xsl:value-of  select="format-number(sum(/NewDataSet/Rep_Sales_Per_Emp_XSLT/TotalDue),'#,##0.00')" />
                        </TD>
						
                        <TD Class="GroupFooterSumRight">
                            <xsl:value-of  select="format-number(sum(/NewDataSet/Rep_Sales_Per_Emp_XSLT/Weight),'0.000')" />
                        </TD>
                    </TR><!--รวมทั้งหมด-->
				
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>
