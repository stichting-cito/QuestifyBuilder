Imports Questify.Builder.Model.ContentModel
Imports System.ComponentModel
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Security

Public Class ResourceMetaDataMultiEdit

    Private WithEvents _entity As EntityClasses.ResourceEntity
    Private _initializingDataSource As Boolean
    Private _currentAction As EntityClasses.ActionEntity
    Private _currentDisplayField As FieldName


    <Description("This event will be raised when data is changed on this control"), Category("ResourceMetaData Control events")> _
    Public Event DataChanged As EventHandler(Of EventArgs)

    Protected Sub OnDataChanged(ByVal e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub



    Public Enum FieldName
        State
        Description
    End Enum

    Public Property DisplayField() As FieldName
        Get
            Return _currentDisplayField
        End Get
        Set(value As FieldName)
            _currentDisplayField = value
        End Set
    End Property

    Public ReadOnly Property CurrentWorkFlowState() As String
        Get
            If _currentAction IsNot Nothing AndAlso _currentAction.StateActionCollection(0) IsNot Nothing Then
                Return _currentAction.StateActionCollection(0).State.Name
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property CurrentWorkFlowAction() As String
        Get
            If _currentAction IsNot Nothing Then
                Return _currentAction.Name
            Else
                Return ""
            End If
        End Get
    End Property

    Public Property ResourceEntity() As EntityClasses.ResourceEntity
        Get
            Return _entity
        End Get

        Set(ByVal value As EntityClasses.ResourceEntity)
            If value IsNot Nothing Then
                _entity = value
                _initializingDataSource = True
                ResourceMetaDataBindingSource.DataSource = _entity
                _initializingDataSource = False

                If _entity.Fields("StateId").DbValue IsNot Nothing Then
                    _currentAction = ResourceFactory.Instance.GetStateAction(CType(_entity.Fields("StateId").DbValue, Integer), "resourceediting")
                Else
                    _currentAction = Nothing
                End If

                If TableLayoutPanelMain.GetControlFromPosition(0, 2) Is Nothing Then
                    TableLayoutPanelMain.SetCellPosition(TableLayoutPanelMain.GetControlFromPosition(0, 0), New TableLayoutPanelCellPosition(0, 2))
                    TableLayoutPanelMain.SetCellPosition(TableLayoutPanelMain.GetControlFromPosition(1, 0), New TableLayoutPanelCellPosition(1, 2))
                Else
                    TableLayoutPanelMain.SetCellPosition(TableLayoutPanelMain.GetControlFromPosition(0, 0), New TableLayoutPanelCellPosition(0, 1))
                    TableLayoutPanelMain.SetCellPosition(TableLayoutPanelMain.GetControlFromPosition(1, 0), New TableLayoutPanelCellPosition(1, 1))
                End If

                If DisplayField = FieldName.State Then
                    StateIdComboBox.Visible = True
                    StateLabel.Visible = True
                    TableLayoutPanelMain.SetCellPosition(StateLabel, New TableLayoutPanelCellPosition(0, 0))
                    TableLayoutPanelMain.SetCellPosition(StateIdComboBox, New TableLayoutPanelCellPosition(1, 0))
                    DescriptionTextBox.Visible = False
                    DescriptionLabel.Visible = False
                ElseIf DisplayField = FieldName.Description Then
                    DescriptionTextBox.Visible = True
                    DescriptionLabel.Visible = True
                    TableLayoutPanelMain.SetCellPosition(DescriptionLabel, New TableLayoutPanelCellPosition(0, 0))
                    TableLayoutPanelMain.SetCellPosition(DescriptionTextBox, New TableLayoutPanelCellPosition(1, 0))
                    StateIdComboBox.Visible = False
                    StateLabel.Visible = False
                End If

            Else
                _currentAction = Nothing
            End If

        End Set
    End Property



    Public Sub New()
        InitializeComponent()
    End Sub



    Public Function IsDirty() As Boolean
        Dim valueToReturn As Boolean = False

        If _entity IsNot Nothing Then
            Return _entity.HasChangesInTopology()
        Else
            Return False
        End If
    End Function

    Public Sub EndEdit()
        ResourceMetaDataBindingSource.EndEdit()
    End Sub

    Public Function CanUpdateResource(ByVal resourceBinDataDirty As Boolean) As Boolean

        Select Case Me.CurrentWorkFlowAction.ToLower
            Case "permit"
                Return True

            Case "prohibit"
                Dim workFlowChangePermitted = PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask, TestBuilderPermissionNamedTask.ChangeWorkflowMetadataWhenProhibittedByState, ResourceEntity.BankId, 0)

                If (IsDirty AndAlso Not (workFlowChangePermitted AndAlso ResourceEntity.OnlyChangesInWorkflowMetaData)) OrElse resourceBinDataDirty Then
                    MessageBox.Show(String.Format(My.Resources.NotAllowedToChangeResourceWithState, Me.ResourceEntity.Name, Me.CurrentWorkFlowState), "Save current entity", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                Else
                    Return True
                End If

            Case "warn"
                If (Me.IsDirty OrElse resourceBinDataDirty) Then
                    Select Case MessageBox.Show(String.Format(My.Resources.SaveChangesForResourceWithState, Me.ResourceEntity.Name, Me.CurrentWorkFlowState), "Save current entity", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                        Case DialogResult.Yes
                            Return True
                        Case Else
                            Return False
                    End Select
                Else
                    Return False
                End If

            Case Else
                Return True
        End Select
    End Function

    Public Function CanNotUpdate() As Boolean
        If Me.CurrentWorkFlowAction.ToLower = "prohibit" Then
            Return True
        Else
            Return False
        End If
    End Function



    Private Sub StateIdComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StateIdComboBox.SelectedIndexChanged
        If Not _initializingDataSource Then
            OnDataChanged(New EventArgs)
        End If
    End Sub



    Private Sub ResourceMetaData_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.DesignMode Then
            Dim availableStates As EntityCollection
            availableStates = ResourceFactory.Instance.GetAvailableStates()

            StateEntityBindingSource.DataSource = availableStates
        End If
    End Sub


End Class
