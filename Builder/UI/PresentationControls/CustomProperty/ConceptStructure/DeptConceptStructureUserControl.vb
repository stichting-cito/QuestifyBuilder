Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq

Public Class DeptConceptStructureUserControl
    Private ReadOnly _allPossibleConceptStructures As IEnumerable(Of ConceptStructurePartCustomBankPropertyEntity)

    Public Sub New()
        InitializeComponent()

    End Sub

    Public Sub New(childConceptStructurePartCustomBankPropertyEntity As ChildConceptStructurePartCustomBankPropertyEntity, allPossibleConceptStructures As IEnumerable(Of ConceptStructurePartCustomBankPropertyEntity))
        Me.New()

        _structurePartCustomBankPropertyEntity = childConceptStructurePartCustomBankPropertyEntity
        _allPossibleConceptStructures = allPossibleConceptStructures.OrderBy(Function(i) i.Name)
    End Sub

    Private Sub DeptConceptStructureUserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        ComboBoxDep.DataSource = New BindingSource(_allPossibleConceptStructures, Nothing)
        ComboBoxDep.DisplayMember = "Name"

        ComboBoxDep.SelectedItem = _allPossibleConceptStructures.FirstOrDefault(Function(i) CType(i, ConceptStructurePartCustomBankPropertyEntity).ConceptStructurePartCustomBankPropertyId = ChildConceptStructurePartCustomBankPropertyEntity.ChildConceptStructurePartCustomBankPropertyId)
    End Sub

    Public Property ChildConceptStructurePartCustomBankPropertyEntity As ChildConceptStructurePartCustomBankPropertyEntity
        Get
            Return CType(_structurePartCustomBankPropertyEntity, ChildConceptStructurePartCustomBankPropertyEntity)
        End Get
        Set(value As ChildConceptStructurePartCustomBankPropertyEntity)
            _structurePartCustomBankPropertyEntity = value
        End Set
    End Property

End Class

