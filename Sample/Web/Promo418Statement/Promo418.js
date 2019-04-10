$(document).ready(function () {
    sitejs.UpdatePageName('Promo 418 Addendum');
    promo418Js.GetStatementData();
});
var promo418Js = function () {
    var api = "/api/Statement/";
    api = sitejs.UpdateAPIURL(api);
    function GetStatementData() {
        $.ajax({
            url: api + 'GetStatementData?UID=5',
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
            url: api + 'UpdatePromo418StatementData',
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            error: function (x) {
                sitejs.ShowAlert("Unable to Update Statement Data. Please Try after some time", sitejs.msgType.Error);
            },
            success: function () {
                sitejs.ShowAlert("Data Updated Successfully", sitejs.msgType.Success);
            }
        });
    }
    function LoadStatementDetails(data) {
        document.getElementById("txtIntroduction").value = data.filter(function (m) { return m.Key == 'txtIntroduction'; })[0].Value;
        document.getElementById("txtAllWillEmployment").value = data.filter(function (m) { return m.Key == 'txtAllWillEmployment'; })[0].Value;
        document.getElementById("txtConsideration").value = data.filter(function (m) { return m.Key == 'txtConsideration'; })[0].Value;
        document.getElementById("txtNatureEmpPostion").value = data.filter(function (m) { return m.Key == 'txtNatureEmpPostion'; })[0].Value;
        document.getElementById("txtIrreperableHarm").value = data.filter(function (m) { return m.Key == 'txtIrreperableHarm'; })[0].Value;
        document.getElementById("txtNoticePeriod").value = data.filter(function (m) { return m.Key == 'txtNoticePeriod'; })[0].Value;
        document.getElementById("txtContObligation").value = data.filter(function (m) { return m.Key == 'txtContObligation'; })[0].Value;
        document.getElementById("txtCITRightTerminate").value = data.filter(function (m) { return m.Key == 'txtCITRightTerminate'; })[0].Value;
        document.getElementById("txtRtnCITProp").value = data.filter(function (m) { return m.Key == 'txtRtnCITProp'; })[0].Value;
        document.getElementById("txtScopeRestrictions").value = data.filter(function (m) { return m.Key == 'txtScopeRestrictions'; })[0].Value;
        document.getElementById("txtChoiceLaw").value = data.filter(function (m) { return m.Key == 'txtChoiceLaw'; })[0].Value;
        document.getElementById("txtGenTerm").value = data.filter(function (m) { return m.Key == 'txtGenTerm'; })[0].Value;
        document.getElementById("txtFooter").value = data.filter(function (m) { return m.Key == 'txtFooter'; })[0].Value;
    }
    function GetFieldValues() {
        var data = [];
        var arrFields = ["txtIntroduction", "txtAllWillEmployment", "txtConsideration", "txtNatureEmpPostion", "txtIrreperableHarm", "txtNoticePeriod", "txtContObligation",
            "txtCITRightTerminate", "txtRtnCITProp", "txtScopeRestrictions", "txtChoiceLaw", "txtGenTerm", "txtFooter"];
        for (var i = 0; i < arrFields.length; i++) {
            var obj = {};
            var _fieldName = arrFields[i];
            obj.Key = _fieldName;
            obj.Value = document.getElementById(_fieldName).value;
            data.push(obj);
        }
        return data;
    }
    return {
        UpdateStatementData: UpdateStatementData,
        GetStatementData: GetStatementData
    };
}();
//# sourceMappingURL=Promo418.js.map