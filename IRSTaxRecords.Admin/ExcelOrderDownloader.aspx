<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ExcelOrderDownloader.aspx.vb" Inherits="IRSTaxRecords.Admin.ExcelOrderDownloader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Export Tax Verification Records to Excel</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr><table border="1" width="1" bordercolorlight="#000000" cellspacing="0" bordercolordark="#FFFFFF" cellpadding="0">
	<tr>
		<td width="100%">
			<table border="0" width="422" cellspacing="0" cellpadding="0" height="1">
				<tr>
					<td width="420" colspan="3" bgcolor="#FFCC00" height="1"><img border="0" src="nothing.gif" width="420" height="8"></td>
				</tr>
				<tr>
					<td width="420" colspan="3" bgcolor="#000000" height="1"><img border="0" src="nothing.gif" width="1" height="1"></td>
				</tr><td width="425" colspan="3" height="2"><img border="0" src="export.gif" width="300" height="50"><font face="Arial" size="2"><h2></h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" ForeColor="Red""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pblExport" runat="server" DefaultButton="btnExport">
                    <table>
                        <tr>
                            <td>
                                From
                            </td>
                            <td>
                                <asp:TextBox ID="txtFrom" runat="server" Width="90"></asp:TextBox>
                            </td>
                            <td>
                                To:
                            </td>
                            <td>
                                <asp:TextBox ID="txtto" runat="server" Width="90"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align:center">
                                <asp:Button ID="btnExport" runat="server" Text="Export" Width="100" />
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="grDMain" runat="server"></asp:DataGrid>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
