Imports System.Xml

Public Module Globals
    Public Function GetNodeText(ByVal node As XmlNode, ByVal xPath As String) As String
        Try
            Return node.SelectSingleNode(xPath).InnerText
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Module
