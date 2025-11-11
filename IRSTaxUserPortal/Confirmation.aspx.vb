Public Class Confirmation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim formType As String = Request.QueryString("form")

            If Not String.IsNullOrEmpty(formType) Then
                Select Case formType
                    Case "4506"
                        'lblFormHeading.Text = "Form 4506"
                        lnkSubmitAnother.Text = "<i class='fas fa-plus-circle me-2'></i> Submit another Form 4506"
                        lnkSubmitAnother.NavigateUrl = "order_4506.aspx"
                        img4506.Visible = True
                    Case "8821"
                        'lblFormHeading.Text = "Form 8821"
                        lnkSubmitAnother.Text = "<i class='fas fa-plus-circle me-2'></i> Submit another Form 8821"
                        lnkSubmitAnother.NavigateUrl = "order_8821.aspx"
                        img8821.Visible = True
                    Case Else
                        'lblFormHeading.Text = "Form SSV"
                        lnkSubmitAnother.Text = "<i class='fas fa-plus-circle me-2'></i> Submit another Form"
                        lnkSubmitAnother.NavigateUrl = "orderSSV.aspx"
                        imgSSV.Visible = True
                End Select
            End If
        End If
    End Sub

End Class