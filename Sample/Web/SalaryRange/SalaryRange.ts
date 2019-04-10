
let salaryRangeApi = "/api/SalaryRange/";
let DeferralRuleId = 2;

$(document).ready(function () {

    salaryRangeApi = sitejs.UpdateAPIURL(salaryRangeApi);

    sitejs.UpdatePageName('Range Tool');

    if (window.location.href.indexOf("SalaryRangeSetup") > -1) {
        sitejs.UpdatePageName('Range Tool Setup');

        document.getElementById('inSalaryRange').addEventListener('change', RangeFileProcess, false);
        document.getElementById('inRangeUserAccess').addEventListener('change', UserFileProcess, false);

        return;
    }


    SalaryRange.GetOrg1();

});



function RangeFileProcess(evt) {
    debugger;

    var inFile = evt.target.files[0];

    var reader = new FileReader();
    reader.readAsText(inFile);

   

    setTimeout(function () {
        SalaryRangeSetup.SetFileContent(reader.result);
    },
        1000);

}


function UserFileProcess(evt) {

    debugger;

    var inFile = evt.target.files[0];

    var reader = new FileReader();
    reader.readAsText(inFile);

    setTimeout(function () {
        SalaryRangeSetup.SetUserFileContent(reader.result);
    },
    1000);

    
}

declare let SalaryRange: any;

