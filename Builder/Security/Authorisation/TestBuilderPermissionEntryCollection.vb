Imports System.Linq

<Serializable()> _
Public Class TestBuilderPermissionEntryCollection
    Inherits List(Of TestBuilderPermissionEntry)

    Public Function GetEntryByKey(ByVal key As String) As TestBuilderPermissionEntry
        Return Me.FirstOrDefault(Function(e) e.Key.Equals(key))
    End Function

End Class
