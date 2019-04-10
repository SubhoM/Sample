
$(document).ready(function () {

    sitejs.UpdatePageName('Promo 418 Addendum');

    promo418Js.GetStatementData();

});

let promo418Js = function () {

    let api = "/api/Statement/";

    api = sitejs.UpdateAPIURL(api);


interface Statement {
        UID: number;
        Key:string;
        Value: string;
}

    function GetStatementData() {

        $.ajax({
            url: api + 'GetStatementData?UID=5',
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
            url: api + 'UpdatePromo418StatementData',
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json; charset=utf-8",            
            error: function (x) {
                //Handle the server errors 

                sitejs.ShowAlert("Unable to Update Statement Data. Please Try after some time", sitejs.msgType.Error);
            },
            success: function () {

                sitejs.ShowAlert("Data Updated Successfully", sitejs.msgType.Success);
            }
        })

    }

    function LoadStatementDetails(data: Statement[]) {
        
        (<HTMLInputElement>document.getElementById("txtIntroduction")).value = data.filter(m => m.Key == 'txtIntroduction')[0].Value;

        (<HTMLInputElement>document.getElementById("txtAllWillEmployment")).value = data.filter(m => m.Key == 'txtAllWillEmployment')[0].Value;
        (<HTMLInputElement>document.getElementById("txtConsideration")).value = data.filter(m => m.Key == 'txtConsideration')[0].Value; 
        
        (<HTMLInputElement>document.getElementById("txtNatureEmpPostion")).value = data.filter(m => m.Key == 'txtNatureEmpPostion')[0].Value;
        (<HTMLInputElement>document.getElementById("txtIrreperableHarm")).value = data.filter(m => m.Key == 'txtIrreperableHarm')[0].Value; 

        (<HTMLInputElement>document.getElementById("txtNoticePeriod")).value = data.filter(m => m.Key == 'txtNoticePeriod')[0].Value;
        (<HTMLInputElement>document.getElementById("txtContObligation")).value = data.filter(m => m.Key == 'txtContObligation')[0].Value; 

        (<HTMLInputElement>document.getElementById("txtCITRightTerminate")).value = data.filter(m => m.Key == 'txtCITRightTerminate')[0].Value;
        (<HTMLInputElement>document.getElementById("txtRtnCITProp")).value = data.filter(m => m.Key == 'txtRtnCITProp')[0].Value; 

        (<HTMLInputElement>document.getElementById("txtScopeRestrictions")).value = data.filter(m => m.Key == 'txtScopeRestrictions')[0].Value;
        (<HTMLInputElement>document.getElementById("txtChoiceLaw")).value = data.filter(m => m.Key == 'txtChoiceLaw')[0].Value; 
        (<HTMLInputElement>document.getElementById("txtGenTerm")).value = data.filter(m => m.Key == 'txtGenTerm')[0].Value;
        (<HTMLInputElement>document.getElementById("txtFooter")).value = data.filter(m => m.Key == 'txtFooter')[0].Value;
                
    }


    function GetFieldValues() {

        let data: any = [];

        
        var arrFields = ["txtIntroduction", "txtAllWillEmployment", "txtConsideration", "txtNatureEmpPostion", "txtIrreperableHarm", "txtNoticePeriod", "txtContObligation",
            "txtCITRightTerminate", "txtRtnCITProp", "txtScopeRestrictions", "txtChoiceLaw", "txtGenTerm","txtFooter"];

        for (let i = 0; i < arrFields.length; i++ ) {

            let obj: any = {};

            let _fieldName = arrFields[i];

            obj.Key = _fieldName;
            obj.Value = (<HTMLInputElement>document.getElementById(_fieldName)).value;


            data.push(obj);

        }
        
        return data;
    }

    return {
        UpdateStatementData: UpdateStatementData,
        GetStatementData: GetStatementData
    }

}();