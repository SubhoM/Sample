<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportDesc.ascx.cs" Inherits="HR.Reports.ReportDesc" %>

<div class="panel panel-default" >
    <div class="panel-body" id="pnlReportDesc"  >
        <div class="row" style="padding-bottom:15px">
            <div class="col-md-2">
                <strong>Report Name</strong>
            </div>
            <div class="col-md-4">
                <span id="spnRptName"></span>
                <input class="form-control" id="txtRptName" type="text" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <strong>Report Description</strong>
            </div>
            <div class="col-md-6">
                <span id="spnRptDescription"></span>
                <textarea class="form-control" id="txtRptDescription" ></textarea>
            </div>
        </div>

        <div class="row">
            <div class="col-md-3 col-md-offset-9" id="divDescEdit">
                 <a type="button" href="#" onclick="ReportObj.EditReportDetails()" >   
                <i class="fa fa-pencil-square-o" style="font-size:35px;color:dodgerblue"></i>
                  </a>  
            </div>
             <div class="col-md-3 col-md-offset-9" id="divDescUpdate" hidden="hidden">
                 <a type="button" class="btn btn-sm btn-primary" href="#" onclick="ReportObj.UpdateReport(true)" > Done </a>  
                 <a type="button" class="btn btn-sm btn-default" href="#" onclick="ReportObj.UpdateReport(false)" > Cancel </a>  
            </div>
        </div>

    </div>
</div>
