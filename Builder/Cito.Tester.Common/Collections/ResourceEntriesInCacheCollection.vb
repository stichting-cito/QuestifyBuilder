Public Class ResourceEntriesInCacheCollection
    Inherits List(Of ResourceEntry)


    Public Overloads Property Item(name As String) As ResourceEntry
        Get
            For i As Integer = 0 To Me.Count - 1
                If Me.Item(i).Name = name Then
                    Return Me.Item(i)
                End If
            Next

            Return Nothing
        End Get
        Set(value As ResourceEntry)
            Me.Item(name) = value
        End Set
    End Property

    Public Shadows Sub Add(item As ResourceEntry)
        If item Is Nothing Then
            Throw New ResourceManagerException(My.Resources.Error_ResourceEntriesInCacheCollection_itemParNotSet)
        End If
        If Me.Item(item.Name) Is Nothing Then
            MyBase.Add(item)
        End If
    End Sub
End Class
