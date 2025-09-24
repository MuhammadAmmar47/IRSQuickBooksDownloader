<Serializable()> _
Public Class DataRequest

    Public Class Parameter

        Private _paramName As String
        Private _paramValue As Object

        Public Sub New()

        End Sub

        Public Sub New(ByVal name As String, ByVal value As String)
            ParamName = name
            ParamValue = value
        End Sub

        Public Sub New(ByVal name As String, ByVal value As DateTime)
            ParamName = name
            ParamValue = value
        End Sub


        Public Sub New(ByVal name As String, ByVal value As Integer)
            ParamName = name
            ParamValue = value
        End Sub

        Public Sub New(ByVal name As String, ByVal value As Decimal)
            ParamName = name
            ParamValue = value
        End Sub

        Public Sub New(ByVal name As String, ByVal value As Boolean)
            ParamName = name
            ParamValue = value
        End Sub

        Public Property ParamName() As String
            Get
                ParamName = _paramName
            End Get
            Set(ByVal Value As String)
                _paramName = Value
            End Set
        End Property

        Public Property ParamValue() As Object
            Get
                ParamValue = _paramValue
            End Get
            Set(ByVal Value As Object)
                _paramValue = Value
            End Set
        End Property

    End Class

    Private _commandType As CommandType
    Private _command As String
    Private _transactional As Boolean
    Private _parameters As New Collection
    Private _dataSet As DataSet
    Private _exception As Exception

    Public Sub New()
        CommandType = Data.CommandType.Text
    End Sub

    Public Property CommandType() As CommandType
        Get
            CommandType = _commandType
        End Get
        Set(ByVal Value As CommandType)
            _commandType = Value
        End Set
    End Property
    Public Property Command() As String
        Get
            Command = _command
        End Get
        Set(ByVal Value As String)
            _command = Value
        End Set
    End Property
    Public Property Parameters() As Collection
        Get
            Parameters = _parameters
        End Get
        Set(ByVal Value As Collection)
            _parameters = Value
        End Set
    End Property
    Public Property Transactional() As Boolean
        Get
            Transactional = _transactional
        End Get
        Set(ByVal Value As Boolean)
            _transactional = Value
        End Set
    End Property
    Public Property Exception() As Exception
        Get
            Exception = _exception
        End Get
        Set(ByVal Value As Exception)
            _exception = Value
        End Set
    End Property

    Public Overloads Sub AddParameter(ByVal name As String, ByVal value As String)
        Dim param As New Parameter(name, value)
        Parameters.Add(param)
        param = Nothing
    End Sub

    Public Sub AddReturnParameter()
        Dim param As New Parameter("@ReturnValue", 0)
        Parameters.Add(param)
        param = Nothing
    End Sub

    Public Overloads Sub AddParameter(ByVal name As String, ByVal value As Integer)
        Dim param As New Parameter(name, value)
        Parameters.Add(param)
        param = Nothing
    End Sub

    Public Overloads Sub AddParameter(ByVal name As String, ByVal value As Boolean)
        Dim param As New Parameter(name, value)
        Parameters.Add(param)
        param = Nothing
    End Sub

    Public Overloads Sub AddParameter(ByVal name As String, ByVal value As Decimal)
        Dim param As New Parameter(name, value)
        Parameters.Add(param)
        param = Nothing
    End Sub
    Public Overloads Sub AddParameter(ByVal name As String, ByVal value As DateTime)
        Dim param As New Parameter(name, value)
        If value.Equals(DateTime.MinValue) Then
            param.ParamValue = DBNull.Value
        End If

        Parameters.Add(param)
        param = Nothing
    End Sub

End Class
