Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq

Public Class TreeStructureGrid

    Private _properties As TreeStructureCustomBankPropertyEntity


    Public Sub Initialize(ByVal customBankProperty As TreeStructureCustomBankPropertyEntity)
        ListViewData.Items.Clear()
        UpdateData(customBankProperty)
    End Sub

    Public Sub UpdateData(customBankProperty As TreeStructureCustomBankPropertyEntity)
        ListViewData.Items.Clear()
        _properties = customBankProperty

        Dim items = From cp In customBankProperty.TreeStructurePartCustomBankPropertyCollection
                    Select New ListViewItem(New String() {cp.Name, cp.Title})

        ListViewData.Items.AddRange(items.ToArray())
    End Sub

    Friend Sub SelectFirstRow()
        If ListViewData.Items.Count > 0 Then
            ListViewData.Items(0).Selected = True
            ListViewData.Select()
        End If
    End Sub

    Friend Sub SelectRowWithMatchingName(name As String)
        Dim item As ListViewItem = ListViewData.FindItemWithText(name)

        If Not item Is Nothing Then
            item.Selected = True
        End If
    End Sub

    Public ReadOnly Property SelectedRow() As Object
        Get
            If ListViewData.SelectedItems.Count = 0 Then
                Return Nothing
            End If

            Dim name As String = ListViewData.SelectedItems.Item(0).SubItems(0).Text
            Return _properties.TreeStructurePartCustomBankPropertyCollection.FirstOrDefault(Function(f) f.Name = name)
        End Get
    End Property

End Class
