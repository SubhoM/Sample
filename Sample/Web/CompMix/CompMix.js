var currTabID = 1;
var deletedCompMixGridIds = "";
var deletedCompMixTierIds = "";
var grid;
var CompMixApi = "/api/CompMix/";
var msgType = {
    Success: "Success",
    Warning: "Warning",
    Error: "Error",
};
$(document).ready(function () {
    sitejs.UpdatePageName('Comp Mix Configuration');
    var port = +location.port;
    CompMixApi = port > 100 ? CompMixApi : ".." + CompMixApi;
    LoadApplicableGrid();
});
function GetData(TabID) {
    if (currTabID === TabID)
        return;
    currTabID = TabID;
    switch (TabID) {
        case 1:
            $('#divCompMixTierGrid').hide();
            LoadApplicableGrid();
            break;
        case 2:
            $('#divApplicableGrid').hide();
            LoadCompMixTier();
            break;
    }
}
function showAlert(message, type) {
    $('#spn' + type).text(message);
    if (type == msgType.Success) {
        $('#' + type + '-alert').fadeTo(10000, 500).slideUp(500, function () {
            $('#' + type + '-alert').slideUp(500);
        });
        return;
    }
    $('#' + type + '-alert').show();
}
function LoadApplicableGrid(productsData) {
    var currObj = this;
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
        });
    grid = $("#applicableGrid").data("kendoGrid");
}
function deleteTemplateString(data) {
    var CompMixGridID = data.CompMixGridID;
    var outPutString = "<div style='text-align:center'><img src='../Images/cms_delete.png' class='link activeLink' onclick='deleteGridEntry(" + CompMixGridID + ",false)' /></div>";
    return outPutString;
}
function deleteGridEntry(compMixGridId) {
    var IsConfirm = confirm("Are you sure you want to Delete?");
    if (!IsConfirm)
        return;
    var grid = $("#applicableGrid").data("kendoGrid"), currentData = grid.dataSource.data();
    for (var i = 0; i < currentData.length; i++) {
        if (currentData[i].compMixGridId == compMixGridId) {
            currentData.splice(i, 1);
            break;
        }
    }
    deletedCompMixGridIds = deletedCompMixGridIds == "" ? compMixGridId : deletedCompMixGridIds + "," + compMixGridId;
}
function gridBound() {
    var parameterMap = grid.dataSource.transport.parameterMap;
    var currentData = grid.dataSource.data();
    for (var i = 0; i < currentData.length; i++) {
        currentData[i].existing = true;
    }
}
function sendData() {
    $('#Warning-alert').hide();
    $('#Error-alert').hide();
    var _displaySuccessMessage = false;
    for (var iGrid = 0; iGrid < 2; iGrid++) {
        if (iGrid == 1)
            _displaySuccessMessage = true;
        var _grid = void 0;
        var _url = void 0;
        var _deletedIds;
        if (iGrid == 0) {
            _grid = $("#applicableGrid").data("kendoGrid");
            _url = "UpdateApplicableGrid";
            _deletedIds = deletedCompMixGridIds;
        }
        else {
            _grid = $("#compMixTierGrid").data("kendoGrid");
            _url = "UpdateCompMixTier";
            _deletedIds = deletedCompMixTierIds;
        }
        if (!_grid) {
            CompMixRecalculate();
            return;
        }
        var _parameterMap = _grid.dataSource.transport.parameterMap;
        var _currentData = _grid.dataSource.data();
        if (iGrid == 1 && !ValidateTierData(_currentData))
            return;
        var _updatedRecords = [];
        var _newRecords = [];
        var _deletedRecords = [];
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
        var _data = {};
        $.extend(_data, _parameterMap({ updatedList: _updatedRecords }), _parameterMap({ deletedIds: _deletedIds }), _parameterMap({ newList: _newRecords }));
        UpdateDatabase(_data, _url, _grid, _displaySuccessMessage);
    }
}
function ValidateTierData(tierData) {
    var result = true;
    var currTierData = tierData;
    var errMessage;
    var gridTypes = ["Commercial Business", "Consumer/Corp. Function"];
    gridTypes.some(function (obj) {
        var _tierData = currTierData.filter(function (m) { return m.ApplicableGrid == obj.toString(); }).sort(function (m, n) { return m.TCUSDMin - n.TCUSDMin; });
        for (var i = 0; i < _tierData.length; i++) {
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
function UpdateDatabase(data, url, _grid, displaySuccessMessage) {
    $.ajax({
        url: CompMixApi + url,
        data: data,
        type: "POST",
        error: function () {
        },
        success: function () {
            if (displaySuccessMessage)
                showAlert("Data Updated Successfully", msgType.Success);
            _grid.dataSource._destroyed = [];
            _grid.dataSource.read();
        }
    });
}
function CompMixRecalculate() {
    $.ajax({
        url: CompMixApi + "CompMixRecalculate",
        data: {},
        type: "POST",
        error: function () {
        },
        success: function () {
            showAlert("Data Updated Successfully", msgType.Success);
        }
    });
}
function cancelChanges() {
    for (var iGrid = 0; iGrid < 2; iGrid++) {
        var _grid = void 0;
        if (iGrid == 0) {
            _grid = $("#applicableGrid").data("kendoGrid");
        }
        else {
            _grid = $("#compMixTierGrid").data("kendoGrid");
        }
        _grid.dataSource._destroyed = [];
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
            data: dropDownValues
        }
    });
}
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
                            Tier: { type: "string" },
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
        });
    grid = $("#compMixTierGrid").data("kendoGrid");
}
function deleteTierTemplateString(data) {
    var CompMixTierID = data.CompMixTierID;
    var outPutString = "<div style='text-align:center'><img src='../Images/cms_delete.png' class='link activeLink' onclick='deleteTierGridEntry(" + CompMixTierID + ",false)' /></div>";
    return outPutString;
}
function deleteTierGridEntry(CompMixTierID) {
    var IsConfirm = confirm("Are you sure you want to Delete?");
    if (!IsConfirm)
        return;
    var grid = $("#compMixTierGrid").data("kendoGrid"), currentData = grid.dataSource.data();
    for (var i = 0; i < currentData.length; i++) {
        if (currentData[i].CompMixTierID == CompMixTierID) {
            currentData.splice(i, 1);
            break;
        }
    }
    deletedCompMixTierIds = deletedCompMixTierIds == "" ? CompMixTierID : deletedCompMixTierIds + "," + CompMixTierID;
}
//# sourceMappingURL=CompMix.js.map