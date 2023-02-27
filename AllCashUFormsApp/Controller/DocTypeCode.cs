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
        CultureInfo cultures = System.Globalization.CultureInfo.GetCultureInfo("th-TH");

        public string GenManualDocNo(string docTypeCode, string whCode = null, DateTime? docdate = null)
        {
            try
            {
                string ret = string.Empty;
                //edit by sailom .k 13/12/2021
                var tbl_DocumentType = new tbl_DocumentType().Select(docTypeCode.Trim()); //(new tbl_DocumentType()).SelectAll().FirstOrDefault(x => x.DocTypeCode.Trim() == docTypeCode.Trim());
                if (tbl_DocumentType != null) //PO
                {
                    int runLength = tbl_DocumentType.RunLength.Value;
                    string mode = "";

                    var endDayMode = new List<string> { "H", "C", "M" }.ToList();
                    if (string.IsNullOrEmpty(mode) && endDayMode.Contains(docTypeCode))
                    {
                       
                          ret = GenDocNo(docTypeCode, tbl_DocumentType, "0", runLength, whCode);
                    }
                    else
                    {
                        var tbl_POMasters = new List<tbl_POMaster>();

                        //Func<tbl_POMaster, bool> tbl_POMasterPre = null;
                        if (new List<string> { "OD", "RE", "IV", "IM", "RT" }.Contains(docTypeCode))
                        {
                            if (docTypeCode == "IM" || docTypeCode == "IV")
                            {
                                if (docTypeCode == "IM")
                                    tbl_POMasters = new tbl_POMaster().SelectRefMaxAutoID(docTypeCode.Trim());  //tbl_POMasterPre = (x => x.DocTypeCode.Trim() == "IV" && !string.IsNullOrEmpty(x.DocRef) && x.DocRef.Trim() == docTypeCode); //Last edit by sailom .k 21/10/2021
                                else
                                    tbl_POMasters = new tbl_POMaster().SelectNewMaxAutoID(docTypeCode.Trim(), whCode, docdate.Value); //for support max docno from tablet sales transaction by sailom .k 21/11/2022
                            }
                            else
                            {
                                tbl_POMasters = new tbl_POMaster().SelectMaxAutoID(docTypeCode.Trim());  //tbl_POMasterPre = (x => x.DocTypeCode.Trim() == docTypeCode.Trim());//Last edit by sailom .k 21/10/2021
                            }

                            //var tbl_POMasters = (new tbl_POMaster()).Select(tbl_POMasterPre, (docTypeCode == "IM" ? "IV" : docTypeCode.Trim()));
                            if (tbl_POMasters != null && tbl_POMasters.Count > 0)
                            {
                                mode = "PO";
                                var maxAutoID = tbl_POMasters.Max(x => x.AutoID);
                                tbl_POMaster tbl_POMaster = tbl_POMasters.FirstOrDefault(x => x.AutoID == maxAutoID);

                                ret = CheckManualDocNo(docTypeCode, tbl_DocumentType, runLength, whCode, tbl_POMaster.DocNo, docdate);
                            }
                        }


                        if (string.IsNullOrEmpty(mode))
                        {
                            if (!string.IsNullOrEmpty(whCode))
                            {
                                ret = CheckManualDocNo(docTypeCode, tbl_DocumentType, runLength, whCode, string.Empty, docdate);
                            }
                            else
                                ret = CheckManualDocNo(docTypeCode, tbl_DocumentType, runLength, string.Empty, string.Empty, docdate);
                        }
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return string.Empty;
            }
        }


        public string GenDocNo(string docTypeCode, string whCode = null, DateTime? docdate = null)
        {
            try
            {
                string ret = string.Empty;
                //edit by sailom .k 13/12/2021
                var tbl_DocumentType = new tbl_DocumentType().Select(docTypeCode.Trim()); //(new tbl_DocumentType()).SelectAll().FirstOrDefault(x => x.DocTypeCode.Trim() == docTypeCode.Trim());
                if (tbl_DocumentType != null) //PO
                {
                    int runLength = tbl_DocumentType.RunLength.Value;
                    string mode = "";

                    var endDayMode = new List<string> { "H", "C", "M" }.ToList();
                    if (string.IsNullOrEmpty(mode) && endDayMode.Contains(docTypeCode))
                    {
                        if (docTypeCode == "H")
                        {
                            //var tbl_IVMasters = new tbl_IVMaster().Select(x => x.DocTypeCode.Trim() == docTypeCode.Trim());
                            var tbl_IVMasters = new tbl_IVMaster().SelectMaxAutoID(docTypeCode.Trim());
                            if (tbl_IVMasters != null && tbl_IVMasters.Count > 0)
                            {
                                var maxAutoID = tbl_IVMasters.Max(x => x.AutoID);
                                var tbl_IVMaster = tbl_IVMasters.FirstOrDefault(x => x.AutoID == maxAutoID);
                                ret = CheckDocNo(docTypeCode, tbl_DocumentType, runLength, whCode, tbl_IVMaster.DocNo);
                            }
                            else
                                ret = GenDocNo(docTypeCode, tbl_DocumentType, "0", runLength, whCode);
                        }
                        else if (docTypeCode == "C" || docTypeCode == "M")
                        {
                            string _docTypeCode = docTypeCode == "C" ? "RL" : "RB";

                            //var tbl_PRMasters = new tbl_PRMaster().Select(x => x.DocTypeCode.Trim() == _docTypeCode.Trim()
                            //&& !string.IsNullOrEmpty(x.DocRef) && x.DocRef.Contains(docTypeCode), _docTypeCode.Trim());

                            var tbl_PRMasters = new tbl_PRMaster().SelectRefMaxAutoID(_docTypeCode.Trim());
                            if (tbl_PRMasters != null && tbl_PRMasters.Count > 0)
                            {
                                var maxAutoID = tbl_PRMasters.Max(x => x.AutoID);
                                var tbl_IVMaster = tbl_PRMasters.FirstOrDefault(x => x.AutoID == maxAutoID);
                                ret = CheckDocNo(docTypeCode, tbl_DocumentType, runLength, whCode, tbl_IVMaster.DocRef);
                            }
                            else
                                ret = GenDocNo(docTypeCode, tbl_DocumentType, "0", runLength, whCode);
                        }
                        else
                            ret = GenDocNo(docTypeCode, tbl_DocumentType, "0", runLength, whCode);
                    }
                    else
                    {
                        var tbl_POMasters = new List<tbl_POMaster>();

                        //Func<tbl_POMaster, bool> tbl_POMasterPre = null;
                        if (new List<string> { "OD", "RE", "IV", "IM", "RT" }.Contains(docTypeCode))
                        {
                            if (docTypeCode == "IM" || docTypeCode == "IV")
                            {
                                if (docTypeCode == "IM")
                                    tbl_POMasters = new tbl_POMaster().SelectRefMaxAutoID(docTypeCode.Trim());  //tbl_POMasterPre = (x => x.DocTypeCode.Trim() == "IV" && !string.IsNullOrEmpty(x.DocRef) && x.DocRef.Trim() == docTypeCode); //Last edit by sailom .k 21/10/2021
                                else
                                    tbl_POMasters = new tbl_POMaster().SelectNewMaxAutoID(docTypeCode.Trim(), whCode, docdate.Value); //for support max docno from tablet sales transaction by sailom .k 21/11/2022
                            }
                            else
                            {
                                tbl_POMasters = new tbl_POMaster().SelectMaxAutoID(docTypeCode.Trim());  //tbl_POMasterPre = (x => x.DocTypeCode.Trim() == docTypeCode.Trim());//Last edit by sailom .k 21/10/2021
                            }
                           
                            //var tbl_POMasters = (new tbl_POMaster()).Select(tbl_POMasterPre, (docTypeCode == "IM" ? "IV" : docTypeCode.Trim()));
                            if (tbl_POMasters != null && tbl_POMasters.Count > 0)
                            {
                                mode = "PO";
                                var maxAutoID = tbl_POMasters.Max(x => x.AutoID);
                                tbl_POMaster tbl_POMaster = tbl_POMasters.FirstOrDefault(x => x.AutoID == maxAutoID);

                                ret = CheckDocNo(docTypeCode, tbl_DocumentType, runLength, whCode, tbl_POMaster.DocNo);
                            }
                        }

                        if (string.IsNullOrEmpty(mode)) //(tbl_POMasters.Count == 0) //PR
                        {
                            var tbl_PRMasters = new List<tbl_PRMaster>();

                            if (new List<string> { "RB", "RL", "ST", "TR", "RJ" }.Contains(docTypeCode))
                            {
                                if (docTypeCode.Trim() == "RL")
                                {
                                    //tbl_PRMasters = (new tbl_PRMaster()).Select(x => !x.DocNo.Contains("V") && x.DocTypeCode.Trim() == docTypeCode.Trim(), docTypeCode.Trim());
                                    tbl_PRMasters = (new tbl_PRMaster()).SelectVMaxAutoID(docTypeCode.Trim());
                                }
                                else
                                {
                                    tbl_PRMasters = (new tbl_PRMaster()).SelectMaxAutoID(docTypeCode.Trim());
                                    //tbl_PRMasters = (new tbl_PRMaster()).SelectMaxAutoID(x => x.DocTypeCode.Trim() == docTypeCode.Trim(), docTypeCode.Trim());
                                }

                                if (tbl_PRMasters != null && tbl_PRMasters.Count > 0)
                                {
                                    mode = "PR";
                                    var maxAutoID = tbl_PRMasters.Max(x => x.AutoID);
                                    var tbl_PRMaster = tbl_PRMasters.FirstOrDefault(x => x.AutoID == maxAutoID);

                                    ret = CheckDocNo(docTypeCode, tbl_DocumentType, runLength, whCode, tbl_PRMaster.DocNo, docdate); //edit by sailom .k 30/11/2022
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(mode))
                        {
                            //Func<tbl_IVMaster, bool> tbl_IVMasterPre = (x => x.DocTypeCode.Trim() == docTypeCode.Trim());
                            //var tbl_IVMasters = (new tbl_IVMaster()).Select(tbl_IVMasterPre);
                            var tbl_IVMasters = (new tbl_IVMaster()).SelectMaxAutoID(docTypeCode.Trim());
                            if (tbl_IVMasters != null && tbl_IVMasters.Count > 0)
                            {
                                mode = "IV";
                                //var maxAutoID = tbl_IVMasters.Where(x => x.DocDate.Day == 23 && x.DocDate.Month == 8 && x.DocDate.Year == 2021 && x.DocNo != "V9644802").Max(x => x.AutoID);/////////////////////////////////////////////
                                var maxAutoID = tbl_IVMasters.Max(x => x.AutoID);
                                var tbl_IVMaster = tbl_IVMasters.FirstOrDefault(x => x.AutoID == maxAutoID);

                                ret = CheckDocNo(docTypeCode, tbl_DocumentType, runLength, whCode, tbl_IVMaster.DocNo);
                                //if (ret == "V9644802")
                                //{
                                //    ret = "V9644803";
                                //}
                            }
                        }

                        if (string.IsNullOrEmpty(mode))
                        {
                            if (!string.IsNullOrEmpty(whCode))
                            {
                                ret = GenDocNo(docTypeCode, tbl_DocumentType, "0", runLength, whCode);
                            }
                            else
                                ret = GenDocNo(docTypeCode, tbl_DocumentType, "0", runLength);
                        }
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return string.Empty;
            }
        }

        private string CheckManualDocNo(string docTypeCode, tbl_DocumentType tbl_DocumentType, int runLength, string whCode, string docNo, DateTime? docdate = null)
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

            ret = GenManualDocNoRunNo(docTypeCode, tbl_DocumentType, docdate.Value, runLength, whCode, docNo);

            return ret;
        }

        private string CheckDocNo(string docTypeCode, tbl_DocumentType tbl_DocumentType, int runLength, string whCode, string docNo, DateTime? docdate = null)
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

            tempMonth = DateTime.Now.ToString(dateFormate, cultures);
            //tempMonth = Convert.ToDateTime(DateTime.Now, cultures).ToString(dateFormate);

            if (docNo.Substring(startLen, dateFormate.Length) == tempMonth)
            {
                if (docTypeCode == "V" && docNo.Length > tbl_DocumentType.DocFormat.Length) //over current Length by sailom.k 25/11/2022
                {
                    runLength = runLength + (docNo.Length - tbl_DocumentType.DocFormat.Length);
                }

                _rNoTemp = docNo.Substring(docNo.Length - runLength, runLength);
                ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength, whCode);
            }
            else if (docNo.Substring(startLen, dateFormate.Length) != tempMonth && //for end year to new year by sailom.k 04/01/2023-------------------
                tbl_DocumentType.DocFormat.Contains("YY") && 
                docTypeCode == "V")
            {
                if (Convert.ToInt32(tempMonth) > Convert.ToInt32(docNo.Substring(startLen, dateFormate.Length)))
                {
                    if (docTypeCode == "V" && docNo.Length > tbl_DocumentType.DocFormat.Length) //over current Length by sailom.k 25/11/2022
                    {
                        runLength = runLength + (docNo.Length - tbl_DocumentType.DocFormat.Length);
                    }

                    string tmpInitNo = "";
                    for (int i = 0; i < runLength; i++)
                    {
                        tmpInitNo += "0";
                    }
                    _rNoTemp = tmpInitNo;
                    ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength, whCode);
                }
            }//for end year to new year by sailom.k 04/01/2023-------------------------------------------------------------------------------------------
            else
            {
                var _tbl_PRMasters = new List<tbl_PRMaster>();
                if (docTypeCode.Trim() == "RL")
                {
                    //_tbl_PRMasters = (new tbl_PRMaster()).Select(x => x.DocTypeCode.Trim() == docTypeCode.Trim() && !x.DocNo.Contains("V") && x.DocNo.Contains(tempMonth), docTypeCode.Trim());
                    _tbl_PRMasters = (new tbl_PRMaster()).SelectVMaxAutoID(docTypeCode.Trim()).Where(x => x.DocNo.Contains(tempMonth)).ToList(); //edit by sailom .k 13/12/2021

                    if (_tbl_PRMasters.Count == 0)
                    {
                        _tbl_PRMasters = (new tbl_PRMaster()).SelectNewVMaxAutoID(docTypeCode.Trim(), docdate.Value).Where(x => x.DocNo.Contains(tempMonth)).ToList(); //edit by sailom .k 30/11/2022
                    }
                }
                else
                {
                    //_tbl_PRMasters = (new tbl_PRMaster()).Select(x => x.DocTypeCode.Trim() == docTypeCode.Trim() && x.DocNo.Contains(tempMonth), docTypeCode.Trim());
                    _tbl_PRMasters = (new tbl_PRMaster()).SelectMaxAutoID(docTypeCode.Trim()).Where(x => x.DocNo.Contains(tempMonth)).ToList(); //edit by sailom .k 13/12/2021
                }

                if (_tbl_PRMasters != null && _tbl_PRMasters.Count > 0)
                {
                    int autoID = _tbl_PRMasters.Max(a => a.AutoID);
                    var tbl_PRMaster = _tbl_PRMasters.FirstOrDefault(x => x.AutoID == autoID);
                    if (tbl_PRMaster != null)
                    {
                        string tmpRunning = tbl_PRMaster.DocNo.Substring(9, tbl_PRMaster.DocNo.Length - 9);
                        _rNoTemp = tmpRunning;
                    }
                }

                if (docTypeCode == "IM" || docTypeCode == "V")  //if (docTypeCode == "IV" || docTypeCode == "IM" || docTypeCode == "V") //for support running docno from tablet request by admin lri  last edit by sailom  02/11/2021
                    ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength, whCode);
                else if (docTypeCode == "H" || docTypeCode == "C" || docTypeCode == "M")
                    ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength, whCode);
                //else if (docTypeCode == "RL")
                //    ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength, whCode);
                else if (docTypeCode == "IV") //for support running docno from tablet request by admin lri  last edit by sailom  02/11/2021
                {
                    if (!string.IsNullOrEmpty(docNo))
                    {
                        Int64 temp = 0;
                        if (Int64.TryParse(docNo, out temp))
                        {
                            ret = (temp + 1).ToString(); //for support pre-order by sailom.k 05/04/2022
                        }
                        else
                        {
                            ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength);
                        }
                    }
                }
                else
                    ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength);
            }

            return ret;
        }

        private string CheckDocNoPRE(string docTypeCode, tbl_DocumentType tbl_DocumentType, int runLength, string whCode, string docNo)
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

            tempMonth = DateTime.Now.ToString(dateFormate, cultures);
            //tempMonth = Convert.ToDateTime(DateTime.Now, cultures).ToString(dateFormate);

            if (docNo.Substring(startLen, dateFormate.Length) == tempMonth)
            {
                _rNoTemp = docNo.Substring(docNo.Length - runLength, runLength);
                ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength, whCode);
            }
            else
            {
                var _tbl_PRMasters = new List<tbl_PRMaster>();
                if (docTypeCode.Trim() == "RL")
                {
                    //_tbl_PRMasters = (new tbl_PRMaster()).Select(x => x.DocTypeCode.Trim() == docTypeCode.Trim() && !x.DocNo.Contains("V") && x.DocNo.Contains(tempMonth), docTypeCode.Trim());
                    _tbl_PRMasters = (new tbl_PRMaster()).SelectVMaxAutoID(docTypeCode.Trim()).Where(x => x.DocNo.Contains(tempMonth)).ToList(); //edit by sailom .k 13/12/2021
                }
                else
                {
                    //_tbl_PRMasters = (new tbl_PRMaster()).Select(x => x.DocTypeCode.Trim() == docTypeCode.Trim() && x.DocNo.Contains(tempMonth), docTypeCode.Trim());
                    _tbl_PRMasters = (new tbl_PRMaster()).SelectMaxAutoID(docTypeCode.Trim()).Where(x => x.DocNo.Contains(tempMonth)).ToList(); //edit by sailom .k 13/12/2021
                }

                if (_tbl_PRMasters != null && _tbl_PRMasters.Count > 0)
                {
                    int autoID = _tbl_PRMasters.Max(a => a.AutoID);
                    var tbl_PRMaster = _tbl_PRMasters.FirstOrDefault(x => x.AutoID == autoID);
                    if (tbl_PRMaster != null)
                    {
                        string tmpRunning = tbl_PRMaster.DocNo.Substring(9, tbl_PRMaster.DocNo.Length - 9);
                        _rNoTemp = tmpRunning;
                    }
                }

                if (docTypeCode == "IM" || docTypeCode == "V")  //if (docTypeCode == "IV" || docTypeCode == "IM" || docTypeCode == "V") //for support running docno from tablet request by admin lri  last edit by sailom  02/11/2021
                    ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength, whCode);
                else if (docTypeCode == "H" || docTypeCode == "C" || docTypeCode == "M")
                    ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength, whCode);
                //else if (docTypeCode == "RL")
                //    ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength, whCode);
                else if (docTypeCode == "IV") //for support running docno from tablet request by admin lri  last edit by sailom  02/11/2021
                {
                    if (!string.IsNullOrEmpty(docNo))
                    {
                        Int64 temp = 0;
                        if (Int64.TryParse(docNo, out temp))
                        {
                            var tmp = new tbl_POMaster().SelectMaxAutoIDPRE_ReGen(docNo);
                            ret = (Convert.ToInt64(tmp) + 1).ToString();
                        }
                        else
                        {
                            ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength);
                        }
                    }
                }
                else
                    ret = GenDocNo(docTypeCode, tbl_DocumentType, _rNoTemp, runLength);
            }

            return ret;
        }

        private string GenManualDocNoRunNo(string docTypeCode, tbl_DocumentType tbl_DocumentType, DateTime docdate, int runLength, string whCode, string docNo)
        {
            string ret = "";
            string docFormat = tbl_DocumentType.DocFormat;
            var allBranch = new BaseControl("").tbl_Branchs; //edit by sailom .k 13/12/2021

            List<char> docFormatArr = new List<char>();
            docFormatArr = docFormat.ToCharArray().ToList();

            string compCode = new BaseControl("").tbl_Companies.FirstOrDefault().CompanyCode; //edit by sailom .k 13/12/2021//(new tbl_Company()).SelectAll().FirstOrDefault().CompanyCode;

            DateTime cDate = docdate;
            int _rNo = 0;
            if (!string.IsNullOrEmpty(docNo))
            {
                int maxManualDocNo = docNo.Length == 13 ? Convert.ToInt32(docNo.Substring(10, 3)) : Convert.ToInt32(docNo.Substring(11, 3));
                _rNo = maxManualDocNo < 900 ? 900 : (maxManualDocNo + 1);
            }
            else
            {
                _rNo = 900;
            }

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
                    if (i + 1 < docFormatArr.Count)
                    {
                        var dFormat = docFormatArr[i].ToString() + docFormatArr[i + 1].ToString();
                        if (dFormat == "DD")
                            _day = cDate.ToString("dd", cultures);
                    }
                }
                if (docFormatArr[i] == '0')
                {
                    _runningNo = tempRunningNo;
                }
            }

            if (docTypeCode == "IM")
                ret = string.Join("", _docType, _compCode, _year, _month, _day, _m, _runningNo);
            else if (docTypeCode == "H")
                ret = string.Join("", _docType, _compCode, _month, _year, _day, _runningNo);
            else
            {
                //if (docTypeCode == "RL")
                //    ret = string.Join("", _docType, _compCode, _year, _month, _day);
                //else
                ret = string.Join("", _docType, _compCode, _year, _month, _day, _runningNo);
            }


            return ret;
        }

        private string GenDocNo(string docTypeCode, tbl_DocumentType tbl_DocumentType, string _rNoTemp, int runLength, string whCode = null)
        {
            string ret = "";
            string docFormat = tbl_DocumentType.DocFormat;
            var allBranch = new BaseControl("").tbl_Branchs; //edit by sailom .k 13/12/2021

            List<char> docFormatArr = new List<char>();
            docFormatArr = docFormat.ToCharArray().ToList();

            string compCode = new BaseControl("").tbl_Companies.FirstOrDefault().CompanyCode; //edit by sailom .k 13/12/2021//(new tbl_Company()).SelectAll().FirstOrDefault().CompanyCode;

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
                        if (!string.IsNullOrEmpty(whCode))
                        {
                            Func<tbl_Branch, bool> whFunc = (x => x.BranchCode == whCode.Split('V')[0]);
                            var wh = allBranch.Where(whFunc).ToList();//(new IV()).GetBranch(whFunc);
                            if (wh != null && wh.Count > 0)
                                _compCode = wh[0].AgentID;
                        }
                    }
                    //else if(docTypeCode == "RL")
                    //{
                    //    if (whCode != null && !string.IsNullOrEmpty(whCode))
                    //        _compCode = whCode;
                    //}
                    else if (docTypeCode == "H" ||
                        docTypeCode == "C" ||
                        docTypeCode == "M")
                    {
                        if (!string.IsNullOrEmpty(whCode))
                        {
                            string whID = string.Join("", whCode.ToCharArray().Take(3));
                            Func<tbl_Branch, bool> whFunc = (x => x.BranchCode == whID);
                            var wh = allBranch.Where(whFunc).ToList();// (new IV()).GetBranch(whFunc);
                            if (wh != null && wh.Count > 0)
                                _compCode = wh[0].AgentID;
                        }
                    }
                    else
                        _compCode = compCode;

                    if (string.IsNullOrEmpty(_compCode))
                    {
                        _compCode = compCode;
                    }
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
                    if (i + 1 < docFormatArr.Count)
                    {
                        var dFormat = docFormatArr[i].ToString() + docFormatArr[i + 1].ToString();
                        if (dFormat == "DD")
                            _day = cDate.ToString("dd", cultures);
                    }
                }
                if (docFormatArr[i] == '0')
                {
                    _runningNo = tempRunningNo;
                }
            }

            if (docTypeCode == "IM")
                ret = string.Join("", _docType, _compCode, _year, _month, _day, _m, _runningNo);
            else if (docTypeCode == "H")
                ret = string.Join("", _docType, _compCode, _month, _year, _day, _runningNo);
            else
            {
                //if (docTypeCode == "RL")
                //    ret = string.Join("", _docType, _compCode, _year, _month, _day);
                //else
                ret = string.Join("", _docType, _compCode, _year, _month, _day, _runningNo);
            }


            return ret;
        }

        public string GenDocNoPre(string docTypeCode, string whCode = null)
        {
            try
            {
                string ret = string.Empty;
                //edit by sailom .k 13/12/2021
                var tbl_DocumentType = new tbl_DocumentType().Select(docTypeCode.Trim()); //(new tbl_DocumentType()).SelectAll().FirstOrDefault(x => x.DocTypeCode.Trim() == docTypeCode.Trim());
                if (tbl_DocumentType != null) //PO
                {
                    int runLength = tbl_DocumentType.RunLength.Value;

                    var tbl_POMasters = new List<tbl_POMaster_PRE>();

                    //Func<tbl_POMaster, bool> tbl_POMasterPre = null;
                    if (new List<string> { "OD", "RE", "IV", "IM", "RT" }.Contains(docTypeCode))
                    {
                        if (docTypeCode != "IM")
                            tbl_POMasters = new tbl_POMaster_PRE().SelectMaxAutoID(whCode, docTypeCode.Trim());  //tbl_POMasterPre = (x => x.DocTypeCode.Trim() == docTypeCode.Trim());//Last edit by sailom .k 21/10/2021
                        else if (docTypeCode == "IM")
                            tbl_POMasters = new tbl_POMaster_PRE().SelectRefMaxAutoID(whCode, docTypeCode.Trim());  //tbl_POMasterPre = (x => x.DocTypeCode.Trim() == "IV" && !string.IsNullOrEmpty(x.DocRef) && x.DocRef.Trim() == docTypeCode); //Last edit by sailom .k 21/10/2021

                        //var tbl_POMasters = (new tbl_POMaster()).Select(tbl_POMasterPre, (docTypeCode == "IM" ? "IV" : docTypeCode.Trim()));
                        if (tbl_POMasters != null && tbl_POMasters.Count > 0)
                        {
                            var maxAutoID = tbl_POMasters.Max(x => x.AutoID);
                            tbl_POMaster_PRE tbl_POMaster = tbl_POMasters.FirstOrDefault(x => x.AutoID == maxAutoID);

                            ret = CheckDocNoPRE(docTypeCode, tbl_DocumentType, runLength, whCode, tbl_POMaster.DocNo);
                        }
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return string.Empty;
            }
        }

        public string GenNewDocNoPre(string docTypeCode, string whCode, DateTime docdate)
        {
            try
            {
                string ret = string.Empty;
                //edit by sailom .k 13/12/2021
                var tbl_DocumentType = new tbl_DocumentType().Select(docTypeCode.Trim()); //(new tbl_DocumentType()).SelectAll().FirstOrDefault(x => x.DocTypeCode.Trim() == docTypeCode.Trim());
                if (tbl_DocumentType != null) //PO
                {
                    int runLength = tbl_DocumentType.RunLength.Value;

                    var tbl_POMasters = new List<tbl_POMaster_PRE>();

                    //Func<tbl_POMaster, bool> tbl_POMasterPre = null;
                    if (new List<string> { "OD", "RE", "IV", "IM", "RT" }.Contains(docTypeCode))
                    {
                        if (docTypeCode != "IM")
                            tbl_POMasters = new tbl_POMaster_PRE().SelectNewMaxAutoID(docTypeCode.Trim(), whCode, docdate);  //tbl_POMasterPre = (x => x.DocTypeCode.Trim() == docTypeCode.Trim());//Last edit by sailom .k 21/10/2021
                        else if (docTypeCode == "IM")
                            tbl_POMasters = new tbl_POMaster_PRE().SelectRefMaxAutoID(whCode, docTypeCode.Trim());  //tbl_POMasterPre = (x => x.DocTypeCode.Trim() == "IV" && !string.IsNullOrEmpty(x.DocRef) && x.DocRef.Trim() == docTypeCode); //Last edit by sailom .k 21/10/2021

                        //var tbl_POMasters = (new tbl_POMaster()).Select(tbl_POMasterPre, (docTypeCode == "IM" ? "IV" : docTypeCode.Trim()));
                        if (tbl_POMasters != null && tbl_POMasters.Count > 0)
                        {
                            var maxAutoID = tbl_POMasters.Max(x => x.AutoID);
                            tbl_POMaster_PRE tbl_POMaster = tbl_POMasters.FirstOrDefault(x => x.AutoID == maxAutoID);

                            ret = CheckDocNoPRE(docTypeCode, tbl_DocumentType, runLength, whCode, tbl_POMaster.DocNo);
                        }
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return string.Empty;
            }
        }

        public string GenDocNoPreEX(string docTypeCode, string whCode = null)
        {
            try
            {
                string ret = string.Empty;
                //edit by sailom .k 13/12/2021
                var tbl_DocumentType = new tbl_DocumentType().Select(docTypeCode.Trim()); //(new tbl_DocumentType()).SelectAll().FirstOrDefault(x => x.DocTypeCode.Trim() == docTypeCode.Trim());
                if (tbl_DocumentType != null) //PO
                {
                    int runLength = tbl_DocumentType.RunLength.Value;

                    var tbl_POMasters = new List<tbl_POMaster_PRE>();

                    Dictionary<int, string> docList = new Dictionary<int, string>();
                    //Func<tbl_POMaster, bool> tbl_POMasterPre = null;
                    if (new List<string> { "OD", "RE", "IV", "IM", "RT" }.Contains(docTypeCode))
                    {
                        if (docTypeCode != "IM")
                        {
                            tbl_POMasters = new tbl_POMaster_PRE().SelectMaxAutoID(whCode, docTypeCode.Trim());  //tbl_POMasterPre = (x => x.DocTypeCode.Trim() == docTypeCode.Trim());//Last edit by sailom .k 21/10/2021
                            foreach (var item in tbl_POMasters)
                            {
                                docList.Add(item.AutoID, item.DocNo);
                            }
                            
                            var tmp = new tbl_POMaster().SelectMaxAutoIDPRE(whCode, docTypeCode.Trim());
                            foreach (var item2 in tmp)
                            {
                                docList.Add(item2.AutoID, item2.DocNo);
                            }
                        }
                        else if (docTypeCode == "IM")
                        {
                            tbl_POMasters = new tbl_POMaster_PRE().SelectRefMaxAutoID(whCode, docTypeCode.Trim());  //tbl_POMasterPre = (x => x.DocTypeCode.Trim() == "IV" && !string.IsNullOrEmpty(x.DocRef) && x.DocRef.Trim() == docTypeCode); //Last edit by sailom .k 21/10/2021
                            foreach (var item in tbl_POMasters)
                            {
                                docList.Add(item.AutoID, item.DocNo);
                            }

                            var tmp = new tbl_POMaster().SelectRefMaxAutoIDPRE(whCode, docTypeCode.Trim());
                            foreach (var item2 in tmp)
                            {
                                docList.Add(item2.AutoID, item2.DocNo);
                            }
                        }

                        //var tbl_POMasters = (new tbl_POMaster()).Select(tbl_POMasterPre, (docTypeCode == "IM" ? "IV" : docTypeCode.Trim()));
                        if (docList != null && docList.Count > 0)
                        {
                            var maxAutoID = docList.Max(x => x.Key);
                            var tmpMaxItem = docList.FirstOrDefault(x => x.Key == maxAutoID);

                            ret = CheckDocNoPRE(docTypeCode, tbl_DocumentType, runLength, whCode, tmpMaxItem.Value);
                        }
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return string.Empty;
            }
        }

        public string GenNewDocNoPreEX(string docTypeCode, string whCode, DateTime docdate)
        {
            try
            {
                string ret = string.Empty;
                //edit by sailom .k 13/12/2021
                var tbl_DocumentType = new tbl_DocumentType().Select(docTypeCode.Trim()); //(new tbl_DocumentType()).SelectAll().FirstOrDefault(x => x.DocTypeCode.Trim() == docTypeCode.Trim());
                if (tbl_DocumentType != null) //PO
                {
                    int runLength = tbl_DocumentType.RunLength.Value;

                    var tbl_POMasters = new List<tbl_POMaster_PRE>();

                    Dictionary<int, string> docList = new Dictionary<int, string>();
                    //Func<tbl_POMaster, bool> tbl_POMasterPre = null;
                    if (new List<string> { "OD", "RE", "IV", "IM", "RT" }.Contains(docTypeCode))
                    {
                        if (docTypeCode != "IM")
                        {
                            tbl_POMasters = new tbl_POMaster_PRE().SelectNewMaxAutoID(docTypeCode.Trim(), whCode, docdate);  //tbl_POMasterPre = (x => x.DocTypeCode.Trim() == docTypeCode.Trim());//Last edit by sailom .k 21/10/2021
                            foreach (var item in tbl_POMasters)
                            {
                                docList.Add(item.AutoID, item.DocNo);
                            }

                            var tmp = new tbl_POMaster().SelectNewMaxAutoID(whCode, docTypeCode.Trim(), docdate);
                            foreach (var item2 in tmp)
                            {
                                docList.Add(item2.AutoID, item2.DocNo);
                            }
                        }
                        else if (docTypeCode == "IM")
                        {
                            tbl_POMasters = new tbl_POMaster_PRE().SelectRefMaxAutoID(whCode, docTypeCode.Trim());  //tbl_POMasterPre = (x => x.DocTypeCode.Trim() == "IV" && !string.IsNullOrEmpty(x.DocRef) && x.DocRef.Trim() == docTypeCode); //Last edit by sailom .k 21/10/2021
                            foreach (var item in tbl_POMasters)
                            {
                                docList.Add(item.AutoID, item.DocNo);
                            }

                            var tmp = new tbl_POMaster().SelectRefMaxAutoIDPRE(whCode, docTypeCode.Trim());
                            foreach (var item2 in tmp)
                            {
                                docList.Add(item2.AutoID, item2.DocNo);
                            }
                        }

                        //var tbl_POMasters = (new tbl_POMaster()).Select(tbl_POMasterPre, (docTypeCode == "IM" ? "IV" : docTypeCode.Trim()));
                        if (docList != null && docList.Count > 0)
                        {
                            var maxAutoID = docList.Max(x => x.Key);
                            var tmpMaxItem = docList.FirstOrDefault(x => x.Key == maxAutoID);

                            ret = CheckDocNoPRE(docTypeCode, tbl_DocumentType, runLength, whCode, tmpMaxItem.Value);
                        }
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return string.Empty;
            }
        }
    }
}
