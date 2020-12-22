Imports System.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class ListValueDetailsGrid
    Private _properties As ListCustomBankPropertyEntity

    Public Sub Initialize(ByVal listCustomBankProperty As ListCustomBankPropertyEntity)
        UpdateData(listCustomBankProperty)
    End Sub

    Public Sub UpdateData(customBankProperty As ListCustomBankPropertyEntity)
        ListViewData.Items.Clear()
        _properties = customBankProperty

        Dim items = From cp In customBankProperty.ListValueCustomBankPropertyCollection
                    Select New ListViewItem(New String() {cp.Name, cp.Title})

        ListViewData.Items.AddRange(items.ToArray())
    End Sub

    Friend Sub SelectFirstRow()
        If ListViewData.Items.Count > 0 Then
            ListViewData.Items(0).Selected = True
            ListViewData.Select()
        End If
    End Sub

    Public ReadOnly Property RowCount() As Integer
        Get
            Return ListViewData.Items.Count
        End Get
    End Property

    Public ReadOnly Property SelectedRow() As Object
        Get
            If ListViewData.SelectedItems.Count = 0 Then
                Return Nothing
            End If

            Dim name As String = ListViewData.SelectedItems.Item(0).SubItems(0).Text
            Return _properties.ListValueCustomBankPropertyCollection.FirstOrDefault(Function(f) f.Name = name)
        End Get
    End Property
End Class
