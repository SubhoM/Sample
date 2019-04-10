
//import { ICompMixTier } from "./ICompMixTier";


interface ICompMixTier {
    CompMixTierID: number;
    ApplicableGrid: string;
    Tier: string;
    TCUSDMin: number;
    TCUSDMax: number
    MinSalary: number;
    existing: boolean;
}

let currTabID = 1;
let deletedCompMixGridIds: string = "";
let deletedCompMixTierIds: string = "";
let grid;
let CompMixApi = "/api/CompMix/";

let msgType = {
    Success: "Success",
    Warning: "Warning",
    Error: "Error",
}

$(document).ready(function () {

    sitejs.UpdatePageName('Comp Mix Configuration');
    
    let port = +location.port;

    CompMixApi = port > 100 ? CompMixApi : ".." + CompMixApi;

    LoadApplicableGrid();   
  
});




function GetData(TabID : number){

    if (currTabID === TabID)
        return;

    currTabID = TabID;

    switch (TabID) {
        case 1:
            $('#divCompMixTierGrid').hide();
            LoadApplicableGrid();            
            break
        case 2:
            $('#divApplicableGrid').hide();          

            LoadCompMixTier();

            
            
            break;
    }

       
}


//region Comp Mix Grd
 
 function showAlert(message: string, type: string) {

    $('#spn' + type).text(message);

    if (type == msgType.Success) {
        $('#' + type + '-alert').fadeTo(10000, 500).slideUp(500, function () {
            $('#' + type + '-alert').slideUp(500);
        });
        return;
    }

    $('#' + type + '-alert').show();


 }

function LoadApplicableGrid(productsData?) { 

    let currObj = this;

    $('#divApplicableGrid').show();

    if (!$("#applicableGrid").data('kendoGrid'))
    $("#applicableGrid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    url: CompMixApi + 'GetCompMixGrid',
                    dataType: "json",                    
                }
            },
            schema: {
                model: {
                    fields: {
                        CompMixGridID: {editable: false, type: "number"  },
                        OrgLevel1: { editable: false, type: "string" },
                        ApplicableGrid: { type: "string", defaultValue: "Commercial Business" }                        
                    }
                }
            },           
        },        
        scrollable: false,        
        sortable: true,
        filterable: true,
        pageable: false,
        editable: true,
        columns: [
            { field: "CompMixGridID", hidden:true },            
            { field: "OrgLevel1", title: "Org Level1"},
            { field: "ApplicableGrid", title: "Applicable Grid", editor: gridDropDownEditor, template: "#=ApplicableGrid#" },
            {
                field: "CompMixGridID",
                title: "Delete",
                template: '#=deleteTemplateString(data)#',
                encoded: true,
                filterable: false,
                sortable: false,
                width: 80
            }
        ],
        //dataBound:  gridBound,        
    });  

    grid = $("#applicableGrid").data("kendoGrid");
 }

function deleteTemplateString(data): string {

    let CompMixGridID: number = data.CompMixGridID;

    let outPutString: string = "<div style='text-align:center'><img src='../Images/cms_delete.png' class='link activeLink' onclick='deleteGridEntry(" + CompMixGridID + ",false)' /></div>";

    return outPutString;


}

function deleteGridEntry(compMixGridId: string) {

    var IsConfirm = confirm("Are you sure you want to Delete?")

    if (!IsConfirm)
        return;

    var grid = $("#applicableGrid").data("kendoGrid"),        
        currentData = grid.dataSource.data();

    for (var i = 0; i < currentData.length; i++) {

        if (currentData[i].compMixGridId == compMixGridId) {
            currentData.splice(i, 1);
            break;
        }        
    }
    
    deletedCompMixGridIds = deletedCompMixGridIds == "" ? compMixGridId : deletedCompMixGridIds + "," + compMixGridId;

}


function gridBound() {

    //var grid = $("#applicableGrid").data("kendoGrid"),
    var parameterMap = grid.dataSource.transport.parameterMap;

    //get the new and the updated records
    var currentData = grid.dataSource.data();
    

    for (var i = 0; i < currentData.length; i++) {
        currentData[i].existing = true;
    }

        

}

