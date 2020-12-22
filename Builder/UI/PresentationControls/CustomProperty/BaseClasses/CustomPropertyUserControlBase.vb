Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports System.ComponentModel
Imports System.Linq
Imports Enums
Imports Questify.Builder.Logic.Service.Factories

Public Class CustomPropertyUserControlBase
    Inherits UserControl
    Implements IEditableCollectionControl

    Private _customBankProperty As CustomBankPropertyEntity
    Private _removedEntities As New EntityCollection()
    Private _controlIsReadOnly As Boolean = False

    Private _removeConfirmed As New Dictionary(Of Guid, Boolean)

    Public ReadOnly Property RemoveConfirmed As Dictionary(Of Guid, Boolean) Implements IEditableCollectionControl.RemoveConfirmed
        Get
            Return _removeConfirmed
        End Get
    End Property

    Public ReadOnly Property RemovedEntitiesAllConfirmed As Boolean Implements IEditableCollectionControl.RemovedEntitiesAllConfirmed
        Get
            Return Not RemovedEntities.Any(Function(entity)
                                               Dim entityId As Guid = DirectCast(entity, ListValueCustomBankPropertyEntity).ListValueBankCustomPropertyId
                                               Return Not RemoveConfirmed.ContainsKey(entityId) OrElse Not RemoveConfirmed(entityId)
                                           End Function)
        End Get
    End Property

    Protected Overridable Function AskConfirmationForReferencedValues(entityId As Guid, customPropertyType As CustomBankPropertyType) As Boolean
        Dim isReferenced = BankFactory.Instance.IsCustomBankPropertyValueReferenced(entityId, customPropertyType)
        Dim continueDespiteReference = True
        If isReferenced Then
            If MessageBox.Show(My.Resources.WarningDeleteCustomProperties, String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
                _removeConfirmed.Add(entityId, True)
            Else
                continueDespiteReference = False
            End If
        End If

        Return continueDespiteReference
    End Function

    Public Overridable Sub Initialize(ByVal customBankProperty As CustomBankPropertyEntity, ByVal initAsReadOnly As Boolean) Implements IEditableCollectionControl.Initialize
        _customBankProperty = customBankProperty
        _controlIsReadOnly = initAsReadOnly

        If initAsReadOnly Then
            DisableControls()
        End If
    End Sub

    Public Overridable ReadOnly Property SelectedEntityId As Guid Implements IEditableCollectionControl.SelectedEntityId
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Protected Overridable ReadOnly Property IsControlReadOnly As Boolean
        Get
            Return _controlIsReadOnly
        End Get
    End Property

    Protected Overridable Sub DisableControls()

    End Sub

    Public ReadOnly Property CustomBankProperty As CustomBankPropertyEntity
        Get
            Return _customBankProperty
        End Get
    End Property

    Public Overridable Sub UndoRemovedEntities()
        _removedEntities.Clear()
    End Sub

    <Browsable(False)>
    Public ReadOnly Property RemovedEntities As EntityCollection Implements IEditableCollectionControl.RemovedEntities
        Get
            Return _removedEntities
        End Get
    End Property

    Public Overridable Sub AddItem() Implements IEditableCollectionControl.AddItem
        Throw New NotSupportedException()
    End Sub

    Public Overridable Sub RemoveItem() Implements IEditableCollectionControl.RemoveItem
        Throw New NotSupportedException()
    End Sub

    Public Overridable Sub Saved() Implements IEditableCollectionControl.Saved

    End Sub

    Public Sub UndoRemoveEntities() Implements IEditableCollectionControl.UndoRemoveEntities
        UndoRemovedEntities()
    End Sub

    Public Overridable ReadOnly Property ErrorMessage As String Implements IEditableCollectionControl.ErrorMessage
        Get
            Return String.Empty
        End Get
    End Property
End Class
