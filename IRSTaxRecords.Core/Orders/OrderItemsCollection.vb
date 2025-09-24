Namespace Orders
    Public Class OrderItemsCollection
        Inherits CollectionBase

        Overridable Sub Add(ByVal o As OrderItem)
            Me.List.Add(o)
        End Sub

        Overridable Sub Insert(ByVal index As Integer, ByVal o As OrderItem)
            Me.List.Insert(index, o)
        End Sub

        Overridable Sub Remove(ByVal o As OrderItem)
            Me.List.Remove(o)
        End Sub
        Default Property Item(ByVal index As Integer) As OrderItem
            Get
                ' this operation requires a cast 
                If index <= Me.List.Count - 1 Then
                    Return DirectCast(Me.List.Item(index), OrderItem)
                Else
                    Throw New IndexOutOfRangeException
                End If
            End Get
            Set(ByVal Value As OrderItem)
                Me.List.Item(index) = Value
            End Set
        End Property

    End Class
End Namespace