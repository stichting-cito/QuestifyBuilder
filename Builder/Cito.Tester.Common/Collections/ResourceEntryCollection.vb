Imports System.Collections.ObjectModel

Public Class ResourceEntryCollection
    Inherits List(Of ResourceEntry)

    Public Overloads Property Item(name As String) As ResourceEntry
        Get
            For i As Integer = 0 To Me.Count - 1
                If Me.Item(i).Name.Equals(name, StringComparison.OrdinalIgnoreCase) Then
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

    Public Sub AddResourceEntry(name As String)
        Me.Add(New ResourceEntry(name))
    End Sub


    Public Function ToStringCollection() As ReadOnlyCollection(Of String)
        Dim entries As New List(Of String)

        For Each entry As ResourceEntry In Me
            entries.Add(entry.Name)
        Next

        Return New ReadOnlyCollection(Of String)(entries)
    End Function

    Public Function ContainsResource(resourceName As String) As Boolean
        Dim returnValue As Boolean = False

        For Each entry As ResourceEntry In Me
            If entry.Name = resourceName Then
                returnValue = True
                Exit For
            End If
        Next

        Return returnValue
    End Function

End Class
