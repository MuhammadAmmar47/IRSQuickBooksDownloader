<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IRSTaxClientAdminMaster.Master"
    CodeBehind="Default.aspx.vb" Inherits="IRSTaxRecords.Admin._Default1" %>

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
            <td style="background-color: Gray">
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                    <table>
                        <tr>
                            <td>
                                Find Records:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFindRecords" runat="server" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" />
                            </td>
                            <td>
                                (Search by Taxpayer name or Social Security Number)
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr id="trStats" runat="server">
            <td style="text-align:center">
                <asp:HyperLink ID="hypStats" runat="server"  NavigateUrl="Statistics.aspx">
                    <asp:Image ID="imgStats" runat="server" ImageUrl="~/images/IRSSTATS.gif" />
                </asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="grdMain" runat="server">                    
                    <MasterTableView AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Branch">
                                <ItemTemplate>
                                    <%#Eval("CompanyName")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Account Login">
                                <ItemTemplate>
                                    Login To <asp:LinkButton ID="btnLoginto" runat="server" Text='<%#Eval("Name")%>'></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="UserID">
                                <ItemTemplate>
                                    <%#Eval("UserID")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Email">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypEmail" runat="server" NavigateUrl='<%#"mailto:" & Eval("Email")%>' Text='<%#Eval("Email")%>'></asp:HyperLink>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    
    <script language="javascript" type="text/javascript">
        function fnSubmit2(uid, pass) {
            document.forms[0].userId.value = uid;
            document.forms[0].password.value = pass;
            document.forms[0].action = "../newwelcomeframe.asp";
            document.forms[0].submit();            
        }
    </script>
    <input type="hidden" name="userId" value="" ID="userId">
    <input type="hidden" name="password" value="" ID="password">

</asp:Content>