SalaryRange = function () {

    
    function CompCalc() {

        var currSalary = isNaN(+(<HTMLInputElement>document.getElementById("txtCurrSalary")).value) ? 0 : (<HTMLInputElement>document.getElementById("txtCurrSalary")).value ;
        var currIncentive = isNaN(+(<HTMLInputElement>document.getElementById("txtCurrIncentive")).value) ? 0 : (<HTMLInputElement>document.getElementById("txtCurrIncentive")).value;
        var ProposedSalary = isNaN(+(<HTMLInputElement>document.getElementById("txtProposedSalary")).value) ? 0 : (<HTMLInputElement>document.getElementById("txtProposedSalary")).value;
        var ProposedIncentive = isNaN(+(<HTMLInputElement>document.getElementById("txtProposedIncentive")).value) ? 0 : (<HTMLInputElement>document.getElementById("txtProposedIncentive")).value;



        (<HTMLSpanElement>document.getElementById("spnCurrTC")).innerHTML = (+currSalary + +currIncentive).toLocaleString();
        (<HTMLSpanElement>document.getElementById("spnProposedTC")).innerHTML = (+ProposedSalary + +ProposedIncentive).toLocaleString();

        (<HTMLSpanElement>document.getElementById("spnSalaryIncreasePerc")).innerHTML = ((100 * (+ProposedSalary - +currSalary) / +currSalary).toFixed(2)).toString();
        (<HTMLSpanElement>document.getElementById("spnIncentiveIncreasePerc")).innerHTML = ((100 * (+ProposedIncentive - +currIncentive) / +currIncentive).toFixed(2)).toString();

        (<HTMLSpanElement>document.getElementById("spnTCIncreasePerc")).innerHTML = ((100 * ((+ProposedSalary + +ProposedIncentive) - (+currSalary + +currIncentive)) / (+currSalary + +currIncentive)).toFixed(2)).toString();

        
    }

    $('#lstCovered li').on('click', function (event) {
        event.preventDefault();

        let RuleID;

        $('#spnCovered').text($(this).text());

        RuleID = $('#spnCovered').text() == 'Yes - C2' ? 3 : 2;

        if (RuleID != DeferralRuleId) {
            DeferralRuleId = RuleID;
            CalculateCashEquitySplit();
        }        

     });

    function CalculateCashEquitySplit() {

        let New_Base_USD = 220000, Total_Incentive = 30000;

        
        New_Base_USD = isNaN(+$('#txtAnnualSalary').val()) ? 0: +$('#txtAnnualSalary').val()  ;
        Total_Incentive = isNaN(+$('#txtTotalIncentive').val()) ? 0 : +$('#txtTotalIncentive').val();

        $('#spnIncentiveCash').text("");
        $('#spnIncentiveDeferred').text("");
        $('#spnIncentiveCashPerc').text("");
        $('#spnIncentiveDeferredhPerc').text("");


        if (New_Base_USD > 0 && Total_Incentive > 0) {
            $.ajax({
                url: salaryRangeApi + 'GetCashEquitySplit?Rule_ID=' + DeferralRuleId + "&New_Base_USD=" + New_Base_USD + "&Total_Incentive=" + Total_Incentive,
                type: "GET",
                error: function () {
                    //Handle the server errors using the approach from the previous example
                },
                success: function (data) {
                    $('#spnIncentiveCash').text((+data.Cash_Incentive).toLocaleString());
                    $('#spnIncentiveDeferred').text((+data.Equity_Incentive).toLocaleString());
                    $('#spnIncentiveCashPerc').text(data.Cash_Perc);
                    $('#spnIncentiveDeferredhPerc').text(data.Equity_Perc);
                }
            })
        }


    }


    function GetOrg1() {

        $.ajax({
            url: salaryRangeApi + 'GetOrg1',
            type: "GET",
            error: function () {
                //Handle the server errors using the approach from the previous example
            },
            success: function (data) {

                AddDropDownItems('OrgLevel1', data, false);

                BindDropDown('OrgLevel1');

            }
        })

    }

    function GetOrg2() {

        $.ajax({
            url: salaryRangeApi + 'GetOrg2?OrgLevel1=' + $('#spnOrgLevel1').text(),
            type: "GET",
            error: function () {
                //Handle the server errors using the approach from the previous example
            },
            success: function (data) {

                AddDropDownItems('OrgLevel2', data, false);

                BindDropDown('OrgLevel2');

            }
        })

    }

    function GetOrg3() {

        
        $.ajax({
            url: salaryRangeApi + 'GetOrg3?OrgLevel1=' + $('#spnOrgLevel1').text() + '&OrgLevel2=' + encodeURIComponent( $('#spnOrgLevel2').text()),
            type: "GET",
            error: function (e) {
                //Handle the server errors using the approach from the previous example

                debugger;
            },
            success: function (data) {

                AddDropDownItems('OrgLevel3', data, true);

                BindDropDown('OrgLevel3');

                if (data.length <= 1) {

                    GetGrades();
                }

            }
        });

    }

    function GetJobFamily() {

        let strOrg3 = $('#spnOrgLevel3').text().indexOf("Select") > -1 ? "" : encodeURIComponent($('#spnOrgLevel3').text());
        let strGrade = $('#spnGrade').text().indexOf("Select") > -1 ? "" : $('#spnGrade').text();

        //let _url = encodeURIComponent(salaryRangeApi +  'GetJobSubFamily?OrgLevel1=' + $('#spnOrgLevel1').text() + '&OrgLevel2=' + $('#spnOrgLevel2').text() + '&OrgLevel3=' + strOrg3)

        $.ajax({
            url: salaryRangeApi + 'GetJobFamily?OrgLevel1=' + $('#spnOrgLevel1').text() + '&OrgLevel2=' + encodeURIComponent($('#spnOrgLevel2').text()) + '&OrgLevel3=' + strOrg3 + '&Grade=' + strGrade,
            type: "GET",
            error: function (e) {
                //Handle the server errors using the approach from the previous example

                debugger;
            },
            success: function (data) {

                AddDropDownItems('JobFamily', data);

                BindDropDown('JobFamily');

            }
        })

    }

    function GetJobSubFamily() {

        let strOrg3 = $('#spnOrgLevel3').text().indexOf("Select") > -1 ? "" : encodeURIComponent($('#spnOrgLevel3').text());
        let strGrade = $('#spnGrade').text().indexOf("Select") > -1 ? "" : $('#spnGrade').text();
        let strJobFamily = $('#spnJobFamily').text().indexOf("Select") > -1 ? "" : $('#spnJobFamily').text();

        //let _url = encodeURIComponent(salaryRangeApi +  'GetJobSubFamily?OrgLevel1=' + $('#spnOrgLevel1').text() + '&OrgLevel2=' + $('#spnOrgLevel2').text() + '&OrgLevel3=' + strOrg3)

        $.ajax({
            url: salaryRangeApi + 'GetJobSubFamily?OrgLevel1=' + $('#spnOrgLevel1').text() + '&OrgLevel2=' + encodeURIComponent($('#spnOrgLevel2').text()) + '&OrgLevel3=' + strOrg3 + '&Grade=' + strGrade + '&JobFamily=' + strJobFamily,
            type: "GET",
            error: function (e) {
                //Handle the server errors using the approach from the previous example

                debugger;
            },
            success: function (data) {

                AddDropDownItems('JobSubFamily', data);

                BindDropDown('JobSubFamily');

            }
        })

    }

    function GetGrades() {

        let strOrgLevel3 = $('#spnOrgLevel3').text().indexOf("Select") > -1 ? "" : encodeURIComponent($('#spnOrgLevel3').text());

        $.ajax({
            url: salaryRangeApi + 'GetGrade?OrgLevel1=' + $('#spnOrgLevel1').text() + '&OrgLevel2=' + encodeURIComponent($('#spnOrgLevel2').text()) + '&OrgLevel3=' + strOrgLevel3,
            type: "GET",
            error: function () {
                //Handle the server errors using the approach from the previous example
            },
            success: function (data) {

                AddDropDownItems('Grade', data);

                BindDropDown('Grade');

            }
        })

    }

    function GetSalaryRange() {

        let strOrgLevel1 = $('#spnOrgLevel1').text().indexOf("Select") > -1 ? "" : $('#spnOrgLevel1').text();

        let strOrgLevel2 = $('#spnOrgLevel2').text().indexOf("Select") > -1 ? "" : encodeURIComponent($('#spnOrgLevel2').text());
        let strOrgLevel3 = $('#spnOrgLevel3').text().indexOf("Select") > -1 ? "" : encodeURIComponent($('#spnOrgLevel3').text());
        let strJobFamily = $('#spnJobFamily').text().indexOf("Select") > -1 ? "" : $('#spnJobFamily').text();
        let strJobSubFamily = $('#spnJobSubFamily').text().indexOf("Select") > -1 ? "" : $('#spnJobSubFamily').text();
        let strGrade = $('#spnGrade').text().indexOf("Select") > -1 ? "" : $('#spnGrade').text();
        let strLocation = $('#spnLocation').text().indexOf("Select") > -1 ? "" : $('#spnLocation').text();
        let strTitle = $('#spnTitle').text().indexOf("Select") > -1 ? "" : $('#spnTitle').text();

        if (strOrgLevel1 != "" && strOrgLevel2 != "" && strJobSubFamily != "" && strGrade != "") {
            $.ajax({
                url: salaryRangeApi + 'GetSalaryRange?OrgLevel1=' + strOrgLevel1 + '&OrgLevel2=' + strOrgLevel2 + '&OrgLevel3=' + strOrgLevel3 + '&JobFamily=' + strJobFamily + '&JobSubFamily=' + strJobSubFamily + '&Grade=' + strGrade
                     + '&Location=' + strLocation + '&Title=' + strTitle,
                type: "GET",
                error: function (e) {
                    //Handle the server errors using the approach from the previous example

                    debugger;
                },
                success: function (data) {

                    $('#tdMinAnnualSalary').text((+data.Salary_Range_Minimum).toLocaleString());
                    $('#tdMidAnnualSalary').text((+data.Salary_Range_Midpoint).toLocaleString());
                    $('#tdMaxAnnualSalary').text((+data.Salary_Range_Maximum).toLocaleString());
                    $('#tdMinTC').text((+data.TC_Range_Minimum).toLocaleString());
                    $('#tdMidTC').text((+data.TC_Range_Midpoint).toLocaleString());
                    $('#tdMaxTC').text((+data.TC_Range_Maximum).toLocaleString());

                    if (data.TC_Min_Tier != "" || data.TC_Midpoint_Tier != "" || data.TC_Max_Tier != "") {

                        $('#tdMinAnnualSalary').text('*');
                        $('#tdMidAnnualSalary').text('*');
                        $('#tdMaxAnnualSalary').text('*');
                        $('#tdAnnualSalary').text("Annual Salary*");
                        $('.trApplicableGrid').show();

                        $('#tdSalaryTierMin').text(data.SalaryTierMin);
                        $('#tdSalaryTierMid').text(data.SalaryTierMid);
                        $('#tdSalaryTierMax').text(data.SalaryTierMax);

                        $('#tdSalary1Min').text((+data.Salary1Min).toLocaleString());
                        $('#tdSalary2Min').text((+data.Salary2Min).toLocaleString());

                        $('#tdSalary1Mid').text((+data.Salary1Mid).toLocaleString());
                        $('#tdSalary2Mid').text((+data.Salary2Mid).toLocaleString());

                        $('#tdSalary1Max').text((+data.Salary1Max).toLocaleString());
                        $('#tdSalary2Max').text((+data.Salary2Max).toLocaleString());

                        $('#tdMinIncentive').text('*');
                        $('#tdMidIncentive').text('*');
                        $('#tdMaxIncentive').text('*');


                        return;
                    }

                    $('#tdAnnualSalary').text("Annual Salary");
                    $('.trApplicableGrid').hide();


                    $('#tdMinIncentive').text((+data.TC_Range_Minimum - +data.Salary_Range_Minimum).toLocaleString());
                    $('#tdMidIncentive').text((+data.TC_Range_Midpoint - +data.Salary_Range_Midpoint).toLocaleString());
                    $('#tdMaxIncentive').text((+data.TC_Range_Maximum - +data.Salary_Range_Maximum).toLocaleString());




                }
            })
        }

    }

    function GetLocation() {

        let strOrgLevel1 = $('#spnOrgLevel1').text().indexOf("Select") > -1 ? "" : $('#spnOrgLevel1').text();

        let strOrgLevel2 = $('#spnOrgLevel2').text().indexOf("Select") > -1 ? "" : encodeURIComponent($('#spnOrgLevel2').text());
        let strOrgLevel3 = $('#spnOrgLevel3').text().indexOf("Select") > -1 ? "" : encodeURIComponent($('#spnOrgLevel3').text());
        let strJobFamily = $('#spnJobFamily').text().indexOf("Select") > -1 ? "" : $('#spnJobFamily').text();
        let strJobSubFamily = $('#spnJobSubFamily').text().indexOf("Select") > -1 ? "" : $('#spnJobSubFamily').text();
        let strGrade = $('#spnGrade').text().indexOf("Select") > -1 ? "" : $('#spnGrade').text();

       
        $.ajax({
            url: salaryRangeApi + 'GetLocation?OrgLevel1=' + strOrgLevel1 + '&OrgLevel2=' + strOrgLevel2 + '&OrgLevel3=' + strOrgLevel3 +  '&Grade=' + strGrade + '&JobFamily=' + strJobFamily + '&JobSubFamily=' + strJobSubFamily ,
                type: "GET",
                error: function (e) {
                    //Handle the server errors using the approach from the previous example

                    debugger;
                },
                success: function (data) {

                    AddDropDownItems('Location', data,true);

                    BindDropDown('Location');

                    if (data.length > 0) {
                        $('#trLocTitle').show();
                    }

                }
            })
    }

    function GetTitle() {

        let strOrgLevel1 = $('#spnOrgLevel1').text().indexOf("Select") > -1 ? "" : $('#spnOrgLevel1').text();

        let strOrgLevel2 = $('#spnOrgLevel2').text().indexOf("Select") > -1 ? "" : encodeURIComponent($('#spnOrgLevel2').text());
        let strOrgLevel3 = $('#spnOrgLevel3').text().indexOf("Select") > -1 ? "" : encodeURIComponent($('#spnOrgLevel3').text());
        let strJobFamily = $('#spnJobFamily').text().indexOf("Select") > -1 ? "" : $('#spnJobFamily').text();
        let strJobSubFamily = $('#spnJobSubFamily').text().indexOf("Select") > -1 ? "" : $('#spnJobSubFamily').text();
        let strGrade = $('#spnGrade').text().indexOf("Select") > -1 ? "" : $('#spnGrade').text();


        $.ajax({
            url: salaryRangeApi + 'GetTitle?OrgLevel1=' + strOrgLevel1 + '&OrgLevel2=' + strOrgLevel2 + '&OrgLevel3=' + strOrgLevel3 + '&Grade=' + strGrade + '&JobFamily=' + strJobFamily + '&JobSubFamily=' + strJobSubFamily,
            type: "GET",
            error: function (e) {
                //Handle the server errors using the approach from the previous example

                debugger;
            },
            success: function (data) {

                AddDropDownItems('Title', data, true);

                BindDropDown('Title');

                if (data.length > 0) {
                    $('#trLocTitle').show();
                }

            }
        })
    }

    function ResetRange() {

        location.reload();

    }

    function ResetDropDowns(lstDrpDowns: string[]) {

        let i = 0;

        let OptionalDrpDowns = ['OrgLevel3',"Location","Title"];

        for (i = 0; i < lstDrpDowns.length; i++) {

            var ul = document.getElementById("lst" + lstDrpDowns[i]);
            var spn = document.getElementById("spn" + lstDrpDowns[i]);
            var lbl = document.getElementById("lbl" + lstDrpDowns[i]);

            ul.innerHTML = "";
            spn.innerText = "Select " + lbl.innerText.replace(":","");

            if (OptionalDrpDowns.indexOf(lstDrpDowns[i]) > -1) {

                $('#lbl' + lstDrpDowns[i]).hide();
                $('#drp' + lstDrpDowns[i]).hide();
                $('#btn' + lstDrpDowns[i]).hide();

                $('#trLocTitle').hide();
            }
        }

        $('#tdMinAnnualSalary').text("");
        $('#tdMidAnnualSalary').text("");
        $('#tdMaxAnnualSalary').text("");
        $('#tdMinTC').text("");
        $('#tdMidTC').text("");
        $('#tdMaxTC').text("");
        $('#tdAnnualSalary').text("Annual Salary");
        $('#tdMinIncentive').text("");
        $('#tdMidIncentive').text("");
        $('#tdMaxIncentive').text("");
        $('.trApplicableGrid').hide();

    }


    function AddDropDownItems(ulId, lstItems: string[], isOptional :boolean = false) {

        var ul = document.getElementById('lst' + ulId);

        ul.innerHTML = "";

        let i = 0;

        for (i = 0; i < lstItems.length; i++) {
            var li = document.createElement("li");
            var a = document.createElement("a");
            a.text = lstItems[i];
            a.setAttribute("href", "#");
            li.appendChild(a);
            ul.appendChild(li);
        }

        if (isOptional) {
            if (lstItems.length > 0) {
                $('#lbl' + ulId).show();
                $('#drp' + ulId).show();
                $('#btn' + ulId).show();
            } else {
                $('#lbl' + ulId).hide();
                $('#drp' + ulId).hide();
                $('#btn' + ulId).hide();
             }
        }

    }

    function BindDropDown(drpDwn){
        $('#lst'+drpDwn  +' li').on('click', function (event) {
            event.preventDefault();
            $('#spn' + drpDwn).text($(this).text());

            GetChildDropDownItems(drpDwn);

        });
    }

    function GetChildDropDownItems(drpDwn){
        switch(drpDwn)
        {
            case 'OrgLevel1':
                ResetDropDowns(['OrgLevel2', 'OrgLevel3', 'JobSubFamily', 'Grade']);
                GetOrg2();
                break;
            case 'OrgLevel2':
                ResetDropDowns(['OrgLevel3', 'JobSubFamily', 'Grade']);
                GetOrg3();
                break;
            case 'OrgLevel3':
                ResetDropDowns(['JobFamily', 'JobSubFamily', 'Grade']);
                GetGrades();
                break;
            case 'Grade':
                ResetDropDowns(['JobFamily', 'JobSubFamily', 'Location', 'Title']);
                GetJobFamily();
                GetJobSubFamily();                
                //GetLocation();
                //GetTitle();
                break;
            case 'JobFamily':
                ResetDropDowns(['JobSubFamily', 'Location', 'Title']);
                GetJobSubFamily();                
                //GetLocation();
                //GetTitle();
                break;
            case 'JobSubFamily':
                ResetDropDowns(['Location', 'Title']);
                GetLocation();
                GetTitle();
                break;
            default:
                break;
        }
    }

    return {
        CompCalc: CompCalc,
        CalculateCashEquitySplit: CalculateCashEquitySplit,
        GetOrg1: GetOrg1,
        GetSalaryRange: GetSalaryRange,
        ResetRange: ResetRange
    }
}();

