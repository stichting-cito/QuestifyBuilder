Imports System.ComponentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Security
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class TestEditorControlBase
    Implements ITestEditorPropertyEditor


    Private _errorMessage As String



    <Description("This event will be raised when data is changed on this control"), Category("TestEditorControlBase events")>
    Public Event DataChanged As EventHandler(Of EventArgs) Implements ITestEditorPropertyEditor.DataChanged

    Protected Sub OnDataChanged(ByVal e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub

    <Description("This event will be raised when an action is initiated from a property editor."), Category("PropertyControl Base-events")>
    Public Event CommandExecuteRequest As EventHandler(Of CommandExecuteRequestEventArgs) Implements ITestEditorPropertyEditor.CommandExecuteRequest

    Protected Sub OnCommandExecuteRequest(ByVal e As CommandExecuteRequestEventArgs)
        RaiseEvent CommandExecuteRequest(Me, e)
    End Sub




    <Bindable(False)>
    Public ReadOnly Property ErrorMessage() As String Implements ITestEditorPropertyEditor.ErrorMessage
        Get
            Return _errorMessage
        End Get
    End Property

    Public ReadOnly Property ContainsValidationError() As Boolean Implements ITestEditorPropertyEditor.ContainsValidationError
        Get
            Return Not String.IsNullOrEmpty(_errorMessage)
        End Get
    End Property

    Public Property TestEntity() As AssessmentTestResourceEntity Implements ITestEditorPropertyEditor.TestEntity

    Public Overridable ReadOnly Property FrameTitle() As String Implements ITestEditorPropertyEditor.FrameTitle
        Get
            Throw New NotImplementedException("This property must be implemented by the sub-type editor and is shown as the grouping caption.")
        End Get
    End Property

    Public Overridable ReadOnly Property HasFieldsToFillByUser() As Boolean Implements ITestEditorPropertyEditor.HasFieldsToFillByUser
        Get
            Throw New NotImplementedException("This property must be implemented by the sub-type editor.")
        End Get
    End Property



    Public Sub ResetDatasource() Implements ITestEditorPropertyEditor.ResetDatasource
        ControlBindingSource.ResetCurrentItem()
    End Sub

    Public Overridable Sub HandleTestDesignPermissionChange(ByVal permission As TestDesignPermission) Implements ITestEditorPropertyEditor.HandleTestDesignPermissionChange
    End Sub



    Private Sub ControlBindingSource_CurrentItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ControlBindingSource.CurrentItemChanged
        OnDataChanged(New EventArgs)
    End Sub

End Class
