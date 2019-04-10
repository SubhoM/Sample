<%@ Page Title="" Language="C#" MasterPageFile="~/CSSMaster.master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Admin" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

    <asp:ScriptManager runat="server"></asp:ScriptManager>--%>


    
    <script type="text/javascript">
        function Confirm(sender, args) {
            var msg = "Are you sure you want to proceed?";
            args.set_cancel(!window.confirm(msg));

        }

        function RSUReportClientClicking(sender, args) {
            args.set_cancel(true);
            var txtRSUPrice = $("#<%=txtRSUPrice.ClientID%>");
            if (txtRSUPrice.val() != "") {
                 __doPostBack("<%= btnRSUReport.UniqueID %>", "onclick");
                //sender.click();
            }
            else
                alert("Please enter RSU Price");

        }

        // Dispaly confirm Dialogue box for Print Statement
        function ConfirmPrint(sender, args) {
            var name = document.getElementById('<%=txtName.ClientID%>').value;
            var country = document.getElementById('<%=ddlCountryCodes_Print.ClientID%>').value;


            if ((name == null || name == '') && (country == null || country == '')) {
                var msg = "Are you sure you want to print all statements?";
                args.set_cancel(!window.confirm(msg));
            }
        }

    </script>
    <asp:Label ID="lblHead" runat="server" Text="Administration" Font-Bold="True" Font-Names="Microsoft Sans Serif" Font-Size="Medium" ForeColor="#0080C0"></asp:Label>
    <br />
    <br />
    <div id="divBulkActions" runat="server" style="width: 800px; border: medium double #808000; border-width: 1px; background-color: #DFE8FD; font-family: 'Microsoft Sans Serif'; font-size: small; text-align: left">
        <br />
        <table border="0" width="500px">
            <tr>
                <td style="background-color: #003366; color: White; font-weight: bold;" colspan="3">Bulk Actions:</td>
            </tr>
            <tr>
                <td>
                    <telerik:RadButton ID="bntGrantAccess" runat="server" Skin="Web20"
                        OnClick="bntGrantAccess_Click" Text="Grant Access to ALL Planning Managers"
                        Width="280px" OnClientClicking="Confirm">
                    </telerik:RadButton>
                </td>
                <td>
                    <telerik:RadButton ID="btnRemoveAccess" runat="server" Skin="Web20"
                        OnClick="btnRemoveAccess_Click" Text="Remove Access from ALL Planning Managers"
                        Width="280px" OnClientClicking="Confirm">
                    </telerik:RadButton>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadButton ID="btnGrantAccessHRBP" runat="server" Skin="Web20"
                        OnClick="btnGrantAccessHRBP_Click" Text="Grant Access to ALL HRBPs"
                        Width="280px" OnClientClicking="Confirm">
                    </telerik:RadButton>
                </td>
                <td>
                    <telerik:RadButton ID="btnRemoveAccessHRBP" runat="server" Skin="Web20"
                        OnClick="btnRemoveAccessHRBP_Click" Text="Remove Access from ALL HRBPs"
                        Width="280px" OnClientClicking="Confirm">
                    </telerik:RadButton>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Label ID="ErrorMessageLabel" runat="server" ForeColor="Red" Width="150px"></asp:Label>
        <br />
        <br />
        <asp:FormView ID="FormView1" runat="server" AllowPaging="True" DataSourceID="SqlDataSource1" DataKeyNames="Config_Item">
            <HeaderTemplate>Application Configuration</HeaderTemplate>
            <HeaderStyle BackColor="#003366" ForeColor="White" Font-Bold="true" Height="30px" HorizontalAlign="Center" />
            <PagerStyle BackColor="#336699" ForeColor="White" Font-Bold="true" HorizontalAlign="Right" />
            <EditItemTemplate>
                <table width="300px" border="0">
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Configuration Item:
                        </td>
                        <td>
                            <asp:Label ID="lblConfig_Item" runat="server"
                                Text='<%# Bind("Config_Item") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>Configuration Value:
                        </td>
                        <td>
                            <asp:TextBox ID="Config_ValTextBox" runat="server"
                                Text='<%# Bind("Config_Val") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" Font-Bold="true" BackColor="#003366" ForeColor="White" BorderWidth="0px"
                                CommandName="Update" Text="Update" />
                        </td>
                        <td>
                            <asp:Button ID="UpdateCancelButton" runat="server" Font-Bold="true" BackColor="#003366" ForeColor="White" BorderWidth="0px"
                                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table width="300px" border="0">
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Configuration Item:
                        </td>
                        <td>
                            <asp:TextBox ID="Config_ItemTextBox" runat="server"
                                Text='<%# Bind("Config_Item") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>Configuration Value:
                        </td>
                        <td>
                            <asp:TextBox ID="Config_ValTextBox" runat="server"
                                Text='<%# Bind("Config_Val") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="InsertButton" runat="server" CausesValidation="True" Font-Bold="true" BackColor="#003366" ForeColor="White" BorderWidth="0px"
                                CommandName="Insert" Text="Insert" />
                        </td>
                        <td>
                            <asp:Button ID="InsertCancelButton" runat="server" Font-Bold="true" BackColor="#003366" ForeColor="White" BorderWidth="0px"
                                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <ItemTemplate>
                <table width="300px" border="0">
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Configuration Item:
                        </td>
                        <td>
                            <asp:Label ID="Config_ItemLabel" runat="server"
                                Text='<%# Bind("Config_Item") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>Configuration Value:
                        </td>
                        <td>
                            <asp:Label ID="Config_ValLabel" runat="server"
                                Text='<%# Bind("Config_Val") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" CommandName="Edit" Text="Edit" Font-Bold="true" BackColor="#003366" ForeColor="White" BorderWidth="0px" />
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" CommandName="New" Text="New" Font-Bold="true" BackColor="#003366" ForeColor="White" BorderWidth="0px" />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
            ConnectionString="<%$ ConnectionStrings:DEV %>"
            ProviderName="<%$ ConnectionStrings:DEV.ProviderName %>"
            SelectCommand="SELECT * FROM [AppConfiguration]"
            InsertCommand="INSERT INTO [AppConfiguration](Config_Item,Config_Val) VALUES (@Config_Item,@Config_Val)"
            UpdateCommand="UPDATE [AppConfiguration] set Config_Val=@Config_Val WHERE Config_Item=@Config_Item"></asp:SqlDataSource>
        <br />

    </div>
    <br />
    <div style="width: 800px; border: medium double #808000; border-width: 1px; background-color: #DFE8FD; font-family: 'Microsoft Sans Serif'; font-size: small; text-align: left">
        <br />
        <table border="0" width="500px">
            <tr>
                <td style="background-color: #003366; color: White; font-weight: bold;" colspan="6">Downloads:</td>
            </tr>
            <tr>
                <td>
                    <telerik:RadButton ID="btnPrintStmt" runat="server" Skin="Web20"
                        OnClick="btnPrintStmt_Click" Text="Print Statements"
                        Width="180px" OnClientClicking="ConfirmPrint">
                    </telerik:RadButton>
                </td>
                 <td style="width:30px;"><b>Select Country:</b></td>
                <td>
                    <telerik:RadComboBox runat="server" ID="ddlCountryCodes_Print" Skin="Office2007" Width="55px"></telerik:RadComboBox>
                    &nbsp;<asp:TextBox ID="txtName" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadNumericTextBox ID="txtRSUPrice" runat="server" ReadOnly="false" Type="Currency" ToolTip="RSU Price" Width="80px">
                        <NegativeStyle ForeColor="#FF3300" HorizontalAlign="Center" />
                        <NumberFormat DecimalDigits="2" DecimalSeparator="." AllowRounding="false" />
                    </telerik:RadNumericTextBox>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRSUPrice" runat="server" Text="RSU Price"></asp:Label></td>
                <td><asp:Label ID="Label1" runat="server" Text="Date of Award" Width="100px"></asp:Label></td>
                <td><telerik:RadDatePicker ID="AwardDate" runat="server" MinDate="1/1/1950"> 
                              <%-- <DateInput ClientEvents-OnError="HandleError" runat="server"></DateInput>--%>
                            </telerik:RadDatePicker></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <telerik:RadButton ID="btnRSUReport" runat="server" Skin="Web20"
                        OnClick="btnRSUReport_Click" Text="RSU Report"
                        Width="180px" OnClientClicking="RSUReportClientClicking">
                    </telerik:RadButton>
                </td>

                <td>
                    
                    
                </td>
                <td><telerik:RadButton ID="btnRSUGrant" runat="server" Skin="Web20"
                        OnClick="btnRSUGrant_Click" Text="RSU Grant" Visible="true"
                        Width="63px"></telerik:RadButton>                   
                </td>
            
            </tr>
            <tr id="divDiscretion" runat="server">
                <td><telerik:RadButton ID="btnDiscretionQuestionnaire" runat="server" Skin="Web20"
                        OnClick="btnDiscretionQuestionnaire_Click" Text="Discretion Questionnaire Report">
                    </telerik:RadButton></td>
                <td></td>
                <td><telerik:RadButton ID="btnDiscretionQuestionnaireStatement" runat="server" Skin="Web20"
                                           Text="Discretion Questionnaire Statements" OnClick="btnDiscretionQuestionnaireStatement_Click" ></telerik:RadButton></td>

            </tr>
            <tr id="divDeferredCash" runat="server">
                <td colspan="3"><telerik:RadButton ID="btnDeferredCash" runat="server" Skin="Web20"
                        OnClick="btnDeferredCashStatement_Click" Text="Deferred Cash Statement">
                   </telerik:RadButton></td>                
            </tr>
             
        </table>
        <br />
        <table style="padding:3px;border:0;width:500px" id="tblAudit" runat="server">
            
            <tr>
                <td style="background-color: #003366; color: White; font-weight: bold;" colspan="2">Audit:</td>
            </tr>
            
            <tr >
                <td><telerik:RadButton ID="btnTableChangesLogFile" runat="server" Skin="Web20"
                        OnClick="btnTableChangesLogFile_Click" Text="Table Changes Log File"
                        Width="180px">
                    </telerik:RadButton></td>
                <td><telerik:RadButton ID="btnAuditLogFile" runat="server" Skin="Web20"
                        OnClick="btnAuditLogFile_Click" Text="Audit Log File"
                        Width="180px">
                    </telerik:RadButton></td>
            </tr>
        </table>
        <br />
        <table style="padding:3px;border:0;width:500px" id="tblCleanDB" runat="server">
            
            <tr>
                <td style="background-color: #003366; color: White; font-weight: bold;" colspan="2">Clean Database:</td>
            </tr>
            
            <tr >
                <td><telerik:RadButton ID="RadButton1" runat="server" Skin="Web20"
                        OnClick="btnCleanDB_Click" Text="Clean Data" OnClientClicking="Confirm"
                        Width="180px">
                    </telerik:RadButton></td>
                <td><telerik:RadButton ID="RadButton2" runat="server" Skin="Web20"  OnClientClicking="Confirm"
                        OnClick="btnCleanStaging_Click" Text="Clean Staging Data"
                        Width="180px">
                    </telerik:RadButton></td>
            </tr>
        </table>
    </div>
</asp:Content>
