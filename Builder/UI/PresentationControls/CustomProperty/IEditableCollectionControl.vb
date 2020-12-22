Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Interface IEditableCollectionControl
    Sub Initialize(ByVal customBankProperty As CustomBankPropertyEntity, ByVal initAsReadOnly As Boolean)
    Sub AddItem()
    Sub RemoveItem()
    Sub Saved()
    Sub UndoRemoveEntities()

    ReadOnly Property SelectedEntityId As Guid
    ReadOnly Property RemovedEntities As Entitycollection
    ReadOnly Property ErrorMessage As String

    ReadOnly Property RemoveConfirmed As Dictionary(Of Guid, Boolean)
    ReadOnly Property RemovedEntitiesAllConfirmed As Boolean
End Interface