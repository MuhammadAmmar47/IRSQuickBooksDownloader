<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IRSTaxClientAdminMaster.Master" CodeBehind="SearchResult.aspx.vb" Inherits="IRSTaxRecords.Admin.SearchResult" %>

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
                <h3>
                    <%=ClientName%>
                    Administration Section</h3>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblQuery" runat="server"></asp:Label>
            </td>
        </tr>
        <tr id="trStats" runat="server">
            <td style="text-align: center">
                <asp:HyperLink ID="hypStats" runat="server" NavigateUrl="Statistics.aspx">
                    <asp:Image ID="imgStats" runat="server" ImageUrl="~/images/IRSSTATS.gif" />
                </asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td style="background-color: Gray">
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                    <table>
                        <tr>
                            <td>Find Records:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFindRecords" runat="server" Width="300px"></asp:TextBox>
                            </td>
                            <td>(Search by Taxpayer name or Social Security Number)
                            </td>
                        </tr>
                        <tr>
                            <td>Record Status:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRecordStatus" runat="server">
                                    <asp:ListItem Text="ALL" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Rejected" Value="r"></asp:ListItem>
                                    <asp:ListItem Text="Processed" Value="d"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>From:
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="dtFromDate" runat="server"></telerik:RadDatePicker>
                                        </td>
                                        <td>To:
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="dtToDate" runat="server"></telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align: right">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlAction" runat="server">
                    <asp:ListItem Text="Select Action" Value=""></asp:ListItem>
                    <asp:ListItem Text="Download Records" Value="DownloadRecords"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnDoAction" runat="server" Text="Go" />
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="grdMain" runat="server" AllowMultiRowSelection="true" AllowPaging="true" PageSize="100">
                    <ClientSettings Selecting-AllowRowSelect="true" />
                    <MasterTableView AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridClientSelectColumn />
                            <telerik:GridTemplateColumn HeaderText="TaxPayer Name">
                                <ItemStyle Width="250px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestName" runat="server" Text='<%#Eval("fldRequestName").ToString.Replace(" 1099", "").Replace(" 17", "").Replace(" 18", "")%>'></asp:Label>
                                    <asp:Label ID="lblOrderNumber" runat="server" Visible="false" Text='<%# Eval("fldordernumber") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="File Number">
                                <ItemStyle Width="75px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFileNumber" runat="server"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Order Date">
                                <ItemStyle Width="75px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderDaste" runat="server"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Form Type">
                                <ItemStyle Width="75px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFormType" runat="server" Text='<%#Eval("fldTypeOfForm")%>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="SSN">
                                <ItemStyle Width="75px" />
                                <ItemTemplate>
                                    <%#Eval("fldSSNNo")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Years Requested">
                                <ItemTemplate>
                                    <asp:Label ID="lblTaxYears" runat="server"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Order Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblORderStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Response Time" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblResponseTime" runat="server"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Records">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnRecordFile" runat="server" Text='<%# Eval("fldPDF") %>' CommandName="PDF" CommandArgument='<%# Eval("fldPDF") %>'></asp:LinkButton>
                                    <asp:LinkButton ID="btnRejectedFile" runat="server" Text="" CommandName="RejectFile"></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypRejectReason" Target="_blank" runat="server"></asp:HyperLink>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <PagerStyle AlwaysVisible="true" Position="TopAndBottom" />
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
