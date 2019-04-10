
import { compMixTier } from './CompMixTier'

let currTabID = 1;
let deletedCompMixGridIds : string = "";

$(document).ready(function () {

   var _compMix = new CompMix();

  _compMix.LoadApplicableGrid();   

});

export class CompMix{

 _compMix = this;

 GetData(TabID : number){

    if (currTabID == TabID)
        return;

    currTabID = TabID;

    switch (TabID) {
        case 1:
            $('#compMixTierGrid').hide();
            this.LoadApplicableGrid();            
            break
        case 2:
            $('#divApplicableGrid').hide();
            var _compTier = new compMixTier();

            _compTier.LoadCompMixTier();

            
            
            break;
    }

       
}

 
 showAlert(message: string) {

    $('#spnSuccess').text(message);

    $("#success-alert").fadeTo(5000, 500).slideUp(500, function () {
        $("#success-alert").slideUp(500);
    });
 };

LoadApplicableGrid(productsData?) { 

    $('#divApplicableGrid').show();

    $("#applicableGrid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    url: '/api/CompMix/GetCompMixGrid',
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
            { field: "ApplicableGrid", title: "Applicable Grid", editor: this.gridDropDownEditor, template: "#=ApplicableGrid#" },
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
        dataBound:  this.gridBound,        
    });  
 }

 deleteTemplateString(data): string {

    let CompMixGridID: number = data.CompMixGridID;

    let outPutString: string = "<div style='text-align:center'><img src='/Images/cms_delete.png' class='link activeLink' onclick='deleteGridEntry(" + CompMixGridID + ",false)' /></div>";

    return outPutString;


}

 deleteGridEntry(compMixGridId: string) {

    var IsConfirm = confirm("Are you sure you want to Delete?")

    if (!IsConfirm)
        return;

    var grid = $("#applicableGrid").data("kendoGrid"),        
        currentData = grid.dataSource.data();

    for (var i = 0; i < currentData.length; i++) {

        if (currentData[i].compMixGridId = compMixGridId) {
            currentData.splice(i, 1);
            break;
        }        
    }
    
    deletedCompMixGridIds = deletedCompMixGridIds == "" ? compMixGridId : deletedCompMixGridIds + "," + compMixGridId;

}


 gridBound() {

    var grid = $("#applicableGrid").data("kendoGrid"),
        parameterMap = grid.dataSource.transport.parameterMap;

    //get the new and the updated records
    var currentData = grid.dataSource.data();
    

    for (var i = 0; i < currentData.length; i++) {
        currentData[i].existing = true;
    }

        

}

 sendData() {
    var grid = $("#applicableGrid").data("kendoGrid"),
        parameterMap = grid.dataSource.transport.parameterMap;

    //get the new and the updated records
    var currentData = grid.dataSource.data();
    var updatedRecords = [];
    var newRecords = [];
    var deletedRecords = [];

    for (var i = 0; i < currentData.length; i++) {
        //if (currentData[i].isNew()) {
        //    //this record is new
        //    newRecords.push(currentData[i].toJSON());
        //} else if (currentData[i].dirty) {
        //    updatedRecords.push(currentData[i].toJSON());
        //}
               
        if (currentData[i].dirty) {
            updatedRecords.push(currentData[i].toJSON());
            continue;
        }
        

        if (!currentData[i].existing) {
            newRecords.push(currentData[i].toJSON());
        }
    }

    //this records are deleted
    //var deletedRecords = [];
    //for (var i = 0; i < grid.dataSource._destroyed.length; i++) {
    //    deletedRecords.push(grid.dataSource._destroyed[i].toJSON());
    //}

    var data = {};
    $.extend(data, parameterMap({ updatedList: updatedRecords }), parameterMap({ deletedCompMixGridIds: deletedCompMixGridIds }), parameterMap({ newList: newRecords }));

    $.ajax({
        url: "/api/CompMix/UpdateCreateDelete",
        data: data,
        type: "POST",
        error: function () {
            //Handle the server errors using the approach from the previous example
        },
        success: function () {
            
            this.showAlert("Data Update is completed");


            grid.dataSource._destroyed = [];
            //refresh the grid - optional
            grid.dataSource.read();    
        }
    })

    
}

 cancelChanges() {

    var grid = $("#applicableGrid").data("kendoGrid");

    grid.dataSource._destroyed = [];
    //refresh the grid - optional
    grid.dataSource.read();    
}



 gridDropDownEditor(container, options) {

    var dropDownValues = [{
        gridName: 'Commercial Business',
        gridID: 'Commercial Business'
    },
    {
        gridName: 'Consumer/Corp. Function',
        gridID: 'Consumer/Corp. Function'
    }
    ];

    $('<input required name="' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: true,
            dataTextField: "gridName",
            dataValueField: "gridID",
            dataSource: {
              data:dropDownValues
            }
        });
}


}