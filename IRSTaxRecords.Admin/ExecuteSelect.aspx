<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ExecuteSelect.aspx.vb" Inherits="IRSTaxRecords.Admin.ExecuteSelect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptMAnagerMain" runat="server">
        </asp:ScriptManager>
        <div>
            <table>
                <tr>
                    <td>
                        <bvc:WebPageMessage ID="msg" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Enter Password: <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" Height="200px" Width="1000px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnExecute" runat="server" Text="RUN" Width="100" />
                        &nbsp;&nbsp;
                        <asp:CheckBox ID="chkIgnorePaging" runat="server" Text="Ignore Paging" />
                        <asp:Button ID="btnExportToExcel" runat="server" Text="Export" Width="100" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblExecutetime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="grdMain" runat="server" AllowPaging="True" GridLines="None" AllowSorting="true">
                            <MasterTableView PageSize="100">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <PagerStyle Position="TopAndBottom" AlwaysVisible="true" />
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
