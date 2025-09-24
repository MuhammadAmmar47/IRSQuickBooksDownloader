<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IRSTaxClientAdminMaster.Master" CodeBehind="RejectedOrderInfo.aspx.vb" Inherits="IRSTaxRecords.Admin.RejectedOrderInfo" %>
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
                            <asp:Label ID="lblInfoType" runat="server"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="300px" Height="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:Button ID="btnSubmitInfo" runat="server" Text="Submit corrected information" />
                            <asp:Button ID="btnClose" runat="server" Text="Close" Width="200px" OnClientClick="window.close(); return;" />
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
        
    </table>
</asp:Content>
