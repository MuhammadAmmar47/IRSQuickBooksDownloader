Imports System
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions


Namespace Email
    Public Class PropertyMapper

        Private _entity As Object

        Public Sub New(ByVal entity As Object)
            Me.Entity = entity
        End Sub 'New


        Public Property Entity() As Object
            Get
                Return _entity
            End Get
            Set(ByVal Value As Object)
                _entity = Value
            End Set
        End Property

        Public Function MapContent(ByVal content As String) As String
            Dim pattern As String = "#\?(?'property'\S+?)\?#"

            Return Regex.Replace(content, pattern, New MatchEvaluator(AddressOf RegexMatchEvaluation), RegexOptions.ExplicitCapture)
        End Function
        Private Function RegexMatchEvaluation(ByVal match As Match) As String
            Try
                'get the property name (named group of the regex)
                Dim propertyName As String = match.Groups("property").Value

                'try to get a property handle from the business object
                Dim pi As PropertyInfo = Me.Entity.GetType().GetProperty(propertyName)
                'do not replace anything if no such property exists
                If pi Is Nothing Then
                    Return match.Value
                End If
                'return the property value
                Dim propertyValue As Object = pi.GetValue(Entity, Nothing)
                Try
                    If pi.PropertyType.Equals(Type.GetType("System.DateTime", False)) Then
                        If Not propertyValue Is Nothing Then
                            propertyValue = CType(propertyValue, DateTime).ToShortDateString
                        End If
                    End If
                Catch ex As Exception

                End Try
                If Not (propertyValue Is Nothing) Then
                    Return propertyValue.ToString()
                Else
                    Return ""
                End If
            Catch
            End Try

            Return String.Empty
        End Function
    End Class
End Namespace