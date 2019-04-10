<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalaryRange.aspx.cs" MasterPageFile="~/CSSMaster.master" Inherits="HR.SalaryRange.SalaryRange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head>
        <title>Salary Range Setup</title>
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

        <asp:PlaceHolder runat="server">
            <%: Styles.Render("~/commonSite/css") %>         
        </asp:PlaceHolder>

    </head>

    <body>

        <div class="container">

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

            <div class="col-md-8">

                <table class="table table-bordered">
                    <tr>
                        <td style="width: 50%">

                            <h3 class="header" style="text-align:center;font-weight:bold"> Range Tool Data </h3>

                            <input type="file" id="inSalaryRange" accept=".csv"/>
                            <br />

                            <%--<span style="color: red" id="spnNoFiles">No Files have been Uploaded yet</span>--%>
                            <br />

                            <button type="button" class="btn btn-sm btn-primary" onclick="SalaryRangeSetup.DelayedUpload('SalaryRange')">Upload File</button>

                            <button type="button" class="btn btn-sm btn-default pull-right" onclick="SalaryRangeSetup.DownloadFile('SalaryRange')">Download</button>

                        </td>
                        <td style="width: 50%">

                            <h3 class="header" style="text-align:center;font-weight:bold"> User Access Data </h3>

                            <input type="file" id="inRangeUserAccess" accept=".csv"/>
                            <br />

<%--                            <span style="color: red" id="spnNoFiles">No Files have been Uploaded yet</span>--%>
                            <br />

                            <button type="button" class="btn btn-sm btn-primary" onclick="SalaryRangeSetup.DelayedUpload('RangeUserAccess')">Upload File</button>

                            <button type="button" class="btn btn-sm btn-default pull-right" onclick="SalaryRangeSetup.DownloadFile('RangeUserAccess')">Download</button>

                        </td>
                    </tr>
                </table>
            </div>
        </div>


        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/commonSiteJS") %>               
        </asp:PlaceHolder>

        <script type="text/javascript" src="SalaryRange.js"></script>

    </body>
    </html>

</asp:Content>
