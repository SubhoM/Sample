<%@ Page Language="C#" MasterPageFile="~/CSSMaster.master" AutoEventWireup="true" CodeBehind="TIReport.aspx.cs" Inherits="HR.Reports.TIReport" %>

<%@ Register Src="~/Reports/CriteriaControl.ascx" TagPrefix="uc1" TagName="CriteriaControl" %>
<%@ Register Src="~/Reports/ReportDesc.ascx" TagPrefix="uc1" TagName="ReportDesc" %>





<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
     <meta name="viewport" content="width=device-width, initial-scale=1"/>
     <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

    <asp:PlaceHolder runat="server">        
         <%: Styles.Render("~/commonSite/css") %>         
    </asp:PlaceHolder>

   

    <link href="Reports.css" rel="stylesheet" />

</head>
<body>

    <input type="hidden" value="TI Report" id="reportName" />

    <div class="container-fluid">

        <div class="row">      
        <div class="col-sm-5">
        <div class="alert alert-success" id="Success-alert" hidden>
            <button type="button" class="close" data-dismiss="alert">x</button>
            <strong>Success! </strong>
            <span id="spnSuccess"></span>
        </div>
        <div class="alert alert-warning" id="Warning-alert" hidden>
            <button type="button" class="close" data-dismiss="alert">x</button>
            <strong>Warning! </strong>
            <span id="spnWarning"></span>
        </div>
        <div class="alert alert-danger" id="Error-alert" hidden>
            <button type="button" class="close" data-dismiss="alert">x</button>
            <strong>Error! </strong>
            <span id="spnError"></span>
        </div>
        </div>
        
</div>


        <uc1:ReportDesc runat="server" id="ReportDesc" />

    <uc1:criteriacontrol runat="server" id="CriteriaControl" />
    </div>

       <asp:PlaceHolder runat="server">        
         <%: Scripts.Render("~/bundles/commonSiteJS") %>      
         <%: Scripts.Render("~/Reports/js") %>             
    </asp:PlaceHolder>


</body>
</html>

 </asp:Content>