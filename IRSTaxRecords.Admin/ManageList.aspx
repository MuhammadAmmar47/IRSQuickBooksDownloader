<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IRSWebAdminMaster.Master" CodeBehind="ManageList.aspx.vb" Inherits="IRSTaxRecords.Admin.ManageList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<center>
    <table style="width:100%">
        <tr>
            <td>
                <bvc:WebPageMessage ID="msg" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlNewList" runat="server" DefaultButton="btnProceed">
                    <table style="width:100%">
                        <tr>
                            <td>
                            <font size="3" face="Arial">
                                Start a new list
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color:#efefef">
                                <table style="width:100%">
                                    <tr>
                                        <td>
                                            List Type:
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="lstListType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                <asp:ListItem Text="1040" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="W-2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="SSN" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Specials" Value="4"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnProceed" runat="server" Text="Proceed" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="grdMain" runat="server">
                    <MasterTableView AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypList" runat="server" Text='<%# Eval("fldListName") %>' NavigateUrl='<%# "ListFresno.aspx?id=" & Eval("fldListID") %>'></asp:HyperLink> <asp:Label ID="lblRemaining" runat="server" Text='<%# " (" & Eval("TotalPending") & " Remaining)" %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    </center>
</asp:Content>
