Imports System.Collections.ObjectModel

Public Class ResourceManifestMetaDataEntryReferenceCollection
    Inherits List(Of ResourceManifestMetaDataEntryReference)

    Public Overloads Property Item(name As String) As ResourceManifestMetaDataEntryReference
        Get
            For i As Integer = 0 To Me.Count - 1
                If Me.Item(i).Name.Equals(name, StringComparison.OrdinalIgnoreCase) Then
                    Return Me.Item(i)
                End If
            Next

            Return Nothing
        End Get
        Set(value As ResourceManifestMetaDataEntryReference)
            Me.Item(name) = value
        End Set
    End Property

    Public Shadows Sub Add(item As ResourceManifestMetaDataEntryReference)
        If item Is Nothing Then
            Throw New NoNullAllowedException("item has no value")
        End If
        If Me.Item(item.Name) Is Nothing Then
            MyBase.Add(item)
        End If
    End Sub

    Public Function ToStringCollection() As ReadOnlyCollection(Of String)
        Dim entries As New List(Of String)

        For Each entry As ResourceManifestMetaDataEntryReference In Me
            entries.Add(entry.Name)
        Next

        Return New ReadOnlyCollection(Of String)(entries)
    End Function
End Class
