<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Promo418.aspx.cs" MasterPageFile="~/CSSMaster.master" Inherits="HR.DeferredCashGrant" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>Deferred Cash Grant</title>
     <meta name="viewport" content="width=device-width, initial-scale=1"/>
     <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

    <asp:PlaceHolder runat="server">        
         <%: Styles.Render("~/commonSite/css") %>         
    </asp:PlaceHolder>

    <style type="text/css">
        .panel .row{
            padding:15px 5px 10px 0;
            border-bottom:1px solid lightgray;
        }

        .container-fluid{
            padding:25px!important;
        }


    </style>
   
</head>

<body>

    <div class="container-fluid" > 

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

        <div class="row">
        <div class="panel panel-default">

        <div class="panel-heading">

            <h3 class="text-center">NOTICE PERIOD AGREEMENT</h3>
        </div>
        
        <div class="panel-body">
        <div class="row">
            <div class="col-sm-2"><span><b>Header:</b></span></div>
            <div class="col-sm-10">
            <input type="text" id="txtIntroduction" class="form-control"  />
            </div>            
        </div>

        <div class="row">
            <div class="col-sm-2"><span><b>At-Will Employment:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtAllWillEmployment" class="form-control"></textarea> 
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>Consideration:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtConsideration" class="form-control"></textarea>
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>Nature of Employee’s Position:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtNatureEmpPostion" class="form-control"></textarea>
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>Irreparable Harm:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtIrreperableHarm" class="form-control"></textarea>
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>Notice Period:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtNoticePeriod" class="form-control"></textarea>
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>Continuing Obligations:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtContObligation" class="form-control"></textarea>
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>CIT Right to Terminate:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtCITRightTerminate" class="form-control"></textarea>
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>Return of CIT Property:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtRtnCITProp" class="form-control"></textarea>
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>Scope of Restrictions:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtScopeRestrictions" class="form-control"></textarea>
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>Choice of Law:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtChoiceLaw" class="form-control"></textarea>
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>General Terms:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtGenTerm" class="form-control"></textarea>
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>Footer:</b></span></div>
            <div class="col-sm-10">
                <strong>
                    <textarea id="txtFooter" class="form-control"></textarea>
                 </strong>
            </div>            
        </div>

        <div class="col-sm-8 col-sm-offset-4" style="padding-top:15px">                
                <button class="btn btn-primary" type="button" onclick="promo418Js.UpdateStatementData()">Update</button>    
                <button class="btn btn-default" type="button" style="margin-left:20px" onclick="location.reload();">Cancel</button>    
        </div>
     </div>

        </div>

        </div>
    </div>

    <asp:PlaceHolder runat="server">        
         <%: Scripts.Render("~/bundles/commonSiteJS") %>               
    </asp:PlaceHolder>

    <script type="text/javascript" src="Promo418.js"></script>

</body>

</html>

</asp:Content>