Imports System.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class ConceptStructureGrid
    Private _allConceptTypes As New List(Of KeyValuePair(Of ConceptTypeEntity, String))
    Private _properties As ConceptStructureCustomBankPropertyEntity

    Public Sub Initialize(ByVal customBankProperty As ConceptStructureCustomBankPropertyEntity, ByVal conceptTypes As List(Of KeyValuePair(Of ConceptTypeEntity, String)))
        _allConceptTypes = conceptTypes

        UpdateData(customBankProperty)
    End Sub

    Public Sub UpdateData(customBankProperty As ConceptStructureCustomBankPropertyEntity)
        ListViewData.Items.Clear()
        _properties = customBankProperty

        Dim items = From cp In customBankProperty.ConceptStructurePartCustomBankPropertyCollection
                    Select New ListViewItem(New String() {cp.Name, cp.Title, GetConceptType(cp)})

        ListViewData.Items.AddRange(items.ToArray())
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Friend Sub SelectFirstRow()
        If ListViewData.Items.Count > 0 Then
            ListViewData.Items(0).Selected = True
            ListViewData.Select()
        End If
    End Sub

    Public ReadOnly Property SelectedRow() As Object
        Get
            If ListViewData.SelectedItems.Count = 0 Then
                Return Nothing
            End If

            Dim name As String = ListViewData.SelectedItems.Item(0).SubItems(0).Text
            Return _properties.ConceptStructurePartCustomBankPropertyCollection.FirstOrDefault(Function(f) f.Name = name)
        End Get
    End Property

    Public ReadOnly Property RowCount() As Integer
        Get
            Return ListViewData.Items.Count
        End Get
    End Property

    Private Function GetConceptType(cp As ConceptStructurePartCustomBankPropertyEntity) As String
        cp.ConceptType = _allConceptTypes.First(Function(c) c.Key.ConceptTypeId = cp.ConceptTypeId).Key
        Return _allConceptTypes.First(Function(c) c.Key.ConceptTypeId = cp.ConceptTypeId).Value
    End Function
End Class