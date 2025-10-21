﻿Imports IRSTaxRecords.Core

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblErrorDetails.Visible = False
        End If

    End Sub

    Private Sub LoginButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim isSuccess As Boolean = StoreInstance.LoginUser(Me.txtUsername.Text.Trim, Me.txtPassword.Text, False)
            If (isSuccess) Then
                Response.Redirect("welcome")
            Else
                FailureText.Text = "Invalid username/password"
            End If
        Catch ex As Exception
            FailureText.Text = ex.Message
            lblErrorDetails.Visible = True
        End Try
    End Sub

End Class