Imports System.Xml.Serialization

<Serializable>
<XmlRoot("Targets")>
Public Class TargetCollection(Of templateTarget As TargetBase)
    Inherits List(Of TargetBase)

    Public Overloads ReadOnly Property Item(targetName As String) As templateTarget
        Get
            For Each target As templateTarget In Me
                If target.Name.Equals(targetName, StringComparison.InvariantCultureIgnoreCase) Then
                    Return target
                End If
            Next

            Return Nothing
        End Get
    End Property


    Public Function HasTarget(targetName As String) As Boolean

        For Each target As TargetBase In Me
            If target.Name.Equals(targetName, StringComparison.InvariantCultureIgnoreCase) Then
                Return True
                Exit For
            End If
        Next

        Return False
    End Function

End Class