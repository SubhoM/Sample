using HR.API.Helpers;
using HR.BLL;
using HR.Entities;
using HR.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;


namespace HR.Controller
{
    public class ReportsController : ApiController
    {
        [VersionedRoute("GetReportList/{id?}", 1)]
        public HttpResponseMessage GetReportList()
        {
            try
            {
                var session = HttpContext.Current.Session;

                HRUser currentUser = session["currentUser"] as HRUser;

                var isHRAccess = currentUser.IsCompTeam == 'Y' ? false : currentUser.IsHRGen == 'Y' ? true : false;

                var isMgrAccess = currentUser.IsCompTeam == 'Y' ? false : currentUser.IsManager == 'Y' ? true : false;

                var isCompTeam = currentUser.IsCompTeam == 'Y' ? true : false;

                var _reportList = ReportInfo.getReportList(isHRAccess, isMgrAccess);

                return Request.CreateResponse(HttpStatusCode.OK, new { _reportList, isCompTeam} );

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/Reports/GetReportList");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }

        [VersionedRoute("PublishReport/{id?}", 1)]
        public HttpResponseMessage PublishReport([FromBody] Report_SavedCriteria RptPublish)
        {
            try
            {
                UpdateCriteria(RptPublish, "Publish");
                    
                var report = ReportInfo.getReportDetails(RptPublish.Report_ID);

                report.HR_Access = RptPublish.HR_Access;
                report.Mgr_Access = RptPublish.Mgr_Access;
                report.Updated_By = RptPublish.Updated_By;

                report.Published_Date = RptPublish.Updated_Date;
                report.Updated_Date = RptPublish.Updated_Date;

                ReportInfo.updateReport(report);

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/Reports/PublishReport");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }

        [VersionedRoute("GetReportInfo/{id?}", 1)]
        public HttpResponseMessage GetReportInfo(int ReportID)
        {
            try
            {
                var report = ReportInfo.getReportDetails(ReportID);

                var rptInfo = new {
                    ReportName = report.Report_Name,
                    ReportDescription = report.Report_Description
                };
                
                return Request.CreateResponse(HttpStatusCode.OK, rptInfo);
            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/Reports/GetReportInfo");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }
        }

        [VersionedRoute("UpdateReportInfo/{id?}", 1)]
        public HttpResponseMessage UpdateReportInfo([FromBody] HRCMS_Report UpdatedRptInfo)
        {
            try
            {
                var _report = ReportInfo.getReportDetails(UpdatedRptInfo.Report_ID);

                _report.Report_Name = UpdatedRptInfo.Report_Name;
                _report.Report_Description = UpdatedRptInfo.Report_Description;
                
                ReportInfo.updateReport(_report);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/Reports/UpdateReportInfo");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }
        }



        [VersionedRoute("GetReportSavedData/{id?}", 1)]
        public HttpResponseMessage GetReportSavedData(int ReportID)
        {
            try
            {

                var RptSavedCriteria = ReportInfo.getReportSavedCriteria(ReportID, "Publish");

                var report = ReportInfo.getReportDetails(ReportID);

                RptSavedCriteria.HR_Access = report.HR_Access;
                RptSavedCriteria.Mgr_Access = report.Mgr_Access;


                

                return Request.CreateResponse(HttpStatusCode.OK, RptSavedCriteria);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/Reports/PublishReport");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }
        }

        [VersionedRoute("UpdateCriteriaAndGenerateReport/{id?}", 1)]
        public HttpResponseMessage UpdateCriteriaAndGenerateReport([FromBody] Report_SavedCriteria RptPublish)
        {
            try
            {
                UpdateCriteria(RptPublish, "Generate");
                
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/Reports/UpdateCriteriaAndGenerateReport");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }


        protected void UpdateCriteria(Report_SavedCriteria RptPublish,string criteriaType)
        {
            var session = HttpContext.Current.Session;

            HRUser currentUser = session["currentUser"] as HRUser;


            RptPublish.SavedCriteria_Type = criteriaType;
            RptPublish.Updated_By = currentUser.UserId;
            RptPublish.Updated_Date = DateTime.Now.ToString("MMM dd yyyy");

            ReportInfo.saveReportCriteria(RptPublish);
        }


     

    }
}