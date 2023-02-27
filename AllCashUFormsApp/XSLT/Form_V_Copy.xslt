﻿<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
			xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
			version="1.0">
	<xsl:output encoding="utf-8" />
	<xsl:template match="Form_V">
		<xsl:text disable-output-escaping="yes">
    <![CDATA[<?mso-application progid="Excel.Sheet"?> ]]>    
    </xsl:text>
		<ss:Workbook>
			<ss:Worksheet ss:Name="ใบกำกับภาษีเต็มรูป">
				<ss:Table>
					<xsl:apply-templates select="Form_V"/>
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
							<xsl:value-of select="NewDataSet/Form_V/CompanyName"/>
						</td>
						<td Class="HReport" Colspan="4">
							สำเนาใบกำกับภาษี/สำเนาใบเสร็จรับเงิน
						</td>
					</tr>
					<tr>
						<td style="font-weight: bold;">ที่อยู่บริษัท :</td>
						<td Colspan="5">
							<xsl:value-of select="NewDataSet/Form_V/AddrBranch"/>
						</td>
					</tr>
					<tr>
						<td style="font-weight: bold;">โทร :</td>
						<td Colspan="1" style="text-align:center;">
							<xsl:value-of select="NewDataSet/Form_V/Phone"/>
						</td>
						<td style="font-weight: bold;">เลขที่ประจำตัวผู้เสียภาษี :</td>
						<td Colspan="1">
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of select="NewDataSet/Form_V/TaxId"/>
						</td>
					</tr>
					<tr></tr>

					<tr>
						<td class="HL" style="border-top: solid thin Black;font-weight: bold;">รหัสลูกค้า :</td>
						<td Colspan="4" style="border-top: solid thin Black;font-weight: bold;">
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of select="NewDataSet/Form_V/CustomerID"/>
						</td>
						<td class="HL" style="border-top: solid thin Black;font-weight: bold;">วันที่ :</td>
						<td Colspan="2" class="HR" style="border-top: solid thin Black;font-weight: bold;">
							<xsl:value-of select="NewDataSet/Form_V/DocDate"/>
						</td>
					</tr>
					<tr>
						<td class="HL" style="font-weight: bold;">ชื่อลูกค้า :</td>
						<td Colspan="4">
							<xsl:value-of select="NewDataSet/Form_V/CustName"/>
						</td>
						<td class="HL" style="font-weight: bold;">เลขที่ :</td>
						<td Colspan="2" class="HR">
							<xsl:value-of select="NewDataSet/Form_V/DocNo"/>
						</td>
					</tr>
					<tr>
						<td class="HL" style="font-weight: bold;">ที่อยู่ลุกค้า :</td>
						<td Colspan="4">
							<xsl:value-of select="NewDataSet/Form_V/Address"/>
						</td>
						<td class="HL" style="font-weight: bold;">เลขที่อ้างอิง :</td>
						<td Colspan="2" class="HR">
							<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
							<xsl:value-of select="NewDataSet/Form_V/DocRef"/>
						</td>
					</tr>
					<tr>
						<td Colspan="5" class="HL"></td>
						<td class="HL" style="font-weight: bold;">พนักงานขาย :</td>
						<td Colspan="2" class="HR">
							<xsl:value-of select="NewDataSet/Form_V/EmpName"/>
						</td>
					</tr>
					<tr>
						<td Colspan="5" class="HL"></td>
						<td class="HL"></td>
						<td Colspan="2" class="HR">
							<xsl:value-of select="NewDataSet/Form_V/EmpIDCard"/>
						</td>
					</tr>
					<tr>
						<td class="HL" style="font-weight: bold;">โทร :</td>
						<td style="text-align:center;">
							<xsl:value-of select="NewDataSet/Form_V/ContactTel"/>
						</td>
						<td style="font-weight: bold;">เลขประจำตัวผู้เสียภาษี(ลูกค้า) :</td>
						<td Colspan="2">
							<xsl:value-of select="NewDataSet/Form_V/TaxIDCust"/>
						</td>
						<td class="HL" style="font-weight: bold;">เขตการขาย :</td>
						<td Colspan="2" class="HR">
							<xsl:value-of select="NewDataSet/Form_V/SalAreaName"/>
						</td>
					</tr>
					<TR>
						<TD CLASS="HReport">No</TD>
						<TD CLASS="HReport">รหัสสินค้า</TD>
						<TD Colspan="2" CLASS="HReport">รายละเอียดสินค้า</TD>
						<TD CLASS="HReport">จำนวน</TD>
						<TD CLASS="HReport">หน่วยนับ</TD>
						<TD CLASS="HReport">ราคาหน่วยละ</TD>
						<TD CLASS="HReport">จำนวนเงิน</TD>
					</TR>
					<xsl:for-each select="NewDataSet/Form_V">
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
								<xsl:value-of select="format-number(ReceivedQty,'#,##0.00')"/>
							</TD>
							<TD class="TDetail">
								<xsl:value-of  select="ProductUomName"/>
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
						<td>หมายเหตุ</td>
						<td Colspan="4" CLASS="BorderG">
							<xsl:value-of select="NewDataSet/Form_V/Comment"/>
						</td>
						<td Colspan="2" class="FReport">มูลค่าก่อนหักส่วนลด</td>
						<td class="FReport">
							<xsl:value-of select="format-number(NewDataSet/Form_V/Amount,'#,##0.00')"/>
						</td>
					</tr>
					<tr>
						<td Colspan="5"></td>
						<td Colspan="2" class="FReport">ส่วนลด</td>
						<td class="FReport">
							<xsl:value-of select="format-number(NewDataSet/Form_V/Discount,'#,##0.00')"/>
						</td>
					</tr>
					<tr>
						<td Colspan="5"></td>
						<td Colspan="2" class="FReport">ก่อนภาษี</td>
						<td class="FReport">
							<xsl:value-of select="format-number(NewDataSet/Form_V/BeVat,'#,##0.00')"/>
						</td>
					</tr>
					<tr>
						<td Colspan="5"></td>
						<td Colspan="2" class="FReport">ภาษีมูลค่าเพิ่ม %</td>
						<td class="FReport">
							<xsl:value-of select="format-number(NewDataSet/Form_V/VatAmt,'#,##0.00')"/>
						</td>
					</tr>
					<tr>
						<td Colspan="5"></td>
						<td Colspan="2" class="FReport">สินค้ายกเว้นภาษี</td>
						<td class="FReport">
							<xsl:value-of select="format-number(NewDataSet/Form_V/ExcVat,'#,##0.00')"/>
						</td>
					</tr>
					<tr>
						<td Colspan="5" Class="HReport">
							<xsl:value-of select="NewDataSet/Form_V/ThaiTxt"/>
						</td>
						<td Colspan="2" class="TDetail" style="font-weight: bold;">รวมเงินสุทธิ</td>
						<td class="TDetail" style="font-weight: bold;">
							<xsl:value-of select="format-number(NewDataSet/Form_V/TotalDue,'#,##0.00')"/>
						</td>
					</tr>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>