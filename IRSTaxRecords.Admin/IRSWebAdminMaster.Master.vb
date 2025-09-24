Public Partial Class IRSWebAdminMaster
    Inherits System.Web.UI.MasterPage

    Public Sub HideMainMenu()
        Me.mnuMain.Visible = False
    End Sub

    Public Sub SetLeftBorderColor(ByVal colorName As String)
        divLeft.Style.Remove("border-left")
        divLeft.Style.Add("border-left", "solid 20px " & colorName)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            divLeft.Style.Add("border-left", "solid 20px Maroon")
        End If

    End Sub

End Class