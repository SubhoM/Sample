using HR.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Migrations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.BLL
{
    public class SalaryRangeInfo
    {

        public static CashEquity GetCashEquitySplit(decimal Rule_ID, decimal New_Base_USD, decimal Total_Incentive)
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {
                var key = ConfigurationManager.AppSettings["EncKey"];

                var cashEquityObj = context.CalculateCashEquity(key, Rule_ID,New_Base_USD, Total_Incentive).FirstOrDefault();

                return cashEquityObj;
            }

        }

        public static void UploadFile(string fileName, string fileType)
        {

            
            DataLoad dataload;
            dataload = new DataLoad();

            DataTable dtLoads = dataload.ReadCsvFile(fileName);

            string tableName = string.Empty;

            switch (fileType)
            {
                case "SalaryRange":
                    tableName = "SalaryRange";
                    break;

                case "RangeUserAccess":
                    tableName = "RangeToolUserAccess";
                    break;
            }


            UpdateDB(fileName, dataload, dtLoads,tableName);

        }

    
        public static void UpdateDB(string fileName, DataLoad dataLoad, DataTable dtLoads, string TableName)
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {
                context.Database.ExecuteSqlCommand("Delete "+ TableName);


                dataLoad.BulkCopy(fileName, TableName, dtLoads);

                if (TableName == "SalaryRange") {

                    var _salaryRange = context.SalaryRanges.ToList();

                    _salaryRange.ForEach(m =>
                    {
                    //m.SalaryRange_ID = m.SalaryRange_ID;
                    m.Org_2 = m.Org_2 != null ? m.Org_2.Replace("^", ",") : null;
                        m.Org_3 = m.Org_3 != null ? m.Org_3.Replace("^", ",") : null;
                    });

                    context.SaveChanges();
                }
            }
        }

        public static DataTable GetRangeToolData(string fileType)
        {

            

            var dt = new DataTable();

            using (HRCMSEntities context = new HRCMSEntities())
            {
                if (fileType == "SalaryRange")
                {

                  var  _result = context.SalaryRanges.ToList();

                    dt = _result.ConvertToDataTable();

                } 

                if (fileType == "RangeUserAccess")
                {
                   var  _result = context.RangeToolUserAccesses.ToList();

                    dt = _result.ConvertToDataTable();
                }
               

            }
            
            return dt;
        }


        public static Boolean checkRangeToolAccess(string Emplid)
        {
            var _isAccessEnabled = false;

            using (HRCMSEntities context = new HRCMSEntities())
            {
               var _result = context.RangeToolUserAccesses.Where(m => m.EMPLID == Emplid).ToList();

                if(_result.Count()> 0)
                {
                    _isAccessEnabled = true;
                }
            }


            return _isAccessEnabled;
        }
    
        public static List<string> GetOrg1(Boolean isCompAdmin, string EmplID)
        {
            var _result = new List<string>();

            var _AccessOrg1List = new List<string>();

            using (HRCMSEntities context = new HRCMSEntities())
            {
               if (isCompAdmin == false)
                {
                    _result = context.RangeToolUserAccesses.Where(m=>m.EMPLID == EmplID).Select(m => m.Org_1).Distinct().ToList();

                    return _result;
                }

                _result = context.SalaryRanges.Select(m => m.Org_1).Distinct().ToList();

            }

            return _result;

        }


        public static List<string> GetOrg2(string Org1, Boolean isCompAdmin, string EmplID)
        {
            var _result = new List<string>();

            var _usrOrg2Access = new List<string>();

            using (HRCMSEntities context = new HRCMSEntities())
            {
                _result = context.SalaryRanges.Where(m=>m.Org_1 == Org1).Select(m => m.Org_2).Distinct().ToList();

                if (isCompAdmin == false)
                {
                    _usrOrg2Access = context.RangeToolUserAccesses.Where(m => m.EMPLID == EmplID && m.Org_1 == Org1).Select(m => m.Org_2).Distinct().ToList();

                    _result = _result.Where(item => _usrOrg2Access.Any(m => m.Equals(item))).ToList();
                }

            }

            return _result;

        }

        public static List<string> GetOrg3(string Org1, string Org2)
        {
            var _result = new List<string>();

            using (HRCMSEntities context = new HRCMSEntities())
            {
                _result = context.SalaryRanges.Where(m => m.Org_1 == Org1 && m.Org_2 == Org2).Select(m => m.Org_3).Distinct().ToList();
            }

            _result.Remove("");

            return _result;

        }

        public static List<string> GetJobFamily(string Org1, string Org2, string Org3, string Grade)
        {
            var _result = new List<string>();

            using (HRCMSEntities context = new HRCMSEntities())
            {
                _result = context.SalaryRanges.Where(m => m.Org_1 == Org1 && m.Org_2 == Org2 
                && m.Org_3 == ( Org3 == null? m.Org_3 : Org3)
                && m.Grade == (Grade == null ? m.Grade : Grade)
                )
                .Select(m => m.Job_Family).Distinct().ToList();
            }

            return _result;

        }

        public static List<string> GetJobSubFamily(string Org1, string Org2, string Org3,string Grade, string JobFamily)
        {
            var _result = new List<string>();

            using (HRCMSEntities context = new HRCMSEntities())
            {             
               
                _result = context.SalaryRanges.Where(m => m.Org_1 == Org1 && m.Org_2 == Org2 
                        && m.Org_3 ==  (Org3 == null ? m.Org_3 : Org3)
                        && m.Grade == (Grade == null ? m.Grade : Grade)
                        && m.Job_Family == (JobFamily == null? m.Job_Family : JobFamily) )
                        .Select(m => m.Job_SubFamily).Distinct().ToList();

                
            }

            return _result;

        }

        public static List<string> GetGrade(string Org1, string Org2, string Org3)
        {
            var _result = new List<string>();

            using (HRCMSEntities context = new HRCMSEntities())
            {
                _result = context.SalaryRanges.Where(m => m.Org_1 == Org1 && m.Org_2 == Org2 
                && m.Org_3 == ( Org3 == null ? m.Org_3 : Org3) ).Select(m => m.Grade).Distinct().ToList();
            }

            return _result;

        }

        public static List<string> GetLocation(string Org1, string Org2, string Org3, string Grade, string JobFamily, string JobSubFamily)
        {
            var _result = new List<string>();

            using (HRCMSEntities context = new HRCMSEntities())
            {
                _result = context.SalaryRanges.Where(m => m.Org_1 == Org1 && m.Org_2 == Org2
                && m.Org_3 == (Org3 == null ? m.Org_3 : Org3)
                && m.Grade == (Grade == null ? m.Grade : Grade)
                && m.Job_Family == (JobFamily == null ? m.Job_Family : JobFamily)
                && m.Job_SubFamily == (JobSubFamily == null ? m.Job_SubFamily : JobSubFamily)
                ).Select(m => m.Location).Distinct().ToList();
            }

            _result.Remove("");

            return _result;

        }

        public static List<string> GetTitle(string Org1, string Org2, string Org3, string Grade, string JobFamily, string JobSubFamily)
        {
            var _result = new List<string>();

            using (HRCMSEntities context = new HRCMSEntities())
            {
                _result = context.SalaryRanges.Where(m => m.Org_1 == Org1 && m.Org_2 == Org2 
                && m.Org_3 == (Org3 == null ? m.Org_3 : Org3)
                && m.Grade == (Grade == null ? m.Grade : Grade)
                && m.Job_Family == (JobFamily == null? m.Job_Family : JobFamily)
                && m.Job_SubFamily == (JobSubFamily == null ? m.Job_SubFamily : JobSubFamily)
                ).Select(m => m.Title).Distinct().ToList();
            }

            _result.Remove("");

            return _result;

        }


        public static SalaryRange GetSalaryRange(string Org1, string Org2, string Org3,string JobFamily, string JobSubFamily, string Grade, string Location, string Title)
        {
            var _result = new SalaryRange();

            using (HRCMSEntities context = new HRCMSEntities())
            {
                _result = context.SalaryRanges.Where(m => m.Org_1 == Org1 && m.Org_2 == Org2 
                && m.Org_3 == ( Org3 == null? m.Org_3 : Org3)
                && m.Job_Family == (JobFamily == null ? m.Job_Family : JobFamily)
                && m.Job_SubFamily== JobSubFamily 
                && m.Grade == Grade
                && m.Location == (Location == null ? m.Location : Location)
                && m.Title == (Title == null ? m.Title : Title)
                ).FirstOrDefault();
            }


            if (_result.TC_Min_Tier != "" || _result.TC_Midpoint_Tier != "" || _result.TC_Max_Tier != "")
            {
                _result.Salary_Range_Minimum = "*";
                _result.Salary_Range_Midpoint = "*";
                _result.Salary_Range_Maximum = "*";


                var _minCompMixInfo = _result.TC_Min_Tier!="" ? GetCompMixRange(_result.Applicable_Grid, _result.TC_Min_Tier) : null;
                var _midCompMixInfo = _result.TC_Midpoint_Tier != "" ? GetCompMixRange(_result.Applicable_Grid, _result.TC_Midpoint_Tier) : null;
                var _maxCompMixInfo = _result.TC_Max_Tier != "" ? GetCompMixRange(_result.Applicable_Grid, _result.TC_Max_Tier) : null;

                //_result.TC_Range_Minimum = _minCompMixInfo != null ? _minCompMixInfo.TCUSDMin.ToString() : "";
                //_result.TC_Range_Midpoint = _midCompMixInfo != null ? _midCompMixInfo.TCUSDMin.ToString() : "";
                //_result.TC_Range_Maximum = _maxCompMixInfo != null ? _maxCompMixInfo.TCUSDMin.ToString() : "";


                _result.Salary1Min = _minCompMixInfo != null ? _minCompMixInfo.MinSalary.ToString() : "";
                _result.Salary2Min = _minCompMixInfo != null ? (_minCompMixInfo.MinSalary + 25000).ToString() : "";

                _result.Salary1Mid = _midCompMixInfo != null ? _midCompMixInfo.MinSalary.ToString(): "";
                _result.Salary2Mid = _midCompMixInfo != null ? (_midCompMixInfo.MinSalary + 25000).ToString(): "";


                _result.Salary1Max = _maxCompMixInfo != null ? _maxCompMixInfo.MinSalary.ToString() : "";
                _result.Salary2Max = _maxCompMixInfo != null ? (_maxCompMixInfo.MinSalary + 25000).ToString() : "";

                _result.SalaryTierMin = _minCompMixInfo != null ? string.Format("{0:n0}", _minCompMixInfo.TCUSDMin) + " - " + string.Format("{0:n0}", _minCompMixInfo.TCUSDMax) : "";
                _result.SalaryTierMid = _midCompMixInfo != null ? string.Format("{0:n0}", _midCompMixInfo.TCUSDMin) + " - " + string.Format("{0:n0}", _midCompMixInfo.TCUSDMax) : "";
                _result.SalaryTierMax = _maxCompMixInfo != null ? string.Format("{0:n0}", _maxCompMixInfo.TCUSDMin) + " - " + string.Format("{0:n0}", _maxCompMixInfo.TCUSDMax) : "";

            }


            return _result;

        }

        public static CompMixTier GetCompMixRange(string ApplicableGrid, string Tier)
        {
            var _result = new CompMixTier();

            using (HRCMSEntities context = new HRCMSEntities())
            {
                _result = context.CompMixTiers.Where(m => m.ApplicableGrid == ApplicableGrid 
                && m.Tier == Tier).FirstOrDefault();
            }
                       
            return _result;
        }



    }
}
