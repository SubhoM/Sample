
let api = "/api/Reports/";

var reportType = {
    Report1: { ReportID: 1, ReportName: 'TI Report', Fields: ["IncentivePlan", "Eligibility", "TotalIncentive", "PerfRating"]},
    Report2: { ReportID: 2, ReportName: 'PTIE Adjustments', Fields: ["IncentivePlan", "Eligibility", "TotalIncentive", "PerfRating", "PopulationType"] },
    Report3: { ReportID: 3, ReportName: 'Promo Grade', Fields: [] },
    Report4: { ReportID: 4, ReportName: 'Rating 4/5 - TI or Merit', Fields: ["IncentivePlan", "PerfRating", "Eligibility"] },
    Report5: { ReportID: 5, ReportName: 'Off Cycle  + Merit', Fields: ["SalaryIncreaseType", "PerfRating", "Eligibility"] },
    Report6: { ReportID: 6, ReportName: 'C2 Employee Missing Use of Discretion Forms', Fields: ["Eligibility", "Covered", "C2RiskForm", "IncentivePlan"] },
    Report7: { ReportID: 7, ReportName: 'Sal <25P with No Merit', Fields: ["PerfRating" , "Eligibility", "MktSalPosition"] },
    Report8: { ReportID: 8, ReportName: 'Significant YOY Changes', Fields: ["PerfRating", "Eligibility", "CompPercentageChange"] },
    Report9: { ReportID: 9, ReportName: 'Missing Ratings', Fields: ["Status", "Eligibility", "PerfRating"] },
    Report10: { ReportID: 10, ReportName: 'LOA Flag', Fields: ["PopulationType", "PerfRating", "Eligibility"] },
    Report11: { ReportID: 11, ReportName: 'Hourly Rate', Fields: ["PerfRating", "Eligibility", "HourlyRate"] },
    Report12: { ReportID: 12, ReportName: '2019 Pay Mix Shifts', Fields: ["IncentivePlan", "Eligibility", "PopulationType", "SalaryIncreaseType"] }
    };


let _report: _IReport;

//let _reportListWithAccess: number[];

interface _IReport {
    ReportID: number;
    ReportName: number;
    Fields: string[];
}


$(document).ready(function () {

    $('#displayloading').show();

    let pageName: string;

    var reportName = sitejs.getParameterByName("ReportName");
        
    //sitejs.UpdatePageName(pageName);

    api = sitejs.UpdateAPIURL(api);

    if (!reportName) {

        sitejs.UpdatePageName('Reports');

        ReportHome.LoadReportHomePage();
        
        return;

    }
    
    _report = reportType[reportName.replace(/\s/g, '')];

    //sitejs.UpdatePageName(_report.ReportName);
    
    ReportObj.PageLoad();

    $('#pnlCriteria').show();

    $('#displayloading').hide();
    
});

declare let ReportObj: any;

