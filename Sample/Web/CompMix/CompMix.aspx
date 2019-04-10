<%@ Page Language="C#" MasterPageFile="~/CSSMaster.master" AutoEventWireup="true" CodeBehind="CompMix.aspx.cs" Inherits="HR.CompMix.CompMix" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
 <!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    

     <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    
  
    <%--<link href="../Content/bootstrap.min.css" rel="stylesheet" />--%>

    <link href="../Content/kendo/2017.2.621/kendo.common-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Content/kendo/2017.2.621/kendo.mobile.all.min.css" rel="stylesheet" type="text/css" />
    <link href="../Content/kendo/2017.2.621/kendo.dataviz.min.css" rel="stylesheet" type="text/css" />
    <link href="../Content/kendo/2017.2.621/kendo.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Content/kendo/2017.2.621/kendo.dataviz.bootstrap.min.css" rel="stylesheet" type="text/css" />

    <asp:PlaceHolder runat="server">        
         <%: Styles.Render("~/commonSite/css") %>         
    </asp:PlaceHolder>

    <link href="CompMix.css" rel="stylesheet" />
   
</head>
<body>   

    <div class="container-fluid text-left">

    <div class="row" style="padding-bottom:15px;">
       <div class="col-sm-12 text-right">
        <button class="btn btn-md btn-primary" type="button" onclick="sendData()">Save and Recalculate Comp</button>
        <button class="btn btn-md btn-secondary" type="button" onclick="cancelChanges()">Cancel</button>
       </div>
    </div>

    <div class="row" style="padding-bottom:15px">        
        <div class="col-sm-12">
            <div class="nav nav-tabs" id="CompMixTab">
                <div class="btnGrpCustom btn-group" data-toggle="buttons">
                    <label class="btn btn-primary active" id="lblAssignedToMe" onclick="GetData(1)">
                        <input type="radio" name="options" autocomplete="off" value="1"/> Applicable Grid
                    </label>
                    <label class="btn btn-primary" id="lblAssignedByMe"  onclick="GetData(2)">
                        <input type="radio" name="options" autocomplete="off" value="2" /> Salary Tier
                    </label>
                </div>
            </div>       
            </div>
    </div>

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
        
    <div id="divApplicableGrid" class="row">      
        <div class="col-sm-4">
            <div id="applicableGrid"> </div>
        </div>
     </div>

      <div id="divCompMixTierGrid" class="row">      
        <div class="col-sm-6" style="padding:15px">
            <div id="compMixTierGrid"> </div>
        </div>
     </div>
    
    </div>

     <asp:PlaceHolder runat="server">        
         <%: Scripts.Render("~/bundles/commonSiteJSWithKendo") %>      
         <%: Scripts.Render("~/CompMix/js") %>             
    </asp:PlaceHolder>
    
      
</body>
</html>


 </asp:Content>