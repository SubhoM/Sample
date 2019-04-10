using HR.API.Helpers;
using HR.BLL;
using HR.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace HR.Controller
{
    public class SalaryRangeController : ApiController
    {

        [VersionedRoute("GetCashEquitySplit/{id?}", 1)]
        public HttpResponseMessage GetCashEquitySplit(decimal Rule_ID, decimal New_Base_USD, decimal Total_Incentive)
        {
            try
            {
                
                var _cashEquity = SalaryRangeInfo.GetCashEquitySplit(Rule_ID, New_Base_USD, Total_Incentive);
                
                return Request.CreateResponse(HttpStatusCode.OK, _cashEquity);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/SalaryRange/GetCashEquitySplit");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }


        [VersionedRoute("UploadSalaryRange/{id?}", 1)]
        public HttpResponseMessage UploadFile([FromBody] rangeData _rangeData)
        {
            string sPath = "";

            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/LoadFiles/") + _rangeData.FileType + ".csv";
            
            try
            {
                
                File.Delete(sPath);
                               
                File.WriteAllText(sPath, _rangeData.FileContent);
                
                SalaryRangeInfo.UploadFile(sPath, _rangeData.FileType);
                
                File.Delete(sPath);

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/SalaryRange/UploadSalaryRange");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
            finally
            {


                File.Delete(sPath);

            }

        }

        public class rangeData
        {
            public string FileContent { get; set; }
            public string FileType { get; set; }
        }

        [VersionedRoute("GetOrg1/{id?}", 1)]
        public HttpResponseMessage GetOrg1()
        {
            try
            {

                var session = HttpContext.Current.Session;

                HRUser currentUser = session["currentUser"] as HRUser;

                var isCompAdmin = currentUser.IsCompTeam == 'Y' ? true : false;


                var _lstOrg1 = SalaryRangeInfo.GetOrg1(isCompAdmin, currentUser.UserId);

                return Request.CreateResponse(HttpStatusCode.OK, _lstOrg1);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/SalaryRange/GetOrg1");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }

        [VersionedRoute("GetOrg2/{id?}", 1)]
        public HttpResponseMessage GetOrg2(string OrgLevel1)
        {
            try
            {
                var session = HttpContext.Current.Session;
                
                HRUser currentUser = session["currentUser"] as HRUser;

                var isCompAdmin = currentUser.IsCompTeam == 'Y' ? true : false;


                var _lstOrg2 = SalaryRangeInfo.GetOrg2(OrgLevel1,isCompAdmin, currentUser.UserId);

                return Request.CreateResponse(HttpStatusCode.OK, _lstOrg2);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/SalaryRange/GetOrg2");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }


        [VersionedRoute("GetOrg3/{id?}", 1)]
        public HttpResponseMessage GetOrg3(string OrgLevel1, string OrgLevel2)
        {
            try
            {

                var _lstOrg3 = SalaryRangeInfo.GetOrg3(OrgLevel1,OrgLevel2);

                return Request.CreateResponse(HttpStatusCode.OK, _lstOrg3);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/SalaryRange/GetOrg3");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }


        [VersionedRoute("GetJobFamily/{id?}", 1)]
        public HttpResponseMessage GetJobFamily(string OrgLevel1, string OrgLevel2, string OrgLevel3, string Grade)
        {
            try
            {

                var _lstJobFamily = SalaryRangeInfo.GetJobFamily(OrgLevel1, OrgLevel2, OrgLevel3, Grade);

                return Request.CreateResponse(HttpStatusCode.OK, _lstJobFamily);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/SalaryRange/GetJobFamily");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }

        [VersionedRoute("GetJobSubFamily/{id?}", 1)]
        public HttpResponseMessage GetJobSubFamily(string OrgLevel1, string OrgLevel2, string OrgLevel3, string Grade, string JobFamily)
        {
            try
            {

                var _lstJobSubFamily = SalaryRangeInfo.GetJobSubFamily(OrgLevel1, OrgLevel2, OrgLevel3, Grade, JobFamily);

                return Request.CreateResponse(HttpStatusCode.OK, _lstJobSubFamily);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/SalaryRange/GetJobSubFamily");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }

        [VersionedRoute("GetGrade/{id?}", 1)]
        public HttpResponseMessage GetGrade(string OrgLevel1, string OrgLevel2, string OrgLevel3)
        {
            try
            {

                var _lstGrade = SalaryRangeInfo.GetGrade(OrgLevel1, OrgLevel2, OrgLevel3);

                return Request.CreateResponse(HttpStatusCode.OK, _lstGrade);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/SalaryRange/GetGrade");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }


        [VersionedRoute("GetLocation/{id?}", 1)]
        public HttpResponseMessage GetLocation(string OrgLevel1, string OrgLevel2, string OrgLevel3,  string Grade, string JobFamily, string JobSubFamily)
        {
            try
            {

                var _salaryRange = SalaryRangeInfo.GetLocation(OrgLevel1, OrgLevel2, OrgLevel3, Grade, JobFamily, JobSubFamily);

                return Request.CreateResponse(HttpStatusCode.OK, _salaryRange);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/SalaryRange/GetLocation");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }

        [VersionedRoute("GetTitle/{id?}", 1)]
        public HttpResponseMessage GetTitle(string OrgLevel1, string OrgLevel2, string OrgLevel3, string Grade, string JobFamily, string JobSubFamily)
        {
            try
            {

                var _salaryRange = SalaryRangeInfo.GetTitle(OrgLevel1, OrgLevel2, OrgLevel3, Grade, JobFamily, JobSubFamily);

                return Request.CreateResponse(HttpStatusCode.OK, _salaryRange);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/SalaryRange/GetTitle");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }

        [VersionedRoute("GetSalaryRange/{id?}", 1)]
        public HttpResponseMessage GetSalaryRange(string OrgLevel1, string OrgLevel2, string OrgLevel3, string JobFamily, string JobSubFamily, string Grade, string Location, string Title)
        {
            try
            {

                var _salaryRange = SalaryRangeInfo.GetSalaryRange(OrgLevel1, OrgLevel2, OrgLevel3, JobFamily,JobSubFamily, Grade, Location, Title);


                return Request.CreateResponse(HttpStatusCode.OK, _salaryRange);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/SalaryRange/GetSalaryRange");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }


        

    }
}
