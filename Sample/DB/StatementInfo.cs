using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CIT.Shared.DataHelper;
using HR.Entities;
using System.Configuration;
using System.Linq;
using System.ComponentModel;
using System.Reflection;

namespace HR.BLL
{

    public class StatementInfo
    {
        string key = ConfigurationManager.AppSettings["EncKey"];
        public StatementInfo()
        {
        }

        public static List<Statement> getStatementDetails(int UID)
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {

                var _statementDetails = context.getStatementDetails(UID).ToList();

                return _statementDetails;
            }

        }

        public static List<DeferredCash> getDeferredCashData()
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {
                var key = "ECR";

                var _deferredCashData = context.getDeferredCashData(key).ToList();

                return _deferredCashData;
            }
        }

        public static void UpdateCashDeferredStatementData(int uID, string txtHeader, string txtHeaderNote, string txtPaymentPeriod, string txtScheduledVesting, string txtIntDurVesting, string txtIntroduction, string txtTerminationResaon1, string txtUnvestedDeferredCash1, string txtTerminationResaon2, string txtUnvestedDeferredCash2, string txtTerminationResaon3, string txtUnvestedDeferredCash3, string txtTerminationResaon4, string txtUnvestedDeferredCash4, string txtNonCompetition, string txtNonSoliciation, string txtInteraction, string txtCancellation, string txtRegRequirement, string txtFootNote, string txtFooter)
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {

               context.UpdateCashDeferredStatementData(uID,txtHeader, txtHeaderNote ,txtPaymentPeriod,txtScheduledVesting,txtIntDurVesting , txtIntroduction, txtTerminationResaon1, txtUnvestedDeferredCash1, txtTerminationResaon2, txtUnvestedDeferredCash2, txtTerminationResaon3, txtUnvestedDeferredCash3, txtTerminationResaon4, txtUnvestedDeferredCash4, txtNonCompetition, txtNonSoliciation, txtInteraction, txtCancellation, txtRegRequirement, txtFootNote, txtFooter);

            }

        }

        public static void UpdatePromo418StatementData(int UID,List<StatementData> lstPromo418Statement)
        {

            using (HRCMSEntities context = new HRCMSEntities())
            {
                if(lstPromo418Statement.Count != 13)
                    throw new System.ArgumentException("All field Values are not available", "lstPromo418Statement");

                context.UpdatePromo418StatementData(UID, lstPromo418Statement[0].Value, lstPromo418Statement[1].Value, lstPromo418Statement[2].Value, 
                    lstPromo418Statement[3].Value, lstPromo418Statement[4].Value, lstPromo418Statement[5].Value, lstPromo418Statement[6].Value, 
                    lstPromo418Statement[7].Value, lstPromo418Statement[8].Value, lstPromo418Statement[9].Value, lstPromo418Statement[10].Value, lstPromo418Statement[11].Value, 
                    lstPromo418Statement[12].Value);

            }

        }

        public DataTable getStatementData(int UID)
        {
            string query = "getStatementData";

            object[] param = new object[] { UID };

            DataTable dtRSUData = DALHelper.GetDataTable(query, CommandType.StoredProcedure, DataAccess.UsingTransaction.OptionNone, param);

            return dtRSUData;
        }

        public DataTable getRSUVestingSTGData()
        {
            string query = "getRSUVestingSTGData";

            object[] param = new object[] { };

            DataTable dtRSUData = DALHelper.GetDataTable(query, CommandType.StoredProcedure, DataAccess.UsingTransaction.OptionNone, param);

            return dtRSUData;
        }

        public bool updateStatementData(CompStatements compStatement, out bool result)
        {
            result = false;

            string updateQuery = string.Empty;
            
            updateQuery = "UpdateStatementData";
            object[] param = new object[] { compStatement.UID, compStatement.Header, compStatement.Footer, compStatement.CashIncentive, compStatement.RSUValue, compStatement.DetailsofAward, compStatement.Deferred, compStatement.GradeFootnote, compStatement.SummaryTemplate, compStatement.SpecialAward, compStatement.Promotion, compStatement.CompMixFooter,compStatement.Promo418Footer, compStatement.CashAdvance,
            compStatement.NYIllustration};

            if (DALHelper.ExecuteCommand(updateQuery,
                CommandType.StoredProcedure,
                DataAccess.UsingTransaction.OptionNone, param) != 0)
            {
                result = true;
            }
            
            return result;
        }

        public bool UpdateStatementRSUGrantData(RSUStatements rsuStatement, out bool result)
        {
            result = false;

            string updateQuery = string.Empty;

            updateQuery = "UpdateStatementRSUGrantData";
            object[] param = new object[] { rsuStatement.UID, rsuStatement.txtAwardSummary, rsuStatement.txtAgreementTerm, rsuStatement.txtAwardValue, rsuStatement.txtAwardValueText, rsuStatement.txtPerfPeriod,
            rsuStatement.txtPerfPeriodText, rsuStatement.txtScheduledGrant, rsuStatement.txtScheduledGrantText, rsuStatement.txtDividendEquiv, rsuStatement.txtDividendEquivText, rsuStatement.txtPaymentDte,
            rsuStatement.txtPaymentDteText, rsuStatement.txtCanclRecoup, rsuStatement.txtCanclRecoupText, rsuStatement.txtReguReq, rsuStatement.txtReguReqtext, rsuStatement.txtFootNote1,
            rsuStatement.txtFootNote2, rsuStatement.txtFootNote3, rsuStatement.txtFootNote4, rsuStatement.txtCopyRight, rsuStatement.txtTreatment, rsuStatement.txtNonCompetition,
            rsuStatement.txtNonCompetitionText, rsuStatement.txtNonSolicitation, rsuStatement.txtNonSolicitationText, rsuStatement.txtInteraction, rsuStatement.txtInteractionText, rsuStatement.txtFootNote5};

            if (DALHelper.ExecuteCommand(updateQuery,
                CommandType.StoredProcedure,
                DataAccess.UsingTransaction.OptionNone, param) != 0)
            {
                result = true;
            }

            return result;
        }


        public bool UpdateStatementRSUVestingData(RSUVestingStatements rsuStatement, out bool result)
        {
            result = false;

            string updateQuery = string.Empty;

            updateQuery = "UpdateStatementRSUVestingData";
            object[] param = new object[] { rsuStatement.UID, rsuStatement.Header, rsuStatement.Date, rsuStatement.Header1, rsuStatement.Footnote1, rsuStatement.Footnote2, rsuStatement.Footnote3, rsuStatement.Footnote4 };

            if (DALHelper.ExecuteCommand(updateQuery,
                CommandType.StoredProcedure,
                DataAccess.UsingTransaction.OptionNone, param) != 0)
            {
                result = true;
            }

            return result;
        }

        public DataTable LoadRSUVesting()
        {
            DataTable dtRSU = getRSUVestingSTGData();
            DataTable dtRSUResult = new DataTable();
            RSUVesting rsuVesting;
            List<RSUVesting> lstrsu = null;

            if (dtRSU.Rows.Count > 0)
            {
                var distinctEmpIds = (from row in dtRSU.AsEnumerable()
                                      select row.Field<string>("Employee ID")).Distinct();
                lstrsu = new List<RSUVesting>();

                foreach (var Id in distinctEmpIds)
                {
                    DataTable dtRSUv = new DataTable();
                    dtRSUv = dtRSU.AsEnumerable().Where(m => m.Field<string>("Employee ID") == Convert.ToString(Id)).CopyToDataTable();

                    var ProductIds = (from row in dtRSUv.AsEnumerable()
                                      select row.Field<string>("Product ID")).Distinct();
                    rsuVesting = new RSUVesting();
                    int count = 0;
                    foreach (var productid in ProductIds)
                    {
                        DataTable dtRSUProductID = new DataTable();
                        dtRSUProductID = dtRSUv.AsEnumerable().Where(m => m.Field<string>("Product ID") == Convert.ToString(productid)).OrderByDescending(m => m.Field<string>("Exercise / Distribution Type Description")).CopyToDataTable();

                        if (dtRSUProductID.Rows.Count > 0)
                        {
                            count++;
                            if (count == 1)
                            {
                                rsuVesting.Name = dtRSUProductID.Rows[0]["Participant Name"].ToString().Replace('^', ',');
                                rsuVesting.EID = dtRSUProductID.Rows[0]["Employee ID"].ToString();
                                rsuVesting.FXRate = dtRSUProductID.Rows[0]["FX rate 2/1/18"].ToString();
                                rsuVesting.CurrenyCode = dtRSUProductID.Rows[0]["Currency Code"].ToString();
                                rsuVesting.ActiveEmployee = dtRSUProductID.Rows[0]["PS ESS"].ToString();
                                rsuVesting.StatementName = dtRSUProductID.Rows[0]["Statement Name"].ToString();

                                rsuVesting.GrantDate1 = dtRSUProductID.Rows[0]["Grant Date"].ToString();
                                rsuVesting.VestDate1 = dtRSUProductID.Rows[0]["Exercise / Distribution Transaction Date"].ToString();
                                rsuVesting.FMVatVest1 = dtRSUProductID.Rows[0]["FMV Price"].ToString();
                                rsuVesting.RSUS1 = dtRSUProductID.Rows[0]["Product ID"].ToString();

                                //1st Row
                                rsuVesting.PretaxVested1 = Convert.ToDouble(dtRSUProductID.Rows[0]["QTY Exercised / Distributed"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["QTY Exercised / Distributed"]);
                                rsuVesting.PretaxShares1 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]);
                                rsuVesting.PretaxDividendEqui1 = Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]);
                                rsuVesting.PretaxTotal1 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]);

                                //2nd Row
                                rsuVesting.FederalShares1 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Federal Amount"]);
                                rsuVesting.FederalDividendEqui1 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Federal Amount"]);
                                rsuVesting.FederalTotal1 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Federal Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Federal Amount"]);

                                //3rd Row
                                rsuVesting.StateShares1 = Convert.ToDouble(dtRSUProductID.Rows[0]["US State Tax Amount"]);
                                rsuVesting.StateDividendEqui1 = Convert.ToDouble(dtRSUProductID.Rows[1]["US State Tax Amount"]);
                                rsuVesting.StateTotal1 = Convert.ToDouble(dtRSUProductID.Rows[0]["US State Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US State Tax Amount"]);

                                if (string.Equals("USD", rsuVesting.CurrenyCode, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    //4th Row
                                    rsuVesting.LocaltaxShares1 = Convert.ToDouble(dtRSUProductID.Rows[0]["Tax 5 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui1 = Convert.ToDouble(dtRSUProductID.Rows[1]["Tax 5 Amount"]);
                                    rsuVesting.LocaltaxTotal1 = Convert.ToDouble(dtRSUProductID.Rows[0]["Tax 5 Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Tax 5 Amount"]);

                                    rsuVesting.LocaltaxShares1_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 6 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui1_2 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 6 Amount"]);
                                    rsuVesting.LocaltaxTotal1_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 6 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 6 Amount"]);

                                    rsuVesting.LocaltaxShares1_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 7 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui1_3 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 7 Amount"]);
                                    rsuVesting.LocaltaxTotal1_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 7 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 7 Amount"]);

                                    rsuVesting.LocaltaxHeader1 = Convert.ToString(dtRSUProductID.Rows[0]["Tax 5 Desc."]);
                                    rsuVesting.LocaltaxHeader1_2 = Convert.ToString(dtRSUProductID.Rows[0]["Tax 6 Desc."]);
                                    rsuVesting.LocaltaxHeader1_3 = Convert.ToString(dtRSUProductID.Rows[0]["Tax 7 Desc."]);
                                }
                                else
                                {
                                    rsuVesting.LocaltaxShares1 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 8 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui1 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 8 Amount"]);
                                    rsuVesting.LocaltaxTotal1 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 8 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 8 Amount"]);

                                    rsuVesting.LocaltaxShares1_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 9 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui1_2 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 9 Amount"]);
                                    rsuVesting.LocaltaxTotal1_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 9 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 9 Amount"]);

                                    rsuVesting.LocaltaxShares1_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 10 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui1_3 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 10 Amount"]);
                                    rsuVesting.LocaltaxTotal1_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 10 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 10 Amount"]);

                                    rsuVesting.LocaltaxHeader1 = Convert.ToString(dtRSUProductID.Rows[0]["Tax 8 Desc."]);
                                    rsuVesting.LocaltaxHeader1_2 = Convert.ToString(dtRSUProductID.Rows[0]["Tax 9 Desc."]);
                                    rsuVesting.LocaltaxHeader1_3 = Convert.ToString(dtRSUProductID.Rows[0]["Tax 10 Desc."]);
                                }

                                //5th Row
                                rsuVesting.SocialSecurityShares1 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Social Security Tax Amount"]);
                                rsuVesting.SocialSecurityDividendEqui1 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Social Security Tax Amount"]);
                                rsuVesting.SocialSecurityTotal1 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Social Security Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Social Security Tax Amount"]);

                                //6th Row
                                rsuVesting.MedicareShares1 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Medicare Tax Amount"]);
                                rsuVesting.MedicareDividendEqui1 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Medicare Tax Amount"]);
                                rsuVesting.MedicareTotal1 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Medicare Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Medicare Tax Amount"]);

                                //7th Row
                                rsuVesting.TotaltaxesVested1 = Convert.ToDouble(dtRSUProductID.Rows[0]["Shares Netted for Taxes"]);
                                rsuVesting.TotaltaxesShares1 = Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]);
                                rsuVesting.TotaltaxesDividendEqui1 = Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);
                                rsuVesting.TotaltaxesTotal1 = Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);

                                rsuVesting.AftertaxVested1 = Convert.ToDouble(rsuVesting.PretaxVested1) - Convert.ToDouble(rsuVesting.TotaltaxesVested1);
                                rsuVesting.AftertaxesShares1 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]) - Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]);
                                rsuVesting.AftertaxesDividendEqui1 = Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]) - Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);
                                rsuVesting.AftertaxesTotal1 = Convert.ToDouble(rsuVesting.AftertaxesShares1) + Convert.ToDouble(rsuVesting.AftertaxesDividendEqui1);
                            }

                            if (count == 2)
                            {
                                rsuVesting.GrantDate2 = dtRSUProductID.Rows[0]["Grant Date"].ToString();
                                rsuVesting.VestDate2 = dtRSUProductID.Rows[0]["Exercise / Distribution Transaction Date"].ToString();
                                rsuVesting.FMVatVest2 = dtRSUProductID.Rows[0]["FMV Price"].ToString();
                                rsuVesting.RSUS2 = dtRSUProductID.Rows[0]["Product ID"].ToString();

                                //1st Row
                                rsuVesting.PretaxVested2 = Convert.ToDouble(dtRSUProductID.Rows[0]["QTY Exercised / Distributed"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["QTY Exercised / Distributed"]);
                                rsuVesting.PretaxShares2 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]);
                                rsuVesting.PretaxDividendEqui2 = Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]);
                                rsuVesting.PretaxTotal2 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]);

                                //2nd Row
                                rsuVesting.FederalShares2 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Federal Amount"]);
                                rsuVesting.FederalDividendEqui2 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Federal Amount"]);
                                rsuVesting.FederalTotal2 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Federal Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Federal Amount"]);

                                //3rd Row
                                rsuVesting.StateShares2 = Convert.ToDouble(dtRSUProductID.Rows[0]["US State Tax Amount"]);
                                rsuVesting.StateDividendEqui2 = Convert.ToDouble(dtRSUProductID.Rows[1]["US State Tax Amount"]);
                                rsuVesting.StateTotal2 = Convert.ToDouble(dtRSUProductID.Rows[0]["US State Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US State Tax Amount"]);

                                if (string.Equals("USD", rsuVesting.CurrenyCode, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    //4th Row
                                    rsuVesting.LocaltaxShares2 = Convert.ToDouble(dtRSUProductID.Rows[0]["Tax 5 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui2 = Convert.ToDouble(dtRSUProductID.Rows[1]["Tax 5 Amount"]);
                                    rsuVesting.LocaltaxTotal2 = Convert.ToDouble(dtRSUProductID.Rows[0]["Tax 5 Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Tax 5 Amount"]);

                                    rsuVesting.LocaltaxShares2_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 6 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui2_2 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 6 Amount"]);
                                    rsuVesting.LocaltaxTotal2_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 6 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 6 Amount"]);

                                    rsuVesting.LocaltaxShares2_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 7 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui2_3 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 7 Amount"]);
                                    rsuVesting.LocaltaxTotal2_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 7 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 7 Amount"]);
                                }
                                else
                                {
                                    rsuVesting.LocaltaxShares2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 8 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui2 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 8 Amount"]);
                                    rsuVesting.LocaltaxTotal2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 8 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 8 Amount"]);

                                    rsuVesting.LocaltaxShares2_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 9 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui2_2 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 9 Amount"]);
                                    rsuVesting.LocaltaxTotal2_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 9 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 9 Amount"]);

                                    rsuVesting.LocaltaxShares2_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 10 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui2_3 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 10 Amount"]);
                                    rsuVesting.LocaltaxTotal2_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 10 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 10 Amount"]);
                                }

                                //5th Row
                                rsuVesting.SocialSecurityShares2 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Social Security Tax Amount"]);
                                rsuVesting.SocialSecurityDividendEqui2 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Social Security Tax Amount"]);
                                rsuVesting.SocialSecurityTotal2 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Social Security Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Social Security Tax Amount"]);

                                //6th Row
                                rsuVesting.MedicareShares2 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Medicare Tax Amount"]);
                                rsuVesting.MedicareDividendEqui2 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Medicare Tax Amount"]);
                                rsuVesting.MedicareTotal2 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Medicare Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Medicare Tax Amount"]);

                                //7th Row
                                rsuVesting.TotaltaxesVested2 = Convert.ToDouble(dtRSUProductID.Rows[0]["Shares Netted for Taxes"]);
                                rsuVesting.TotaltaxesShares2 = Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]);
                                rsuVesting.TotaltaxesDividendEqui2 = Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);
                                rsuVesting.TotaltaxesTotal2 = Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);

                                rsuVesting.AftertaxVested2 = Convert.ToDouble(rsuVesting.PretaxVested2) - Convert.ToDouble(rsuVesting.TotaltaxesVested2);
                                rsuVesting.AftertaxesShares2 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]) - Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]);
                                rsuVesting.AftertaxesDividendEqui2 = Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]) - Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);
                                rsuVesting.AftertaxesTotal2 = Convert.ToDouble(rsuVesting.AftertaxesShares2) + Convert.ToDouble(rsuVesting.AftertaxesDividendEqui2);
                            }

                            if (count == 3)
                            {
                                rsuVesting.GrantDate3 = dtRSUProductID.Rows[0]["Grant Date"].ToString();
                                rsuVesting.VestDate3 = dtRSUProductID.Rows[0]["Exercise / Distribution Transaction Date"].ToString();
                                rsuVesting.FMVatVest3 = dtRSUProductID.Rows[0]["FMV Price"].ToString();
                                rsuVesting.RSUS3 = dtRSUProductID.Rows[0]["Product ID"].ToString();

                                //1st Row
                                rsuVesting.PretaxVested3 = Convert.ToDouble(dtRSUProductID.Rows[0]["QTY Exercised / Distributed"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["QTY Exercised / Distributed"]);
                                rsuVesting.PretaxShares3 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]);
                                rsuVesting.PretaxDividendEqui3 = Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]);
                                rsuVesting.PretaxTotal3 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]);

                                //2nd Row
                                rsuVesting.FederalShares3 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Federal Amount"]);
                                rsuVesting.FederalDividendEqui3 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Federal Amount"]);
                                rsuVesting.FederalTotal3 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Federal Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Federal Amount"]);

                                //3rd Row
                                rsuVesting.StateShares3 = Convert.ToDouble(dtRSUProductID.Rows[0]["US State Tax Amount"]);
                                rsuVesting.StateDividendEqui3 = Convert.ToDouble(dtRSUProductID.Rows[1]["US State Tax Amount"]);
                                rsuVesting.StateTotal3 = Convert.ToDouble(dtRSUProductID.Rows[0]["US State Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US State Tax Amount"]);

                                if (string.Equals("USD", rsuVesting.CurrenyCode, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    //4th Row
                                    rsuVesting.LocaltaxShares3 = Convert.ToDouble(dtRSUProductID.Rows[0]["Tax 5 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui3 = Convert.ToDouble(dtRSUProductID.Rows[1]["Tax 5 Amount"]);
                                    rsuVesting.LocaltaxTotal3 = Convert.ToDouble(dtRSUProductID.Rows[0]["Tax 5 Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Tax 5 Amount"]);

                                    rsuVesting.LocaltaxShares3_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 6 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui3_2 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 6 Amount"]);
                                    rsuVesting.LocaltaxTotal3_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 6 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 6 Amount"]);

                                    rsuVesting.LocaltaxShares3_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 7 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui3_3 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 7 Amount"]);
                                    rsuVesting.LocaltaxTotal3_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 7 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 7 Amount"]);
                                }
                                else
                                {
                                    rsuVesting.LocaltaxShares3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 8 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui3 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 8 Amount"]);
                                    rsuVesting.LocaltaxTotal3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 8 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 8 Amount"]);

                                    rsuVesting.LocaltaxShares3_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 9 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui3_2 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 9 Amount"]);
                                    rsuVesting.LocaltaxTotal3_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 9 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 9 Amount"]);

                                    rsuVesting.LocaltaxShares3_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 10 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui3_3 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 10 Amount"]);
                                    rsuVesting.LocaltaxTotal3_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 10 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 10 Amount"]);
                                }

                                //5th Row
                                rsuVesting.SocialSecurityShares3 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Social Security Tax Amount"]);
                                rsuVesting.SocialSecurityDividendEqui3 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Social Security Tax Amount"]);
                                rsuVesting.SocialSecurityTotal3 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Social Security Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Social Security Tax Amount"]);

                                //6th Row
                                rsuVesting.MedicareShares3 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Medicare Tax Amount"]);
                                rsuVesting.MedicareDividendEqui3 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Medicare Tax Amount"]);
                                rsuVesting.MedicareTotal3 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Medicare Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Medicare Tax Amount"]);

                                //7th Row
                                rsuVesting.TotaltaxesVested3 = Convert.ToDouble(dtRSUProductID.Rows[0]["Shares Netted for Taxes"]);
                                rsuVesting.TotaltaxesShares3 = Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]);
                                rsuVesting.TotaltaxesDividendEqui3 = Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);
                                rsuVesting.TotaltaxesTotal3 = Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);

                                rsuVesting.AftertaxVested3 = Convert.ToDouble(rsuVesting.PretaxVested3) - Convert.ToDouble(rsuVesting.TotaltaxesVested3);
                                rsuVesting.AftertaxesShares3 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]) - Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]);
                                rsuVesting.AftertaxesDividendEqui3 = Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]) - Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);
                                rsuVesting.AftertaxesTotal3 = Convert.ToDouble(rsuVesting.AftertaxesShares3) + Convert.ToDouble(rsuVesting.AftertaxesDividendEqui3);
                            }

                            if (count == 4)
                            {
                                rsuVesting.GrantDate4 = dtRSUProductID.Rows[0]["Grant Date"].ToString();
                                rsuVesting.VestDate4 = dtRSUProductID.Rows[0]["Exercise / Distribution Transaction Date"].ToString();
                                rsuVesting.FMVatVest4 = dtRSUProductID.Rows[0]["FMV Price"].ToString();
                                rsuVesting.RSUS4 = dtRSUProductID.Rows[0]["Product ID"].ToString();

                                //1st Row
                                rsuVesting.PretaxVested4 = Convert.ToDouble(dtRSUProductID.Rows[0]["QTY Exercised / Distributed"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["QTY Exercised / Distributed"]);
                                rsuVesting.PretaxShares4 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]);
                                rsuVesting.PretaxDividendEqui4 = Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]);
                                rsuVesting.PretaxTotal4 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]);

                                //2nd Row
                                rsuVesting.FederalShares4 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Federal Amount"]);
                                rsuVesting.FederalDividendEqui4 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Federal Amount"]);
                                rsuVesting.FederalTotal4 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Federal Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Federal Amount"]);

                                //3rd Row
                                rsuVesting.StateShares4 = Convert.ToDouble(dtRSUProductID.Rows[0]["US State Tax Amount"]);
                                rsuVesting.StateDividendEqui4 = Convert.ToDouble(dtRSUProductID.Rows[1]["US State Tax Amount"]);
                                rsuVesting.StateTotal4 = Convert.ToDouble(dtRSUProductID.Rows[0]["US State Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US State Tax Amount"]);

                                if (string.Equals("USD", rsuVesting.CurrenyCode, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    //4th Row
                                    rsuVesting.LocaltaxShares4 = Convert.ToDouble(dtRSUProductID.Rows[0]["Tax 5 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui4 = Convert.ToDouble(dtRSUProductID.Rows[1]["Tax 5 Amount"]);
                                    rsuVesting.LocaltaxTotal4 = Convert.ToDouble(dtRSUProductID.Rows[0]["Tax 5 Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Tax 5 Amount"]);

                                    rsuVesting.LocaltaxShares4_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 6 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui4_2 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 6 Amount"]);
                                    rsuVesting.LocaltaxTotal4_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 6 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 6 Amount"]);

                                    rsuVesting.LocaltaxShares4_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 7 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui4_3 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 7 Amount"]);
                                    rsuVesting.LocaltaxTotal4_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 7 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 7 Amount"]);
                                }
                                else
                                {
                                    rsuVesting.LocaltaxShares4 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 8 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui4 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 8 Amount"]);
                                    rsuVesting.LocaltaxTotal4 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 8 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 8 Amount"]);

                                    rsuVesting.LocaltaxShares4_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 9 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui4_2 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 9 Amount"]);
                                    rsuVesting.LocaltaxTotal4_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 9 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 9 Amount"]);

                                    rsuVesting.LocaltaxShares4_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 10 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui4_3 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 10 Amount"]);
                                    rsuVesting.LocaltaxTotal4_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 10 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 10 Amount"]);
                                }

                                //5th Row
                                rsuVesting.SocialSecurityShares4 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Social Security Tax Amount"]);
                                rsuVesting.SocialSecurityDividendEqui4 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Social Security Tax Amount"]);
                                rsuVesting.SocialSecurityTotal4 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Social Security Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Social Security Tax Amount"]);

                                //6th Row
                                rsuVesting.MedicareShares4 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Medicare Tax Amount"]);
                                rsuVesting.MedicareDividendEqui4 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Medicare Tax Amount"]);
                                rsuVesting.MedicareTotal4 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Medicare Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Medicare Tax Amount"]);

                                //7th Row
                                rsuVesting.TotaltaxesVested4 = Convert.ToDouble(dtRSUProductID.Rows[0]["Shares Netted for Taxes"]);
                                rsuVesting.TotaltaxesShares4 = Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]);
                                rsuVesting.TotaltaxesDividendEqui4 = Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);
                                rsuVesting.TotaltaxesTotal4 = Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);

                                rsuVesting.AftertaxVested4 = Convert.ToDouble(rsuVesting.PretaxVested4) - Convert.ToDouble(rsuVesting.TotaltaxesVested4);
                                rsuVesting.AftertaxesShares4 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]) - Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]);
                                rsuVesting.AftertaxesDividendEqui4 = Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]) - Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);
                                rsuVesting.AftertaxesTotal4 = Convert.ToDouble(rsuVesting.AftertaxesShares4) + Convert.ToDouble(rsuVesting.AftertaxesDividendEqui4);
                            }
                            if (count == 5)
                            {
                                rsuVesting.GrantDate5 = dtRSUProductID.Rows[0]["Grant Date"].ToString();
                                rsuVesting.VestDate5 = dtRSUProductID.Rows[0]["Exercise / Distribution Transaction Date"].ToString();
                                rsuVesting.FMVatVest5 = dtRSUProductID.Rows[0]["FMV Price"].ToString();
                                rsuVesting.RSUS5 = dtRSUProductID.Rows[0]["Product ID"].ToString();

                                //1st Row
                                rsuVesting.PretaxVested5 = Convert.ToDouble(dtRSUProductID.Rows[0]["QTY Exercised / Distributed"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["QTY Exercised / Distributed"]);
                                rsuVesting.PretaxShares5 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]);
                                rsuVesting.PretaxDividendEqui5 = Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]);
                                rsuVesting.PretaxTotal5 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]);

                                //2nd Row
                                rsuVesting.FederalShares5 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Federal Amount"]);
                                rsuVesting.FederalDividendEqui5 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Federal Amount"]);
                                rsuVesting.FederalTotal5 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Federal Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Federal Amount"]);

                                //3rd Row
                                rsuVesting.StateShares5 = Convert.ToDouble(dtRSUProductID.Rows[0]["US State Tax Amount"]);
                                rsuVesting.StateDividendEqui5 = Convert.ToDouble(dtRSUProductID.Rows[1]["US State Tax Amount"]);
                                rsuVesting.StateTotal5 = Convert.ToDouble(dtRSUProductID.Rows[0]["US State Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US State Tax Amount"]);

                                if (string.Equals("USD", rsuVesting.CurrenyCode, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    //4th Row
                                    rsuVesting.LocaltaxShares5 = Convert.ToDouble(dtRSUProductID.Rows[0]["Tax 5 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui5 = Convert.ToDouble(dtRSUProductID.Rows[1]["Tax 5 Amount"]);
                                    rsuVesting.LocaltaxTotal5 = Convert.ToDouble(dtRSUProductID.Rows[0]["Tax 5 Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Tax 5 Amount"]);

                                    rsuVesting.LocaltaxShares5_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 6 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui5_2 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 6 Amount"]);
                                    rsuVesting.LocaltaxTotal5_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 6 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 6 Amount"]);

                                    rsuVesting.LocaltaxShares5_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 7 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui5_3 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 7 Amount"]);
                                    rsuVesting.LocaltaxTotal5_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 7 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 7 Amount"]);
                                }
                                else
                                {
                                    rsuVesting.LocaltaxShares5 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 8 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui5 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 8 Amount"]);
                                    rsuVesting.LocaltaxTotal5 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 8 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 8 Amount"]);

                                    rsuVesting.LocaltaxShares5_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 9 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui5_2 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 9 Amount"]);
                                    rsuVesting.LocaltaxTotal5_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 9 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 9 Amount"]);

                                    rsuVesting.LocaltaxShares5_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 10 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui5_3 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 10 Amount"]);
                                    rsuVesting.LocaltaxTotal5_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 10 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 10 Amount"]);
                                }

                                //5th Row
                                rsuVesting.SocialSecurityShares5 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Social Security Tax Amount"]);
                                rsuVesting.SocialSecurityDividendEqui5 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Social Security Tax Amount"]);
                                rsuVesting.SocialSecurityTotal5 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Social Security Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Social Security Tax Amount"]);

                                //6th Row
                                rsuVesting.MedicareShares5 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Medicare Tax Amount"]);
                                rsuVesting.MedicareDividendEqui5 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Medicare Tax Amount"]);
                                rsuVesting.MedicareTotal5 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Medicare Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Medicare Tax Amount"]);

                                //7th Row
                                rsuVesting.TotaltaxesVested5 = Convert.ToDouble(dtRSUProductID.Rows[0]["Shares Netted for Taxes"]);
                                rsuVesting.TotaltaxesShares5 = Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]);
                                rsuVesting.TotaltaxesDividendEqui5 = Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);
                                rsuVesting.TotaltaxesTotal5 = Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);

                                rsuVesting.AftertaxVested5 = Convert.ToDouble(rsuVesting.PretaxVested5) - Convert.ToDouble(rsuVesting.TotaltaxesVested5);
                                rsuVesting.AftertaxesShares5 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]) - Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]);
                                rsuVesting.AftertaxesDividendEqui5 = Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]) - Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);
                                rsuVesting.AftertaxesTotal5 = Convert.ToDouble(rsuVesting.AftertaxesShares5) + Convert.ToDouble(rsuVesting.AftertaxesDividendEqui5);
                            }
                            if (count == 6)
                            {
                                rsuVesting.GrantDate6 = dtRSUProductID.Rows[0]["Grant Date"].ToString();
                                rsuVesting.VestDate6 = dtRSUProductID.Rows[0]["Exercise / Distribution Transaction Date"].ToString();
                                rsuVesting.FMVatVest6 = dtRSUProductID.Rows[0]["FMV Price"].ToString();
                                rsuVesting.RSUS6 = dtRSUProductID.Rows[0]["Product ID"].ToString();

                                //1st Row
                                rsuVesting.PretaxVested6 = Convert.ToDouble(dtRSUProductID.Rows[0]["QTY Exercised / Distributed"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["QTY Exercised / Distributed"]);
                                rsuVesting.PretaxShares6 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]);
                                rsuVesting.PretaxDividendEqui6 = Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]);
                                rsuVesting.PretaxTotal6 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]);

                                //2nd Row
                                rsuVesting.FederalShares6 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Federal Amount"]);
                                rsuVesting.FederalDividendEqui6 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Federal Amount"]);
                                rsuVesting.FederalTotal6 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Federal Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Federal Amount"]);

                                //3rd Row
                                rsuVesting.StateShares6 = Convert.ToDouble(dtRSUProductID.Rows[0]["US State Tax Amount"]);
                                rsuVesting.StateDividendEqui6 = Convert.ToDouble(dtRSUProductID.Rows[1]["US State Tax Amount"]);
                                rsuVesting.StateTotal6 = Convert.ToDouble(dtRSUProductID.Rows[0]["US State Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US State Tax Amount"]);

                                if (string.Equals("USD", rsuVesting.CurrenyCode, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    //4th Row
                                    rsuVesting.LocaltaxShares6 = Convert.ToDouble(dtRSUProductID.Rows[0]["Tax 5 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui6 = Convert.ToDouble(dtRSUProductID.Rows[1]["Tax 5 Amount"]);
                                    rsuVesting.LocaltaxTotal6 = Convert.ToDouble(dtRSUProductID.Rows[0]["Tax 5 Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Tax 5 Amount"]);

                                    rsuVesting.LocaltaxShares6_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 6 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui6_2 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 6 Amount"]);
                                    rsuVesting.LocaltaxTotal6_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 6 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 6 Amount"]);

                                    rsuVesting.LocaltaxShares6_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 7 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui6_3 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 7 Amount"]);
                                    rsuVesting.LocaltaxTotal6_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 7 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 7 Amount"]);
                                }
                                else
                                {
                                    rsuVesting.LocaltaxShares6 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 8 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui6 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 8 Amount"]);
                                    rsuVesting.LocaltaxTotal6 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 8 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 8 Amount"]);

                                    rsuVesting.LocaltaxShares6_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 9 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui6_2 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 9 Amount"]);
                                    rsuVesting.LocaltaxTotal6_2 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 9 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 9 Amount"]);

                                    rsuVesting.LocaltaxShares6_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 10 Amount"]);
                                    rsuVesting.LocaltaxDividendEqui6_3 = GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 10 Amount"]);
                                    rsuVesting.LocaltaxTotal6_3 = GetValueAsDouble(dtRSUProductID.Rows[0]["Tax 10 Amount"]) + GetValueAsDouble(dtRSUProductID.Rows[1]["Tax 10 Amount"]);
                                }

                                //5th Row
                                rsuVesting.SocialSecurityShares6 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Social Security Tax Amount"]);
                                rsuVesting.SocialSecurityDividendEqui6 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Social Security Tax Amount"]);
                                rsuVesting.SocialSecurityTotal6 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Social Security Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Social Security Tax Amount"]);

                                //6th Row
                                rsuVesting.MedicareShares6 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Medicare Tax Amount"]);
                                rsuVesting.MedicareDividendEqui6 = Convert.ToDouble(dtRSUProductID.Rows[1]["US Medicare Tax Amount"]);
                                rsuVesting.MedicareTotal6 = Convert.ToDouble(dtRSUProductID.Rows[0]["US Medicare Tax Amount"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["US Medicare Tax Amount"]);

                                //7th Row
                                rsuVesting.TotaltaxesVested6 = Convert.ToDouble(dtRSUProductID.Rows[0]["Shares Netted for Taxes"]);
                                rsuVesting.TotaltaxesShares6 = Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]);
                                rsuVesting.TotaltaxesDividendEqui6 = Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);
                                rsuVesting.TotaltaxesTotal6 = Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]) + Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);

                                rsuVesting.AftertaxVested6 = Convert.ToDouble(rsuVesting.PretaxVested6) - Convert.ToDouble(rsuVesting.TotaltaxesVested6);
                                rsuVesting.AftertaxesShares6 = Convert.ToDouble(dtRSUProductID.Rows[0]["Taxable Income"]) - Convert.ToDouble(dtRSUProductID.Rows[0]["Total Tax Due"]);
                                rsuVesting.AftertaxesDividendEqui6 = Convert.ToDouble(dtRSUProductID.Rows[1]["Taxable Income"]) - Convert.ToDouble(dtRSUProductID.Rows[1]["Total Tax Due"]);
                                rsuVesting.AftertaxesTotal6 = Convert.ToDouble(rsuVesting.AftertaxesShares6) + Convert.ToDouble(rsuVesting.AftertaxesDividendEqui6);
                            }
                        }
                    }

                    lstrsu.Add(rsuVesting);
                }
            }

            return ToDataTable(lstrsu);
        }

        private double GetValueAsDouble(object val)
        {
            if (val != null)
            {
                if (string.IsNullOrEmpty(val.ToString()) || string.IsNullOrWhiteSpace(val.ToString()))
                {
                    return 0;
                }
                else
                    return Convert.ToDouble(val);
            }
            return 0;
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public DataTable DeleteRSUVestingSTGData()
        {
            string query = "DeleteRSUVestingSTGData";

            object[] param = new object[] { };

            DataTable dtRSUData = DALHelper.GetDataTable(query, CommandType.StoredProcedure, DataAccess.UsingTransaction.OptionNone, param);

            return dtRSUData;
        }
    }    
}
