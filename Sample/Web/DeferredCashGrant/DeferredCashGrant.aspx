<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeferredCashGrant.aspx.cs" MasterPageFile="~/CSSMaster.master" Inherits="HR.DeferredCashGrant" %>



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

            <h3 class="text-center">Deferred Cash Grant Statement</h3>
        </div>
        
        <div class="panel-body">
        <div class="row">
            <div class="col-sm-2"><span><b>Header:</b></span></div>
            <div class="col-sm-10">
            <input type="text" id="txtHeader" class="form-control"  />
            </div>            
        </div>
        
        <div class="row">
            <div class="col-sm-2"><span><b>HeaderNote:</b></span></div>
            <div class="col-sm-10">
            <input type="text" id="txtHeaderNote" class="form-control"  />
            </div>            
        </div>


        <div class="row">
            <div class="col-sm-2"><span><b>Payment Period:</b></span></div>
            <div class="col-sm-10">
            <input type="text" id="txtPaymentPeriod" class="form-control"  />
            </div>            
        </div>

        <div class="row">
            <div class="col-sm-2"><span><b>Scheduled Vesting:</b></span></div>
            <div class="col-sm-10">
            <input type="text" id="txtScheduledVesting" class="form-control"  />
            </div>            
        </div>

        <div class="row">
            <div class="col-sm-2"><span><b>Interest During Vesting:</b></span></div>
            <div class="col-sm-10">
            <input type="text" id="txtIntDurVesting" class="form-control"  />
            </div>            
        </div>


        <div class="row">
            <div class="col-sm-2"><span><b>Payment Date:</b></span></div>
            <div class="col-sm-10">
            <input type="text" id="txtIntroduction" class="form-control"  />
            </div>            
        </div>
        <div class="row">
        <div class="col-sm-2"><span><b>Treatment of Outstanding Awards Upon Termination:</b></span></div>
        <div class="col-sm-10">
            <table class="table table-bordered">
                <thead class="thead-dark">
                     <tr>
                        <th class="text-center">Termination Reason</th>
                        <th class="text-center">Unvested Deferred Cash</th>
                     </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <textarea id="txtTerminationResaon1" class="form-control"></textarea>

                        </td>
                        <td>
                            <textarea id="txtUnvestedDeferredCash1" class="form-control"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <textarea id="txtTerminationResaon2" class="form-control"></textarea>
                            
                        </td>
                        <td>
                            <textarea id="txtUnvestedDeferredCash2" class="form-control"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <textarea id="txtTerminationResaon3" class="form-control">                           
                            </textarea>
                        </td>
                        <td>
                            <textarea id="txtUnvestedDeferredCash3" class="form-control"></textarea>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <textarea id="txtTerminationResaon4" class="form-control"></textarea>

                        </td>
                        <td>
                            <textarea id="txtUnvestedDeferredCash4" class="form-control"></textarea>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <span>Non-Competition</span>
                            <br />
                            <textarea id="txtNonCompetition" class="form-control"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <span>Non-Solicitation</span>
                            <br />
                            <textarea id="txtNonSolicitation" class="form-control"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <span>Interaction with Employment / Other Agreements</span>
                            <br />
                            <textarea id="txtInteraction" class="form-control"></textarea>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>Cancellation\Recoupment:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtCancellation" class="form-control"></textarea> 
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>Regulatory Requirements:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtRegRequirement" class="form-control"></textarea>
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>Footnote:</b></span></div>
            <div class="col-sm-10">
            <textarea id="txtFootNote" class="form-control"></textarea>
            </div>            
        </div>
        <div class="row">
            <div class="col-sm-2"><span><b>Footer:</b></span></div>
            <div class="col-sm-10">
            <input type="text" id="txtFooter" class="form-control"  />
            </div>            
        </div>
        <div class="col-sm-8 col-sm-offset-4" style="padding-top:15px">                
                <button class="btn btn-primary" type="button" onclick="statementJs.UpdateStatementData()">Update</button>    
                <button class="btn btn-default" type="button" style="margin-left:20px" onclick="location.reload();">Cancel</button>    
        </div>
     </div>

        </div>

        </div>
    </div>

    <asp:PlaceHolder runat="server">        
         <%: Scripts.Render("~/bundles/commonSiteJS") %>               
    </asp:PlaceHolder>

    <script type="text/javascript" src="DeferredCashGrant.js"></script>

</body>

</html>

</asp:Content>