<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FileUploadTest.aspx.vb"
    Inherits="IRSTaxRecords.Admin.FileUploadTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="divMain">
                <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="onClickButton();"/>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <img src="indicator_mozilla_blu.gif" /><br />
                uploading.............
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    
    <script language="javascript" type="text/javascript">
        function onClickButton() {
            document.getElementById('divMain').style.display = 'none';
        }
        
    </script>
    </form>
</body>
</html>
