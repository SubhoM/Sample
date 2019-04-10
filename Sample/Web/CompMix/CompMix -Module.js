"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var CompMixTier_1 = require("./CompMixTier");
var currTabID = 1;
var deletedCompMixGridIds = "";
$(document).ready(function () {
    var _compMix = new CompMix();
    _compMix.LoadApplicableGrid();
});
var CompMix = (function () {
    function CompMix() {
        this._compMix = this;
    }
    CompMix.prototype.GetData = function (TabID) {
        if (currTabID == TabID)
            return;
        currTabID = TabID;
        switch (TabID) {
            case 1:
                $('#compMixTierGrid').hide();
                this.LoadApplicableGrid();
                break;
            case 2:
                $('#divApplicableGrid').hide();
                var _compTier = new CompMixTier_1.compMixTier();
                _compTier.LoadCompMixTier();
                break;
        }
    };
    CompMix.prototype.showAlert = function (message) {
        $('#spnSuccess').text(message);
        $("#success-alert").fadeTo(5000, 500).slideUp(500, function () {
            $("#success-alert").slideUp(500);
        });
    };
    ;
    CompMix.prototype.LoadApplicableGrid = function (productsData) {
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
                            CompMixGridID: { editable: false, type: "number" },
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
                { field: "CompMixGridID", hidden: true },
                { field: "OrgLevel1", title: "Org Level1" },
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
            dataBound: this.gridBound,
        });
    };
    CompMix.prototype.deleteTemplateString = function (data) {
        var CompMixGridID = data.CompMixGridID;
        var outPutString = "<div style='text-align:center'><img src='/Images/cms_delete.png' class='link activeLink' onclick='deleteGridEntry(" + CompMixGridID + ",false)' /></div>";
        return outPutString;
    };
    CompMix.prototype.deleteGridEntry = function (compMixGridId) {
        var IsConfirm = confirm("Are you sure you want to Delete?");
        if (!IsConfirm)
            return;
        var grid = $("#applicableGrid").data("kendoGrid"), currentData = grid.dataSource.data();
        for (var i = 0; i < currentData.length; i++) {
            if (currentData[i].compMixGridId = compMixGridId) {
                currentData.splice(i, 1);
                break;
            }
        }
        deletedCompMixGridIds = deletedCompMixGridIds == "" ? compMixGridId : deletedCompMixGridIds + "," + compMixGridId;
    };
    CompMix.prototype.gridBound = function () {
        var grid = $("#applicableGrid").data("kendoGrid"), parameterMap = grid.dataSource.transport.parameterMap;
        var currentData = grid.dataSource.data();
        for (var i = 0; i < currentData.length; i++) {
            currentData[i].existing = true;
        }
    };
    CompMix.prototype.sendData = function () {
        var grid = $("#applicableGrid").data("kendoGrid"), parameterMap = grid.dataSource.transport.parameterMap;
        var currentData = grid.dataSource.data();
        var updatedRecords = [];
        var newRecords = [];
        var deletedRecords = [];
        for (var i = 0; i < currentData.length; i++) {
            if (currentData[i].dirty) {
                updatedRecords.push(currentData[i].toJSON());
                continue;
            }
            if (!currentData[i].existing) {
                newRecords.push(currentData[i].toJSON());
            }
        }
        var data = {};
        $.extend(data, parameterMap({ updatedList: updatedRecords }), parameterMap({ deletedCompMixGridIds: deletedCompMixGridIds }), parameterMap({ newList: newRecords }));
        $.ajax({
            url: "/api/CompMix/UpdateCreateDelete",
            data: data,
            type: "POST",
            error: function () {
            },
            success: function () {
                this.showAlert("Data Update is completed");
                grid.dataSource._destroyed = [];
                grid.dataSource.read();
            }
        });
    };
    CompMix.prototype.cancelChanges = function () {
        var grid = $("#applicableGrid").data("kendoGrid");
        grid.dataSource._destroyed = [];
        grid.dataSource.read();
    };
    CompMix.prototype.gridDropDownEditor = function (container, options) {
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
                data: dropDownValues
            }
        });
    };
    return CompMix;
}());
exports.CompMix = CompMix;
//# sourceMappingURL=CompMix -Module.js.map