Imports System.ComponentModel

Namespace Entities

    <Serializable> _
    Public Class StaticGroupEntryCollection
        Inherits BindingList(Of StaticGroupEntry)

        Public Function GetEntryByIdentifier(resourceIdentifierToSearch As String) As StaticGroupEntry
            Dim returnValue As StaticGroupEntry = Nothing

            For Each entry As StaticGroupEntry In Me
                If entry.ResourceIdentifier = resourceIdentifierToSearch Then
                    returnValue = entry
                    Exit For
                End If
            Next

            Return returnValue
        End Function

    End Class

End Namespace
