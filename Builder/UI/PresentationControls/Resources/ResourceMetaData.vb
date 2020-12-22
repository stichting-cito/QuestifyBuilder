Imports Questify.Builder.Model.ContentModel
Imports System.ComponentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Security

Public Class ResourceMetaData

    Private WithEvents _entity As IPropertyEntity
    Private _isNameChangeable As Boolean
    Private _initializingDataSource As Boolean
    Private _currentAction As EntityClasses.ActionEntity


    <Description("This event will be raised when data is changed on this control"), Category("ResourceMetaData Control events")> _
    Public Event DataChanged As EventHandler(Of EventArgs)

    Protected Sub OnDataChanged(ByVal e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub

    Public Event OpenResourcePropertyDialogButtonClicked As EventHandler(Of EventArgs(Of Integer))

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

    Public Property ResourceEntity() As IPropertyEntity
        Get
            Return _entity
        End Get

        Set(ByVal value As IPropertyEntity)
            If value IsNot Nothing Then
                _entity = value

                _initializingDataSource = True

                ResourceMetaDataBindingSource.DataSource = _entity

                _initializingDataSource = False

                If _entity.Fields("StateId") IsNot Nothing AndAlso _entity.Fields("StateId").DbValue IsNot Nothing Then
                    _currentAction = ResourceFactory.Instance.GetStateAction(CType(_entity.Fields("StateId").DbValue, Integer), "resourceediting")
                Else
                    _currentAction = Nothing
                End If
            Else
                _currentAction = Nothing
            End If
        End Set
    End Property

    <Browsable(True), Description("Is the name field changable in this control"), DefaultValue(True)> _
    Public Property IsNameChangable() As Boolean
        Get
            Return _isNameChangeable
        End Get
        Set(ByVal value As Boolean)
            _isNameChangeable = value
        End Set
    End Property

    Public Sub New()
        InitializeComponent()

        _isNameChangeable = True
    End Sub

    Public Function IsDirty() As Boolean

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
                    MessageBox.Show(String.Format(My.Resources.TestEditor_v2_EditNotAllowed, Me.ResourceEntity.Name, Me.CurrentWorkFlowState), "Save current entity", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Public Function NeedsVersionIncrementForStatus() As Boolean
        Dim stateIdField = ResourceEntity.Fields("StateId")
        If stateIdField Is Nothing Then Return False

        If Not stateIdField.CurrentValue.Equals(stateIdField.DbValue) Then
            Dim action = ResourceFactory.Instance.GetStateAction(CType(_entity.Fields("StateId").DbValue, Integer), "resourcesaving")
            If action Is Nothing Then Return False

            Select Case action.Name.ToLower
                Case "incrementversion"
                    Return True
            End Select
        End If

        Return False
    End Function

    Private Sub Control_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DescriptionTextBox.TextChanged
        If Not _initializingDataSource Then
            OnDataChanged(New EventArgs)
        End If
    End Sub

    Private Sub StateIdComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StateIdComboBox.SelectedIndexChanged
        If Not _initializingDataSource Then
            If (StateIdComboBox.SelectedValue IsNot Nothing) Then
                _entity.State = GetState(CInt(StateIdComboBox.SelectedValue))
            End If
            OnDataChanged(New EventArgs)
        End If
    End Sub

    Private Function GetState(ByVal stateId As Integer) As StateEntity
        If StateEntityBindingSource.DataSource Is Nothing Then Return Nothing
        For Each stateEntity As StateEntity In CType(StateEntityBindingSource.DataSource, EntityCollection)
            If stateEntity.StateId = stateId Then
                Return stateEntity
            End If
        Next

        Return Nothing
    End Function

    Private Sub ResourceMetaData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not DesignMode Then
            StateEntityBindingSource.DataSource = ResourceFactory.Instance.GetAvailableStates()
        End If
    End Sub

    Private Sub OpenResourcePropertyDialogButton_Click(sender As Object, e As EventArgs) Handles OpenResourcePropertyDialogButton.Click
        RaiseEvent OpenResourcePropertyDialogButtonClicked(sender, New EventArgs(Of Integer) With {.Value = 4})
    End Sub

End Class
