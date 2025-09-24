<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IRSTaxClientAdminMaster.Master"
    CodeBehind="Default.aspx.vb" Inherits="IRSTaxRecords.Admin._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr>
        <td>
            <h3>Click company to add associates</h3>
        </td>
    </tr>
        <tr>
            <td>
                <bvc:WebPageMessage ID="msg" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="hypAddNewClient" runat="server" Text="Add new client" NavigateUrl="ClientDetail.aspx"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="grdMain" runat="server">
                    <MasterTableView AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Client Name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypClientName" runat="server" Text='<%#Eval("ClientName")%>' NavigateUrl='<%# "ClientDetail.aspx?ClientID=" & Eval("ID") %>'></asp:HyperLink>
                                    
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="LoginUserName">
                                <ItemTemplate>
                                    <%#Eval("LoginUserName")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="AddStatistics">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAddStats" runat="server" Checked='<%# Eval("AddStatistics") %>' Enabled="false" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
