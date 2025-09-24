<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IRSWebAdminMaster.Master"
    CodeBehind="CreateOrder.aspx.vb" Inherits="IRSTaxRecords.Admin.CreateOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
        .BoldStyle
        {
            font-weight:bold;
        }
        
        .TableRow
        {
            background-color:#c0c0c0;
        }
        .TableRowAlternate
        {
            background-color:#c0c0c0;
        }
        
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                <bvc:WebPageMessage ID="msg" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlUserSearch" runat="server" DefaultButton="btnSearchUser">
                    <table>
                        <tr>
                            <td>
                                User ID/UserName:
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearchUser" runat="server"  Text="Search User"/>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="BoldStyle">
            User Id: <asp:Label ID="lblUserId" runat="server"></asp:Label><br />
                <asp:Label ID="lblUserName" runat="server"></asp:Label> <br />
                <asp:Label ID="lblOrderSummary" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td  class="TableRow">
                <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnCreateOrder">
                    <table cellpadding="10">
                        <tr id="trLoanNumber" runat="server">
                            <td style="text-align: right">
                                <b>Loan Number:</b>
                                <asp:TextBox ID="txtLoanNumber" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <table>
                                    <tr>
                                        <td>
                                            <b>
                                            Name:
                                            </b>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        <b>
                                            SSN:
                                            </b>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSSN" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="">
                                <div style="font-weight:bold">
                                    Tax Years Requested:
                                </div>
                                <div style="text-align: right">
                                    <asp:CheckBoxList ID="chkTaxyears" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                        <asp:ListItem Text="2013" Value="2013"></asp:ListItem>
                                        <asp:ListItem Text="2012" Value="2012"></asp:ListItem>
                                        <asp:ListItem Text="2011" Value="2011"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="">
                                <div style="font-weight:bold">
                                    Tax Forms Requested:
                                </div>
                                <div style="text-align: right">
                                    <asp:RadioButtonList ID="rdoTaxForms" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="1040" Value="1040"></asp:ListItem>
                                        <asp:ListItem Text="1040/W2" Value="1040/W2"></asp:ListItem>
                                        <asp:ListItem Text="1120" Value="1120"></asp:ListItem>
                                        <asp:ListItem Text="1065" Value="1065"></asp:ListItem>
                                        <asp:ListItem Text="W-2" Value="W-2"></asp:ListItem>
                                        <asp:ListItem Text="1099" Value="1099"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="">
                                <div style="font-weight:bold">
                                    Requested Delivery:
                                </div>
                                <div style="text-align: left">
                                    <asp:CheckBox ID="chkEmail" Checked="true" runat="server" Text="Email" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox ID="chkFax" runat="server" Text="Fax" />
                                    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnCreateOrder" runat="server" Text="Create Order" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
