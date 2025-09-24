<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IRSTaxClientAdminMaster.Master"
    CodeBehind="Statistics.aspx.vb" Inherits="IRSTaxRecords.Admin.Statistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td style="text-align:right">
                <asp:HyperLink ID="hypBackToHome" runat="server" Text="Home Page" NavigateUrl="Default.aspx"></asp:HyperLink>
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
                    <%=ClientName%>
                    Administration Section</h3>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnShowResults">
                    <table>
                        <tr>
                            <td>
                                Choose dates: From:
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dtFromDate" runat="server">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                To:
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dtToDate" runat="server">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:Button ID="btnShowResults" runat="server" Text="Show Statistics" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlSearchREsults" runat="server">
                    <table>
                        <tr>
                            <td>
                                <table style="background-color: #c4ffc4">
                                    <tr>
                                        <td>
                                            Total Number of orders between
                                            <%=FromDate.ToShortDateString() %>
                                            and
                                            <%=ToDate.ToShortDateString()%>:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotalOrders" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Average turn time for all orders
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAvgTurnTimeForAllOrders" runat="server"></asp:Label>
                                            hours
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fastest Turn time VIP Rush
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFastestTurnTime" runat="server"></asp:Label>
                                            hours
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h3>
                                    Requests made by processor</h3>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="grdMain" runat="server">
                                    <MasterTableView AutoGenerateColumns="false">
                                        <AlternatingItemStyle BackColor="LightGray" />
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Processor Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProcessorName" runat="server" Text='<%# Eval("ProcessorName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Number of Orders Submitted">
                                                <itemstyle horizontalalign="Center" />
                                                <ItemTemplate>                                                    
                                                    <asp:Label ID="lblNumberOfOrders" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Percentage of company orders placed by this processor"> 
                                                <itemstyle horizontalalign="Center" />
                                                <ItemTemplate>                                                    
                                                    <asp:Label ID="lblPercentOrdered" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Number of Rejects by this processor">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumberOfRejcts" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 <table border="1" width="100%" bordercolor="#CCCCCC" cellspacing="0" cellpadding="7">
                                      <tr>
                                        <td width="43%" bgcolor="#C4FFC4"><font face="Arial" size="2">Total
                                          number of rejects:</font></td>
                                        <td width="57%" bgcolor="#C4FFC4"><font face="Arial" size="2"><asp:Label ID="lblTotalNumberOfRejcts" runat="server"></asp:Label></font></td>
                                      </tr>
                                      <tr>
                                        <td width="43%"><font face="Arial" size="2">Percent
                                          of rejects returned for incorrect
                                          address</font></td>
                                        <td width="57%"><font face="Arial" size="2"><asp:Label ID="lblRejectReason_InvalidAddress" runat="server"></asp:Label></font></td>
                                      </tr>
                                      <tr>
                                        <td width="43%"><font face="Arial" size="2">Percent
                                          of rejects returned for incorrect
                                          Social Security Number</font></td>
                                        <td width="57%"><font face="Arial" size="2"><asp:Label ID="lblRejectReason_SSN" runat="server"></asp:Label></font></td>
                                      </tr>
                                      <tr>
                                        <td width="43%"><font face="Arial" size="2">Percent
                                          of rejects returned for Name not
                                          matching record</font></td>
                                        <td width="57%"><font face="Arial" size="2"><asp:Label ID="lblRejectReason_NameNotMatch" runat="server"></asp:Label></font></td>
                                      </tr>
                                      <tr>
                                        <td width="43%"><font face="Arial" size="2">Percent
                                          of rejects returned for Un-processable
                                          (identity theft)</font></td>
                                        <td width="57%"><font face="Arial" size="2"><asp:Label ID="lblRejectReason_UnProcessable" runat="server"></asp:Label></font></td>
                                      </tr>
                                      <tr>
                                        <td width="43%"><font face="Arial" size="2">Percent
                                          of rejects returned for Illegible</font></td>
                                        <td width="57%"><font face="Arial" size="2"><asp:Label ID="lblRejectReason_Illegible" runat="server"></asp:Label></font></td>
                                      </tr>
                                      <tr>
                                        <td width="43%"><font face="Arial" size="2">Percent
                                          of rejects returned for altered</font></td>
                                        <td width="57%"><font face="Arial" size="2"><asp:Label ID="lblRejectReason_Altered" runat="server"></asp:Label></font></td>
                                      </tr>
                                      <tr>
                                        <td width="43%"><font face="Arial" size="2">Percent
                                          of rejects returned for Invalid date</font></td>
                                        <td width="57%"><font face="Arial" size="2"><asp:Label ID="lblRejectReason_InvalidDate" runat="server"></asp:Label></font></td>
                                      </tr>
                                    </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
