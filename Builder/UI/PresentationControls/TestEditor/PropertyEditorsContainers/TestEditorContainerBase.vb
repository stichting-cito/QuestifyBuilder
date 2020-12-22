Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic
Imports Questify.Builder.Security

Imports System.ComponentModel
Imports System.Text

Public Class TestEditorContainerBase


    Private _currentPropertyEditorControls As List(Of TestEditorControlBase)
    Private _testEntity As AssessmentTestResourceEntity



    Public Sub New()
        InitializeComponent()

        _currentPropertyEditorControls = New List(Of TestEditorControlBase)
    End Sub



    Public ReadOnly Property ContainsValidationError() As Boolean
        Get
            Dim returnValue As Boolean = False

            For Each propertyEditor As TestEditorControlBase In _currentPropertyEditorControls
                If propertyEditor.ContainsValidationError Then
                    returnValue = True
                    Exit For
                End If
            Next

            Return returnValue
        End Get
    End Property

    Public ReadOnly Property CurrentPropertyEditorControls() As List(Of TestEditorControlBase)
        Get
            Return _currentPropertyEditorControls
        End Get
    End Property

    <Bindable(False)> _
    Public ReadOnly Property ErrorMessage() As String
        Get
            Dim returnValue As New StringBuilder()

            For Each propertyEditor As TestEditorControlBase In _currentPropertyEditorControls
                If returnValue.Length > 0 Then
                    returnValue.AppendLine()
                End If

                returnValue.Append(propertyEditor.ErrorMessage)
            Next

            Return returnValue.ToString()
        End Get
    End Property

    Public Property TestEntity() As AssessmentTestResourceEntity
        Get
            Return _testEntity
        End Get
        Set(ByVal value As AssessmentTestResourceEntity)
            _testEntity = value

            SetTestEntityToPropertyEditors()
        End Set
    End Property



    Private Sub SetTestEntityToPropertyEditors()
        For Each propertyEditor As TestEditorControlBase In _currentPropertyEditorControls
            propertyEditor.TestEntity = _testEntity
        Next
    End Sub



    Protected Sub CreateFrameForPropertyEditorAndAddToContainer(ByVal propEditor As TestEditorControlBase, ByVal frameTitle As String)
        Dim groupBoxControl As New GroupBox
        groupBoxControl.Text = frameTitle
        groupBoxControl.Padding = New Padding(8)
        groupBoxControl.Margin = New Padding(8, 8, 8, 8)
        groupBoxControl.TabStop = True
        groupBoxControl.TabIndex = Me.Controls.Count + 1
        groupBoxControl.AutoSizeMode = AutoSizeMode.GrowOnly
        groupBoxControl.AutoSize = True
        Me.Controls.Add(groupBoxControl)
        groupBoxControl.Dock = DockStyle.Top
        groupBoxControl.BringToFront()

        groupBoxControl.Controls.Add(propEditor)
        propEditor.Dock = DockStyle.Top

        groupBoxControl.Visible = DirectCast(propEditor, ITestEditorPropertyEditor).HasFieldsToFillByUser
    End Sub

    Protected Sub OnDataChanged(ByVal e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub

    Protected Sub OnCommandExecuteRequest(ByVal e As CommandExecuteRequestEventArgs)
        RaiseEvent CommandExecuteRequest(Me, e)
    End Sub



    Public Sub HandleTestDesignPermissionChange(ByVal permission As TestDesignPermission)
        For Each propertyEditor As TestEditorControlBase In _currentPropertyEditorControls
            propertyEditor.HandleTestDesignPermissionChange(permission)
        Next
    End Sub

    Public Sub ResetDatasource()
        For Each propertyEditor As TestEditorControlBase In _currentPropertyEditorControls
            propertyEditor.ResetDatasource()
        Next
    End Sub



    <Description("This event will be raised when data is changed on this control"), _
Category("TestEditorControlBase events")> _
    Public Event DataChanged As EventHandler(Of EventArgs)

    <Description("This event will be raised when an action is initiated from a property editor."), _
Category("PropertyControl Base-events")> _
    Public Event CommandExecuteRequest As EventHandler(Of CommandExecuteRequestEventArgs)


End Class