Namespace Content
    Public Class OrderPadFile
        Private _ID As Integer
        Private _UserID As Integer
        Private _UploadedOn As DateTime
        Private _FileName As String = ""
        Private _FileNameReal As String = ""
        Private _ErrorMessage As String = ""

        Public Property ErrorMessage() As String
            Get
                Return _ErrorMessage
            End Get
            Set(ByVal value As String)
                _ErrorMessage = value
            End Set
        End Property
        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal Value As Integer)
                _ID = value
            End Set
        End Property

        Public Property UserID() As Integer
            Get
                Return _UserID
            End Get
            Set(ByVal Value As Integer)
                _UserID = value
            End Set
        End Property

        Public Property UploadedOn() As DateTime
            Get
                Return _UploadedOn
            End Get
            Set(ByVal Value As DateTime)
                _UploadedOn = value
            End Set
        End Property

        Public Property FileName() As String
            Get
                Return _FileName
            End Get
            Set(ByVal Value As String)
                _FileName = value
            End Set
        End Property

        Public Property FileNameReal() As String
            Get
                Return _FileNameReal
            End Get
            Set(ByVal Value As String)
                _FileNameReal = value
            End Set
        End Property
    End Class
End Namespace