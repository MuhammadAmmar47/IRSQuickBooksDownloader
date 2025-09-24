<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IRSTaxRecords.Admin.Master"
    CodeBehind="SetIRSBatchNumber.aspx.vb" Inherits="IRSTaxRecords.Admin.SetIRSBatchNumber" %>

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
                <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnSearchBatch">
                    <table>
                        <tr>
                            <td>
                                Local Batch # From:
                            </td>
                            <td>
                                <asp:TextBox ID="txtBatchNumberFrom" runat="server" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Local Batch # To:
                            </td>
                            <td>
                                <asp:TextBox ID="txtBatchNumberTo" runat="server" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSearchBatch" runat="server" Text="Search Batches" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlUpload" runat="server">
                    <table>
                        <tr>
                            <td>
                                Select file to upload:
                            </td>
                            <td>
                                <asp:FileUpload ID="flUpload" runat="server" />
                            </td>
                            <td>
                                <asp:Button id="btnUploadfile" runat="server" Text="Upload file" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnDownloadsample" runat="server" Text="Download Sample" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="grdMain" runat="server" GridLines="None" CellPadding="0" AllowPaging="False"
                    AllowSorting="true" PageSize="400" Width="100%">
                    <ExportSettings>
                        <Pdf PageWidth="8.5in" PageHeight="11in" PageTopMargin="" PageBottomMargin="" PageLeftMargin=""
                            PageRightMargin="" PageHeaderMargin="" PageFooterMargin=""></Pdf>
                    </ExportSettings>
                    <MasterTableView AutoGenerateColumns="False">
                        <RowIndicatorColumn Visible="False">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Resizable="False">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Local Batch#">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocalBatchNumber" runat="server" Text='<%# Eval("fldListID") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="IRS Batch#">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtIRSBatchNumber" runat="server" Width="70px" Text='<%# Eval("IRSBatchNumber") %>'></asp:TextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom" AlwaysVisible="true" />
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Button ID="btnUpdateBatchNumbers" runat="server" Text="Update Batch #s" />
            </td>
        </tr>
    </table>
</asp:Content>