declare let SalaryRangeSetup: any;

SalaryRangeSetup = function() {

    let fileContent = "";
    let userFileContent = "";

    function UploadFile(fileType: string) {

        debugger;

        var data = {
            FileContent: fileType == 'SalaryRange'?  fileContent : userFileContent,
            FileType: fileType
        }

        if (!data.FileContent) {

            sitejs.ShowAlert("Please Select file to Upload", sitejs.msgType.Error);

            return;

        }

        $.ajax({
            url: salaryRangeApi + 'UploadFile',
            type: "POST",
            data: data,
            error: function (e) {
                //Handle the server errors using the approach from the previous example

                sitejs.ShowAlert("Unable to Update Salary Range Data. Please Try after some time" + e.responseText , sitejs.msgType.Error);
            },
            success: function (data) {
                sitejs.ShowAlert("Salary Range Data Uploaded Successfully", sitejs.msgType.Success);
            }
        })



    };

    function DelayedUpload(fileType: string) {
        setTimeout(function () {
            UploadFile(fileType);
        }, 1000)
    }


    function DownloadFile(fileType: string) {

        window.location.href = "GenerateDownload.aspx?fileType=" + fileType;                               
    }

    function SetFileContent(strdata:string) {

        fileContent = strdata;

    }

    function SetUserFileContent(strdata: string) {

        userFileContent = strdata;

    }

    return {
        UploadFile : UploadFile,
        DownloadFile: DownloadFile,
        SetFileContent: SetFileContent,
        SetUserFileContent: SetUserFileContent,
        DelayedUpload: DelayedUpload
    }


} ();