ReportObj = function () {

    let TabID: number = 1;

    let lstFields = ["IncentivePlan", "Eligibility", "TotalIncentive", "PerfRating", "PopulationType", "Status", "SalaryIncreaseType", "Covered", "C2RiskForm", "CompPercentageChange", "HourlyRate", "MktSalPosition"];

    function PageLoad() {

        $('#txtRptName').hide();
        $('#txtRptDescription').hide();

        GetReportInfo();
        BindDropDowns();
        GetReportSavedData();

    }
    
    function BindDropDowns() {
        
        let fieldName;

        for (let i = 0; i < lstFields.length; i++) {
            
            fieldName = lstFields[i];

            let checkField = _report.Fields.filter(m => m == fieldName);

            if (checkField.length == 0) {
                $('#div' + fieldName).hide();
                continue;
            }
                        
            $('#lst' + fieldName + ' li').on('click', function (event) {               
                event.stopPropagation();
                multiSelectClick(lstFields[i], $(this));                
            });

        }
        
        
    }       

    function multiSelectClick(fieldName: string, currItem: JQuery) {

        let newText = $('#spn' + fieldName).text().indexOf("Select") > -1 ? "" : $('#spn' +  fieldName).text();

        let chkItem = currItem.find(':checkbox').first().is(':checked');

        //newText = chkItem ? newText + $(this).text() + ', ' : newText.replace(new RegExp($(this).text() + ', ', 'g'), '');

        var selText = currItem.text();

        if (chkItem) {

            if (newText.indexOf(selText) == -1)
                newText = newText + selText + ', ';

        }
        else {

            //newText = newText.replace(new RegExp(selText + ', ', 'g'), '');

            //let removeText = new RegExp(selText + ', ', 'gi') ;

            newText = newText.replace(selText + ", ", '');

        }

        newText = newText.trim() === '' ? 'Select ' + $('#lbl' + fieldName).text() : newText;

        $('#spn' + fieldName).text(newText);
    }

    function GetData(_tabID: number) {

        if (TabID == _tabID)
            return;

        TabID = _tabID;

        switch (TabID) {
            case 1:
                $('#pnlCriteria').show();
                $('#pnlPublish').hide();
                break;
            case 2:
                $('#pnlPublish').show();
                $('#pnlCriteria').hide();
                break;

        }

       
    }

    function GetReportInfo() {

        $.ajax({
            url: api + 'GetReportInfo?ReportID=' + _report.ReportID,
            type: "GET",
            error: function () {
                //Handle the server errors using the approach from the previous example
            },
            success: function (data) {
                $('#spnRptName').text(data.ReportName);
                $('#spnRptDescription').text(data.ReportDescription);
                sitejs.UpdatePageName(data.ReportName);
            }
        })

    }

    function EditReportDetails() {

        $('#divDescUpdate').show();

        $('#divDescEdit').hide();

        $('#txtRptName').val($('#spnRptName').text());
        $('#txtRptDescription').val($('#spnRptDescription').text());

        $('#txtRptName').show();
        $('#txtRptDescription').show();

        $('#spnRptName').hide();
        $('#spnRptDescription').hide();
    }

    function UpdateReport(cnfrmUpdate: boolean) {

        $('#Warning-alert').hide();
        $('#Error-alert').hide();

        if (cnfrmUpdate && ($('#txtRptName').val().trim() == "" || $('#txtRptDescription').val().trim() == "")){
            sitejs.ShowAlert("Please Enter Report Name and Description.", sitejs.msgType.Error);
            return;
        }

        $('#divDescUpdate').hide();
        $('#divDescEdit').show();

        $('#txtRptName').hide();
        $('#txtRptDescription').hide();

        $('#spnRptName').show();
        $('#spnRptDescription').show();

        if (!cnfrmUpdate) {            
            return;
        }

        dbUpdateReportInfo($('#txtRptName').val(), $('#txtRptDescription').val());

        $('#spnRptName').text($('#txtRptName').val());
        $('#spnRptDescription').text($('#txtRptDescription').val());
        sitejs.UpdatePageName($('#txtRptName').val());

    }

    function dbUpdateReportInfo(ReportName: string, ReportDescription: string) {

        let data = {
            Report_ID : _report.ReportID,
            Report_Name: ReportName,
            Report_Description: ReportDescription
        }

        $.ajax({
            url: api + 'UpdateReportInfo',
            type: "POST",
            data: data,
            error: function () {
                //Handle the server errors using the approach from the previous example
            },
            success: function (data) {
                sitejs.ShowAlert("Data Updated Successfully", sitejs.msgType.Success);
            }
        })
    }

    function GetReportSavedData() {


        $.ajax({
            url: api + 'GetReportSavedData?ReportID=' + _report.ReportID,
            type: "GET",
            error: function () {
                //Handle the server errors using the approach from the previous example
            },
            success: function (data) {
                BindData(data);                
            }
        })

    }

    function BindData(savedData) {

        let chkHRTeam = (<HTMLInputElement>document.getElementById("chkHRTeam")); 

        chkHRTeam.checked = savedData.HR_Access ? true : false;

        let chkPlanningManager = (<HTMLInputElement>document.getElementById("chkPlanningManager"));

        chkPlanningManager.checked = savedData.Mgr_Access ? true : false;


        let spnUpdateInfo = (<HTMLLabelElement>document.getElementById("spnUpdateInfo")); 

        spnUpdateInfo.innerText = savedData.UpdatedByUserName ? spnUpdateInfo.innerText.replace("{{UserName}}", savedData.UpdatedByUserName).replace("{{Date}}", savedData.Updated_Date) : "";        


        if (savedData.Incentive_Plan)
            (<HTMLSpanElement>document.getElementById("spnIncentivePlan")).innerText = AddCommaIfRequired(savedData.Incentive_Plan);

        if (savedData.Bonus_Eligible)
            (<HTMLSpanElement>document.getElementById("spnEligibility")).innerText = AddCommaIfRequired(savedData.Bonus_Eligible);
                
        
        //if (!savedData.Bonus_Eligible) {
        //    switch (_report.ReportID) {
        //        case 6:               
        //            (<HTMLSpanElement>document.getElementById("spnEligibility")).innerText = "A, I, ";
        //            break;
        //        case 7:
        //            (<HTMLSpanElement>document.getElementById("spnEligibility")).innerText = "A, S, ";
        //            break;
        //    }

        //}

        
        if (savedData.Status)
            (<HTMLSpanElement>document.getElementById("spnStatus")).innerText = AddCommaIfRequired(savedData.Status);

        //if (!savedData.Bonus_Eligible) {
        //    switch (_report.ReportID) {
        //        case 9:
        //            (<HTMLSpanElement>document.getElementById("spnStatus")).innerText = "A, L, P, ";
        //            break;                
        //    }

        //}

        //if (savedData.Total_Incentive_Local_Min)
        //    (<HTMLInputElement>document.getElementById("txtTotalIncentive")).value = AddCommaIfRequired(savedData.Total_Incentive_Local_Min);

        
        if (savedData.Perf_Rtg)
            (<HTMLSpanElement>document.getElementById("spnPerfRating")).innerText = AddCommaIfRequired(savedData.Perf_Rtg);



        //if (!savedData.Perf_Rtg) {
        //    switch (_report.ReportID) {
        //        case 1:
        //        case 2:
        //        case 7:
        //            (<HTMLSpanElement>document.getElementById("spnPerfRating")).innerText = "1, 2, 3, "
        //            break;
        //        case 4:
        //            (<HTMLSpanElement>document.getElementById("spnPerfRating")).innerText = "4, 5, "
        //            break;
        //        case 9:
        //            (<HTMLSpanElement>document.getElementById("spnPerfRating")).innerText = "(Blank), "
        //            break;
        //    }
            
        //}


        
        if (savedData.Population_Type)
            (<HTMLSpanElement>document.getElementById("spnPopulationType")).innerText = AddCommaIfRequired(savedData.Population_Type);


        //if (!savedData.Population_Type) {
        //    switch (_report.ReportID) {
        //        case 10:
        //            (<HTMLSpanElement>document.getElementById("spnPopulationType")).innerText = "CY LOA, PY LOA, ";
        //            break;
        //    }

        //}

        if (savedData.Merit_Promotion_Flag)
            (<HTMLSpanElement>document.getElementById("spnSalaryIncreaseType")).innerText = AddCommaIfRequired(savedData.Merit_Promotion_Flag);

        //if (!savedData.Merit_Promotion_Flag) {
        //    switch (_report.ReportID) {
        //        case 5:
        //            (<HTMLSpanElement>document.getElementById("spnSalaryIncreaseType")).innerText = "Merit, Comp Mix, ";
        //            break;
        //        case 12:
        //            (<HTMLSpanElement>document.getElementById("spnSalaryIncreaseType")).innerText = "Comp Mix, ";
        //            break;
        //    }
        //}

        if (savedData.Covered)
            (<HTMLSpanElement>document.getElementById("spnCovered")).innerText = AddCommaIfRequired(savedData.Covered);


        //if (!savedData.Covered) {
        //    switch (_report.ReportID) {
        //        case 6:
        //            (<HTMLSpanElement>document.getElementById("spnCovered")).innerText = "C2, ";
        //            break;
        //    }
        //}

        if (savedData.C2_Risk_Form)
            (<HTMLSpanElement>document.getElementById("spnC2RiskForm")).innerText = AddCommaIfRequired(savedData.C2_Risk_Form);


        //if (!savedData.C2_Risk_Form) {
        //    switch (_report.ReportID) {
        //        case 6:
        //            (<HTMLSpanElement>document.getElementById("spnC2RiskForm")).innerText = "N, ";
        //            break;
        //    }

        //}

        if (savedData.Total_Comp_Chg_YOY)
            (<HTMLSpanElement>document.getElementById("spnCompPercentageChange")).innerText = AddCommaIfRequired(savedData.Total_Comp_Chg_YOY);


        //if (!savedData.Total_Comp_Chg_YOY) {
        //    switch (_report.ReportID) {
        //        case 8:
        //            (<HTMLSpanElement>document.getElementById("spnCompPercentageChange")).innerText = "15, ";
        //            break;
        //    }

        //}

        if (savedData.Min_Hourly_Rate)
            (<HTMLInputElement>document.getElementById("txtHourlyRate")).value = savedData.Min_Hourly_Rate;

        //if (!savedData.Min_Hourly_Rate) {
        //    switch (_report.ReportID) {
        //        case 11:
        //            (<HTMLInputElement>document.getElementById("txtHourlyRate")).value = "15";
        //            break;
        //    }

        //}

        if (savedData.Internal_TC_Position)
            (<HTMLSpanElement>document.getElementById("spnMktSalPosition")).innerText = savedData.Internal_TC_Position;
        

        spnUpdateInfo.hidden = false;

        for (let i = 0; i < lstFields.length; i++) {

            UpdateCriteriaCheckBoxBySavedData(lstFields[i]);

        }

       
    }

    function AddCommaIfRequired(inputText : string) {

        let checkComma = inputText.substring(inputText.length - 2);

        if (checkComma.trim() != ",")
            inputText = inputText + ", ";

        return inputText;
    }


    function UpdateCriteriaCheckBoxBySavedData(fieldName: string) {

        let lstItems = $('#lst' + fieldName + ' li');

        

        if (!lstItems)
            return;

        let savedTextArr = $('#spn' + fieldName).text().split(",");

        for (let i = 0; i < lstItems.length; i++) {

            let currItemText = lstItems[i].innerText;
            
            let checkInSavedText = savedTextArr.filter(m => m.trim() == currItemText.trim());

            if (checkInSavedText.length == 0)
                continue;
            
            (<HTMLInputElement>$(lstItems[i]).find(':checkbox')[0]).checked = true;
            
        }




    }

    function PublishReport() {

        let data = GetFieldValues();

        $.ajax({
            url: api + 'PublishReport',
            data: data,
            type: "POST",
            error: function () {
                //Handle the server errors using the approach from the previous example
            },
            success: function () {
                sitejs.ShowAlert("Data Updated Successfully", sitejs.msgType.Success);
            }
        })

    }

    function GetFieldValues() {

        let data :any = {};

        data.Report_ID = _report.ReportID;
        data.HR_Access = (<HTMLInputElement>document.getElementById("chkHRTeam")).checked ;
        data.Mgr_Access = (<HTMLInputElement>document.getElementById("chkPlanningManager")).checked;  

        var incentivePlan = (<HTMLSpanElement>document.getElementById("spnIncentivePlan")).innerText;
        data.Incentive_Plan = incentivePlan.indexOf("Select") > - 1 ? null : incentivePlan;             

        var eligibility = (<HTMLSpanElement>document.getElementById("spnEligibility")).innerText;
        data.Bonus_Eligible = eligibility.indexOf("Select") > - 1 ? null : eligibility;             

        
        
        //data.Total_Incentive_Local_Min = (<HTMLInputElement>document.getElementById("txtTotalIncentive")).value;

        var perfRating = (<HTMLSpanElement>document.getElementById("spnPerfRating")).innerText;
        data.Perf_Rtg = perfRating.indexOf("Select") > - 1 ? null : perfRating;     

        var populationType = (<HTMLSpanElement>document.getElementById("spnPopulationType")).innerText;
        data.Population_Type = populationType.indexOf("Select") > - 1 ? null : populationType;    

        var salaryIncreaseType = (<HTMLSpanElement>document.getElementById("spnSalaryIncreaseType")).innerText;
        data.Merit_Promotion_Flag = salaryIncreaseType.indexOf("Select") > - 1 ? null : salaryIncreaseType;  

        var covered = (<HTMLSpanElement>document.getElementById("spnCovered")).innerText;
        data.Covered = covered.indexOf("Select") > - 1 ? null : covered;  

        var C2RiskForm = (<HTMLSpanElement>document.getElementById("spnC2RiskForm")).innerText;
        data.C2_Risk_Form = C2RiskForm.indexOf("Select") > - 1 ? null : C2RiskForm;  

        var CompPercentageChange = (<HTMLSpanElement>document.getElementById("spnCompPercentageChange")).innerText;
        data.Total_Comp_Chg_YOY = CompPercentageChange.indexOf("Select") > - 1 ? null : CompPercentageChange;  

        var MinHourlyRate = (<HTMLInputElement>document.getElementById("txtHourlyRate")).value;
        data.Min_Hourly_Rate = MinHourlyRate != '' ? MinHourlyRate : null;

        var MktSalPosition = (<HTMLSpanElement>document.getElementById("spnMktSalPosition")).innerText;
        data.Internal_TC_Position = MktSalPosition.indexOf("Select") > - 1 ? null : MktSalPosition;
        
        return data;
    }

    function GenerateReport(id) {
       

        let data = GetFieldValues();

        $.ajax({
            url: api + 'UpdateCriteriaAndGenerateReport',
            data: data,
            type: "POST",            
            error: function () {
                //Handle the server errors using the approach from the previous example

                sitejs.ShowAlert("Unable to get Report Data. Please Try after some time", sitejs.msgType.Error);
            },
            success: function () {

                window.location.href = "GenerateReport.aspx?reportId=" + _report.ReportID + "&criteriaType=Generate&fileName=" + _report.ReportName +".xls";                               
            }
        })
    }

  
    
    return {
        TabID: TabID,        
        GetData: GetData,        
        api: api,
        PublishReport: PublishReport,
        GenerateReport: GenerateReport,  
        PageLoad: PageLoad,
        EditReportDetails: EditReportDetails,
        UpdateReport: UpdateReport,
        GetReportSavedData: GetReportSavedData
    }

}();

