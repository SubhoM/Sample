<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalaryRange.aspx.cs" MasterPageFile="~/CSSMaster.master" Inherits="HR.SalaryRange.SalaryRange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>Salary Range</title>
     <meta name="viewport" content="width=device-width, initial-scale=1"/>
     <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

    <asp:PlaceHolder runat="server">        
         <%: Styles.Render("~/commonSite/css") %>         
    </asp:PlaceHolder>

    <link href="SalaryRange.css" rel="stylesheet" />
</head>

<body>

    <div class="container-fluid" > </div>

    <div class="col-sm-10 col-sm-offset-1">

        

        <table class="table table-bordered">
            <tr>
                <td class="tdRangeCriteria">
                <label for="drpOrgLevel1" id="lblOrgLevel1" class="lblRangeCriteria">Org Level 1: </label>
                <div class="dropdown" id="drpOrgLevel1" style="display:inline">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" id="btnOrgLevel1">
                        <span id="spnOrgLevel1">Select Org Level 1</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu" id="lstOrgLevel1">
                  
                    </ul>
                </div>
                </td>
                <td class="tdRangeCriteria">
                <label for="drpOrgLevel2" id="lblGrade" class="lblRangeCriteria">Grade: </label>
                <div class="dropdown" id="drpGrade" style="display:inline">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" id="btnGrade">
                        <span id="spnGrade">Select Grade</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu " id="lstGrade">
                  
                    </ul>
                </div>
                </td>
            </tr>
            <tr>
                <td class="tdRangeCriteria">
                <label for="drpOrgLevel1" id="lblOrgLevel2" class="lblRangeCriteria">Org Level 2: </label>
                <div class="dropdown" id="drpOrgLevel2" style="display:inline">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" id="btnOrgLevel2">
                        <span id="spnOrgLevel2">Select Org Level 2</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu " id="lstOrgLevel2">
                  
                    </ul>
                </div>
                </td>
               <td class="tdRangeCriteria">
                <label for="drpOrgLevel1" id="lblJobFamily" class="lblRangeCriteria">Job Family: </label>
                <div class="dropdown" id="drpJobFamily" style="display:inline">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" id="btnJobFamily">
                        <span id="spnJobFamily">Select Job Family</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu " id="lstJobFamily">
                  
                    </ul>
                </div>
                </td>

           </tr>

            <tr>
                <td class="tdRangeCriteria" id="tdOrgLevel3">
                <label for="drpOrgLevel1" id="lblOrgLevel3" hidden class="lblRangeCriteria">Org Level 3: </label>
                <div class="dropdown" id="drpOrgLevel3" hidden style="display:inline">
                    <button class="btn btn-default dropdown-toggle" type="button" style="display:none" data-toggle="dropdown" id="btnOrgLevel3">
                        <span id="spnOrgLevel3">Select Org Level 3</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu " id="lstOrgLevel3">
                  
                    </ul>
                </div>
                </td>
               <td class="tdRangeCriteria">
                <label for="drpOrgLevel1" id="lblJobSubFamily" class="lblRangeCriteria">Job Sub Family: </label>
                <div class="dropdown" id="drpJobSubFamily" style="display:inline">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" id="btnJobSubFamily">
                        <span id="spnJobSubFamily">Select Job Sub Family</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu " id="lstJobSubFamily">
                  
                    </ul>
                </div>
                </td>

           </tr>

            <tr id="trLocTitle" style="display:none">
                <td class="tdRangeCriteria">
                <label for="drpLocation" id="lblLocation" hidden class="lblRangeCriteria">Location: </label>
                <div class="dropdown" id="drpLocation" hidden style="display:inline">
                    <button class="btn btn-default dropdown-toggle" style="display:none" type="button" data-toggle="dropdown" id="btnLocation">
                        <span id="spnLocation">Select Location</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu " id="lstLocation">
                  
                    </ul>
                </div>
                </td>
               <td class="tdRangeCriteria">
                <label for="drpTitle" id="lblTitle" hidden class="lblRangeCriteria">Title: </label>
                <div class="dropdown" id="drpTitle" hidden style="display:inline">
                    <button class="btn btn-default dropdown-toggle" style="display:none;" type="button" data-toggle="dropdown" id="btnTitle">
                        <span id="spnTitle">Select Title</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu " id="lstTitle">
                  
                    </ul>
                </div>
                </td>

           </tr>

            <tr>
                <td colspan="2" style="text-align:center">
                    <button type="button" class="btn btn-sm btn-primary" onclick="SalaryRange.GetSalaryRange()">Get Range</button>
                    <button type="button" class="btn btn-sm btn-default" onclick="SalaryRange.ResetRange()">Clear Range</button>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center">
                    <table class="table table-bordered" align="center" style="width:70%;"> 
                        <thead class="thead">
                            <tr>
                            <th style="border:0"></th>
                            <th>Min</th>
                            <th>Mid</th>
                            <th>Max</th>
                            </tr>
                        </thead>
                        <tbody>
                        <tr>
                        <td class="tdRange" style="text-align:left" id="tdAnnualSalary">Annual Salary</td>
                        <td id="tdMinAnnualSalary"></td>
                        <td id="tdMidAnnualSalary"></td>
                        <td id="tdMaxAnnualSalary"></td>
                        </tr>
                        <tr>
                        <td class="tdRange" style="text-align:left">Incentive (Estimate)</td>
                        <td id="tdMinIncentive"></td>
                        <td id="tdMidIncentive"></td>
                        <td id="tdMaxIncentive"></td>
                        </tr>
                        <tr>
                        <td class="tdRange" style="text-align:left">Total Compensation</td>
                        <td id="tdMinTC"></td>
                        <td id="tdMidTC"></td>
                        <td id="tdMaxTC"></td>
                        </tr>
                        <tr class="trApplicableGrid">
                        <td class="tdRange" style="text-align:left">Tier</td>
                        <td id="tdSalaryTierMin"></td>
                        <td id="tdSalaryTierMid"></td>
                        <td id="tdSalaryTierMax"></td>
                        </tr>
                        <tr class="trApplicableGrid">
                        <td class="tdRange" style="text-align:left">Salary 1</td>
                        <td id="tdSalary1Min"></td>
                        <td id="tdSalary1Mid"></td>
                        <td id="tdSalary1Max"></td>
                        </tr>
                        <tr class="trApplicableGrid">
                        <td class="tdRange" style="text-align:left">Salary 2</td>
                        <td id="tdSalary2Min"></td>
                        <td id="tdSalary2Mid"></td>
                        <td id="tdSalary2Max"></td>
                        </tr>
                        </tbody>
                    </table>


                </td>


            </tr>

        </table>

        <hr style="border-top-color:grey"/>

                <table class="table table-bordered">
                    <tr>
                        <td style="width:50%;">

                            <h3 class="header" style="text-align:center;font-weight:bold"> Comp Adjustment Calculator </h3>

                    <table class="table table-bordered" > 
                        <thead class="thead">
                            <tr>
                            <th style="border:0"></th>
                            <th>Current</th>
                            <th>Proposed</th>
                            <th>% Incr</th>
                            </tr>
                        </thead>
                        <tbody>
                        <tr>
                        <td class="RangeInput">Salary*</td>
                        <td><input type="text" id="txtCurrSalary" class="form-control" onchange="SalaryRange.CompCalc()" /></td>
                        <td><input type="text" id="txtProposedSalary" class="form-control" onchange="SalaryRange.CompCalc()"/></td>
                        <td><span id="spnSalaryIncreasePerc">0</span>%</td>
                        </tr>
                        <tr>
                        <td class="RangeInput">Incentive</td>
                        <td><input type="text" id="txtCurrIncentive" class="form-control" onchange="SalaryRange.CompCalc()"/></td>
                        <td><input type="text" id="txtProposedIncentive" class="form-control" onchange="SalaryRange.CompCalc()"/></td>
                        <td><span id="spnIncentiveIncreasePerc">0</span>%</td>
                        </tr>
                        <tr>
                        <td class="tdRange" style="white-space:nowrap">Total Compensation</td>
                        <td><span id="spnCurrTC" ></span></td>
                        <td><span id="spnProposedTC" ></span></td>
                        <td><span id="spnTCIncreasePerc">0</span>%</td>
                        </tr>
                        </tbody>
                    </table>

                        </td>
                    
                        <td style="width:50%;">

                       <h3 class="header" style="text-align:center;font-weight:bold"> Incentive Cash/Deferral Estimate </h3>


                       <table class="table table-bordered" > 
                        <thead class="thead" style="text-align:center">
                            <tr>
                            <th style="border:0">Currency</th>
                            <th colspan="2" style="border:0">USD</th>  
                            
                            </tr>
                        </thead>
                        <tbody style="text-align:left">
                        <tr>
                        <td class="RangeInput">Covered (C2) Employee</td>
                        <td colspan="2">
                        <div class="dropdown" id="drpCovered" style="display:inline">
                        <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" id="btnCovered" >
                        <span id="spnCovered">Not C2</span>
                        <span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu " id="lstCovered">
                            <li><a href="#">Not C2</a></li>
                            <li><a href="#">Yes - C2</a></li>                  
                        </ul>
                        </div>
                            
                        </tr>
                        <tr>
                        <td class="RangeInput">Annual Salary</td>
                        <td  colspan="2"><input type="text" id="txtAnnualSalary" class="form-control" onchange="SalaryRange.CalculateCashEquitySplit()" /> </td> 
                        
                        </tr>
                        <tr>
                        <td class="RangeInput">Total Incentive</td>
                        <td  colspan="2"><input type="text" id="txtTotalIncentive" class="form-control" onchange="SalaryRange.CalculateCashEquitySplit()" /></td>
                        
                        </tr>
                        <tr>
                        <td> &nbsp;&nbsp;&nbsp; Incentive - Cash</td>
                        <td ><span id="spnIncentiveCash" >0</span></td>
                        <td style="width:15%"> <span id="spnIncentiveCashPerc">0</span> %</td>                        
                        </tr>
                        <tr>
                        <td> &nbsp;&nbsp;&nbsp; Incentive - Deferral</td>
                        <td><span id="spnIncentiveDeferred" >0</span></td>
                        <td> <span id="spnIncentiveDeferredhPerc">0</span> %</td>                        
                                                
                        </tr>
                        </tbody>
                    </table>

                        </td>

                    </tr>
                </table>


     </div>

        <asp:PlaceHolder runat="server">        
         <%: Scripts.Render("~/bundles/commonSiteJS") %>               
    </asp:PlaceHolder>

    <script type="text/javascript" src="SalaryRange.js"></script>

</body>
</html>

</asp:Content>