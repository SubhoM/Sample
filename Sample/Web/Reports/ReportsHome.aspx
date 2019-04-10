<%@ Page Language="C#" MasterPageFile="~/CSSMaster.master" AutoEventWireup="true" CodeBehind="ReportsHome.aspx.cs" Inherits="HR.Reports.ReportsHome" %>

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



    <div class="container-fluid">

        <div class="col-sm-9 col-sm-offset-1">

      <table class="table table-hover table-striped" >
            <thead>
            <tr>
            <th>Report Name</th>
            <th>Description</th>
            <th></th>
            <th></th>
             </tr>
            </thead>
          <tbody id="reportBody">
            <tr style="display:none">
                <td style="width:20%">
                    Report Name
                </td>
                 <td style="width:65%">
                     Report Description
                </td>
                <td style="width:7%">
                    <a type="button" href="#" onclick="ReportHome.GenerateReport(this.id)">
                        <i class="fa fa-file-excel-o" style="font-size:30px;color:green"></i>
                    </a>
                </td>
                <td style="width:8%;padding-top:10px">
                    <a type="button" href="#" onclick="ReportHome.EditReport(this.id)" >                    
                         <i class="fa fa-pencil-square-o" style="font-size:30px;color:dodgerblue"></i>
                    </a>
                </td>
            </tr>
          </tbody>
        </table>

        </div>
    
    </div>

       <asp:PlaceHolder runat="server">        
         <%: Scripts.Render("~/bundles/commonSiteJS") %>      
         <%: Scripts.Render("~/Reports/js") %>             
    </asp:PlaceHolder>


</body>
</html>

</asp:Content>