declare let ReportHome: any;

ReportHome = function () {

    function LoadReportHomePage() {

        GetReportList();
    }

    function GetReportList() {

        $.ajax({
            url: api + 'GetReportList',           
            type: "GET",
            error: function () {
                //Handle the server errors using the approach from the previous example
            },
            success: function (data) {
                AppendRow(data);
            }
        })

    }

    function EditReport(ReportName) {
        window.location.href = "TIReport.aspx?ReportName=" + ReportName;
    }

    function AppendRow(data : any) {

        var x = <HTMLTableElement>document.getElementById('reportBody');
        // deep clone the targeted row

        for (let i = 0; i < data._reportList.length; i++) {
            
            var new_row: any = x.rows[0].cloneNode(true);

            new_row.cells[0].innerText = data._reportList[i].Report_Name;
            new_row.cells[1].innerText = data._reportList[i].Report_Description;

            let a1 = new_row.cells[2].getElementsByTagName('a')[0];
            a1.id = 'genReport' + data._reportList[i].Report_ID;

            let a2 = new_row.cells[3].getElementsByTagName('a')[0];
            a2.id = 'Report' + data._reportList[i].Report_ID;

            if (data.isCompTeam == false) {
                a2.style.display = "none";
            }

            new_row.style.display = "table-row";


            // append the new row to the table
            x.appendChild(new_row);
        }
    }

    function GenerateReport(ctlID) {

        let _reportName = ctlID.replace("gen", "");

        let _report = reportType[_reportName.replace(/\s/g, '')];

        window.location.href = "GenerateReport.aspx?reportId=" + _report.ReportID + "&criteriaType=Publish&fileName=" + _report.ReportName + ".xls";                               

        
    }


    return {
        LoadReportHomePage: LoadReportHomePage,
        EditReport: EditReport,     
        GenerateReport: GenerateReport
    }
}();

