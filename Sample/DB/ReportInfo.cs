using CIT.Shared.DataHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.BLL
{
   public class ReportInfo
    {
        public static List<HRCMS_Report> getReportList(Boolean isHRAccess, Boolean isMgrAccess)
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {
                var _reportList = new List<HRCMS_Report>();

                if (isHRAccess == true)
                    _reportList = context.HRCMS_Report.Where(m => m.HR_Access == isHRAccess && m.Report_Status == true).ToList();

                if (isMgrAccess == true)
                    _reportList = context.HRCMS_Report.Where(m => m.Mgr_Access == isMgrAccess && m.Report_Status == true).ToList();

                if(!isHRAccess && !isMgrAccess)
                    _reportList = context.HRCMS_Report.Where(m=>m.Report_Status == true).ToList();

                return _reportList;
            }
        }

        public static HRCMS_Report getReportDetails(int ReportID)
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {
                var _report = context.HRCMS_Report.Where(m => m.Report_ID == ReportID ).FirstOrDefault();
                return _report;
            }
        }



        public static Report_SavedCriteria getReportSavedCriteria(int ReportID, string savedCriteriaType)
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {
                var _reportSavedCriteria = context.Report_SavedCriteria.Where(m => m.Report_ID == ReportID && m.SavedCriteria_Type == savedCriteriaType).FirstOrDefault();

                if (_reportSavedCriteria == null)
                {
                    _reportSavedCriteria = new Report_SavedCriteria();
                    return _reportSavedCriteria;
                }

                var _UpdatedByUser = context.HRCMS_USER.Where(m => m.EMPLID == _reportSavedCriteria.Updated_By).FirstOrDefault();

                if (_UpdatedByUser != null)
                {                    
                    _reportSavedCriteria.UpdatedByUserName = _UpdatedByUser.USERNAME;
                }

                return _reportSavedCriteria;
            }
        }

        public static void saveReportCriteria(Report_SavedCriteria RptSaveCriteria)
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {
                
                var _reportLst = context.Report_SavedCriteria.Where(m => m.Report_ID == RptSaveCriteria.Report_ID && m.SavedCriteria_Type == RptSaveCriteria.SavedCriteria_Type).ToList();

                if(RptSaveCriteria.SavedCriteria_Type == "Generate")
                {
                    _reportLst = _reportLst.Where(m => m.Updated_By == RptSaveCriteria.Updated_By).ToList();
                }

                var _report = _reportLst.FirstOrDefault();

                if (_report != null)
                {
                    RptSaveCriteria.Report_SavedCriteria_ID = _report.Report_SavedCriteria_ID;
                }

                context.Report_SavedCriteria.AddOrUpdate(RptSaveCriteria);
                
                context.SaveChanges();

            }
        }

        public static void updateReport(HRCMS_Report hrReport)
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {
                context.HRCMS_Report.AddOrUpdate(hrReport);
                context.SaveChanges();
            }
        }


        //public static HRCMS_Report updateReport(HRCMS_Report ReportInfo)
        //{
        //    using (HRCMSEntities context = new HRCMSEntities())
        //    {
        //        var _report = context.HRCMS_Report.Where(m => m.Report_ID == ReportInfo.Report_ID).FirstOrDefault();

        //        context.HRCMS_Report.AddOrUpdate(ReportInfo);

        //        context.SaveChanges();
        //    }

        //}

        public static DataTable GetReportData(int ReportID, string CriteriaType, string HrBPID,  string PlanningManagerID, string Updated_By)
        {
                var key = ConfigurationManager.AppSettings["EncKey"];


                string queryString = "GenerateReport";

                object[] param = new object[] { ReportID, CriteriaType, HrBPID, PlanningManagerID, Updated_By, key };
                DataSet ds = DALHelper.GetDataSet(queryString, CommandType.StoredProcedure, DataAccess.UsingTransaction.OptionNone, param);
                DataTable dtHRBPRpt = ds.Tables[0];

                return dtHRBPRpt;
        
        }


        public static List<Employee> GetSubEmployees(string PlanningManagerID)
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {
                HRCompInfo compInfo = new HRCompInfo();
                DataTable dtCurrYear = compInfo.getConfiguration("CurrentYear");
                var currYear = Convert.ToInt32(dtCurrYear.Rows[0]["Config_Val"].ToString());

                var _subEmployees = from COMP_REC_ENC in context.COMP_REC_ENC
                                    .Where(m => m.Planning_Manager_ID == PlanningManagerID && m.Year == currYear)
                                    select new Employee
                                    {
                                        EmplID=  COMP_REC_ENC.EMPLID
                                    };                

                return _subEmployees.Distinct().ToList();
            }

        } 


        public static DataTable RemoveColumnsByRoleAndReport(DataTable SrcDt, int ReportID, int currUsrAccessLevel)
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {
                var columnList = context.Report_Columns.Where(m => m.Report_ID == ReportID).ToList();

                foreach (Report_Columns Col in columnList)
                {
                    var dCol = SrcDt.Columns[Col.Column_Name];

                    if(Col.UserAccessLevel == 0 || Col.UserAccessLevel > currUsrAccessLevel)
                    {
                        SrcDt.Columns.Remove(dCol);
                    }

                }

            }
            return SrcDt;
        }
    }
}
