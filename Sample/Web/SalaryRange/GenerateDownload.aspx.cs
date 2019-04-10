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

namespace HR.SalaryRange
{
    public partial class GenerateDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
                var fileType = Request.QueryString["fileType"].ToString();
            
                var dt = GetData(fileType);

                CreateExcelOutPut(dt, fileType + ".xls");
           
            
        }

        private DataTable GetData(string fileType)
        {
            try
            {

                var dt = SalaryRangeInfo.GetRangeToolData(fileType);
                
                return dt;
            }
            catch (Exception Ex)
            {
                Response.Redirect("SalaryRangeSetup.aspx");
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