<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IRSTaxClientAdminMaster.Master"
    CodeBehind="ClientDetail.aspx.vb" Inherits="IRSTaxRecords.Admin.ClientDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnSaveClient">
        <table style="vertical-align: top">
            <tr>
                <td style="text-align:right">
                    <asp:HyperLink ID="hypListAllClients" runat="server" Text="List all Clients" NavigateUrl="Default.aspx"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    <bvc:WebPageMessage ID="msg" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <h3>
                        Create Customer Administration section</h3>
                </td>
            </tr>
            <tr>
                <td>
                    Name of Company:
                    <asp:TextBox ID="txtNameOfCompany" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Create User Id:
                    <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                    Create Password:
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Add Statistics:
                    <asp:CheckBox ID="chkAddStatistics" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSaveClient" runat="server" Text="Save Client Details" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlUsers" runat="server" DefaultButton="btnSaveUsers">
                        <table>
                            <tr>
                                <td>
                                    <h3>
                                        Associate User IDs:</h3>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="vertical-align:top">
                                                User ID :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUserId1" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSaveUsers" runat="server" Text="Associate Users" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <h3>
                                        Existing Users Associated</h3>
                                            </td>
                                            <td style="text-align:right">
                                                <asp:DropDownList ID="ddlDoAction" runat="server">
                                                    <asp:ListItem Text="Select Action" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Remove user association" Value="RemoveUserAssociation"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Button ID="btnDoAction" runat="server" Text="Go" />
                                            </td>
                                        </tr>
                                    </table>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="grdMain" runat="server" AllowMultiRowSelection="true">
                                    <ClientSettings Selecting-AllowRowSelect="true" />
                                        <MasterTableView AutoGenerateColumns="false">
                                            <Columns>
                                                <telerik:GridClientSelectColumn />
                                                <telerik:GridTemplateColumn HeaderText="UserID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerID" runat="server" Text='<%#Eval("CustomerID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="UserName">
                                                    <ItemTemplate>
                                                        <%#Eval("UserId")%>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Company">
                                                    <ItemTemplate>
                                                        <%#Eval("CompanyName")%>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
