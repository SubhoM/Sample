using HR.API.Helpers;
using HR.BLL;
using HR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HR.Controller
{
    public class StatementController : ApiController
    {
        [VersionedRoute("GetStatementData/{id?}", 1)]
        public HttpResponseMessage GetStatementData(int UID)
        {
            try
            {
                var _statementData = StatementInfo.getStatementDetails(UID);

                return Request.CreateResponse(HttpStatusCode.OK, _statementData);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/Statement/GetStatementData");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }

        [VersionedRoute("UpdateCashDeferredStatementData/{id?}", 1)]
        public HttpResponseMessage UpdateCashDeferredStatementData([FromBody] DeferredCashStatement _dcStatement)
        {
            try
            {
                int uID = 4;

                StatementInfo.UpdateCashDeferredStatementData(uID,_dcStatement.txtHeader, _dcStatement.txtHeaderNote, _dcStatement.txtPaymentPeriod,
                    _dcStatement.txtScheduledVesting,_dcStatement.txtIntDurVesting,_dcStatement.txtIntroduction, _dcStatement.txtTerminationResaon1, _dcStatement.txtUnvestedDeferredCash1, 
                    _dcStatement.txtTerminationResaon2, _dcStatement.txtUnvestedDeferredCash2, _dcStatement.txtTerminationResaon3, 
                    _dcStatement.txtUnvestedDeferredCash3, _dcStatement.txtTerminationResaon4, _dcStatement.txtUnvestedDeferredCash4, 
                    _dcStatement.txtNonCompetition, _dcStatement.txtNonSolicitation, _dcStatement.txtInteraction, 
                    _dcStatement.txtCancellation, _dcStatement.txtRegRequirement, _dcStatement.txtFootNote, _dcStatement.txtFooter);

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/Statement/UpdateCashDeferredStatementData");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);
            }

        }


        [VersionedRoute("UpdatePromo418StatementData/{id?}", 1)]
        public HttpResponseMessage UpdatePromo418StatementData([FromBody] List<StatementData> lstPromo418StatementParm)
        {
            try
            {
                int uID = 5;

                StatementInfo.UpdatePromo418StatementData(uID, lstPromo418StatementParm);

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                ex.Data.Add("HTTPReferrer", "HRCMSAPI/Statement/UpdateCashDeferredStatementData");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }

        }



        
    }
}
