<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChangePassword.aspx.vb" Inherits="IRSTaxRecords.Admin.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <link href="/Styles/WebPageMessage.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <h3>Change Password</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <bvc:WebPageMessage ID="msg" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlChangePassword" runat="server" DefaultButton="btnChangePassword">
                        <table>
                            <tr>
                                <td>
                                    UserId:
                                </td>
                                <td>
                                    <asp:Label ID="lblUserID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Old Password:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    New Password:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" onblur="copyPassword();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Confirm Password:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNewPassword2" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align:center">                                                                
                                    <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    
                                      
                    
                    
                </td>
            </tr>
        </table>
    </div>     
    </form>
</body>
</html>
