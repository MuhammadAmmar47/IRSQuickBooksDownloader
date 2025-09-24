<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IRSTaxRecords.Admin.Master" CodeBehind="SendListToIRS.aspx.vb" Inherits="IRSTaxRecords.Admin.SendListToIRS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                <table>
                    <tr>
                        <td>
                            Email From:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmailFrom" runat="server" text="admin@irstaxrecords.com"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email To:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmailto" runat="server" Text="info@irs.gov"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Subject:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSubject" runat="server" Text="Reject Reason for Batch# "></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadEditor ID="txtEditor" ShowSubmitCancelButtons="False" runat="server" Width="800px" Height="450"></telerik:RadEditor>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSendEmail" runat="server" Text="Send Email" />
            </td>
        </tr>
    </table>
</asp:Content>

