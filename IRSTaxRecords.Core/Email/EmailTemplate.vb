Namespace Email
    Public Class EmailTemplate
        Private _Id As Integer
        Private _Subject As String = ""
        Private _TemplateName As String = ""
        Private _Body As String = ""
        Private _SenderName As String
        Private _SenderEmail As String = ""
        Private _IsHtml As Boolean = True

        Public Property ID() As Integer
            Get
                Return _Id
            End Get
            Set(ByVal value As Integer)
                _Id = value
            End Set
        End Property
        Public Property Subject() As String
            Get
                Return _Subject
            End Get
            Set(ByVal value As String)
                _Subject = value
            End Set
        End Property
        Public Property Name() As String
            Get
                Return _TemplateName
            End Get
            Set(ByVal value As String)
                _TemplateName = value
            End Set
        End Property
        Public Property Body() As String
            Get
                Return _Body
            End Get
            Set(ByVal value As String)
                _Body = value
            End Set
        End Property
        Public Property SenderName() As String
            Get
                Return _SenderName
            End Get
            Set(ByVal value As String)
                _SenderName = value
            End Set
        End Property
        Public Property SenderEmail() As String
            Get
                Return _SenderEmail
            End Get
            Set(ByVal value As String)
                _SenderEmail = value
            End Set
        End Property
        Public Property IsHtml() As Boolean
            Get
                Return _IsHtml
            End Get
            Set(ByVal value As Boolean)
                _IsHtml = value
            End Set
        End Property

        Public Shared Function GetTemplate(ByVal _type As EMAIL_TYPE) As EmailTemplate
            Dim fileName As String = ""
            Dim subject As String = ""

            Select Case _type
                Case EMAIL_TYPE.PASSWORD_REMINDER
                    fileName = "ForgetPassword.html"
                    subject = "Your Password at aGreenLawn.com"
                Case EMAIL_TYPE.WELCOME
                    fileName = "Welcome.html"
                    subject = "Welcome to aGreenLawn.com"
                Case EMAIL_TYPE.CONTACT_US
                    fileName = "ContactUs.html"
                    subject = "Thank you for contacting aGreenLawn"
                Case EMAIL_TYPE.REFER_A_FRIEND
                    fileName = "ReferAFriend.html"
                    subject = "New Referral."
                Case EMAIL_TYPE.ONLINE_ANALYSIS
                    fileName = "OnlineQuote.html"
                    subject = "Thank you for requesting analysis of your lawn"
                Case EMAIL_TYPE.ORDER_SERVICES
                    fileName = "NewOrder.html"
                    subject = "New Order at aGreenLawn"
                Case Else
                    Throw New Exception("Invalid type of email")
            End Select

            Dim templateFile As String = TemplatesPath & fileName
            Dim body As String = ReadFileToString(templateFile)
            Dim template As New EmailTemplate
            With template
                .SenderEmail = AppSettings.EmailFromName
                .SenderName = AppSettings.EmailFromName

                .Body = Body
                .IsHtml = True
                .Subject = subject
            End With

            Return template

        End Function

        Private Shared ReadOnly Property TemplatesPath() As String
            Get
                Dim path As String = System.Web.HttpContext.Current.Server.MapPath("~/EmailTemplates")
                If Not path.EndsWith("\") Then path += "\"
                Return path
            End Get
        End Property
        Private Shared Function ReadFileToString(ByVal filePath As String) As String
            If Not System.IO.File.Exists(filePath) Then
                Return Nothing
            End If

            Dim reader As New System.IO.StreamReader(filePath)
            Dim text As String = reader.ReadToEnd
            reader.Close()

            Return text
        End Function
    End Class

    Public Enum EMAIL_TYPE
        WELCOME
        PASSWORD_REMINDER
        CONTACT_US
        REFER_A_FRIEND
        ONLINE_ANALYSIS
        ORDER_SERVICES
    End Enum
End Namespace

