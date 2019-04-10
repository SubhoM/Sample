$(document).ready(function () {
    statementJs.GetStatementData();
});
var statementJs = function () {
    var api = "/api/Statement/";
    api = sitejs.UpdateAPIURL(api);
    function GetStatementData() {
        $.ajax({
            url: api + 'GetStatementData?UID=4',
            type: "GET",
            error: function (x) {
                sitejs.ShowAlert("Unable to Retrieve Statement Data. Please Try after some time", sitejs.msgType.Error);
            },
            success: function (data) {
                LoadStatementDetails(data);
            }
        });
    }
    function UpdateStatementData() {
        var data = GetFieldValues();
        $.ajax({
            url: api + 'UpdateCashDeferredStatementData',
            data: data,
            type: "POST",
            error: function (x) {
                sitejs.ShowAlert("Unable to Update Statement Data. Please Try after some time", sitejs.msgType.Error);
            },
            success: function () {
                sitejs.ShowAlert("Data Updated Successfully", sitejs.msgType.Success);
            }
        });
    }
    function LoadStatementDetails(data) {
        document.getElementById("txtHeader").value = data.filter(function (m) { return m.Key == 'txtHeader'; })[0].Value;
        document.getElementById("txtHeaderNote").value = data.filter(function (m) { return m.Key == 'txtHeaderNote'; })[0].Value;
        document.getElementById("txtPaymentPeriod").value = data.filter(function (m) { return m.Key == 'txtPaymentPeriod'; })[0].Value;
        document.getElementById("txtScheduledVesting").value = data.filter(function (m) { return m.Key == 'txtScheduledVesting'; })[0].Value;
        document.getElementById("txtIntDurVesting").value = data.filter(function (m) { return m.Key == 'txtIntDurVesting'; })[0].Value;
        document.getElementById("txtIntroduction").value = data.filter(function (m) { return m.Key == 'txtIntroduction'; })[0].Value;
        document.getElementById("txtTerminationResaon1").value = data.filter(function (m) { return m.Key == 'txtTerminationResaon1'; })[0].Value;
        document.getElementById("txtUnvestedDeferredCash1").value = data.filter(function (m) { return m.Key == 'txtUnvestedDeferredCash1'; })[0].Value;
        document.getElementById("txtTerminationResaon2").value = data.filter(function (m) { return m.Key == 'txtTerminationResaon2'; })[0].Value;
        document.getElementById("txtUnvestedDeferredCash2").value = data.filter(function (m) { return m.Key == 'txtUnvestedDeferredCash2'; })[0].Value;
        document.getElementById("txtTerminationResaon3").value = data.filter(function (m) { return m.Key == 'txtTerminationResaon3'; })[0].Value;
        document.getElementById("txtUnvestedDeferredCash3").value = data.filter(function (m) { return m.Key == 'txtUnvestedDeferredCash3'; })[0].Value;
        document.getElementById("txtTerminationResaon4").value = data.filter(function (m) { return m.Key == 'txtTerminationResaon4'; })[0].Value;
        document.getElementById("txtUnvestedDeferredCash4").value = data.filter(function (m) { return m.Key == 'txtUnvestedDeferredCash4'; })[0].Value;
        document.getElementById("txtNonCompetition").value = data.filter(function (m) { return m.Key == 'txtNonCompetition'; })[0].Value;
        document.getElementById("txtNonSolicitation").value = data.filter(function (m) { return m.Key == 'txtNonSolicitation'; })[0].Value;
        document.getElementById("txtInteraction").value = data.filter(function (m) { return m.Key == 'txtInteraction'; })[0].Value;
        document.getElementById("txtCancellation").value = data.filter(function (m) { return m.Key == 'txtCancellation'; })[0].Value;
        document.getElementById("txtRegRequirement").value = data.filter(function (m) { return m.Key == 'txtRegRequirement'; })[0].Value;
        document.getElementById("txtFootNote").value = data.filter(function (m) { return m.Key == 'txtFootNote'; })[0].Value;
        document.getElementById("txtFooter").value = data.filter(function (m) { return m.Key == 'txtFooter'; })[0].Value;
    }
    function GetFieldValues() {
        var data = {};
        data.txtHeader = document.getElementById("txtHeader").value;
        data.txtHeaderNote = document.getElementById("txtHeaderNote").value;
        data.txtPaymentPeriod = document.getElementById("txtPaymentPeriod").value;
        data.txtScheduledVesting = document.getElementById("txtScheduledVesting").value;
        data.txtIntDurVesting = document.getElementById("txtIntDurVesting").value;
        data.txtIntroduction = document.getElementById("txtIntroduction").value;
        data.txtTerminationResaon1 = document.getElementById("txtTerminationResaon1").value;
        data.txtUnvestedDeferredCash1 = document.getElementById("txtUnvestedDeferredCash1").value;
        data.txtTerminationResaon2 = document.getElementById("txtTerminationResaon2").value;
        data.txtUnvestedDeferredCash2 = document.getElementById("txtUnvestedDeferredCash2").value;
        data.txtTerminationResaon3 = document.getElementById("txtTerminationResaon3").value;
        data.txtUnvestedDeferredCash3 = document.getElementById("txtUnvestedDeferredCash3").value;
        data.txtTerminationResaon4 = document.getElementById("txtTerminationResaon4").value;
        data.txtUnvestedDeferredCash4 = document.getElementById("txtUnvestedDeferredCash4").value;
        data.txtNonCompetition = document.getElementById("txtNonCompetition").value;
        data.txtNonSolicitation = document.getElementById("txtNonSolicitation").value;
        data.txtInteraction = document.getElementById("txtInteraction").value;
        data.txtCancellation = document.getElementById("txtCancellation").value;
        data.txtRegRequirement = document.getElementById("txtRegRequirement").value;
        data.txtFootNote = document.getElementById("txtFootNote").value;
        data.txtFooter = document.getElementById("txtFooter").value;
        return data;
    }
    return {
        UpdateStatementData: UpdateStatementData,
        GetStatementData: GetStatementData
    };
}();
//# sourceMappingURL=DeferredCashGrant.js.map