function sendData() {

    $('#Warning-alert').hide();
    $('#Error-alert').hide();

    let _displaySuccessMessage = false;

    for (var iGrid = 0; iGrid < 2; iGrid++) {


        if (iGrid == 1)
            _displaySuccessMessage = true;

        let _grid;
        let _url;
        var _deletedIds;

        if (iGrid == 0) {
            _grid = $("#applicableGrid").data("kendoGrid");
            _url = "UpdateApplicableGrid";
            _deletedIds = deletedCompMixGridIds;
        } else {

            _grid = $("#compMixTierGrid").data("kendoGrid");
            _url = "UpdateCompMixTier";
            _deletedIds = deletedCompMixTierIds;
            
        }
        
        if (!_grid) {
            CompMixRecalculate();
            return;
        }

        let _parameterMap = _grid.dataSource.transport.parameterMap;

        //get the new and the updated records
        let _currentData = _grid.dataSource.data();

        if (iGrid == 1 && !ValidateTierData(_currentData))
            return;

        let _updatedRecords = [];
        let _newRecords = [];
        let _deletedRecords = [];

        for (var i = 0; i < _currentData.length; i++) {

            if (!_currentData[i].existing) {
                _currentData[i].existing = false;
                _newRecords.push(_currentData[i].toJSON());
                continue;
            }

            if (_currentData[i].dirty) {
                _updatedRecords.push(_currentData[i].toJSON());                
            }
        }

        //this records are deleted
        //var deletedRecords = [];
        //for (var i = 0; i < grid.dataSource._destroyed.length; i++) {
        //    deletedRecords.push(grid.dataSource._destroyed[i].toJSON());
        //}



        let _data = {};
        $.extend(_data, _parameterMap({ updatedList: _updatedRecords }), _parameterMap({ deletedIds: _deletedIds }), _parameterMap({ newList: _newRecords }));

        UpdateDatabase(_data, _url, _grid, _displaySuccessMessage);

    }
}

function ValidateTierData(tierData: any) {
    
    let result = true;

    let currTierData: ICompMixTier[] = tierData;

    let errMessage;
        

    let gridTypes = ["Commercial Business", "Consumer/Corp. Function"];

    gridTypes.some(function (obj) {

        let _tierData = currTierData.filter(m => m.ApplicableGrid == obj.toString()).sort((m,n) => m.TCUSDMin - n.TCUSDMin); 

        for (let i = 0; i < _tierData.length ; i++) {                       

            if (i > 0 && _tierData[i].TCUSDMin < _tierData[i - 1].TCUSDMax) {

                errMessage = _tierData[i].ApplicableGrid + ": " + _tierData[i - 1].Tier + " Total Comp Max value for should be less than " + _tierData[i].Tier + " Total Comp Min Value";
                showAlert(errMessage, msgType.Error);
                result = false;
                return true;                
            }

            if (i > 0 && _tierData[i].MinSalary < _tierData[i - 1].MinSalary) {
                errMessage = _tierData[i].ApplicableGrid + ": " + _tierData[i - 1].Tier + " Minimum Salary should be less than " + _tierData[i].Tier + " Minimum Salary";
                showAlert(errMessage, msgType.Error);
                result = false;
                return true;                
            }

        }
        
    });
    
    return result;

}

//var promise = new Promise(function (resolve, reject) {
//    // do a thing, possibly async, then…

//    if (1==1) {
//        resolve("Stuff worked!");
//    }
//    else {
//        reject(Error("It broke"));
//    }
//});

//async function UpdateDatabase(data, url, _grid, displaySuccessMessage) {

//    $.ajax({
//        url: api + url,
//        data: data,
//        type: "POST",
//        error: function () {
//            //Handle the server errors using the approach from the previous example
//        },
//        success: function () {

//            if (displaySuccessMessage)
//                showAlert("Data Updated Successfully");


//            _grid.dataSource._destroyed = [];
//            //refresh the grid - optional
//            _grid.dataSource.read();
//        }
//    })


//    try {
//        const config = {
//            method: 'POST',
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json',
//            },
//            body: JSON.stringify(data)
//        }
//        const response = await fetch(url, config)
//        //const json = await response.json()
//        if (response.ok) {
//            //return json
//            return response
//        } else {
//            //
//        }
//    } catch (error) {
//        //
//    }

//}

