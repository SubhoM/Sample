var api = "/api/Reports/";
var reportType = {
    Report1: { ReportID: 1, ReportName: 'TI Report', Fields: ["IncentivePlan", "Eligibility", "TotalIncentive", "PerfRating"] },
    Report2: { ReportID: 2, ReportName: 'PTIE Adjustments', Fields: ["IncentivePlan", "Eligibility", "TotalIncentive", "PerfRating", "PopulationType"] },
    Report3: { ReportID: 3, ReportName: 'Promo Grade', Fields: [] },
    Report4: { ReportID: 4, ReportName: 'Rating 4/5 - TI or Merit', Fields: ["IncentivePlan", "PerfRating", "Eligibility"] },
    Report5: { ReportID: 5, ReportName: 'Off Cycle  + Merit', Fields: ["SalaryIncreaseType", "PerfRating", "Eligibility"] },
    Report6: { ReportID: 6, ReportName: 'C2 Employee Missing Use of Discretion Forms', Fields: ["Eligibility", "Covered", "C2RiskForm", "IncentivePlan"] },
    Report7: { ReportID: 7, ReportName: 'Sal <25P with No Merit', Fields: ["PerfRating", "Eligibility", "MktSalPosition"] },
    Report8: { ReportID: 8, ReportName: 'Significant YOY Changes', Fields: ["PerfRating", "Eligibility", "CompPercentageChange"] },
    Report9: { ReportID: 9, ReportName: 'Missing Ratings', Fields: ["Status", "Eligibility", "PerfRating"] },
    Report10: { ReportID: 10, ReportName: 'LOA Flag', Fields: ["PopulationType", "PerfRating", "Eligibility"] },
    Report11: { ReportID: 11, ReportName: 'Hourly Rate', Fields: ["PerfRating", "Eligibility", "HourlyRate"] },
    Report12: { ReportID: 12, ReportName: '2019 Pay Mix Shifts', Fields: ["IncentivePlan", "Eligibility", "PopulationType", "SalaryIncreaseType"] }
};
var _report;
$(document).ready(function () {
    $('#displayloading').show();
    var pageName;
    var reportName = sitejs.getParameterByName("ReportName");
    api = sitejs.UpdateAPIURL(api);
    if (!reportName) {
        sitejs.UpdatePageName('Reports');
        ReportHome.LoadReportHomePage();
        return;
    }
    _report = reportType[reportName.replace(/\s/g, '')];
    ReportObj.PageLoad();
    $('#pnlCriteria').show();
    $('#displayloading').hide();
});
ReportObj = function () {
    var TabID = 1;
    var lstFields = ["IncentivePlan", "Eligibility", "TotalIncentive", "PerfRating", "PopulationType", "Status", "SalaryIncreaseType", "Covered", "C2RiskForm", "CompPercentageChange", "HourlyRate", "MktSalPosition"];
    function PageLoad() {
        $('#txtRptName').hide();
        $('#txtRptDescription').hide();
        GetReportInfo();
        BindDropDowns();
        GetReportSavedData();
    }
    function BindDropDowns() {
        var fieldName;
        var _loop_1 = function (i) {
            fieldName = lstFields[i];
            var checkField = _report.Fields.filter(function (m) { return m == fieldName; });
            if (checkField.length == 0) {
                $('#div' + fieldName).hide();
                return "continue";
            }
            $('#lst' + fieldName + ' li').on('click', function (event) {
                event.stopPropagation();
                multiSelectClick(lstFields[i], $(this));
            });
        };
        for (var i = 0; i < lstFields.length; i++) {
            _loop_1(i);
        }
    }
    function multiSelectClick(fieldName, currItem) {
        var newText = $('#spn' + fieldName).text().indexOf("Select") > -1 ? "" : $('#spn' + fieldName).text();
        var chkItem = currItem.find(':checkbox').first().is(':checked');
        var selText = currItem.text();
        if (chkItem) {
            if (newText.indexOf(selText) == -1)
                newText = newText + selText + ', ';
        }
        else {
            newText = newText.replace(selText + ", ", '');
        }
        newText = newText.trim() === '' ? 'Select ' + $('#lbl' + fieldName).text() : newText;
        $('#spn' + fieldName).text(newText);
    }
    function GetData(_tabID) {
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
            },
            success: function (data) {
                $('#spnRptName').text(data.ReportName);
                $('#spnRptDescription').text(data.ReportDescription);
                sitejs.UpdatePageName(data.ReportName);
            }
        });
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
    function UpdateReport(cnfrmUpdate) {
        $('#Warning-alert').hide();
        $('#Error-alert').hide();
        if (cnfrmUpdate && ($('#txtRptName').val().trim() == "" || $('#txtRptDescription').val().trim() == "")) {
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
    function dbUpdateReportInfo(ReportName, ReportDescription) {
        var data = {
            Report_ID: _report.ReportID,
            Report_Name: ReportName,
            Report_Description: ReportDescription
        };
        $.ajax({
            url: api + 'UpdateReportInfo',
            type: "POST",
            data: data,
            error: function () {
            },
            success: function (data) {
                sitejs.ShowAlert("Data Updated Successfully", sitejs.msgType.Success);
            }
        });
    }
    function GetReportSavedData() {
        $.ajax({
            url: api + 'GetReportSavedData?ReportID=' + _report.ReportID,
            type: "GET",
            error: function () {
            },
            success: function (data) {
                BindData(data);
            }
        });
    }
    function BindData(savedData) {
        var chkHRTeam = document.getElementById("chkHRTeam");
        chkHRTeam.checked = savedData.HR_Access ? true : false;
        var chkPlanningManager = document.getElementById("chkPlanningManager");
        chkPlanningManager.checked = savedData.Mgr_Access ? true : false;
        var spnUpdateInfo = document.getElementById("spnUpdateInfo");
        spnUpdateInfo.innerText = savedData.UpdatedByUserName ? spnUpdateInfo.innerText.replace("{{UserName}}", savedData.UpdatedByUserName).replace("{{Date}}", savedData.Updated_Date) : "";
        if (savedData.Incentive_Plan)
            document.getElementById("spnIncentivePlan").innerText = AddCommaIfRequired(savedData.Incentive_Plan);
        if (savedData.Bonus_Eligible)
            document.getElementById("spnEligibility").innerText = AddCommaIfRequired(savedData.Bonus_Eligible);
        if (savedData.Status)
            document.getElementById("spnStatus").innerText = AddCommaIfRequired(savedData.Status);
        if (savedData.Perf_Rtg)
            document.getElementById("spnPerfRating").innerText = AddCommaIfRequired(savedData.Perf_Rtg);
        if (savedData.Population_Type)
            document.getElementById("spnPopulationType").innerText = AddCommaIfRequired(savedData.Population_Type);
        if (savedData.Merit_Promotion_Flag)
            document.getElementById("spnSalaryIncreaseType").innerText = AddCommaIfRequired(savedData.Merit_Promotion_Flag);
        if (savedData.Covered)
            document.getElementById("spnCovered").innerText = AddCommaIfRequired(savedData.Covered);
        if (savedData.C2_Risk_Form)
            document.getElementById("spnC2RiskForm").innerText = AddCommaIfRequired(savedData.C2_Risk_Form);
        if (savedData.Total_Comp_Chg_YOY)
            document.getElementById("spnCompPercentageChange").innerText = AddCommaIfRequired(savedData.Total_Comp_Chg_YOY);
        if (savedData.Min_Hourly_Rate)
            document.getElementById("txtHourlyRate").value = savedData.Min_Hourly_Rate;
        if (savedData.Internal_TC_Position)
            document.getElementById("spnMktSalPosition").innerText = savedData.Internal_TC_Position;
        spnUpdateInfo.hidden = false;
        for (var i = 0; i < lstFields.length; i++) {
            UpdateCriteriaCheckBoxBySavedData(lstFields[i]);
        }
    }
    function AddCommaIfRequired(inputText) {
        var checkComma = inputText.substring(inputText.length - 2);
        if (checkComma.trim() != ",")
            inputText = inputText + ", ";
        return inputText;
    }
    function UpdateCriteriaCheckBoxBySavedData(fieldName) {
        var lstItems = $('#lst' + fieldName + ' li');
        if (!lstItems)
            return;
        var savedTextArr = $('#spn' + fieldName).text().split(",");
        var _loop_2 = function (i) {
            var currItemText = lstItems[i].innerText;
            var checkInSavedText = savedTextArr.filter(function (m) { return m.trim() == currItemText.trim(); });
            if (checkInSavedText.length == 0)
                return "continue";
            $(lstItems[i]).find(':checkbox')[0].checked = true;
        };
        for (var i = 0; i < lstItems.length; i++) {
            _loop_2(i);
        }
    }
    function PublishReport() {
        var data = GetFieldValues();
        $.ajax({
            url: api + 'PublishReport',
            data: data,
            type: "POST",
            error: function () {
            },
            success: function () {
                sitejs.ShowAlert("Data Updated Successfully", sitejs.msgType.Success);
            }
        });
    }
    function GetFieldValues() {
        var data = {};
        data.Report_ID = _report.ReportID;
        data.HR_Access = document.getElementById("chkHRTeam").checked;
        data.Mgr_Access = document.getElementById("chkPlanningManager").checked;
        var incentivePlan = document.getElementById("spnIncentivePlan").innerText;
        data.Incentive_Plan = incentivePlan.indexOf("Select") > -1 ? null : incentivePlan;
        var eligibility = document.getElementById("spnEligibility").innerText;
        data.Bonus_Eligible = eligibility.indexOf("Select") > -1 ? null : eligibility;
        var perfRating = document.getElementById("spnPerfRating").innerText;
        data.Perf_Rtg = perfRating.indexOf("Select") > -1 ? null : perfRating;
        var populationType = document.getElementById("spnPopulationType").innerText;
        data.Population_Type = populationType.indexOf("Select") > -1 ? null : populationType;
        var salaryIncreaseType = document.getElementById("spnSalaryIncreaseType").innerText;
        data.Merit_Promotion_Flag = salaryIncreaseType.indexOf("Select") > -1 ? null : salaryIncreaseType;
        var covered = document.getElementById("spnCovered").innerText;
        data.Covered = covered.indexOf("Select") > -1 ? null : covered;
        var C2RiskForm = document.getElementById("spnC2RiskForm").innerText;
        data.C2_Risk_Form = C2RiskForm.indexOf("Select") > -1 ? null : C2RiskForm;
        var CompPercentageChange = document.getElementById("spnCompPercentageChange").innerText;
        data.Total_Comp_Chg_YOY = CompPercentageChange.indexOf("Select") > -1 ? null : CompPercentageChange;
        var MinHourlyRate = document.getElementById("txtHourlyRate").value;
        data.Min_Hourly_Rate = MinHourlyRate != '' ? MinHourlyRate : null;
        var MktSalPosition = document.getElementById("spnMktSalPosition").innerText;
        data.Internal_TC_Position = MktSalPosition.indexOf("Select") > -1 ? null : MktSalPosition;
        return data;
    }
    function GenerateReport(id) {
        var data = GetFieldValues();
        $.ajax({
            url: api + 'UpdateCriteriaAndGenerateReport',
            data: data,
            type: "POST",
            error: function () {
                sitejs.ShowAlert("Unable to get Report Data. Please Try after some time", sitejs.msgType.Error);
            },
            success: function () {
                window.location.href = "GenerateReport.aspx?reportId=" + _report.ReportID + "&criteriaType=Generate&fileName=" + _report.ReportName + ".xls";
            }
        });
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
    };
}();
ReportHome = function () {
    function LoadReportHomePage() {
        GetReportList();
    }
    function GetReportList() {
        $.ajax({
            url: api + 'GetReportList',
            type: "GET",
            error: function () {
            },
            success: function (data) {
                AppendRow(data);
            }
        });
    }
    function EditReport(ReportName) {
        window.location.href = "TIReport.aspx?ReportName=" + ReportName;
    }
    function AppendRow(data) {
        var x = document.getElementById('reportBody');
        for (var i = 0; i < data._reportList.length; i++) {
            var new_row = x.rows[0].cloneNode(true);
            new_row.cells[0].innerText = data._reportList[i].Report_Name;
            new_row.cells[1].innerText = data._reportList[i].Report_Description;
            var a1 = new_row.cells[2].getElementsByTagName('a')[0];
            a1.id = 'genReport' + data._reportList[i].Report_ID;
            var a2 = new_row.cells[3].getElementsByTagName('a')[0];
            a2.id = 'Report' + data._reportList[i].Report_ID;
            if (data.isCompTeam == false) {
                a2.style.display = "none";
            }
            new_row.style.display = "table-row";
            x.appendChild(new_row);
        }
    }
    function GenerateReport(ctlID) {
        var _reportName = ctlID.replace("gen", "");
        var _report = reportType[_reportName.replace(/\s/g, '')];
        window.location.href = "GenerateReport.aspx?reportId=" + _report.ReportID + "&criteriaType=Publish&fileName=" + _report.ReportName + ".xls";
    }
    return {
        LoadReportHomePage: LoadReportHomePage,
        EditReport: EditReport,
        GenerateReport: GenerateReport
    };
}();
//# sourceMappingURL=Reports.js.map