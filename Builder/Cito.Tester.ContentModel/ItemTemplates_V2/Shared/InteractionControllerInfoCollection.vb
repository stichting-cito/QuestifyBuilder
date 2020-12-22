Public Class InteractionControllerInfoCollection
    Inherits List(Of InteractionControllerInfo)



    Sub New()
    End Sub




    Public Overloads ReadOnly Property Item(id As String) As InteractionControllerInfo
        Get
            For Each iControllerInfo As InteractionControllerInfo In Me
                If iControllerInfo.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase) Then
                    Return iControllerInfo
                End If
            Next

            Return Nothing
        End Get
    End Property




    Public Overloads Function Contains(id As String) As Boolean
        Return Me.item(id) IsNot Nothing
    End Function


End Class