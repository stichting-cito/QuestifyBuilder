Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic
Imports Questify.Builder.Security
Imports System.ComponentModel
Imports System.Text
Public Class TestPackageEditorContainerBase

    Private _testPackageEntity As TestPackageResourceEntity


    Public Sub New()
        InitializeComponent()

        CurrentPropertyEditorControls = New List(Of TestPackageEditorControlBase)
    End Sub




    Public ReadOnly Property ContainsValidationError() As Boolean
        Get
            Dim returnValue As Boolean = False

            For Each propertyEditor As TestPackageEditorControlBase In CurrentPropertyEditorControls
                If propertyEditor.ContainsValidationError Then
                    returnValue = True
                    Exit For
                End If
            Next

            Return returnValue
        End Get
    End Property


    Public ReadOnly Property CurrentPropertyEditorControls() As List(Of TestPackageEditorControlBase)

    <Bindable(False)>
    Public ReadOnly Property ErrorMessage() As String
        Get
            Dim returnValue As New StringBuilder()

            For Each propertyEditor As TestPackageEditorControlBase In CurrentPropertyEditorControls
                If returnValue.Length > 0 Then
                    returnValue.AppendLine()
                End If

                returnValue.Append(propertyEditor.ErrorMessage)
            Next

            Return returnValue.ToString()
        End Get
    End Property


    Public Property TestPackageEntity() As TestPackageResourceEntity
        Get
            Return _testPackageEntity
        End Get
        Set(ByVal value As TestPackageResourceEntity)
            _testPackageEntity = value

            SetTestPackageEntityToPropertyEditors()
        End Set
    End Property



    Private Sub SetTestPackageEntityToPropertyEditors()
        For Each propertyEditor As TestPackageEditorControlBase In CurrentPropertyEditorControls
            propertyEditor.TestPackageEntity = _testPackageEntity
        Next
    End Sub


    Protected Sub OnCommandExecuteRequest(ByVal e As CommandExecuteRequestEventArgs)
        RaiseEvent CommandExecuteRequest(Me, e)
    End Sub
    Protected Sub CreateFrameForPropertyEditorAndAddToContainer(ByVal propEditor As TestPackageEditorControlBase, ByVal frameTitle As String)
        Dim groupBoxControl As New GroupBox
        groupBoxControl.Text = frameTitle
        groupBoxControl.Padding = New Padding(8)
        groupBoxControl.Margin = New Padding(8, 8, 8, 8)
        groupBoxControl.TabStop = True
        groupBoxControl.TabIndex = Me.Controls.Count + 1
        groupBoxControl.AutoSizeMode = AutoSizeMode.GrowOnly
        groupBoxControl.AutoSize = True
        groupBoxControl.Dock = DockStyle.Top

        groupBoxControl.Controls.Add(propEditor)
        propEditor.Dock = DockStyle.Top

        Me.Controls.Add(groupBoxControl)
        groupBoxControl.BringToFront()

        groupBoxControl.Visible = DirectCast(propEditor, ITestPackagePropertyEditor).HasFieldsToFillByUser
    End Sub

    Protected Sub OnDataChanged(ByVal e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub




    Public Sub HandleTestDesignPermissionChange(ByVal permission As TestDesignPermission)
        For Each propertyEditor As TestPackageEditorControlBase In CurrentPropertyEditorControls
            propertyEditor.HandleTestPackageDesignPermissionChange(permission)
        Next
    End Sub

    Public Sub ResetDatasource()
        For Each propertyEditor As TestPackageEditorControlBase In CurrentPropertyEditorControls
            propertyEditor.ResetDatasource()
        Next
    End Sub



    <Description("This event will be raised when data is changed on this control"),
Category("TestEditorControlBase events")>
    Public Event DataChanged As EventHandler(Of EventArgs)

    <Description("This event will be raised when an action is initiated from a property editor."),
Category("PropertyControl Base-events")>
    Public Event CommandExecuteRequest As EventHandler(Of CommandExecuteRequestEventArgs)


End Class

