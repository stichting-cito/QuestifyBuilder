Imports System.ComponentModel

Public Class SimpleChoiceCollection
    Inherits BindingList(Of SimpleChoice)

    Public Sub AddRange(collection As IEnumerable(Of SimpleChoice))
        For Each choice As SimpleChoice In collection
            Me.Add(choice)
        Next
    End Sub
End Class