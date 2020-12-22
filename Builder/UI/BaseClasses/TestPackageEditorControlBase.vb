Imports System.ComponentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Security
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class TestPackageEditorControlBase
    Implements ITestPackagePropertyEditor



    Private _errorMessage As String
    Private _testPackageEntity As TestPackageResourceEntity




    <Description("This event will be raised when data is changed on this control"), Category("TestPackageEditorControlBase events")>
    Public Event DataChanged As EventHandler(Of EventArgs) Implements ITestPackagePropertyEditor.DataChanged

    Protected Sub OnDataChanged(ByVal e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub

    <Description("This event will be raised when an action is initiated from a property editor."), Category("PropertyControl Base-events")>
    Public Event CommandExecuteRequest As EventHandler(Of CommandExecuteRequestEventArgs) Implements ITestPackagePropertyEditor.CommandExecuteRequest

    Protected Sub OnCommandExecuteRequest(ByVal e As CommandExecuteRequestEventArgs)
        RaiseEvent CommandExecuteRequest(Me, e)
    End Sub




    <Bindable(False)>
    Public ReadOnly Property ErrorMessage() As String Implements ITestPackagePropertyEditor.ErrorMessage
        Get
            Return _errorMessage
        End Get
    End Property

    Public ReadOnly Property ContainsValidationError() As Boolean Implements ITestPackagePropertyEditor.ContainsValidationError
        Get
            Return Not String.IsNullOrEmpty(_errorMessage)
        End Get
    End Property

    Public Property TestPackageEntity() As TestPackageResourceEntity Implements ITestPackagePropertyEditor.TestPackageEntity
        Get
            Return _testPackageEntity
        End Get
        Set(ByVal value As TestPackageResourceEntity)
            _testPackageEntity = value
        End Set
    End Property

    Public Overridable ReadOnly Property FrameTitle() As String Implements ITestPackagePropertyEditor.FrameTitle
        Get
            Throw New NotImplementedException("This property must be implemented by the sub-type editor and is shown as the grouping caption.")
        End Get
    End Property

    Public Overridable ReadOnly Property HasFieldsToFillByUser() As Boolean Implements ITestPackagePropertyEditor.HasFieldsToFillByUser
        Get
            Throw New NotImplementedException("This property must be implemented by the sub-type editor.")
        End Get
    End Property



    Public Sub ResetDatasource() Implements ITestPackagePropertyEditor.ResetDatasource
        ControlBindingSource.ResetCurrentItem()
    End Sub

    Public Overridable Sub HandleTestPackageDesignPermissionChange(ByVal permission As TestDesignPermission) Implements ITestPackagePropertyEditor.HandleTestPackageDesignPermissionChange
    End Sub



    Private Sub ControlBindingSource_CurrentItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ControlBindingSource.CurrentItemChanged
        OnDataChanged(New EventArgs)
    End Sub

End Class