function UpdateDatabase(data, url, _grid, displaySuccessMessage) {
    
    $.ajax({
        url: CompMixApi + url,
        data: data,
        type: "POST",
        error: function () {
            //Handle the server errors using the approach from the previous example
        },
        success: function () {

            if (displaySuccessMessage)
            showAlert("Data Updated Successfully", msgType.Success);


            _grid.dataSource._destroyed = [];
            //refresh the grid - optional
            _grid.dataSource.read();
        }
    })

}

function CompMixRecalculate() {
    $.ajax({
        url: CompMixApi + "CompMixRecalculate",
        data: {},
        type: "POST",
        error: function () {
            //Handle the server errors using the approach from the previous example
        },
        success: function () {
                showAlert("Data Updated Successfully", msgType.Success);            
        }
    })
}

 function cancelChanges() {
    //var grid = $("#applicableGrid").data("kendoGrid");

     for (var iGrid = 0; iGrid < 2; iGrid++) {

         let _grid;      

         if (iGrid == 0) {
             _grid = $("#applicableGrid").data("kendoGrid");
         } else {
             _grid = $("#compMixTierGrid").data("kendoGrid");
         }

         _grid.dataSource._destroyed = [];
         //refresh the grid - optional
         _grid.dataSource.read();    
     }
     
}



 function gridDropDownEditor(container, options) {

    var dropDownValues = [{
        Value: 'Commercial Business',
        Text: 'Commercial Business'
    },
    {
        Value: 'Consumer/Corp. Function',
        Text: 'Consumer/Corp. Function'
    }
    ];

    $('<input required name="' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: true,
            dataTextField: "Text",
            dataValueField: "Value",
            dataSource: {
              data:dropDownValues
            }
        });
}


//region Comp Mix Tier

function LoadCompMixTier() {

    $('#divCompMixTierGrid').show();
    
    if (!$("#compMixTierGrid").data('kendoGrid'))
    $("#compMixTierGrid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    url: CompMixApi + 'GetCompMixTier',
                    dataType: "json",
                }
            },
            schema: {
                model: {
                    fields: {
                        CompMixTierID: { editable: false, type: "number" },
                        ApplicableGrid: { type: "string", defaultValue: "Commercial Business" },
                        Tier: {  type: "string" },
                        TCUSDMin: { type: "number" },
                        TCUSDMax: { type: "number" },
                        MinSalary: { type: "number" }
                    }
                }
            },
        },
        scrollable: false,
        sortable: true,
        filterable: true,
        toolbar: ["create"],
        pageable: false,
        editable: true,
        columns: [
            { field: "CompMixTierID", hidden: true },            
            { field: "ApplicableGrid", title: "Applicable Grid", editor: gridDropDownEditor, template: "#=ApplicableGrid#" },
            { field: "Tier", title: "Tier" },
            { field: "TCUSDMin", title: "TC USD Min" },
            { field: "TCUSDMax", title: "TC USD Max" },
            { field: "MinSalary", title: "Minimum Salary" },
            {
                field: "CompMixGridID",
                title: "Delete",
                template: '#=deleteTierTemplateString(data)#',
                encoded: true,
                filterable: false,
                sortable: false,
                width: 80
            }
        ],
        //dataBound: gridBound
    });  

    grid = $("#compMixTierGrid").data("kendoGrid");
 }

function deleteTierTemplateString(data): string {

    let CompMixTierID: number = data.CompMixTierID;

    let outPutString: string = "<div style='text-align:center'><img src='../Images/cms_delete.png' class='link activeLink' onclick='deleteTierGridEntry(" + CompMixTierID + ",false)' /></div>";

    return outPutString;


}


function deleteTierGridEntry(CompMixTierID: string) {

    var IsConfirm = confirm("Are you sure you want to Delete?")

    if (!IsConfirm)
        return;

    var grid = $("#compMixTierGrid").data("kendoGrid"),
        currentData = grid.dataSource.data();

    for (var i = 0; i < currentData.length; i++) {

        if (currentData[i].CompMixTierID == CompMixTierID) {
            currentData.splice(i, 1);
            break;
        }
    }

    deletedCompMixTierIds = deletedCompMixTierIds == "" ? CompMixTierID : deletedCompMixTierIds + "," + CompMixTierID;

}