using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class DocTypeCode
    {
        CultureInfo cultures = CultureInfo.CreateSpecificCulture("th-TH");

        public string GenDocNo(string docTypeCode, string whCode = null)
        {
            try
            {
                string ret = string.Empty;
                var tbl_DocumentType = (new tbl_DocumentType()).SelectAll().FirstOrDefault(x => x.DocTypeCode == docTypeCode);
                if (tbl_DocumentType != null) //PO
                {
                    int runLength = tbl_DocumentType.RunLength.Value;
                    string mode = "";

                    Func<tbl_POMaster, bool> tbl_POMasterPre = null;
                    if (docTypeCode != "IM")
                        tbl_POMasterPre = (x => x.DocTypeCode == docTypeCode);
                    else if (docTypeCode == "IM")
                        tbl_POMasterPre = (x => x.DocTypeCode == "IV" && x.DocRef == docTypeCode);

                    var tbl_POMasters = (new tbl_POMaster()).Select(tbl_POMasterPre);
                    if (tbl_POMasters != null && tbl_POMasters.Count > 0)
                    {
                        mode = "PO";
                        var maxAutoID = tbl_POMasters.Max(x => x.AutoID);
                        tbl_POMaster tbl_POMaster = tbl_POMasters.FirstOrDefault(x => x.AutoID == maxAutoID);
                   
                        ret = CheckDocNo(docTypeCode, tbl_DocumentType, runLength, whCode, tbl_POMaster.DocNo);
                    }

                    if (string.IsNullOrEmpty(mode)) //(tbl_POMasters.Count == 0) //PR
                    {
                        Func<tbl_PRMaster, bool> tbl_PRMasterPre = (x => x.DocTypeCode == docTypeCode);
                        var tbl_PRMasters = (new tbl_PRMaster()).Select(tbl_PRMasterPre);
                        if (tbl_PRMasters != null && tbl_PRMasters.Count > 0)
                        {
                            mode = "PR";
                            var maxAutoID = tbl_PRMasters.Max(x => x.AutoID);
                            var tbl_PRMaster = tbl_PRMasters.FirstOrDefault(x => x.AutoID == maxAutoID);

                            ret = CheckDocNo(docTypeCode, tbl_DocumentType, runLength, whCode, tbl_PRMaster.DocNo);
                        }
                    }
                    
                    if (string.IsNullOrEmpty(mode))
                    {
                        Func<tbl_IVMaster, bool> tbl_IVMasterPre = (x => x.DocTypeCode.Trim() == docTypeCode.Trim());
                        var tbl_IVMasters = (new tbl_IVMaster()).Select(tbl_IVMasterPre);
                        if (tbl_IVMasters != null && tbl_IVMasters.Count > 0)
                        {
                            mode = "IV";
                            var maxAutoID = tbl_IVMasters.Max(x => x.AutoID);
                            var tbl_IVMaster = tbl_IVMasters.FirstOrDefault(x => x.AutoID == maxAutoID);

                            ret = CheckDocNo(docTypeCode, tbl_DocumentType, runLength, whCode, tbl_IVMaster.DocNo);
                        }
                    }
                    
                    if (string.IsNullOrEmpty(mode))
                        ret = GenDocNo(docTypeCode, tbl_DocumentType, "0", runLength);
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return string.Empty;
            }
        }

        private string CheckDocNo(string docTypeCode, tbl_DocumentType tbl_DocumentType, int runLength, string whCode, string docNo)
        {
            string ret = "";
            string _rNoTemp = "0";
            string dateFormate = "";
            string tempMonth = "";
            int startLen = 0;

            if (tbl_DocumentType.DocFormat.Contains("YYMMDD"))
            {
                dateFormate = "yyMMdd";

                if (docTypeCode == "PO" || docTypeCode == "PR" || docTypeCode == "SO")
                    startLen = 2;
                else
                    startLen = 5;
            }
            else if (tbl_DocumentType.DocFormat.Contains("YYMM"))
            {
                dateFormate = "yyMM";
                startLen = 5;
            }
            else if (tbl_DocumentType.DocFormat.Contains("MMYY"))
            {
                dateFormate = "MMyy";
                startLen = 2;
            }
            else if (tbl_DocumentType.DocFormat.Contains("YY"))
            {
                dateFormate = "yy";
                startLen = 2;
            }

            tempMonth = Convert.ToDateTime(DateTime.Now, cultures).ToString(dateFormate);

            if (docNo.Substring(startLen, dateFormate.Length) == tempMonth)
            {
                _rNoTemp = docNo.Substring(docNo.Length - runLength, runLength);
                ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength, whCode);
            }          
            else
            {
                if (docTypeCode == "IV" || docTypeCode == "IM" || docTypeCode == "V")
                    ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength, whCode);
                else
                    ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength);
            }

            return ret;
        }

        private string GenDocNo(string docTypeCode, tbl_DocumentType tbl_DocumentType, string _rNoTemp, int runLength, string whCode = null)
        {
            string ret = "";
            string docFormat = tbl_DocumentType.DocFormat;

            List<char> docFormatArr = new List<char>();
            docFormatArr = docFormat.ToCharArray().ToList();

            string compCode = (new tbl_Company()).SelectAll().FirstOrDefault().CompanyCode;

            DateTime cDate = DateTime.Now;

            int _rNo = Convert.ToInt32(_rNoTemp) + 1;

            string _docType = "";
            string _compCode = "";
            string _year = "";
            string _month = "";
            string _day = "";
            string _m = "";

            string formateRunNo = "";
            for (int i = 0; i < runLength; i++)
            {
                formateRunNo += "0";
            }

            string tempRunningNo = _rNo.ToString(formateRunNo);
            string _runningNo = tempRunningNo;

            for (int i = 0; i < docFormatArr.Count; i++)
            {
                if (i <= docTypeCode.Length && docTypeCode.Contains(docFormatArr[i]))
                {
                    _docType = docTypeCode;// += docFormatArr[i];
                }
                if (docFormatArr[i] == '#')
                {
                    if (docTypeCode == "IV" || docTypeCode == "IM")
                    {
                        if (whCode != null && !string.IsNullOrEmpty(whCode))
                            _compCode = string.Join("", whCode.Split('V').ToList());
                    }
                    else if (docTypeCode == "V")
                    {
                        Func<tbl_Branch, bool> whFunc = (x => x.BranchCode == whCode.Split('V')[0]);
                        var wh = (new IV()).GetBranch(whFunc);
                        if (wh != null && wh.Count > 0)
                            _compCode = wh[0].AgentID;
                    }
                    else
                        _compCode = compCode;
                }
                if (docFormatArr[i] == 'Y')
                {
                    var yFormat = docFormatArr[i].ToString() + docFormatArr[i + 1].ToString();
                    if (yFormat == "YY")
                        _year = cDate.ToString("yy", cultures);
                }
                if (docFormatArr[i] == 'M')
                {
                    if (docTypeCode == "IM")
                    {
                        if (docFormatArr[i + 1] == 'M')
                        {
                            var mFormat = docFormatArr[i].ToString() + docFormatArr[i + 1].ToString();
                            if (mFormat == "MM")
                                _month = cDate.ToString("MM", cultures);
                        }
                        else
                        {
                            _m = docFormatArr[i].ToString();
                        }
                    }
                    else
                    {
                        var mFormat = docFormatArr[i].ToString() + docFormatArr[i + 1].ToString();
                        if (mFormat == "MM")
                            _month = cDate.ToString("MM", cultures);
                    }
                }
                if (docFormatArr[i] == 'D')
                {
                    var dFormat = docFormatArr[i].ToString() + docFormatArr[i + 1].ToString();
                    if (dFormat == "DD")
                        _day = cDate.ToString("dd", cultures);
                }
                if (docFormatArr[i] == '0')
                {
                    _runningNo = tempRunningNo;
                }
            }

            if (docTypeCode == "IM")
                ret = string.Join("", _docType, _compCode, _year, _month, _day, _m, _runningNo);
            else
                ret = string.Join("", _docType, _compCode, _year, _month, _day, _runningNo);

            return ret;
        }
    }
}
