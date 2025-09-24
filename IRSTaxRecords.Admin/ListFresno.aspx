<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IRSWebAdminMaster.Master"
    CodeBehind="ListFresno.aspx.vb" Inherits="IRSTaxRecords.Admin.ListFresno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <table width="800px" style="text-align: left; border-left: solid 20px white; background-color: White;">
            <tr>
                <td>
                    <bvc:WebPageMessage ID="msg" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <font face="Arial" size="3"><b>Income Verification Express Service</b></font>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <font size="2" face="Arial">Participant Number:&nbsp;3000028 </font>
                            </td>
                            <td style="text-align: right">
                                <font size="2" face="Arial">Date:
                                    <asp:Label ID="lblListName" runat="server"></asp:Label>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <font size="2" face="Arial">Participant's Name: NATIONAL TAX VERIFICATION </font>
                            </td>
                            <td style="text-align: right">
                                <font size="2" face="Arial">Phone Number: 1-888-941-4506       </font>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <font size="2" face="Arial">Participant’s E-Mail </font>
                            </td>
                            <td style="text-align: right">
                            <font size="2" face="Arial">Fax Number: 1-800-482-9158</font>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <font size="2" face="Arial">Batch Number</font> : <%=ListID%>
                            </td>
                            <td style="text-align: right">
                            <font size="2" face="Arial">Total Number of Years Requested: <asp:Label ID="lblTotalTaxYears" runat="server"></asp:Label></font>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
                       
            
            <tr id="trListType" runat="server" visible="false">
                <td>
                    <font size="3" face="Arial">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;LIST TYPE: <b>W-2</b></font>
                    <asp:Label ID="lblListTypeName" Visible="false" runat="server">/</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="grdMain" runat="server" GridLines="None" CellPadding="0" AllowPaging="false"
                        ShowFooter="true" AllowMultiRowSelection="true" AllowSorting="true" PageSize="50"
                        Width="100%">
                        <ExportSettings>
                            <Pdf PageWidth="8.5in" PageHeight="11in" PageTopMargin="" PageBottomMargin="" PageLeftMargin=""
                                PageRightMargin="" PageHeaderMargin="" PageFooterMargin=""></Pdf>
                        </ExportSettings>
                        <ClientSettings Selecting-AllowRowSelect="true">
                        </ClientSettings>
                        <MasterTableView AutoGenerateColumns="False">
                            <RowIndicatorColumn Visible="False">
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn Resizable="False">
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="">
                                    <ItemStyle Width="25px" />
                                    <ItemTemplate>
                                        <%#Eval("RowOrder")%>.
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="First Four Characters of Last Name<br>/First Initial or<br>First Four Characters of Business Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hypName" runat="server" Text='<%# Eval("RequestNameWithInitials") %>'
                                            NavigateUrl='<%# "http://www.irstaxrecords.com/_/new/newnotify/delivery2.asp?action=" & Eval("fldOrderNumber")  & "&pagetype=l" %>'>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Last Four Digits <br>of TIN">
                                    <ItemTemplate>
                                        <%#Right(Eval("fldssnno"), 4)%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Tax Period<br>Ending" DataField="YearsString"
                                    Aggregate="Custom" FooterText=" ">
                                    <ItemTemplate>
                                        <%#Eval("YearsString")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Form Number">
                                    <ItemTemplate>
                                        <%#Eval("FormTypeName")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="IRS Use Only">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Order#" Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hypOrderNumber" NavigateUrl='<%# "javascript:openWindow(" & Eval("fldOrderNumber") &");" %>'
                                            runat="server" Text='<%#Eval("fldOrderNumber") %>'>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom" AlwaysVisible="true" />
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                </td>
            </tr>
            <tr>
                <td>
                    <p align="right">
                        <b><font size="3" face="Arial">THIS IS A <asp:Label ID="lblListTypeName2" Visible="true" runat="server">
                            </asp:Label>
                            LIST</a></font></b></p>
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                        &nbsp;</p>
                    <p>
                        &nbsp;</p>
                </td>
            </tr>
        </table>
    </center>

    <script language="javascript" type="text/javascript">
        function openWindow(Oid) {
            window.open("http://www.irstaxrecords.com/_/new/newnotify/commentDetails.asp?id=" + Oid, "myWindow", "status=1,scrollbars=1,resizable=1,width=600,height=400");

        }

    </script>

</asp:Content>
