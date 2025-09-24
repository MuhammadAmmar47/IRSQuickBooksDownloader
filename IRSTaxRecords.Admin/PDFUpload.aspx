<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PDFUpload.aspx.vb" Inherits="IRSTaxRecords.Admin.PDFUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/WebPageMessage.css" rel="stylesheet" type="text/css" />
    
</head>
<body style="padding-left:0px; padding-top:0px">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="scriptManager1" runat="server">
    </telerik:RadScriptManager>
    <h2><%=UserID%></h2>
        <table style="padding-left:0px; padding-top:0px" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div id="divMain">
                        <table border="1" width="1" bordercolorlight="#000000" cellspacing="0" bordercolordark="#FFFFFF"
                            cellpadding="0">
                            <tr>
                                <td>
                                    <bvc:WebPageMessage ID="msg" runat="server" />
                                </td>
                            </tr>
                            <tr id="trFile">
                                <td width="100%">
                                    <table border="0" width="100%" cellspacing="0" cellpadding="0" height="1">
                                        <tr>
                                            <td bgcolor="#FFCC00" height="1">
                                                <img border="0" src="images/nothing.gif" width="250" height="8">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#000000" height="1">
                                                <img border="0" src="images/nothing.gif" width="1" height="1">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="21">
                                                <img border="0" src="orderuploadingsystem.gif" width="300" height="50"><font
                                                    face="Arial" size="2"></font>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="white-space: nowrap">
                                                            <h3>
                                                            <font
                                                    face="Arial">
                                                                &nbsp;&nbsp;1.&nbsp; Select PDF file:&nbsp;
                                                                </font>
                                                            </h3>
                                                        </td>
                                                        <td>
                                                            <telerik:RadUpload ID="fileUpload" runat="server" MaxFileInputsCount="1" ControlObjectsVisibility="None">
                                                            </telerik:RadUpload>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trSubmit">
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width:250px">
                                                <h3>
                                                <font face="Arial">
                                                    &nbsp;&nbsp;2. Click to Upload the file:
                                                    </font>
                                                </h3>
                                            </td>
                                            <td style="text-align:left">
                                                <font face="Arial" size="2">
                                                    <asp:Button ID="btnUploadFile" runat="server" Text="Upload File" OnClientClick="onClickButton();" />
                                                </font>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divProgress" style="display: none; text-align:center; padding-top:100px; padding-left:150px">
                        <img src="/images/animated_progress_bar.gif" alt="Uploading, Please wait..." /><br />
                        Uploading File Please wait.....
                    </div>
                </td>
            </tr>
        </table>
    

    <script language="javascript" type="text/javascript">
        function onClickButton() {
            document.getElementById('divMain').style.display = 'none';
            document.getElementById('divProgress').style.display = 'block';
        }
        
    </script>

    </form>
</body>
</html>
