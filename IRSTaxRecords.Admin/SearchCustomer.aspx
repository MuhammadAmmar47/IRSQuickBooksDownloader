<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IRSWebAdminMaster.Master" CodeBehind="SearchCustomer.aspx.vb" Inherits="IRSTaxRecords.Admin.SearchCustomer" %>
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
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                    <table>
                        <tr>
                            <td>
                                Search For:
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearchFor" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" />
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
                            <telerik:GridTemplateColumn HeaderText="CustomerID">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerID" runat="server" Text='<%# Eval("CustomerID") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="UserName">
                                <ItemTemplate>
                                    <%#Eval("UserID")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn HeaderText="Company Name">
                                <ItemTemplate>
                                    <%#Eval("CompanyName")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Name">
                                <ItemTemplate>
                                    <%#Eval("Name")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            
                            <telerik:GridTemplateColumn HeaderText="">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypCreatEOrder" runat="server" Text="Create Order" NavigateUrl='<%# "CreateOrder.aspx?UserID=" & Eval("CustomerID") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
