Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq

Public Class DeptTreeStructureUserControl

    Public Sub New()
        InitializeComponent()

    End Sub

    Public Sub New(childTreeStructurePartCustomBankPropertyEntity As ChildTreeStructurePartCustomBankPropertyEntity, validTreeStructures As IEnumerable(Of TreeStructurePartCustomBankPropertyEntity))
        Me.New()

        _structurePartCustomBankPropertyEntity = childTreeStructurePartCustomBankPropertyEntity
        Me.Name = childTreeStructurePartCustomBankPropertyEntity.childTreeStructurePartCustomBankPropertyId.ToString()

        ComboBoxDep.DataSource = New BindingSource(validTreeStructures.OrderBy(Function(i) i.Name), Nothing)
        ComboBoxDep.DisplayMember = "Name"
    End Sub

    Public Property ChildTreeStructurePartCustomBankPropertyEntity As ChildTreeStructurePartCustomBankPropertyEntity
        Get
            Return CType(_structurePartCustomBankPropertyEntity, ChildTreeStructurePartCustomBankPropertyEntity)
        End Get
        Set(value As ChildTreeStructurePartCustomBankPropertyEntity)
            _structurePartCustomBankPropertyEntity = value
        End Set
    End Property

End Class

