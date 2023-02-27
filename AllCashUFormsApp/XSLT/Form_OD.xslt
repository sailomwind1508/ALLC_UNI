<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Form_OD">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="ใบสั่งสินค้า">
				<ss:Table>
					<xsl:apply-templates select="Form_OD"/>
				</ss:Table>
			</ss:Worksheet>
		</ss:Workbook>
	</xsl:template>

	<xsl:template match="/">
		<html>
			<head>
				<style>
					.gridHeader {
					font-size: 11pt;
					font-weight: bold;
					vertical-align:middle;
					text-align:center;
					border: solid thin Black;
					background-color: #99CCFF;
					}
					.SearchValue
					{
					color: Black;
					font-size: 9pt;
					font-weight: bold;
					vertical-align:middle;
					text-align:left;
					border: solid thin Black;
					}
					.BorderG {
					border: solid thin Black;
					}
					.HReport {
					font-size: 11pt;
					font-weight: bold;
					text-align:center;
					border: solid thin Black;
					}
					.TDetail {
					border: solid thin Black;
					}
					.FReport {
					border-left: solid thin Black;
					border-right: solid thin Black;
					}
					.HL {
					border-left: solid thin Black;
					}
					.HR {
					border-right: solid thin Black;
					}
				</style>
			</head>
			<body class="content">
				<table>
					<tr>
						<td Colspan="4" style="font-weight: bold;font-size: 11pt;">
							<xsl:value-of select="NewDataSet/Form_OD/CompanyName"/>
						</td>
						<td Class="HReport" Colspan="5">
							ใบสั่งสินค้า
						</td>
					</tr>
					<tr>
						<td style="font-weight: bold;">ที่อยู่บริษัท :</td>
						<td Colspan="5">
							<xsl:value-of select="NewDataSet/Form_OD/AddrComp"/>
						</td>
					</tr>
					<tr>
						<td style="font-weight: bold;">เลขที่ประจำตัวผู้เสียภาษี :</td>
						<td>
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of select="NewDataSet/Form_OD/TaxId"/>
						</td>
					</tr>
					<tr></tr>

					<tr>
						<td class="HL" style="border-top: solid thin Black;font-weight: bold;">ผู้จำหน่าย :</td>
						<td Colspan="3" style="border-top: solid thin Black;font-weight: bold;">
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of select="NewDataSet/Form_OD/SupplierID"/>
						</td>
						<td Colspan="2" class="HL" style="border-top: solid thin Black;font-weight: bold;text-align:right;">เลขที่เอกสาร :</td>
						<td Colspan="3" class="HR" style="border-top: solid thin Black;font-weight: bold;">
							<xsl:value-of select="NewDataSet/Form_OD/DocNo"/>
						</td>
					</tr>
					<tr>
						<td class="HL" style="font-weight: bold;">บริษัท :</td>
						<td Colspan="3">
							<xsl:value-of select="NewDataSet/Form_OD/SuppName"/>
						</td>
						<td Colspan="2" class="HL" style="font-weight: bold;text-align:right;">วันที่ :</td>
						<td Colspan="3" class="HR">
							<xsl:value-of select="NewDataSet/Form_OD/DocDate"/>
						</td>
					</tr>
					<tr>
						<td class="HL" style="font-weight: bold;">ที่อยู่ :</td>
						<td Colspan="3">
							<xsl:value-of select="NewDataSet/Form_OD/Address"/>
						</td>
						<td Colspan="2" class="HL" style="font-weight: bold;text-align:right;">เอกสารอ้างอิง :</td>
						<td Colspan="3" class="HR">
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of select="NewDataSet/Form_OD/DocRef"/>
						</td>
					</tr>
					<tr>
						<td class="HL" style="font-weight: bold;">สถานที่ส่งสินค้า :</td>
						<td Colspan="3">
							<xsl:value-of select="NewDataSet/Form_OD/Shipto"/>
						</td>
						<td Colspan="2" class="HL" style="font-weight: bold;text-align:right;">คลังต้นทาง :</td>
						<td Colspan="3" class="HR">
							<!--<xsl:value-of select="NewDataSet/Form_OD/EmpName"/>-->
						</td>
					</tr>
					<tr>
						<td Colspan="4" class="HL"></td>
						<td Colspan="2" class="HL" style="font-weight: bold;text-align:right;">คลังปลายทาง :</td>
						<td Colspan="3" class="HR">
							<!--<xsl:value-of select="NewDataSet/Form_OD/EmpIDCard"/>-->
						</td>
					</tr>
					<tr>
						<td class="HL" style="font-weight: bold;">หมายเหตุ :</td>
						<td Colspan="3" style="text-align:left;">
							<xsl:value-of select="NewDataSet/Form_OD/Remark"/>
						</td>
						<td Colspan="2" class="HL" style="font-weight: bold;text-align:right;">ศูนย์ :</td>
						<td Colspan="3" class="HR">
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of select="NewDataSet/Form_OD/BranchName"/>
						</td>
					</tr>
					<TR>
						<TD CLASS="HReport">No</TD>
						<TD CLASS="HReport">รหัสสินค้า</TD>
						<TD Colspan="2" CLASS="HReport">รายการ</TD>
						<TD CLASS="HReport">จำนวน</TD>
						<TD CLASS="HReport">หน่วย</TD>
						<TD CLASS="HReport">Vat</TD>
						<TD CLASS="HReport">หน่วยละ</TD>
						<TD CLASS="HReport">จำนวนเงิน</TD>
					</TR>
					<xsl:for-each select="NewDataSet/Form_OD">
						<xsl:sort select="ProductID"/>
						<tr>
							<TD class="TDetail">
								<xsl:value-of  select="position()"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="ProductID"/>
							</TD>
							<TD class="TDetail" Colspan="2">
								<xsl:value-of  select="ProductName"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of select="format-number(OrderQty,'#,##0.00')"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="ProductUomName"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of select="format-number(LineVatAmt,'#,##0.00')"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of select="format-number(UnitPrice,'#,##0.00')"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of select="format-number(LineTotal,'#,##0.00')"/>
							</TD>
						</tr>
					</xsl:for-each>
					<tr>
						<td>หมายเหตุเพิ่มเติม :</td>
						<td Colspan="2">
							<xsl:value-of select="NewDataSet/Form_OD/Comment"/>
						</td>
						<td class="TDetail" style="font-weight: bold;text-align:right;">รวม</td>
						<td class="TDetail" style="font-weight: bold;text-align:right;">
							<xsl:value-of select="format-number(sum(NewDataSet/Form_OD/OrderQty),'#,##0.00')"/>
						</td>
						<td Colspan="4" class="TDetail"></td>
					</tr>
					<tr>
						<td Colspan="5"></td>
						<td Colspan="2" class="FReport" style="border-top: solid thin Black;">
							รวมเป็นเงิน
						</td>
						<td Colspan="2" class="FReport" style="border-top: solid thin Black;">
							<xsl:value-of select="format-number(NewDataSet/Form_OD/Amount,'#,##0.00')"/>
						</td>
					</tr>
					<tr>
						<td Colspan="5"></td>
						<td Colspan="2" class="FReport">สินค้ายกเว้นภาษีมูลค่าเพิ่ม</td>
						<td Colspan="2" class="FReport">
							<xsl:value-of select="format-number(NewDataSet/Form_OD/ExcVat,'#,##0.00')"/>
						</td>
					</tr>
					<tr>
						<td Colspan="5"></td>
						<td Colspan="2" class="FReport">
							<xsl:value-of select="format-number(NewDataSet/Form_OD/VatRate,'#,##0.00')"/>
						</td>
						<td Colspan="2" class="FReport">
							<xsl:value-of select="format-number(NewDataSet/Form_OD/IncVat,'#,##0.00')"/>
						</td>
					</tr>
					<tr>
						<td Colspan="5"></td>
						<td Colspan="2" class="FReport">จำนวนภาษีมูลค่าเพิ่ม</td>
						<td Colspan="2" class="FReport">
							<xsl:value-of select="format-number(NewDataSet/Form_OD/VatAmt,'#,##0.00')"/>
						</td>
					</tr>
					<tr>
						<td Colspan="5" Class="HReport">
							<xsl:value-of select="NewDataSet/Form_OD/ThaiTxt"/>
						</td>
						<td Colspan="2" class="TDetail" style="font-weight: bold;">จำนวนเงินรวมทั้งสิ้น</td>
						<td Colspan="2" class="TDetail" style="font-weight: bold;">
							<xsl:value-of select="format-number(NewDataSet/Form_OD/TotalDue,'#,##0.00')"/>
						</td>
					</tr>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>