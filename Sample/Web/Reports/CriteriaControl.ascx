<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CriteriaControl.ascx.cs" Inherits="HR.Reports.CriteriaControl" %>

<div class="col-sm-3 updatedStyle pull-right" >                
    <span style="padding-left:10px" id="spnUpdateInfo" hidden="hidden">Last Published by {{UserName}} on {{Date}}</span> <br/>                
</div>


<div class="row">        
        <div class="col-sm-12">
            <div class="nav nav-tabs" id="CompMixTab">
                <div class="btnGrpCustom btn-group" data-toggle="buttons">
                    <label class="btn btn-primary active" id="lblAssignedToMe" onclick="ReportObj.GetData(1)">
                        <input type="radio" name="options" autocomplete="off" value="1"/> Criteria Selection
                    </label>
                    <label class="btn btn-primary" id="lblAssignedByMe"  onclick="ReportObj.GetData(2)">
                        <input type="radio" name="options" autocomplete="off" value="2" /> Publish
                    </label>
                </div>
            </div>       
            </div>
    </div>

<div class="panel panel-default" style="border-top:none" >  
    <div class="panel-body" id="pnlCriteria" style="display:none">
        <div class="row">
            <div class="col-sm-2" id="divIncentivePlan">
                <label for="drpIncentivePlan" id="lblIncentivePlan">Incentive Plan</label>
                <div class="dropdown" id="drpIncentivePlan">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" id="btnIncentivePlan">
                        <span id="spnIncentivePlan">Select Incentive Plan</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu checkbox" id="lstIncentivePlan">
                        <li><a href="#"><label><input type="checkbox" />Annual</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />CEFICP</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />CMSSICP</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />DCSICPC</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />DCSICPT</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />EFSALES</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />EFSUPRT</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />I29</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />MLPBLO</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />MLPBLOSR</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />MLPDLO</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />MLPNONMGR</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />MLPSA</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />N82</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />N87</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />NBICP</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />NOBONUS</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />RBCB</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />RBCBM</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />RBCB</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />RBCOM</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />RBCPB</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />RBCRM</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />RBCTL</label></a></li>                                                                     
                    </ul>
                </div>
            </div>
            <div class="col-sm-2" id="divEligibility">
                <label for="drpEligibility" id="lblEligibility">Eligibility</label>
                <div class="dropdown" id="drpEligibility">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">
                        <span id="spnEligibility">Select Eligibility</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu checkbox" id="lstEligibility">
                        <li><a href="#"><label><input type="checkbox" />A</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />I</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />S</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />N</label></a></li>
                    </ul>
                </div>
            </div>

            <div class="col-sm-2" id="divSalaryIncreaseType">
                <label for="drpEligibility" id="lblSalaryIncreaseType">Salary Increase Type</label>
                <div class="dropdown" id="drpSalaryIncreaseType">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">
                        <span id="spnSalaryIncreaseType">Select Salary Increase Type</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu checkbox" id="lstSalaryIncreaseType">
                        <li><a href="#"><label><input type="checkbox" />Merit</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />Promotion</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />Comp Mix</label></a></li>                        
                    </ul>
                </div>
            </div>

            <div class="col-sm-2" id="divHourlyRate">
                <label for="txtHourlyRate" id="lblHourlyRate">Minimum Hourly Rate</label>
                <input type="text" class="form-control" id="txtHourlyRate" />
            </div>

            <div class="col-sm-2" id="divPerfRating">
                <label for="drpPerfRating" id="lblPerfRating">CY Performance Rating</label>
                <div class="dropdown" id="drpPerfRating">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">
                        <span id="spnPerfRating">Select Performance Rating</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu checkbox" id="lstPerfRating">
                        <li><a href="#"><label><input type="checkbox" />(Blank)</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />1</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />2</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />3</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />4</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />5</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />6</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />7</label></a></li>                        
                    </ul>
                </div>
            </div>
                <div class="col-sm-2" id="divPopulationType">
                <label for="drpPopulationType" id="lblPopulationType">Population Type</label>
                <div class="dropdown" id="drpPopulationType">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">
                        <span id="spnPopulationType">Select Population Type</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu checkbox" id="lstPopulationType">
                         <li><a href="#"><label><input type="checkbox" />CY Corp To SICP</label></a></li>
                         <li><a href="#"><label><input type="checkbox" />CY LOA</label></a></li>
                         <li><a href="#"><label><input type="checkbox" />CY SICP to Corp</label></a></li>
                         <li><a href="#"><label><input type="checkbox" />EIP</label></a></li>
                         <li><a href="#"><label><input type="checkbox" />New Hire</label></a></li>
                         <li><a href="#"><label><input type="checkbox" />No Bonus</label></a></li>
                         <li><a href="#"><label><input type="checkbox" />Pay Mix Shift</label></a></li>
                         <li><a href="#"><label><input type="checkbox" />PY Corp to SICP</label></a></li>
                         <li><a href="#"><label><input type="checkbox" />PY LOA</label></a></li>
                         <li><a href="#"><label><input type="checkbox" />PY New Hire</label></a></li>
                         <li><a href="#"><label><input type="checkbox" />PY SICP to Corp</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />Same Store</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />Same Store Adjustment</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />Same Store Promo</label></a></li>                        
                        <li><a href="#"><label><input type="checkbox" />SICP</label></a></li>
                    </ul>
                </div>
            </div>

             <div class="col-sm-2" id="divStatus">
                <label for="drpStatus" id="lblStatus">Status</label>
                <div class="dropdown" id="drpStatus">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">
                        <span id="spnStatus">Select Status</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu checkbox" id="lstStatus">
                        <li><a href="#"><label><input type="checkbox" />A</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />L</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />P</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />T</label></a></li>
                    </ul>
                </div>
            </div>

            <div class="col-sm-2" id="divCovered">
                <label for="drpCovered" id="lblCovered">Covered(C)</label>
                <div class="dropdown" id="drpCovered">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">
                        <span id="spnCovered">Select Covered(C)</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu checkbox" id="lstCovered">
                        <li><a href="#"><label><input type="checkbox" />#</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />C1</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />C2</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />C3</label></a></li>
                    </ul>
                </div>
            </div>
                      

            <div class="col-sm-3" id="divC2RiskForm">
                <label for="drpC2RiskForm" id="lblC2RiskForm">C2 Risk Form Completed</label>
                <div class="dropdown" id="drpC2RiskForm">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">
                        <span id="spnC2RiskForm">Select C2 Risk Form Completed</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu checkbox" id="lstC2RiskForm">
                        <li><a href="#"><label><input type="checkbox" />Y</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />N</label></a></li>                        
                    </ul>
                </div>
            </div>

              <div class="col-sm-3" id="divCompPercentageChange">
                <label for="drpCompPercentageChange" id="lblCompPercentageChange">Comp Percentage Change</label>
                <div class="dropdown" id="drpCompPercentageChange">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">
                        <span id="spnCompPercentageChange">Select Comp Percentage Change</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu checkbox" id="lstCompPercentageChange">
                        <li><a href="#"><label><input type="checkbox" />5</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />10</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />15</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />20</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />25</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />30</label></a></li>
                    </ul>
                </div>
            </div>

          <div class="col-sm-3" id="divMktSalPosition">
                <label for="drpCompPercentageChange" id="lblMktSalPosition">Mkt Sal Position</label>
                <div class="dropdown" id="drpMktSalPosition">
                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">
                        <span id="spnMktSalPosition">Select Mkt Sal Position</span>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu checkbox" id="lstMktSalPosition">
                        <li><a href="#"><label><input type="checkbox" />(Blank)</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />< 25P</label></a></li>                        
                        <li><a href="#"><label><input type="checkbox" />< 50P</label></a></li>
                        <li><a href="#"><label><input type="checkbox" /> 50P</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />> 50P</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />25P to 50P</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />50P to 75P</label></a></li>
                        <li><a href="#"><label><input type="checkbox" />> 75P</label></a></li>
                    </ul>
                </div>
            </div>

        </div>
        <div class="row" style="padding-top: 20px">
            <div class="col-sm-offset-9 col-sm-3">
                <button class="btn btn-primary" type="button" onclick="ReportObj.GenerateReport()">Generate Report</button>
                <button class="btn" type="button" onclick="ReportObj.GetReportSavedData()">Reset </button>
            </div>

        </div>
    </div>

    <div class="panel-body" id="pnlPublish"  hidden="hidden">
        <div class="row" style="padding-top: 20px">



            <div class="col-sm-6">
           

                <div class="checkbox">
                  <label><input type="checkbox" id="chkHRTeam" value="">HR Generalist</label>
                </div>
                <div class="checkbox">
                  <label><input type="checkbox" id="chkPlanningManager" value="">Planning Managers</label>
                </div>

            </div>

            <div class="col-sm-offset-9 col-sm-3">
                <button class="btn btn-primary dropdown-toggle" type="button" onclick="ReportObj.PublishReport()">Publish Report</button>                
            </div>
        </div>
    </div>
</div>
