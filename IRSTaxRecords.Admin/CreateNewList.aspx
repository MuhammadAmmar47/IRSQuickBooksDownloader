<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IRSTaxRecords.Admin.Master"
    CodeBehind="CreateNewList.aspx.vb" Inherits="IRSTaxRecords.Admin.CreateNewList" %>

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
                <asp:Panel ID="pnlNewList" runat="server" DefaultButton="btnNewList">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblListTypeName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtListName" runat="server" Width="250px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnNewList" runat="server" Text="Start New List" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
