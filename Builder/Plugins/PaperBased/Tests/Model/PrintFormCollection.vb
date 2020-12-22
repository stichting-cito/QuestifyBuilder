Imports System.ComponentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Plugins.PaperBased


Namespace Global.Cito.Tester.ContentModel

    Public Class PrintFormCollection
        Inherits BindingList(Of PrintForm)

        Public Overloads Function Contains(type As PrintFormType) As Boolean
            For Each printForm As PrintForm In Me.Items
                If printForm.Type = type Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function GetPrintFormByType(type As PrintFormType) As PrintForm
            For Each printForm As PrintForm In Me.Items
                If printForm.Type = type Then
                    Return printForm
                End If
            Next
            Return Nothing
        End Function

        Protected Overrides Sub InsertItem(index As Integer, item As PrintForm)
            If item.Type = PrintFormType.UserDefinedBooklet OrElse Not Me.Contains(item.Type) Then
                MyBase.InsertItem(index, item)
            Else
                Throw New ContentModelException($"PrintType with type: {item.Type.ToString} already in collection.")
            End If
        End Sub
        Protected Overrides Sub RemoveItem(index As Integer)
            MyBase.RemoveItem(index)
        End Sub

        Public Overloads Sub Remove(type As PrintFormType)
            If GetPrintFormByType(type) IsNot Nothing Then
                If Not MyBase.Remove(GetPrintFormByType(type)) Then
                    Throw New ContentModelException(String.Format("Failed to delete printform from collection"))
                End If
            End If
        End Sub

        Public Overloads Sub Remove(id As Guid)
            For Each printForm As PrintForm In Me.Items
                If printForm.Id = id Then
                    MyBase.Remove(printForm)
                    Exit For
                End If
            Next
        End Sub


        Private Sub PrintFormCollection_ListChanged(sender As Object, e As ListChangedEventArgs) Handles Me.ListChanged

        End Sub

    End Class

End Namespace