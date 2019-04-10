using HR.BLL;
using HR.Entities;
using HR.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR.Reports
{
    public partial class GenerateReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
                var reportId = Convert.ToInt32(Request.QueryString["reportId"].ToString());
                var criteriaType = Request.QueryString["criteriaType"].ToString();
                var filename = Request.QueryString["fileName"].ToString();

                var dt = GetReportData(reportId, criteriaType);

                CreateExcelOutPut(dt, filename);
           
            
        }

        private DataTable GetReportData(Int32 reportId,string criteriaType)
        {
            try
            {

                var session = HttpContext.Current.Session;

                HRUser currentUser = session["currentUser"] as HRUser;

                string HrBPID = currentUser.IsCompTeam == 'Y' ? null : currentUser.IsHRGen == 'Y' ? currentUser.UserId : null;

                string PlanningManagerID = (currentUser.IsCompTeam == 'Y' || currentUser.IsHRGen == 'Y') ? null : currentUser.IsManager == 'Y' ? currentUser.UserId : null;

                var Updated_By = criteriaType == "Publish" ? null : currentUser.UserId;

                var dt = ReportInfo.GetReportData(reportId, criteriaType, HrBPID, PlanningManagerID, Updated_By);

                if (PlanningManagerID != null)
                {
                    HR.BLL.Reports rpt = new HR.BLL.Reports();

                    //var dtEmployees = rpt.MGRExtractReport(PlanningManagerID);

                    var lstSubEmployees = ReportInfo.GetSubEmployees(PlanningManagerID);

                    var lstPlanningManagers = EmployeeInfo.getPoolManagersLoadedinMaster();

                    //var orgDt = dtEmployees.Copy();

                    

                    //Find Manager Extract for all low level Planning Managers
                    while (lstSubEmployees.Count > 0)
                    {
                        var subDt = new List<Employee>();

                        foreach (Employee subEmployee in lstSubEmployees)
                        {

                            if (lstPlanningManagers.Find(m => m.Planning_manager_ID == subEmployee.EmplID) != null)
                            {
                                var dtExtract = ReportInfo.GetReportData(reportId, criteriaType, HrBPID, subEmployee.EmplID, null);

                                dt.Merge(dtExtract);

                                var subEmployees = ReportInfo.GetSubEmployees(subEmployee.EmplID);
                                subDt.AddRange(subEmployees);

                            }

                        }
                        lstSubEmployees = null;
                        lstSubEmployees = subDt;
                        subDt = null;
                    }

                }

                var UserAccessLevel = currentUser.IsCompTeam == 'Y' ? 3 : 0;
                UserAccessLevel = UserAccessLevel == 0 && currentUser.IsHRGen == 'Y' ? 2 : UserAccessLevel;
                UserAccessLevel = UserAccessLevel == 0 && currentUser.IsManager == 'Y' ? 1 : UserAccessLevel;
                
                dt = ReportInfo.RemoveColumnsByRoleAndReport(dt, reportId,UserAccessLevel);
                
                return dt;
            }
            catch (Exception Ex)
            {
                Response.Redirect("ReportsHome.aspx");
                return new DataTable();
            }

        }

        protected void CreateExcelOutPut(DataTable dt, string filename )
        {
            
            HRCompInfo compInfo = new HRCompInfo();
            DataTable dtPrevYear = compInfo.getConfiguration("PreviousYear");
            string prevYear = dtPrevYear.Rows[0]["Config_Val"].ToString();
            DataTable dtCurrYear = compInfo.getConfiguration("CurrentYear");
            string currYear = dtCurrYear.Rows[0]["Config_Val"].ToString();

            string tab = string.Empty;

            string attachment = "attachment; filename=" + filename;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            foreach (System.Data.DataColumn dtcol in dt.Columns)
            {
                string col = dtcol.ColumnName;
                col = col.Replace("PY", prevYear.ToString());
                col = col.Replace("CY", currYear.ToString());
                Response.Write(tab + col);
                tab = "\t";
            }            
            Response.Write("\n");

            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName == "EID")
                    {
                        tab = "";
                        Response.Write(tab + dr[j].ToString());
                        tab = "\t";
                    }
                    else
                    {
                        Response.Write(tab + Convert.ToString(dr[j]));
                        tab = "\t";
                    }
                }
                Response.Write("\n");
            }
            Response.End();

           
        }
    }
}