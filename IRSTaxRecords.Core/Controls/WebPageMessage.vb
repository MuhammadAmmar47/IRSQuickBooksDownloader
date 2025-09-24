Imports System.ComponentModel
Imports System.Web.UI

Namespace WebControls
    <ToolboxData("<{0}:WebPageMessage runat=server></{0}:WebPageMessage>"), ParseChildren(False)> Public Class WebPageMessage
        Inherits System.Web.UI.Control

        <Bindable(True), Category("Behavior"), Description("Message to display"), DefaultValue("")> Property Message() As String
            Get
                Dim s As String = ViewState("Message")
                If s = Nothing Then
                    Return String.Empty
                Else
                    Return s
                End If
            End Get

            Set(ByVal Value As String)
                ViewState("Message") = Value
            End Set
        End Property
        <Bindable(True), Category("Behavior"), Description("What icon to display"), DefaultValue(1)> Property MessageMode() As WebPageMessageMode
            Get
                Dim mm As WebPageMessageMode = ViewState("MessageMode")
                If mm = Nothing Then
                    Return WebPageMessageMode.Information
                Else
                    Return mm
                End If
            End Get
            Set(ByVal Value As WebPageMessageMode)
                ViewState("MessageMode") = Value
            End Set
        End Property

        Public Sub Clear()
            Me.MessageMode = WebPageMessageMode.Information
            Me.Message = ""
        End Sub

        Public Sub ShowOK(ByVal msg As String)
            Me.MessageMode = WebPageMessageMode.OK
            Me.Message = msg
        End Sub

        Public Sub ShowInformation(ByVal msg As String)
            Me.MessageMode = WebPageMessageMode.Information
            Me.Message = msg
        End Sub

        Public Sub ShowError(ByVal msg As String)
            Me.MessageMode = WebPageMessageMode.ErrorMessage
            Me.Message = msg
        End Sub
        Public Sub ShowError(ByVal msg As String, ByVal ex As Exception)
            Me.MessageMode = WebPageMessageMode.ErrorMessage
            If ex Is Nothing Then
                Me.Message = msg
            Else
                Me.Message = msg & vbCrLf & ex.StackTrace.Replace(vbCrLf, "<br />")
            End If

        End Sub
        Public Sub ShowWarning(ByVal msg As String)
            Me.MessageMode = WebPageMessageMode.Warning
            Me.Message = msg
        End Sub

        Public Sub ShowQuestion(ByVal msg As String)
            Me.MessageMode = WebPageMessageMode.Question
            Me.Message = msg
        End Sub

        Public Sub ShowException(ByVal ex As System.Exception)
            Me.Message = ex.Message & "<br>" & ex.Source & "<br>" & ex.StackTrace
            Me.MessageMode = WebPageMessageMode.ErrorMessage
        End Sub

        Protected Overrides Sub Render(ByVal output As System.Web.UI.HtmlTextWriter)
            Dim msg As String = Me.Message
            If msg.Length > 0 Then
                output.Write("<div class=""WebPageMessage"">")
                Select Case Me.MessageMode
                    Case WebPageMessageMode.ErrorMessage
                        output.Write("<div class=""WebPageMessageError"">")
                    Case WebPageMessageMode.Information
                        output.Write("<div class=""WebPageMessageInformation"">")
                    Case WebPageMessageMode.OK
                        output.Write("<div class=""WebPageMessageOK"">")
                    Case WebPageMessageMode.Question
                        output.Write("<div class=""WebPageMessageQuestion"">")
                    Case WebPageMessageMode.Warning
                        output.Write("<div class=""WebPageMessageWarning"">")
                End Select
                output.Write("<div class=""Message"">")
                output.Write(msg)
                output.Write("</div></div>")
                output.Write("</div>")
            End If

        End Sub

        Protected Overrides Function CreateControlCollection() As System.Web.UI.ControlCollection
            Return New EmptyControlCollection(Me)
        End Function
    End Class
    Public Enum WebPageMessageMode
        Information = 1
        Warning = 2
        ErrorMessage = 3
        OK = 4
        Question = 5
    End Enum
End Namespace