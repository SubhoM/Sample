using HR.API.Helpers;
using HR.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HR.API.Controllers
{
  
    public class CompMixController : ApiController
    {

        [VersionedRoute("GetSampleData/{id?}", 1)]
        [HttpGet]
        public HttpResponseMessage GetSampleData()
        {
            try
            {
                var session = HttpContext.Current.Session;
                
                var sampleText = "Sample Text";

                return Request.CreateResponse(HttpStatusCode.OK, sampleText);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/CompMixInfo/GetSampleData");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }


        [VersionedRoute("GetSampleDatatwo/{id?}", 1)]
        [HttpGet]
        public HttpResponseMessage GetSampleDatatwo()
        {
            try
            {
                var sampleText = "Sample Text Two";

                return Request.CreateResponse(HttpStatusCode.OK, sampleText);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/CompMixInfo/GetSampleData");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }


        [VersionedRoute("GetCompMixGrid/{id?}", 1)]
        [HttpGet]
        public HttpResponseMessage GetCompMixGrid()
        {
            try
            {
                var session = HttpContext.Current.Session;

                var _compMixGrid = CompMixInfo.getCompMixGrid();                

                return Request.CreateResponse(HttpStatusCode.OK, _compMixGrid);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/CompMixInfo/GetCompMixGrid");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }

        [VersionedRoute("UpdateApplicableGrid/{id?}", 1)]
        public HttpResponseMessage UpdateApplicableGrid([FromBody] CompMixUpdate _compMixUpdate)
        {
            try
            {
                var session = HttpContext.Current.Session;

                CompMixInfo.deleteCompMixGrid(_compMixUpdate.deletedIds);

                CompMixInfo.updateCompMixGrid(_compMixUpdate.updatedList);                

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/CompMixInfo/UpdateCompMixGrid");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }

        [VersionedRoute("GetCompMixTier/{id?}", 1)]
        [HttpGet]
        public HttpResponseMessage GetCompMixTier()
        {
            try
            {
                var session = HttpContext.Current.Session;

                var _compMixGrid = CompMixInfo.getCompMixTier();

                return Request.CreateResponse(HttpStatusCode.OK, _compMixGrid);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/CompMixInfo/GetCompMixGrid");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }

        [VersionedRoute("UpdateCompMixTier/{id?}", 1)]
        public HttpResponseMessage UpdateCompMixTier([FromBody] CompMixTierUpdate _compMixTierUpdate)
        {
            try
            {
                var session = HttpContext.Current.Session;

                CompMixInfo.deleteCompMixTier(_compMixTierUpdate.deletedIds);

                CompMixInfo.insertCompMixTier(_compMixTierUpdate.newList);

                CompMixInfo.updateCompMixTier(_compMixTierUpdate.updatedList);

                CompMixRecalculate();

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/CompMixInfo/UpdateCompMixTier");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }

        [VersionedRoute("CompMixRecalculate/{id?}", 1)]
        public void CompMixRecalculate()
        {
            try
            {
                var session = HttpContext.Current.Session;
                
                CompMixInfo.calculateCompMix(null);                               

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/CompMixInfo/CompMixRecalculate");

                throw ex;
            }

        }

    }
}
