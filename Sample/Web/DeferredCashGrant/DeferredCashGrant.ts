
$(document).ready(function () {

    statementJs.GetStatementData();

});

let statementJs = function () {

    let api = "/api/Statement/";

    api = sitejs.UpdateAPIURL(api);


interface Statement {
        UID: number;
        Key:string;
        Value: string;
}

    function GetStatementData() {

        $.ajax({
            url: api + 'GetStatementData?UID=4',
            type: "GET",
            error: function (x) {
                //Handle the server errors using the approach from the previous example

                sitejs.ShowAlert("Unable to Retrieve Statement Data. Please Try after some time", sitejs.msgType.Error);
            },
            success: function (data) {
                LoadStatementDetails(data);
            }
        });

    }

    function UpdateStatementData() {

        let data = GetFieldValues();

        $.ajax({
            url: api + 'UpdateCashDeferredStatementData',
            data: data,
            type: "POST",
            error: function (x) {
                //Handle the server errors 

                sitejs.ShowAlert("Unable to Update Statement Data. Please Try after some time", sitejs.msgType.Error);
            },
            success: function () {

                sitejs.ShowAlert("Data Updated Successfully", sitejs.msgType.Success);
            }
        })

    }

    function LoadStatementDetails(data:Statement[]) {

        (<HTMLInputElement>document.getElementById("txtHeader")).value = data.filter(m => m.Key == 'txtHeader')[0].Value;
        (<HTMLInputElement>document.getElementById("txtHeaderNote")).value = data.filter(m => m.Key == 'txtHeaderNote')[0].Value;

        (<HTMLInputElement>document.getElementById("txtPaymentPeriod")).value = data.filter(m => m.Key == 'txtPaymentPeriod')[0].Value;
        (<HTMLInputElement>document.getElementById("txtScheduledVesting")).value = data.filter(m => m.Key == 'txtScheduledVesting')[0].Value;
        (<HTMLInputElement>document.getElementById("txtIntDurVesting")).value = data.filter(m => m.Key == 'txtIntDurVesting')[0].Value;

        (<HTMLInputElement>document.getElementById("txtIntroduction")).value = data.filter(m => m.Key == 'txtIntroduction')[0].Value;

        (<HTMLInputElement>document.getElementById("txtTerminationResaon1")).value = data.filter(m => m.Key == 'txtTerminationResaon1')[0].Value;
        (<HTMLInputElement>document.getElementById("txtUnvestedDeferredCash1")).value = data.filter(m => m.Key == 'txtUnvestedDeferredCash1')[0].Value; 
        
        (<HTMLInputElement>document.getElementById("txtTerminationResaon2")).value = data.filter(m => m.Key == 'txtTerminationResaon2')[0].Value;
        (<HTMLInputElement>document.getElementById("txtUnvestedDeferredCash2")).value = data.filter(m => m.Key == 'txtUnvestedDeferredCash2')[0].Value; 

        (<HTMLInputElement>document.getElementById("txtTerminationResaon3")).value =data.filter(m => m.Key == 'txtTerminationResaon3')[0].Value;
        (<HTMLInputElement>document.getElementById("txtUnvestedDeferredCash3")).value = data.filter(m => m.Key == 'txtUnvestedDeferredCash3')[0].Value; 

        (<HTMLInputElement>document.getElementById("txtTerminationResaon4")).value = data.filter(m => m.Key == 'txtTerminationResaon4')[0].Value;
        (<HTMLInputElement>document.getElementById("txtUnvestedDeferredCash4")).value = data.filter(m => m.Key == 'txtUnvestedDeferredCash4')[0].Value; 

        (<HTMLInputElement>document.getElementById("txtNonCompetition")).value = data.filter(m => m.Key == 'txtNonCompetition')[0].Value;
        (<HTMLInputElement>document.getElementById("txtNonSolicitation")).value = data.filter(m => m.Key == 'txtNonSolicitation')[0].Value; 
        (<HTMLInputElement>document.getElementById("txtInteraction")).value = data.filter(m => m.Key == 'txtInteraction')[0].Value;
        (<HTMLInputElement>document.getElementById("txtCancellation")).value = data.filter(m => m.Key == 'txtCancellation')[0].Value;
        (<HTMLInputElement>document.getElementById("txtRegRequirement")).value = data.filter(m => m.Key == 'txtRegRequirement')[0].Value; 
        (<HTMLInputElement>document.getElementById("txtFootNote")).value = data.filter(m => m.Key == 'txtFootNote')[0].Value;
        (<HTMLInputElement>document.getElementById("txtFooter")).value = data.filter(m => m.Key == 'txtFooter')[0].Value;
                
    }


    function GetFieldValues() {

        let data: any = {};

        //txtIntroduction, txtTerminationResaon1, txtUnvestedDeferredCash1, txtTerminationResaon2, txtUnvestedDeferredCash2, txtTerminationResaon3, txtUnvestedDeferredCash3, txtTerminationResaon4, txtUnvestedDeferredCash4, txtNonCompetition, txtNonSoliciation, txtInteraction, txtCancellation, txtRegRequirement, txtFootNote

        data.txtHeader = (<HTMLInputElement>document.getElementById("txtHeader")).value;
        data.txtHeaderNote = (<HTMLInputElement>document.getElementById("txtHeaderNote")).value;

        data.txtPaymentPeriod = (<HTMLInputElement>document.getElementById("txtPaymentPeriod")).value;
        data.txtScheduledVesting = (<HTMLInputElement>document.getElementById("txtScheduledVesting")).value;
        data.txtIntDurVesting = (<HTMLInputElement>document.getElementById("txtIntDurVesting")).value;

        data.txtIntroduction = (<HTMLInputElement>document.getElementById("txtIntroduction")).value;
        data.txtTerminationResaon1 = (<HTMLInputElement>document.getElementById("txtTerminationResaon1")).value;
        data.txtUnvestedDeferredCash1 = (<HTMLInputElement>document.getElementById("txtUnvestedDeferredCash1")).value;


        data.txtTerminationResaon2 = (<HTMLInputElement>document.getElementById("txtTerminationResaon2")).value;
        data.txtUnvestedDeferredCash2 = (<HTMLInputElement>document.getElementById("txtUnvestedDeferredCash2")).value;
        data.txtTerminationResaon3 = (<HTMLInputElement>document.getElementById("txtTerminationResaon3")).value;
        data.txtUnvestedDeferredCash3 = (<HTMLInputElement>document.getElementById("txtUnvestedDeferredCash3")).value;
        data.txtTerminationResaon4 = (<HTMLInputElement>document.getElementById("txtTerminationResaon4")).value;
        data.txtUnvestedDeferredCash4 = (<HTMLInputElement>document.getElementById("txtUnvestedDeferredCash4")).value;
        data.txtNonCompetition = (<HTMLInputElement>document.getElementById("txtNonCompetition")).value;
        data.txtNonSolicitation = (<HTMLInputElement>document.getElementById("txtNonSolicitation")).value;
        data.txtInteraction = (<HTMLInputElement>document.getElementById("txtInteraction")).value;
        data.txtCancellation = (<HTMLInputElement>document.getElementById("txtCancellation")).value;
        data.txtRegRequirement = (<HTMLInputElement>document.getElementById("txtRegRequirement")).value;
        data.txtFootNote = (<HTMLInputElement>document.getElementById("txtFootNote")).value;
        data.txtFooter = (<HTMLInputElement>document.getElementById("txtFooter")).value;


        return data;
    }

    return {
        UpdateStatementData: UpdateStatementData,
        GetStatementData: GetStatementData
    }

}();