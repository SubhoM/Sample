using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.XPath;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.DirectoryServices;
using Telerik.Web.UI;
using Telerik.Reporting;
using Telerik.ReportViewer;
using HR.BLL;
using Ionic.Zip;
using HR.Reporting;
using HR.Entities;
using HR.Shared;
using System.Web.Configuration;
using System.Threading;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using ReportParameterCollection = Microsoft.Reporting.WebForms.ReportParameterCollection;
using ReportParameter = Microsoft.Reporting.WebForms.ReportParameter;

public partial class Admin : BasePage
{
    UserInfo hrUser = new UserInfo();
    public DataTable dtApp;
    HRCompInfo compInfo = new HRCompInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        CSSMaster mstPage = null;
        mstPage = Master as CSSMaster;
        mstPage.PageName = "System Administration";



        if (!IsPostBack)
        {
            HRUser currentUser = Session["currentUser"] as HRUser;

            if (Request.QueryString["Download"] != null && Request.QueryString["Download"].ToLower() == "fxrates")
            {
                btnFX_Click(null, null);
            }
            else
            {

                Reports rpt = new Reports();
                DataTable dtCountry = rpt.getCountryCodes();
                //ddlCountryCodes.DataSource = dtCountry;
                //ddlCountryCodes.DataTextField = "Country";
                //ddlCountryCodes.DataValueField = "Country";
                //ddlCountryCodes.DataBind();

                //Bind Country codes for Print Statements
                ddlCountryCodes_Print.DataSource = dtCountry;
                ddlCountryCodes_Print.DataTextField = "Country";
                ddlCountryCodes_Print.DataValueField = "Country";
                ddlCountryCodes_Print.DataBind();


                HRAudit AuditEntity = new HRAudit();
                HRAuditInfo AuditBLL = new HRAuditInfo();


                AuditEntity.EmplID = currentUser.UserId;
                AuditEntity.ActionID = AuditEntity.STATUS_SCREEN_ACCESS;

                AuditEntity.Detail = "System Administration page";
                AuditBLL.LogAudit(AuditEntity);
            }

            
            if (currentUser.IsCompMember == 'Y')
            {
                btnPrintStmt.Visible = false;
                divBulkActions.Style.Add("display", "none");
                tblAudit.Style.Add("display", "none");
                tblCleanDB.Style.Add("display", "none");
                divDiscretion.Style.Add("display", "none");
                divDeferredCash.Style.Add("display", "none");
            }
        }
        //btnAddConfig.Visible = true;
        //GetData();
        //string country = ddlCountryCodes.SelectedValue.ToString();


    }
    //protected void ddlCountryCodes_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    //{
    //    if (e.Value != "")
    //    {
    //        lblMsg.Text = "";
    //    }
    //}
    //protected void GetData()
    //{
    //    DataLoad dl = new DataLoad();
    //    dtApp = dl.getAppConfig();
    //    grdAppConfig.DataSource = dtApp;
    //    grdAppConfig.DataBind();
    //}
    protected void bntGrantAccess_Click(object sender, EventArgs e)
    {
        hrUser.grantAccessToAllManagers();
    }
    protected void btnGrantAccessHRBP_Click(object sender, EventArgs e)
    {
        hrUser.grantAccessToAllHRBPs();
    }
    protected void btnRemoveAccessHRBP_Click(object sender, EventArgs e)
    {
        hrUser.removeAccessToAllHRBPs();
    }
    //protected void btnMeritIncentive_Click(object sender, EventArgs e)
    //{
    //    Reports rpt = new Reports();
    //    DataTable dt = new DataTable();
    //    string country = ddlCountryCodes.SelectedValue.ToString();
    //    if (country == "")
    //    {
    //        lblMsg.Text = "Please select a country to generate the file";
    //        return;
    //    }
    //    else
    //    {
    //        lblMsg.Text = "";
    //    }
    //    dt = rpt.GenerateMeritIncentiveFile(country);

    //    string filename = "MeritIncentive.xls";
    //    string attachment = "attachment; filename=" + filename;
    //    Response.ClearContent();
    //    Response.AddHeader("content-disposition", attachment);
    //    Response.ContentType = "application/text-plain";
    //    string tab = string.Empty;
    //    DataTable dtCurrYear = compInfo.getConfiguration("CurrentYear");
    //    string currYear = dtCurrYear.Rows[0]["Config_Val"].ToString();
    //    foreach (System.Data.DataColumn dtcol in dt.Columns)
    //    {
    //        string col = dtcol.ColumnName;
    //        col = col.Replace("CY", currYear.ToString());
    //        Response.Write(tab + col);
    //        tab = "\t";
    //    }
    //    Response.Write("\n");
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        tab = "";
    //        for (int j = 0; j < dt.Columns.Count; j++)
    //        {
    //            string val = dr[j].ToString();
    //            Response.Write(tab + Convert.ToString(dr[j]));
    //            tab = "\t";

    //        }
    //        Response.Write(Environment.NewLine);
    //    }

    //    Response.End();
    //}
    //protected void btnTI_Click(object sender, EventArgs e)
    //{
    //    Reports rpt = new Reports();
    //    DataTable dt = new DataTable();
    //    string country = ddlCountryCodes.SelectedValue.ToString();
    //    if (country == "")
    //    {
    //        lblMsg.Text = "Please select a country to generate the file";
    //        return;
    //    }
    //    else
    //    {
    //        lblMsg.Text = "";
    //    }
    //    dt = rpt.GenerateTotalIncentiveFile(country);

    //    string filename = "TotalIncentives.xls";
    //    string attachment = "attachment; filename=" + filename;
    //    Response.ClearContent();
    //    Response.AddHeader("content-disposition", attachment);
    //    Response.ContentType = "application/text-plain";
    //    string tab = string.Empty;
    //    DataTable dtCurrYear = compInfo.getConfiguration("CurrentYear");
    //    string currYear = dtCurrYear.Rows[0]["Config_Val"].ToString();
    //    foreach (System.Data.DataColumn dtcol in dt.Columns)
    //    {
    //        string col = dtcol.ColumnName;
    //        col = col.Replace("CY", currYear.ToString());
    //        Response.Write(tab + col);
    //        tab = "\t";
    //    }
    //    Response.Write("\n");
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        tab = "";
    //        for (int j = 0; j < dt.Columns.Count; j++)
    //        {
    //            string val = dr[j].ToString();
    //            Response.Write(tab + Convert.ToString(dr[j]));
    //            tab = "\t";

    //        }
    //        Response.Write(Environment.NewLine);
    //    }

    //    Response.End();
    //}

    protected void btnCompHist_Click(object sender, EventArgs e)
    {
        Reports rpt = new Reports();
        DataTable dt = new DataTable();

        dt = rpt.GenerateCompHistoryFile();

        string filename = "PS_RPTC_COMP_HIST.csv";
        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/csv";
        string tab = string.Empty;
        foreach (System.Data.DataColumn dtcol in dt.Columns)
        {
            Response.Write(tab + dtcol.ColumnName);
            tab = ",";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string val = dr[j].ToString();
                Response.Write(tab + Convert.ToString(dr[j]));
                tab = ",";

            }
            Response.Write(Environment.NewLine);
        }

        Response.End();
    }

    protected void btnCompAccrual_Click(object sender, EventArgs e)
    {
        Reports rpt = new Reports();
        DataTable dt = new DataTable();

        dt = rpt.GenerateCompAccrualFile();

        string filename = "PS_RPTC_COMP_ACCRL.csv";
        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/csv";
        string tab = string.Empty;
        foreach (System.Data.DataColumn dtcol in dt.Columns)
        {
            Response.Write(tab + dtcol.ColumnName);
            tab = ",";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string val = dr[j].ToString();
                Response.Write(tab + Convert.ToString(dr[j]));
                tab = ",";

            }
            Response.Write(Environment.NewLine);
        }

        Response.End();
    }

    protected void btnCompAccrualNewHires_Click(object sender, EventArgs e)
    {
        Reports rpt = new Reports();
        DataTable dt = new DataTable();

        dt = rpt.GenerateCompAccrualNewHireFile();

        string filename = "PS_RPTC_COMP_ACCRL_NewHire.csv";
        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/csv";
        string tab = string.Empty;
        foreach (System.Data.DataColumn dtcol in dt.Columns)
        {
            Response.Write(tab + dtcol.ColumnName);
            tab = ",";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string val = dr[j].ToString();
                Response.Write(tab + Convert.ToString(dr[j]));
                tab = ",";

            }
            Response.Write(Environment.NewLine);
        }

        Response.End();
    }

    //protected void btnMerit_Click(object sender, EventArgs e)
    //{
    //    Reports rpt = new Reports();
    //    DataTable dt = new DataTable();
    //    string country = ddlCountryCodes.SelectedValue.ToString();
    //    if (country == "")
    //    {
    //        lblMsg.Text = "Please select a country to generate the file";
    //        return;
    //    }
    //    else
    //    {
    //        lblMsg.Text = "";
    //    }
    //    dt = rpt.GenerateCompDataForUpload(country);

    //    string filename = "rcimerit.prn";
    //    string attachment = "attachment; filename=" + filename;
    //    Response.ClearContent();
    //    Response.AddHeader("content-disposition", attachment);
    //    Response.ContentType = "application/text-plain";
    //    string tab = string.Empty;

    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        //tab = "";
    //        for (int j = 0; j < dt.Columns.Count; j++)
    //        {
    //            string val = dr[j].ToString();
    //            Response.Write(Convert.ToString(dr[j]));
    //            //tab = "\t";

    //        }
    //        Response.Write(Environment.NewLine);
    //    }

    //    Response.End();
    //}

    private class StatementProgress
    {
        public int ReportsGenerated { get; set; }
    }

    private class StatementDetails
    {
        public string EmplId { get; set; }
        public string FileName { get; set; }
        public string Key { get; set; }
        public string StmtFolder { get; set; }
        public string EmpType { get; set; }
    }

    private class RSUGrantDetails
    {
        public string EmplId { get; set; }
        public string FileName { get; set; }
        public string Key { get; set; }
        public string StmtFolder { get; set; }      
        public string AwardPrice { get; set; }
        public string DateofAward { get; set; }


    }   
    private StatementProgress statementProgress = null;

    private void GenerateDiscretionQuestionStatement(object state)
    {
        StatementDetails statementDetails = state as StatementDetails;

        ReportUtility reporting = new ReportUtility();

        DataLoad dl = new DataLoad();
        string strCurrYear = dl.getAppConfigString("CurrentYear");

        reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("Emplid", statementDetails.EmplId));
        reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("Key", statementDetails.Key));
        reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("Title", strCurrYear + " Discretion & Risk Assessment Questionnaire"));

        DiscretionSurveyStatement compStatement = new DiscretionSurveyStatement();
        reporting.GenerateReport(compStatement, ReportUtility.ReportOutputType.PDF, statementDetails.FileName, statementDetails.StmtFolder);

        if (statementProgress != null)
        {
            lock (statementProgress)
            {
                statementProgress.ReportsGenerated++;
            }
        }
    }

    private void GenerateStatement(object state)
    {
        StatementDetails statementDetails = state as StatementDetails;

        ReportUtility reporting = new ReportUtility();

        reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("Emplid", statementDetails.EmplId));
        reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("Key", statementDetails.Key));

        IReportDocument compStatement = new CompStatement();
        string empType = statementDetails.EmpType;
        if (string.Equals(empType, "ANNUAL", StringComparison.CurrentCultureIgnoreCase))
            compStatement = new CompStatementAnnual();
        else if (string.Equals(empType, "SICP", StringComparison.CurrentCultureIgnoreCase))
            compStatement = new CompStatementSICP();
        else
            compStatement = new CompStatement();
        reporting.GenerateReport(compStatement, ReportUtility.ReportOutputType.PDF, statementDetails.FileName, statementDetails.StmtFolder);

        if (statementProgress != null)
        {
            lock (statementProgress)
            {
                statementProgress.ReportsGenerated++;
            }
        }
    }

    protected void btnPrintStmt_Click(object sender, EventArgs e)
    {       
        HRUser currentUser = Session["currentUser"] as HRUser;
        string name = txtName.Text;
        string country = ddlCountryCodes_Print.SelectedValue;
        DataTable dtEmp = hrUser.getEmployeesForPrintStmt(name, country);

        string StmtFolder = Server.MapPath("~/");
        string FolderName = currentUser.UserName.Trim();
        FolderName = FolderName.Replace(" ", "");
        FolderName = FolderName.Replace(",", "_");
        FolderName += '_';
        FolderName += currentUser.UserId.ToString();
        StmtFolder += "Statements\\" + FolderName;

        if (System.IO.Directory.Exists(StmtFolder))
            DeleteDirectory(StmtFolder); // delete all files inside directory and also delete directory

        System.IO.Directory.CreateDirectory(StmtFolder);

        DataLoad dl = new DataLoad();
        string strCurrYear = dl.getAppConfigString("CurrentYear");
        if(string.IsNullOrEmpty(strCurrYear) || string.IsNullOrWhiteSpace(strCurrYear))
        {
            strCurrYear = (DateTime.Now.Year - 1).ToString();
        }

        statementProgress = new StatementProgress();

        string NameonStatementFlag = dl.getAppConfigString("IncludeNameOnStatement");

        foreach (DataRow item in dtEmp.Rows)
        {

            string emplId = item["EMPLID"].ToString();
            string psEMPLID = item["PSEMPLID"].ToString();
            //string empName = item["Employee_Name"].ToString().Trim();
            //empName = empName.Replace(" ", "");
            //empName = empName.Replace(",", "_");
            string fileName = string.Empty;
            if (NameonStatementFlag.ToString() == "Y")
            {
                string empName = item["Employee_Name"].ToString().Trim();
                empName = empName.Replace(" ", "");
                empName = empName.Replace(",", "_");
                fileName = StatementHelper.StatementName(empName, strCurrYear);
            } 
            else
            {
                if (!string.IsNullOrEmpty(psEMPLID) && !string.IsNullOrWhiteSpace(psEMPLID))
                    fileName = StatementHelper.StatementName(psEMPLID, strCurrYear);
                else
                    fileName = StatementHelper.StatementName(emplId, strCurrYear);

            }                

            string empType = item["EmployeeType"].ToString();

            ThreadPool.QueueUserWorkItem(new WaitCallback(GenerateStatement), new StatementDetails { FileName = fileName, EmplId = emplId, Key = ConfigurationManager.AppSettings["EncKey"], StmtFolder = StmtFolder, EmpType = empType});

            //GenerateStatement(fileName, emplId, StmtFolder);
        }

        while (statementProgress.ReportsGenerated < dtEmp.Rows.Count)
        {

        }

        Response.ContentType = "application/zip";
        Response.AddHeader("content-disposition", "attachment; filename=" + FolderName + ".zip");

        using (ZipFile zip = new ZipFile())
        {
            if (StatementHelper.PasswordProtectStatement())
            {               
                //zip.Password = WebConfigurationManager.AppSettings["StatementPW"];

                zip.Password = dl.getAppConfigString("StatementPW");

            }

            zip.AddDirectory(StmtFolder);
            zip.Save(Response.OutputStream);
        }

        Response.End();

        DeleteDirectory(StmtFolder);








        //string EMPin = String.Empty;
        //string EMPNAMEin = "";

        //HRUser currentUser = Session["currentUser"] as HRUser;
        //string name = txtName.Text;
        //string country = ddlCountryCodes_Print.SelectedValue;
        //DataTable dtEmp = hrUser.getEmployeesForPrintStmt(name, country);
        //string daRoot = Server.MapPath("~/");
        //Session["daRoot"] = daRoot;
        //string thisUserId = currentUser.UserId.ToString(); // Session["userid"].ToString();
        //// Directory Path
        //string SSPATH = Session["daRoot"].ToString();

        //// Foldername
        //string FolderName = currentUser.UserName.Trim();
        //FolderName = FolderName.Replace(" ", "");
        //FolderName = FolderName.Replace(",", "_");

        //FolderName += '_';
        //string FolderNamePadded = currentUser.UserId.ToString().Trim();
        //FolderNamePadded = FolderNamePadded.PadLeft(6, '0');
        //FolderName += FolderNamePadded;

        //SSPATH += "Statements\\" + FolderName;
        //string StmtFolder = SSPATH;
        //// Create folder if not exists
        //if (System.IO.Directory.Exists(StmtFolder))
        //{
        //    //System.IO.Directory.Delete(uploadFolder, true);
        //    DeleteDirectory(StmtFolder); // delete all files inside directory and also delete directory
        //    System.IO.Directory.CreateDirectory(StmtFolder);
        //}
        //else
        //{
        //    System.IO.Directory.CreateDirectory(StmtFolder);
        //}

        //foreach (DataRow item in dtEmp.Rows)
        //{

        //    EMPin = item["EMPLID"].ToString();
        //    string EMPinPadded = EMPin.ToString().Trim().PadLeft(6, '0');
        //    EMPNAMEin = item["Employee_Name"].ToString().Trim();
        //    EMPNAMEin = EMPNAMEin.Replace(" ", "");
        //    EMPNAMEin = EMPNAMEin.Replace(",", "_");
        //    string fileName = EMPNAMEin + "_" + EMPinPadded;
        //    GenerateReport(StmtFolder,fileName, EMPin);

        //}
        //Response.ContentType = "application/zip";

        //Response.AddHeader("content-disposition", "attachment; filename=" + FolderName + ".zip");

        //using (ZipFile zip = new ZipFile())
        //{
        //    zip.Password = WebConfigurationManager.AppSettings["StatementPW"];
        //    zip.AddDirectory(SSPATH);
        //    zip.Save(Response.OutputStream);
        //}

        //DeleteDirectory(SSPATH);

        //Response.End();
    }
    public void DeleteDirectory(string target_dir)
    {
        string[] files = Directory.GetFiles(target_dir);
        string[] dirs = Directory.GetDirectories(target_dir);
        foreach (string file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }
        foreach (string dir in dirs)
        {
            DeleteDirectory(dir);
        }
        Directory.Delete(target_dir, true);
    }


    //private void GenerateReport(string StmtFolder,string fileName, string empID)
    //{

    //    //string SSPATH = Server.MapPath("~/");
    //    //string FolderName = ViewState["selectedEmpName"].ToString().Trim();
    //    //FolderName = FolderName.Replace(" ", "");
    //    //FolderName = FolderName.Replace(",", "_");
    //    //FolderName += '_';
    //    //string FolderNamePadded = ViewState["selectedID"].ToString().Trim();
    //    //FolderNamePadded = FolderNamePadded.PadLeft(6, '0');
    //    //FolderName += FolderNamePadded;
    //    //SSPATH += "Statements\\" + FolderName;

    //    //string StmtFolder = SSPATH;

    //    ReportUtility reporting = new ReportUtility();
    //    // add call to new method getMissingFields
    //    string isSICP = "";
    //    string isCI = "";
    //    string isBand = "";               // 3 byte band
    //    string isSalaryChange = "";
    //    string isRSUorDef = "";           // D, R, B 
    //    string sTemplateID = "";
    //    string isSuppressPct = "";        // Y or N
    //    string isSuppressRSUDef = "";     // Y or N 
    //    string CtryCd = "";
    //    string CCYCd = "";
    //    string isCovered = "N";
    //    string covered = "";
    //    string isSuppressPSU = "N";
    //    string isSuppressSuppComp = "N";
    //    string isSuppressTCPct = "N";
    //    HR.BLL.HRCompInfo compInfo = new HRCompInfo();
    //    DataTable dtParams = compInfo.getMissingFields(empID.ToString());

    //    foreach (DataRow dr in dtParams.Rows)
    //    {
    //        for (int i = 0; i < dtParams.Rows.Count; i++)
    //        {
    //            // Suppress % changed
    //            sTemplateID = dr["Summary_TemplateID"].ToString();
    //            isSuppressPct = "N";// dr["SUPPRESS_FLAG"].ToString();
    //            CtryCd = dr["Country"].ToString();
    //            CCYCd = dr["CY_Currency_Code"].ToString();

    //            // Band Change
    //            if (dr["New_BAND"].ToString() == "")
    //            {
    //                isBand = "N";
    //            }
    //            else
    //            {
    //                isBand = "Y";
    //            }
    //            // End Band Change

    //            //Salary Change
    //            string CY = dr["Base_Salary_Local"].ToString().Trim();
    //            string NS = dr["New_Base_Salary_Local"].ToString().Trim();
    //            string CY_PSU = dr["CY_PSU_Value_USD"].ToString().Trim();
    //            string PY_PSU = dr["PY_PSU_Value_USD"].ToString().Trim();

    //            string CY_Supp_Comp = dr["CY_SuppComp_Local"].ToString().Trim();
    //            string PY_Supp_Comp = dr["PY_SuppComp_Local"].ToString().Trim();
    //            if (Convert.ToInt32(CY_PSU) <= 0 && Convert.ToInt32(PY_PSU) <= 0)
    //            {

    //                isSuppressPSU = "Y";
    //            }
    //            if (Convert.ToInt32(CY_Supp_Comp) <= 0 && Convert.ToInt32(CY_Supp_Comp) <= 0)
    //            {

    //                isSuppressSuppComp = "Y";
    //            }
    //            string tc_vs_py_pct = dr["TC_VS_PY_PCT"].ToString().Trim();
    //            if (Convert.ToInt32(tc_vs_py_pct) < 5)
    //            {
    //                isSuppressTCPct = "Y";
    //            }
    //            covered = dr["Covered"].ToString().Trim();
    //            if (covered.ToString() != "")
    //            {
    //                isCovered = "Y";
    //            }
    //            if (CY == NS)
    //            {
    //                isSalaryChange = "N";
    //            }
    //            else
    //            {
    //                isSalaryChange = "Y";
    //            }
    //            // End Salary Change

    //            // Equity and Cash 
    //            // if no equity either year then suppress equity payout line 
    //            if (dr["CY_AWARD_TYPE"].ToString() != "" && dr["PY_AWARD_TYPE"].ToString() != "")
    //            {
    //                if ((dr["CY_RSU_Deferred_Cash_USD"].ToString() == "" || dr["CY_RSU_Deferred_Cash_USD"].ToString() == "0") && (dr["PY_RSU_Deferred_Cash_USD"].ToString() == "" || dr["PY_RSU_Deferred_Cash_USD"].ToString() == "0"))
    //                {
    //                    isSuppressRSUDef = "Y";
    //                }
    //            }
    //            else if ((dr["CY_AWARD_TYPE"].ToString() == "") && (dr["PY_AWARD_TYPE"].ToString() == ""))
    //            {
    //                // Suppress RSU/Def Cash payout line
    //                isSuppressRSUDef = "Y";
    //            }
    //            else
    //            {
    //                // Display RSU/Def Cash payout line
    //                isSuppressRSUDef = "N";
    //                // if either or both years are RSU
    //                if ((dr["CY_AWARD_TYPE"].ToString() == "RSU") && (dr["PY_AWARD_TYPE"].ToString() == "RSU"))
    //                {
    //                    isRSUorDef = "R";
    //                }
    //                if ((dr["CY_AWARD_TYPE"].ToString() == "RSU") && (dr["PY_AWARD_TYPE"].ToString() == ""))
    //                {
    //                    isRSUorDef = "R";
    //                }
    //                if ((dr["CY_AWARD_TYPE"].ToString() == "") && (dr["PY_AWARD_TYPE"].ToString() == "RSU"))
    //                {
    //                    isRSUorDef = "R";
    //                }
    //                // if either or both tears are Deferred Cash
    //                if ((dr["CY_AWARD_TYPE"].ToString() == "Deferred Cash") && (dr["PY_AWARD_TYPE"].ToString() == "Deferred Cash"))
    //                {
    //                    isRSUorDef = "D";
    //                }
    //                if ((dr["CY_AWARD_TYPE"].ToString() == "Deferred Cash") && (dr["PY_AWARD_TYPE"].ToString() == ""))
    //                {
    //                    isRSUorDef = "D";
    //                }
    //                if ((dr["CY_AWARD_TYPE"].ToString() == "") && (dr["PY_AWARD_TYPE"].ToString() == "Deferred Cash"))
    //                {
    //                    isRSUorDef = "D";
    //                }
    //                // If one year or the other is Rsu or Deferred Cash
    //                if ((dr["CY_AWARD_TYPE"].ToString() == "Deferred Cash") && (dr["PY_AWARD_TYPE"].ToString() == "RSU"))
    //                {
    //                    isRSUorDef = "B";
    //                }
    //                if ((dr["CY_AWARD_TYPE"].ToString() == "RSU") && (dr["PY_AWARD_TYPE"].ToString() == "Deferred Cash"))
    //                {
    //                    isRSUorDef = "B";
    //                }
    //            }

    //        }
    //    }


    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("EMPLID", empID));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("isSICP", isSICP));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("isSalaryChange", isSalaryChange));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("isBand", isBand));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("isCI", isCI));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("isRSUorDef", isRSUorDef));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("TemplateID", sTemplateID));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("isSuppressPct", isSuppressPct));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("isSuppressRSUDef", isSuppressRSUDef));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("CtryCd", CtryCd));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("CCYCd", CCYCd));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("isCovered", isCovered));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("covered", covered));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("isSuppressPSU", isSuppressPSU));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("isSuppressSuppComp", isSuppressSuppComp));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("isSuppressTCPct", isSuppressTCPct));
    //    if (sTemplateID == "1" || sTemplateID == "2")
    //    {
    //        CompSummary reportSource1 = new CompSummary();
    //        reporting.GenerateReport(reportSource1, ReportUtility.ReportOutputType.PDF, fileName, StmtFolder);
    //    }

    //    if (sTemplateID == "10")
    //    {
    //        CompSICP reportSource = new CompSICP();
    //        reporting.GenerateReport(reportSource, ReportUtility.ReportOutputType.PDF, fileName, StmtFolder);
    //    }

    //}


    protected void btnRemoveAccess_Click(object sender, EventArgs e)
    {
        hrUser.removeAccessToAllManagers();
    }
    protected void btnFX_Click(object sender, EventArgs e)
    {
        bool isCurrentYear = sender != null ? false : true;

        DataTable dt = hrUser.getFXRates(isCurrentYear);
        string filename = "FXRates.xls";
        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = string.Empty;
        foreach (System.Data.DataColumn dtcol in dt.Columns)
        {
            string col = dtcol.ColumnName;
            Response.Write(tab + col);
            tab = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                if (dt.Columns[j].ColumnName == "Currency_Code")
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

    protected void btnRSUReport_Click(object sender, EventArgs e)
    {
        DataTable dtIncentiveData = hrUser.GetIncentiveData();

        if (dtIncentiveData != null && dtIncentiveData.Rows != null && dtIncentiveData.Rows.Count > 0)
        {
            dtIncentiveData.AsEnumerable()
                .ToList<DataRow>()
                .ForEach(r =>
                {
                    r["Price_Used"] = txtRSUPrice.Text.Trim();
                    if (string.Equals(r["Award_Type"].ToString(), "RSU", StringComparison.CurrentCultureIgnoreCase))
                        r["No_Of_RSU"] = Math.Round((Convert.ToDecimal(r["TI_Deferred_USD"]) + Convert.ToDecimal(r["Discretionary_SICP_Deferred"]) + Convert.ToDecimal(r["Special_Equity"])) / Convert.ToDecimal(txtRSUPrice.Text.Trim()), 3);
                    else
                        r["No_Of_RSU"] = 0;
                });

            string[] columns = dtIncentiveData.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName + "\t").ToArray();
            columns.SetValue(columns[columns.Length - 1] + "\n", columns.Length - 1);

            string[] rows = dtIncentiveData.AsEnumerable()
                .Select(x => x["Emplid"] + "\t" + x["Employee_Name"] + "\t" + string.Format("{0:C}", x["Total_Incentive_USD"]) + "\t" + string.Format("{0:C}", x["TI_Cash_USD"]) + "\t" + string.Format("{0:C}", x["TI_Deferred_USD"]) + "\t" + string.Format("{0:C}", x["Discretionary_SICP_Deferred"]) + "\t" + string.Format("{0:C}", x["Special_Equity"]) + "\t" + x["No_Of_RSU"] + "\t" + string.Format("{0:C}", x["Price_Used"]) + "\t" + x["Award_Type"] + "\t\n")
                .ToArray();

            string response = string.Join("", columns);
            response += string.Join("", rows);

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=RSUReport.xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Write(response);
            Response.End();


        }

    }

    protected void btnRSUGrant_Click(object sender, EventArgs e)
    {
        //string fileName = "RSUGrant";
        //IReportDocument RSUStatement = new RSUGrantStatement();
        //GenerateRSUStatement(fileName, RSUStatement);

        HRUser currentUser = Session["currentUser"] as HRUser;
        string name = txtName.Text;
        string country = ddlCountryCodes_Print.SelectedValue;
        DataTable dtEmp = hrUser.getEmployeesForPrintGrantStmt();

        string StmtFolder = Server.MapPath("~/");
        string FolderName = currentUser.UserName.Trim();
        FolderName = FolderName.Replace(" ", "");
        FolderName = FolderName.Replace(",", "_");
        FolderName += '_';
        FolderName += currentUser.UserId.ToString();
        StmtFolder += "Statements\\" + FolderName;

        if (System.IO.Directory.Exists(StmtFolder))
            DeleteDirectory(StmtFolder); // delete all files inside directory and also delete directory

        System.IO.Directory.CreateDirectory(StmtFolder);

        DataLoad dl = new DataLoad();
        string strCurrYear = dl.getAppConfigString("CurrentYear");
        if (string.IsNullOrEmpty(strCurrYear) || string.IsNullOrWhiteSpace(strCurrYear))
        {
            strCurrYear = (DateTime.Now.Year - 1).ToString();
        }

        statementProgress = new StatementProgress();

        string NameonStatementFlag = dl.getAppConfigString("IncludeNameOnStatement");

        foreach (DataRow item in dtEmp.Rows)
        {

            string emplId = item["EMPLID"].ToString();
            string psEMPLID = item["PSEMPLID"].ToString();
            string fileName = string.Empty;
            if (NameonStatementFlag.ToString() == "Y")
            {
                string empName = item["Employee_Name"].ToString().Trim();
                empName = empName.Replace(" ", "");
                empName = empName.Replace(",", "_");
                fileName = StatementHelper.RSUGrantStatementName(empName, Convert.ToDateTime(AwardDate.SelectedDate));
            }
            else
            {
                if (!string.IsNullOrEmpty(psEMPLID) && !string.IsNullOrWhiteSpace(psEMPLID))
                    fileName = StatementHelper.RSUGrantStatementName(psEMPLID, Convert.ToDateTime(AwardDate.SelectedDate));
                else
                    fileName = StatementHelper.RSUGrantStatementName(emplId, Convert.ToDateTime(AwardDate.SelectedDate));

            }

            //string empType = item["EmployeeType"].ToString();

            //ThreadPool.QueueUserWorkItem(new WaitCallback(GenerateRSUStatements), new RSUGrantDetails { FileName = fileName, EmplId = emplId, Key = ConfigurationManager.AppSettings["EncKey"], StmtFolder = StmtFolder, AwardPrice=txtRSUPrice.Text.Trim(), DateofAward= Convert.ToDateTime(AwardDate.SelectedDate).ToShortDateString() });
            RSUGrantDetails statementDetails = new RSUGrantDetails { FileName = fileName, EmplId = emplId, Key = ConfigurationManager.AppSettings["EncKey"], StmtFolder = StmtFolder, AwardPrice = txtRSUPrice.Text.Trim(), DateofAward = Convert.ToDateTime(AwardDate.SelectedDate).ToShortDateString() };

            ReportUtility reporting = new ReportUtility();

            reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("Key", ConfigurationManager.AppSettings["EncKey"]));
            reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("Emplid", emplId));
            reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("AwardPrice", txtRSUPrice.Text.Trim()));
            reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("DateofAward", Convert.ToDateTime(AwardDate.SelectedDate).ToShortDateString()));


            IReportDocument RSUStatement = new RSUGrantStatement();
            reporting.GenerateReport(RSUStatement, ReportUtility.ReportOutputType.PDF, statementDetails.FileName, statementDetails.StmtFolder);

            if (statementProgress != null)
            {
                lock (statementProgress)
                {
                    statementProgress.ReportsGenerated++;
                }
            }
        }

        while (statementProgress.ReportsGenerated < dtEmp.Rows.Count)
        {

        }

        Response.ContentType = "application/zip";
        Response.AddHeader("content-disposition", "attachment; filename=" + FolderName + ".zip");

        using (ZipFile zip = new ZipFile())
        {
            if (StatementHelper.PasswordProtectStatement())
            {
                //zip.Password = WebConfigurationManager.AppSettings["StatementPW"];

                zip.Password = dl.getAppConfigString("StatementPW");
            }

            zip.AddDirectory(StmtFolder);
            zip.Save(Response.OutputStream);
        }

        Response.End();

        DeleteDirectory(StmtFolder);

    }

    //private void GenerateRSUStatements(object state)
    //{
    //    RSUGrantDetails statementDetails = state as RSUGrantDetails;

    //    ReportUtility reporting = new ReportUtility();

        
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("Key", statementDetails.Key));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("Emplid", statementDetails.EmplId));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("AwardPrice", statementDetails.AwardPrice));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("DateofAward", statementDetails.DateofAward));



    //    IReportDocument RSUStatement = new RSUGrantStatement();
    //    reporting.GenerateReport(RSUStatement, ReportUtility.ReportOutputType.PDF, statementDetails.FileName, statementDetails.StmtFolder);

    //    if (statementProgress != null)
    //    {
    //        lock (statementProgress)
    //        {
    //            statementProgress.ReportsGenerated++;
    //        }
    //    }
    //}  

    //public void GenerateRSUStatement(string fileName, IReportDocument RSUStatement)
    //{
    //    HRUser currentUser = Session["currentUser"] as HRUser;

    //    string StmtFolder = Server.MapPath("~/");
    //    string FolderName = currentUser.UserName.Trim();
    //    FolderName = FolderName.Replace(" ", "");
    //    FolderName = FolderName.Replace(",", "_");
    //    FolderName += '_';
    //    FolderName += currentUser.UserId.ToString();
    //    StmtFolder += "Statements\\" + FolderName;

    //    if (System.IO.Directory.Exists(StmtFolder))
    //        DeleteDirectory(StmtFolder); // delete all files inside directory and also delete directory

    //    System.IO.Directory.CreateDirectory(StmtFolder);
    //    //string fileName = "RSUGrant";

    //    string key = ConfigurationManager.AppSettings["EncKey"];
    //    ReportUtility reporting = new ReportUtility();
        
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("Emplid", currentUser.UserId));
    //    reporting.ReportParameters.Add(new Telerik.Reporting.Parameter("Key", key));

    //    reporting.GenerateReport(RSUStatement, ReportUtility.ReportOutputType.PDF, fileName, StmtFolder);


    //    Response.ContentType = "application/zip";
    //    Response.AddHeader("content-disposition", "attachment; filename=" + FolderName + ".zip");

    //    using (ZipFile zip = new ZipFile())
    //    {
    //        if (StatementHelper.PasswordProtectStatement())
    //            zip.Password = WebConfigurationManager.AppSettings["StatementPW"];

    //        zip.AddDirectory(StmtFolder);
    //        zip.Save(Response.OutputStream);
    //    }

    //    Response.End();

    //    DeleteDirectory(StmtFolder);

    //}

    protected void btnTableChangesLogFile_Click(object sender, EventArgs e)
    {
        HRAuditInfo hrAuditInfo = new HRAuditInfo();
        ExcelUtilities excelUtilities = new ExcelUtilities();

        string exportString = excelUtilities.GetExportExcelString(hrAuditInfo.GetAuditCompRecs());

        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=TableChangesLogFile.xls");
        Response.ContentType = "application/vnd.ms-excel";
        Response.Write(exportString);
        Response.End();

    }

    protected void btnAuditLogFile_Click(object sender, EventArgs e)
    {
        HRAuditInfo hrAuditInfo = new HRAuditInfo();
        ExcelUtilities excelUtilities = new ExcelUtilities();

        string exportString = excelUtilities.GetExportExcelString(hrAuditInfo.GetAllAuditRecs());

        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=AuditLogFile.xls");
        Response.ContentType = "application/vnd.ms-excel";
        Response.Write(exportString);
        Response.End();
    }

    protected void btnDeferredCashStatement_Click(object sender, EventArgs e)
    {
        byte[] fileContents = null;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;
        Warning[] warnings;
        string[] streamIds;

        DataLoad dl = new DataLoad();
        string strCurrYear = dl.getAppConfigString("CurrentYear");

        var _deferredCashData = StatementInfo.getDeferredCashData();

        string _awardDate = AwardDate.SelectedDate == null ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(AwardDate.SelectedDate).ToShortDateString();
        
        string rdlcName = "rptDeferredCash.rdlc";

        var UID = 4; //Deferred Cash Report

        var _statementData = StatementInfo.getStatementDetails(UID);

        HRUser currentUser = Session["currentUser"] as HRUser;

        string StmtFolder = Server.MapPath("~/");

        string FolderName = currentUser.UserName.Trim();
        FolderName = FolderName.Replace(",", "_");
        FolderName += '_';
        FolderName += currentUser.UserId.ToString();
        StmtFolder += "Statements\\" + FolderName;

        if (System.IO.Directory.Exists(StmtFolder))
            DeleteDirectory(StmtFolder); // delete all files inside directory and also delete directory

        System.IO.Directory.CreateDirectory(StmtFolder);

        foreach (DeferredCash dfrdCash in _deferredCashData)
        {
            ReportParameterCollection reportParameterCollection = new ReportParameterCollection();

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;

            reportParameterCollection.Add(new ReportParameter("Header", _statementData.Where(m => m.Key == "txtHeader").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("HeaderNote", _statementData.Where(m => m.Key == "txtHeaderNote").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("Year", strCurrYear));
            reportParameterCollection.Add(new ReportParameter("EmployeeName", dfrdCash.EmployeeName));
            reportParameterCollection.Add(new ReportParameter("RSUDate", _awardDate));
            reportParameterCollection.Add(new ReportParameter("DeferredAmount", dfrdCash.Deferred_Amount.ToString()));
            reportParameterCollection.Add(new ReportParameter("PaymentPeriod", _statementData.Where(m => m.Key == "txtPaymentPeriod").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("ScheduledVesting", _statementData.Where(m => m.Key == "txtScheduledVesting").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("IntDurVesting", _statementData.Where(m => m.Key == "txtIntDurVesting").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("PaymentDate", _statementData.Where(m => m.Key == "txtIntroduction").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("TerminationResaon1", formatstring(_statementData.Where(m => m.Key == "txtTerminationResaon1").FirstOrDefault().Value)));
            reportParameterCollection.Add(new ReportParameter("UnvestedDeferredCash1", _statementData.Where(m => m.Key == "txtUnvestedDeferredCash1").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("TerminationReason2", formatstring(_statementData.Where(m => m.Key == "txtTerminationResaon2").FirstOrDefault().Value)));
            reportParameterCollection.Add(new ReportParameter("UnvestedDeferredCash2", _statementData.Where(m => m.Key == "txtUnvestedDeferredCash2").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("TerminationReason3", formatstring( _statementData.Where(m => m.Key == "txtTerminationResaon3").FirstOrDefault().Value)));
            reportParameterCollection.Add(new ReportParameter("UnvestedDeferredCash3", _statementData.Where(m => m.Key == "txtUnvestedDeferredCash3").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("TerminationReason4", formatstring(_statementData.Where(m => m.Key == "txtTerminationResaon4").FirstOrDefault().Value)));
            reportParameterCollection.Add(new ReportParameter("UnvestedDeferredCash4", _statementData.Where(m => m.Key == "txtUnvestedDeferredCash4").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("NonCompetition", _statementData.Where(m => m.Key == "txtNonCompetition").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("NonSolicitation", _statementData.Where(m => m.Key == "txtNonSolicitation").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("Interaction", _statementData.Where(m => m.Key == "txtInteraction").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("Cancellation", _statementData.Where(m => m.Key == "txtCancellation").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("RegRequirement", _statementData.Where(m => m.Key == "txtRegRequirement").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("FootNote", _statementData.Where(m => m.Key == "txtFootNote").FirstOrDefault().Value));
            reportParameterCollection.Add(new ReportParameter("Footer", _statementData.Where(m => m.Key == "txtFooter").FirstOrDefault().Value));

            reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = System.Web.HttpContext.Current.Request.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath) + @"\RDLC\" + rdlcName.ToString();

            reportViewer.LocalReport.SetParameters(reportParameterCollection);

            string format = "PDF";

            fileContents = reportViewer.LocalReport.Render(format, null, out mimeType, out encoding, out extension, out streamIds, out warnings);


            //EMPLID_CITDEFAWARD_YYYYMMDD

            using (FileStream fs = new FileStream(StmtFolder + "/" + dfrdCash.Emplid.ToString() + "_CITDEFAWARD_" + Convert.ToDateTime(_awardDate).ToString("yyyyMMdd") + "." + extension, FileMode.Create))
            {
                fs.Write(fileContents, 0, fileContents.Length);
            }

        }


        Response.ContentType = "application/zip";
        Response.AddHeader("content-disposition", "attachment; filename=" + FolderName + ".zip");

        using (ZipFile zip = new ZipFile())
        {
            if (StatementHelper.PasswordProtectStatement())
            {         
                zip.Password = dl.getAppConfigString("StatementPW");
            }

            zip.AddDirectory(StmtFolder);
            zip.Save(Response.OutputStream);
        }

        Response.End();

        DeleteDirectory(StmtFolder);



        //// Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
        //Response.Buffer = true;
        //Response.Clear();
        //Response.ContentType = mimeType;
        //Response.AddHeader("content-disposition", "attachment; filename=" + "DeferredCash" + "." + extension);
        //Response.BinaryWrite(fileContents); // create the file
        //Response.Flush(); // send it to the client to download
    }

    protected string formatstring(string inputString)
    {
        string CarriageReturn = "\n";

        inputString = inputString.Replace(CarriageReturn, "<br/>");

        return inputString;

    }

    protected void btnDiscretionQuestionnaire_Click(object sender, EventArgs e)
    {
        HRAuditInfo hrAuditInfo = new HRAuditInfo();
        ExcelUtilities excelUtilities = new ExcelUtilities();

        string exportString = excelUtilities.GetExportExcelString(hrAuditInfo.GetDiscretionQuestionnaireResult());

        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=DiscretionQuestionnaireReport.xls");
        Response.ContentType = "application/vnd.ms-excel";
        Response.Write(exportString);
        Response.End();




    }
    protected void btnDiscretionQuestionnaireStatement_Click(object sender, EventArgs e)
    {
        HRUser currentUser = Session["currentUser"] as HRUser;
        string name = txtName.Text;
        HRAuditInfo auditInfo = new HRAuditInfo();
        DataTable dtEmp = auditInfo.GetDiscretionQuestionnaireCount();

        string StmtFolder = Server.MapPath("~/");
        string FolderName = currentUser.UserName.Trim();
        FolderName = FolderName.Replace(" ", "");
        FolderName = FolderName.Replace(",", "_");
        FolderName += '_';
        FolderName += currentUser.UserId.ToString();
        StmtFolder += "Statements\\" + FolderName;

        if (System.IO.Directory.Exists(StmtFolder))
            DeleteDirectory(StmtFolder); // delete all files inside directory and also delete directory

        System.IO.Directory.CreateDirectory(StmtFolder);

        statementProgress = new StatementProgress();

        foreach (DataRow item in dtEmp.Rows)
        {

            string emplId = item["EMPLID"].ToString();
            string empName = item["Employee_Name"].ToString().Trim();
            empName = empName.Replace(" ", "");
            empName = empName.Replace(",", "_");
            string fileName = empName + "_" + emplId;

            ThreadPool.QueueUserWorkItem(new WaitCallback(GenerateDiscretionQuestionStatement), new StatementDetails { FileName = fileName, EmplId = emplId, Key = ConfigurationManager.AppSettings["EncKey"], StmtFolder = StmtFolder });

            //GenerateStatement(fileName, emplId, StmtFolder);
        }

        while (statementProgress.ReportsGenerated < dtEmp.Rows.Count)
        {

        }

        Response.ContentType = "application/zip";
        Response.AddHeader("content-disposition", "attachment; filename=" + FolderName + ".zip");

        using (ZipFile zip = new ZipFile())
        {
            //zip.Password = WebConfigurationManager.AppSettings["StatementPW"];            
            zip.AddDirectory(StmtFolder);
            zip.Save(Response.OutputStream);
        }

        Response.End();

        DeleteDirectory(StmtFolder);
    }

    protected void btnCleanDB_Click(object sender, EventArgs e)
    {
        HRCompInfo hr = new HRCompInfo();
        HRUser currentUser = Session["currentUser"] as HRUser;
        hr.ClearDatabase(currentUser.UserName);
    }

    protected void btnCleanStaging_Click(object sender, EventArgs e)
    {
        HRCompInfo hr = new HRCompInfo();
        HRUser currentUser = Session["currentUser"] as HRUser;
        hr.ClearStagingData(currentUser.UserName);
    }


}
